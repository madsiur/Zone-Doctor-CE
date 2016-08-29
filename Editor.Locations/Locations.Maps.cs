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
        private PaletteSet paletteSet
        {
            get
            {
                if (index == 0) return paletteSets[48];
                else if (index == 1) return paletteSets[49];
                else if (index == 2) return paletteSets[50];
                else
                    return paletteSets[locationMap.PaletteSet];
            }
            set
            {
                if (index == 0) paletteSets[48] = value;
                else if (index == 1) paletteSets[49] = value;
                else if (index == 2) paletteSets[50] = value;
                else
                    paletteSets[locationMap.PaletteSet] = value;
            }
        }
        private Tilemap tilemap; public Tilemap Tilemap { get { return tilemap; } }
        private Tileset tileset; public Tileset Tileset { get { return tileset; } }
        private SoliditySet soliditySet; public SoliditySet SoliditySet { get { return soliditySet; } }
        private LocationMap locationMap { get { return location.LocationMap; } set { location.LocationMap = value; } }
        public LocationMap LocationMap { get { return locationMap; } }// Layer for the current location
        #endregion
        #region Methods
        private void InitializeMapProperties()
        {
            this.Updating = true;
            this.mapGFXSet1Num.Value = locationMap.GraphicSetA;
            this.mapGFXSet2Num.Value = locationMap.GraphicSetB;
            this.mapGFXSet3Num.Value = locationMap.GraphicSetC;
            this.mapGFXSet4Num.Value = locationMap.GraphicSetD;
            this.mapGFXSetL3Num.Value = locationMap.GraphicSetL3;
            this.animationL2.Value = locationMap.AnimationL2;
            this.animationL3.Value = locationMap.AnimationL3;
            this.animationBG.Value = locationMap.AnimationBG;
            this.mapTilemapL1Num.Value = locationMap.TilemapL1;
            this.mapTilemapL2Num.Value = locationMap.TilemapL2;
            this.mapTilemapL3Num.Value = locationMap.TilemapL3;
            this.mapTilesetL1Num.Value = locationMap.TilesetL1;
            this.mapTilesetL2Num.Value = locationMap.TilesetL2;
            this.mapPhysicalMapNum.Value = locationMap.SoliditySet;
            this.useWorldMapBG.Checked = locationMap.WorldMapBG;
            this.mapPaletteSetNum.Value = locationMap.PaletteSet;
            this.mapBattleBG.Value = locationMap.BattleBG;
            this.mapBattleBGName.SelectedIndex = locationMap.BattleBG;
            this.mapBattleZone.Value = locationMap.BattleZone;
            this.mapRandomBattles.Checked = locationMap.RandomBattle;
            this.Updating = false;
        }
        // set images
        private Image SetPaletteOverlay(Size s, Size u, int index)  // s = palette dimen, u = color dimen
        {
            Point p = new Point();
            int colspan = s.Width / u.Width;
            int color;
            p.X = index % colspan * u.Width;
            p.Y = index / colspan * u.Height;
            int[] pixels = new int[s.Width * s.Height];
            for (int x = p.X; x < p.X + u.Width - 1; x++)
            {
                color = x % 2 == 0 ? Color.White.ToArgb() : Color.Black.ToArgb();
                pixels[p.Y * s.Width + x] = color;
                pixels[(p.Y + u.Height - 2) * s.Width + x] = color;
                color = x % 2 == 0 ? Color.Black.ToArgb() : Color.White.ToArgb();
                pixels[(p.Y + 1) * s.Width + x] = color;
                pixels[(p.Y + u.Height - 3) * s.Width + x] = color;
            }
            for (int y = p.Y; y < p.Y + u.Height - 1; y++)
            {
                color = y % 2 == 0 ? Color.White.ToArgb() : Color.Black.ToArgb();
                pixels[y * s.Width + p.X] = color;
                pixels[y * s.Width + u.Width - 2 + p.X] = color;
                color = y % 2 == 0 ? Color.Black.ToArgb() : Color.White.ToArgb();
                pixels[y * s.Width + 1 + p.X] = color;
                pixels[y * s.Width + u.Width - 3 + p.X] = color;
            }
            return Do.PixelsToImage(pixels, s.Width, s.Height);
        }
        #endregion
        #region Event Handlers
        private void mapGFXSet1Num_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.GraphicSetA = (byte)this.mapGFXSet1Num.Value;
            if (!this.Refreshing)
            {
                fullUpdate = true;
                RefreshLocation();
            }
        }
        private void mapGFXSet2Num_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.GraphicSetB = (byte)this.mapGFXSet2Num.Value;
            if (!this.Refreshing)
            {
                fullUpdate = true;
                RefreshLocation();
            }
        }
        private void mapGFXSet3Num_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.GraphicSetC = (byte)this.mapGFXSet3Num.Value;
            if (!this.Refreshing)
            {
                fullUpdate = true;
                RefreshLocation();
            }
        }
        private void mapGFXSet4Num_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.GraphicSetD = (byte)this.mapGFXSet4Num.Value;
            if (!this.Refreshing)
            {
                fullUpdate = true;
                RefreshLocation();
            }
        }
        private void mapGFXSetL3Num_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            if (this.mapGFXSetL3Num.Value > 0x1b)
            {
                locationMap.GraphicSetL3 = (byte)0xff;
                this.mapTilemapL3Num.Enabled = false;
                if (tilesetEditor.Layer == 2)
                    tilesetEditor.Layer = 0;
            }
            else
            {
                locationMap.GraphicSetL3 = (byte)this.mapGFXSetL3Num.Value;
                this.mapTilemapL3Num.Enabled = true;
            }
            if (!this.Refreshing)
            {
                fullUpdate = true;
                RefreshLocation();
            }
        }
        private void mapTilesetL1Num_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.TilesetL1 = (byte)this.mapTilesetL1Num.Value;
            if (!this.Refreshing)
            {
                fullUpdate = true;
                RefreshLocation();
            }
        }
        private void mapTilesetL2Num_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.TilesetL2 = (byte)this.mapTilesetL2Num.Value;
            if (!this.Refreshing)
            {
                fullUpdate = true;
                RefreshLocation();
            }
        }
        private void mapTilemapL1Num_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.TilemapL1 = (ushort)this.mapTilemapL1Num.Value;
            if (!this.Refreshing)
            {
                fullUpdate = true;
                RefreshLocation();
            }
        }
        private void mapTilemapL2Num_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.TilemapL2 = (ushort)this.mapTilemapL2Num.Value;
            if (!this.Refreshing)
            {
                fullUpdate = true;
                RefreshLocation();
            }
        }
        private void mapTilemapL3Num_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.TilemapL3 = (ushort)this.mapTilemapL3Num.Value;
            if (!this.Refreshing)
            {
                fullUpdate = true;
                RefreshLocation();
            }
        }
        private void mapBattlefieldNum_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.BattleBG = (byte)this.mapBattleBG.Value;
        }
        private void mapPhysicalMapNum_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.SoliditySet = (ushort)this.mapPhysicalMapNum.Value;
            if (!this.Refreshing)
            {
                fullUpdate = true;
                soliditySet = new SoliditySet(locationMap, false);
                LoadTilemapEditor();
            }
        }
        private void mapSetL3Priority_CheckedChanged(object sender, EventArgs e)
        {
            mapSetL3Priority.ForeColor = mapSetL3Priority.Checked ? Color.Black : Color.Gray;
            locationMap.TopPriorityL3 = mapSetL3Priority.Checked;
            if (!this.Refreshing)
            {
                fullUpdate = true;
                RefreshLocation();
            }
        }
        private void mapPaletteSetNum_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.PaletteSet = (byte)this.mapPaletteSetNum.Value;
            if (!this.Refreshing)
            {
                fullUpdate = true;
                RefreshLocation();
            }
        }
        private void animationL2_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.AnimationL2 = (byte)animationL2.Value;
            if (!this.Refreshing)
            {
                fullUpdate = true;
                RefreshLocation();
            }
        }
        private void animationL3_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.AnimationL3 = (byte)animationL3.Value;
        }
        private void animationBG_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            locationMap.AnimationBG = (byte)animationBG.Value;
        }
        private void useWorldMapBG_CheckedChanged(object sender, EventArgs e)
        {
            useWorldMapBG.ForeColor = useWorldMapBG.Checked ? Color.Black : SystemColors.ControlDark;
            if (this.Updating)
                return;
            locationMap.WorldMapBG = useWorldMapBG.Checked;
        }
        #endregion
    }
}
