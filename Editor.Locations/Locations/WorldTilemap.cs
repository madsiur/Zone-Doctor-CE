using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ZONEDOCTOR
{
    public class WorldTilemap : Tilemap
    {
        #region Variables
        private Location location;
        private Tileset tileset;
        private State state = State.Instance;
        private BackgroundWorker bgw;
        private int bgw_progress = 0;
        private int Width = 256;
        private int Height = 256;
        private byte[][] tilemaps_Bytes = new byte[3][];
        private Tile[] tilemap_Tiles;
        private int[] pixels;
        // accessors
        public override Size Size { get { return new Size(Width, Height); } }
        public override Size Size_p { get { return new Size(Width_p, Height_p); } }
        public override int Width_p { get { return Width * 16; } set { Width = value / 16; } }
        public override int Height_p { get { return Height * 16; } set { Height = value / 16; } }
        public override byte[][] Tilemaps_Bytes { get { return tilemaps_Bytes; } set { tilemaps_Bytes = value; } }
        public override Tile[][] Tilemaps_Tiles
        {
            get { return new Tile[][] { tilemap_Tiles, null, null }; }
            set { tilemap_Tiles = value[0]; }
        }
        public override int[] Pixels { get { return pixels; } }
        #endregion
        // constructor
        public WorldTilemap(Location location, Tileset tileset, BackgroundWorker bgw)
        {
            this.location = location;
            this.tileset = tileset;
            this.bgw = bgw;
            switch (location.Index)
            {
                case 0:
                    tilemaps_Bytes[0] = Model.WOBTilemap;
                    Width = 256; Height = 256; break;
                case 1:
                    tilemaps_Bytes[0] = Model.WORTilemap;
                    Width = 256; Height = 256; break;
                case 2:
                    tilemaps_Bytes[0] = Model.STTilemap;
                    Width = 128; Height = 128; break;
            }
            pixels = new int[Width_p * Height_p];
            CreateLayer();
            DrawLayer(pixels);
            if (bgw != null && bgw.WorkerReportsProgress)
                bgw.ReportProgress(bgw_progress += 256 / Height, "DRAWING LOCATION IMAGE");
            bgw_progress = 0;
        }
        // assemblers
        public override void Assemble()
        {
            for (int i = 0; i < tilemap_Tiles.Length; i++)
                tilemaps_Bytes[0][i] = (byte)tilemap_Tiles[i].Index;
        }
        // drawing
        private void ChangeSingleTile(int placement, int tile, int x, int y)
        {
            tilemap_Tiles[placement] = tileset.Tilesets_tiles[0][tile]; // Change the tile in the layer map
            Tile source = tilemap_Tiles[placement]; // Grab the new tile
            Do.PixelsToPixels(source.Subtiles[0].Pixels, pixels, Width_p, new Rectangle(x, y, 8, 8));
            Do.PixelsToPixels(source.Subtiles[1].Pixels, pixels, Width_p, new Rectangle((x + 8), y, 8, 8));
            Do.PixelsToPixels(source.Subtiles[2].Pixels, pixels, Width_p, new Rectangle(x, (y + 8), 8, 8));
            Do.PixelsToPixels(source.Subtiles[3].Pixels, pixels, Width_p, new Rectangle((x + 8), (y + 8), 8, 8));
            DrawSingleMainscreenTile(x, y);
        }
        private void CopySingleTileToArray(int[] dst, int[] src, int width, int x, int y)
        {
            int counter = 0;
            for (int i = 0; i < 256; i++)
            {
                if (src[i] != 0)
                    dst[y * width + x + counter] = src[i];
                counter++;
                if (counter % 16 == 0)
                {
                    y++;
                    counter = 0;
                }
            }
        }
        private void Clear()
        {
            Model.WOBTilemap = new byte[Model.WOBTilemap.Length];
            Model.WORTilemap = new byte[Model.WORTilemap.Length];
            Model.STTilemap = new byte[Model.STTilemap.Length];
            RedrawTilemap();
        }
        private void ClearSingleTile(int[] pixels, int x, int y)
        {
            int counter = 0;
            for (int i = 0; i < 256; i++)
            {
                pixels[y * Width_p + x + counter] = 0;
                counter++;
                if (counter % 16 == 0)
                {
                    y++;
                    counter = 0;
                }
            }
        }
        private void CreateLayer()
        {
            tilemap_Tiles = new Tile[Width * Height]; // Create our layer here
            int offset = 0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int i = y * Width + x;
                    byte tileNum = tilemaps_Bytes[0][offset++];
                    tilemap_Tiles[i] = tileset.Tilesets_tiles[0][tileNum];
                }
                if (bgw != null && bgw.WorkerReportsProgress)
                    bgw.ReportProgress(bgw_progress += 256 / Height, "DRAWING TILE MAP: layer tiles");
            }
        }
        private void DrawLayer(int[] dst)
        {
            if (dst.Length != pixels.Length || tilemap_Tiles == null)
                return;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int i = y * Width + x;
                    for (int z = 0; z < 4; z++)
                    {
                        Point location = new Point(x * 16, y * 16);
                        location.X += (z % 2) * 8;
                        location.Y += (z / 2) * 8;
                        Size size = new Size(8, 8);
                        Do.PixelsToPixels(tilemap_Tiles[i].Subtiles[z].Pixels, dst, Width_p, new Rectangle(location, size));
                    }
                }
                if (bgw != null && bgw.WorkerReportsProgress)
                    bgw.ReportProgress(bgw_progress += 256 / Height, "DRAWING TILE MAP: mainscreen pixels");
            }
        }
        private void DrawSingleMainscreenTile(int x, int y)
        {
            if (state.Layer1)
                CopySingleTileToArray(pixels, Do.GetPixelRegion(pixels, Width_p, Height_p, 16, 16, x, y), Width_p, x, y);
        }
        public override void RedrawTilemap()
        {
            Array.Clear(pixels, 0, pixels.Length);
            CreateLayer();
            DrawLayer(pixels);
            bgw_progress = 0;
        }
        // accessor functions
        public override int GetTileNum(int layer, int x, int y, bool ignoretransparent)
        {
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (x >= Width_p) x = Width_p - 1;
            if (y >= Height_p) y = Height_p - 1;
            Point p = new Point(x % 16, y % 16);
            y /= 16;
            x /= 16;
            int placement = y * Width + x;
            if (layer < 3 && tilemap_Tiles != null)
            {
                if (!ignoretransparent)
                    return tilemap_Tiles[placement].Index;
                else if (tilemap_Tiles[placement].Pixels[p.Y * 16 + p.X] != 0)
                    return tilemap_Tiles[placement].Index;
                else
                    return 0;
            }
            else
                return 0;
        }
        public override int GetTileNum(int layer, int x, int y)
        {
            Math.Min(Width_p - 1, Math.Max(0, x));
            Math.Min(Height_p - 1, Math.Max(0, y));
            y /= 16;
            x /= 16;
            int placement = y * Width + x;
            if (this.tilemap_Tiles != null)
                return this.tilemap_Tiles[placement].Index;
            else return 0;
        }
        public override int[] GetPixels(int layer, Point p, Size s)
        {
            return GetPixels(p, s);
        }
        public override int[] GetPixels(Point p, Size s)
        {
            int[] pixels = new int[s.Width * s.Height];
            for (int b = 0, y = p.Y; b < s.Height; b++, y++)
            {
                for (int a = 0, x = p.X; a < s.Width; a++, x++)
                {
                    pixels[b * s.Width + a] = this.pixels[y * Width_p + x];
                    if (this.pixels[y * Width_p + x] != 0)
                        pixels[b * s.Width + a] = this.pixels[y * Width_p + x];
                }
            }
            return pixels;
        }
        public override int GetPixelLayer(int x, int y)
        {
            return 0;
        }
        public override int[] GetPriority1Pixels()
        {
            return new int[Width_p * Height_p];
        }
        public override void SetTileNum(int tilenum, int layer, int x, int y)
        {
            // x and y are in pixel format
            Math.Min(Width_p - 1, Math.Max(0, x));
            Math.Min(Height_p - 1, Math.Max(0, y));
            y /= 16;
            x /= 16;
            int tile = y * Width + x;
            if (location.Index < 2)
            {
                if (x >= 0 && y >= 0 && tile < 0x10000)
                    ChangeSingleTile(tile, tilenum, x * 16, y * 16);
            }
            else
            {
                if (x >= 0 && y >= 0 && tile < 0x4000)
                    ChangeSingleTile(tile, tilenum, x * 16, y * 16);
            }
            switch (location.Index)
            {
                case 0: Model.EditWOBTilemap = true; break;
                case 1: Model.EditWORTilemap = true; break;
                case 2: Model.EditSTTilemap = true; break;
            }
        }
    }
}
