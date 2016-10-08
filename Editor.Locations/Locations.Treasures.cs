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
        private LocationTreasures treasures { get { return location.LocationTreasures; } set { location.LocationTreasures = value; } }
        public NumericUpDown TreasureX { get { return treasureXCoord; } set { treasureXCoord = value; } }
        public NumericUpDown TreasureY { get { return treasureYCoord; } set { treasureYCoord = value; } }
        private Object copyTreasure;
        #endregion
        #region Methods
        private void InitializeTreasureProperties()
        {
            this.Updating = true;
            treasureListBox.Items.Clear();
            if (treasures.Treasures.Count == 0)
            {
                this.Updating = false;
                groupBox19.Enabled = false;
                groupBox20.Enabled = false;
                treasureCopy.Enabled = false;
                treasureDuplicate.Enabled = false;
                buttonDeleteTreasure.Enabled = false;
                treasureMoveDown.Enabled = false;
                treasureMoveUp.Enabled = false;
                return;
            }
            //
            for (int i = 0; i < treasures.Treasures.Count; i++)
                treasureListBox.Items.Add("TREASURE #" + i.ToString());
            treasures.CurrentTreasure = treasures.SelectedTreasure = treasureListBox.SelectedIndex = 0;
            RefreshTreasureProperties();
            this.Updating = false;
        }
        private void RefreshTreasureProperties()
        {
            this.Updating = true;
            groupBox19.Enabled = treasures.Treasures.Count != 0;
            groupBox20.Enabled = treasures.Treasures.Count != 0;
            treasureCopy.Enabled = treasures.Treasures.Count != 0;
            treasureDuplicate.Enabled = treasures.Treasures.Count != 0;
            buttonDeleteTreasure.Enabled = treasures.Treasures.Count != 0;
            treasureMoveDown.Enabled = treasures.Treasures.Count != 0;
            treasureMoveUp.Enabled = treasures.Treasures.Count != 0;
            //
            treasureCheckMem.Value = treasures.CheckMem;
            treasureCheckBit.Value = treasures.CheckBit;
            treasureXCoord.Value = treasures.X;
            treasureYCoord.Value = treasures.Y;
            treasureType.SelectedIndex = treasures.Type;
            switch (treasureType.SelectedIndex)
            {
                case 0:
                case 1:
                    treasurePropertyNum.Enabled = false;
                    treasurePropertyName.Enabled = false;
                    label84.Text = "";
                    label83.Text = "";
                    break;
                case 2:
                    treasurePropertyNum.Enabled = true;
                    treasurePropertyName.Enabled = false;
                    treasurePropertyNum.Maximum = 255;
                    treasurePropertyNum.Increment = 1;
                    treasurePropertyNum.Value = treasures.PropertyNum;
                    label84.Text = "Pack #";
                    label83.Text = "";
                    break;
                case 3:
                    treasurePropertyNum.Enabled = true;
                    treasurePropertyName.Enabled = true;
                    treasurePropertyNum.Maximum = 255;
                    treasurePropertyNum.Increment = 1;
                    treasurePropertyNum.Value = treasures.PropertyNum;
                    treasurePropertyName.SelectedIndex = treasures.PropertyNum;
                    label84.Text = "Item #";
                    label83.Text = "Item Name";
                    break;
                case 4:
                    treasurePropertyName.Enabled = false;
                    treasurePropertyNum.Enabled = true;
                    treasurePropertyNum.Maximum = 25500;
                    treasurePropertyNum.Increment = 100;
                    treasurePropertyNum.Value = treasures.PropertyNum * 100;
                    label84.Text = "GP Amount";
                    label83.Text = "";
                    break;
            }
            treasuresBytesLeft.Text = CalculateFreeTreasureSpace() + " bytes left";
            treasuresBytesLeft.BackColor = CalculateFreeTreasureSpace() >= 0 ? SystemColors.Control : Color.Red;
            this.Updating = false;
        }
        
        private int CalculateFreeTreasureSpace()
        {
            int used = 0;
            
            for (int i = 0; i < Model.NUM_LOCATIONS; i++)
            {
                for (int a = 0; a < locations[i].LocationTreasures.Treasures.Count; a++)
                    used += 5;
            }
            
            return Model.SIZE_CHEST_DATA - used;
        }
        //
        private void AddNewTreasure()
        {
            Point p = new Point(Math.Abs(this.picture.Left) / 16, Math.Abs(this.picture.Top) / 16);
            if (CalculateFreeTreasureSpace() >= 5)
            {
                if (treasureListBox.Items.Count < 32)
                {
                    if (treasureListBox.Items.Count > 0)
                        treasures.New(treasureListBox.SelectedIndex + 1, p);
                    else
                        treasures.New(0, p);
                    int reselect;
                    if (treasureListBox.Items.Count > 0)
                        reselect = treasureListBox.SelectedIndex;
                    else
                        reselect = -1;
                    treasureListBox.BeginUpdate();
                    this.treasureListBox.Items.Clear();
                    for (int i = 0, a = 0; i < treasures.Treasures.Count; i++, a++)
                    {
                        this.treasureListBox.Items.Add("TREASURE #" + a.ToString());
                        treasures.CurrentTreasure = i;
                    }
                    this.treasureListBox.SelectedIndex = reselect + 1;
                    treasureListBox.EndUpdate();
                }
                else
                    MessageBox.Show("Could not insert any more treasures. The maximum number of treasures allowed per location is 32.",
                        Model.APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Could not insert the treasure. " + MaximumSpaceExceeded("treasure"),
                    Model.APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void AddNewTreasure(Treasure e)
        {
            Point p = new Point(Math.Abs(this.picture.Left) / 16, Math.Abs(this.picture.Top) / 16);
            if (CalculateFreeTreasureSpace() >= 9)
            {
                if (treasureListBox.Items.Count < 32)
                {
                    if (treasureListBox.Items.Count > 0)
                        treasures.New(treasureListBox.SelectedIndex + 1, e);
                    else
                        treasures.New(0, e);
                    int reselect;
                    if (treasureListBox.Items.Count > 0)
                        reselect = treasureListBox.SelectedIndex;
                    else
                        reselect = -1;
                    treasureListBox.BeginUpdate();
                    this.treasureListBox.Items.Clear();
                    for (int i = 0, a = 0; i < treasures.Treasures.Count; i++, a++)
                    {
                        this.treasureListBox.Items.Add("TREASURE #" + a.ToString());
                        treasures.CurrentTreasure = i;
                    }
                    this.treasureListBox.SelectedIndex = reselect + 1;
                    treasureListBox.EndUpdate();
                }
                else
                    MessageBox.Show("Could not insert any more treasures. The maximum number of treasures allowed per location is 32.",
                        Model.APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Could not insert the treasure. " + MaximumSpaceExceeded("treasure"),
                    Model.APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
        #region Event Handlers
        public ListBox TreasureListBox { get { return treasureListBox; } set { treasureListBox = value; } }
        private void treasureListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            treasures.CurrentTreasure = treasureListBox.SelectedIndex;
            treasures.SelectedTreasure = treasureListBox.SelectedIndex;
            RefreshTreasureProperties();
            this.picture.Invalidate();
        }
        private void buttonInsertTreasure_Click(object sender, EventArgs e)
        {
            AddNewTreasure();
        }
        private void buttonDeleteTreasure_Click(object sender, EventArgs e)
        {
            treasureListBox.Focus();
            if (treasureListBox.SelectedIndex != -1 && treasures.CurrentTreasure == treasureListBox.SelectedIndex)
            {
                treasures.Remove();
                int reselect = treasureListBox.SelectedIndex;
                if (reselect == treasureListBox.Items.Count - 1)
                    reselect--;
                treasureListBox.BeginUpdate();
                treasureListBox.Items.Clear();
                for (int i = 0; i < treasures.Treasures.Count; i++)
                    treasureListBox.Items.Add("TREASURE #" + i.ToString());
                if (treasureListBox.Items.Count > 0)
                    treasureListBox.SelectedIndex = reselect;
                else
                {
                    treasureListBox.SelectedIndex = -1;
                    this.picture.Invalidate();
                    groupBox19.Enabled = groupBox20.Enabled = false;
                    RefreshTreasureProperties();
                }
                treasureListBox.EndUpdate();
            }
        }
        private void treasureMoveUp_Click(object sender, EventArgs e)
        {
            int reselectP = 0;
            if (treasures.CurrentTreasure > 0)
            {
                reselectP = treasureListBox.SelectedIndex - 1;
                treasures.Reverse(treasures.CurrentTreasure - 1);
            }
            else return;
            this.treasureListBox.BeginUpdate();
            this.treasureListBox.Items.Clear();
            for (int i = 0, a = 0; i < treasures.Treasures.Count; i++, a++)
            {
                this.treasureListBox.Items.Add("TREASURE #" + a.ToString());
                treasures.CurrentTreasure = i;
            }
            this.treasureListBox.SelectedIndex = reselectP;
            this.treasureListBox.EndUpdate();
        }
        private void treasureMoveDown_Click(object sender, EventArgs e)
        {
            int reselectP = 0;
            if (treasures.CurrentTreasure < treasures.Treasures.Count - 1)
            {
                reselectP = treasureListBox.SelectedIndex + 1;
                treasures.Reverse(treasures.CurrentTreasure);
            }
            else return;
            this.treasureListBox.BeginUpdate();
            this.treasureListBox.Items.Clear();
            for (int i = 0, a = 0; i < treasures.Treasures.Count; i++, a++)
            {
                this.treasureListBox.Items.Add("TREASURE #" + a.ToString());
                treasures.CurrentTreasure = i;
            }
            this.treasureListBox.SelectedIndex = reselectP;
            this.treasureListBox.EndUpdate();
        }
        private void treasureCopy_Click(object sender, EventArgs e)
        {
            copyTreasure = treasures.Treasure.Copy();
            treasurePaste.Enabled = true;
        }
        private void treasurePaste_Click(object sender, EventArgs e)
        {
            if (copyTreasure == null)
                return;
            AddNewTreasure((Treasure)copyTreasure);
        }
        private void treasureDuplicate_Click(object sender, EventArgs e)
        {
            AddNewTreasure(treasures.Treasure.Copy());
        }
        private void treasureXCoord_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            treasures.X = (byte)treasureXCoord.Value;
            this.picture.Invalidate();
        }
        private void treasureYCoord_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            treasures.Y = (byte)treasureYCoord.Value;
            this.picture.Invalidate();
        }
        private void treasureCheckMem_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            treasures.CheckMem = (ushort)treasureCheckMem.Value;
        }
        private void treasureCheckBit_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            treasures.CheckBit = (byte)treasureCheckBit.Value;
        }
        private void treasureType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            treasures.Type = (byte)treasureType.SelectedIndex;
            this.Updating = true;
            switch (treasureType.SelectedIndex)
            {
                case 0:
                case 1:
                    treasurePropertyNum.Enabled = false;
                    treasurePropertyName.Enabled = false;
                    label84.Text = "";
                    label83.Text = "";
                    break;
                case 2:
                    treasurePropertyNum.Enabled = true;
                    treasurePropertyName.Enabled = false;
                    treasurePropertyNum.Maximum = 255;
                    treasurePropertyNum.Increment = 1;
                    treasurePropertyNum.Value = treasures.PropertyNum;
                    label84.Text = "Pack #";
                    label83.Text = "";
                    break;
                case 3:
                    treasurePropertyNum.Enabled = true;
                    treasurePropertyName.Enabled = true;
                    treasurePropertyNum.Maximum = 255;
                    treasurePropertyNum.Increment = 1;
                    treasurePropertyNum.Value = treasures.PropertyNum;
                    treasurePropertyName.SelectedIndex = treasures.PropertyNum;
                    label84.Text = "Item #";
                    label83.Text = "Item Name";
                    break;
                case 4:
                    treasurePropertyName.Enabled = false;
                    treasurePropertyNum.Enabled = true;
                    treasurePropertyNum.Maximum = 25500;
                    treasurePropertyNum.Increment = 100;
                    treasurePropertyNum.Value = treasures.PropertyNum * 100;
                    label84.Text = "GP Amount";
                    label83.Text = "";
                    break;
            }
            this.Updating = false;
            this.picture.Invalidate();
        }
        private void treasurePropertyNum_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            if (treasureType.SelectedIndex == 4)
            {
                treasures.PropertyNum = (byte)(treasurePropertyNum.Value / 100);
                treasurePropertyNum.Value = (int)treasurePropertyNum.Value / 100 * 100;
            }
            else
                treasures.PropertyNum = (byte)treasurePropertyNum.Value;
            if (treasureType.SelectedIndex == 3)
                treasurePropertyName.SelectedIndex = (int)treasurePropertyNum.Value;
            this.picture.Invalidate();
        }
        private void treasurePropertyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            treasurePropertyNum.Value = treasurePropertyName.SelectedIndex;
            this.picture.Invalidate();
        }
        #endregion
    }
}
