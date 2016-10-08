using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ZONEDOCTOR
{
    public class Tileset
    {
        private LocationMap locationMap;
        private byte[][] tilesets_bytes = new byte[3][];
        private byte[] graphics;
        private byte[] graphicsL3;
        private byte[] palette_bytes = new byte[0x80];
        private Tile[][] tilesets_tiles = new Tile[3][];
        // public variables
        public TilesetType Type;
        public PaletteSet paletteSet;
        public byte[] Tileset_bytes { get { return tilesets_bytes[0]; } set { tilesets_bytes[0] = value; } }
        public byte[][] Tilesets_bytes { get { return tilesets_bytes; } set { tilesets_bytes = value; } }
        public byte[] Graphics { get { return graphics; } set { graphics = value; } }
        public byte[] GraphicsL3 { get { return graphicsL3; } set { graphicsL3 = value; } }
        public Tile[] Tileset_tiles { get { return tilesets_tiles[0]; } set { tilesets_tiles[0] = value; } }
        public Tile[][] Tilesets_tiles { get { return tilesets_tiles; } set { tilesets_tiles = value; } }
        // constructors
        public Tileset(LocationMap locationMap, PaletteSet paletteSet)
        {
            this.Type = TilesetType.Location;
            this.locationMap = locationMap; // grab the current location map
            this.paletteSet = paletteSet; // grab the current Palette Set
            // set tileset byte arrays
            tilesets_bytes[0] = Model.Tilesets[locationMap.TilesetL1];
            tilesets_bytes[1] = Model.Tilesets[locationMap.TilesetL2];
            tilesets_bytes[2] = new byte[0x1000];
            // combine graphic sets into one array
            graphics = new byte[0x8000];
            Buffer.BlockCopy(Model.GraphicSets[locationMap.GraphicSetA], 0, graphics, 0, 0x2000);
            Buffer.BlockCopy(Model.GraphicSets[locationMap.GraphicSetB], 0, graphics, 0x2000, 0x1000);
            Buffer.BlockCopy(Model.GraphicSets[locationMap.GraphicSetC], 0, graphics, 0x3000, 0x1000);
            Buffer.BlockCopy(Model.GraphicSets[locationMap.GraphicSetD], 0, graphics, 0x4000, 0x1000);
            if (locationMap.TilemapL3 != 0)
            {
                graphicsL3 = Bits.GetBytes(Model.GraphicSetsL3[locationMap.GraphicSetL3], 0x40, 0x1000);
                palette_bytes = Bits.GetBytes(Model.GraphicSetsL3[locationMap.GraphicSetL3], 0, 0x40);
            }
            // store animation graphics
            int pointer = Bits.GetShort(Model.ROM, locationMap.AnimationL2 * 2 + 0x0091D5) + 2;
            /*for (int y = 0; y < 32; y++) // 8 rows
            {
                for (int i = 0; i < 0x80; i++) // 128 bytes each tile
                {
                    int offset = Bits.GetShort(Model.ROM, pointer + 0x0091FF + (y * 10));
                    graphics[y * 0x80 + i + 0x5000] = Model.AnimatedGraphics[i + offset];
                }
            }*/
            // initialize 16x16 tile arrays
            if (locationMap.TilemapL1 != 0)
                tilesets_tiles[0] = new Tile[16 * 16];
            if (locationMap.TilemapL2 != 0)
                tilesets_tiles[1] = new Tile[16 * 16];
            if (locationMap.TilemapL3 != 0)
                tilesets_tiles[2] = new Tile[16 * 16];
            for (int l = 0; l < 3; l++)
            {
                if (tilesets_tiles[l] == null)
                    continue;
                for (int i = 0; i < tilesets_tiles[l].Length; i++)
                    tilesets_tiles[l][i] = new Tile(i);
            }
            // draw all 16x16 tiles
            if (locationMap.TilemapL1 != 0)
                DrawTileset(tilesets_bytes[0], tilesets_tiles[0], graphics, 0x20);
            if (locationMap.TilemapL2 != 0)
                DrawTileset(tilesets_bytes[1], tilesets_tiles[1], graphics, 0x20);
            if (locationMap.TilemapL3 != 0)
                DrawTileset(tilesets_bytes[2], tilesets_tiles[2], graphicsL3, 0x10);
        }
        // world map tileset constructor
        public Tileset(LocationMap locationMap, PaletteSet paletteSet, TilesetType type)
        {
            this.Type = type;
            this.locationMap = locationMap; // grab the current location map
            this.paletteSet = paletteSet; // grab the current Palette Set
            this.graphics = new byte[0x2000];
            this.tilesets_tiles[0] = new Tile[16 * 16];
            this.tilesets_bytes[0] = new byte[0x480];
            for (int i = 0; i < tilesets_tiles[0].Length; i++)
                this.tilesets_tiles[0][i] = new Tile(i);
            if (locationMap.Index == 0)
            {
                for (int i = 0; i < 0x400; i++)
                    tilesets_bytes[0][i] = Model.WOBGraphicSet[i];
                for (int i = 0x400, a = 0; i < 0x2400; i++, a++)
                    graphics[a] = Model.WOBGraphicSet[i];
                for (int i = 0x2400, a = 0; i < 0x2480; i++, a++)
                    palette_bytes[a] = Model.WOBGraphicSet[i];
            }
            else if (locationMap.Index == 1)
            {
                for (int i = 0; i < 0x400; i++)
                    tilesets_bytes[0][i] = Model.WORGraphicSet[i];
                for (int i = 0x400, a = 0; i < 0x2400; i++, a++)
                    graphics[a] = Model.WORGraphicSet[i];
                for (int i = 0x2400, a = 0; i < 0x2480; i++, a++)
                    palette_bytes[a] = Model.WORGraphicSet[i];
            }
            else
            {
                for (int i = 0; i < 0x400; i++)
                    tilesets_bytes[0][i] = Model.STGraphicSet[i];
                for (int i = 0x400, a = 0; i < 0x2400; i++, a++)
                    graphics[a] = Model.STGraphicSet[i];
                for (int i = 0x2400, a = 0; i < 0x2480; i++, a++)
                    palette_bytes[a] = Model.STGraphicSet[i];
            }
            DrawTilesetM7(tilesets_bytes[0], tilesets_tiles[0]);
        }
        // class functions
        public void DrawTilesetM7(byte[] src, Tile[] dst)
        {
            int offset = 0;
            int[][] palettes = paletteSet.Palettes;
            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    int i = y * 16 + x;
                    for (int z = 0; z < 4; z++)
                    {
                        byte tile = tilesets_bytes[0][offset];
                        byte temp = palette_bytes[tile / 2];
                        temp = tile % 2 == 0 ? (byte)(temp & 0x07) : (byte)((temp & 0x70) >> 4);
                        Subtile source = Do.DrawSubtileM7(tile, temp, graphics, palettes, 0x20);
                        tilesets_tiles[0][i].Subtiles[z] = source;
                        offset++;
                    }
                }
            }
        }
        public void DrawTileset(byte[] src, Tile[] dst, byte[] graphics, byte format)
        {
            int offset = 0;
            int[][] palettes = paletteSet.Palettes;
            for (int i = 0; i < dst.Length; i++)
            {
                if (format == 0x20)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        ushort tile = src[offset]; offset += 0x400;
                        byte temp = src[offset]; offset -= 0x400; // Palette Set?
                        tile += (ushort)((temp & 0x03) << 8);
                        Subtile source = Do.DrawSubtile(tile, temp, graphics, palettes, format);
                        dst[i].Subtiles[z] = source;
                        offset += 0x100;
                    }
                    offset++;
                    offset -= 0x400;
                }
                else
                {
                    for (int z = 0; z < 4; z++)
                    {
                        byte tile = (byte)((i * 4) + z);
                        byte temp = palette_bytes[i % 64];
                        Subtile source = Do.DrawSubtile(tile, temp, graphics, palettes, format);
                        dst[i].Subtiles[z] = source;
                    }
                }
            }
        }
        public void DrawTileset(Tile[] src, byte[] dst)
        {
            int offset = 0;
            if (Type == TilesetType.Location)
            {
                for (int y = 0; y < src.Length / 16; y++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        Tile tile = src[y * 16 + x];
                        for (int z = 0; z < 4; z++)
                        {
                            offset = y * 16 + x;
                            offset += z * 0x100;
                            Subtile subtile = tile.Subtiles[z];
                            if (subtile == null) continue;
                            dst[offset] = (byte)subtile.Index;
                            offset += 0x400;
                            dst[offset] = (byte)(subtile.Index >> 8);
                            dst[offset] |= (byte)(subtile.Palette << 2);
                            Bits.SetBit(dst, offset, 5, subtile.Priority1);
                            Bits.SetBit(dst, offset, 6, subtile.Mirror);
                            Bits.SetBit(dst, offset, 7, subtile.Invert);
                        }
                    }
                }
            }
            else
            {
                for (int y = 0; y < 16; y++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        int i = y * 16 + x;
                        for (int z = 0; z < 4; z++)
                        {
                            Subtile source = src[i].Subtiles[z];
                            byte tile = (byte)source.Index;
                            dst[offset] = tile;
                            if (tile % 2 == 0)
                                Bits.SetByteBits(palette_bytes, tile / 2, (byte)source.Palette, 0x07);
                            else
                                Bits.SetByteBits(palette_bytes, tile / 2, (byte)(source.Palette << 4), 0x70);
                            offset++;
                        }
                    }
                }
            }
        }
        public void RedrawTilesets()
        {
            if (Type == TilesetType.Location)
            {
                if (tilesets_tiles[0] != null)// || layer == 3)
                    DrawTileset(tilesets_bytes[0], tilesets_tiles[0], graphics, 0x20);
                if (tilesets_tiles[1] != null)// || layer == 3)
                    DrawTileset(tilesets_bytes[1], tilesets_tiles[1], graphics, 0x20);
                if (tilesets_tiles[2] != null)// || layer == 3)
                    DrawTileset(tilesets_bytes[2], tilesets_tiles[2], graphicsL3, 0x10);
            }
            else
                DrawTilesetM7(tilesets_bytes[0], tilesets_tiles[0]);
        }
        public void RedrawTilesets(int layer)
        {
            if (Type == TilesetType.Location)
            {
                byte format = (byte)(layer != 2 ? 0x20 : 0x10);
                byte[] graphics = layer != 2 ? this.graphics : this.graphicsL3;
                //
                if (tilesets_tiles[layer] != null)
                    DrawTileset(tilesets_bytes[layer], tilesets_tiles[layer], graphics, format);
            }
            else
                DrawTilesetM7(tilesets_bytes[0], tilesets_tiles[0]);
        }
        // assemblers
        public void Assemble(int width, int layer)
        {
            if (tilesets_tiles[layer] == null)
                return;
            int offset = 0;
            if (Type == TilesetType.Location)
            {
                for (int y = 0; y < tilesets_tiles[layer].Length / width; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Tile tile = tilesets_tiles[layer][y * width + x];
                        for (int s = 0; s < 4; s++)
                        {
                            offset = y * width + x;
                            offset += s * 0x100;
                            Subtile subtile = tile.Subtiles[s];
                            if (subtile == null) continue;
                            tilesets_bytes[layer][offset] = (byte)subtile.Index;
                            offset += 0x400;
                            tilesets_bytes[layer][offset] = (byte)(subtile.Index >> 8);
                            tilesets_bytes[layer][offset] |= (byte)(subtile.Palette << 2);
                            Bits.SetBit(tilesets_bytes[layer], offset, 5, subtile.Priority1);
                            Bits.SetBit(tilesets_bytes[layer], offset, 6, subtile.Mirror);
                            Bits.SetBit(tilesets_bytes[layer], offset, 7, subtile.Invert);
                        }
                    }
                }
                if (layer == 0)
                    Model.EditTilesets[this.locationMap.TilesetL1] = true;
                if (layer == 1)
                    Model.EditTilesets[this.locationMap.TilesetL2] = true;
                Buffer.BlockCopy(graphics, 0, Model.GraphicSets[this.locationMap.GraphicSetA], 0, 0x2000);
                Buffer.BlockCopy(graphics, 0x2000, Model.GraphicSets[this.locationMap.GraphicSetB], 0, 0x1000);
                Buffer.BlockCopy(graphics, 0x3000, Model.GraphicSets[this.locationMap.GraphicSetC], 0, 0x1000);
                Buffer.BlockCopy(graphics, 0x4000, Model.GraphicSets[this.locationMap.GraphicSetD], 0, 0x1000);
                if (locationMap.TilemapL3 != 0)
                {
                    palette_bytes.CopyTo(Model.GraphicSetsL3[locationMap.GraphicSetL3], 0);
                    graphicsL3.CopyTo(Model.GraphicSetsL3[locationMap.GraphicSetL3], 0x40);
                }
                // Store animation graphics
                int pointer = Bits.GetShort(Model.ROM, locationMap.AnimationL2 * 2 + 0x0091D5) + 2;
                for (int y = 0; y < 32; y++) // 8 rows
                {
                    for (int i = 0; i < 0x80; i++) // 128 bytes each tile
                    {
                        offset = Bits.GetShort(Model.ROM, pointer + 0x0091FF + (y * 10));
                        Model.AnimatedGraphics[i + offset] = graphics[y * 0x80 + i + 0x5000];
                    }
                }
            }
            else
                Assemble(locationMap.Index < 2 ? 256 : 128);
        }
        public void Assemble(int width)
        {
            int offset = 0;
            if (Type == TilesetType.Location)
            {
                for (int l = 0; l < tilesets_tiles.Length; l++)
                {
                    if (tilesets_tiles[l] == null) continue;
                    for (int y = 0; y < tilesets_tiles[l].Length / width; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            Tile tile = tilesets_tiles[l][y * width + x];
                            for (int s = 0; s < 4; s++)
                            {
                                offset = y * width + x;
                                offset += s * 0x100;
                                Subtile subtile = tile.Subtiles[s];
                                if (subtile == null) continue;
                                tilesets_bytes[l][offset] = (byte)subtile.Index;
                                offset += 0x400;
                                tilesets_bytes[l][offset] |= (byte)(subtile.Palette << 2);
                                Bits.SetBit(tilesets_bytes[l], offset, 5, subtile.Priority1);
                                Bits.SetBit(tilesets_bytes[l], offset, 6, subtile.Mirror);
                                Bits.SetBit(tilesets_bytes[l], offset, 7, subtile.Invert);
                            }
                        }
                    }
                }
                Model.EditTilesets[locationMap.TilesetL1] = true;
                Model.EditTilesets[locationMap.TilesetL2] = true;
                Buffer.BlockCopy(graphics, 0, Model.GraphicSets[locationMap.GraphicSetA], 0, 0x2000);
                Buffer.BlockCopy(graphics, 0x2000, Model.GraphicSets[locationMap.GraphicSetB], 0, 0x1000);
                Buffer.BlockCopy(graphics, 0x3000, Model.GraphicSets[locationMap.GraphicSetC], 0, 0x1000);
                Buffer.BlockCopy(graphics, 0x4000, Model.GraphicSets[locationMap.GraphicSetD], 0, 0x1000);
                if (locationMap.TilemapL3 != 0)
                {
                    palette_bytes.CopyTo(Model.GraphicSetsL3[locationMap.GraphicSetL3], 0);
                    graphicsL3.CopyTo(Model.GraphicSetsL3[locationMap.GraphicSetL3], 0);
                }
                // Store animation graphics
                int pointer = Bits.GetShort(Model.ROM, locationMap.AnimationL2 * 2 + 0x0091D5) + 2;
                for (int y = 0; y < 32; y++) // 8 rows
                {
                    for (int i = 0; i < 0x80; i++) // 128 bytes each tile
                    {
                        offset = Bits.GetShort(Model.ROM, pointer + 0x0091FF + (y * 10));
                        Model.AnimatedGraphics[i + offset] = graphics[y * 0x80 + i + 0x5000];
                    }
                }
            }
            else
            {
                for (int y = 0; y < 16; y++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        int i = y * 16 + x;
                        for (int z = 0; z < 4; z++)
                        {
                            Subtile source = tilesets_tiles[0][i].Subtiles[z];
                            byte tile = (byte)source.Index;
                            tilesets_bytes[0][offset] = tile;
                            if (tile % 2 == 0)
                                Bits.SetByteBits(palette_bytes, tile / 2, (byte)source.Palette, 0x07);
                            else
                                Bits.SetByteBits(palette_bytes, tile / 2, (byte)(source.Palette << 4), 0x70);
                            offset++;
                        }
                    }
                }
                if (locationMap.Index == 0)
                {
                    Model.EditWOBGraphicSet = true;
                    for (int i = 0, a = 0; i < 0x400; i++, a++)
                        Model.WOBGraphicSet[i] = tilesets_bytes[0][a];
                    for (int i = 0x400, a = 0; i < 0x2400; i++, a++)
                        Model.WOBGraphicSet[i] = graphics[a];
                    for (int i = 0x2400, a = 0; i < 0x2480; i++, a++)
                        Model.WOBGraphicSet[i] = palette_bytes[a];
                }
                else if (locationMap.Index == 1)
                {
                    Model.EditWORGraphicSet = true;
                    for (int i = 0, a = 0; i < 0x400; i++, a++)
                        Model.WORGraphicSet[i] = tilesets_bytes[0][a];
                    for (int i = 0x400, a = 0; i < 0x2400; i++, a++)
                        Model.WORGraphicSet[i] = graphics[a];
                    for (int i = 0x2400, a = 0; i < 0x2480; i++, a++)
                        Model.WORGraphicSet[i] = palette_bytes[a];
                }
                else
                {
                    Model.EditSTGraphicSet = true;
                    for (int i = 0, a = 0; i < 0x400; i++, a++)
                        Model.STGraphicSet[i] = tilesets_bytes[0][a];
                    for (int i = 0x400, a = 0; i < 0x2400; i++, a++)
                        Model.STGraphicSet[i] = graphics[a];
                    for (int i = 0x2400, a = 0; i < 0x2480; i++, a++)
                        Model.STGraphicSet[i] = palette_bytes[a];
                }
            }
        }
        // accessor functions
        public int GetTileNum(int layer, int x, int y)
        {
            if (layer < 3)
                return tilesets_tiles[layer][x + y * 16].Index;
            else return 0;
        }
        public int GetTileNum(int x, int y)
        {
            return tilesets_tiles[0][x + y * 16].Index;
        }
        // universal functions
        public void Clear(int count)
        {
            if (Type == TilesetType.Location)
            {
                if (count == 1)
                {
                    Model.Tilesets[locationMap.TilesetL1] = new byte[0x2000];
                    Model.Tilesets[locationMap.TilesetL2] = new byte[0x2000];
                    Model.EditTilesets[locationMap.TilesetL1] = true;
                    Model.EditTilesets[locationMap.TilesetL2] = true;
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (i < 0x20)
                            Model.Tilesets[i] = new byte[0x1000];
                        else
                            Model.Tilesets[i] = new byte[0x2000];
                        Model.EditTilesets[i] = true;
                    }
                }
                for (int i = 0; i < 3; i++)
                    RedrawTilesets(i);
            }
            else
            {
                if (locationMap.Index == 0)
                {
                    for (int i = 0; i < 0x400; i++)
                        tilesets_bytes[0][i] = Model.WOBGraphicSet[i] = 0;
                }
                else if (locationMap.Index == 1)
                {
                    for (int i = 0; i < 0x400; i++)
                        tilesets_bytes[0][i] = Model.WORGraphicSet[i] = 0;
                }
                else
                {
                    for (int i = 0; i < 0x400; i++)
                        tilesets_bytes[0][i] = Model.STGraphicSet[i] = 0;
                }
                RedrawTilesets();
            }
        }
    }
}
