using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using ZONEDOCTOR.Properties;

namespace ZONEDOCTOR
{
    public partial class Editor : NewForm, IMRUClient
    {
        #region Variables
        private ProgramController AppControl;
        private Settings settings = Settings.Default;
        private bool cancelAnotherLoad;
        // MRU List manager
        private MRUManager mruManager;      // MRU list manager
        private string initialDirectory;    // Initial directory for Save/Load operations
        const string registryPath = "SOFTWARE\\ZONEDOCTOR\\ZoneDoctor";  // Registry path to keep persistent data
        [DllImport("advapi32.dll", EntryPoint = "RegDeleteKey")]
        public static extern int RegDeleteKeyA(int hKey, string lpSubKey);
        private Restore restore;
        public Panel Panel2 { get { return panel2; } set { panel2 = value; } }
        #endregion
        // Constructor
        public Editor(ProgramController controls)
        {
            this.AppControl = controls;
            //notes = Notes.Instance;
            InitializeComponent();
            Do.AddShortcut(toolStrip4, Keys.Control | Keys.S, new EventHandler(saveToolStripMenuItem_Click));
            loadRomTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            // MRU
            LoadSettingsFromRegistry();
            mruManager = new MRUManager();
            mruManager.Initialize(this, recentFiles, registryPath);
            //
            if (settings.LoadLastUsedROM && mruManager.MRUList.Count > 0)
            {
                try
                {
                    Open((string)mruManager.MRUList[0]);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Could not load most recently used ROM.\n\n" + e.Message,
                        Model.APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            // create backup list collections BEFORE loading notes
            if (Model.LevelNames == null)
            {
                Model.LevelNames = Lists.originalLocations;
            }

            Model.CreateListCollections();

            if (settings.LoadNotes && settings.NotePathCustom != "")
            {
                try
                {
                    OpenProject(settings.NotePathCustom);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Could not load most recently used project database.\n\n" + e.Message,
                        Model.APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            this.History = new History(this);
            settings.FirstLoad = true;
        }
        #region Function
        public static void GuiMain(ProgramController AppControl)
        {
            // Start the application.
            //Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.NoneEnabled;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Editor(AppControl));
        }
        // Loading
        private void Open(string filename)
        {
            if (AppControl.AssembleAndCloseWindows())
            {
                MessageBox.Show("All of the editor's windows must be closed before loading a new ROM.", Model.APPNAME);
                return;
            }
            else if (Model.HexEditor != null && Model.HexEditor.Visible)
                Model.HexEditor.Close();
            bool ret;
            if (filename == null) // Load the rom
                ret = AppControl.OpenRomFile();
            else
                ret = AppControl.OpenRomFile(filename);
            if (ret && !AppControl.Locked()) // Verify it is a SMRPG rom of the correct version
            {
                if (AppControl.GameCode() != "F6  ")
                {
                    MessageBox.Show("The game code for this ROM is invalid. There will likely be problems editing the ROM.", Model.APPNAME);
                    return;
                }
                toolStrip2.Enabled = true;
                toolStrip3.Enabled = true;
                foreach (ToolStripItem item in toolStrip4.Items)
                    if (item != recentFiles && item != openSettings)
                        item.Enabled = true;
                this.saveToolStripMenuItem.Enabled = true;
                this.saveAsToolStripMenuItem.Enabled = true;
                this.restoreElementsToolStripMenuItem.Enabled = true;
                AppControl.CreateNewMd5Checksum(); // Create a new checksum for a new rom
                UpdateRomInfo();
            }
            else if (ret)
            {
                if (AppControl.Locked())
                {
                    this.saveToolStripMenuItem.Enabled = true;
                    this.saveAsToolStripMenuItem.Enabled = true;
                    this.restoreElementsToolStripMenuItem.Enabled = true;
                    UpdateRomInfo();
                }
                toolStrip2.Enabled = false;
                toolStrip3.Enabled = false;
                foreach (ToolStripItem item in toolStrip4.Items)
                    if (item != recentFiles && item != openSettings)
                        item.Enabled = false;
            }
            if (ret)
            {
                mruManager.Add(AppControl.GetPathWithoutFileName() + AppControl.GetFileNameWithoutPath());
                settings.Save();
            }
            if (toolStrip2.Enabled && settings.LoadAllData)
                AppControl.LoadAll();
        }
        private void OpenProject(string filename)
        {
            if (!File.Exists(filename))
            {
                MessageBox.Show("Error loading last used database. The file has been moved, renamed, or no longer exists.",
                    Model.APPNAME);
                return;
            }
            Stream s = File.OpenRead(filename);
            BinaryFormatter b = new BinaryFormatter();
            Model.Project = (ProjectDB)b.Deserialize(s);
            Model.RefreshListCollections();
            s.Close();
        }
        private void CloseROM()
        {
            if (AppControl.AssembleAndCloseWindows())
                return;
            AppControl.CloseRomFile();
            toolStrip2.Enabled = false;
            toolStrip3.Enabled = false;
            foreach (ToolStripItem item in toolStrip4.Items)
                if (item != recentFiles && item != openSettings && item != info)
                    item.Enabled = false;
            this.loadRomTextBox.Text = "";
            this.romInfo.Text = "";
        }
        public void UpdateRomInfo()
        {
            this.loadRomTextBox.Text = Model.FileName;
            this.romInfo.Text =
                AppControl.GetRomName() + "\n" +
                AppControl.HeaderPresent() + "\n" +
                AppControl.RomChecksum() + "\n" +
                AppControl.GameCode();
        }
        // Closing
        private void FinalizeAndSave(FormClosingEventArgs e, int assembleFlag)
        {
            DialogResult result;
            if (e != null && AppControl.AssembleAndCloseWindows())
            {
                e.Cancel = true;
                return;
            }
            if (!AppControl.VerifyMD5Checksum())
            {
                result = MessageBox.Show(
                    "There are changes to the rom that have not been saved.\n\n" +
                    "Would you like to save them now" + (assembleFlag == 1 ? " and quit?" : "?"), Model.APPNAME,
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    if (!AppControl.SaveRomFile())
                    {
                        MessageBox.Show(
                            "There was an error saving to \"" + Model.FileName + "\"",
                           Model.APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (result == DialogResult.Cancel)
                {
                    if (e != null)
                        e.Cancel = true;
                    cancelAnotherLoad = true;
                    AppControl.Assemble();
                    return;
                }
                else cancelAnotherLoad = false;
            }
            if (e != null)
            {
                this.Dispose();
                Application.Exit();
            }
        }
        // Notes
        private string GetDirectoryPath(string caption)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.SelectedPath = settings.LastDirectory;
            folderBrowserDialog1.Description = caption;
            // Display the openFile dialog.
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                settings.LastDirectory = folderBrowserDialog1.SelectedPath;
                return folderBrowserDialog1.SelectedPath;
            }
            else
                return null;
        }
        // MRU list manager
        public void OpenMRUFile(string fileName)
        {
            try
            {
                Open(fileName);
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not load most recently used ROM(s).\n\n" + e.Message,
                        Model.APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadSettingsFromRegistry()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey(registryPath);
                initialDirectory = (string)key.GetValue(
                    "InitDir",                          // value name
                    Directory.GetCurrentDirectory());   // default value
            }
            catch
            {
                Trace.WriteLine("LoadSettingsFromRegistry failed");
            }
        }
        #endregion
        #region Event Handlers
        // Main buttons
        private void loadRom_Click(object sender, System.EventArgs e)
        {
            if (saveToolStripMenuItem.Enabled)
                FinalizeAndSave(null, 0);
            if (!cancelAnotherLoad)
                Open(null);
        }
        private void toolStrip1_SizeChanged(object sender, EventArgs e)
        {
            loadRomTextBox.Width = toolStrip1.Width - 95;
        }
        // toolstripMenuItems : File
        private void openToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (saveToolStripMenuItem.Enabled)
                FinalizeAndSave(null, 0);
            if (!cancelAnotherLoad)
                Open(null);
        }
        private void refreshROM_Click(object sender, EventArgs e)
        {
            if (saveToolStripMenuItem.Enabled)
                FinalizeAndSave(null, 0);
            if (!cancelAnotherLoad)
                Open(loadRomTextBox.Text);
        }
        private void closeROM_Click(object sender, EventArgs e)
        {
            if (saveToolStripMenuItem.Enabled)
                FinalizeAndSave(null, 0);
            if (!cancelAnotherLoad)
                CloseROM();
        }
        private void showROMInfo_Click(object sender, EventArgs e)
        {
            panel4.Visible = showROMInfo.Checked;
        }
        private void saveToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            // Check if read only, if it is do a "Save As" routine
            FileInfo file = new FileInfo(Model.FileName);
            if ((file.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                saveAsToolStripMenuItem.PerformClick();
                return;
            }
            // Check if currently in use by another application
            FileStream fs = null;
            try
            {
                fs = File.Open(Model.FileName, FileMode.Open);
                fs.Close();
            }
            catch
            {
                MessageBox.Show("Zone Doctor could not save the ROM.\n\nThe file is currently in use by another application.", Model.APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Now, save the file
            AppControl.SaveRomFile();
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppControl.Assemble();
            if (AppControl.SaveRomFileAs())
            {
                UpdateRomInfo();
                mruManager.Add(AppControl.GetPathWithoutFileName() + AppControl.GetFileNameWithoutPath());
                settings.Save();
            }
            else
                MessageBox.Show("Zone Doctor could not save the ROM.\n\nMake sure that the file is not currently in use by another appliaction.", Model.APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void restoreElementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppControl.AssembleAndCloseWindows())
            {
                MessageBox.Show("All of the editor's windows must be closed before importing data from another ROM.", Model.APPNAME);
                return;
            }
            restore = new Restore();
            restore.ShowDialog();
        }
        private void publishRomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppControl.Publish())
                UpdateRomInfo();
        }
        private void openSettings_Click(object sender, EventArgs e)
        {
            new SettingsEditor().ShowDialog();
        }
        // toolStripMenuitems : Help
        private void history_Click(object sender, EventArgs e)
        {
            NewMessage.Show("EVENT HISTORY - Zone Doctor",
                "This is a list of past actions performed by the user exclusively within the Zone Doctor application. " +
                "This is to be used for debugging purposes, chiefly for reproducing bugs and other defects encountered by the user.",
                Model.History, 600, 600, true);
        }
        private void hexViewer_Click(object sender, EventArgs e)
        {
            Model.HexEditor.Show();
            Model.HexEditor.BringToFront();
        }
        private void helpToolStripMenuItem1_Click(object sender, System.EventArgs e)
        {
            string path = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf('\\') + 1) + "helpTopics\\index.html";
            try
            {
                System.Diagnostics.Process.Start(path);
            }
            catch
            {
                MessageBox.Show("Could not load the index help file. Try unzipping the program's files again.", Model.APPNAME);
            }
        }
        private void aboutToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form about = new About(this);
            about.ShowDialog(this);
        }
        // other
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Model.Crashing)
                return;
            FinalizeAndSave(e, 1);
            settings.Save();
        }
        // Editor buttons
        private void openEventScripts_Click(object sender, System.EventArgs e)
        {
            AppControl.Scripts();
        }
        private void openLocations_Click(object sender, System.EventArgs e)
        {
            AppControl.Locations();
        }
        private void openProject_Click(object sender, EventArgs e)
        {
            AppControl.Project();
        }
        // window editing
        private void docking_Click(object sender, EventArgs e)
        {
            AppControl.DockEditors = docking.Checked;
            if (docking.Checked)
                AppControl.Dock();
            else
                AppControl.Undock();
        }
        private void openAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You are about to open all 15 editor windows.\n\nAre you sure you want to do this?",
                Model.APPNAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            AppControl.OpenAll();
        }
        private void closeAll_Click(object sender, EventArgs e)
        {
            AppControl.CloseAll();
        }
        private void minimizeAll_Click(object sender, EventArgs e)
        {
            AppControl.MinimizeAll();
        }
        private void restoreAll_Click(object sender, EventArgs e)
        {
            AppControl.RestoreAll();
        }
        private void loadAllData_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "You are about to reset the editor's memory of all elements. Continue?", Model.APPNAME,
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            AppControl.ClearAll();
            AppControl.LoadAll();
        }
        private void clearModel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "You are about to clear the editor's memory of all elements. Continue?", Model.APPNAME,
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            AppControl.ClearAll();
        }
        #endregion
    }
}