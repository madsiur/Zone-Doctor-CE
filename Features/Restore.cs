using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ZONEDOCTOR.Properties;

namespace ZONEDOCTOR
{
    public partial class Restore : NewForm
    {
        private Settings settings = Settings.Default;
        private byte[] romSrc = null;
        private byte[] romDst
        {
            get { return Model.ROM; }
            set { Model.ROM = value; }
        }
        // constructor
        public Restore()
        {
            InitializeComponent();
            // LJ 2011-12-28: interoperability fix for FF3usME
            Model.LoadVarCompDataAbsPtrs();
            Model.DecompressWorldMaps();
        }
        // event handlers
        private void browseFreshRom_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = settings.LastRomPath;
            openFileDialog1.Title = "Select a FF6 ROM";
            openFileDialog1.Filter = "SMC files (*.SMC)|*.SMC|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
            Retry:
                try
                {
                    FileInfo fInfo = new FileInfo(openFileDialog1.FileName);
                    long numBytes = fInfo.Length;
                    FileStream fStream = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fStream);
                    byte[] temp = br.ReadBytes((int)numBytes);
                    br.Close();
                    fStream.Close();
                    // remove header if it has one
                    if ((temp.Length & 0x200) == 0x200)
                        temp = Bits.GetBytes(temp, 0x200);
                    // check if valid rom
                    System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                    if (encoding.GetString(Bits.GetBytes(temp, 0xFFB2, 4)) != "F6  ")
                    {
                        MessageBox.Show("The game code for this ROM is invalid.", "ZONE DOCTOR");
                        return;
                    }
                    romSrc = temp;
                    freshRomTextBox.Text = openFileDialog1.FileName;
                    buttonOK.Enabled = true;
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show("Zone Doctor was unable to load the rom.\n\n" + ex.Message,
                        "ZONE DOCTOR", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) != DialogResult.Cancel)
                        goto Retry;
                }
            }
        }
        private void elements_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null)
                return;
            foreach (TreeNode tn in e.Node.Nodes)
                tn.Checked = e.Node.Checked;
        }
        
        private void buttonOK_Click(object sender, EventArgs e)
        {
            // Event Scripts
            if (elements.Nodes["EventScripts"].Checked)   // event scripts
            {
                Buffer.BlockCopy(romSrc, 0x0A0000, romDst, 0x0A0000, 0x2E600);
            }
            // Locations
            if (elements.Nodes["Locations"].Nodes["Maps"].Checked)   // maps
            {
                Buffer.BlockCopy(romSrc, Model.BASE_LOCATION, romDst, Model.BASE_LOCATION, Model.SIZE_LOCATION); 
                
                Buffer.BlockCopy(romSrc, 0xFE00, romDst, 0xFE00, 0x40);  // priority sets
                Buffer.BlockCopy(romSrc, 0x2DC480, romDst, 0x2DC480, 0x3000);   // location palettes
                Buffer.BlockCopy(romSrc, 0x12EC00, romDst, 0x12EC00, 0x200);   // WOB and WOR palettes
                Buffer.BlockCopy(romSrc, 0x18E6BA, romDst, 0x18E6BA, 0xF7);   // serpent trench palettes
            }
            if (elements.Nodes["Locations"].Nodes["NPCs"].Checked)   // npcs
            {
                Buffer.BlockCopy(romSrc, Model.BASE_NPC_PTR, romDst, Model.BASE_NPC_PTR, Model.SIZE_NPC_PTR); 
                Buffer.BlockCopy(romSrc, Model.BASE_NPC, romDst, Model.BASE_NPC, Model.SIZE_NPC_DATA);
            }
            if (elements.Nodes["Locations"].Nodes["Treasures"].Checked)   // treasures
            {
                Buffer.BlockCopy(romSrc, Model.BASE_CHEST_PTR, romDst, Model.BASE_CHEST_PTR, Model.SIZE_CHEST_PTR + Model.SIZE_CHEST_DATA);
            }
            if (elements.Nodes["Locations"].Nodes["Exits"].Checked)   // exits
            {
                Buffer.BlockCopy(romSrc, Model.BASE_SHORT_EXIT_PTR, romDst, Model.BASE_SHORT_EXIT_PTR, Model.SIZE_SHORT_EXIT_PTR + Model.SIZE_SHORT_EXIT_DATA);
            }
            if (elements.Nodes["Locations"].Nodes["Events"].Checked)   // events
            {
                Buffer.BlockCopy(romSrc, 0x11FA00, romDst, 0x11FA00, 0x600);  // entrance events
                
                Buffer.BlockCopy(romSrc, Model.BASE_EVENT_PTR, romDst, Model.BASE_EVENT_PTR, Model.SIZE_EVENT_PTR + Model.SIZE_EVENT_DATA);
            }
            if (elements.Nodes["Locations"].Nodes["Tilemaps"].Checked)   // tilemaps
            {
                Buffer.BlockCopy(romSrc, Model.BASE_TILEMAP_PTR, romDst, Model.BASE_TILEMAP_PTR, Model.SIZE_TILEMAP_PTR);
                Buffer.BlockCopy(romSrc, Model.BASE_TILEMAP, romDst, Model.BASE_TILEMAP, Model.SIZE_TILEMAP_DATA);

                Buffer.BlockCopy(romSrc, 0x2F9D17, romDst, 0x2F9D17, 0x191A); // serpent trench tilemap
            }
            if (elements.Nodes["Locations"].Nodes["Tilesets"].Checked)   // tilesets
            {
                Buffer.BlockCopy(romSrc, 0x1FBA00, romDst, 0x1FB000, 0xE4);  // tileset pointers
                Buffer.BlockCopy(romSrc, 0x1E0000, romDst, 0x1E0000, 0x1B400);
            }
            if (elements.Nodes["Locations"].Nodes["Graphics"].Checked)   // graphics
            {
                Buffer.BlockCopy(romSrc, 0x1FDA00, romDst, 0x1FDA00, 0x61A00); // location graphics
                Buffer.BlockCopy(romSrc, 0x260000, romDst, 0x260000, 0x8000); // animated graphics
                Buffer.BlockCopy(romSrc, 0x2FB631, romDst, 0x2FB631, 0xFF3); // serpent trench graphics
            }
            if (elements.Nodes["Locations"].Nodes["SoliditySets"].Checked)   // solidity sets
            {
                Buffer.BlockCopy(romSrc, 0x19CD10, romDst, 0x19CD10, 0x56); // pointers
                Buffer.BlockCopy(romSrc, 0x19A800, romDst, 0x19A800, 0x244C);
                Buffer.BlockCopy(romSrc, 0x2E9B14, romDst, 0x2E9B14, 0x400); // WOB and WOR solidity
            }
            //madsiur, CE Edition
            if (elements.Nodes["Locations"].Nodes["LocationNames"].Checked)
            {
                Buffer.BlockCopy(romSrc, Model.BASE_LOC_NAMES, romDst, Model.BASE_LOC_NAMES, Model.SIZE_LOC_NAMES);
                Buffer.BlockCopy(romSrc, Model.BASE_TILEMAP, romDst, Model.BASE_TILEMAP, Model.SIZE_TILEMAP_DATA);
            }
            if (elements.Nodes["WorldMaps"].Nodes["WorldOfBirth"].Checked)
            {
                int offset = (int)Model.HiROMToSMC(Model.m_varCompDataAbsPtrs[Model.IDX_VARP_MAP_WOR]);
                Bits.SetInt24(romDst, 0x2EB20F, 0xEED434);
                Buffer.BlockCopy(romSrc, 0x2ED434, romDst, offset, 0x3D1B); // WOB tilemap
                Model.WOBTilemap = null;
                //
                offset = (int)Model.HiROMToSMC(Model.m_varCompDataAbsPtrs[Model.IDX_VARP_TILE_WOR]);
                Bits.SetInt24(romDst, 0x2EB212, 0xEF114F);
                Buffer.BlockCopy(romSrc, 0x2F114F, romDst, offset, 0x2101); // WOB graphics
                Model.WOBGraphicSet = null;
            }
            if (elements.Nodes["WorldMaps"].Nodes["WorldOfRuin"].Checked)
            {
                int offset = (int)Model.HiROMToSMC(Model.m_varCompDataAbsPtrs[Model.IDX_VARP_TILE_WOR]);
                Bits.SetInt24(romDst, 0x2EB21E, 0xEF4A46);
                Buffer.BlockCopy(romSrc, 0x2F4A46, romDst, 0x2F4A46, 0x2010); // WOR graphics
                Model.WORGraphicSet = null;
                //
                offset = (int)Model.HiROMToSMC(Model.m_varCompDataAbsPtrs[Model.IDX_VARP_MAP_WOR]);
                Bits.SetInt24(romDst, 0x2EB221, 0xEF6A56);
                Bits.SetInt24(romDst, 0x2EB224, 0xEF6A56);
                Buffer.BlockCopy(romSrc, 0x2F6A56, romDst, 0x2F6A56, 0x32C1); // WOR tilemap
                Model.WORTilemap = null;
            }
            // LJ 2011-12-28: interoperability fix for FF3usME
                Model.LoadVarCompDataAbsPtrs();
            Model.DecompressWorldMaps();
            //
            this.Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void selectAll_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in elements.Nodes)
                node.Checked = true;
        }
        private void deselectAll_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in elements.Nodes)
                node.Checked = false;
        }

        private void elements_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
