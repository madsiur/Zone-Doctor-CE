using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ZONEDOCTOR
{
    public partial class Locations
    {
        #region Variables
        private PrioritySet[] prioritySets; public PrioritySet[] PrioritySets { get { return prioritySets; } }
        public NumericUpDown LayerMaskHighX { get { return layerMaskHighX; } set { layerMaskHighX = value; } }
        public NumericUpDown LayerMaskHighY { get { return layerMaskHighY; } set { layerMaskHighY = value; } }
        #endregion
        #region Methods
        private void InitializeLayerProperties()
        {
            this.Updating = true;

            // madsiur, for location renaming feature [3.18.4-0.1]
            this.messageName.SelectedIndex = locationMap.MessageBox;
            this.tbLocation.Text = this.messageName.SelectedItem.ToString();

            this.musicName.SelectedIndex = locationMap.Music;
            this.layerPrioritySet.Value = locationMap.PrioritySet;
            this.layerMainscreenL1.Checked = prioritySets[locationMap.PrioritySet].MainscreenL1;
            this.layerMainscreenL2.Checked = prioritySets[locationMap.PrioritySet].MainscreenL2;
            this.layerMainscreenL3.Checked = prioritySets[locationMap.PrioritySet].MainscreenL3;
            this.layerMainscreenNPC.Checked = prioritySets[locationMap.PrioritySet].MainscreenOBJ;
            this.layerSubscreenL1.Checked = prioritySets[locationMap.PrioritySet].SubscreenL1;
            this.layerSubscreenL2.Checked = prioritySets[locationMap.PrioritySet].SubscreenL2;
            this.layerSubscreenL3.Checked = prioritySets[locationMap.PrioritySet].SubscreenL3;
            this.layerSubscreenNPC.Checked = prioritySets[locationMap.PrioritySet].SubscreenOBJ;
            this.layerColorMathL1.Checked = prioritySets[locationMap.PrioritySet].ColorMathL1;
            this.layerColorMathL2.Checked = prioritySets[locationMap.PrioritySet].ColorMathL2;
            this.layerColorMathL3.Checked = prioritySets[locationMap.PrioritySet].ColorMathL3;
            this.layerColorMathNPC.Checked = prioritySets[locationMap.PrioritySet].ColorMathOBJ;
            this.layerColorMathBG.Checked = prioritySets[locationMap.PrioritySet].ColorMathBG;
            this.layerColorMathIntensity.SelectedIndex = prioritySets[locationMap.PrioritySet].ColorMathHalfIntensity;
            this.layerColorMathMode.SelectedIndex = prioritySets[locationMap.PrioritySet].ColorMathMinusSubscreen;
            this.layerMaskHighX.Value = locationMap.MaskHighX;
            this.layerMaskHighY.Value = locationMap.MaskHighY;
            this.spriteMask.Value = locationMap.SpriteMask;
            this.topSync.Value = locationMap.Scrolling;
            this.mapSetL3Priority.Checked = locationMap.TopPriorityL3;
            this.layerL2LeftShift.Value = locationMap.XNegL2;
            this.layerL2UpShift.Value = locationMap.YNegL2;
            this.layerL3LeftShift.Value = locationMap.XNegL3;
            this.layerL3UpShift.Value = locationMap.YNegL3;
            this.l1height.SelectedIndex = locationMap.Height[0];
            this.l1width.SelectedIndex = locationMap.Width[0];
            this.l2height.SelectedIndex = locationMap.Height[1];
            this.l2width.SelectedIndex = locationMap.Width[1];
            this.l3height.SelectedIndex = locationMap.Height[2];
            this.l3width.SelectedIndex = locationMap.Width[2];
            this.windowMask.SelectedIndex = locationMap.WindowMask;
            this.layerEffects.SetItemChecked(0, locationMap.HeatWaveL1);
            this.layerEffects.SetItemChecked(1, locationMap.HeatWaveL2);
            this.layerEffects.SetItemChecked(2, locationMap.HeatWaveL3);
            this.layerEffects.SetItemChecked(3, locationMap.WarpEnabled);
            this.layerEffects.SetItemChecked(4, locationMap.WarpEnabledX);
            this.layerEffects.SetItemChecked(5, locationMap.SearchLights);
            this.layerUnknownBits.SetItemChecked(0, locationMap.B1b6);
            this.layerUnknownBits.SetItemChecked(1, locationMap.B6b7);
            this.Updating = false;
        }
        private Size LayerSize(int layer)
        {
            Size s = new Size();
            s.Width = (int)(256 * Math.Pow(2, this.locationMap.Width[layer]));
            s.Height = (int)(256 * Math.Pow(2, this.locationMap.Height[layer]));
            return s;
        }
        #endregion
        #region Event Handlers
        private void layerMessageBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // madsiur, for location renaming feature [3.18.4-0.1]
            locationMap.MessageBox = (byte)messageName.SelectedIndex;
            tbLocation.Text = messageName.SelectedItem.ToString();
        }
        private void layerPrioritySet_ValueChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
                locationMap.PrioritySet = (byte)layerPrioritySet.Value;
            this.Updating = true;
            this.layerMainscreenL1.Checked = prioritySets[locationMap.PrioritySet].MainscreenL1;
            this.layerMainscreenL2.Checked = prioritySets[locationMap.PrioritySet].MainscreenL2;
            this.layerMainscreenL3.Checked = prioritySets[locationMap.PrioritySet].MainscreenL3;
            this.layerMainscreenNPC.Checked = prioritySets[locationMap.PrioritySet].MainscreenOBJ;
            this.layerSubscreenL1.Checked = prioritySets[locationMap.PrioritySet].SubscreenL1;
            this.layerSubscreenL2.Checked = prioritySets[locationMap.PrioritySet].SubscreenL2;
            this.layerSubscreenL3.Checked = prioritySets[locationMap.PrioritySet].SubscreenL3;
            this.layerSubscreenNPC.Checked = prioritySets[locationMap.PrioritySet].SubscreenOBJ;
            this.layerColorMathL1.Checked = prioritySets[locationMap.PrioritySet].ColorMathL1;
            this.layerColorMathL2.Checked = prioritySets[locationMap.PrioritySet].ColorMathL2;
            this.layerColorMathL3.Checked = prioritySets[locationMap.PrioritySet].ColorMathL3;
            this.layerColorMathNPC.Checked = prioritySets[locationMap.PrioritySet].ColorMathOBJ;
            this.layerColorMathBG.Checked = prioritySets[locationMap.PrioritySet].ColorMathBG;
            this.layerColorMathIntensity.SelectedIndex = prioritySets[locationMap.PrioritySet].ColorMathHalfIntensity;
            this.layerColorMathMode.SelectedIndex = prioritySets[locationMap.PrioritySet].ColorMathMinusSubscreen;
            this.Updating = false;
            if (!this.Refreshing)
            {
                RefreshLocation();
            }
        }
        private void layerMainscreenL1_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                this.prioritySets[locationMap.PrioritySet].MainscreenL1 = layerMainscreenL1.Checked;
                if (!this.Refreshing)
                    RefreshLocation();
            }
            if (layerMainscreenL1.Checked) layerMainscreenL1.ForeColor = Color.Black;
            else layerMainscreenL1.ForeColor = Color.Gray;
        }
        private void layerMainscreenL2_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                this.prioritySets[locationMap.PrioritySet].MainscreenL2 = layerMainscreenL2.Checked;
                if (!this.Refreshing)
                    RefreshLocation();
            }
            if (layerMainscreenL2.Checked) layerMainscreenL2.ForeColor = Color.Black;
            else layerMainscreenL2.ForeColor = Color.Gray;
        }
        private void layerMainscreenL3_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                this.prioritySets[locationMap.PrioritySet].MainscreenL3 = layerMainscreenL3.Checked;
                if (!this.Refreshing)
                    RefreshLocation();
            }
            if (layerMainscreenL3.Checked) layerMainscreenL3.ForeColor = Color.Black;
            else layerMainscreenL3.ForeColor = Color.Gray;
        }
        private void layerMainscreenNPC_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                this.prioritySets[locationMap.PrioritySet].MainscreenOBJ = layerMainscreenNPC.Checked;
                if (!this.Refreshing)
                    RefreshLocation();
            }
            if (layerMainscreenNPC.Checked) layerMainscreenNPC.ForeColor = Color.Black;
            else layerMainscreenNPC.ForeColor = Color.Gray;
        }
        private void layerSubscreenL1_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                this.prioritySets[locationMap.PrioritySet].SubscreenL1 = layerSubscreenL1.Checked;
                if (!this.Refreshing)
                    RefreshLocation();
            }
            if (layerSubscreenL1.Checked) layerSubscreenL1.ForeColor = Color.Black;
            else layerSubscreenL1.ForeColor = Color.Gray;
        }
        private void layerSubscreenL2_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                this.prioritySets[locationMap.PrioritySet].SubscreenL2 = layerSubscreenL2.Checked;
                if (!this.Refreshing)
                    RefreshLocation();
            }
            if (layerSubscreenL2.Checked) layerSubscreenL2.ForeColor = Color.Black;
            else layerSubscreenL2.ForeColor = Color.Gray;
        }
        private void layerSubscreenL3_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                this.prioritySets[locationMap.PrioritySet].SubscreenL3 = layerSubscreenL3.Checked;
                if (!this.Refreshing)
                    RefreshLocation();
            }
            if (layerSubscreenL3.Checked) layerSubscreenL3.ForeColor = Color.Black;
            else layerSubscreenL3.ForeColor = Color.Gray;
        }
        private void layerSubscreenNPC_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                this.prioritySets[locationMap.PrioritySet].SubscreenOBJ = layerSubscreenNPC.Checked;
                if (!this.Refreshing)
                    RefreshLocation();
            }
            if (layerSubscreenNPC.Checked) layerSubscreenNPC.ForeColor = Color.Black;
            else layerSubscreenNPC.ForeColor = Color.Gray;
        }
        private void layerColorMathL1_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                this.prioritySets[locationMap.PrioritySet].ColorMathL1 = layerColorMathL1.Checked;
                if (!this.Refreshing)
                    RefreshLocation();
            }
            if (layerColorMathL1.Checked) layerColorMathL1.ForeColor = Color.Black;
            else layerColorMathL1.ForeColor = Color.Gray;
        }
        private void layerColorMathL2_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                this.prioritySets[locationMap.PrioritySet].ColorMathL2 = layerColorMathL2.Checked;
                if (!this.Refreshing)
                    RefreshLocation();
            }
            if (layerColorMathL2.Checked) layerColorMathL2.ForeColor = Color.Black;
            else layerColorMathL2.ForeColor = Color.Gray;
        }
        private void layerColorMathL3_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                this.prioritySets[locationMap.PrioritySet].ColorMathL3 = layerColorMathL3.Checked;
                if (!this.Refreshing)
                    RefreshLocation();
            }
            if (layerColorMathL3.Checked) layerColorMathL3.ForeColor = Color.Black;
            else layerColorMathL3.ForeColor = Color.Gray;
        }
        private void layerColorMathNPC_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                this.prioritySets[locationMap.PrioritySet].ColorMathOBJ = layerColorMathNPC.Checked;
                if (!this.Refreshing)
                    RefreshLocation();
            }
            if (layerColorMathNPC.Checked) layerColorMathNPC.ForeColor = Color.Black;
            else layerColorMathNPC.ForeColor = Color.Gray;
        }
        private void layerColorMathBG_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                this.prioritySets[locationMap.PrioritySet].ColorMathBG = layerColorMathBG.Checked;
                if (!this.Refreshing)
                    RefreshLocation();
            }
            if (layerColorMathBG.Checked) layerColorMathBG.ForeColor = Color.Black;
            else layerColorMathBG.ForeColor = Color.Gray;
        }
        private void layerColorMathIntensity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                if (layerColorMathIntensity.SelectedIndex == 0)
                    this.prioritySets[locationMap.PrioritySet].ColorMathHalfIntensity = 0;//false;
                else if (layerColorMathIntensity.SelectedIndex == 1)
                    this.prioritySets[locationMap.PrioritySet].ColorMathHalfIntensity = 1;//true;
                if (!this.Refreshing)
                    RefreshLocation();
            }
        }
        private void layerColorMathMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                if (layerColorMathMode.SelectedIndex == 0)
                    this.prioritySets[locationMap.PrioritySet].ColorMathMinusSubscreen = 0;//false;
                else if (layerColorMathMode.SelectedIndex == 1)
                    this.prioritySets[locationMap.PrioritySet].ColorMathMinusSubscreen = 1;// true;
                if (!this.Refreshing)
                    RefreshLocation();
            }
        }
        private void layerMaskHighX_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.MaskHighX = (byte)layerMaskHighX.Value;
            this.picture.Invalidate();
        }
        private void layerMaskHighY_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.MaskHighY = (byte)layerMaskHighY.Value;
            this.picture.Invalidate();
        }
        private void layerL2LeftShift_ValueChanged(object sender, EventArgs e)
        {
            locationMap.XNegL2 = (byte)layerL2LeftShift.Value;
        }
        private void layerL3LeftShift_ValueChanged(object sender, EventArgs e)
        {
            locationMap.XNegL3 = (byte)layerL3LeftShift.Value;
        }
        private void layerL2UpShift_ValueChanged(object sender, EventArgs e)
        {
            locationMap.YNegL2 = (byte)layerL2UpShift.Value;
        }
        private void layerL3UpShift_ValueChanged(object sender, EventArgs e)
        {
            locationMap.YNegL3 = (byte)layerL3UpShift.Value;
        }
        private void spriteMask_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.SpriteMask = (byte)spriteMask.Value;
        }
        private void l1width_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.Width[0] = (byte)l1width.SelectedIndex;
            if (!this.Refreshing)
            {
                fullUpdate = true;
                RefreshLocation();
            }
            Size s = LayerSize(0);
            Model.TilemapSizes[locationMap.TilemapL1] = (ushort)((s.Width / 16) * (s.Height / 16));
        }
        private void l2width_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.Width[1] = (byte)l2width.SelectedIndex;
            if (!this.Refreshing)
            {
                fullUpdate = true;
                RefreshLocation();
            }
            Size s = LayerSize(1);
            Model.TilemapSizes[locationMap.TilemapL2] = (ushort)((s.Width / 16) * (s.Height / 16));
        }
        private void l3width_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.Width[2] = (byte)l3width.SelectedIndex;
            if (!this.Refreshing)
            {
                fullUpdate = true;
                RefreshLocation();
            }
            Size s = LayerSize(2);
            Model.TilemapSizes[locationMap.TilemapL3] = (ushort)((s.Width / 16) * (s.Height / 16));
        }
        private void l1height_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.Height[0] = (byte)l1height.SelectedIndex;
            if (!this.Refreshing)
            {
                fullUpdate = true;
                RefreshLocation();
            }
            Size s = LayerSize(0);
            Model.TilemapSizes[locationMap.TilemapL1] = (ushort)((s.Width / 16) * (s.Height / 16));
        }
        private void l2height_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.Height[1] = (byte)l2height.SelectedIndex;
            if (!this.Refreshing)
            {
                fullUpdate = true;
                RefreshLocation();
            }
            Size s = LayerSize(1);
            Model.TilemapSizes[locationMap.TilemapL2] = (ushort)((s.Width / 16) * (s.Height / 16));
        }
        private void l3height_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.Height[2] = (byte)l3height.SelectedIndex;
            if (!this.Refreshing)
            {
                fullUpdate = true;
                RefreshLocation();
            }
            Size s = LayerSize(2);
            Model.TilemapSizes[locationMap.TilemapL3] = (ushort)((s.Width / 16) * (s.Height / 16));
        }
        private void messageName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;

            // madsiur, for location renaming feature [3.18.4-0.1]
            locationMap.MessageBox = (byte)messageName.SelectedIndex;
            tbLocation.Text = messageName.SelectedItem.ToString();
        }
        private void musicName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.Music = (byte)musicName.SelectedIndex;
        }
        private void topSync_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.Scrolling = (byte)topSync.Value;
        }
        private void mapBattleBG_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.BattleBG = (byte)mapBattleBG.Value;
            mapBattleBGName.SelectedIndex = (int)mapBattleBG.Value;
        }
        private void mapBattleBGName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            mapBattleBG.Value = mapBattleBGName.SelectedIndex;
        }
        private void mapBattleZone_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.BattleZone = (byte)mapBattleZone.Value;
        }
        private void mapRandomBattles_CheckedChanged(object sender, EventArgs e)
        {
            mapRandomBattles.ForeColor = mapRandomBattles.Checked ? Color.Black : SystemColors.ControlDark;
            if (this.Updating)
                return;
            locationMap.RandomBattle = mapRandomBattles.Checked;
        }
        private void windowMask_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.WindowMask = (byte)windowMask.SelectedIndex;
        }
        private void layerEffects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.HeatWaveL1 = this.layerEffects.GetItemChecked(0);
            locationMap.HeatWaveL2 = this.layerEffects.GetItemChecked(1);
            locationMap.HeatWaveL3 = this.layerEffects.GetItemChecked(2);
            locationMap.WarpEnabled = this.layerEffects.GetItemChecked(3);
            locationMap.WarpEnabledX = this.layerEffects.GetItemChecked(4);
            locationMap.SearchLights = this.layerEffects.GetItemChecked(5);
        }
        private void layerUnknownBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.B1b6 = layerUnknownBits.GetItemChecked(0);
            locationMap.B6b7 = layerUnknownBits.GetItemChecked(1);
        }
        #endregion
    }
}
