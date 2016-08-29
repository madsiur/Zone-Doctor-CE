﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ZONEDOCTOR
{
    public partial class TileEditor : NewForm
    {
        private Delegate update;
        private Tile tile;
        private Tile tileBackup;
        private SolidityTile solid;
        private SolidityTile solidBackup;
        private byte[] graphics;
        private PaletteSet paletteSet;
        private byte format;
        private int currentSubtile;
        private Bitmap tileImage, subtileImage;
        /// <summary>
        /// View and edit the properties of a single 16x16 tile.
        /// </summary>
        /// <param name="update">The update function to invoke when "APPLY" is clicked.</param>
        /// <param name="tile">The 16x16 tile to analyze.</param>
        /// <param name="graphics">The graphics used by the tile.</param>
        /// <param name="paletteSet">The palette set used by the tile.</param>
        /// <param name="format">Either 0x10 or 0x20 for 2bpp or 4bpp format, respectively.</param>
        /// <param name="sender">The control that was double-clicked to open the tile editor.</param>
        public TileEditor(Delegate update, Tile tile, SolidityTile solid, byte[] graphics, PaletteSet paletteSet, byte format)
        {
            this.update = update;
            this.tile = tile;
            this.solid = solid;
            this.tileBackup = tile.Copy();
            this.solidBackup = solid.Copy();
            this.graphics = graphics;
            this.paletteSet = paletteSet;
            this.format = format;
            currentSubtile = 0;
            InitializeComponent();
            InitializeSubtile();
            SetTileImage();
            SetSubtileImage();
            this.BringToFront();
        }
        public void Reload(Delegate update, Tile tile, SolidityTile solid, byte[] graphics, PaletteSet paletteSet, byte format)
        {
            if (this.Refreshing)
                return;
            this.update = update;
            this.tile = tile;
            this.solid = solid;
            this.tileBackup = tile.Copy();
            this.solidBackup = solid.Copy();
            this.graphics = graphics;
            this.paletteSet = paletteSet;
            this.format = format;
            InitializeSubtile();
            SetTileImage();
            SetSubtileImage();
            this.BringToFront();
        }
        private void InitializeSubtile()
        {
            this.Updating = true;
            subtileIndex.Value = tile.Subtiles[currentSubtile].Index;
            subtilePalette.Value = tile.Subtiles[currentSubtile].Palette;
            subtileStatus.SetItemChecked(0, tile.Subtiles[currentSubtile].Priority1);
            subtileStatus.SetItemChecked(1, tile.Subtiles[currentSubtile].Mirror);
            subtileStatus.SetItemChecked(2, tile.Subtiles[currentSubtile].Invert);
            if (solid.WorldMap)
            {
                groupBoxSolidWM.BringToFront();
                physicalAirship.SelectedIndex = solid.AirshipShadow;
                physicalBattleBG.SelectedIndex = solid.Battlefield;
                physicalProperties.SetItemChecked(0, solid.BlockChocobo);
                physicalProperties.SetItemChecked(1, solid.BlockAirship);
                physicalProperties.SetItemChecked(2, solid.Solid);
                physicalProperties.SetItemChecked(3, solid.HideSpriteLegs);
                physicalProperties.SetItemChecked(4, solid.EnableRandomBattle);
                physicalUnknown.SetItemChecked(0, solid.B0b7);
                physicalUnknown.SetItemChecked(1, solid.B1b3);
                physicalUnknown.SetItemChecked(2, solid.B1b4);
                physicalUnknown.SetItemChecked(3, solid.Veldt);
                physicalUnknown.SetItemChecked(4, solid.PhoenixCave);
                physicalUnknown.SetItemChecked(5, solid.KefkaTower);
            }
            else
            {
                groupBoxSolid.BringToFront();
                physicalSolidTile.Checked = solid.Solid;
                physicalStairs.SelectedIndex = solid.Stairs;
                physicalTileProperties.SetItemChecked(0, solid.West);
                physicalTileProperties.SetItemChecked(1, solid.East);
                physicalTileProperties.SetItemChecked(2, solid.North);
                physicalTileProperties.SetItemChecked(3, solid.South);
                physicalTileProperties.SetItemChecked(4, solid.AlwaysFaceUp);
                physicalOtherBits.SetItemChecked(0, solid.SolidTier1);
                physicalOtherBits.SetItemChecked(1, solid.SolidTier2);
                physicalOtherBits.SetItemChecked(2, solid.B0b3);
                physicalOtherBits.SetItemChecked(3, solid.B0b4);
                physicalOtherBits.SetItemChecked(4, solid.Door);
                physicalOtherBits.SetItemChecked(5, solid.B1b4);
                physicalOtherBits.SetItemChecked(6, solid.B1b5);
                physicalOtherBits.SetItemChecked(7, solid.PassableQuadrants);
            }
            this.Updating = false;
        }
        // set images
        private void SetTileImage()
        {
            int[] temp = new int[16 * 16];
            int[] pixels = new int[64 * 64];
            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 2; x++)
                {
                    Do.PixelsToPixels(
                        tile.Subtiles[y * 2 + x].Pixels,
                        temp, 16, new Rectangle(x * 8, y * 8, 8, 8));
                }
            }
            for (int y = 0; y < 64; y++)
            {
                for (int x = 0; x < 64; x++)
                    pixels[y * 64 + x] = temp[y / 4 * 16 + (x / 4)];
            }
            tileImage = Do.PixelsToImage(pixels, 64, 64);
            pictureBoxTile.Invalidate();
        }
        private void SetSubtileImage()
        {
            int[] temp = new int[8 * 8];
            int[] pixels = new int[64 * 64];
            Do.PixelsToPixels(
                tile.Subtiles[currentSubtile].Pixels,
                temp, 8, new Rectangle(0, 0, 8, 8));
            for (int y = 0; y < 64; y++)
            {
                for (int x = 0; x < 64; x++)
                    pixels[y * 64 + x] = temp[y / 8 * 8 + (x / 8)];
            }
            subtileImage = Do.PixelsToImage(pixels, 64, 64);
            pictureBoxSubtile.Invalidate();
        }
        private Subtile CreateNewSubtile()
        {
            if (solid.WorldMap)
                return Do.DrawSubtileM7((ushort)this.subtileIndex.Value,
                    (byte)this.subtilePalette.Value,
                    graphics, paletteSet.Palettes, 0x20);
            else
                return Do.DrawSubtile((ushort)this.subtileIndex.Value,
                    (byte)this.subtilePalette.Value,
                    this.subtileStatus.GetItemChecked(0),
                    this.subtileStatus.GetItemChecked(1),
                    this.subtileStatus.GetItemChecked(2),
                    graphics, paletteSet.Palettes, format);
        }
        #region Event Handlers
        private void TileEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            buttonReset.PerformClick();
        }
        private void subtilePalette_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            if (subtilePalette.Value >= paletteSet.Palettes.Length)
                subtilePalette.Value = paletteSet.Palettes.Length - 1;
            tile.Subtiles[currentSubtile] = CreateNewSubtile();
            SetTileImage();
            SetSubtileImage();
            this.Refreshing = true;
            if (autoUpdate.Checked)
                update.DynamicInvoke();
            this.Refreshing = false;
        }
        private void tileAttributes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            tile.Subtiles[currentSubtile] = CreateNewSubtile();
            SetTileImage();
            SetSubtileImage();
            this.Refreshing = true;
            if (autoUpdate.Checked)
                update.DynamicInvoke();
            this.Refreshing = false;
        }
        private void tile8x8Tile_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            if (subtileIndex.Value * format >= graphics.Length)
                subtileIndex.Value = (graphics.Length / format) - 1;
            tile.Subtiles[currentSubtile] = CreateNewSubtile();
            SetTileImage();
            SetSubtileImage();
            this.Refreshing = true;
            if (autoUpdate.Checked)
                update.DynamicInvoke();
            this.Refreshing = false;
        }
        private void pictureBoxSubtile_Paint(object sender, PaintEventArgs e)
        {
            if (subtileImage != null)
                e.Graphics.DrawImage(subtileImage, 0, 0);
        }
        private void pictureBoxTile_MouseClick(object sender, MouseEventArgs e)
        {
            currentSubtile = e.X / 32 + ((e.Y / 32) * 2);
            InitializeSubtile();
            SetSubtileImage();
        }
        private void pictureBoxTile_Paint(object sender, PaintEventArgs e)
        {
            if (tileImage != null)
                e.Graphics.DrawImage(tileImage, 0, 0);
        }
        private void buttonMirrorTile_Click(object sender, EventArgs e)
        {
            Do.FlipHorizontal(tile);
            InitializeSubtile();
            SetTileImage();
            SetSubtileImage();
            if (autoUpdate.Checked)
                update.DynamicInvoke();
        }
        private void buttonInvertTile_Click(object sender, EventArgs e)
        {
            Do.FlipVertical(tile);
            InitializeSubtile();
            SetTileImage();
            SetSubtileImage();
            if (autoUpdate.Checked)
                update.DynamicInvoke();
        }
        //solid tile
        private void physicalSolidTile_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solid.Solid = physicalSolidTile.Checked;
        }
        private void physicalStairs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solid.Stairs = physicalStairs.SelectedIndex;
        }
        private void physicalTileProperties_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solid.West = physicalTileProperties.GetItemChecked(0);
            solid.East = physicalTileProperties.GetItemChecked(1);
            solid.North = physicalTileProperties.GetItemChecked(2);
            solid.South = physicalTileProperties.GetItemChecked(3);
            solid.AlwaysFaceUp = physicalTileProperties.GetItemChecked(4);
        }
        private void physicalOtherBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solid.SolidTier1 = physicalOtherBits.GetItemChecked(0);
            solid.SolidTier2 = physicalOtherBits.GetItemChecked(1);
            solid.B0b3 = physicalOtherBits.GetItemChecked(2);
            solid.B0b4 = physicalOtherBits.GetItemChecked(3);
            solid.Door = physicalOtherBits.GetItemChecked(4);
            solid.B1b4 = physicalOtherBits.GetItemChecked(5);
            solid.B1b5 = physicalOtherBits.GetItemChecked(6);
            solid.PassableQuadrants = physicalOtherBits.GetItemChecked(7);
        }
        private void physicalAirship_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solid.AirshipShadow = physicalAirship.SelectedIndex;
        }
        private void physicalBattleBG_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solid.Battlefield = physicalBattleBG.SelectedIndex;
        }
        private void physicalProperties_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solid.BlockChocobo = physicalProperties.GetItemChecked(0);
            solid.BlockAirship = physicalProperties.GetItemChecked(1);
            solid.Solid = physicalProperties.GetItemChecked(2);
            solid.HideSpriteLegs = physicalProperties.GetItemChecked(3);
            solid.EnableRandomBattle = physicalProperties.GetItemChecked(4);
        }
        private void physicalUnknown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solid.B0b7 = physicalUnknown.GetItemChecked(0);
            solid.B1b3 = physicalUnknown.GetItemChecked(1);
            solid.B1b4 = physicalUnknown.GetItemChecked(2);
            solid.Veldt = physicalUnknown.GetItemChecked(3);
            solid.PhoenixCave = physicalUnknown.GetItemChecked(4);
            solid.KefkaTower = physicalUnknown.GetItemChecked(5);
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            update.DynamicInvoke();
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
                this.tileBackup.Subtiles[i] = this.tile.Subtiles[i];
            this.Close();
            if (!autoUpdate.Checked)
                update.DynamicInvoke();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void buttonReset_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
                this.tile.Subtiles[i] = this.tileBackup.Subtiles[i];
            this.Refreshing = true;
            if (autoUpdate.Checked)
                update.DynamicInvoke();
            this.Refreshing = false;
            InitializeSubtile();
            SetTileImage();
            SetSubtileImage();
        }
        #endregion
    }
}
