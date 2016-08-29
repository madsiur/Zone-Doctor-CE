using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZONEDOCTOR
{
    public partial class Locations
    {
        #region Variables
        private LocationNPCs npcs { get { return location.LocationNPCs; } set { location.LocationNPCs = value; } }
        public ListBox NpcListBox { get { return npcListBox; } set { npcListBox = value; } }
        public NumericUpDown NpcX { get { return npcX; } set { npcX = value; } }
        public NumericUpDown NpcY { get { return npcY; } set { npcY = value; } }
        private Object copyNPC;
        #endregion
        #region Methods
        private void InitializeNPCProperties()
        {
            this.Updating = true;
            npcListBox.Items.Clear();
            if (npcs.Npcs.Count == 0)
            {
                this.Updating = false;
                groupBox12.Enabled = false;
                groupBox13.Enabled = false;
                groupBox14.Enabled = false;
                npcCopy.Enabled = false;
                npcDuplicate.Enabled = false;
                npcRemoveObject.Enabled = false;
                npcMoveDown.Enabled = false;
                npcMoveUp.Enabled = false;
                return;
            }
            //
            for (int i = 0; i < npcs.Npcs.Count; i++)
                npcListBox.Items.Add("NPC #" + i.ToString());
            npcs.CurrentNPC = npcs.SelectedNPC = npcListBox.SelectedIndex = 0;
            RefreshNPCProperties();
            this.Updating = false;
        }
        private void RefreshNPCProperties()
        {
            this.Updating = true;
            groupBox12.Enabled = npcs.Npcs.Count != 0;
            groupBox13.Enabled = npcs.Npcs.Count != 0;
            groupBox14.Enabled = npcs.Npcs.Count != 0;
            npcCopy.Enabled = npcs.Npcs.Count != 0;
            npcDuplicate.Enabled = npcs.Npcs.Count != 0;
            npcRemoveObject.Enabled = npcs.Npcs.Count != 0;
            npcMoveDown.Enabled = npcs.Npcs.Count != 0;
            npcMoveUp.Enabled = npcs.Npcs.Count != 0;
            //
            npcEventPointer.Value = npcs.EventPointer;
            npcPalette.Value = npcs.PaletteNum;
            npcCheckMem.Value = npcs.CheckMem;
            npcCheckBit.Value = npcs.CheckBit;
            npcX.Value = npcs.X;
            npcY.Value = npcs.Y;
            npcSpeed.SelectedIndex = npcs.Speed;
            npcSpriteSet.Value = npcs.SpriteNum;
            npcSpriteIndex.Value = npcs.Action;
            npcWalkability.SetItemChecked(0, npcs.SolidifyActionPath);
            npcWalkability.SetItemChecked(1, npcs.WalkUnder);
            npcWalkability.SetItemChecked(2, npcs.WalkOver);
            npcWalkability.SetItemChecked(3, npcs.DontFaceOnTrigger);
            npcVehicle.SelectedIndex = npcs.Vehicle;
            npcFace.SelectedIndex = npcs.F;
            npcUnknownBits.SetItemChecked(0, npcs.B4b7);
            npcUnknownBits.SetItemChecked(1, npcs.B8b3);
            npcUnknownBits.SetItemChecked(2, npcs.B8b4);
            npcUnknownBits.SetItemChecked(3, npcs.B8b5);
            npcUnknownBits.SetItemChecked(4, npcs.B8b6);
            npcUnknownBits.SetItemChecked(5, npcs.B8b7);
            npcsBytesLeft.Text = CalculateFreeNPCSpace() + " bytes left";
            npcsBytesLeft.BackColor = CalculateFreeNPCSpace() >= 0 ? SystemColors.Control : Color.Red;
            this.Updating = false;
        }
        private int CalculateFreeNPCSpace()
        {
            int used = 0;
            for (int i = 0; i < 415; i++)
            {
                for (int a = 0; a < locations[i].LocationNPCs.Npcs.Count; a++)
                    used += 9;
            }
            return 0x4D6E - used;
        }
        //
        private void AddNewNPC()
        {
            Point p = new Point(Math.Abs(this.picture.Left) / 16, Math.Abs(this.picture.Top) / 16);
            if (CalculateFreeNPCSpace() >= 9)
            {
                if (npcListBox.Items.Count < 32)
                {
                    if (npcListBox.Items.Count > 0)
                        npcs.New(npcListBox.SelectedIndex + 1, p);
                    else
                        npcs.New(0, p);
                    int reselect;
                    if (npcListBox.Items.Count > 0)
                        reselect = npcListBox.SelectedIndex;
                    else
                        reselect = -1;
                    npcListBox.BeginUpdate();
                    this.npcListBox.Items.Clear();
                    for (int i = 0, a = 0; i < npcs.Count; i++, a++)
                    {
                        this.npcListBox.Items.Add("NPC #" + a.ToString());
                        npcs.CurrentNPC = i;
                    }
                    this.npcListBox.SelectedIndex = reselect + 1;
                    npcListBox.EndUpdate();
                }
                else
                    MessageBox.Show("Could not insert any more NPCs. The maximum number of NPCs allowed per location is 32.",
                        "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Could not insert the NPC. " + MaximumSpaceExceeded("NPCs"),
                    "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void AddNewNPC(NPC e)
        {
            Point p = new Point(Math.Abs(this.picture.Left) / 16, Math.Abs(this.picture.Top) / 16);
            if (CalculateFreeNPCSpace() >= 9)
            {
                if (npcListBox.Items.Count < 32)
                {
                    if (npcListBox.Items.Count > 0)
                        npcs.New(npcListBox.SelectedIndex + 1, e);
                    else
                        npcs.New(0, e);
                    int reselect;
                    if (npcListBox.Items.Count > 0)
                        reselect = npcListBox.SelectedIndex;
                    else
                        reselect = -1;
                    npcListBox.BeginUpdate();
                    this.npcListBox.Items.Clear();
                    for (int i = 0, a = 0; i < npcs.Count; i++, a++)
                    {
                        this.npcListBox.Items.Add("NPC #" + a.ToString());
                        npcs.CurrentNPC = i;
                    }
                    this.npcListBox.SelectedIndex = reselect + 1;
                    npcListBox.EndUpdate();
                }
                else
                    MessageBox.Show("Could not insert any more NPCs. The maximum number of NPCs allowed per location is 32.",
                        "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Could not insert the NPC. " + MaximumSpaceExceeded("NPCs"),
                    "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
        #region Event Handlers
        private void npcListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            npcs.CurrentNPC = npcListBox.SelectedIndex;
            npcs.SelectedNPC = npcListBox.SelectedIndex;
            RefreshNPCProperties();
            this.picture.Invalidate();
        }
        private void npcInsertObject_Click(object sender, EventArgs e)
        {
            AddNewNPC();
        }
        private void npcRemoveObject_Click(object sender, EventArgs e)
        {
            npcListBox.Focus();
            if (npcListBox.SelectedIndex != -1 && npcs.CurrentNPC == npcListBox.SelectedIndex)
            {
                npcs.Remove();
                int reselect = npcListBox.SelectedIndex;
                if (reselect == npcListBox.Items.Count - 1)
                    reselect--;
                npcListBox.BeginUpdate();
                npcListBox.Items.Clear();
                for (int i = 0; i < npcs.Npcs.Count; i++)
                    npcListBox.Items.Add("NPC #" + i.ToString());
                if (npcListBox.Items.Count > 0)
                    npcListBox.SelectedIndex = reselect;
                else
                {
                    npcListBox.SelectedIndex = -1;
                    this.picture.Invalidate();
                    groupBox12.Enabled = groupBox13.Enabled = groupBox14.Enabled = false;
                    RefreshNPCProperties();
                }
                npcListBox.EndUpdate();
            }
        }
        private void npcMoveUp_Click(object sender, EventArgs e)
        {
            int reselectP = 0;
            if (npcs.CurrentNPC > 0)
            {
                reselectP = npcListBox.SelectedIndex - 1;
                npcs.Reverse(npcs.CurrentNPC - 1);
            }
            else return;
            this.npcListBox.BeginUpdate();
            this.npcListBox.Items.Clear();
            for (int i = 0, a = 0; i < npcs.Count; i++, a++)
            {
                this.npcListBox.Items.Add("NPC #" + a.ToString());
                npcs.CurrentNPC = i;
            }
            this.npcListBox.SelectedIndex = reselectP;
            this.npcListBox.EndUpdate();
        }
        private void npcMoveDown_Click(object sender, EventArgs e)
        {
            int reselectP = 0;
            if (npcs.CurrentNPC < npcs.Count - 1)
            {
                reselectP = npcListBox.SelectedIndex + 1;
                npcs.Reverse(npcs.CurrentNPC);
            }
            else return;
            this.npcListBox.BeginUpdate();
            this.npcListBox.Items.Clear();
            for (int i = 0, a = 0; i < npcs.Count; i++, a++)
            {
                this.npcListBox.Items.Add("NPC #" + a.ToString());
                npcs.CurrentNPC = i;
            }
            this.npcListBox.SelectedIndex = reselectP;
            this.npcListBox.EndUpdate();
        }
        private void npcCopy_Click(object sender, EventArgs e)
        {
            copyNPC = npcs.Npc.Copy();
            npcPaste.Enabled = true;
        }
        private void npcPaste_Click(object sender, EventArgs e)
        {
            if (copyNPC == null)
                return;
            AddNewNPC((NPC)copyNPC);
        }
        private void npcDuplicate_Click(object sender, EventArgs e)
        {
            AddNewNPC(npcs.Npc.Copy());
        }
        private void buttonGotoA_Click(object sender, EventArgs e)
        {
            if (Model.Program.EventScripts == null || !Model.Program.EventScripts.Visible)
                Model.Program.CreateEventScriptsWindow();
            int offset = (int)npcEventPointer.Value + 0x0A0000;
            Model.Program.EventScripts.GotoAddress(offset);
            Model.Program.EventScripts.BringToFront();
        }
        private void npcEventPointer_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            npcs.EventPointer = (int)npcEventPointer.Value;
            this.picture.Invalidate();
        }
        private void npcPalette_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            npcs.PaletteNum = (byte)npcPalette.Value;
            npcs.Pixels = null;
            this.picture.Invalidate();
        }
        private void npcX_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            npcs.X = (byte)npcX.Value;
            this.picture.Invalidate();
        }
        private void npcY_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            npcs.Y = (byte)npcY.Value;
            this.picture.Invalidate();
        }
        private void npcSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            npcs.Speed = (byte)npcSpeed.SelectedIndex;
        }
        private void npcSpriteSet_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            npcs.SpriteNum = (byte)npcSpriteSet.Value;
            npcs.Pixels = null;
            this.picture.Invalidate();
        }
        private void npcSpriteIndex_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            npcs.Action = (byte)npcSpriteIndex.Value;
        }
        private void npcWalkability_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            npcs.SolidifyActionPath = npcWalkability.GetItemChecked(0);
            npcs.WalkUnder = npcWalkability.GetItemChecked(1);
            npcs.WalkOver = npcWalkability.GetItemChecked(2);
            npcs.DontFaceOnTrigger = npcWalkability.GetItemChecked(3);
        }
        private void npcVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            npcs.Vehicle = (byte)npcVehicle.SelectedIndex;
        }
        private void npcFace_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            npcs.F = (byte)npcFace.SelectedIndex;
            npcs.Pixels = null;
            this.picture.Invalidate();
        }
        private void npcCheckMem_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            npcs.CheckMem = (ushort)npcCheckMem.Value;
        }
        private void npcCheckBit_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            npcs.CheckBit = (byte)npcCheckBit.Value;
        }
        private void npcUnknownBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            npcs.B4b7 = npcUnknownBits.GetItemChecked(0);
            npcs.B8b3 = npcUnknownBits.GetItemChecked(1);
            npcs.B8b4 = npcUnknownBits.GetItemChecked(2);
            npcs.B8b5 = npcUnknownBits.GetItemChecked(3);
            npcs.B8b6 = npcUnknownBits.GetItemChecked(4);
            npcs.B8b7 = npcUnknownBits.GetItemChecked(5);
            npcs.Pixels = null;
            this.picture.Invalidate();
        }
        #endregion
    }
}
