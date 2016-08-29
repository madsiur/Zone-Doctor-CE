﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ZONEDOCTOR.Properties;

namespace ZONEDOCTOR
{
    public partial class Project : NewForm
    {
        #region Variables
        private Settings settings = Settings.Default;
        private List<EIndex> currentIndexes;
        private EIndex currentIndex;
        private EList elist
        {
            get
            {
                EList elist = (EList)listBoxLists.SelectedItem;
                elist = project.ELists.Find(delegate(EList ELIST)
                {
                    return ELIST.Name == elist.Name;
                });
                return elist;
            }
            set
            {
                EList elist = (EList)listBoxLists.SelectedItem;
                elist = project.ELists.Find(delegate(EList ELIST)
                {
                    return ELIST.Name == elist.Name;
                });
                elist = value;
            }
        }
        private ProjectDB project { get { return Model.Project; } set { Model.Project = value; } }
        public NewListView ElementIndexes { get { return elementIndexes; } set { elementIndexes = value; } }
        public NumericUpDown IndexNumber { get { return indexNumber; } set { indexNumber = value; } }
        public TextBox IndexLabel { get { return indexLabel; } set { indexLabel = value; } }
        public RichTextBox IndexDescription { get { return indexDescription; } set { indexDescription = value; } }
        private ListViewColumnSorter elementsColumnSorter = new ListViewColumnSorter();
        private ListViewColumnSorter listsColumnSorter = new ListViewColumnSorter();
        private int listIndex
        {
            get
            {
                if (listViewList.SelectedItems.Count == 0)
                    return -1;
                return Bits.GetInt32(listViewList.SelectedItems[0].SubItems[0].Text);
            }
        }
        private long checksum;
        private Overlay overlay = new Overlay();
        #endregion
        // constructor
        public Project()
        {
            InitializeComponent();
            this.elementIndexes.ListViewItemSorter = elementsColumnSorter;
            this.listViewList.ListViewItemSorter = listsColumnSorter;
            if (project == null)
                return;
            projectFile.Text = settings.NotePathCustom;
            InitializeFields();
        }
        #region Functions
        private void InitializeFields()
        {
            projectTitle.Text = project.Title;
            projectAuthor.Text = project.Author;
            projectDate.Text = project.Date;
            projectWebpage.Text = project.Webpage;
            projectDescription.Text = project.Description;
            projectOtherInfo.Text = project.OtherInfo;
            //
            if (elementType.SelectedIndex == 0)
                RefreshElementIndexes();
            else
                elementType.SelectedIndex = 0;
            listBoxLists.BeginUpdate();
            listBoxLists.Items.Clear();
            foreach (EList elist in Model.ELists)
                listBoxLists.Items.Add(elist);
            listBoxLists.EndUpdate();
            listBoxLists.SelectedIndex = 0;
            projectOtherInfo.Text = project.OtherInfo;
            //
            tabControl1.Enabled = true;
            closeButton.Enabled = true;
            save.Enabled = true;
            saveAs.Enabled = true;
            checksum = Do.GenerateChecksum(project);
        }
        private void RefreshElementIndexes()
        {
            this.Updating = true;
            panelAddressBit.Visible = false;
            panelIndexNumber.Visible = true;
            panelIndexNumber.BringToFront();
            elementIndexes.BeginUpdate();
            elementIndexes.Items.Clear();
            switch ((string)elementType.SelectedItem)
            {
                case "Event Scripts":
                    currentIndexes = project.EventScripts;
                    indexNumber.Maximum = 4095;
                    break;
                case "Locations":
                    currentIndexes = project.Locations;
                    indexNumber.Maximum = 414;
                    break;
                case "Memory Bits":
                    currentIndexes = project.MemoryBits;
                    panelIndexNumber.Visible = false;
                    panelAddressBit.Visible = true;
                    panelAddressBit.BringToFront();
                    break;
                default:
                    panel1.Enabled = false;
                    groupBox1.Enabled = false;
                    indexNumber.Value = 0;
                    indexLabel.Text = "";
                    indexDescription.Text = "";
                    elementIndexes.EndUpdate();
                    elementIndexes.EndUpdate();
                    this.Updating = false;
                    return;
            }
            panel1.Enabled = true;
            if (currentIndexes.Count == 0)
            {
                buttonDelete.Enabled = false;
                buttonMoveDown.Enabled = false;
                buttonMoveUp.Enabled = false;
                buttonLoad.Enabled = false;
                groupBox1.Enabled = false;
                indexNumber.Value = 0;
                address.Value = 0;
                indexLabel.Text = "";
                indexDescription.Text = "";
            }
            int counter = 0;
            List<ListViewItem> listViewItems = new List<ListViewItem>();
            foreach (EIndex index in currentIndexes)
            {
                ListViewItem lvitem = new ListViewItem(new string[]
                {
                    (elementType.SelectedItem == "Memory Bits" ? 
                    index.Address.ToString("X4") : index.Index.ToString()) + 
                    (elementType.SelectedItem == "Memory Bits" ? 
                    ":" + index.AddressBit.ToString() : ""),
                    index.Label
                });
                lvitem.Tag = counter++;
                listViewItems.Add(lvitem);
            }
            elementIndexes.Items.AddRange(listViewItems.ToArray());
            elementIndexes.EndUpdate();
            this.Updating = false;
        }
        private void RefreshIndex()
        {
            this.Updating = true;
            buttonDelete.Enabled = true;
            buttonMoveDown.Enabled = true;
            buttonMoveUp.Enabled = true;
            buttonLoad.Enabled = true;
            groupBox1.Enabled = true;
            currentIndex = (EIndex)currentIndexes[Do.GetSelectedIndex(elementIndexes)];
            indexNumber.Value = currentIndex.Index;
            indexLabel.Text = currentIndex.Label;
            indexDescription.Text = currentIndex.Description;
            address.Value = currentIndex.Address;
            addressBit.Value = currentIndex.AddressBit;
            this.Updating = false;
        }
        public bool LoadProject()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = settings.NotePathCustom;
            openFileDialog.Title = "Open existing project...";
            openFileDialog.FileName = Model.GetFileNameWithoutPath() + ".lsproj";
            openFileDialog.Filter = "Zone Doctor Project/Notes (*.lsproj; *.lsnotes)|*.lsproj;*.lsnotes";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return false;
            //
            Stream s = File.OpenRead(openFileDialog.FileName);
            BinaryFormatter b = new BinaryFormatter();
            try
            {
                string extension = Path.GetExtension(openFileDialog.FileName);
                if (extension == ".lsproj")
                {
                    project = (ProjectDB)b.Deserialize(s);
                }
                else if (extension == ".lsnotes")
                {
                    if (MessageBox.Show("This is a notes file -- in order to load this file it must be converted into a project.\n\n" +
                        "Continue loading file?", "ZONE DOCTOR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        s.Close();
                        return false;
                    }
                    NotesDB notes = (NotesDB)b.Deserialize(s);
                    project = CreateProject(notes);
                    openFileDialog.FileName = Path.ChangeExtension(openFileDialog.FileName, "lsproj");
                }
                if (project == null)
                {
                    MessageBox.Show("This is not a valid project file.", "ZONE DOCTOR", MessageBoxButtons.OK);
                    s.Close();
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("This is not a valid project file.", "ZONE DOCTOR", MessageBoxButtons.OK);
                s.Close();
                return false;
            }
            Model.RefreshListCollections();
            //
            settings.NotePathCustom = openFileDialog.FileName;
            projectFile.Text = openFileDialog.FileName;
            InitializeFields();
            return true;
        }
        private bool CreateNewProject()
        {
            if (project != null)
            {
                DialogResult result = MessageBox.Show("Save changes to currently loaded project?",
                    "ZONE DOCTOR", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                    SaveLoadedProject();
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = settings.NotePathCustom;
            saveFileDialog.Title = "Create new project...";
            saveFileDialog.FileName = Model.GetFileNameWithoutPath() + ".lsproj";
            saveFileDialog.Filter = "Zone Doctor Project (*.lsproj)|*.lsproj";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return false;
            //
            settings.NotePathCustom = saveFileDialog.FileName;
            Stream s = File.Create(saveFileDialog.FileName);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(s, new ProjectDB());
            s.Close();
            // now load the notes
            s = File.OpenRead(saveFileDialog.FileName);
            b = new BinaryFormatter();
            project = (ProjectDB)b.Deserialize(s);
            s.Close();
            projectFile.Text = saveFileDialog.FileName;
            InitializeFields();
            return true;
        }
        private void SaveNewProject(string path)
        {
            Model.ResetListCollections();
            Stream s = File.Create(path);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(s, new ProjectDB());
            s.Close();
        }
        private void SaveLoadedProject()
        {
            if (projectFile.Text == "")
            {
                SaveAsNewProject();
                return;
            }
            Model.RefreshListCollections();
            Stream s = File.Create(projectFile.Text);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(s, project);
            s.Close();
            checksum = Do.GenerateChecksum(project);
        }
        private void SaveAsNewProject()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = settings.NotePathCustom;
            saveFileDialog.Title = "Save as new project...";
            saveFileDialog.FileName = Model.GetFileNameWithoutPath() + ".lsproj";
            saveFileDialog.Filter = "Project DB (*.lsproj)|*.lsproj";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            settings.NotePathCustom = saveFileDialog.FileName;
            projectFile.Text = saveFileDialog.FileName;
            //
            Model.RefreshListCollections();
            Stream s = File.Create(saveFileDialog.FileName);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(s, project);
            s.Close();
            checksum = Do.GenerateChecksum(project);
        }
        private void AddNewIndex()
        {
            if (Do.GetSelectedIndex(elementIndexes) != -1)
                project.AddIndex(Do.GetSelectedIndex(elementIndexes) + 1, currentIndexes);
            else
                project.AddIndex(0, currentIndexes);
            int selectedIndex = Do.GetSelectedIndex(elementIndexes);
            RefreshElementIndexes();
            elementIndexes.Items[selectedIndex + 1].Selected = true;
        }
        public void AddingFromEditor(string type, int number, string label, string description)
        {
            string selectedItem = null;
            foreach (string item in elementType.Items)
                if (item.Trim() == type)
                    selectedItem = item;
            if (selectedItem == null)
                return;
            else
            {
                tabControl1.SelectedIndex = 2;
                elementType.SelectedItem = selectedItem;
            }
            if (elementIndexes.Items.Count > 0)
                elementIndexes.Items[elementIndexes.Items.Count - 1].Selected = true;
            AddNewIndex();
            indexNumber.Value = number;
            indexLabel.Text = label;
            indexDescription.Text = "";
        }
        public void AddingFromEditor(string type, int address, int addressBit, string label, string description)
        {
            string selectedItem = null;
            foreach (string item in elementType.Items)
                if (item.Trim() == type)
                    selectedItem = item;
            if (selectedItem == null)
                return;
            else
            {
                tabControl1.SelectedIndex = 2;
                elementType.SelectedItem = selectedItem;
            }
            if (elementIndexes.Items.Count > 0)
                elementIndexes.Items[elementIndexes.Items.Count - 1].Selected = true;
            AddNewIndex();
            this.address.Value = address;
            this.addressBit.Value = addressBit;
            indexLabel.Text = label;
            indexDescription.Text = "";
        }
        private void SortIndexes(int column)
        {
            int count = currentIndexes.Count;
            for (int y = 0; y < count - 1; y++)
            {
                for (int x = 0; x < count - 1 - y; x++)
                {
                    EIndex indexA = (EIndex)currentIndexes[x];
                    EIndex indexB = (EIndex)currentIndexes[x + 1];
                    if (column == 0)
                    {
                        if (elementType.SelectedItem.ToString() == "Memory Bits")
                        {
                            if ((indexB.Address.ToString() + indexB.AddressBit.ToString()).CompareTo(
                                (indexA.Address.ToString() + indexA.AddressBit.ToString())) < 0)
                                currentIndexes.Reverse(x, 2);
                        }
                        else
                        {
                            if (indexB.Index.CompareTo(indexA.Index) < 0)
                                currentIndexes.Reverse(x, 2);
                        }
                    }
                    else if (column == 1)
                    {
                        if (indexB.Label.CompareTo(indexA.Label) < 0)
                            currentIndexes.Reverse(x, 2);
                    }
                }
            }
        }
        // element lists
        private void RefreshElementList()
        {
            int index = listBoxLists.SelectedIndex;
            string[] list = elist.Labels;
            listViewList.BeginUpdate();
            listViewList.Items.Clear();
            List<ListViewItem> listViewItems = new List<ListViewItem>();
            int digits = list.Length.ToString().Length;
            for (int i = 0; i < list.Length; i++)
            {
                ListViewItem lvitem = new ListViewItem(new string[]
                {
                    i.ToString(), list[i]
                });
                listViewItems.Add(lvitem);
            }
            listViewList.Items.AddRange(listViewItems.ToArray());
            listViewList.EndUpdate();
            //
            listLabel.Text = "";
            listDescription.Text = "";
        }
        private void ExportList(bool exportAll)
        {
            if (listBoxLists.SelectedItem == null)
                return;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            if (exportAll)
                saveFileDialog.FileName = "listCollections";
            else
                saveFileDialog.FileName = "list" + (listBoxLists.SelectedItem.ToString()).Replace(" ", "");
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            //
            List<EList> listsToSave = new List<EList>();
            if (exportAll)
                foreach (EList item in listBoxLists.Items)
                    listsToSave.Add(item);
            else
                listsToSave.Add((EList)listBoxLists.SelectedItem);
            if (listsToSave.Count == 0)
                return;
            StreamWriter writer = File.CreateText(saveFileDialog.FileName);
            for (int a = 0; a < listsToSave.Count; a++)
            {
                string[] list = listsToSave[a].Labels;
                writer.WriteLine("[" + listsToSave[a].Name + "]");
                for (int i = 0; i < list.Length; i++)
                    writer.WriteLine("{" + i.ToString("d" + list.Length.ToString().Length) + "}  " + list[i]);
                writer.WriteLine();
            }
            writer.Close();
        }
        private void ImportList(bool importAll)
        {
            if (listBoxLists.SelectedItem == null)
                return;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            if (importAll)
                openFileDialog.FileName = "listCollections";
            else
                openFileDialog.FileName = listBoxLists.SelectedItem.ToString();
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            //
            TextReader reader = new StreamReader(openFileDialog.FileName);
            string[] listToRead = null;
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                // skip line if empty
                if (line == "")
                    continue;
                // if beginning of another list, set current list
                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    line = line.Substring(1, line.Length - 2);
                    EList elist = project.ELists.Find(delegate(EList list)
                    {
                        return list.Name == line;
                    });
                    if (elist != null)
                        listToRead = elist.Labels;
                    line = reader.ReadLine();
                }
                // if current list not set, continue
                if (listToRead == null)
                    continue;
                // get tagged index of line
                string tag = Regex.Match(line, "^[^ ]+").Value;
                // skip line completely if not tagged with index
                if (!tag.StartsWith("{") || !tag.EndsWith("}"))
                {
                    line = reader.ReadLine();
                    continue;
                }
                // remove tag from line
                line = line.Substring(tag.Length).Trim();
                int indexNumber = Bits.GetInt32(ref tag);
                string indexLabel = line.Trim();
                // skip line if index is out of bounds of list
                if (indexNumber >= listToRead.Length)
                    continue;
                listToRead[indexNumber] = indexLabel;
            }
            reader.Close();
        }
        private ProjectDB CreateProject(NotesDB notes)
        {
            ProjectDB project = new ProjectDB();
            project.OtherInfo = notes.GeneralNotes;
            foreach (NotesDB.Index index in notes.EventScripts) project.EventScripts.Add(new EIndex(index));
            foreach (NotesDB.Index index in notes.Levels) project.Locations.Add(new EIndex(index));
            foreach (NotesDB.Index index in notes.MemoryBits) project.MemoryBits.Add(new EIndex(index));
            return project;
        }
        #endregion
        #region Event Handlers
        private void Project_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (project == null)
                return;
            if (Do.GenerateChecksum(project) == checksum)
            {
                return;
            }
            DialogResult result = MessageBox.Show("Save changes to project?", "ZONE DOCTOR",
            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
                SaveLoadedProject();
            else if (result == DialogResult.Cancel)
                e.Cancel = true;
            else if (result == DialogResult.No && projectFile.Text != "")
            {
                // reload notes file
                try
                {
                    Stream s = File.OpenRead(projectFile.Text);
                    BinaryFormatter b = new BinaryFormatter();
                    project = (ProjectDB)b.Deserialize(s);
                    s.Close();
                }
                catch
                {
                    return;
                }
            }
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            SaveLoadedProject();
            this.Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (project != null)
            {
                DialogResult result = MessageBox.Show("Save changes to currently loaded notes?", "ZONE DOCTOR",
                    MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    SaveLoadedProject();
                if (result == DialogResult.Cancel)
                    return;
            }
            LoadProject();
        }
        // toolstrip
        private void new_Click(object sender, EventArgs e)
        {
            CreateNewProject();
        }
        private void load_Click(object sender, EventArgs e)
        {
            LoadProject();
        }
        private void closeButton_Click(object sender, EventArgs e)
        {
            Model.Project = null;
            Model.ResetListCollections();
            Model.Program.Project.Close();
            if (Model.Program.Project == null || !Model.Program.Project.Visible)
                Model.Program.CreateProjectWindow();
        }
        private void save_Click(object sender, EventArgs e)
        {
            SaveLoadedProject();
        }
        private void saveAs_Click(object sender, EventArgs e)
        {
            SaveAsNewProject();
        }
        private void alwaysOnTop_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = alwaysOnTop.Checked;
        }
        private void tagIndexesWithNumbers_Click(object sender, EventArgs e)
        {
            int selectedIndex = Do.GetSelectedIndex(elementIndexes);
            RefreshElementIndexes();
            elementIndexes.Items[selectedIndex].Selected = true;
        }
        // project information
        private void projectTitle_TextChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            project.Title = projectTitle.Text;
        }
        private void projectAuthor_TextChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            project.Author = projectAuthor.Text;
        }
        private void projectDate_TextChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            project.Date = projectDate.Text;
        }
        private void projectWebpage_TextChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            project.Webpage = projectWebpage.Text;
        }
        private void projectDescription_TextChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            project.Description = projectDescription.Text;
        }
        // element lists
        private void listBoxLists_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshElementList();
        }
        private void listViewList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewList.SelectedItems.Count == 0)
                return;
            listLabel.Text = elist.Indexes[listIndex].Label;
            listDescription.Text = elist.Indexes[listIndex].Description;
        }
        private void listViewList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Do.SortListView(listViewList, listsColumnSorter, e.Column);
        }
        private void addToElements_Click(object sender, EventArgs e)
        {
            if (listIndex < 0)
            {
                MessageBox.Show("Must select an item in the list before adding it to the notes.",
                    "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int number = elist.Indexes[listIndex].Index;
            string label = elist.Indexes[listIndex].Label;
            string description = elist.Indexes[listIndex].Description;
            tabControl1.SelectedIndex = 2;
            AddingFromEditor(elist.Name, number, label, description);
        }
        private void listLabel_TextChanged(object sender, EventArgs e)
        {
            if (listViewList.SelectedItems.Count == 0)
                return;
            if (elist == null)
                return;
            elist.Indexes[listIndex].Label = listLabel.Text;
            listViewList.SelectedItems[0].SubItems[1].Text = listLabel.Text;
        }
        private void listDescription_TextChanged(object sender, EventArgs e)
        {
            if (listViewList.SelectedItems.Count == 0)
                return;
            if (elist == null)
                return;
            elist.Indexes[listIndex].Description = listDescription.Text;
        }
        private void importCollection_Click(object sender, EventArgs e)
        {
            ImportList(true);
        }
        private void importList_Click(object sender, EventArgs e)
        {
            ImportList(false);
        }
        private void exportCollection_Click(object sender, EventArgs e)
        {
            ExportList(true);
        }
        private void exportList_Click(object sender, EventArgs e)
        {
            ExportList(false);
        }
        private void resetAllLists_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You are about to reset all lists in the current project to their default labels.\n\n" +
                "Continue with process?", "ZONE DOCTOR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            foreach (EList elist in project.ELists)
                elist.Reset();
            RefreshElementList();
        }
        private void resetCurrentList_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You are about to reset the current list to it's default labels.\n\n" +
                "Continue with process?", "ZONE DOCTOR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            elist.Reset();
            RefreshElementList();
        }
        // element notes
        private void transferToLists_Click(object sender, EventArgs e)
        {
            string item = (string)elementType.SelectedItem;
            EList elist = project.ELists.Find(delegate(EList list)
            {
                return list.Name == item;
            });
            if (elist == null)
                return;
            List<EIndex> eindexes = null;
            switch (item)
            {
                case "Event Scripts": eindexes = project.EventScripts; break;
                case "Locations": eindexes = project.Locations; break;
                default: break;
            }
            if (eindexes == null)
                return;
            foreach (EIndex eindex in eindexes)
            {
                elist.Indexes[eindex.Index].Label = eindex.Label;
            }
            RefreshElementList();
        }
        private void elementType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            RefreshElementIndexes();
            if (elementIndexes.Items.Count > 0)
                elementIndexes.Items[0].Selected = true;
        }
        private void elementIndexes_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            SortIndexes(e.Column);
            int selectedIndex = Do.GetSelectedIndex(elementIndexes);
            RefreshElementIndexes();
            elementIndexes.Items[selectedIndex].Selected = true;
        }
        private void elementIndexes_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected)
                return;
            if (this.Updating)
                return;
            RefreshIndex();
            elementIndexes.BeginUpdate();
            elementIndexes.Items[Do.GetSelectedIndex(elementIndexes)].EnsureVisible();
            elementIndexes.EndUpdate();
        }
        private void indexNumber_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            currentIndex.Index = (int)indexNumber.Value;
            int selectedIndex = Do.GetSelectedIndex(elementIndexes);
            RefreshElementIndexes();
            elementIndexes.Items[selectedIndex].Selected = true;
        }
        private void indexLabel_TextChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            currentIndex.Label = indexLabel.Text;
            int selectedIndex = Do.GetSelectedIndex(elementIndexes);
            RefreshElementIndexes();
            elementIndexes.Items[selectedIndex].Selected = true;
        }
        private void indexDescription_TextChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            currentIndex.Description = indexDescription.Text;
        }
        private void address_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            currentIndex.Address = (int)address.Value;
            int selectedIndex = Do.GetSelectedIndex(elementIndexes);
            RefreshElementIndexes();
            elementIndexes.Items[selectedIndex].Selected = true;
        }
        private void addressBit_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            currentIndex.AddressBit = (int)addressBit.Value;
            int selectedIndex = Do.GetSelectedIndex(elementIndexes);
            RefreshElementIndexes();
            elementIndexes.Items[selectedIndex].Selected = true;
        }
        private void generalNotes_TextChanged(object sender, EventArgs e)
        {
            project.OtherInfo = projectOtherInfo.Text;
        }
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            switch ((string)elementType.SelectedItem)
            {
                case "Event Scripts":
                    if (Model.Program.EventScripts == null || !Model.Program.EventScripts.Visible)
                        Model.Program.CreateEventScriptsWindow();
                    Model.Program.EventScripts.GotoAddress((int)indexNumber.Value);
                    Model.Program.EventScripts.BringToFront();
                    break;
                case "Locations":
                    if (Model.Program.Locations == null || !Model.Program.Locations.Visible)
                        Model.Program.CreateLocationsWindow();
                    Model.Program.Locations.LocationNum.Value = indexNumber.Value;
                    Model.Program.Locations.BringToFront();
                    break;
                case "Memory Bits":
                    break;
            }
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (Do.GetSelectedIndex(elementIndexes) == -1)
                return;
            project.DeleteIndex(Do.GetSelectedIndex(elementIndexes), currentIndexes);
            int selectedIndex = Do.GetSelectedIndex(elementIndexes);
            RefreshElementIndexes();
            if (currentIndexes.Count > 0)
                elementIndexes.Items[Math.Min(selectedIndex, currentIndexes.Count - 1)].Selected = true;
        }
        private void buttonMoveUp_Click(object sender, EventArgs e)
        {
            if (Do.GetSelectedIndex(elementIndexes) == 0)
                return;
            project.SwitchIndex(Do.GetSelectedIndex(elementIndexes) - 1, currentIndexes);
            int selectedIndex = Do.GetSelectedIndex(elementIndexes);
            RefreshElementIndexes();
            elementIndexes.Items[selectedIndex - 1].Selected = true;
        }
        private void buttonMoveDown_Click(object sender, EventArgs e)
        {
            if (Do.GetSelectedIndex(elementIndexes) >= elementIndexes.Items.Count - 1)
                return;
            project.SwitchIndex(Do.GetSelectedIndex(elementIndexes), currentIndexes);
            int selectedIndex = Do.GetSelectedIndex(elementIndexes);
            RefreshElementIndexes();
            elementIndexes.Items[selectedIndex + 1].Selected = true;
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddNewIndex();
        }
        #endregion
    }
}
