using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace ZONEDOCTOR
{
    public abstract class Tilemap
    {
        public abstract int Width_p { get; set; }
        public abstract int Height_p { get; set; }
        public abstract int[] Pixels { get; }
        public abstract Tile[][] Tilemaps_Tiles { get; set; }
        public abstract byte[][] Tilemaps_Bytes { get; set; }
        public abstract int[] GetPriority1Pixels();
        public abstract int[] GetPixels(int layer, Point p, Size s);
        public abstract int[] GetPixels(Point p, Size s);
        public abstract void SetTileNum(int tileNum, int layer, int x, int y);
        public abstract int GetTileNum(int layer, int x, int y);
        public abstract int GetTileNum(int layer, int x, int y, bool ignoretransparent);
        public abstract void RedrawTilemap();
        public abstract void Assemble();
        public abstract Size Size { get; }
        public abstract Size Size_p { get; }
        public abstract int GetPixelLayer(int x, int y);
    }
}
