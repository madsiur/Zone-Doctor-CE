using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ZONEDOCTOR.Properties;
using ZONEDOCTOR.ScriptsEditor;

namespace ZONEDOCTOR
{
    public class Program
    {
        // class variables and accessors
        private Settings settings = Settings.Default;
        private bool dockEditors;
        public bool DockEditors { get { return dockEditors; } set { dockEditors = value; } }
        #region Editor Windows
        private Locations locations; public Locations Locations { get { return locations; } }
        private EventScripts eventScripts; public EventScripts EventScripts { get { return eventScripts; } }
        private Project project; public Project Project { get { return project; } }
        private Editor editor
        {
            get
            {
                if (Application.OpenForms.Count == 0)
                    return null;
                return (Editor)Application.OpenForms[0];
            }
            set
            {
                if (Application.OpenForms.Count == 0)
                    return;
                Editor main = (Editor)Application.OpenForms[0];
                main = value;
            }
        }
        #endregion
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            Program App = new Program();
        }
        // custom exception form
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Model.History += "***EXCEPTION*** " + e.Exception.Message + ")\n";
            new NewExceptionForm(e.Exception).ShowDialog();
        }
        // Constructor
        public Program()
        {
            Model.Program = this;
            ProgramController controls = new ProgramController(this);
            Editor.GuiMain(controls);
        }
        #region File Managing
        public bool OpenRomFile()
        {
            string filename;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = settings.LastRomPath;
            openFileDialog1.Title = "Select a Final Fantasy III/VI US ROM";
            openFileDialog1.Filter = "SMC files (*.SMC)|*.SMC|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                filename = openFileDialog1.FileName;
                Model.FileName = filename;
                if (Model.ReadRom())
                {
                    settings.LastRomPath = Model.GetPathWithoutFileName();
                    settings.Save();
                    return true;
                }
            }
            else
                filename = "";
            return false;
        }
        public bool OpenRomFile(string filename)
        {
            Model.FileName = filename;
            if (Model.ReadRom())
            {
                settings.LastRomPath = Model.GetPathWithoutFileName();
                settings.Save();
                return true;
            }
            return false;
        }
        public bool SaveRomFile()
        {
            Assemble();
            if (Model.WriteRom())
            {
                Model.CreateNewMD5Checksum();
                return true;
            }
            return false;
        }
        public bool SaveRomFileAs()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "SMC files (*.SMC)|*.SMC|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Model.FileName = saveFileDialog1.FileName;
                // Assemble all changes
                Assemble();
                if (Model.WriteRom())
                {
                    settings.LastRomPath = Model.GetPathWithoutFileName();
                    settings.Save();
                    Model.CreateNewMD5Checksum();
                    return true;
                }
                return false;
            }
            return true;
        }
        public void Assemble()
        {
            if (eventScripts != null && eventScripts.Visible)
                eventScripts.Assemble();
            if (locations != null && locations.Visible)
                locations.Assemble();
        }
        public void CloseRomFile()
        {
            Model.ROMHash = null;
        }
        #endregion
        #region Create Editor Windows
        public void CreateEventScriptsWindow()
        {
            if (eventScripts == null || !eventScripts.Visible)
            {
                Cursor.Current = Cursors.WaitCursor;
                eventScripts = new EventScripts();
                if (dockEditors) Do.AddControl(editor.Panel2, eventScripts);
                else eventScripts.Show();
                Cursor.Current = Cursors.Arrow;
            }
            eventScripts.BringToFront();
        }
        public void CreateLocationsWindow()
        {
            if (locations == null || !locations.Visible)
            {
                Cursor.Current = Cursors.WaitCursor;
                locations = new Locations();
                if (dockEditors) Do.AddControl(editor.Panel2, locations);
                else locations.Show();
                Cursor.Current = Cursors.Arrow;
            }
            locations.BringToFront();
        }
        public void CreateProjectWindow()
        {
            if (project == null || !project.Visible)
            {
                Cursor.Current = Cursors.WaitCursor;
                project = new Project();
                project.Show();
                Cursor.Current = Cursors.Arrow;
            }
        }
        #endregion
        public void Dock()
        {
            if (eventScripts != null && eventScripts.Visible)
                Do.AddControl(editor.Panel2, eventScripts);
            if (locations != null && locations.Visible)
                Do.AddControl(editor.Panel2, locations);
        }
        public void Undock()
        {
            if (eventScripts != null && eventScripts.Visible)
                Do.RemoveControl(eventScripts);
            if (locations != null && locations.Visible)
                Do.RemoveControl(locations);
        }
        public void OpenAll()
        {
            CreateEventScriptsWindow();
            CreateLocationsWindow();
        }
        public void MinimizeAll()
        {
            if (eventScripts != null && eventScripts.Visible)
                eventScripts.WindowState = FormWindowState.Minimized;
            if (locations != null && locations.Visible)
                locations.WindowState = FormWindowState.Minimized;
        }
        public void RestoreAll()
        {
            if (eventScripts != null && eventScripts.Visible)
                eventScripts.WindowState = FormWindowState.Normal;
            if (locations != null && locations.Visible)
                locations.WindowState = FormWindowState.Normal;
        }
        public bool CloseAll()
        {
            if (eventScripts != null && eventScripts.Visible)
                eventScripts.Close();
            if (locations != null && locations.Visible)
                locations.Close();
            if ((eventScripts != null && eventScripts.Visible) ||
                (locations != null && locations.Visible))
                return true;
            return false;
        }
        public void LoadAll()
        {
            Model.LoadAll();
        }
        public void ClearAll()
        {
            Model.ClearModel();
        }
    }
}