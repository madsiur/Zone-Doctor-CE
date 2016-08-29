using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ZONEDOCTOR
{
    public class LocationTilemap : Tilemap
    {
        #region Variables
        private Tileset tileset;
        private PaletteSet paletteSet;
        private State state = State.Instance;
        private TilemapType type = TilemapType.None;
        private BackgroundWorker bgw;
        private int bgw_progress = 0;
        public int Width = 128;
        public int Height = 64;
        private byte[][] tilemaps_Bytes = new byte[3][];
        private Tile[][] tilemaps_Tiles = new Tile[3][];
        private int[] pixels = new int[2048 * 1024];
        private int[] subscreen = null;
        private int[] colorMath = null;
        private int[] tile = new int[256];
        private int[] tileColorMath = new int[256];
        private int[]
            L1Priority0 = new int[2048 * 1024],
            L1Priority1 = new int[2048 * 1024],
            L2Priority0 = new int[2048 * 1024],
            L2Priority1 = new int[2048 * 1024],
            L3Priority0 = new int[2048 * 1024],
            L3Priority1 = new int[2048 * 1024];
        // location variables
        private LocationMap locationMap;
        private PrioritySet prioritySet
        {
            get { return prioritySets[locationMap.PrioritySet]; }
            set { prioritySets[locationMap.PrioritySet] = value; }
        }
        private PrioritySet[] prioritySets;
        // accessors
        public override Size Size { get { return new Size(128, 64); } }
        public override Size Size_p { get { return locationMap.MaximumSize(); } }
        public override int Width_p { get { return 2048; } set { ; } }
        public override int Height_p { get { return 1024; } set { ; } }
        public override byte[][] Tilemaps_Bytes { get { return tilemaps_Bytes; } set { tilemaps_Bytes = value; } }
        public override Tile[][] Tilemaps_Tiles { get { return tilemaps_Tiles; } set { tilemaps_Tiles = value; } }
        public override int[] Pixels { get { return pixels; } }
        #endregion
        // constructors
        public LocationTilemap(Location location, Tileset tileset, BackgroundWorker bgw)
        {
            this.tileset = tileset;
            this.bgw = bgw;
            this.locationMap = location.LocationMap;
            this.paletteSet = Model.PaletteSets[locationMap.PaletteSet];
            this.prioritySets = Model.PrioritySets;
            this.type = TilemapType.Location;
            tilemaps_Bytes[0] = Model.Tilemaps[locationMap.TilemapL1];
            tilemaps_Bytes[1] = Model.Tilemaps[locationMap.TilemapL2];
            tilemaps_Bytes[2] = Model.Tilemaps[locationMap.TilemapL3];
            for (int i = 0; i < 3; i++)
                CreateLayer(i); // Create any required layers
            DrawAllLayers();
            if ((prioritySets[locationMap.PrioritySet].SubscreenL1 && state.Layer1) ||
                (prioritySets[locationMap.PrioritySet].SubscreenL2 && state.Layer2) ||
                (prioritySets[locationMap.PrioritySet].SubscreenL3 && state.Layer3) ||
                (prioritySets[locationMap.PrioritySet].SubscreenOBJ && state.NPCs))
            {
                if (subscreen == null)
                    subscreen = new int[2048 * 1024];
                CreateSubscreen(); // Create the subscreen if needed
            }
            CreateMainscreen();
            //
            if (bgw != null && bgw.WorkerReportsProgress)
                bgw.ReportProgress(bgw_progress += ReportProgressIncrement(), "DRAWING LOCATION IMAGE");
            bgw_progress = 0;
        }
        public LocationTilemap(Location location, Tileset tileset, LocationTemplate template)
        {
            this.tileset = tileset;
            this.locationMap = location.LocationMap;
            this.paletteSet = Model.PaletteSets[locationMap.PaletteSet];
            this.prioritySets = Model.PrioritySets;
            this.type = TilemapType.Template;
            tilemaps_Bytes[0] = new byte[0x2000];
            tilemaps_Bytes[1] = new byte[0x2000];
            tilemaps_Bytes[2] = new byte[0x1000];
            for (int y = 0; y < template.Size.Height / 16; y++)
            {
                for (int x = 0; x < template.Size.Width / 16; x++)
                {
                    Bits.SetShort(tilemaps_Bytes[0], (y * 128 + x) * 2,
                        Bits.GetShort(template.Tilemaps[0], (y * (template.Size.Width / 16) + x) * 2));
                    Bits.SetShort(tilemaps_Bytes[1], (y * 128 + x) * 2,
                        Bits.GetShort(template.Tilemaps[1], (y * (template.Size.Width / 16) + x) * 2));
                    tilemaps_Bytes[2][y * 128 + x] = template.Tilemaps[2][y * (template.Size.Width / 16) + x];
                }
            }
            for (int i = 0; i < 3; i++)
                CreateLayer(i); // Create any required layers
            DrawAllLayers();
            if ((prioritySets[locationMap.PrioritySet].SubscreenL1 && state.Layer1) ||
                (prioritySets[locationMap.PrioritySet].SubscreenL2 && state.Layer2) ||
                (prioritySets[locationMap.PrioritySet].SubscreenL3 && state.Layer3) ||
                (prioritySets[locationMap.PrioritySet].SubscreenOBJ && state.NPCs))
            {
                if (subscreen == null)
                    subscreen = new int[2048 * 1024];
                CreateSubscreen(); // Create the subscreen if needed
            }
            CreateMainscreen();
        }
        #region Functions
        // assemblers
        public override void Assemble()
        {
            int i = 0;
            Size s = new Size();
            for (int l = 0; l < 3; l++)
            {
                if (tilemaps_Tiles[l] == null) continue;
                s.Width = (int)(256 * Math.Pow(2, this.locationMap.Width[l]) / 16);
                s.Height = (int)(256 * Math.Pow(2, this.locationMap.Height[l]) / 16);
                for (int y = 0; y < s.Height; y++)
                {
                    for (int x = 0; x < s.Width; x++)
                    {
                        i = y * s.Width + x;
                        tilemaps_Bytes[l][i] = (byte)tilemaps_Tiles[l][i].Index;
                        if (l == 2)
                        {
                            Bits.SetBit(tilemaps_Bytes[l], i, 6, tilemaps_Tiles[l][i].Mirror);
                            Bits.SetBit(tilemaps_Bytes[l], i, 7, tilemaps_Tiles[l][i].Invert);
                        }
                    }
                }
            }
        }
        // class functions
        private void ChangeSingleTile(int layer, int placement, int tile, int x, int y)
        {
            tilemaps_Tiles[layer][placement] = tileset.Tilesets_tiles[layer][tile]; // Change the tile in the layer map
            Tile source = tilemaps_Tiles[layer][placement]; // Grab the new tile
            int[] layerA = null, layerB = null; // Just used to save space
            if (layer == 0)
            {
                layerA = L1Priority0;
                layerB = L1Priority1;
            }
            else if (layer == 1)
            {
                layerA = L2Priority0;
                layerB = L2Priority1;
            }
            else if (layer == 2)
            {
                layerA = L3Priority0;
                layerB = L3Priority1;
            }
            ClearSingleTile(layerA, x, y);
            ClearSingleTile(layerB, x, y);
            // Draw all 4 subtiles to the appropriate array based on priority
            if (!source.Subtiles[0].Priority1) // tile 0
                Do.PixelsToPixels(source.Subtiles[0].Pixels, layerA, 2048, new Rectangle(x, y, 8, 8), true);
            else
                Do.PixelsToPixels(source.Subtiles[0].Pixels, layerB, 2048, new Rectangle(x, y, 8, 8), true);
            if (!source.Subtiles[1].Priority1) // tile 1
                Do.PixelsToPixels(source.Subtiles[1].Pixels, layerA, 2048, new Rectangle((x + 8), y, 8, 8), true);
            else
                Do.PixelsToPixels(source.Subtiles[1].Pixels, layerB, 2048, new Rectangle((x + 8), y, 8, 8), true);
            if (!source.Subtiles[2].Priority1) // tile 2
                Do.PixelsToPixels(source.Subtiles[2].Pixels, layerA, 2048, new Rectangle(x, (y + 8), 8, 8), true);
            else
                Do.PixelsToPixels(source.Subtiles[2].Pixels, layerB, 2048, new Rectangle(x, (y + 8), 8, 8), true);
            if (!source.Subtiles[3].Priority1) // tile 3
                Do.PixelsToPixels(source.Subtiles[3].Pixels, layerA, 2048, new Rectangle((x + 8), (y + 8), 8, 8), true);
            else
                Do.PixelsToPixels(source.Subtiles[3].Pixels, layerB, 2048, new Rectangle((x + 8), (y + 8), 8, 8), true);
            // If we have a subscreen, draw the new tile to it
            if ((prioritySets[this.locationMap.PrioritySet].SubscreenL1 && state.Layer1) ||
                (prioritySets[this.locationMap.PrioritySet].SubscreenL2 && state.Layer2) ||
                (prioritySets[this.locationMap.PrioritySet].SubscreenL3 && state.Layer3) ||
                (prioritySets[this.locationMap.PrioritySet].SubscreenOBJ && state.NPCs))
            {
                ClearSingleTile(subscreen, x, y);
                DrawSingleSubscreenTile(x, y);
            }
            ClearSingleTile(pixels, x, y);
            DrawSingleMainscreenTile(x, y);
        }
        private void ClearSingleTile(int[] src, int x, int y)
        {
            int counter = 0;
            for (int i = 0; i < 256; i++)
            {
                src[y * 2048 + x + counter] = 0;
                counter++;
                if (counter % 16 == 0)
                {
                    y++;
                    counter = 0;
                }
            }
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
        private void CopyToPixelArray(int[] dst, int[] src)
        {
            Size s = locationMap.MaximumSize();
            for (int y = 0; y < s.Height; y++)
            {
                for (int x = 0; x < s.Width; x++)
                {
                    if (src[y * 2048 + x] != 0)
                        dst[y * 2048 + x] = src[y * 2048 + x];
                }
            }
            if (bgw != null && bgw.WorkerReportsProgress)
                bgw.ReportProgress(bgw_progress += ReportProgressIncrement(), "DRAWING TILE MAP: mainscreen pixels");
        }
        private void CreateLayer(int layer)
        {
            if (tilemaps_Bytes[layer] == null)
                return;
            if (tileset.Tilesets_tiles[layer] == null)
                return;
            int offset = 0;
            byte tileNum;
            tilemaps_Tiles[layer] = new Tile[128 * 128]; // Create our layer here
            Size s = new Size();
            s.Width = (int)(256 * Math.Pow(2, this.locationMap.Width[layer]) / 16);
            s.Height = (int)(256 * Math.Pow(2, this.locationMap.Height[layer]) / 16);
            int temp = 0;
            int i = 0;
            for (int y = 0; y < s.Height; y++)
            {
                for (int x = 0; x < s.Width; x++)
                {
                    i = y * s.Width + x;
                    tileNum = tilemaps_Bytes[layer][offset++];
                    if (layer == 2)
                    {
                        temp = tileNum;
                        tileNum &= 0x3F;
                        tilemaps_Tiles[layer][i] = new Tile(tileNum);
                        for (int a = 0; a < 4; a++)
                            tilemaps_Tiles[layer][i].Subtiles[a] = tileset.Tilesets_tiles[layer][tileNum].Subtiles[a];
                        tilemaps_Tiles[layer][i].Mirror = (temp & 0x40) == 0x40;
                        tilemaps_Tiles[layer][i].Invert = (temp & 0x80) == 0x80;
                    }
                    else
                        tilemaps_Tiles[layer][i] = tileset.Tilesets_tiles[layer][tileNum];
                }
            }
        }
        private void CreateMainscreen()
        {
            if (HaveSubscreen()) // We are doing color math by the layer
            {
                if (colorMath == null)
                    colorMath = new int[2048 * 1024];
                else
                    Array.Clear(colorMath, 0, colorMath.Length);
                if (prioritySets[locationMap.PrioritySet].ColorMathBG && state.BG)
                {
                    DoColorMath(colorMath);
                    CopyToPixelArray(pixels, colorMath);
                    Array.Clear(colorMath, 0, colorMath.Length);
                }
                if (locationMap.TopPriorityL3) // [3,0][2,0][1,0][2,1][1,1][3,1]
                {
                    if (prioritySets[locationMap.PrioritySet].MainscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0)
                    {
                        CopyToPixelArray(colorMath, L2Priority0);
                        if (prioritySets[locationMap.PrioritySet].ColorMathL2)
                            DoColorMath(colorMath);
                        CopyToPixelArray(pixels, colorMath);
                        Array.Clear(colorMath, 0, colorMath.Length);
                    }
                    if (prioritySets[locationMap.PrioritySet].MainscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0)
                    {
                        CopyToPixelArray(colorMath, L1Priority0);
                        if (prioritySets[locationMap.PrioritySet].ColorMathL1)
                            DoColorMath(colorMath);
                        CopyToPixelArray(pixels, colorMath);
                        Array.Clear(colorMath, 0, colorMath.Length);
                    }
                    if (prioritySets[locationMap.PrioritySet].MainscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0 && state.Priority1)
                    {
                        CopyToPixelArray(colorMath, L2Priority1);
                        if (prioritySets[locationMap.PrioritySet].ColorMathL2)
                            DoColorMath(colorMath);
                        CopyToPixelArray(pixels, colorMath);
                        Array.Clear(colorMath, 0, colorMath.Length);
                    }
                    if (prioritySets[locationMap.PrioritySet].MainscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0 && state.Priority1)
                    {
                        CopyToPixelArray(colorMath, L1Priority1);
                        if (prioritySets[locationMap.PrioritySet].ColorMathL1)
                            DoColorMath(colorMath);
                        CopyToPixelArray(pixels, colorMath);
                        Array.Clear(colorMath, 0, colorMath.Length);
                    }
                    if (prioritySets[locationMap.PrioritySet].MainscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0)
                    {
                        CopyToPixelArray(colorMath, L3Priority0);
                        if (prioritySets[locationMap.PrioritySet].ColorMathL3)
                            DoColorMath(colorMath);
                        CopyToPixelArray(pixels, colorMath);
                        Array.Clear(colorMath, 0, colorMath.Length);
                    }
                    if (prioritySets[locationMap.PrioritySet].MainscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0 && state.Priority1)
                    {
                        CopyToPixelArray(colorMath, L3Priority1);
                        if (prioritySets[locationMap.PrioritySet].ColorMathL3)
                            DoColorMath(colorMath);
                        CopyToPixelArray(pixels, colorMath);
                        Array.Clear(colorMath, 0, colorMath.Length);
                    }
                }
                else if (!locationMap.TopPriorityL3) // [3,0][3,1][2,0][1,0][2,1][1,1]
                {
                    if (prioritySets[locationMap.PrioritySet].MainscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0)
                    {
                        CopyToPixelArray(colorMath, L3Priority0);
                        if (prioritySets[locationMap.PrioritySet].ColorMathL3)
                            DoColorMath(colorMath);
                        CopyToPixelArray(pixels, colorMath);
                        Array.Clear(colorMath, 0, colorMath.Length);
                    }
                    if (prioritySets[locationMap.PrioritySet].MainscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0 && state.Priority1)
                    {
                        CopyToPixelArray(colorMath, L3Priority1);
                        if (prioritySets[locationMap.PrioritySet].ColorMathL3)
                            DoColorMath(colorMath);
                        CopyToPixelArray(pixels, colorMath);
                        Array.Clear(colorMath, 0, colorMath.Length);
                    }
                    if (prioritySets[locationMap.PrioritySet].MainscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0)
                    {
                        CopyToPixelArray(colorMath, L2Priority0);
                        if (prioritySets[locationMap.PrioritySet].ColorMathL2)
                            DoColorMath(colorMath);
                        CopyToPixelArray(pixels, colorMath);
                        Array.Clear(colorMath, 0, colorMath.Length);
                    }
                    if (prioritySets[locationMap.PrioritySet].MainscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0)
                    {
                        CopyToPixelArray(colorMath, L1Priority0);
                        if (prioritySets[locationMap.PrioritySet].ColorMathL1)
                            DoColorMath(colorMath);
                        CopyToPixelArray(pixels, colorMath);
                        Array.Clear(colorMath, 0, colorMath.Length);
                    }
                    if (prioritySets[locationMap.PrioritySet].MainscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0 && state.Priority1)
                    {
                        CopyToPixelArray(colorMath, L2Priority1);
                        if (prioritySets[locationMap.PrioritySet].ColorMathL2)
                            DoColorMath(colorMath);
                        CopyToPixelArray(pixels, colorMath);
                        Array.Clear(colorMath, 0, colorMath.Length);
                    }
                    if (prioritySets[locationMap.PrioritySet].MainscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0 && state.Priority1)
                    {
                        CopyToPixelArray(colorMath, L1Priority1);
                        if (prioritySets[locationMap.PrioritySet].ColorMathL1)
                            DoColorMath(colorMath);
                        CopyToPixelArray(pixels, colorMath);
                        Array.Clear(colorMath, 0, colorMath.Length);
                    }
                }
            }
            else // No color math, we can go ahead and draw the mainscreen
            {
                if (locationMap.TopPriorityL3) // [3,0][2,0][1,0][2,1][1,1][3,1]
                {
                    if (prioritySets[locationMap.PrioritySet].MainscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0)
                        CopyToPixelArray(pixels, L2Priority0);
                    if (prioritySets[locationMap.PrioritySet].MainscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0)
                        CopyToPixelArray(pixels, L1Priority0);
                    if (prioritySets[locationMap.PrioritySet].MainscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0 && state.Priority1)
                        CopyToPixelArray(pixels, L2Priority1);
                    if (prioritySets[locationMap.PrioritySet].MainscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0 && state.Priority1)
                        CopyToPixelArray(pixels, L1Priority1);
                    if (prioritySets[locationMap.PrioritySet].MainscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0)
                        CopyToPixelArray(pixels, L3Priority0);
                    if (prioritySets[locationMap.PrioritySet].MainscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0 && state.Priority1)
                        CopyToPixelArray(pixels, L3Priority1);
                }
                else if (!locationMap.TopPriorityL3) // [3,0][3,1][2,0][1,0][2,1][1,1]
                {
                    if (prioritySets[locationMap.PrioritySet].MainscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0)
                        CopyToPixelArray(pixels, L3Priority0);
                    if (prioritySets[locationMap.PrioritySet].MainscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0)
                        CopyToPixelArray(pixels, L3Priority1);
                    if (prioritySets[locationMap.PrioritySet].MainscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0)
                        CopyToPixelArray(pixels, L2Priority0);
                    if (prioritySets[locationMap.PrioritySet].MainscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0)
                        CopyToPixelArray(pixels, L1Priority0);
                    if (prioritySets[locationMap.PrioritySet].MainscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0)
                        CopyToPixelArray(pixels, L2Priority1);
                    if (prioritySets[locationMap.PrioritySet].MainscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0)
                        CopyToPixelArray(pixels, L1Priority1);
                }
            }
        }
        private void CreateSubscreen()
        {
            // 2 possible cases
            if (locationMap.TopPriorityL3) //[3,0][2,0][1,0][2,1][1,1][3,1]
            {
                if (prioritySets[locationMap.PrioritySet].SubscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0)
                    CopyToPixelArray(subscreen, L2Priority0);//DrawLayerByPriorityOne(subscreenPixels, 1, false);
                if (prioritySets[locationMap.PrioritySet].SubscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0)
                    CopyToPixelArray(subscreen, L1Priority0);//DrawLayerByPriorityOne(subscreenPixels, 0, false);
                if (prioritySets[locationMap.PrioritySet].SubscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0)
                    CopyToPixelArray(subscreen, L2Priority1);//DrawLayerByPriorityOne(subscreenPixels, 1, true);
                if (prioritySets[locationMap.PrioritySet].SubscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0)
                    CopyToPixelArray(subscreen, L1Priority1);//DrawLayerByPriorityOne(subscreenPixels, 0, true);
                if (prioritySets[locationMap.PrioritySet].SubscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0)
                    CopyToPixelArray(subscreen, L3Priority0);//DrawLayerByPriorityOne(subscreenPixels, 2, false);
                if (prioritySets[locationMap.PrioritySet].SubscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0)
                    CopyToPixelArray(subscreen, L3Priority1);//DrawLayerByPriorityOne(subscreenPixels, 2, true);
            }
            else if (!locationMap.TopPriorityL3) //[3,0][3,1][2,0][1,0][2,1][1,1]
            {
                if (prioritySets[locationMap.PrioritySet].SubscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0)
                    CopyToPixelArray(subscreen, L3Priority0);//DrawLayerByPriorityOne(subscreenPixels, 2, false);
                if (prioritySets[locationMap.PrioritySet].SubscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0)
                    CopyToPixelArray(subscreen, L3Priority1);//DrawLayerByPriorityOne(subscreenPixels, 2, true);
                if (prioritySets[locationMap.PrioritySet].SubscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0)
                    CopyToPixelArray(subscreen, L2Priority0);//DrawLayerByPriorityOne(subscreenPixels, 1, false);
                if (prioritySets[locationMap.PrioritySet].SubscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0)
                    CopyToPixelArray(subscreen, L1Priority0);//DrawLayerByPriorityOne(subscreenPixels, 0, false);
                if (prioritySets[locationMap.PrioritySet].SubscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0)
                    CopyToPixelArray(subscreen, L2Priority1);//DrawLayerByPriorityOne(subscreenPixels, 1, true);
                if (prioritySets[locationMap.PrioritySet].SubscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0)
                    CopyToPixelArray(subscreen, L1Priority1);//DrawLayerByPriorityOne(subscreenPixels, 0, true);
            }
        }
        private void DoColorMath(int[] dst)
        {
            int r, g, b;
            int minus = prioritySets[this.locationMap.PrioritySet].ColorMathMinusSubscreen;
            int half = prioritySets[this.locationMap.PrioritySet].ColorMathHalfIntensity;
            Size s = locationMap.MaximumSize();
            for (int y = 0; y < s.Height; y++)
            {
                for (int x = 0; x < s.Width; x++)
                {
                    if (subscreen[y * 2048 + x] != 0 && dst[y * 2048 + x] != 0)
                    {
                        r = Color.FromArgb(dst[y * 2048 + x]).R;
                        g = Color.FromArgb(dst[y * 2048 + x]).G;
                        b = Color.FromArgb(dst[y * 2048 + x]).B;
                        if (minus == 0)
                        {
                            if (half == 1)
                            {
                                r /= 2; g /= 2; b /= 2;
                                r += Color.FromArgb(subscreen[y * 2048 + x]).R / 2;
                                g += Color.FromArgb(subscreen[y * 2048 + x]).G / 2;
                                b += Color.FromArgb(subscreen[y * 2048 + x]).B / 2;
                            }
                            else
                            {
                                r += Color.FromArgb(subscreen[y * 2048 + x]).R;
                                g += Color.FromArgb(subscreen[y * 2048 + x]).G;
                                b += Color.FromArgb(subscreen[y * 2048 + x]).B;
                            }
                            if (r > 255) r = 255; if (g > 255) g = 255; if (b > 255) b = 255;
                        }
                        else if (minus == 1)
                        {
                            if (half == 1)
                            {
                                r /= 2; g /= 2; b /= 2;
                                r -= Color.FromArgb(subscreen[y * 2048 + x]).R / 2;
                                g -= Color.FromArgb(subscreen[y * 2048 + x]).G / 2;
                                b -= Color.FromArgb(subscreen[y * 2048 + x]).B / 2;
                            }
                            else
                            {
                                r -= Color.FromArgb(subscreen[y * 2048 + x]).R;
                                g -= Color.FromArgb(subscreen[y * 2048 + x]).G;
                                b -= Color.FromArgb(subscreen[y * 2048 + x]).B;
                            }
                            if (r < 0) r = 0; if (g < 0) g = 0; if (b < 0) b = 0;
                        }
                        dst[y * 2048 + x] = Color.FromArgb(255, r, g, b).ToArgb();
                    }
                }
            }
        }
        private void DoColorMathOnSingleTile(int[] tile, int x, int y)
        {
            int r, g, b;
            for (int w = 0; w < 16; w++)
            {
                for (int v = 0; v < 16; v++)
                {
                    if (subscreen[(y + w) * 2048 + (x + v)] != 0 && tile[w * 16 + v] != 0)
                    {
                        r = Color.FromArgb(tile[w * 16 + v]).R;
                        g = Color.FromArgb(tile[w * 16 + v]).G;
                        b = Color.FromArgb(tile[w * 16 + v]).B;
                        if (prioritySets[locationMap.PrioritySet].ColorMathMinusSubscreen == 0)
                        {
                            if (prioritySets[locationMap.PrioritySet].ColorMathHalfIntensity == 1)
                            {
                                r /= 2; g /= 2; b /= 2;
                                r += Color.FromArgb(subscreen[(y + w) * 2048 + (x + v)]).R / 2;
                                g += Color.FromArgb(subscreen[(y + w) * 2048 + (x + v)]).G / 2;
                                b += Color.FromArgb(subscreen[(y + w) * 2048 + (x + v)]).B / 2;
                            }
                            else
                            {
                                r += Color.FromArgb(subscreen[(y + w) * 2048 + (x + v)]).R;
                                g += Color.FromArgb(subscreen[(y + w) * 2048 + (x + v)]).G;
                                b += Color.FromArgb(subscreen[(y + w) * 2048 + (x + v)]).B;
                            }
                            if (r > 255) r = 255; if (g > 255) g = 255; if (b > 255) b = 255;
                        }
                        else if (prioritySets[locationMap.PrioritySet].ColorMathMinusSubscreen == 1)
                        {
                            if (prioritySets[locationMap.PrioritySet].ColorMathHalfIntensity == 1)
                            {
                                r /= 2; g /= 2; b /= 2;
                                r -= Color.FromArgb(subscreen[(y + w) * 2048 + (x + v)]).R / 2;
                                g -= Color.FromArgb(subscreen[(y + w) * 2048 + (x + v)]).G / 2;
                                b -= Color.FromArgb(subscreen[(y + w) * 2048 + (x + v)]).B / 2;
                            }
                            else
                            {
                                r -= Color.FromArgb(subscreen[(y + w) * 2048 + (x + v)]).R;
                                g -= Color.FromArgb(subscreen[(y + w) * 2048 + (x + v)]).G;
                                b -= Color.FromArgb(subscreen[(y + w) * 2048 + (x + v)]).B;
                            }
                            if (r < 0) r = 0; if (g < 0) g = 0; if (b < 0) b = 0;
                        }
                        tile[w * 16 + v] = Color.FromArgb(255, r, g, b).ToArgb();
                    }
                }
            }
        }
        private void DrawAllLayers()
        {
            if ((prioritySets[locationMap.PrioritySet].SubscreenL1 || prioritySets[locationMap.PrioritySet].MainscreenL1) && locationMap.TilemapL1 != 0)
            {
                DrawLayerByPriorityOne(L1Priority0, 0, false);
                DrawLayerByPriorityOne(L1Priority1, 0, true);
            }
            if ((prioritySets[locationMap.PrioritySet].SubscreenL2 || prioritySets[locationMap.PrioritySet].MainscreenL2) && locationMap.TilemapL2 != 0)
            {
                DrawLayerByPriorityOne(L2Priority0, 1, false);
                DrawLayerByPriorityOne(L2Priority1, 1, true);
            }
            if ((prioritySets[locationMap.PrioritySet].SubscreenL3 || prioritySets[locationMap.PrioritySet].MainscreenL3) && locationMap.TilemapL3 != 0)
            {
                DrawLayerByPriorityOne(L3Priority0, 2, false);
                //DrawLayerByPriorityOne(layer3Priority1, 2, true);
            }
        }
        private void DrawLayerByPriorityOne(int[] dst, int layer, bool priority)
        {
            if (dst.Length != 2048 * 1024 || tilemaps_Tiles[layer] == null)
                return;
            int width = (int)(256 * Math.Pow(2, locationMap.Width[layer]) / 16);
            int height = (int)(256 * Math.Pow(2, locationMap.Height[layer]) / 16);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int i = y * width + x;
                    for (int z = 0; z < 4; z++)
                    {
                        if (tilemaps_Tiles[layer][i].Subtiles[z].Priority1 == priority)
                        {
                            int X = (x * 16) + ((z % 2) * 8);
                            int Y = (y * 16) + ((z / 2) * 8);
                            Do.PixelsToPixels(tilemaps_Tiles[layer][i].Subtiles[z].Pixels, dst, 2048, new Rectangle(X, Y, 8, 8));
                        }
                    }
                    if (tilemaps_Tiles[layer][i].Mirror)
                        Do.FlipHorizontal(dst, 2048, x * 16, y * 16, 16, 16);
                    if (tilemaps_Tiles[layer][i].Invert)
                        Do.FlipVertical(dst, 2048, x * 16, y * 16, 16, 16);
                }
            }
            if (bgw != null && bgw.WorkerReportsProgress)
                bgw.ReportProgress(bgw_progress += ReportProgressIncrement(), "DRAWING TILE MAP: layer " + layer.ToString());
        }
        private void DrawSingleMainscreenTile(int x, int y)
        {
            int bgcolor = Color.Black.ToArgb();
            Array.Clear(tile, 0, tile.Length);
            Array.Clear(tileColorMath, 0, tileColorMath.Length);
            if (HaveSubscreen())
            {
                if (prioritySets[locationMap.PrioritySet].ColorMathBG && state.BG)
                {
                    //for (int i = 0; i < 256; i++)
                    //    tileColorMath[i] = bgcolor;
                    DoColorMathOnSingleTile(tileColorMath, x, y);
                    CopySingleTileToArray(pixels, tileColorMath, 2048, x, y);
                    Array.Clear(tileColorMath, 0, tileColorMath.Length);
                }
                else if (state.BG)
                {
                    //for (int i = 0; i < 256; i++)
                    //    tileColorMath[i] = bgcolor;
                    CopySingleTileToArray(pixels, tileColorMath, 2048, x, y);
                    Array.Clear(tileColorMath, 0, tileColorMath.Length);
                }
                if (locationMap.TopPriorityL3) // [3,0][2,0][1,0][2,1][1,1][3,1]
                {
                    if (prioritySets[locationMap.PrioritySet].MainscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0)
                    {
                        tileColorMath = GetTilePixels(L2Priority0, x, y);
                        if (prioritySets[locationMap.PrioritySet].ColorMathL2)
                            DoColorMathOnSingleTile(tileColorMath, x, y);
                        CopySingleTileToArray(pixels, tileColorMath, 2048, x, y);
                        Array.Clear(tileColorMath, 0, tileColorMath.Length);
                    }
                    if (prioritySets[locationMap.PrioritySet].MainscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0)
                    {
                        tileColorMath = GetTilePixels(L1Priority0, x, y);
                        if (prioritySets[locationMap.PrioritySet].ColorMathL1)
                            DoColorMathOnSingleTile(tileColorMath, x, y);
                        CopySingleTileToArray(pixels, tileColorMath, 2048, x, y);
                        Array.Clear(tileColorMath, 0, tileColorMath.Length);
                    }
                    if (prioritySets[locationMap.PrioritySet].MainscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0 && state.Priority1)
                    {
                        tileColorMath = GetTilePixels(L2Priority1, x, y);
                        if (prioritySets[locationMap.PrioritySet].ColorMathL2)
                            DoColorMathOnSingleTile(tileColorMath, x, y);
                        CopySingleTileToArray(pixels, tileColorMath, 2048, x, y);
                        Array.Clear(tileColorMath, 0, tileColorMath.Length);
                    }
                    if (prioritySets[locationMap.PrioritySet].MainscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0 && state.Priority1)
                    {
                        tileColorMath = GetTilePixels(L1Priority1, x, y);
                        if (prioritySets[locationMap.PrioritySet].ColorMathL1)
                            DoColorMathOnSingleTile(tileColorMath, x, y);
                        CopySingleTileToArray(pixels, tileColorMath, 2048, x, y);
                        Array.Clear(tileColorMath, 0, tileColorMath.Length);
                    }
                    if (prioritySets[locationMap.PrioritySet].MainscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0)
                    {
                        tileColorMath = GetTilePixels(L3Priority0, x, y);
                        if (prioritySets[locationMap.PrioritySet].ColorMathL3)
                            DoColorMathOnSingleTile(tileColorMath, x, y);
                        CopySingleTileToArray(pixels, tileColorMath, 2048, x, y);
                        Array.Clear(tileColorMath, 0, tileColorMath.Length);
                    }
                    if (prioritySets[locationMap.PrioritySet].MainscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0 && state.Priority1)
                    {
                        tileColorMath = GetTilePixels(L3Priority1, x, y);
                        if (prioritySets[locationMap.PrioritySet].ColorMathL3)
                            DoColorMathOnSingleTile(tileColorMath, x, y);
                        CopySingleTileToArray(pixels, tileColorMath, 2048, x, y);
                        Array.Clear(tileColorMath, 0, tileColorMath.Length);
                    }
                }
                else if (!locationMap.TopPriorityL3) // [3,0][3,1][2,0][1,0][2,1][1,1]
                {
                    if (prioritySets[locationMap.PrioritySet].MainscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0)
                    {
                        tileColorMath = GetTilePixels(L3Priority0, x, y);
                        if (prioritySets[locationMap.PrioritySet].ColorMathL3)
                            DoColorMathOnSingleTile(tileColorMath, x, y);
                        CopySingleTileToArray(pixels, tileColorMath, 2048, x, y);
                        Array.Clear(tileColorMath, 0, tileColorMath.Length);
                    }
                    if (prioritySets[locationMap.PrioritySet].MainscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0 && state.Priority1)
                    {
                        tileColorMath = GetTilePixels(L3Priority1, x, y);
                        if (prioritySets[locationMap.PrioritySet].ColorMathL3)
                            DoColorMathOnSingleTile(tileColorMath, x, y);
                        CopySingleTileToArray(pixels, tileColorMath, 2048, x, y);
                        Array.Clear(tileColorMath, 0, tileColorMath.Length);
                    }
                    if (prioritySets[locationMap.PrioritySet].MainscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0)
                    {
                        tileColorMath = GetTilePixels(L2Priority0, x, y);
                        if (prioritySets[locationMap.PrioritySet].ColorMathL2)
                            DoColorMathOnSingleTile(tileColorMath, x, y);
                        CopySingleTileToArray(pixels, tileColorMath, 2048, x, y);
                        Array.Clear(tileColorMath, 0, tileColorMath.Length);
                    }
                    if (prioritySets[locationMap.PrioritySet].MainscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0)
                    {
                        tileColorMath = GetTilePixels(L1Priority0, x, y);
                        if (prioritySets[locationMap.PrioritySet].ColorMathL1)
                            DoColorMathOnSingleTile(tileColorMath, x, y);
                        CopySingleTileToArray(pixels, tileColorMath, 2048, x, y);
                        Array.Clear(tileColorMath, 0, tileColorMath.Length);
                    }
                    if (prioritySets[locationMap.PrioritySet].MainscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0 && state.Priority1)
                    {
                        tileColorMath = GetTilePixels(L2Priority1, x, y);
                        if (prioritySets[locationMap.PrioritySet].ColorMathL2)
                            DoColorMathOnSingleTile(tileColorMath, x, y);
                        CopySingleTileToArray(pixels, tileColorMath, 2048, x, y);
                        Array.Clear(tileColorMath, 0, tileColorMath.Length);
                    }
                    if (prioritySets[locationMap.PrioritySet].MainscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0 && state.Priority1)
                    {
                        tileColorMath = GetTilePixels(L1Priority1, x, y);
                        if (prioritySets[locationMap.PrioritySet].ColorMathL1)
                            DoColorMathOnSingleTile(tileColorMath, x, y);
                        CopySingleTileToArray(pixels, tileColorMath, 2048, x, y);
                        Array.Clear(tileColorMath, 0, tileColorMath.Length);
                    }
                }
            }
            else // No color math, we can go ahead and draw the mainscreen
            {
                if (locationMap.TopPriorityL3) // [3,0][2,0][1,0][2,1][1,1][3,1]
                {
                    if (prioritySets[locationMap.PrioritySet].MainscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0)
                        CopySingleTileToArray(pixels, GetTilePixels(L3Priority0, x, y), 2048, x, y);
                    if (prioritySets[locationMap.PrioritySet].MainscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0)
                        CopySingleTileToArray(pixels, GetTilePixels(L2Priority0, x, y), 2048, x, y);
                    if (prioritySets[locationMap.PrioritySet].MainscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0)
                        CopySingleTileToArray(pixels, GetTilePixels(L1Priority0, x, y), 2048, x, y);
                    if (prioritySets[locationMap.PrioritySet].MainscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0)
                        CopySingleTileToArray(pixels, GetTilePixels(L2Priority1, x, y), 2048, x, y);
                    if (prioritySets[locationMap.PrioritySet].MainscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0)
                        CopySingleTileToArray(pixels, GetTilePixels(L1Priority1, x, y), 2048, x, y);
                    if (prioritySets[locationMap.PrioritySet].MainscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0)
                        CopySingleTileToArray(pixels, GetTilePixels(L3Priority1, x, y), 2048, x, y);
                }
                else if (!locationMap.TopPriorityL3) // [3,0][3,1][2,0][1,0][2,1][1,1]
                {
                    if (prioritySets[locationMap.PrioritySet].MainscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0)
                        CopySingleTileToArray(pixels, GetTilePixels(L3Priority0, x, y), 2048, x, y);
                    if (prioritySets[locationMap.PrioritySet].MainscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0)
                        CopySingleTileToArray(pixels, GetTilePixels(L3Priority1, x, y), 2048, x, y);
                    if (prioritySets[locationMap.PrioritySet].MainscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0)
                        CopySingleTileToArray(pixels, GetTilePixels(L2Priority0, x, y), 2048, x, y);
                    if (prioritySets[locationMap.PrioritySet].MainscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0)
                        CopySingleTileToArray(pixels, GetTilePixels(L1Priority0, x, y), 2048, x, y);
                    if (prioritySets[locationMap.PrioritySet].MainscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0)
                        CopySingleTileToArray(pixels, GetTilePixels(L2Priority1, x, y), 2048, x, y);
                    if (prioritySets[locationMap.PrioritySet].MainscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0)
                        CopySingleTileToArray(pixels, GetTilePixels(L1Priority1, x, y), 2048, x, y);
                }
                // Apply BG color
                if (state.BG)
                {
                    for (int b = y; b < y + 16; b++)
                    {
                        for (int a = x; a < x + 16; a++)
                        {
                            if (pixels[b * 2048 + a] == 0)
                                pixels[b * 2048 + a] = bgcolor;
                        }
                    }
                }
            }
        }
        private void DrawSingleSubscreenTile(int x, int y)
        {
            if (locationMap.TopPriorityL3) //[3,0][2,0][1,0][2,1][1,1][3,1]
            {
                if (prioritySets[locationMap.PrioritySet].SubscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0)
                {
                    tile = GetTilePixels(L3Priority0, x, y);
                    CopySingleTileToArray(subscreen, tile, 2048, x, y);
                    Array.Clear(tile, 0, tile.Length);
                }
                if (prioritySets[locationMap.PrioritySet].SubscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0)
                {
                    tile = GetTilePixels(L2Priority0, x, y);
                    CopySingleTileToArray(subscreen, tile, 2048, x, y);
                    Array.Clear(tile, 0, tile.Length);
                }
                if (prioritySets[locationMap.PrioritySet].SubscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0)
                {
                    tile = GetTilePixels(L1Priority0, x, y);
                    CopySingleTileToArray(subscreen, tile, 2048, x, y);
                    Array.Clear(tile, 0, tile.Length);
                }
                if (prioritySets[locationMap.PrioritySet].SubscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0)
                {
                    tile = GetTilePixels(L2Priority1, x, y);
                    CopySingleTileToArray(subscreen, tile, 2048, x, y);
                    Array.Clear(tile, 0, tile.Length);
                }
                if (prioritySets[locationMap.PrioritySet].SubscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0)
                {
                    tile = GetTilePixels(L1Priority1, x, y);
                    CopySingleTileToArray(subscreen, tile, 2048, x, y);
                    Array.Clear(tile, 0, tile.Length);
                }
                if (prioritySets[locationMap.PrioritySet].SubscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0)
                {
                    tile = GetTilePixels(L3Priority1, x, y);
                    CopySingleTileToArray(subscreen, tile, 2048, x, y);
                    Array.Clear(tile, 0, tile.Length);
                }
            }
            else if (!locationMap.TopPriorityL3) //[3,0][3,1][2,0][1,0][2,1][1,1]
            {
                if (prioritySets[locationMap.PrioritySet].SubscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0)
                {
                    tile = GetTilePixels(L3Priority0, x, y);
                    CopySingleTileToArray(subscreen, tile, 2048, x, y);
                    Array.Clear(tile, 0, tile.Length);
                }
                if (prioritySets[locationMap.PrioritySet].SubscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0)
                {
                    tile = GetTilePixels(L3Priority1, x, y);
                    CopySingleTileToArray(subscreen, tile, 2048, x, y);
                    Array.Clear(tile, 0, tile.Length);
                }
                if (prioritySets[locationMap.PrioritySet].SubscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0)
                {
                    tile = GetTilePixels(L2Priority0, x, y);
                    CopySingleTileToArray(subscreen, tile, 2048, x, y);
                    Array.Clear(tile, 0, tile.Length);
                }
                if (prioritySets[locationMap.PrioritySet].SubscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0)
                {
                    tile = GetTilePixels(L1Priority0, x, y);
                    CopySingleTileToArray(subscreen, tile, 2048, x, y);
                    Array.Clear(tile, 0, tile.Length);
                }
                if (prioritySets[locationMap.PrioritySet].SubscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0)
                {
                    tile = GetTilePixels(L2Priority1, x, y);
                    CopySingleTileToArray(subscreen, tile, 2048, x, y);
                    Array.Clear(tile, 0, tile.Length);
                }
                if (prioritySets[locationMap.PrioritySet].SubscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0)
                {
                    tile = GetTilePixels(L1Priority1, x, y);
                    CopySingleTileToArray(subscreen, tile, 2048, x, y);
                    Array.Clear(tile, 0, tile.Length);
                }
            }
        }
        private bool HaveSubscreen()
        {
            if ((prioritySets[locationMap.PrioritySet].SubscreenL1 && state.Layer1) ||
                (prioritySets[locationMap.PrioritySet].SubscreenL2 && state.Layer2) ||
                (prioritySets[locationMap.PrioritySet].SubscreenL3 && state.Layer3) ||
                (prioritySets[locationMap.PrioritySet].SubscreenOBJ && state.NPCs))
                return true;
            return false;
        }
        private int ReportProgressIncrement()
        {
            int divisor = 0;
            if ((prioritySets[locationMap.PrioritySet].SubscreenL1 || prioritySets[locationMap.PrioritySet].MainscreenL1) && locationMap.TilemapL1 != 0)
                divisor += 2;
            if ((prioritySets[locationMap.PrioritySet].SubscreenL2 || prioritySets[locationMap.PrioritySet].MainscreenL2) && locationMap.TilemapL2 != 0)
                divisor += 2;
            if ((prioritySets[locationMap.PrioritySet].SubscreenL3 || prioritySets[locationMap.PrioritySet].MainscreenL3) && locationMap.TilemapL3 != 0)
                divisor++;
            if (HaveSubscreen()) // We are doing color math by the layer
            {
                if (prioritySets[locationMap.PrioritySet].ColorMathBG && state.BG)
                    divisor++;
                if (locationMap.TopPriorityL3) // [3,0][2,0][1,0][2,1][1,1][3,1]
                {
                    if (prioritySets[locationMap.PrioritySet].MainscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0)
                        divisor += 2;
                    if (prioritySets[locationMap.PrioritySet].MainscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0)
                        divisor += 2;
                    if (prioritySets[locationMap.PrioritySet].MainscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0 && state.Priority1)
                        divisor += 2;
                    if (prioritySets[locationMap.PrioritySet].MainscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0 && state.Priority1)
                        divisor += 2;
                    if (prioritySets[locationMap.PrioritySet].MainscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0)
                        divisor += 2;
                    if (prioritySets[locationMap.PrioritySet].MainscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0 && state.Priority1)
                        divisor += 2;
                }
                else if (!locationMap.TopPriorityL3) // [3,0][3,1][2,0][1,0][2,1][1,1]
                {
                    if (prioritySets[locationMap.PrioritySet].MainscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0)
                        divisor += 2;
                    if (prioritySets[locationMap.PrioritySet].MainscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0 && state.Priority1)
                        divisor += 2;
                    if (prioritySets[locationMap.PrioritySet].MainscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0)
                        divisor += 2;
                    if (prioritySets[locationMap.PrioritySet].MainscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0)
                        divisor += 2;
                    if (prioritySets[locationMap.PrioritySet].MainscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0 && state.Priority1)
                        divisor += 2;
                    if (prioritySets[locationMap.PrioritySet].MainscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0 && state.Priority1)
                        divisor += 2;
                }
            }
            else // No color math, we can go ahead and draw the mainscreen
            {
                if (locationMap.TopPriorityL3) // [3,0][2,0][1,0][2,1][1,1][3,1]
                {
                    if (prioritySets[locationMap.PrioritySet].MainscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0)
                        divisor++;
                    if (prioritySets[locationMap.PrioritySet].MainscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0)
                        divisor++;
                    if (prioritySets[locationMap.PrioritySet].MainscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0 && state.Priority1)
                        divisor++;
                    if (prioritySets[locationMap.PrioritySet].MainscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0 && state.Priority1)
                        divisor++;
                    if (prioritySets[locationMap.PrioritySet].MainscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0)
                        divisor++;
                    if (prioritySets[locationMap.PrioritySet].MainscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0 && state.Priority1)
                        divisor++;
                }
                else if (!locationMap.TopPriorityL3) // [3,0][3,1][2,0][1,0][2,1][1,1]
                {
                    if (prioritySets[locationMap.PrioritySet].MainscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0)
                        divisor++;
                    if (prioritySets[locationMap.PrioritySet].MainscreenL3 && state.Layer3 && locationMap.TilemapL3 != 0)
                        divisor++;
                    if (prioritySets[locationMap.PrioritySet].MainscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0)
                        divisor++;
                    if (prioritySets[locationMap.PrioritySet].MainscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0)
                        divisor++;
                    if (prioritySets[locationMap.PrioritySet].MainscreenL2 && state.Layer2 && locationMap.TilemapL2 != 0)
                        divisor++;
                    if (prioritySets[locationMap.PrioritySet].MainscreenL1 && state.Layer1 && locationMap.TilemapL1 != 0)
                        divisor++;
                }
            }
            if (divisor == 0)
                divisor = 1;
            return 512 / divisor;
        }
        // accessor functions
        public override int GetPixelLayer(int x, int y)
        {
            if (locationMap.TopPriorityL3)
            {
                if (prioritySet.MainscreenL3 && L3Priority1[y * 2048 + x] != 0) return 2;
                else if (prioritySet.MainscreenL3 && L3Priority0[y * 2048 + x] != 0) return 2;
                else if (prioritySet.MainscreenL1 && L1Priority1[y * 2048 + x] != 0) return 0;
                else if (prioritySet.MainscreenL2 && L2Priority1[y * 2048 + x] != 0) return 1;
                else if (prioritySet.MainscreenL1 && L1Priority0[y * 2048 + x] != 0) return 0;
                else if (prioritySet.MainscreenL2 && L2Priority0[y * 2048 + x] != 0) return 1;
            }
            else
            {
                if (prioritySet.MainscreenL1 && L1Priority1[y * 2048 + x] != 0) return 0;
                else if (prioritySet.MainscreenL2 && L2Priority1[y * 2048 + x] != 0) return 1;
                else if (prioritySet.MainscreenL1 && L1Priority0[y * 2048 + x] != 0) return 0;
                else if (prioritySet.MainscreenL2 && L2Priority0[y * 2048 + x] != 0) return 1;
                else if (prioritySet.MainscreenL3 && L3Priority1[y * 2048 + x] != 0) return 2;
                else if (prioritySet.MainscreenL3 && L3Priority0[y * 2048 + x] != 0) return 2;
            }
            return 0;
        }
        public override int[] GetPixels(int layer, Point p, Size s)
        {
            int[] pixels = new int[s.Width * s.Height];
            switch (layer)
            {
                case 0:
                    for (int b = 0, y = p.Y; b < s.Height; b++, y++)
                    {
                        for (int a = 0, x = p.X; a < s.Width; a++, x++)
                        {
                            pixels[b * s.Width + a] = L1Priority0[y * 2048 + x];
                            if (L1Priority1[y * 2048 + x] != 0)
                                pixels[b * s.Width + a] = L1Priority1[y * 2048 + x];
                        }
                    }
                    break;
                case 1:
                    for (int b = 0, y = p.Y; b < s.Height; b++, y++)
                    {
                        for (int a = 0, x = p.X; a < s.Width; a++, x++)
                        {
                            pixels[b * s.Width + a] = L2Priority0[y * 2048 + x];
                            if (L2Priority1[y * 2048 + x] != 0)
                                pixels[b * s.Width + a] = L2Priority1[y * 2048 + x];
                        }
                    }
                    break;
                case 2:
                    for (int b = 0, y = p.Y; b < s.Height; b++, y++)
                    {
                        for (int a = 0, x = p.X; a < s.Width; a++, x++)
                        {
                            pixels[b * s.Width + a] = L3Priority0[y * 2048 + x];
                            if (L3Priority1[y * 2048 + x] != 0)
                                pixels[b * s.Width + a] = L3Priority1[y * 2048 + x];
                        }
                    }
                    break;
                default:
                    goto case 0;
            }
            return pixels;
        }
        public override int[] GetPixels(Point p, Size s)
        {
            int[] pixels = new int[s.Width * s.Height];
            int bgcolor = Color.Black.ToArgb();
            for (int b = 0, y = p.Y; b < s.Height; b++, y++)
            {
                for (int a = 0, x = p.X; a < s.Width; a++, x++)
                {
                    int srcIndex = y * 2048 + x;
                    int dstIndex = b * s.Width + a;
                    if (srcIndex >= this.pixels.Length || dstIndex >= pixels.Length)
                        continue;
                    if (this.pixels[srcIndex] != 0)
                        pixels[dstIndex] = Color.FromArgb(this.pixels[srcIndex]).ToArgb();
                    else if (state.BG)
                        pixels[dstIndex] = Color.FromArgb(bgcolor).ToArgb();
                }
            }
            return pixels;
        }
        public override int[] GetPriority1Pixels()
        {
            int[] pixels = new int[2048 * 1024];
            for (int y = 0; y < 2048; y++)
            {
                for (int x = 0; x < 2048; x++)
                {
                    if (L1Priority1[y * 2048 + x] != 0 ||
                        L2Priority1[y * 2048 + x] != 0 ||
                        L3Priority1[y * 2048 + x] != 0)
                        pixels[y * 2048 + x] = Color.Blue.ToArgb();
                }
            }
            return pixels;
        }
        public override int GetTileNum(int layer, int x, int y)
        {
            return GetTileNum(layer, x, y, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="width">The width, in 16x16 tiles, of the tilemap.</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int GetTileNum(int layer, int width, int x, int y)
        {
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (x >= locationMap.Width_p[layer]) return 0;
            if (y >= locationMap.Height_p[layer]) return 0;
            y /= 16;
            x /= 16;
            int placement = y * width + x;
            if (placement >= tilemaps_Tiles[layer].Length)
                return 0;
            if (layer < 3 && tilemaps_Tiles[layer] != null)
                return tilemaps_Tiles[layer][placement].Index;
            else return 0;
        }
        public override int GetTileNum(int layer, int x, int y, bool ignoretransparent)
        {
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (x >= locationMap.Width_p[layer]) return 0;
            if (y >= locationMap.Height_p[layer]) return 0;
            Point p = new Point(x % 16, y % 16);
            y /= 16;
            x /= 16;
            int width = locationMap.Width_t[layer];
            int placement = y * width + x;
            if (layer < 3 && tilemaps_Tiles[layer] != null)
            {
                if (!ignoretransparent)
                    return tilemaps_Tiles[layer][placement].Index;
                else if (tilemaps_Tiles[layer][placement].Pixels[p.Y * 16 + p.X] != 0)
                    return tilemaps_Tiles[layer][placement].Index;
                else
                    return 0;
            }
            else
                return 0;
        }
        private int[] GetTilePixels(int[] src, int x, int y)
        {
            int counter = 0;
            for (int i = 0; i < 256; i++)
            {
                if (src[y * 2048 + x + counter] != 0)
                    tile[i] = src[y * 2048 + x + counter];
                counter++;
                if (counter % 16 == 0)
                {
                    y++;
                    counter = 0;
                }
            }
            return tile;
        }
        public override void SetTileNum(int tileNum, int layer, int x, int y)
        {
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (x >= locationMap.Width_p[layer]) return;
            if (y >= locationMap.Height_p[layer]) return;
            y /= 16;
            x /= 16;
            int index = y * locationMap.Width_t[layer] + x;
            if (index < tilemaps_Tiles[layer].Length)
                ChangeSingleTile(layer, index, tileNum, x * 16, y * 16);
            switch (layer)
            {
                case 0: Model.EditTilemaps[this.locationMap.TilemapL1] = true; break;
                case 1: Model.EditTilemaps[this.locationMap.TilemapL2] = true; break;
                case 2: Model.EditTilemaps[this.locationMap.TilemapL3] = true; break;
            }
        }
        // universal variables
        public override void RedrawTilemap()
        {
            Array.Clear(L1Priority1, 0, L1Priority1.Length);
            Array.Clear(L2Priority1, 0, L1Priority1.Length);
            Array.Clear(L3Priority1, 0, L1Priority1.Length);
            DrawAllLayers();
            Array.Clear(pixels, 0, pixels.Length);
            if (subscreen != null)
                Array.Clear(subscreen, 0, subscreen.Length);
            if ((prioritySets[locationMap.PrioritySet].SubscreenL1 && state.Layer1) ||
                    (prioritySets[locationMap.PrioritySet].SubscreenL2 && state.Layer2) ||
                    (prioritySets[locationMap.PrioritySet].SubscreenL3 && state.Layer3) ||
                    (prioritySets[locationMap.PrioritySet].SubscreenOBJ && state.NPCs))
            {
                if (subscreen == null)
                    subscreen = new int[2048 * 1024];
                CreateSubscreen(); // Create the subscreen if needed
            }
            CreateMainscreen();
            bgw_progress = 0;
        }
        public void Clear(int count)
        {
            if (count == 1)
            {
                Model.Tilemaps[locationMap.TilemapL1] = new byte[Model.TilemapSizes[locationMap.TilemapL1]];
                Model.Tilemaps[locationMap.TilemapL2] = new byte[Model.TilemapSizes[locationMap.TilemapL2]];
                Model.Tilemaps[locationMap.TilemapL3] = new byte[0x1000];
                Model.EditTilemaps[locationMap.TilemapL1] = true;
                Model.EditTilemaps[locationMap.TilemapL2] = true;
                Model.EditTilemaps[locationMap.TilemapL3] = true;
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    Model.Tilemaps[i] = new byte[Model.TilemapSizes[i]];
                    Model.EditTilemaps[i] = true;
                }
            }
            RedrawTilemap();
        }
        #endregion
    }
}