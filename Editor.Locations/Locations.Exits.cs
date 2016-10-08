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
        private LocationExits exits { get { return location.LocationExits; } set { location.LocationExits = value; } }
        public ListBox ExitListBox { get { return exitListBox; } set { exitListBox = value; } }
        public NumericUpDown ExitX { get { return exitX; } set { exitX = value; } }
        public NumericUpDown ExitY { get { return exitY; } set { exitY = value; } }
        private Object copyExit;
        #endregion
        #region Methods
        private void InitializeExitProperties()
        {
            this.Updating = true;
            exitListBox.Items.Clear();
            if (exits.Exits.Count == 0)
            {
                this.Updating = false;
                groupBox15.Enabled = false;
                groupBox16.Enabled = false;
                groupBox18.Enabled = false;
                exitsCopyField.Enabled = false;
                exitsDuplicateField.Enabled = false;
                buttonDeleteExit.Enabled = false;
                return;
            }
            //
            for (int i = 0; i < exits.Exits.Count; i++)
                exitListBox.Items.Add("EXIT #" + i.ToString());
            exits.CurrentExit = exits.SelectedExit = exitListBox.SelectedIndex = 0;
            RefreshExitProperties();
            this.Updating = false;
        }
        private void RefreshExitProperties()
        {
            this.Updating = true;
            groupBox15.Enabled = exits.Exits.Count != 0;
            groupBox16.Enabled = exits.Exits.Count != 0;
            groupBox18.Enabled = exits.Exits.Count != 0;
            exitsCopyField.Enabled = exits.Exits.Count != 0;
            exitsDuplicateField.Enabled = exits.Exits.Count != 0;
            buttonDeleteExit.Enabled = exits.Exits.Count != 0;
            //
            exitX.Value = exits.X;
            exitY.Value = exits.Y;
            exitWidth.Value = exits.Width + 1;
            exitDirection.SelectedIndex = exits.F;
            exitToWorldMap.Checked = exits.ToWorldMap;
            if (exits.ToWorldMap)
            {
                exitDestination.Enabled = false;
                exitDestination.SelectedIndex = 0;
            }
            else
            {
                exitDestination.Enabled = true;
                exitDestination.SelectedIndex = exits.Destination;
            }
            exitDestinationX.Value = exits.DstX;
            exitDestinationY.Value = exits.DstY;
            exitDestinationFacing.SelectedIndex = exits.DstF;
            exitShowMessage.Checked = exits.ShowMessage;
            exitUnknownBits.SetItemChecked(0, exits.refreshParentMap);
            exitUnknownBits.SetItemChecked(1, exits.B3b2);
            if (exits.Width > 0)
            {
                exitsBytesLeft.Text = CalculateFreeExitLongSpace() + " bytes left";
                exitsBytesLeft.BackColor = CalculateFreeExitLongSpace() >= 0 ? SystemColors.Control : Color.Red;
            }
            else
            {
                exitsBytesLeft.Text = CalculateFreeExitShortSpace() + " bytes left";
                exitsBytesLeft.BackColor = CalculateFreeExitShortSpace() >= 0 ? SystemColors.Control : Color.Red;
            }
            this.Updating = false;
        }
        
        private int CalculateFreeExitShortSpace()
        {
            int used = 0;
            
            for (int i = 0; i < Model.NUM_LOCATIONS; i++)
            {
                foreach (Exit exit in locations[i].LocationExits.Exits)
                    if (exit.Width == 0)
                        used += 6;
            }
            
            return Model.SIZE_SHORT_EXIT_DATA - used;
        }
        
        private int CalculateFreeExitLongSpace()
        {
            int used = 0;
            
            for (int i = 0; i < Model.NUM_LOCATIONS; i++)
            {
                foreach (Exit exit in locations[i].LocationExits.Exits)
                    if (exit.Width > 1)
                        used += 7;
            }
            
            return Model.SIZE_LONG_EXIT_DATA - used;
        }
        //
        private void AddNewExit(Exit exit)
        {
            if (CalculateFreeExitShortSpace() >= 6)
            {
                this.exitListBox.Focus();
                if (exits.Count < 64)
                {
                    if (exitListBox.Items.Count > 0)
                        exits.New(exitListBox.SelectedIndex + 1, exit);
                    else
                        exits.New(0, exit);
                    int reselect;
                    if (exitListBox.Items.Count > 0)
                        reselect = exitListBox.SelectedIndex;
                    else
                        reselect = -1;
                    exitListBox.BeginUpdate();
                    this.exitListBox.Items.Clear();
                    for (int i = 0; i < exits.Count; i++)
                        this.exitListBox.Items.Add("EXIT #" + i.ToString());
                    this.exitListBox.SelectedIndex = reselect + 1;
                    exitListBox.EndUpdate();
                }
                else
                    MessageBox.Show("Could not insert any more exit fields. The maximum number of exit fields allowed per location is 64.",
                        Model.APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Could not insert the field. " + MaximumSpaceExceeded("exit"),
                    Model.APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
        #region Event Handlers
        private void exitListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            exits.CurrentExit = exitListBox.SelectedIndex;
            exits.SelectedExit = exitListBox.SelectedIndex;
            RefreshExitProperties();
            this.picture.Invalidate();
        }
        private void buttonInsertExit_Click(object sender, EventArgs e)
        {
            Point p = new Point(Math.Abs(this.picture.Left) / 16, Math.Abs(this.picture.Top) / 16);
            if (CalculateFreeExitShortSpace() >= 6)
            {
                this.exitListBox.Focus();
                if (exits.Count < 64)
                {
                    if (exitListBox.Items.Count > 0)
                        exits.New(exitListBox.SelectedIndex + 1, p);
                    else
                        exits.New(0, p);
                    int reselect;
                    if (exitListBox.Items.Count > 0)
                        reselect = exitListBox.SelectedIndex;
                    else
                        reselect = -1;
                    exitListBox.BeginUpdate();
                    this.exitListBox.Items.Clear();
                    for (int i = 0; i < exits.Count; i++)
                        this.exitListBox.Items.Add("EXIT #" + i.ToString());
                    this.exitListBox.SelectedIndex = reselect + 1;
                    exitListBox.EndUpdate();
                }
                else
                    MessageBox.Show("Could not insert any more exit fields. The maximum number of exit fields allowed per location is 64.",
                        Model.APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Could not insert the field. " + MaximumSpaceExceeded("exit"),
                   Model.APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void buttonDeleteExit_Click(object sender, EventArgs e)
        {
            exitListBox.Focus();
            if (exitListBox.SelectedIndex != -1 && exits.CurrentExit == exitListBox.SelectedIndex)
            {
                exits.Remove();
                int reselect = exitListBox.SelectedIndex;
                if (reselect == exitListBox.Items.Count - 1)
                    reselect--;
                exitListBox.BeginUpdate();
                exitListBox.Items.Clear();
                for (int i = 0; i < exits.Exits.Count; i++)
                    exitListBox.Items.Add("EXIT #" + i.ToString());
                if (exitListBox.Items.Count > 0)
                    exitListBox.SelectedIndex = reselect;
                else
                {
                    exitListBox.SelectedIndex = -1;
                    this.picture.Invalidate();
                    panel1.Enabled = false;
                    RefreshExitProperties();
                }
                exitListBox.EndUpdate();
            }
        }
        private void exitX_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            exits.X = (byte)exitX.Value;
            this.picture.Invalidate();
        }
        private void exitY_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            exits.Y = (byte)exitY.Value;
            this.picture.Invalidate();
        }
        private void exitWidth_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            // if original exit is short, and changing to long, and if enough space in long exit data
            if (exits.Width == 0 && exitWidth.Value > 1 && CalculateFreeExitLongSpace() < 7)
            {
                MessageBox.Show("Could not change the width. " + MaximumSpaceExceeded("exit"),
                   Model.APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // if original exit is long, and changing to short, and if enough space in short exit data
            if (exits.Width > 0 && exitWidth.Value == 1 && CalculateFreeExitShortSpace() < 6)
            {
                MessageBox.Show("Could not change the width. " + MaximumSpaceExceeded("exit"),
                    Model.APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            exits.Width = (byte)(exitWidth.Value - 1);
            this.picture.Invalidate();
            if (exits.Width == 0)
            {
                exitsBytesLeft.Text = CalculateFreeExitShortSpace() + " bytes left";
                exitsBytesLeft.BackColor = CalculateFreeExitShortSpace() >= 0 ? SystemColors.Control : Color.Red;
            }
            if (exits.Width > 0)
            {
                exitsBytesLeft.Text = CalculateFreeExitLongSpace() + " bytes left";
                exitsBytesLeft.BackColor = CalculateFreeExitLongSpace() >= 0 ? SystemColors.Control : Color.Red;
            }
        }
        private void exitDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            exits.F = (byte)exitDirection.SelectedIndex;
            this.picture.Invalidate();
        }
        private void exitToWorldMap_CheckedChanged(object sender, EventArgs e)
        {
            exitToWorldMap.ForeColor = exitToWorldMap.Checked ? Color.Black : SystemColors.ControlDark;
            if (this.Updating)
                return;
            exits.ToWorldMap = exitToWorldMap.Checked;
            exitDestination.Enabled = !exits.ToWorldMap;
        }
        private void exitDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            exits.Destination = (ushort)exitDestination.SelectedIndex;
            picture.Invalidate();
        }
        private void exitDestinationX_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            exits.DstX = (byte)exitDestinationX.Value;
        }
        private void exitDestinationY_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            exits.DstY = (byte)exitDestinationY.Value;
        }
        private void exitDestinationFacing_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            exits.DstF = (byte)exitDestinationFacing.SelectedIndex;
        }
        private void exitUnknownBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            exits.refreshParentMap = exitUnknownBits.GetItemChecked(0);
            exits.B3b2 = exitUnknownBits.GetItemChecked(1);
        }
        private void exitShowMessage_CheckedChanged(object sender, EventArgs e)
        {
            exitShowMessage.ForeColor = exitShowMessage.Checked ? SystemColors.ControlText : SystemColors.ControlDark;
            if (this.Updating)
                return;
            exits.ShowMessage = exitShowMessage.Checked;
        }
        private void exitsCopyField_Click(object sender, EventArgs e)
        {
            copyExit = exits.Exit.Copy();
            exitsPasteField.Enabled = true;
        }
        private void exitsPasteField_Click(object sender, EventArgs e)
        {
            if (copyExit == null)
                return;
            AddNewExit((Exit)copyExit);
        }
        private void exitsDuplicateField_Click(object sender, EventArgs e)
        {
            AddNewExit(exits.Exit.Copy());
        }
        #endregion
    }
}
