using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using ZONEDOCTOR.Properties;
using ZONEDOCTOR.ScriptsEditor;

namespace ZONEDOCTOR
{
    public partial class Previewer : NewForm
    {
        #region Variables
        private Settings settings = Settings.Default;
        private string romPath;
        private string emulatorPath = "INVALID";
        private bool rom = false, emulator = false, savestate = false, eventchoice = false, initializing = false;
        private int selectNum;
        private List<Entrance> eventTriggers;
        private EType behavior;
        private bool automatic = false;
        private bool snes9x;
        #endregion
        // Constructor
        public Previewer(int num, EType behavior)
        {
            this.selectNum = num;
            this.eventTriggers = new List<Entrance>();
            this.behavior = behavior;
            InitializeComponent();
            InitializePreviewer();
            this.emulator = GetEmulator();
            if (num == 0)
                this.selectNumericUpDown_ValueChanged(null, null);
            UpdateGUI();
            if (settings.PreviewFirstTime)
            {
                DialogResult result = MessageBox.Show("The generated Preview ROM should not be used for anything other than Previews. Doing so will yield unpredictable results.\n\nDo you understand?", "ZONE DOCTOR", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    settings.PreviewFirstTime = false;
                    settings.Save();
                }
            }
        }
        public void Reload(int num, EType behavior)
        {
            if (this.selectNum == num && this.behavior == behavior)
                return;
            this.selectNum = num;
            this.eventTriggers = new List<Entrance>();
            this.behavior = behavior;
            InitializePreviewer();
            this.emulator = GetEmulator();
            if (this.selectIndex.Value == selectNum)
                this.selectNumericUpDown_ValueChanged(null, null);
            UpdateGUI();
            if (settings.PreviewFirstTime)
            {
                DialogResult result = MessageBox.Show("The generated Preview ROM should not be used for anything other than Previews.\nDoing so will yield unpredictable results. Do you understand?", "ZONE DOCTOR", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    settings.PreviewFirstTime = false;
                    settings.Save();
                }
            }
        }
        #region Functions
        private void InitializePreviewer()
        {
            this.initializing = true;
            this.zsnesArgs.Text = settings.PreviewArguments;
            this.dynamicROMPath.Checked = settings.PreviewDynamicRomName;
            if (behavior == EType.EventScript)
            {
                this.Text = "PREVIEW EVENT - Zone Doctor";
                this.label1.Text = "Event #";
                this.selectIndex.Minimum = 0xCA0000;
                this.selectIndex.Maximum = 0xCCE5FF;
                this.selectIndex.Hexadecimal = true;
            }
            else if (behavior == EType.Location)
            {
                this.Text = "PREVIEW LOCATION - Zone Doctor";
                this.label1.Text = "Location #";
                this.selectIndex.Maximum = 414;
                this.selectIndex.Hexadecimal = false;
            }
            // ally stats
            this.partySlot0.Items.AddRange(Lists.Numerize(Model.CharacterNames.Names));
            this.partySlot1.Items.AddRange(Lists.Numerize(Model.CharacterNames.Names));
            this.partySlot2.Items.AddRange(Lists.Numerize(Model.CharacterNames.Names));
            this.partySlot3.Items.AddRange(Lists.Numerize(Model.CharacterNames.Names));
            this.partySlot0.SelectedIndex = settings.PartySlot0;
            this.partySlot1.SelectedIndex = settings.PartySlot1;
            this.partySlot2.SelectedIndex = settings.PartySlot2;
            this.partySlot3.SelectedIndex = settings.PartySlot3;
            this.equipMoogleCharm.Checked = settings.PreviewMoogleCharm;
            this.equipSprintShoes.Checked = settings.PreviewSprintShoes;
            this.walkThroughWalls.Checked = settings.PreviewWTW;
            this.maxOutStats.Checked = settings.PreviewMaxStats;
            romPath = GetRomPath();
            this.initializing = false;
        }
        private bool GetEmulator()
        {
            this.emulatorPath = settings.ZSNESPath; // Gets the saved emulator path
            FileInfo fi;
            try
            {
                fi = new FileInfo(this.emulatorPath);
                if (fi.Exists) // Checks if its a valid path
                    return true;
                else
                    throw new Exception();
            }
            catch
            {
                this.emulatorPath = SelectFile("exe files (*.exe)|*.exe|All files (*.*)|*.*", "C:\\", "Select Emulator");
                if (this.emulatorPath == null || !this.emulatorPath.EndsWith(".exe"))
                    return false;
                fi = new FileInfo(this.emulatorPath);
                if (fi.Exists)
                {
                    settings.ZSNESPath = this.emulatorPath;
                    settings.Save();
                    return true;
                }
                else
                    return false;
            }
        }
        private string SelectFile(string filter, string initDir, string title)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = filter;
            dialog.InitialDirectory = initDir;
            dialog.Title = title;
            return (dialog.ShowDialog() == DialogResult.OK) ? dialog.FileName : null;
        }
        private void UpdateGUI()
        {
            this.emuPathTextBox.Text = this.emulatorPath;
            this.romPathTextBox.Text = this.romPath;
            this.selectIndex.Value = this.selectNum;
            this.eventListBox.Items.Clear();
            Entrance ent;
            for (int i = 0; i < eventTriggers.Count; i++)
            {
                ent = eventTriggers[i];
                if (this.behavior == EType.EventScript)
                {
                    if (ent.Flag)
                        this.eventListBox.Items.Add(
                            "Enter Event - X:" + ent.DstX.ToString("00") +
                            " Y:" + ent.DstY.ToString("000") +
                            " - Location: [" + ent.Destination.ToString("d3") + "] " + Model.LocationNames[ent.Destination]);
                    else // A run event
                        this.eventListBox.Items.Add(
                            "Run Event - X:" + ent.DstX.ToString("00") +
                            " Y:" + ent.DstY.ToString("000") +
                            " - Location: [" + ent.Destination.ToString("d3") + "] " + Model.LocationNames[ent.Destination]);
                }
                else if (this.behavior == EType.Location)
                {
                    this.eventListBox.Items.Add(
                        "Enter - X:" + ent.DstX.ToString("00") +
                        " Y:" + ent.DstY.ToString("000") +
                        " - From Location: [" + ent.Destination.ToString("d3") + "] " + Model.LocationNames[ent.Destination]);
                }
            }
            if (this.eventListBox.Items.Count > 0)
                this.eventListBox.SelectedIndex = 0;
        }
        // launching
        private bool Prelaunch()
        {
            this.rom = GeneratePreviewRom();
            this.savestate = GeneratePreviewSaveState();
            return (rom == savestate == true);
        }
        private void Launch()
        {
            settings.PreviewArguments = zsnesArgs.Text;
            settings.Save();
            if (rom && emulator && savestate && eventchoice)
                LaunchEmulator(this.emulatorPath, this.romPath, snes9x ? snes9xArgs.Text : zsnesArgs.Text);
            else
            {
                if (!rom)
                    MessageBox.Show("There was a problem generating the preview rom", "ZONE DOCTOR");
                if (!emulator)
                    MessageBox.Show("There is a problem with the emulator.\nSNES9X and ZSNESW are the only emulators supported.", "ZONE DOCTOR");
                if (!savestate)
                    MessageBox.Show("There was a problem generating the preview save state.", "ZONE DOCTOR");
                if (!eventchoice)
                    MessageBox.Show("An invalid destination was selected to preview.", "ZONE DOCTOR");
            }
        }
        private bool GeneratePreviewRom()
        {
            bool ret = false;
            // Make backup of current data                
            byte[] backup = Bits.Copy(Model.ROM);
            bool[] editGraphicSets = Bits.Copy(Model.EditGraphicSets);
            bool[] editTilesets = Bits.Copy(Model.EditTilesets);
            bool[] editTilemaps = Bits.Copy(Model.EditTilemaps);
            bool[] editSoliditySets = Bits.Copy(Model.EditSoliditySets);
            bool editWOBGraphicSet = Model.EditWOBGraphicSet;
            bool editWOBTilemap = Model.EditWOBTilemap;
            bool editWORGraphicSet = Model.EditWORGraphicSet;
            bool editWORTilemap = Model.EditWORTilemap;
            bool editSTGraphicSet = Model.EditSTGraphicSet;
            bool editSTTilemap = Model.EditSTTilemap;
            //
            if (!(this.behavior == EType.EventScript &&
                this.eventListBox.SelectedIndex < 0 || this.eventListBox.SelectedIndex >= this.eventTriggers.Count))
            {
                if (this.behavior == EType.EventScript)
                    Model.Program.EventScripts.Assemble();
                if (this.behavior == EType.Location)
                    Model.Program.Locations.Assemble();
                PrepareImage();
                // Backup filename
                string fileNameBackup = Model.FileName;
                // Generate preview name;
                this.romPath = GetRomPath();
                string oldFileName = Model.FileName;
                Model.FileName = romPath;
                ret = Model.WriteRom(); // Save temp rom
                // Restore name
                Model.FileName = oldFileName;
            }
            //Restore Rom Image
            backup.CopyTo(Model.ROM, 0);
            editGraphicSets.CopyTo(Model.EditGraphicSets, 0);
            editTilesets.CopyTo(Model.EditTilesets, 0);
            editTilemaps.CopyTo(Model.EditTilemaps, 0);
            editSoliditySets.CopyTo(Model.EditSoliditySets, 0);
            Model.EditWOBGraphicSet = editWOBGraphicSet;
            Model.EditWOBTilemap = editWOBTilemap;
            Model.EditWORGraphicSet = editWORGraphicSet;
            Model.EditWORTilemap = editWORTilemap;
            Model.EditSTGraphicSet = editSTGraphicSet;
            Model.EditSTTilemap = editSTTilemap;
            return ret;
        }
        private bool GeneratePreviewSaveState()
        {
            try
            {
                snes9x = Do.Contains(this.emulatorPath, "snes9x", StringComparison.CurrentCultureIgnoreCase);
                string ext = snes9x ? ".000" : ".zst";
                FileInfo fInfo = new FileInfo(Model.GetEditorPathWithoutFileName() + "RomPreviewBaseSave" + ext);
                if (!fInfo.Exists)
                {
                    byte[] lc;
                    if (snes9x)
                        lc = Resources.RomPreviewBaseSave;
                    else
                        lc = Resources.RomPreviewBaseSave1;
                    File.WriteAllBytes(Model.GetEditorPathWithoutFileName() + "RomPreviewBaseSave" + ext, lc);
                }
                FileStream fs = fInfo.OpenRead();
                BinaryReader br = new BinaryReader(fs);
                byte[] state = br.ReadBytes((int)fs.Length);
                br.Close();
                fs.Close();
                // modify zst if needed
                int base_offset = snes9x ? 0x10C98 : 0x00C13;
                // max stats
                if (maxOutStats.Checked)
                {
                    byte[] maxStats = Resources.maxstats;
                    maxStats.CopyTo(state, base_offset + 0x1600);
                }
                //
                int index = eventListBox.SelectedIndex;
                int indexNum = (int)selectIndex.Value;
                if (behavior == EType.EventScript)
                {
                    Entrance entrance = new Entrance();
                    entrance = eventTriggers[index];
                    indexNum = entrance.Destination;
                }
                Bits.SetShort(state, base_offset + 0x1F64, (ushort)indexNum);
                if (indexNum < 2)
                {
                    state[base_offset + 0x1F60] = (byte)adjustX.Value;
                    state[base_offset + 0x1F61] = (byte)adjustY.Value;
                }
                else
                {
                    if (eventListBox.SelectedIndex >= 0)
                    {
                        Entrance entrance = eventTriggers[eventListBox.SelectedIndex];
                        state[base_offset + 0x0743] = entrance.DstF;
                    }
                    state[base_offset + 0x1FC0] = (byte)adjustX.Value;
                    state[base_offset + 0x1FC1] = (byte)adjustY.Value;
                }
                // store source exit's coordinates
                if (eventListBox.SelectedIndex >= 0)
                {
                    Entrance entrance = eventTriggers[eventListBox.SelectedIndex];
                    state[base_offset + 0x1F6B] = entrance.SrcX;
                    state[base_offset + 0x1F6C] = entrance.SrcY;
                }
                //
                if (equipSprintShoes.Checked)
                    state[base_offset + 0x1623 + (partySlot0.SelectedIndex * 0x25)] = 0xE6;
                if (equipMoogleCharm.Checked)
                    state[base_offset + 0x1624 + (partySlot0.SelectedIndex * 0x25)] = 0xDE;
                // set characters in party
                for (int i = 0; i < 16; i++)
                {
                    state[base_offset + 0x0867 + (i * 0x29)] &= 0x18;
                    state[base_offset + 0x1850 + i] &= 0x18;
                }
                state[base_offset + 0x0867 + (partySlot0.SelectedIndex * 0x29)] = 0xC1;
                state[base_offset + 0x0867 + (partySlot1.SelectedIndex * 0x29)] = 0x49;
                state[base_offset + 0x0867 + (partySlot2.SelectedIndex * 0x29)] = 0x51;
                state[base_offset + 0x0867 + (partySlot3.SelectedIndex * 0x29)] = 0x59;
                state[base_offset + 0x1850 + partySlot0.SelectedIndex] = 0xC1;
                state[base_offset + 0x1850 + partySlot1.SelectedIndex] = 0x49;
                state[base_offset + 0x1850 + partySlot2.SelectedIndex] = 0x51;
                state[base_offset + 0x1850 + partySlot3.SelectedIndex] = 0x59;
                //
                fInfo = new FileInfo(GetStatePath());
                fs = fInfo.OpenWrite();
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(state);
                bw.Close();
                fs.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
        private bool PrepareImage()
        {
            int index = this.eventListBox.SelectedIndex;
            if (this.behavior == EType.EventScript &&
                index < 0 || index >= this.eventTriggers.Count)
            {
                this.eventchoice = false;
                return false;
            }
            Entrance ent = new Entrance();
            if (this.eventTriggers.Count > 0)
                ent = eventTriggers[index];
            int indexNum;
            if (behavior == EType.Location)
                indexNum = (int)selectIndex.Value;
            else
                indexNum = ent.Destination;
            int save = Model.Locations[indexNum].LocationEvents.EntranceEvent;
            //Model.Locations[indexNum].LocationEvents.EntranceEvent = 0x5EB3;
            if (walkThroughWalls.Checked)
            {
                Model.ROM[0x004E4E] = 0x80;
                Model.ROM[0x004E4F] = 0x4D;
            }
            //
            SaveLocationExitEvents();
            //
            Model.Locations[indexNum].LocationEvents.EntranceEvent = save;
            this.eventchoice = true;
            //
            return true;
        }
        private void SaveLocationExitEvents()
        {
            int offsetStart = 0x0342;
            for (int i = 0; i < Model.Locations.Length; i++)
                Model.Locations[i].LocationEvents.Assemble(ref offsetStart);
        }
        private string GetRomPath()
        {
            if (settings.PreviewDynamicRomName)
                return Model.GetPathWithoutFileName() + "PreviewROM_" + Model.GetFileNameWithoutPath();
            else
                return Model.GetEditorPathWithoutFileName() + "PreviewRom.smc";
        }
        private string GetStatePath()
        {
            string ext = snes9x ? ".000" : ".zst";
            if (settings.PreviewDynamicRomName)
                return Model.GetPathWithoutFileName() + "PreviewROM_" + Model.GetFileNameWithoutPathOrExtension() + ext;
            else
                return Model.GetEditorPathWithoutFileName() + "PreviewRom" + ext;
        }
        private void LaunchEmulator(string emulatorPath, string romPath, string args)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = emulatorPath;
            proc.StartInfo.Arguments = args + " " + "\"" + romPath + "\"";
            proc.Start();
        }
        // scanning data
        private void ScanForEvents()
        {
            this.eventTriggers.Clear();
            ScanForNPCEvents();
            ScanForEnterEvents();
            ScanForRunEvents();
        }
        private void ScanForEnterEvents()
        {
            foreach (Location lvl in Model.Locations)
            {
                if (lvl.LocationEvents.EntranceEvent == this.selectNum)
                {
                    ScanForEntrancesToLocation(lvl.Index);
                }
            }
        }
        private void ScanForRunEvents()
        {
            Entrance dst;
            foreach (Location loc in Model.Locations) // For every location
            {
                foreach (Event EVENT in loc.LocationEvents.Events)
                {
                    if (EVENT.EventPointer == this.selectNum - 0xCA0000)
                    {
                        dst = new Entrance();
                        dst.Destination = (ushort)loc.Index;
                        dst.DstX = EVENT.X;
                        dst.DstY = EVENT.Y;
                        Entrance src = ReturnEntranceToLocation(loc.Index);
                        dst.SrcX = src.SrcX;
                        dst.SrcY = src.SrcY;
                        dst.ShowMessage = false;
                        dst.Flag = false;
                        eventTriggers.Add(dst); // Add the event trigger
                    }
                }
            }
        }
        private Entrance ReturnEntranceToLocation(int locNum)
        {
            Entrance ent;
            foreach (Location loc in Model.Locations) // For every location
            {
                foreach (Exit exit in loc.LocationExits.Exits)
                {
                    if (exit.Destination == locNum) // If this exit points to the location we want
                    {
                        ent = new Entrance();
                        ent.Source = (ushort)loc.Index;
                        ent.Destination = (ushort)loc.Index;
                        ent.DstX = exit.DstX;
                        ent.DstY = exit.DstY;
                        ent.DstF = exit.DstF;
                        ent.SrcX = exit.X;
                        ent.SrcY = exit.Y;
                        ent.ShowMessage = exit.ShowMessage;
                        ent.Flag = true; // Indicates an enter event
                        return ent;
                    }
                }
            }
            ent = new Entrance();
            ent.SrcX = 84;
            ent.SrcY = 33;
            return ent;
        }
        private void ScanForEntrancesToLocation(int locNum)
        {
            Entrance ent;
            foreach (Location loc in Model.Locations) // For every location
            {
                foreach (Exit exit in loc.LocationExits.Exits)
                {
                    if (exit.Destination == locNum) // If this exit points to the location we want
                    {
                        ent = new Entrance();
                        ent.Source = (ushort)loc.Index;
                        ent.Destination = (ushort)loc.Index;
                        ent.DstX = exit.DstX;
                        ent.DstY = exit.DstY;
                        ent.DstF = exit.DstF;
                        ent.SrcX = exit.X;
                        ent.SrcY = exit.Y;
                        ent.ShowMessage = exit.ShowMessage;
                        ent.Flag = true; // Indicates an enter event
                        eventTriggers.Add(ent); // Add the event trigger
                    }
                }
            }
        }
        private void ScanForNPCEvents()
        {
            Entrance dst;
            foreach (Location loc in Model.Locations) // For every location
            {
                int index = 0;
                for (int i = 0; i < loc.LocationNPCs.Count; i++, index++)
                {
                    NPC npc = loc.LocationNPCs.Npcs[i];
                    if (npc.EventPointer == this.selectNum - 0xCA0000)
                    {
                        dst = new Entrance();
                        dst.Destination = (ushort)loc.Index;
                        dst.DstX = npc.X;
                        dst.DstY = (byte)(npc.Y + 1);
                        Entrance src = ReturnEntranceToLocation(loc.Index);
                        dst.SrcX = src.SrcX;
                        dst.SrcY = src.SrcY;
                        dst.MSG = "NPC";
                        dst.ShowMessage = false;
                        dst.Source = index;
                        dst.Flag = false;
                        eventTriggers.Add(dst); // Add the event trigger
                    }
                }
            }
        }
        #endregion
        #region Event Handlers
        private void linkLabelZSNES_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://zsnes-docs.sourceforge.net/html/advanced.htm#command_line");
        }
        private void linkLabelSNES9X_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.snes9x.com/phpbb2/viewtopic.php?t=3020");
        }
        private void defaultZSNES_Click(object sender, EventArgs e)
        {
            this.zsnesArgs.Text = settings.PreviewArgsDefault;
        }
        private void defaultSNES9X_Click(object sender, EventArgs e)
        {
            this.snes9xArgs.Text = "";
        }
        private void dynamicROMPath_CheckedChanged(object sender, EventArgs e)
        {
            this.dynamicROMPath.ForeColor = dynamicROMPath.Checked ? SystemColors.ControlText : SystemColors.ControlDark;
            if (!this.initializing)
            {
                settings.PreviewDynamicRomName = dynamicROMPath.Checked;
                settings.Save();
                this.romPath = GetRomPath();
            }
            UpdateGUI();
        }
        private void changeEmuButton_Click(object sender, EventArgs e)
        {
            string path = SelectFile("exe files (*.exe)|*.exe|All files (*.*)|*.*", "C:\\", "Select Emulator");
            if (path == null || !path.EndsWith(".exe"))
                return;
            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                settings.LastDirectory = Path.GetDirectoryName(path);
                this.emulatorPath = path;
                this.emulator = true;
                settings.ZSNESPath = this.emulatorPath;
                settings.Save();
                UpdateGUI();
            }
        }
        private void eventListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.eventListBox.SelectedIndex;
            if (index < 0 || index >= this.eventTriggers.Count)
                return;
            // Set the XY values
            this.adjustX.Value = eventTriggers[index].DstX;
            this.adjustY.Value = eventTriggers[index].DstY;
        }
        private void selectNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.selectNum = (int)selectIndex.Value;
            if (this.behavior == EType.EventScript)
                ScanForEvents();
            else if (this.behavior == EType.Location)
            {
                this.eventTriggers.Clear();
                ScanForEntrancesToLocation((int)selectIndex.Value);
            }
            UpdateGUI();
        }
        private void partySlot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating || initializing)
                return;
            settings.PartySlot0 = partySlot0.SelectedIndex;
            settings.PartySlot1 = partySlot1.SelectedIndex;
            settings.PartySlot2 = partySlot2.SelectedIndex;
            settings.PartySlot3 = partySlot3.SelectedIndex;
            settings.Save();
        }
        private void equipment_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Updating || initializing)
                return;
            settings.PreviewWTW = walkThroughWalls.Checked;
            settings.PreviewSprintShoes = equipSprintShoes.Checked;
            settings.PreviewMoogleCharm = equipMoogleCharm.Checked;
            settings.Save();
        }
        private void maxOutStats_CheckedChanged(object sender, EventArgs e)
        {
            maxOutStats.ForeColor = maxOutStats.Checked ? SystemColors.ControlText : SystemColors.ControlDark;
            if (this.Updating || initializing)
                return;
            settings.PreviewMaxStats = maxOutStats.Checked;
            settings.Save();
        }
        private void launchButton_Click(object sender, EventArgs e)
        {
            if (Prelaunch())
                Launch();
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        public struct Entrance
        {
            public int Source;
            public bool ShowMessage;
            public byte DstX;
            public byte DstY;
            public byte DstF;
            public byte SrcX;
            public byte SrcY;
            public ushort Destination;
            public bool Flag;
            public string MSG;
        }
    }
}
