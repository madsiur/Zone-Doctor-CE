using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZONEDOCTOR.ScriptsEditor;

namespace ZONEDOCTOR
{
    public partial class ClearElements : NewForm
    {
        private object element;
        private int currentIndex;
        private int start = 0;
        private int end = 0;
        private int indexNum;
        private Type type;
        public ClearElements(object element, int currentIndex, string title, int indexNum)
        {
            this.element = element;
            this.currentIndex = currentIndex;
            this.start = this.end = currentIndex;
            this.indexNum = indexNum;
            if (element != null)
                this.type = element.GetType();
            InitializeComponent();
            this.Text = title;
            if (type != null)
                toIndex.Value = toIndex.Maximum = ((object[])element).Length - 1;
            else if (type == null && this.Text == "CLEAR LOCATION DATA...")
                toIndex.Value = toIndex.Maximum = Model.Locations.Length - 1;
            else if (type == null && this.Text == "CLEAR TILESETS...")
                toIndex.Value = toIndex.Maximum = Model.Tilesets.Length - 1;
            else if (type == null && this.Text == "CLEAR TILEMAPS...")
                toIndex.Value = toIndex.Maximum = Model.Tilemaps.Length - 1;
            else if (type == null && this.Text == "CLEAR SOLIDITY SETS...")
                toIndex.Value = toIndex.Maximum = Model.SoliditySets.Length - 1;
            start = end = currentIndex;
            if (indexNum < 3)
                radioButton2.Enabled = false;
        }
        private void fromDialogue_ValueChanged(object sender, EventArgs e)
        {
            toIndex.Minimum = fromIndex.Value;
            start = (int)fromIndex.Value;
        }
        private void toDialogue_ValueChanged(object sender, EventArgs e)
        {
            fromIndex.Maximum = toIndex.Value;
            end = (int)toIndex.Value;
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            for (int i = start; type != null && i <= end && indexNum >= 3; i++)
                ((Element[])element)[i].Clear();
            // event scripts
            if (type == null && this.Text == "CLEAR EVENT SCRIPTS...")
            {
                for (int i = start; i <= end; i++)
                    Model.EventScripts[i].Clear();
            }
            // locations
            if (type == null && this.Text == "CLEAR LOCATION DATA...")
            {
                for (int i = start; i <= end; i++)
                {
                    Model.Locations[i].LocationMap.Clear();
                    Model.Locations[i].LocationEvents.Clear();
                    Model.Locations[i].LocationExits.Clear();
                    Model.Locations[i].LocationNPCs.Clear();
                    Model.Locations[i].LocationTreasures.Clear();
                }
            }
            if (type == null && this.Text == "CLEAR TILESETS...")
            {
                for (int i = 0; i < 0x400; i++)
                {
                    if (indexNum == 0)
                        Model.WOBGraphicSet[i] = 0;
                    else if (indexNum == 1)
                        Model.WORGraphicSet[i] = 0;
                    else if (indexNum == 2)
                        Model.STGraphicSet[i] = 0;
                }
                if (indexNum >= 3)
                {
                    for (int i = start; i <= end; i++)
                    {
                        Model.Tilesets[i] = new byte[0x800];
                        Model.EditTilesets[i] = true;
                    }
                }
            }
            if (type == null && this.Text == "CLEAR TILEMAPS...")
            {
                if (indexNum == 0)
                    Model.WOBTilemap = new byte[0x10000];
                else if (indexNum == 1)
                    Model.WORTilemap = new byte[0x10000];
                else if (indexNum == 2)
                    Model.STTilemap = new byte[0x4000];
                else
                {
                    for (int i = start; i <= end; i++)
                    {
                        Model.Tilemaps[i] = new byte[Model.TilemapSizes[i]];
                        Model.EditTilemaps[i] = true;
                    }
                }
            }
            if (type == null && this.Text == "CLEAR SOLIDITY SETS...")
            {
                if (indexNum == 0)
                    Model.WOBSolidity = new byte[0x200];
                else if (indexNum == 1)
                    Model.WORSolidity = new byte[0x200];
                else if (indexNum >= 3)
                {
                    for (int i = start; i < end; i++)
                    {
                        Model.SoliditySets[i] = new byte[0x200];
                        Model.EditSoliditySets[i] = true;
                    }
                }
            }
            this.Tag = new Point(start, end);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                fromIndex.Enabled = false;
                toIndex.Enabled = false;
                start = end = currentIndex;
            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                fromIndex.Enabled = true;
                toIndex.Enabled = true;
                start = (int)fromIndex.Value;
                end = (int)toIndex.Value;
            }
        }
    }
}
