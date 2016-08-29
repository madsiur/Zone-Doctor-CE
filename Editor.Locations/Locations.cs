using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using ZONEDOCTOR.Properties;
using ZONEDOCTOR.Undo;

namespace ZONEDOCTOR
{
    public partial class Locations : NewForm
    {
        #region Variables
        public int index { get { return (int)locationNum.Value; } set { locationNum.Value = value; } }
        private Stack<int> navigateBackward = new Stack<int>();
        private Stack<int> navigateForward = new Stack<int>();
        private int lastNavigate = 0;
        private bool disableNavigate = false;
        private State state = State.Instance;
        private Settings settings = Settings.Default;
        private Overlay overlay = new Overlay();
        // elements
        private Location locationCheck; // to verify a location change
        private Location location { get { return locations[index]; } set { locations[index] = value; } }
        public Location LOCATION { get { return location; } set { location = value; } }
        private Location[] locations { get { return Model.Locations; } set { Model.Locations = value; } }
        private PaletteSet[] paletteSets { get { return Model.PaletteSets; } set { Model.PaletteSets = value; } }
        public System.Windows.Forms.ToolStripComboBox LocationName { get { return locationName; } set { locationName = value; } }
        // updating
        private bool initializeProperties = true;
        private bool initializeSettings = true;
        private bool closingEditor = false;
        private bool fullUpdate = false; // indicates that we need to do a complete update instead of a fast update
        private string fullPath; public string FullPath { set { fullPath = value; } }
        // control accessors
        public NewPictureBox picture { get { return tilemapEditor.Picture; } set { tilemapEditor.Picture = value; } }
        public NumericUpDown NPCSpriteSet { get { return npcSpriteSet; } set { npcSpriteSet = value; } }
        public ToolStripNumericUpDown LocationNum { get { return locationNum; } set { locationNum = value; } }
        public TabControl TabControl { get { return tabControl; } set { tabControl = value; } }
        // other windows
        private Search searchWindow;
        private EditLabel labelWindow;
        #endregion
        // constructor
        public Locations()
        {
            InitializeComponent();
            // LJ 2011-12-28: interoperability fix for FF3usME
            Model.LoadVarCompDataAbsPtrs();
            Model.DecompressWorldMaps();
            //
            this.locationInfo.Columns.AddRange(new ColumnHeader[] { new ColumnHeader(), new ColumnHeader() });
            this.analyzerInfo.Columns.AddRange(new ColumnHeader[] { new ColumnHeader(), new ColumnHeader() });
            Do.AddShortcut(toolStrip2, Keys.Control | Keys.S, new EventHandler(save_Click));
            Do.AddShortcut(toolStrip2, Keys.F1, help);
            Do.AddShortcut(toolStrip2, Keys.F2, baseConversion);
            searchWindow = new Search(locationNum, searchBox, searchLocationNames, locationName.Items);
            labelWindow = new EditLabel(locationName, locationNum, "Locations", true);
            new ToolTipLabel(this, baseConversion, help);
            this.locationName.Items.AddRange(Lists.Numerize(Lists.LocationNames));
            this.exitDestination.Items.AddRange(Lists.Numerize(Lists.LocationNames));
            for (int i = 0; i < 73; i++)
                this.messageName.Items.Add("{" + i.ToString("d3") + "}  " + Model.LocationNames[i]);
            this.musicName.Items.AddRange(Lists.Numerize(Lists.MusicNames));
            this.treasurePropertyName.Items.AddRange(Lists.Numerize(Model.ItemNames.Names));
            this.Refreshing = true;
            if (settings.RememberLastIndex)
            {
                locationNum.Value = settings.LastLocation;
                locationName.SelectedIndex = settings.LastLocation;
            }
            else
                locationName.SelectedIndex = 0;
            this.Refreshing = false;
            paletteSets = Model.PaletteSets;
            prioritySets = Model.PrioritySets;
            if (!this.Refreshing)
                RefreshLocation();
            //
            this.History = new History(this, locationName, locationNum);
            lastNavigate = index;
            this.Modified = false;
        }
        #region Methods
        public void RefreshLocation()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (index < 3)
                tabControl.SelectedIndex = 4;
            if (locationCheck != null && (locationCheck.Index != index || fullUpdate))
                initializeProperties = true;
            CreateNewLocation_Start();
        }
        private void LocationChange()
        {
            bool modified = this.Modified;
            // Code that must happen before a location changes goes here
            tilemap.Assemble(); // Assemble the edited tileMap into the Model
            ResetOverlay();
            RefreshLocation();
            if (tilesetEditor.Layer == 2 && locationMap.TilemapL3 == 0)
                tilesetEditor.Layer = 0;
            this.Modified = modified;
            GC.Collect();
        }
        private void CreateNewLocation_Start()
        {
            //CreateNewLocation_SetControls();
            //CreateNewLocation.WorkerReportsProgress = true;
            CreateNewLocation_DoWork(); //CreateNewLocation.RunWorkerAsync();
            CreateNewLocation_RunWorkerCompleted();
            //while (CreateNewLocation.IsBusy)
            //    Application.DoEvents();
        }
        private void CreateNewLocation_SetControls()
        {
            this.Enabled = false;
            this.locationNum.Enabled = false;
            this.locationName.Enabled = false;
            int i = 0;
            foreach (ToolStripItem item in toolStrip1.Items)
                if (i++ < 5)
                    continue;
                else
                    item.Visible = false;
            this.toolStrip2.Enabled = false;
            this.panelLocations.Enabled = false;
            this.searchWindow.Enabled = false;
            loadingLocation.Value = 0;
            loadingLocation.Visible = true;
            loadingLocationLabel.Visible = true;
        }
        private void CreateNewLocation_ResetControls()
        {
            this.Enabled = true;
            CreateNewLocation.WorkerReportsProgress = false;
            // set controls back to normal
            loadingLocation.Visible = false;
            loadingLocationLabel.Visible = false;
            loadingLocationLabel.Text = "";
            this.locationNum.Enabled = true;
            this.locationName.Enabled = true;
            int i = 0;
            foreach (ToolStripItem item in toolStrip1.Items)
            {
                if (i++ < 5)
                    continue;
                item.Visible = true;
            }
            this.toolStrip2.Enabled = true;
            this.panelLocations.Enabled = true;
            this.searchWindow.Enabled = true;
        }
        private void CreateNewLocation_DoWork()
        {
            if (locationCheck != null && locationCheck.Index == index && !fullUpdate)
            {
                tileset.RedrawTilesets(); // Redraw all tilesets
                tilemap.RedrawTilemap();
                return;
            }
            locationCheck = location;
            locationMap = location.LocationMap;
            if (index <= 2)
            {
                tileset = new Tileset(locationMap, paletteSet, TilesetType.World);
                tilemap = new WorldTilemap(location, tileset, CreateNewLocation);
                soliditySet = new SoliditySet(locationMap, true);
            }
            else
            {
                tileset = new Tileset(locationMap, paletteSet);
                tilemap = new LocationTilemap(location, tileset, CreateNewLocation);
                soliditySet = new SoliditySet(locationMap, false);
            }
            fullUpdate = false;
        }
        private void CreateNewLocation_DoWork(object sender, DoWorkEventArgs e)
        {
            CreateNewLocation_DoWork();
        }
        private void CreateNewLocation_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string label = "  " + (string)e.UserState;
            loadingLocation.Value = Math.Min(loadingLocation.Maximum, e.ProgressPercentage);
            loadingLocationLabel.Text = label;
        }
        private void CreateNewLocation_RunWorkerCompleted()
        {
            GC.Collect();
            this.Refreshing = true;
            SetLocationInfo();
            LoadTemplateEditor();
            LoadTilesetEditor();
            LoadTilemapEditor();
            if (initializeProperties)
            {
                LoadPaletteEditor();
                LoadGraphicEditor();
                InitializeLayerProperties();
                InitializeMapProperties();
                InitializeNPCProperties();
                InitializeTreasureProperties();
                InitializeExitProperties();
                InitializeEventProperties();
                initializeProperties = false;
            }
            // load the individual editors
            if (initializeSettings)
            {
                tilesetEditor.TopLevel = false;
                tilemapEditor.TopLevel = false;
                locationTemplate.TopLevel = false;
                tilesetEditor.Dock = DockStyle.Right;
                tilemapEditor.Dock = DockStyle.Fill;
                locationTemplate.Dock = DockStyle.Right;
                panelLocations.Controls.Add(tilesetEditor);
                panelLocations.Controls.Add(tilemapEditor);
                panelLocations.Controls.Add(locationTemplate);
                openTilemap.Checked = true;
                openTilemap_Click(null, null);
                openTileset.Checked = true;
                openTileset_Click(null, null);
                tilesetEditor.BringToFront();
                tilemapEditor.BringToFront();
                initializeSettings = false;
            }
            this.Refreshing = false;
            //CreateNewLocation_ResetControls();
            Cursor.Current = Cursors.Arrow;
        }
        private void CreateNewLocation_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CreateNewLocation_RunWorkerCompleted();
        }
        //
        private void ResetOverlay()
        {
            overlay.NPCImages = null;
        }
        private void SetLocationInfo()
        {
            List<string[]> items = new List<string[]>();
            items.Add(new string[] { "Location map", ((index * 33) + 0x2D8F00).ToString("X6") });
            items.Add(new string[] { "NPCs", ((index * 2) + (Bits.GetInt24(Model.ROM, 0x0052C3) - 0xC00000)).ToString("X6") });
            items.Add(new string[] { "Exits (short)", ((index * 2) + 0x1FBB00).ToString("X6") });
            items.Add(new string[] { "Exits (long)", ((index * 2) + 0x2DF480).ToString("X6") });
            items.Add(new string[] { "Events", ((index * 2) + 0x040000).ToString("X6") });
            items.Add(new string[] { "Treasures", ((index * 2) + 0x2D82F4).ToString("X6") });
            ListViewItem[] listViewItems = new ListViewItem[items.Count];
            for (int i = 0; i < items.Count; i++)
                listViewItems[i] = new ListViewItem(items[i]);
            locationInfo.Columns[0].Text = "Element";
            locationInfo.Columns[1].Text = "Offset";
            locationInfo.Columns[0].Width = locationInfo.Width / 2 - 4;
            locationInfo.Columns[1].Width = locationInfo.Width / 2 - 4;
            locationInfo.Items.Clear();
            locationInfo.Items.AddRange(listViewItems);
        }
        private void SetAnalyzerInfo()
        {
            LocationChange();
            //
            List<string[]> items = new List<string[]>();
            int free = Model.Compress(Model.Tilesets, null, null, 0x1FBA00, 0x1E0000, 0x1FB400, "TILE SET", 3, 0x800, false);
            items.Add(new string[] { "Tilesets", free.ToString() });
            free = Model.Compress(Model.Tilemaps, null, null, 0x19CD90, 0x19D1B0, 0x1E0000, "TILE MAP", 3, 0, true);
            items.Add(new string[] { "Tilemaps", free.ToString() });
            free = Model.Compress(Model.SoliditySets, null, null, 0x19CD10, 0x19A800, 0x19CD10, "SOLIDITY SET", 2, 0x200, false);
            items.Add(new string[] { "Solidity sets", free.ToString() });
            // create a progress bar
            ProgressBar progressBar = new ProgressBar(null, "COMPRESSING WORLD MAP DATA", 7);
            progressBar.Show();
            progressBar.PerformStep("COMPRESSING WORLD OF BIRTH TILE MAP");
            free = Model.Compress(Model.WOBTilemap, null, false, 0x2ED434, 0x2F114F, 0x10000, "WORLD OF BIRTH TILE MAP");
            items.Add(new string[] { "WOB tilemap", free.ToString() });
            free = Model.Compress(Model.WOBGraphicSet, null, false, 0x2F114F, 0x2F324F, 0x2480, "WORLD OF BIRTH GRAPHIC SET");
            items.Add(new string[] { "WOB graphics", free.ToString() });
            free = Model.Compress(Model.WORTilemap, null, false, 0x2F6A56, 0x2F9D17, 0x10000, "WORLD OF RUIN TILE MAP");
            items.Add(new string[] { "WOR tilemap", free.ToString() });
            free = Model.Compress(Model.WORGraphicSet, null, false, 0x2F4A46, 0x2F6A56, 0x2480, "WORLD OF RUIN GRAPHIC SET");
            items.Add(new string[] { "WOR graphics", free.ToString() });
            free = Model.Compress(Model.STTilemap, null, false, 0x2F9D17, 0x2FB631, 0x4000, "SERPENT TRENCH TILE MAP");
            items.Add(new string[] { "ST tilemap", free.ToString() });
            free = Model.Compress(Model.STGraphicSet, null, false, 0x2FB631, 0x2FC624, 0x2480, "SERPENT TRENCH GRAPHIC SET");
            items.Add(new string[] { "ST graphics", free.ToString() });
            free = Model.Compress(Model.STPaletteSet, null, false, 0x18E6BA, 0x18E800, 0x200, "SERPENT TRENCH PALETTE SET");
            items.Add(new string[] { "ST palette", free.ToString() });
            progressBar.Close();
            //
            ListViewItem[] listViewItems = new ListViewItem[items.Count];
            for (int i = 0; i < items.Count; i++)
                listViewItems[i] = new ListViewItem(items[i]);
            analyzerInfo.Columns[0].Text = "Element";
            analyzerInfo.Columns[1].Text = "Free bytes";
            analyzerInfo.Columns[0].Width = analyzerInfo.Width / 2 - 4;
            analyzerInfo.Columns[1].Width = analyzerInfo.Width / 2 - 4;
            analyzerInfo.Items.Clear();
            analyzerInfo.Items.AddRange(listViewItems);
        }
        private void NumeralsUpdate()
        {
            byte[] temp = new byte[0x2000];
            Model.Compress(Model.Numerals, temp, true, 0, 0xC00, 0x2000, "BATTLE NUMERALS");
            FileStream fs = new FileStream("numerals_comp.bin", FileMode.Create, FileAccess.ReadWrite);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(temp, 0, 0x2000);
            bw.Close();
            fs.Close();
        }
        // assemblers
        public void Assemble()
        {
            if (!this.Modified)
                return;
            LocationChange();
            //while (CreateNewLocation.IsBusy)
            //    Application.DoEvents();
            settings.Save();
            foreach (Location l in locations)
                l.Assemble();
            foreach (PrioritySet ps in prioritySets)
                ps.Assemble();
            foreach (PaletteSet ps in paletteSets)
                ps.Assemble(1);
            int temp = 0;
            if (exits.Count > 0)
                exits.CurrentExit = temp;
            int offsetShort = 0x0402;
            int offsetLong = 0x0402;
            if (CalculateFreeExitShortSpace() >= 0 && CalculateFreeExitLongSpace() >= 0)
            {
                for (int i = 0; i < 415; i++)
                    locations[i].LocationExits.Assemble(ref offsetShort, ref offsetLong);
            }
            else
                MessageBox.Show("Exit fields were not saved. " + MaximumSpaceExceeded("exits"), "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (exits.Count > 0)
                exits.CurrentExit = temp;
            int offsetStart = 0x0342;
            if (events.Count > 0)
                temp = events.CurrentEvent;
            if (CalculateFreeEventSpace() >= 0)
            {
                for (int i = 0; i < 415; i++)
                    locations[i].LocationEvents.Assemble(ref offsetStart);
            }
            else
                MessageBox.Show("Event fields were not saved. " + MaximumSpaceExceeded("events"), "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (events.Count > 0)
                events.CurrentEvent = temp;
            offsetStart = 0x0342;
            if (npcs.Count > 0)
                temp = npcs.CurrentNPC;
            if (CalculateFreeNPCSpace() >= 0)
            {
                for (int i = 0; i < 415; i++)
                    locations[i].LocationNPCs.Assemble(ref offsetStart);
            }
            else
                MessageBox.Show("NPCs were not saved. " + MaximumSpaceExceeded("NPCs"), "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (npcs.Count > 0)
                npcs.CurrentNPC = temp;
            offsetStart = 0;
            if (treasures.Treasures.Count > 0)
                temp = treasures.CurrentTreasure;
            if (CalculateFreeTreasureSpace() >= 0)
            {
                for (int i = 0; i < 415; i++)
                    locations[i].LocationTreasures.Assemble(ref offsetStart);
            }
            else
                MessageBox.Show("Treasures were not saved. " + MaximumSpaceExceeded("treasures"), "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (treasures.Treasures.Count > 0)
                treasures.CurrentTreasure = temp;
            //
            RecompressLocationData();
            Model.HexEditor.SetOffset((index * 33) + 0x2D8F00);
            Model.HexEditor.Compare();
            this.Modified = false;
        }
        private void RecompressLocationData()
        {
            // create a progress bar
            ProgressBar progressBar = new ProgressBar(Model.ROM, "COMPRESSING WORLD MAP DATA", 10);
            progressBar.Show();
            // start compressing world map data
            Bits.SetBytes(Model.ROM, 0x2E9B14, Model.WOBSolidity);
            progressBar.PerformStep("COMPRESSING WORLD OF BIRTH SOLIDITY SET");
            Bits.SetBytes(Model.ROM, 0x2E9D14, Model.WORSolidity);
            progressBar.PerformStep("COMPRESSING WORLD OF RUIN SOLIDITY SET");
            //
            if (Model.GetFileSize() == 0x300000)
            {
                // old code that corrupted world maps during FF3usME interoperability
                Model.Compress(Model.WOBTilemap, Model.ROM, Model.EditWOBTilemap, 0x2ED434, 0x2F114F, 0x10000, "WORLD OF BIRTH TILE MAP");
                progressBar.PerformStep("COMPRESSING WORLD OF BIRTH TILE MAP");
                Model.Compress(Model.WOBGraphicSet, Model.ROM, Model.EditWOBGraphicSet, 0x2F114F, 0x2F324F, 0x2480, "WORLD OF BIRTH GRAPHIC SET");
                progressBar.PerformStep("COMPRESSING WORLD OF BIRTH GRAPHIC SET");
                Model.Compress(Model.WORGraphicSet, Model.ROM, Model.EditWORGraphicSet, 0x2F4A46, 0x2F6A56, 0x2480, "WORLD OF RUIN GRAPHIC SET");
                progressBar.PerformStep("COMPRESSING WORLD OF RUIN GRAPHIC SET");
                Model.Compress(Model.WORTilemap, Model.ROM, Model.EditWORTilemap, 0x2F6A56, 0x2F9D17, 0x10000, "WORLD OF RUIN TILE MAP");
                progressBar.PerformStep("COMPRESSING WORLD OF RUIN TILE MAP");
                Model.Compress(Model.STTilemap, Model.ROM, Model.EditSTTilemap, 0x2F9D17, 0x2FB631, 0x4000, "SERPENT TRENCH TILE MAP");
                progressBar.PerformStep("COMPRESSING SERPENT TRENCH TILE MAP");
                Model.Compress(Model.STGraphicSet, Model.ROM, Model.EditSTGraphicSet, 0x2FB631, 0x2FC624, 0x2480, "SERPENT TRENCH GRAPHIC SET");
                progressBar.PerformStep("COMPRESSING SERPENT TRENCH GRAPHIC SET");
                Model.Compress(Model.STPaletteSet, Model.ROM, true, 0x18E6BA, 0x18E800, 0x200, "SERPENT TRENCH PALETTE SET");
                progressBar.PerformStep("COMPRESSING SERPENT TRENCH PALETTE SET");
            }
            else
                RecompressWorldMaps(progressBar);
            //
            progressBar.Close();
            // start compressing overworld data
            for (int i = 0; i < 82; i++)
            {
                int pointer = (i * 3) + 0x1FDA00;
                int offset = (int)(Bits.GetInt24(Model.ROM, pointer) + 0x1FDB00);
                Bits.SetBytes(Model.ROM, offset, Model.GraphicSets[i]);
            }
            Model.Compress(Model.SoliditySets, Model.ROM, Model.EditSoliditySets, 0x19CD10, 0x19A800, 0x19CD10, "SOLIDITY SET", 2, 0x200, false);
            Model.Compress(Model.Tilemaps, Model.ROM, Model.EditTilemaps, 0x19CD90, 0x19D1B0, 0x1E0000, "TILE MAP", 3, 0, true);
            Model.Compress(Model.Tilesets, Model.ROM, Model.EditTilesets, 0x1FBA00, 0x1E0000, 0x1FB400, "TILE SET", 3, 0x800, false);
            // LJ 2011-12-28: interoperability fix for FF3usME
            Model.SaveVarCompDataAbsPtrs();
        }
        private void RecompressWorldMaps(ProgressBar progressBar)
        {
            object dummy;
            dummy = Model.WOBMiniMap;
            dummy = Model.WORMiniMap;
            // LJ 2011-12-28: interoperability fix for FF3usME
            int m_expBank = 0xF1;
            int lSmcOffsetRegBank;
            int lSmcOffsetExpBank;
            int lRemBytesInRegBank;
            int lRemBytesInExpBank = 0x10000;
            // LJ: this preps the data pointer to the expanded bank, every section shall be stiched to each other, so this will be incremented in the following lines
            Model.m_savedExpandedBytes = 0;
            // lSmcOffsetExpBank = Model.OFFS_FF3ED_DTE_D_EX + Model.m_savedExpandedBytes;
            lSmcOffsetExpBank = ((m_expBank - 0xC0) * 0x10000) + Model.m_savedExpandedBytes;
            // LJ 2011-12-28: WOB map data and tile set
            lRemBytesInRegBank = Model.LEN_WOB_MAP_DT_TL; // LJ: this is remaining bytes for both mini-maps, for sure at least one of 'em 'll fit
            lSmcOffsetRegBank = Model.OFFS_WOB_MAP_DT_TL;
            //
            #region WOB Tilemap
            byte[] compressed = new byte[0x10000];
            int size = Comp.Compress(Model.WOBTilemap, compressed);
            // data bigger than remaining bytes in regular section
            if (size > lRemBytesInRegBank)
            {
                // file is expanded, park the data in expanded section
                if (Model.GetFileSize() == 0x400000)  // LJ: only deal with 32-Mbit expansion for the moment
                {
                    if (size <= lRemBytesInExpBank)
                    {
                        Model.m_varCompDataAbsPtrs[Model.IDX_VARP_MAP_WOB] = Model.SMCToHiROM((ulong)lSmcOffsetExpBank);
                        Bits.SetBytes(Model.ROM, lSmcOffsetExpBank, compressed, 0, size);
                        lSmcOffsetExpBank += size;
                        lRemBytesInExpBank -= size;
                    }
                    else
                    {
                        MessageBox.Show(
                           "Recompressed WOB tilemap exceeds allotted space in exp. bank.\n" +
                           "The WOB tilemap was not saved.",
                           "WARNING: NOT ENOUGH SPACE FOR WOB TILEMAP",
                           MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                // file is not expanded, there is nothing that we can do further
                else
                {
                    MessageBox.Show(
                        "Recompressed WOB tilemap exceeds allotted space.\n" +
                        "The WOB tilemap was not saved.",
                        "WARNING: NOT ENOUGH SPACE FOR WOB TILEMAP",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            // data fits in regular section
            else
            {
                lRemBytesInRegBank -= size;
                Model.m_varCompDataAbsPtrs[Model.IDX_VARP_MAP_WOB] = Model.SMCToHiROM((ulong)lSmcOffsetRegBank);
                Bits.SetBytes(Model.ROM, lSmcOffsetRegBank, compressed, 0, size);
                lSmcOffsetRegBank += size;
            }
            progressBar.PerformStep("COMPRESSING WOB TILEMAP");
            #endregion
            #region WOB Tileset/Graphic Set
            compressed = new byte[0x2480];
            size = Comp.Compress(Model.WOBGraphicSet, compressed);
            // data bigger than remaining bytes in regular section
            if (size > lRemBytesInRegBank)
            {
                // file is expanded, park the data in expanded section
                if (Model.GetFileSize() == 0x400000)  // LJ: only deal with 32-Mbit expansion for the moment
                {
                    if (size <= lRemBytesInExpBank)
                    {
                        Model.m_varCompDataAbsPtrs[Model.IDX_VARP_TILE_WOB] = Model.SMCToHiROM((ulong)lSmcOffsetExpBank);
                        Bits.SetBytes(Model.ROM, lSmcOffsetExpBank, compressed, 0, size);
                        lSmcOffsetExpBank += size;
                        lRemBytesInExpBank -= size;
                    }
                    else
                    {
                        MessageBox.Show(
                           "Recompressed WOB tileset exceeds allotted space in exp. bank.\n" +
                           "The WOB tileset was not saved.",
                           "WARNING: NOT ENOUGH SPACE FOR WOB TILESET",
                           MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                // file is not expanded, there is nothing that we can do further
                else
                {
                    MessageBox.Show(
                       "Recompressed WOB tileset exceeds allotted space.\n" +
                       "The WOB tileset was not saved.",
                       "WARNING: NOT ENOUGH SPACE FOR WOB TILESET",
                       MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            // data fits in regular section
            else
            {
                lRemBytesInRegBank -= size;
                Model.m_varCompDataAbsPtrs[Model.IDX_VARP_TILE_WOB] = Model.SMCToHiROM((ulong)lSmcOffsetRegBank);
                Bits.SetBytes(Model.ROM, lSmcOffsetRegBank, compressed, 0, size);
                lSmcOffsetRegBank += size;
            }
            progressBar.PerformStep("COMPRESSING WOB TILESET");
            #endregion
            #region WOR Tilemap
            // LJ 2011-12-28: WOR map Model.ROM and tile set
            lRemBytesInRegBank = Model.LEN_WOR_MAP_DT_TL; // LJ: this is remaining bytes for both mini-maps, for sure at least one of 'em 'll fit
            lSmcOffsetRegBank = Model.OFFS_WOR_MAP_DT_TL;
            // 
            compressed = new byte[0x10000];
            size = Comp.Compress(Model.WORTilemap, compressed);
            // data bigger than remaining bytes in regular section
            if (size > lRemBytesInRegBank)
            {
                // file is expanded, park the data in expanded section
                if (Model.GetFileSize() == 0x400000)  // LJ: only deal with 32-Mbit expansion for the moment
                {
                    if (size <= lRemBytesInExpBank)
                    {
                        Model.m_varCompDataAbsPtrs[Model.IDX_VARP_MAP_WOR] = Model.SMCToHiROM((ulong)lSmcOffsetExpBank);
                        Model.m_varCompDataAbsPtrs[Model.IDX_VARP_MAP_WOR2] = Model.SMCToHiROM((ulong)lSmcOffsetExpBank);
                        Bits.SetBytes(Model.ROM, lSmcOffsetExpBank, compressed, 0, size);
                        lSmcOffsetExpBank += size;
                        lRemBytesInExpBank -= size;
                    }
                    else
                    {
                        MessageBox.Show(
                           "Recompressed WOR tilemap exceeds allotted space in exp. bank.\n" +
                           "The WOR tilemap was not saved.",
                           "WARNING: NOT ENOUGH SPACE FOR WOR TILEMAP",
                           MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                // file is not expanded, there is nothing that we can do further
                else
                {
                    MessageBox.Show(
                        "Recompressed WOR tilemap exceeds allotted space.\n" +
                        "The WOR tilemap was not saved.",
                        "WARNING: NOT ENOUGH SPACE FOR WOR TILEMAP",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            // data fits in regular section
            else
            {
                lRemBytesInRegBank -= size;
                Model.m_varCompDataAbsPtrs[Model.IDX_VARP_MAP_WOR] = Model.SMCToHiROM((ulong)lSmcOffsetRegBank);
                Model.m_varCompDataAbsPtrs[Model.IDX_VARP_MAP_WOR2] = Model.SMCToHiROM((ulong)lSmcOffsetRegBank);
                Bits.SetBytes(Model.ROM, lSmcOffsetRegBank, compressed, 0, size);
                lSmcOffsetRegBank += size;
            }
            progressBar.PerformStep("COMPRESSING WOR TILEMAP");
            #endregion
            #region WOR Tileset/Graphic Set
            compressed = new byte[0x2480];
            size = Comp.Compress(Model.WORGraphicSet, compressed);
            // data bigger than remaining bytes in regular section
            if (size > lRemBytesInRegBank)
            {
                // file is expanded, park the data in expanded section
                if (Model.GetFileSize() == 0x400000)  // LJ: only deal with 32-Mbit expansion for the moment
                {
                    if (size <= lRemBytesInExpBank)
                    {
                        Model.m_varCompDataAbsPtrs[Model.IDX_VARP_TILE_WOR] = Model.SMCToHiROM((ulong)lSmcOffsetExpBank);
                        Bits.SetBytes(Model.ROM, lSmcOffsetExpBank, compressed, 0, size);
                        lSmcOffsetExpBank += size;
                        lRemBytesInExpBank -= size;
                    }
                    else
                    {
                        MessageBox.Show(
                           "Recompressed WOR tileset exceeds allotted space in exp. bank.\n" +
                           "The WOR tileset was not saved.",
                           "WARNING: NOT ENOUGH SPACE FOR WOR TILESET",
                           MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                // file is not expanded, there is nothing that we can do further
                else
                {
                    MessageBox.Show(
                       "Recompressed WOR tileset exceeds allotted space.\n" +
                       "The WOR tileset was not saved.",
                       "WARNING: NOT ENOUGH SPACE FOR WOR TILESET",
                       MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            // data fits in regular section
            else
            {
                lRemBytesInRegBank -= size;
                Model.m_varCompDataAbsPtrs[Model.IDX_VARP_TILE_WOR] = Model.SMCToHiROM((ulong)lSmcOffsetRegBank);
                Bits.SetBytes(Model.ROM, lSmcOffsetRegBank, compressed, 0, size);
                lSmcOffsetRegBank += size;
            }
            progressBar.PerformStep("COMPRESSING WOR TILESET");
            #endregion
            #region WOB mini-map
            // LJ 2011-12-28: Mini-maps data is now handled by FF3LE, though not modified yet
            lRemBytesInRegBank = Model.LEN_WOB_WOR_MMAP; // LJ: this is remaining bytes for both mini-maps, for sure at least one of 'em 'll fit
            lSmcOffsetRegBank = Model.OFFS_WOB_WOR_MMAP;
            // TODO: get the code from FF3Ed that auto-creates the mini-map from what the user had done to the map Model.ROM
            compressed = new byte[0x800];
            size = Comp.Compress(Model.WOBMiniMap, compressed);
            // data bigger than remaining bytes in regular section
            if (size > lRemBytesInRegBank)
            {
                // file is expanded, park the data in expanded section
                if (Model.GetFileSize() == 0x400000)  // LJ: only deal with 32-Mbit expansion for the moment
                {
                    if (size <= lRemBytesInExpBank)
                    {
                        Model.m_varCompDataAbsPtrs[Model.IDX_VARP_MMAP_WOB] = Model.SMCToHiROM((ulong)lSmcOffsetExpBank);
                        Bits.SetBytes(Model.ROM, lSmcOffsetExpBank, compressed, 0, size);
                        lSmcOffsetExpBank += size;
                        lRemBytesInExpBank -= size;
                    }
                    else
                    {
                        MessageBox.Show(
                            "Recompressed WOB mini-map exceeds allotted space in exp. bank.\n" +
                            "The WOB mini-map was not saved.",
                            "WARNING: NOT ENOUGH SPACE FOR WOB MINI-MAP",
                            MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                // file is not expanded, there is nothing that we can do further
                else
                {
                    MessageBox.Show(
                        "Recompressed WOB mini-map exceeds allotted space.\n" +
                        "The WOB mini-map was not saved.",
                        "WARNING: NOT ENOUGH SPACE FOR WOB MINI-MAP",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            // data fits in regular section
            else
            {
                lRemBytesInRegBank -= size;
                Model.m_varCompDataAbsPtrs[Model.IDX_VARP_MMAP_WOB] = Model.SMCToHiROM((ulong)lSmcOffsetRegBank);
                Bits.SetBytes(Model.ROM, lSmcOffsetRegBank, compressed, 0, size);
                lSmcOffsetRegBank += size;
            }
            progressBar.PerformStep("COMPRESSING WOB MINI-MAP...");
            #endregion
            #region WOR mini-map
            // TODO: get the code from FF3Ed that auto-creates the mini-map from what the user had done to the map Model.ROM
            compressed = new byte[0x800];
            size = Comp.Compress(Model.WORMiniMap, compressed);
            // data bigger than remaining bytes in regular section
            if (size > lRemBytesInRegBank)
            {
                // file is expanded, park the data in expanded section
                if (Model.GetFileSize() == 0x400000)  // LJ: only deal with 32-Mbit expansion for the moment
                {
                    if (size <= lRemBytesInExpBank)
                    {
                        Model.m_varCompDataAbsPtrs[Model.IDX_VARP_MMAP_WOR] = Model.SMCToHiROM((ulong)lSmcOffsetExpBank);
                        Bits.SetBytes(Model.ROM, lSmcOffsetExpBank, compressed, 0, size);
                        lSmcOffsetExpBank += size;
                        lRemBytesInExpBank -= size;
                    }
                    else
                    {
                        MessageBox.Show(
                            "Recompressed WOR mini-map exceeds allotted space in exp. bank.\n" +
                            "The WOR mini-map was not saved.",
                            "WARNING: NOT ENOUGH SPACE FOR WOR MINI-MAP",
                            MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                // file is not expanded, there is nothing that we can do further
                else
                {
                    MessageBox.Show(
                        "Recompressed WOR mini-map exceeds allotted space.\n" +
                        "The WOR mini-map was not saved.",
                        "WARNING: NOT ENOUGH SPACE FOR WOR MINI-MAP",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            // data fits in regular section
            else
            {
                lRemBytesInRegBank -= size;
                Model.m_varCompDataAbsPtrs[Model.IDX_VARP_MMAP_WOR] = Model.SMCToHiROM((ulong)lSmcOffsetRegBank);
                Bits.SetBytes(Model.ROM, lSmcOffsetRegBank, compressed, 0, size);
                lSmcOffsetRegBank += size;
            }
            progressBar.PerformStep("COMPRESSING WOR MINI-MAP...");
            #endregion
            // LJ 2011-12-28: we're finished taking expanded bytes for now, update the amount counter:
            // Model.m_savedExpandedBytes = lSmcOffsetExpBank - Model.OFFS_FF3ED_DTE_D_EX;
            Model.m_savedExpandedBytes = lSmcOffsetExpBank - ((m_expBank - 0xC0) * 0x10000);
        }
        public void RefreshLocationName()
        {
            this.Refreshing = true;
            locationName.BeginUpdate();
            locationName.Items.Clear();
            locationName.Items.AddRange(Lists.Numerize(Lists.LocationNames));
            locationName.SelectedIndex = index;
            locationName.EndUpdate();
            this.Refreshing = false;
        }
        private string MaximumSpaceExceeded(string name)
        {
            return
                "The total number of " + name + " for all locations has exceeded the maximum allotted space.\n\n" +
                "Try removing some " + name + " to increase the amount of free space for new " + name + ".";
        }
        private bool VerifyReset(string name)
        {
            if (MessageBox.Show("You're about to undo all changes to the current " + name + ". Go ahead with reset?",
                "ZONE DOCTOR", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return false;
            return true;
        }
        // directories
        private bool CreateDir(string dir)
        {
            DirectoryInfo di = new DirectoryInfo(dir);
            try
            {
                if (!di.Exists)
                {
                    di.Create();
                }
                return true;
            }
            catch
            {
                MessageBox.Show("Sorry, there was an error trying to create the directory : " + dir, "ZONE DOCTOR");
                return false;
            }
        }
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
        #endregion
        #region Event Handlers
        private void locationNum_ValueChanged(object sender, EventArgs e)
        {
            if (this.Refreshing)
                return;
            locationName.SelectedIndex = (int)locationNum.Value;
            picture.ZoomOut();
            LocationChange();
            locationNum.Focus();
            settings.LastLocation = (int)locationNum.Value;
            //
            if (!disableNavigate)
            {
                navigateBackward.Push(lastNavigate);
                navigateBck.Enabled = true;
                lastNavigate = index;
            }
        }
        private void locationName_SelectedIndexChanged(object sender, EventArgs e)
        {
            locationNum.Value = locationName.SelectedIndex;
        }
        private void navigateBck_Click(object sender, EventArgs e)
        {
            if (navigateBackward.Count < 1)
                return;
            navigateForward.Push(index);
            //
            this.Refreshing = true;
            index = navigateBackward.Peek();
            locationName.SelectedIndex = index;
            this.Refreshing = false;
            //
            LocationChange();
            locationNum.Focus();
            settings.LastLocation = (int)locationNum.Value;
            lastNavigate = index;
            navigateBackward.Pop();
            navigateBck.Enabled = navigateBackward.Count > 0;
            navigateFwd.Enabled = true;
        }
        private void navigateFwd_Click(object sender, EventArgs e)
        {
            if (navigateForward.Count < 1)
                return;
            navigateBackward.Push(index);
            //
            this.Refreshing = true;
            index = navigateForward.Peek();
            locationName.SelectedIndex = index;
            this.Refreshing = false;
            //
            LocationChange();
            locationNum.Focus();
            settings.LastLocation = (int)locationNum.Value;
            lastNavigate = index;
            navigateForward.Pop();
            navigateFwd.Enabled = navigateForward.Count > 0;
            navigateBck.Enabled = true;
        }
        // toolstrip menu items : File
        private void save_Click(object sender, EventArgs e)
        {
            Assemble();
        }
        private void importLocationDataAll_Click(object sender, EventArgs e)
        {
            IOElements ioElements = new IOElements(this, (int)locationNum.Value, "IMPORT LOCATION DATA...");
            if (ioElements.ShowDialog() == DialogResult.Cancel)
                return;
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLocation();
            if (CalculateFreeNPCSpace() < 0)
                MessageBox.Show("The total number of NPCs for all locations has exceeded the maximum allotted space.", "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (CalculateFreeTreasureSpace() < 0)
                MessageBox.Show("The total number of treasures for all locations has exceeded the maximum allotted space.", "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (CalculateFreeExitShortSpace() < 0)
                MessageBox.Show("The total number of short exit fields for all locations has exceeded the maximum allotted space.", "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (CalculateFreeExitLongSpace() < 0)
                MessageBox.Show("The total number of long exit fields for all locations has exceeded the maximum allotted space.", "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (CalculateFreeEventSpace() < 0)
                MessageBox.Show("The total number of event fields for all locations has exceeded the maximum allotted space.", "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void exportLocationDataAll_Click(object sender, EventArgs e)
        {
            new IOElements(this, (int)locationNum.Value, "EXPORT LOCATION DATA...").ShowDialog();
        }
        private void importArchitectureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (new IOArchitecture("import", index, locationMap, paletteSet, tileset, tilemap, prioritySets[locationMap.PrioritySet]).ShowDialog() == DialogResult.Cancel)
                return;
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLocation();
        }
        private void exportArchitectureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new IOArchitecture("export", index, locationMap, paletteSet, tileset, tilemap, prioritySets[locationMap.PrioritySet]).ShowDialog();
        }
        private void exportLocationImagesAll_Click(object sender, EventArgs e)
        {
            ExportImages exportImages = new ExportImages(index);
            exportImages.ShowDialog();
        }
        private void spaceAnalyzer_DropDownOpening(object sender, EventArgs e)
        {
            SetAnalyzerInfo();
        }
        private void clearLocationDataAll_Click(object sender, EventArgs e)
        {
            if (new ClearElements(null, (int)locationNum.Value, "CLEAR LOCATION DATA...", index).ShowDialog() == DialogResult.Cancel)
                return;
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLocation();
        }
        private void clearTilesetsAll_Click(object sender, EventArgs e)
        {
            if (new ClearElements(null, (int)mapTilesetL1Num.Value, "CLEAR TILESETS...", index).ShowDialog() == DialogResult.Cancel)
                return;
            new ClearElements(null, (int)mapTilesetL2Num.Value, "CLEAR TILESETS...", index).AcceptButton.PerformClick();
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLocation();
        }
        private void clearTilemapsAll_Click(object sender, EventArgs e)
        {
            if (locationMap.TilemapL1 != 0)
                if (new ClearElements(null, (int)mapTilemapL1Num.Value, "CLEAR TILEMAPS...", index).ShowDialog() == DialogResult.Cancel)
                    return;
            if (locationMap.TilemapL2 != 0)
                new ClearElements(null, (int)mapTilemapL2Num.Value, "CLEAR TILEMAPS...", index).AcceptButton.PerformClick();
            if (locationMap.TilemapL3 != 0)
                new ClearElements(null, (int)mapTilemapL3Num.Value, "CLEAR TILEMAPS...", index).AcceptButton.PerformClick();
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLocation();
        }
        private void clearPhysicalMapsAll_Click(object sender, EventArgs e)
        {
            if (new ClearElements(null, (int)mapPhysicalMapNum.Value, "CLEAR SOLIDITY SETS...", index).ShowDialog() == DialogResult.Cancel)
                return;
            fullUpdate = true;
        }
        private void clearAllComponentsAll_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "You are about to clear all location data, tilesets, tilemaps, physical maps and battlefields.\n" +
                "This will essentially wipe the slate clean for anything having to do with locations.\n\n" +
                "Are you sure you want to do this?", "ZONE DOCTOR", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
                return;
            for (int i = 0; i < 415; i++)
            {
                locations[i].LocationMap.Clear();
                locations[i].LocationEvents.Clear();
                locations[i].LocationExits.Clear();
                locations[i].LocationNPCs.Clear();
                locations[i].LocationNPCs.Clear();
            }
            for (int i = 0; i < Model.Tilesets.Length; i++)
            {
                Model.Tilesets[i] = new byte[0x800];
                Model.EditTilesets[i] = true;
            }
            for (int i = 0; i <= Model.Tilemaps.Length; i++)
            {
                Model.Tilemaps[i] = new byte[Model.TilemapSizes[i]];
                Model.EditTilemaps[i] = true;
            }
            for (int i = 0; i < Model.SoliditySets.Length; i++)
            {
                Model.SoliditySets[i] = new byte[0x200];
                Model.EditSoliditySets[i] = true;
            }
            Model.WOBGraphicSet = new byte[0x2480];
            Model.WOBSolidity = new byte[0x200];
            Model.WOBTilemap = new byte[0x10000];
            Model.EditWOBGraphicSet = true;
            Model.EditWOBTilemap = true;
            Model.WORGraphicSet = new byte[0x2480];
            Model.WORSolidity = new byte[0x200];
            Model.WORTilemap = new byte[0x10000];
            Model.EditWORGraphicSet = true;
            Model.EditWORTilemap = true;
            Model.STGraphicSet = new byte[0x2480];
            Model.STTilemap = new byte[0x4000];
            Model.STPaletteSet = new byte[0x200];
            Model.EditSTGraphicSet = true;
            Model.EditSTTilemap = true;
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLocation();
        }
        private void clearAllComponentsCurrent_Click(object sender, EventArgs e)
        {
            location.LocationMap.Clear();
            location.LocationEvents.Clear();
            location.LocationExits.Clear();
            location.LocationNPCs.Clear();
            if (index >= 3)
            {
                Model.Tilesets[locationMap.TilesetL1] = new byte[0x800];
                Model.Tilesets[locationMap.TilesetL2] = new byte[0x800];
                Model.EditTilesets[locationMap.TilesetL1] = true;
                Model.EditTilesets[locationMap.TilesetL2] = true;
                Model.Tilemaps[locationMap.TilemapL1] = new byte[Model.TilemapSizes[locationMap.TilemapL1]];
                Model.Tilemaps[locationMap.TilemapL2] = new byte[Model.TilemapSizes[locationMap.TilemapL2]];
                Model.Tilemaps[locationMap.TilemapL3] = new byte[0x1000];
                Model.EditTilemaps[locationMap.TilemapL1] = true;
                Model.EditTilemaps[locationMap.TilemapL2] = true;
                Model.EditTilemaps[locationMap.TilemapL3] = true;
            }
            else if (index == 0)
            {
                Model.WOBGraphicSet = new byte[0x2480];
                Model.WOBSolidity = new byte[0x200];
                Model.WOBTilemap = new byte[0x10000];
                Model.EditWOBGraphicSet = true;
                Model.EditWOBTilemap = true;
            }
            else if (index == 1)
            {
                Model.WORGraphicSet = new byte[0x2480];
                Model.WORSolidity = new byte[0x200];
                Model.WORTilemap = new byte[0x10000];
                Model.EditWORGraphicSet = true;
                Model.EditWORTilemap = true;
            }
            else if (index == 2)
            {
                Model.STGraphicSet = new byte[0x2480];
                Model.STTilemap = new byte[0x4000];
                Model.STPaletteSet = new byte[0x200];
                Model.EditSTGraphicSet = true;
                Model.EditSTTilemap = true;
            }
            soliditySet.Clear(1);
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLocation();
        }
        private void unusedGraphicSetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "You are about to clear all UNUSED graphic sets.\n\n" +
                "Do you wish to continue?",
                "ZONE DOCTOR", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;
            // Clear unused tilesets
            bool[] used = new bool[Model.GraphicSets.Length];
            LocationMap lm;
            foreach (Location loc in locations)
            {
                lm = loc.LocationMap;
                used[lm.GraphicSetA] = true;
                used[lm.GraphicSetB] = true;
                used[lm.GraphicSetC] = true;
                used[lm.GraphicSetD] = true;
                used[lm.GraphicSetL3] = true;
            }
            for (int i = 0; i < Model.Tilesets.Length; i++)
            {
                if (!used[i])
                {
                    Model.GraphicSets[i] = new byte[Model.GraphicSets[i].Length];
                    Model.EditGraphicSets[i] = true;
                }
            }
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLocation();
        }
        private void unusedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "You are about to clear all UNUSED tilesets.\n\n" +
                "Do you wish to continue?",
                "ZONE DOCTOR", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;
            // Clear unused tilesets
            bool[] used = new bool[Model.Tilesets.Length];
            LocationMap layer;
            foreach (Location lv in locations)
            {
                layer = lv.LocationMap;
                used[layer.TilesetL1] = true;
                used[layer.TilesetL2] = true;
            }
            for (int i = 0; i < Model.Tilesets.Length; i++)
            {
                if (!used[i])
                {
                    Model.Tilesets[i] = new byte[i < 0x20 ? 0x1000 : 0x2000];
                    Model.EditTilesets[i] = true;
                }
            }
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLocation();
        }
        private void unusedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
              "You are about to clear all UNUSED tilemaps.\n\n" +
              "Do you wish to continue?",
              "ZONE DOCTOR", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;
            // Clear unused tilemaps
            bool[] used = new bool[Model.Tilemaps.Length];
            LocationMap layer;
            foreach (Location lv in locations)
            {
                layer = lv.LocationMap;
                used[layer.TilemapL1] = true;
                used[layer.TilemapL2] = true;
                used[layer.TilemapL3] = true;
            }
            for (int i = 0; i < Model.Tilemaps.Length; i++)
            {
                if (!used[i])
                {
                    Model.Tilemaps[i] = new byte[i < 0x40 ? 0x1000 : 0x2000];
                    Model.EditTilemaps[i] = true;
                }
            }
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLocation();
        }
        private void unusedToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
              "You are about to clear all UNUSED solidity sets.\n\n" +
              "Do you wish to continue?",
              "ZONE DOCTOR", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;
            // Clear unused physical maps
            bool[] used = new bool[Model.SoliditySets.Length];
            LocationMap layer;
            foreach (Location lv in locations)
            {
                layer = lv.LocationMap;
                used[layer.SoliditySet] = true;
            }
            for (int i = 0; i < Model.SoliditySets.Length; i++)
            {
                if (!used[i])
                {
                    Model.SoliditySets[i] = new byte[0x200];
                    Model.EditSoliditySets[i] = true;
                }
            }
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLocation();
        }
        private void unusedToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            // Clear all unused components
            unusedGraphicSetsToolStripMenuItem.PerformClick();
            unusedToolStripMenuItem.PerformClick();
            unusedToolStripMenuItem1.PerformClick();
            unusedToolStripMenuItem2.PerformClick();
        }
        private void arraysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fullPath = GetDirectoryPath("Select directory to export arrays to...");
            fullPath += "\\" + Model.GetFileNameWithoutPath() + " - Arrays\\";
            // Create Location Data directory
            if (!CreateDir(fullPath))
                return;
            FileStream fs;
            BinaryWriter bw;
            for (int i = 0; i < Model.GraphicSets.Length; i++)
            {
                CreateDir(fullPath + "Graphic Sets\\");
                fs = new FileStream(fullPath + "Graphic Sets\\graphicSet." + i.ToString("d3") + ".bin", FileMode.Create, FileAccess.ReadWrite);
                bw = new BinaryWriter(fs);
                bw.Write(Model.GraphicSets[i], 0, Model.GraphicSets[i].Length);
                bw.Close();
                fs.Close();
            }
            for (int i = 0; i < Model.SoliditySets.Length; i++)
            {
                CreateDir(fullPath + "Solidity Sets\\");
                fs = new FileStream(fullPath + "Solidity Sets\\soliditySet." + i.ToString("d3") + ".bin", FileMode.Create, FileAccess.ReadWrite);
                bw = new BinaryWriter(fs);
                bw.Write(Model.SoliditySets[i], 0, Model.SoliditySets[i].Length);
                bw.Close();
                fs.Close();
            }
            for (int i = 0; i < Model.Tilemaps.Length; i++)
            {
                CreateDir(fullPath + "Tile Maps\\");
                fs = new FileStream(fullPath + "Tile Maps\\tileMap." + i.ToString("d3") + ".bin", FileMode.Create, FileAccess.ReadWrite);
                bw = new BinaryWriter(fs);
                bw.Write(Model.Tilemaps[i], 0, Model.Tilemaps[i].Length);
                bw.Close();
                fs.Close();
            }
            for (int i = 0; i < Model.Tilesets.Length; i++)
            {
                CreateDir(fullPath + "Tile Sets\\");
                fs = new FileStream(fullPath + "Tile Sets\\tileSet." + i.ToString("d3") + ".bin", FileMode.Create, FileAccess.ReadWrite);
                bw = new BinaryWriter(fs);
                bw.Write(Model.Tilesets[i], 0, Model.Tilesets[i].Length);
                bw.Close();
                fs.Close();
            }
        }
        private void arraysToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fullPath = GetDirectoryPath("Select directory to import arrays from...");
            fullPath += "\\";
            FileStream fs;
            BinaryReader br;
            try
            {
                // Create the file to store the location data
                for (int i = 0; i < Model.GraphicSets.Length; i++)
                {
                    if (!File.Exists(fullPath + "Graphic Sets\\graphicSet." + i.ToString("d3") + ".bin"))
                        continue;
                    fs = File.OpenRead(fullPath + "Graphic Sets\\graphicSet." + i.ToString("d3") + ".bin");
                    br = new BinaryReader(fs);
                    Model.GraphicSets[i] = br.ReadBytes(Model.GraphicSets[i].Length);
                    br.Close();
                    fs.Close();
                    Model.EditGraphicSets[i] = true;
                }
                for (int i = 0; i < Model.SoliditySets.Length; i++)
                {
                    if (!File.Exists(fullPath + "Solidity Sets\\soliditySet." + i.ToString("d3") + ".bin"))
                        continue;
                    fs = File.OpenRead(fullPath + "Solidity Sets\\soliditySet." + i.ToString("d3") + ".bin");
                    br = new BinaryReader(fs);
                    Model.SoliditySets[i] = br.ReadBytes(Model.SoliditySets[i].Length);
                    br.Close();
                    fs.Close();
                    Model.EditSoliditySets[i] = true;
                }
                for (int i = 0; i < Model.Tilemaps.Length; i++)
                {
                    if (!File.Exists(fullPath + "Tile Maps\\tileMap." + i.ToString("d3") + ".bin"))
                        continue;
                    fs = File.OpenRead(fullPath + "Tile Maps\\tileMap." + i.ToString("d3") + ".bin");
                    br = new BinaryReader(fs);
                    Model.Tilemaps[i] = br.ReadBytes(Model.Tilemaps[i].Length);
                    br.Close();
                    fs.Close();
                    Model.EditTilemaps[i] = true;
                }
                for (int i = 0; i < Model.Tilesets.Length; i++)
                {
                    if (!File.Exists(fullPath + "Tile Sets\\tileSet." + i.ToString("d3") + ".bin"))
                        continue;
                    fs = File.OpenRead(fullPath + "Tile Sets\\tileSet." + i.ToString("d3") + ".bin");
                    br = new BinaryReader(fs);
                    Model.Tilesets[i] = br.ReadBytes(Model.Tilesets[i].Length);
                    br.Close();
                    fs.Close();
                    Model.EditTilesets[i] = true;
                }
                fullUpdate = true;
                RefreshLocation();
            }
            catch
            {
                MessageBox.Show("There was a problem importing the arrays.", "ZONE DOCTOR");
            }
        }
        private void graphicSetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BinaryWriter binWriter;
            string path = GetDirectoryPath("Where do you want to save the graphic sets?");
            path += "\\" + Model.GetFileNameWithoutPath() + " - Graphic Sets\\";
            if (!CreateDir(path))
                return;
            if (path == null)
                return;
            try
            {
                for (int i = 0; i < Model.GraphicSets.Length; i++)
                {
                    binWriter = new BinaryWriter(File.Open(path + "graphicSet." + i.ToString("d3") + ".bin", FileMode.Create));
                    binWriter.Write(Model.GraphicSets[i]);
                    binWriter.Close();
                }
            }
            catch (Exception ioexc)
            {
                MessageBox.Show("Zone Doctor was unable to save the graphic sets.\n\n" + ioexc.Message, "ZONE DOCTOR");
            }
        }
        private void graphicSetsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string filename;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = settings.LastRomPath;
            openFileDialog1.Title = "Import graphic set";
            openFileDialog1.Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
                filename = openFileDialog1.FileName;
            else
                return;
            string num = filename.Substring(filename.LastIndexOf('.') - 2, 2);
            int index = Int32.Parse(num, System.Globalization.NumberStyles.HexNumber);
            try
            {
                FileInfo fInfo = new FileInfo(filename);
                if (fInfo.Length != 8192)
                {
                    MessageBox.Show("File is incorrect size, Graphic Sets are 8192 bytes", "ZONE DOCTOR");
                    return;
                }
                FileStream fStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fStream);
                Model.GraphicSets[index] = br.ReadBytes((int)fInfo.Length);
                Model.EditGraphicSets[index] = true;
                br.Close();
                fStream.Close();
                fullUpdate = true;
                RefreshLocation();
                return;
            }
            catch (Exception ioexc)
            {
                MessageBox.Show("Zone Doctor was unable to Import the Graphic Set.\n\n" + ioexc.Message, "ZONE DOCTOR");
                return;
            }
        }
        // reset
        private void resetLocationMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!VerifyReset("location map"))
                return;
            locationMap = new LocationMap(index);
            locationNum_ValueChanged(null, null);
        }
        private void resetNPCDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!VerifyReset("NPCs"))
                return;
            npcs = new LocationNPCs(index);
            overlay.NPCImages = null;
            InitializeNPCProperties();
        }
        private void resetTreasuresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!VerifyReset("treasures"))
                return;
            treasures = new LocationTreasures(index);
            InitializeTreasureProperties();
        }
        private void resetEventDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!VerifyReset("events"))
                return;
            events = new LocationEvents(index);
            InitializeEventProperties();
        }
        private void resetExitDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!VerifyReset("exits"))
                return;
            exits = new LocationExits(index);
            InitializeExitProperties();
        }
        private void resetGraphicSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!VerifyReset("graphic sets"))
                return;
            //
            if (index == 0)
                Model.WOBGraphicSet = Comp.Decompress(Model.ROM, (int)Model.HiROMToSMC(Model.m_varCompDataAbsPtrs[Model.IDX_VARP_TILE_WOB]), 0x2480);
            else if (index == 1)
                Model.WORGraphicSet = Comp.Decompress(Model.ROM, (int)Model.HiROMToSMC(Model.m_varCompDataAbsPtrs[Model.IDX_VARP_TILE_WOR]), 0x2480);
            else if (index == 2)
                Model.STGraphicSet = Comp.Decompress(Model.ROM, 0x2FB631, 0x2480);
            else
            {
                int pointer = (locationMap.GraphicSetA * 3) + 0x1FDA00;
                int offset = (int)(Bits.GetInt24(Model.ROM, pointer) + 0x1FDB00);
                Model.GraphicSets[locationMap.GraphicSetA] = Bits.GetBytes(Model.ROM, offset, 0x2000);
                pointer = (locationMap.GraphicSetB * 3) + 0x1FDA00;
                offset = (int)(Bits.GetInt24(Model.ROM, pointer) + 0x1FDB00);
                Model.GraphicSets[locationMap.GraphicSetB] = Bits.GetBytes(Model.ROM, offset, 0x2000);
                pointer = (locationMap.GraphicSetC * 3) + 0x1FDA00;
                offset = (int)(Bits.GetInt24(Model.ROM, pointer) + 0x1FDB00);
                Model.GraphicSets[locationMap.GraphicSetC] = Bits.GetBytes(Model.ROM, offset, 0x2000);
                pointer = (locationMap.GraphicSetD * 3) + 0x1FDA00;
                offset = (int)(Bits.GetInt24(Model.ROM, pointer) + 0x1FDB00);
                Model.GraphicSets[locationMap.GraphicSetD] = Bits.GetBytes(Model.ROM, offset, 0x2000);
                pointer = (locationMap.GraphicSetL3 * 3) + 0x26CD60;
                offset = (int)(Bits.GetInt24(Model.ROM, pointer) + 0x268780);
                Model.GraphicSetsL3[locationMap.GraphicSetL3] = Comp.Decompress(Model.ROM, offset, 0x1040);
            }
            //
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLocation();
        }
        private void resetPaletteSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!VerifyReset("palette set"))
                return;
            int palette = locationMap.PaletteSet;
            if (index == 0) paletteSet = new PaletteSet(Model.ROM, palette, 0x12EC00, 8, 16, 32);
            else if (index == 1) paletteSet = new PaletteSet(Model.ROM, palette, 0x12ED00, 8, 16, 32);
            else if (index == 2) paletteSet = new PaletteSet(Model.STPaletteSet, 0, 0, 8, 16, 32);
            else paletteSet = new PaletteSet(Model.ROM, palette, (palette * 0x100) + 0x2DC480, 8, 16, 32);
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLocation();
        }
        private void resetTilesetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!VerifyReset("tilesets"))
                return;
            if (index == 0)
                Model.WOBGraphicSet = Comp.Decompress(Model.ROM, (int)Model.HiROMToSMC(Model.m_varCompDataAbsPtrs[Model.IDX_VARP_TILE_WOB]), 0x2480);
            else if (index == 1)
                Model.WORGraphicSet = Comp.Decompress(Model.ROM, (int)Model.HiROMToSMC(Model.m_varCompDataAbsPtrs[Model.IDX_VARP_TILE_WOR]), 0x2480);
            else if (index == 2)
                Model.STGraphicSet = Comp.Decompress(Model.ROM, 0x2FB631, 0x2480);
            else
            {
                Model.Decompress(Model.Tilesets, 0x1FBA00, 0x1E0000, "", 0x800, 3, locationMap.TilesetL1, locationMap.TilesetL1 + 1, null);
                Model.Decompress(Model.Tilesets, 0x1FBA00, 0x1E0000, "", 0x800, 3, locationMap.TilesetL2, locationMap.TilesetL2 + 1, null);
            }
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLocation();
        }
        private void resetTilemapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!VerifyReset("tilemaps"))
                return;
            if (index == 0)
                Model.WOBTilemap = Comp.Decompress(Model.ROM, (int)Model.HiROMToSMC(Model.m_varCompDataAbsPtrs[Model.IDX_VARP_MAP_WOB]), 0x10000);
            else if (index == 1)
                Model.WORTilemap = Comp.Decompress(Model.ROM, (int)Model.HiROMToSMC(Model.m_varCompDataAbsPtrs[Model.IDX_VARP_MAP_WOR]), 0x10000);
            else if (index == 2)
                Model.STTilemap = Comp.Decompress(Model.ROM, 0x2F9D17, 0x4000);
            else
            {
                Model.Decompress(Model.Tilemaps, 0x19CD90, 0x19D1B0, "", 0x4000, 3, locationMap.TilemapL1, locationMap.TilemapL1 + 1, Model.TilemapSizes);
                Model.Decompress(Model.Tilemaps, 0x19CD90, 0x19D1B0, "", 0x4000, 3, locationMap.TilemapL1, locationMap.TilemapL1 + 1, Model.TilemapSizes);
                Model.Decompress(Model.Tilemaps, 0x19CD90, 0x19D1B0, "", 0x4000, 3, locationMap.TilemapL1, locationMap.TilemapL1 + 1, Model.TilemapSizes);
            }
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLocation();
        }
        private void resetSoliditySetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!VerifyReset("solidity map"))
                return;
            if (index == 0)
                Model.WOBSolidity = Bits.GetBytes(Model.ROM, 0x2E9B14, 0x200);
            else if (index == 1)
                Model.WORSolidity = Bits.GetBytes(Model.ROM, 0x2E9D14, 0x200);
            else if (index == 2)
                return;
            Model.Decompress(Model.SoliditySets, 0x19CD10, 0x19A800, "", 0x200, 2, locationMap.SoliditySet, locationMap.SoliditySet + 1, null);
            picture.Invalidate();
        }
        private void resetAnimatedGraphicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!VerifyReset("animated graphics"))
                return;
            Model.AnimatedGraphics = Bits.GetBytes(Model.ROM, 0x260000, 0x8000);
        }
        private void resetAllComponentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to all components. Go ahead with reset?",
                "ZONE DOCTOR", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            locationMap = new LocationMap(index);
            npcs = new LocationNPCs(index);
            overlay.NPCImages = null;
            treasures = new LocationTreasures(index);
            events = new LocationEvents(index);
            exits = new LocationExits(index);
            //
            int palette = locationMap.PaletteSet;
            if (index == 0) paletteSet = new PaletteSet(Model.ROM, palette, 0x12EC00, 8, 16, 32);
            else if (index == 1) paletteSet = new PaletteSet(Model.ROM, palette, 0x12ED00, 8, 16, 32);
            else if (index == 2) paletteSet = new PaletteSet(Model.STPaletteSet, 0, 0, 8, 16, 32);
            else paletteSet = new PaletteSet(Model.ROM, palette, (palette * 0x100) + 0x2DC480, 8, 16, 32);
            //
            if (index == 0)
            {
                Model.WOBGraphicSet = Comp.Decompress(Model.ROM, (int)Model.HiROMToSMC(Model.m_varCompDataAbsPtrs[Model.IDX_VARP_TILE_WOB]), 0x2480);
                Model.WOBTilemap = Comp.Decompress(Model.ROM, (int)Model.HiROMToSMC(Model.m_varCompDataAbsPtrs[Model.IDX_VARP_MAP_WOB]), 0x10000);
                Model.WOBSolidity = Bits.GetBytes(Model.ROM, 0x2E9B14, 0x200);
            }
            else if (index == 1)
            {
                Model.WORGraphicSet = Comp.Decompress(Model.ROM, (int)Model.HiROMToSMC(Model.m_varCompDataAbsPtrs[Model.IDX_VARP_TILE_WOR]), 0x2480);
                Model.WORTilemap = Comp.Decompress(Model.ROM, (int)Model.HiROMToSMC(Model.m_varCompDataAbsPtrs[Model.IDX_VARP_MAP_WOR]), 0x10000);
                Model.WORSolidity = Bits.GetBytes(Model.ROM, 0x2E9D14, 0x200);
            }
            else if (index == 2)
            {
                Model.STGraphicSet = Comp.Decompress(Model.ROM, 0x2FB631, 0x2480);
                Model.STTilemap = Comp.Decompress(Model.ROM, 0x2F9D17, 0x4000);
            }
            else
            {
                int pointer = (locationMap.GraphicSetA * 3) + 0x1FDA00;
                int offset = (int)(Bits.GetInt24(Model.ROM, pointer) + 0x1FDB00);
                Model.GraphicSets[locationMap.GraphicSetA] = Bits.GetBytes(Model.ROM, offset, 0x2000);
                pointer = (locationMap.GraphicSetB * 3) + 0x1FDA00;
                offset = (int)(Bits.GetInt24(Model.ROM, pointer) + 0x1FDB00);
                Model.GraphicSets[locationMap.GraphicSetB] = Bits.GetBytes(Model.ROM, offset, 0x2000);
                pointer = (locationMap.GraphicSetC * 3) + 0x1FDA00;
                offset = (int)(Bits.GetInt24(Model.ROM, pointer) + 0x1FDB00);
                Model.GraphicSets[locationMap.GraphicSetC] = Bits.GetBytes(Model.ROM, offset, 0x2000);
                pointer = (locationMap.GraphicSetD * 3) + 0x1FDA00;
                offset = (int)(Bits.GetInt24(Model.ROM, pointer) + 0x1FDB00);
                Model.GraphicSets[locationMap.GraphicSetD] = Bits.GetBytes(Model.ROM, offset, 0x2000);
                pointer = (locationMap.GraphicSetL3 * 3) + 0x26CD60;
                offset = (int)(Bits.GetInt24(Model.ROM, pointer) + 0x268780);
                Model.GraphicSetsL3[locationMap.GraphicSetL3] = Comp.Decompress(Model.ROM, offset, 0x1040);
                //
                Model.Decompress(Model.Tilesets, 0x1FBA00, 0x1E0000, "", 0x800, 3, locationMap.TilesetL1, locationMap.TilesetL1 + 1, null);
                Model.Decompress(Model.Tilesets, 0x1FBA00, 0x1E0000, "", 0x800, 3, locationMap.TilesetL2, locationMap.TilesetL2 + 1, null);
                Model.Decompress(Model.Tilemaps, 0x19CD90, 0x19D1B0, "", 0x4000, 3, locationMap.TilemapL1, locationMap.TilemapL1 + 1, Model.TilemapSizes);
                Model.Decompress(Model.Tilemaps, 0x19CD90, 0x19D1B0, "", 0x4000, 3, locationMap.TilemapL1, locationMap.TilemapL1 + 1, Model.TilemapSizes);
                Model.Decompress(Model.Tilemaps, 0x19CD90, 0x19D1B0, "", 0x4000, 3, locationMap.TilemapL1, locationMap.TilemapL1 + 1, Model.TilemapSizes);
                Model.Decompress(Model.SoliditySets, 0x19CD10, 0x19A800, "", 0x200, 2, locationMap.SoliditySet, locationMap.SoliditySet + 1, null);
                //
                Model.AnimatedGraphics = Bits.GetBytes(Model.ROM, 0x260000, 0x8000);
            }
            fullUpdate = true;
            RefreshLocation();
        }
        // hex editor
        private void hexEditor_Click(object sender, EventArgs e)
        {
            Model.HexEditor.SetOffset((index * 33) + 0x2D8F00);
            Model.HexEditor.Compare();
            Model.HexEditor.Show();
        }
        //
        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (index < 3 && e.TabPageIndex < 4)
                e.Cancel = true;
        }
        private void Locations_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.Modified && !tilemapEditor.Modified && !tilesetEditor.Modified)
                goto Close;
            state.Draw = false;
            state.Erase = false;
            state.Fill = false;
            state.Select = false;
            state.TileGrid = false;
            state.IsometricGrid = false;
            DialogResult result;
            result = MessageBox.Show("Locations have not been saved.\n\nWould you like to save changes?", "ZONE DOCTOR", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
                Assemble();
            else if (result == DialogResult.No)
            {
                Model.Locations = null;
                Model.PaletteSets = null;
                Model.PrioritySets = null;
                Model.Tilemaps[0] = null;
                Model.Tilesets[0] = null;
                Model.GraphicSets[0] = null;
                Model.SoliditySets[0] = null;
                Model.WOBGraphicSet = null;
                Model.WOBSolidity = null;
                Model.WOBTilemap = null;
                Model.WORGraphicSet = null;
                Model.WORSolidity = null;
                Model.WORTilemap = null;
                Model.STGraphicSet = null;
                Model.STPaletteSet = null;
                Model.STTilemap = null;
            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }
        Close:
            closingEditor = true;
            searchWindow.Close();
            if (tilesetEditor.TileEditor != null)
                tilesetEditor.TileEditor.Close();
            paletteEditor.Close();
            graphicEditor.Close();
            searchWindow.Dispose();
            if (tilesetEditor.TileEditor != null)
                tilesetEditor.TileEditor.Dispose();
            paletteEditor.Dispose();
            graphicEditor.Dispose();
            if (previewer != null)
                previewer.Close();
            settings.Save();
            closingEditor = false;
        }
        private void Locations_FormClosed(object sender, FormClosedEventArgs e)
        {
            GC.Collect();
        }
        //
        private void numeralsButton_Click(object sender, EventArgs e)
        {
            PaletteSet palette = new PaletteSet(Model.ROM, 0, 0x02C671, 1, 16, 32);
            GraphicEditor temp = new GraphicEditor(new Function(NumeralsUpdate), Model.Numerals, Model.Numerals.Length, 0, palette, 0, 0x20);
            temp.Show();
        }
        #endregion
    }
}
