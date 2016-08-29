﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using ZONEDOCTOR.Properties;
using ZONEDOCTOR.ScriptsEditor;
using ZONEDOCTOR.ScriptsEditor.Commands;

namespace ZONEDOCTOR
{
    /// <summary>
    /// Provides a number of functions for drawing and modifying images and writing text.
    /// </summary>
    public static class Do
    {
        private static ProgressBar ProgressBar;
        private static Stopwatch StopWatch;
        #region Drawing
        /// <summary>
        /// Applys a palette to a pixel array.
        /// </summary>
        /// <param name="array">The pixel array.</param>
        /// <param name="palette">The palette to apply.</param>
        /// <returns></returns>
        public static void ApplyPaletteToPixels(int[] array, int[] palette)
        {
            Color[] colors = new Color[palette.Length];
            Color[] newColors = new Color[palette.Length];
            double distance = 500.0;
            double temp;
            double r, g, b;
            double dbl_test_red;
            double dbl_test_green;
            double dbl_test_blue;
            for (int i = 0; i < palette.Length; i++)
                colors[i] = Color.FromArgb(palette[i]);
            for (int i = 0; i < array.Length; i++)
            {
                distance = 500;
                r = Convert.ToDouble(Color.FromArgb(array[i]).R);
                g = Convert.ToDouble(Color.FromArgb(array[i]).G);
                b = Convert.ToDouble(Color.FromArgb(array[i]).B);
                int nearest_color = 0;
                Color o;
                for (int v = 1; v < colors.Length; v++)
                {
                    o = colors[v];
                    dbl_test_red = Math.Pow(Convert.ToDouble(((Color)o).R) - r, 2.0);
                    dbl_test_green = Math.Pow(Convert.ToDouble(((Color)o).G) - g, 2.0);
                    dbl_test_blue = Math.Pow(Convert.ToDouble(((Color)o).B) - b, 2.0);
                    temp = Math.Sqrt(dbl_test_blue + dbl_test_green + dbl_test_red);
                    // explore the result and store the nearest color
                    if (temp == 0.0)
                    {
                        nearest_color = v;
                        break;
                    }
                    else if (temp < distance)
                    {
                        distance = temp;
                        nearest_color = v;
                    }
                }
                if (array[i] != 0)
                    array[i] = nearest_color;
            }
        }
        /// <summary>
        /// Applys a palette to a region in a pixel array.
        /// </summary>
        /// <param name="array">The pixel array.</param>
        /// <param name="palette">The palette to apply.</param>
        /// <param name="src">The full region of the source.</param>
        /// <param name="dst">The region to modify.</param>
        public static void ApplyPaletteToPixels(int[] array, int[] palette, Rectangle src, Rectangle dst)
        {
            Color[] colors = new Color[palette.Length];
            Color[] newColors = new Color[palette.Length];
            double distance = 500.0;
            double temp;
            double r, g, b;
            double dbl_test_red;
            double dbl_test_green;
            double dbl_test_blue;
            for (int i = 0; i < palette.Length; i++)
                colors[i] = Color.FromArgb(palette[i]);
            for (int y = dst.Y; y < dst.Y + dst.Height; y++)
            {
                for (int x = dst.X; x < dst.X + dst.Width; x++)
                {
                    distance = 500;
                    r = Convert.ToDouble(Color.FromArgb(array[y * src.Width + x]).R);
                    g = Convert.ToDouble(Color.FromArgb(array[y * src.Width + x]).G);
                    b = Convert.ToDouble(Color.FromArgb(array[y * src.Width + x]).B);
                    int nearest_color = 0;
                    Color o;
                    for (int v = 1; v < colors.Length; v++)
                    {
                        o = colors[v];
                        dbl_test_red = Math.Pow(Convert.ToDouble(((Color)o).R) - r, 2.0);
                        dbl_test_green = Math.Pow(Convert.ToDouble(((Color)o).G) - g, 2.0);
                        dbl_test_blue = Math.Pow(Convert.ToDouble(((Color)o).B) - b, 2.0);
                        temp = Math.Sqrt(dbl_test_blue + dbl_test_green + dbl_test_red);
                        // explore the result and store the nearest color
                        if (temp == 0.0)
                        {
                            nearest_color = v;
                            break;
                        }
                        else if (temp < distance)
                        {
                            distance = temp;
                            nearest_color = v;
                        }
                    }
                    if (array[y * src.Width + x] != 0)
                        array[y * src.Width + x] = nearest_color;
                }
            }
        }
        public static int[] BPPtoPixels(byte[] subtile, int[] palette, bool twobpp)
        {
            int offset = 0;
            int[] pixels = new int[8 * 8];
            if (twobpp == false)
            {
                for (int r = 0; r < 8; r++) // Number of Rows in an 8x8 Tile
                {
                    // Get all the pixels in a row
                    byte[] row = new byte[8];
                    for (int i = 7, b = 1; i >= 0; i--, b *= 2)
                        if ((subtile[offset + r * 2 + 0x11] & b) == b)
                            row[i] += 8;
                    for (int i = 7, b = 1; i >= 0; i--, b *= 2)
                        if ((subtile[offset + r * 2 + 0x10] & b) == b)
                            row[i] += 4;
                    for (int i = 7, b = 1; i >= 0; i--, b *= 2)
                        if ((subtile[offset + r * 2 + 1] & b) == b)
                            row[i] += 2;
                    for (int i = 7, b = 1; i >= 0; i--, b *= 2)
                        if ((subtile[offset + r * 2] & b) == b)
                            row[i]++;
                    for (int c = 0; c < 8; c++) // Number of Columns in an 8x8 Tile
                    {
                        if (row[c] != 0)
                            pixels[r * 8 + c] = palette[row[c]]; // Set pixel in 8x8 tile
                    }
                }
            }
            else
            {
                byte b1, b2, t1, t2, col = 0;
                int[] pal = new int[4];
                for (int i = 0; i < 4; i++)
                    pal[i] = palette[i];
                for (byte i = 0; i < 8; i++)
                {
                    b1 = subtile[offset];
                    b2 = subtile[offset + 1];
                    for (byte z = 7; col < 8; z--)
                    {
                        t1 = (byte)((b1 >> z) & 1);
                        t2 = (byte)((b2 >> z) & 1);
                        if ((t2 * 2) + t1 != 0)
                            pixels[(i * 8) + col] = pal[(t2 * 2) + t1];
                        col++;
                    }
                    col = 0;
                    offset += 2;
                }
            }
            return pixels;
        }
        /// <summary>
        /// Resizes the canvas of a bitmap image.
        /// </summary>
        /// <param name="image">The image to modify.</param>
        /// <param name="width">The new width of the canvas.</param>
        /// <param name="height">The new height of the canvas.</param>
        /// <returns></returns>
        public static Bitmap CanvasSize(Bitmap image, int width, int height)
        {
            Bitmap resized = new Bitmap(width, height);
            Graphics temp = Graphics.FromImage(resized);
            temp.DrawImage(image, 0, 0, image.Width, image.Height);
            return resized;
        }
        /// <summary>
        /// Performs color math on a pixel.
        /// </summary>
        /// <param name="src">The pixel to draw onto.</param>
        /// <param name="dst">The pixel being drawn.</param>
        /// <returns></returns>
        public static int ColorMath(int src, int dst, bool halfIntensity, bool minusSubscreen)
        {
            int rsrc = Color.FromArgb(src).R;
            int gsrc = Color.FromArgb(src).G;
            int bsrc = Color.FromArgb(src).B;
            int rdst = Color.FromArgb(dst).R;
            int gdst = Color.FromArgb(dst).G;
            int bdst = Color.FromArgb(dst).B;
            if (minusSubscreen)
            {
                if (halfIntensity)
                {
                    rsrc /= 2; gsrc /= 2; bsrc /= 2;
                    rsrc -= rdst / 2;
                    gsrc -= gdst / 2;
                    bsrc -= bdst / 2;
                }
                else
                {
                    rsrc -= rdst;
                    gsrc -= gdst;
                    bsrc -= bdst;
                }
                if (rsrc < 0) rsrc = 0;
                if (gsrc < 0) gsrc = 0;
                if (bsrc < 0) bsrc = 0;
            }
            else
            {
                if (halfIntensity)
                {
                    rsrc /= 2; gsrc /= 2; bsrc /= 2;
                    rsrc += rdst / 2;
                    gsrc += gdst / 2;
                    bsrc += bdst / 2;
                }
                else
                {
                    rsrc += rdst;
                    gsrc += gdst;
                    bsrc += bdst;
                }
                if (rsrc > 255) rsrc = 255;
                if (gsrc > 255) gsrc = 255;
                if (bsrc > 255) bsrc = 255;
            }
            return Color.FromArgb(rsrc, gsrc, bsrc).ToArgb();
        }
        public static int[] ColorsToPixels(int[] src, int[] palette)
        {
            return ColorsToPixels(src, palette, 0);
        }
        /// <summary>
        /// Converts an array of color indexes to an array of pixels.
        /// </summary>
        /// <param name="src">The source array of color indexes.</param>
        /// <param name="palette">The palette to use.</param>
        /// <param name="index">The color index offset.</param>
        /// <returns></returns>
        public static int[] ColorsToPixels(int[] src, int[] palette, int index)
        {
            int[] pixels = new int[src.Length];
            for (int i = 0; i < src.Length; i++)
                if (src[i] < palette.Length && src[i] != 0)
                    pixels[i] = palette[src[i] + index];
            return pixels;
        }
        public static Bitmap CombineImages(Bitmap[] images, int maxwidth, int maxheight, int tilesize, bool padedges)
        {
            if (images.Length == 0)
                return null;
            // pad dimensions to multiples of tilesize
            Bitmap sheet = new Bitmap(maxwidth, maxheight);
            int rowheight = 0;
            for (int i = 0, x = 0, y = 0; i < images.Length; i++)
            {
                // if need to move to a new row
                if (images[i].Width + x >= maxwidth)
                {
                    x = 0;
                    y += rowheight;
                    if (padedges && y % tilesize != 0)
                        y += tilesize - (y % tilesize);
                }
                // raise the row's height if needed
                if (images[i].Height > rowheight)
                    rowheight = images[i].Height;
                // draw image to sheet
                Graphics temp = Graphics.FromImage(sheet);
                temp.DrawImage(images[i], x, y, Math.Min(256, images[i].Width), Math.Min(256, images[i].Height));
                //
                x += images[i].Width;
                if (padedges && x % tilesize != 0)
                    x += tilesize - (x % tilesize);
            }
            return sheet;
        }
        /// <summary>
        /// Copy a block of BPP graphics into a region in another block of BPP graphics.
        /// </summary>
        /// <param name="src">The source graphics to copy from.</param>
        /// <param name="dst">The destination graphics to copy to.</param>
        /// <param name="region">The region (in 8x8 tile units) in the destination graphics to draw to.</param>
        /// <param name="dstWidth">The width (in 8x8 tile units) of the destination graphics.</param>
        /// <param name="offset">The offset to start drawing at.</param>
        public static void CopyOverBPPGraphics(byte[] src, byte[] dst, Rectangle region, int dstWidth, int offset, byte format)
        {
            Point p;
            for (int b = 0; b < region.Height; b++)
            {
                for (int a = 0; a < region.Width; a++)
                {
                    p = new Point(region.X + a, region.Y + b);
                    for (int i = 0; i < format; i++)
                    {
                        if ((p.Y * dstWidth * format + (p.X * format) + i + offset) >= dst.Length)
                            continue;
                        if ((b * region.Width * format + (a * format) + i) >= src.Length)
                            continue;
                        dst[p.Y * dstWidth * format + (p.X * format) + i + offset] = src[b * region.Width * format + (a * format) + i];
                    }
                }
            }
        }
        /// <summary>
        /// Copy a block of 4bpp graphics into a tileset.
        /// </summary>
        /// <param name="src">The raw graphics to copy from.</param>
        /// <param name="tileset">The raw tileset to copy to.</param>
        /// <param name="palettes">The set of palettes to apply.</param>
        /// <param name="paletteIndexes">The palette index of each 8x8 tile in the graphics.</param>
        /// <param name="checkIfFlipped">Check if 8x8 tiles are mirrors or inversions of another, and cull the graphics accordingly.</param>
        /// <param name="priority1">Sets whether or not the tiles in the tileset will be priority 1.</param>
        /// <param name="tileSize">The tile size, either 0x10 or 0x20 (2bpp and 4bpp, respectively).</param>
        /// <param name="tileLength">Length, in bytes, of an 8x8 tile in a tileset. Either one or two.</param>
        /// <param name="tilesetSize">Size, in pixels, of the tileset being drawn to.</param>
        /// <param name="tileIndexStart">The index to start writing tilenums to the tileset. Normally 0, 1, or 2</param>
        public static void CopyToTileset(byte[] src, byte[] tileset, int[][] palettes, int[] paletteIndexes,
            bool checkIfFlipped, bool priority1, byte tileSize, byte tileLength, Size tilesetSize, int tileIndexStart)
        {
            ArrayList tiles_a = new ArrayList();    // the tileset, essentially, in array form
            ArrayList tiles_b = new ArrayList();    // used for redrawing a culled 4bpp graphic block
            ArrayList tiles_c = new ArrayList();
            for (int i = 0; i < src.Length / tileSize; i++)
            {
                Subtile temp = new Subtile(i, src, i * tileSize, palettes[paletteIndexes[i]], false, false, false, false);
                tiles_a.Add(temp);
                tiles_b.Add(temp);
                tiles_c.Add(temp);
            }
            // look through entire set of tiles for duplicates
            for (int a = 0; a < tiles_a.Count; a++)
            {
                Subtile tile_a = (Subtile)tiles_a[a];
                if (tile_a.Index != a) continue;  // skip if already set as duplicate
                for (int b = a; b < tiles_a.Count; b++)
                {
                    Subtile tile_b = (Subtile)tiles_a[b];
                    if (a == b) continue;   // cannot be duplicate of self
                    if (Bits.Compare(tile_a.Pixels, tile_b.Pixels)) // if a duplicate...
                    {
                        // first set the tile to the one that it's a duplicate of
                        tile_b.Index = a;
                        // then remove
                        tiles_b.Remove(tile_b);
                    }
                    if (checkIfFlipped)
                    {
                        byte status = GetFlippedStatus(tile_a.Pixels, tile_b.Pixels);
                        if ((status & 0x40) == 0x40)
                        {
                            tile_b.Mirror = true;
                            tile_b.Index = a;
                            tiles_b.Remove(tile_b);
                        }
                        if ((status & 0x80) == 0x80)
                        {
                            tile_b.Invert = true;
                            tile_b.Index = a;
                            tiles_b.Remove(tile_b);
                        }
                    }
                }
            }
            // redraw into newly culled graphic block, and reorganize tilenums
            int c = 0; byte[] culledGraphics = new byte[src.Length];
            foreach (Subtile tile in tiles_b)
            {
                int orig = tile.Index;
                Buffer.BlockCopy(src, tile.Index * tileSize, culledGraphics, c * tileSize, tileSize);
                tile.Index = c;
                // check for other duplicates or mirrors/inversions of this current tile
                foreach (Subtile check in tiles_a)
                {
                    if (check.Index == orig)
                        check.Index = c;
                }
                c++;
            }
            // now rewrite tileset data using tiles_a
            c = 0; byte[] culledTileset = new byte[tileset.Length];
            foreach (Subtile tile in tiles_a)
            {
                culledTileset[c * tileLength] = (byte)(tile.Index + tileIndexStart);
                if (tileLength == 2)
                {
                    culledTileset[c * tileLength + 1] = (byte)(paletteIndexes[c] << 2);    // set the palette index
                    culledTileset[c * tileLength + 1] |= (byte)(tile.Index >> 8); // set the graphic index
                    Bits.SetBit(culledTileset, c * tileLength + 1, 5, priority1);
                    Bits.SetBit(culledTileset, c * tileLength + 1, 6, tile.Mirror);
                    Bits.SetBit(culledTileset, c * tileLength + 1, 7, tile.Invert);
                }
                c++;
            }
            Buffer.BlockCopy(culledTileset, 0, tileset, 0, tileset.Length);
            Buffer.BlockCopy(culledGraphics, 0, src, 0, src.Length);
        }
        /// <summary>
        /// Copy a block of 4bpp graphics into a tileset and returns the final size of the imported graphics.
        /// </summary>
        /// <param name="src">The raw graphics to copy from.</param>
        /// <param name="tileset">The raw tileset to copy to.</param>
        /// <param name="palette">The single palette to apply.</param>
        /// <param name="paletteIndex">The universal palette index of all 8x8 tiles in the graphics.</param>
        /// <param name="checkIfFlipped">Check if 8x8 tiles are mirrors or inversions of another, and cull the graphics accordingly.</param>
        /// <param name="priority1">Sets whether or not the tiles in the tileset will be priority 1.</param>
        /// <param name="format">The format, either 0x10 or 0x20 (2bpp and 4bpp, respectively).</param>
        /// <param name="tileLength">Length, in bytes, of an 8x8 tile in a tileset. Either one or two.</param>
        /// <param name="tilesetSize">Size, in pixels, of the tileset being drawn to.</param>
        /// <param name="tileIndexStart">The index to start writing tilenums to the tileset. Normally 0, 1, or 2</param>
        /// <returns></returns>
        public static int CopyToTileset(byte[] src, byte[] tileset, int[] palette, int paletteIndex,
            bool checkIfFlipped, bool priority1, byte format, byte tileLength, Size tilesetSize, int tileIndexStart)
        {
            List<Subtile> tiles_a = new List<Subtile>();    // the tileset, essentially, in array form
            List<Subtile> tiles_b = new List<Subtile>();    // used for redrawing a culled 4bpp graphic block
            for (int i = 0; i < src.Length / format; i++)
            {
                Subtile temp = new Subtile(i, src, i * format, palette, false, false, false, format == 0x10);
                tiles_a.Add(temp);
                tiles_b.Add(temp);
            }
            // look through entire set of tiles for duplicates
            for (int a = 0; a < tiles_a.Count; a++)
            {
                Subtile tile_a = tiles_a[a];
                if (tile_a.Index != a) continue;  // skip if already set as duplicate
                for (int b = a; b < tiles_a.Count; b++)
                {
                    Subtile tile_b = tiles_a[b];
                    if (a == b) continue;   // cannot be duplicate of self
                    if (Bits.Compare(tile_a.Pixels, tile_b.Pixels)) // if a duplicate...
                    {
                        // first set the tile to the one that it's a duplicate of
                        tile_b.Index = a;
                        // then remove
                        tiles_b.Remove(tile_b);
                    }
                    if (checkIfFlipped) // if effect tileset, don't bother setting status
                    {
                        byte status = GetFlippedStatus(tile_a.Pixels, tile_b.Pixels);
                        if ((status & 0x40) == 0x40)
                        {
                            tile_b.Mirror = true;
                            tile_b.Index = a;
                            tiles_b.Remove(tile_b);
                        }
                        if ((status & 0x80) == 0x80)
                        {
                            tile_b.Invert = true;
                            tile_b.Index = a;
                            tiles_b.Remove(tile_b);
                        }
                    }
                }
            }
            // redraw into newly culled graphic block, and reorganize tilenums
            int c = 0; byte[] culledGraphics = new byte[src.Length];
            foreach (Subtile tile in tiles_b)
            {
                int orig = tile.Index;
                Buffer.BlockCopy(src, tile.Index * format, culledGraphics, c * format, format);
                tile.Index = c;
                // check for other duplicates or mirrors/inversions of this current tile
                foreach (Subtile check in tiles_a)
                {
                    if (check.Index == orig)
                        check.Index = c;
                }
                c++;
            }
            // now rewrite tileset data using tiles_a
            c = 0; byte[] culledTileset = new byte[tileset.Length];
            foreach (Subtile tile in tiles_a)
            {
                if (c * tileLength >= culledTileset.Length)
                {
                    MessageBox.Show(
                        "Imported graphics were too large to fit into the tileset.",
                        "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
                Bits.SetShort(culledTileset, c * tileLength, (ushort)(tile.Index + tileIndexStart));
                if (tileLength == 2)
                {
                    culledTileset[c * tileLength + 1] |= (byte)(paletteIndex << 2);    // set the palette index
                    culledTileset[c * tileLength + 1] |= (byte)(tile.Index >> 8); // set the graphic index
                    Bits.SetBit(culledTileset, c * tileLength + 1, 5, priority1);
                    Bits.SetBit(culledTileset, c * tileLength + 1, 6, tile.Mirror);
                    Bits.SetBit(culledTileset, c * tileLength + 1, 7, tile.Invert);
                }
                c++;
            }
            Buffer.BlockCopy(culledTileset, 0, tileset, 0, tileset.Length);
            Buffer.BlockCopy(culledGraphics, 0, src, 0, src.Length);
            return tiles_b.Count * format;
        }
        /// <summary>
        /// Crops a pixel array to the boundaries of the pixel edges and returns the newly cropped region.
        /// </summary>
        /// <param name="src">The source array.</param>
        /// <param name="width">The width of the source array.</param>
        /// <param name="height">The height of the source array.</param>
        /// <returns></returns>
        public static Rectangle Crop(int[] src, int width, int height)
        {
            int leftEdge = 0, bottomEdge = 0, rightEdge = 0, topEdge = 0;
            // find top edge
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                    if (src[y * width + x] != 0)
                    {
                        topEdge = y;
                        goto FindBottomEdge;
                    }
            }
        FindBottomEdge:
            // find bottom edge
            for (int y = height - 1; y >= 0; y--)
            {
                for (int x = 0; x < width; x++)
                    if (src[y * width + x] != 0)
                    {
                        bottomEdge = y;
                        goto FindLeftEdge;
                    }
            }
        FindLeftEdge:
            // find left edge
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                    if (src[y * width + x] != 0)
                    {
                        leftEdge = x;
                        goto FindRightEdge;
                    }
            }
        FindRightEdge:
            // find right edge
            for (int x = width - 1; x >= 0; x--)
            {
                for (int y = 0; y < height; y++)
                    if (src[y * width + x] != 0)
                    {
                        rightEdge = x;
                        goto Done;
                    }
            }
        Done:
            if (rightEdge - leftEdge <= 0 ||
                bottomEdge - topEdge <= 0)
                return new Rectangle(0, 0, 1, 1);
            else
                return new Rectangle(leftEdge, topEdge, rightEdge - leftEdge + 1, bottomEdge - topEdge + 1);
        }
        /// <summary>
        /// Crops a pixel array to the boundaries of the pixel edges and returns the newly cropped region.
        /// </summary>
        /// <param name="src">The source array.</param>
        /// <param name="dst">The array to write the cropped region to.</param>
        /// <param name="width">The width of the source array.</param>
        /// <param name="height">The height of the source array.</param>
        /// <returns></returns>
        public static Rectangle Crop(int[] src, out int[] dst, int width, int height, bool left, bool bottom, bool right, bool top)
        {
            int leftEdge = 0, bottomEdge = 0, rightEdge = 0, topEdge = 0;
            // find top edge
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                    if (src[y * width + x] != 0)
                    {
                        topEdge = y;
                        goto FindBottomEdge;
                    }
            }
        FindBottomEdge:
            // find bottom edge
            for (int y = height - 1; y >= 0; y--)
            {
                for (int x = 0; x < width; x++)
                    if (src[y * width + x] != 0)
                    {
                        bottomEdge = y;
                        goto FindLeftEdge;
                    }
            }
        FindLeftEdge:
            // find left edge
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                    if (src[y * width + x] != 0)
                    {
                        leftEdge = x;
                        goto FindRightEdge;
                    }
            }
        FindRightEdge:
            // find right edge
            for (int x = width - 1; x >= 0; x--)
            {
                for (int y = 0; y < height; y++)
                    if (src[y * width + x] != 0)
                    {
                        rightEdge = x;
                        goto Done;
                    }
            }
        Done:
            if (!top) topEdge = 0;
            if (!bottom) bottomEdge = height;
            if (!left) leftEdge = 0;
            if (!right) rightEdge = width;
            //
            if (rightEdge - leftEdge <= 0 ||
                bottomEdge - topEdge <= 0)
            {
                dst = new int[1];
                return new Rectangle(0, 0, 1, 1);
            }
            else
            {
                dst = GetPixelRegion(src, width, height,
                    rightEdge - leftEdge + 1, bottomEdge - topEdge + 1, leftEdge, topEdge);
                return new Rectangle(leftEdge, topEdge, rightEdge - leftEdge + 1, bottomEdge - topEdge + 1);
            }
        }
        public static Rectangle Crop(int[] src, out int[] dst, int width, int height)
        {
            return Crop(src, out dst, width, height, true, true, true, true);
        }
        /// <summary>
        /// Crop several pixel arrays to the largest boundary of all arrays and returns the boundary.
        /// Assumes that all source arrays are the same width and height.
        /// </summary>
        /// <param name="src">The source array.</param>
        /// <param name="dst">The array to write the cropped region to.</param>
        /// <param name="width">The width of the source array.</param>
        /// <param name="height">The height of the source array.</param>
        /// <returns></returns>
        public static Rectangle Crop(int[][] src, out int[][] dst, int width, int height)
        {
            dst = new int[src.Length][];
            int leftEdge = 0, bottomEdge = 0, rightEdge = 0, topEdge = 0;
            for (int i = 0; i < src.Length; i++)
            {
                // find top edge
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                        if (src[i][y * width + x] != 0)
                        {
                            if (y < topEdge)
                                topEdge = y;
                            goto FindBottomEdge;
                        }
                }
            FindBottomEdge:
                // find bottom edge
                for (int y = height - 1; y >= 0; y--)
                {
                    for (int x = 0; x < width; x++)
                        if (src[i][y * width + x] != 0)
                        {
                            if (y > bottomEdge)
                                bottomEdge = y;
                            goto FindLeftEdge;
                        }
                }
            FindLeftEdge:
                // find left edge
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                        if (src[i][y * width + x] != 0)
                        {
                            if (x < leftEdge)
                                leftEdge = x;
                            goto FindRightEdge;
                        }
                }
            FindRightEdge:
                // find right edge
                for (int x = width - 1; x >= 0; x--)
                {
                    for (int y = 0; y < height; y++)
                        if (src[i][y * width + x] != 0)
                        {
                            if (x > rightEdge)
                                rightEdge = x;
                            goto Done;
                        }
                }
            Done:
                continue;
            }
            for (int i = 0; i < dst.Length; i++)
            {
                if (rightEdge - leftEdge <= 0 || bottomEdge - topEdge <= 0)
                    dst[i] = new int[1];
                else
                    dst[i] = GetPixelRegion(src[i], width, height,
                        rightEdge - leftEdge + 1, bottomEdge - topEdge + 1, leftEdge, topEdge);
            }
            if (rightEdge - leftEdge <= 0 || bottomEdge - topEdge <= 0)
                return new Rectangle(0, 0, 1, 1);
            else
                return new Rectangle(leftEdge, topEdge, rightEdge - leftEdge + 1, bottomEdge - topEdge + 1);
        }
        /// <summary>
        /// Crops a tilemap, based on an empty tile index.
        /// </summary>
        /// <param name="src">The tilemap to crop.</param>
        /// <param name="width">The width, in 16x16 tiles, of the tilemap.</param>
        /// <param name="height">The height, in 16x16 tiles, of the tilemap.</param>
        /// <param name="emptyTileIndex">The index of the empty tile. Either 0xFF (used by effects) or 0.</param>
        /// <returns></returns>
        public static Rectangle Crop(byte[] src, int width, int height, byte emptyTileIndex)
        {
            int leftEdge = 0, bottomEdge = 0, rightEdge = 0, topEdge = 0;
            // find top edge
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                    if (src[y * width + x] != emptyTileIndex)
                    {
                        topEdge = y;
                        goto FindBottomEdge;
                    }
            }
        FindBottomEdge:
            // find bottom edge
            for (int y = height - 1; y >= 0; y--)
            {
                for (int x = 0; x < width; x++)
                    if (src[y * width + x] != emptyTileIndex)
                    {
                        bottomEdge = y;
                        goto FindLeftEdge;
                    }
            }
        FindLeftEdge:
            // find left edge
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                    if (src[y * width + x] != emptyTileIndex)
                    {
                        leftEdge = x;
                        goto FindRightEdge;
                    }
            }
        FindRightEdge:
            // find right edge
            for (int x = width - 1; x >= 0; x--)
            {
                for (int y = 0; y < height; y++)
                    if (src[y * width + x] != emptyTileIndex)
                    {
                        rightEdge = x;
                        goto Done;
                    }
            }
        Done:
            if (rightEdge - leftEdge <= 0 ||
                bottomEdge - topEdge <= 0)
            {
                for (int i = 0; i < src.Length; i++)
                    src[i] = emptyTileIndex;
                return new Rectangle(0, 0, 1, 1);
            }
            else
            {
                byte[] temp = new byte[src.Length]; src.CopyTo(temp, 0); Bits.Fill(src, emptyTileIndex);
                Rectangle region = new Rectangle(leftEdge, topEdge, (rightEdge - leftEdge) + 1, (bottomEdge - topEdge) + 1);
                for (int y = 0, y_ = region.Top; y <= region.Height && y_ < region.Bottom; y++, y_++)
                {
                    for (int x = 0, x_ = region.Left; x <= region.Width && x_ < region.Right; x++, x_++)
                        src[y * region.Width + x] = temp[y_ * width + x_];
                }
                return region;
            }
        }
        public static byte[] CullGraphics(byte[] src, int[] palette, byte format, bool checkIfFlipped)
        {
            List<Subtile> tiles_a = new List<Subtile>();    // the tileset, essentially, in array form
            List<Subtile> tiles_b = new List<Subtile>();    // used for redrawing a culled 4bpp graphic block
            for (int i = 0; i < src.Length / format; i++)
            {
                Subtile temp = new Subtile(i, src, i * format, palette, false, false, false, format == 0x10);
                tiles_a.Add(temp);
                tiles_b.Add(temp);
            }
            // look through entire set of tiles for duplicates
            for (int a = 0; a < tiles_a.Count; a++)
            {
                Subtile tile_a = tiles_a[a];
                if (tile_a.Index != a) continue;  // skip if already set as duplicate
                for (int b = a; b < tiles_a.Count; b++)
                {
                    Subtile tile_b = tiles_a[b];
                    // always remove if empty
                    if (Bits.Empty(tile_b.Pixels))
                        tiles_b.Remove(tile_b);
                    // cannot be duplicate of self
                    if (a == b)
                        continue;
                    // if a duplicate...
                    if (Bits.Compare(tile_a.Pixels, tile_b.Pixels))
                    {
                        // first set the tile to the one that it's a duplicate of
                        tile_b.Index = a;
                        // then remove
                        tiles_b.Remove(tile_b);
                    }
                    // if effect tileset, don't bother setting status
                    if (checkIfFlipped)
                    {
                        byte status = GetFlippedStatus(tile_a.Pixels, tile_b.Pixels);
                        if ((status & 0x40) == 0x40)
                        {
                            tile_b.Mirror = true;
                            tile_b.Index = a;
                            tiles_b.Remove(tile_b);
                        }
                        if ((status & 0x80) == 0x80)
                        {
                            tile_b.Invert = true;
                            tile_b.Index = a;
                            tiles_b.Remove(tile_b);
                        }
                    }
                }
            }
            // redraw into newly culled graphic block, and reorganize tilenums
            int c = 0; byte[] culledGraphics = new byte[src.Length];
            foreach (Subtile tile in tiles_b)
            {
                int orig = tile.Index;
                Buffer.BlockCopy(src, tile.Index * format, culledGraphics, c * format, format);
                tile.Index = c;
                // check for other duplicates or mirrors/inversions of this current tile
                foreach (Subtile check in tiles_a)
                {
                    if (check.Index == orig)
                        check.Index = c;
                }
                c++;
            }
            Array.Resize<byte>(ref culledGraphics, tiles_b.Count * format);
            return culledGraphics;
        }
        /// <summary>
        /// Reorder indexes of tiles in a tileset based on duplicates.
        /// </summary>
        /// <param name="tileset">The raw tileset to reduce the size of.</param>
        public static void CullTileset(ref Tile[] tileset)
        {
            // set duplicate tiles to originals
            ArrayList tilesetTiles = new ArrayList();
            foreach (Tile tile in tileset)
            {
                int contains = Contains(tile, tileset);
                if (tile.Index == contains)
                    tilesetTiles.Add(tile);
                else
                    tile.Index = contains;
            }
            // renumber tile indexes
            int c = 0;
            foreach (Tile tile in tilesetTiles)
                tile.Index = c++;
            // finally cull the original tileset
            c = 0;
            foreach (Tile tile in tilesetTiles)
                tileset[c++] = tile;
            Array.Resize<Tile>(ref tileset, c);
        }
        /// <summary>
        /// Reorder indexes of tiles in a tileset based on duplicates and draws to a tilemap.
        /// </summary>
        /// <param name="tileset">The raw tileset to reduce the size of.</param>
        /// <param name="tilemap">The tilemap to draw to.</param>
        /// <param name="width">The width, in 16x16 tiles, of the tilemap.</param>
        /// <param name="height">The height, in 16x16 tiles, of the tilemap.</param>
        public static void CullTileset(Tile[] tileset, byte[] tilemap, int width, int height)
        {
            // set duplicate tiles to originals
            ArrayList tilesetTiles = new ArrayList();
            foreach (Tile tile in tileset)
            {
                int contains = Contains(tile, tileset);
                if (Bits.Empty(tile.Pixels))
                    tile.Index = 0xFF;
                else if (tile.Index == contains)
                    tilesetTiles.Add(tile);
                else
                    tile.Index = contains;
            }
            // renumber tile indexes
            int c = 0;
            foreach (Tile tile in tilesetTiles)
                tile.Index = c++;
            // draw to tilemap with culled indexes
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (y * width + x >= tileset.Length ||
                        y * width + x >= tilemap.Length)
                        continue;
                    tilemap[y * width + x] = (byte)tileset[y * width + x].Index;
                }
            }
            // finally cull the original tileset
            c = 0;
            foreach (Tile tile in tilesetTiles)
                tileset[c++] = tile;
            while (c < tileset.Length)
                tileset[c++] = new Tile(c);
        }
        /// <summary>
        /// Draws an overworld menu frame to a pixel array.
        /// </summary>
        /// <param name="dst">The pixel array to draw to.</param>
        /// <param name="dstWidth">The width of the pixel array.</param>
        /// <param name="r">The location (in pixels) and size (in 8x8 tiles) of the frame.</param>
        /// <returns></returns>
        public static void DrawMenuFrame(int[] dst, int dstWidth, Rectangle r, byte[] graphics, int[] palette)
        {
            // set tileset
            Subtile[] frameTileset = new Subtile[16 * 2];
            int[] framePalette = Bits.GetInts(palette, 12, 4);
            for (int i = 0; i < frameTileset.Length; i++)
                frameTileset[i] = new Subtile(i, graphics, i * 0x10, framePalette, false, false, false, true);
            // draw tiles to pixels
            for (int x = 2; x < r.Width - 2; x++)
            {
                if (x * 8 + r.X < dstWidth)
                {
                    PixelsToPixels(frameTileset[0x02].Pixels, dst, dstWidth, new Rectangle(x * 8 + r.X, r.Y, 8, 8), true, true);
                    PixelsToPixels(frameTileset[0x19].Pixels, dst, dstWidth, new Rectangle(x * 8 + r.X, (r.Height - 1) * 8 + r.Y, 8, 8), true, true);
                }
            }
            for (int y = 2; y < r.Height - 3; y++)
            {
                PixelsToPixels(frameTileset[0x05].Pixels, dst, dstWidth, new Rectangle(r.X, y * 8 + r.Y, 8, 8), true, true);
                if ((r.Width - 1) * 8 + r.X < dstWidth)
                    PixelsToPixels(frameTileset[0x06].Pixels, dst, dstWidth, new Rectangle((r.Width - 1) * 8 + r.X, y * 8 + r.Y, 8, 8), true, true);
            }
            // top-left corner
            PixelsToPixels(frameTileset[0x00].Pixels, dst, dstWidth, new Rectangle(r.X, r.Y, 8, 8), true, true);
            PixelsToPixels(frameTileset[0x01].Pixels, dst, dstWidth, new Rectangle(r.X + 8, r.Y, 8, 8), true, true);
            PixelsToPixels(frameTileset[0x10].Pixels, dst, dstWidth, new Rectangle(r.X, r.Y + 8, 8, 8), true, true);
            // top-right corner
            if ((r.Width - 2) * 8 + r.X < dstWidth)
                PixelsToPixels(frameTileset[0x03].Pixels, dst, dstWidth, new Rectangle((r.Width - 2) * 8 + r.X, r.Y, 8, 8), true, true);
            if ((r.Width - 1) * 8 + r.X < dstWidth)
            {
                PixelsToPixels(frameTileset[0x04].Pixels, dst, dstWidth, new Rectangle((r.Width - 1) * 8 + r.X, r.Y, 8, 8), true, true);
                PixelsToPixels(frameTileset[0x14].Pixels, dst, dstWidth, new Rectangle((r.Width - 1) * 8 + r.X, r.Y + 8, 8, 8), true, true);
            }
            // bottom-left corner
            if (r.Height > 3)
                PixelsToPixels(frameTileset[0x15].Pixels, dst, dstWidth, new Rectangle(r.X, (r.Height - 3) * 8 + r.Y, 8, 8), true, true);
            PixelsToPixels(frameTileset[0x07].Pixels, dst, dstWidth, new Rectangle(r.X, (r.Height - 2) * 8 + r.Y, 8, 8), true, true);
            PixelsToPixels(frameTileset[0x17].Pixels, dst, dstWidth, new Rectangle(r.X, (r.Height - 1) * 8 + r.Y, 8, 8), true, true);
            PixelsToPixels(frameTileset[0x18].Pixels, dst, dstWidth, new Rectangle(r.X + 8, (r.Height - 1) * 8 + r.Y, 8, 8), true, true);
            // bottom-right corner
            if ((r.Width - 1) * 8 + r.X < dstWidth)
            {
                if (r.Height > 3)
                    PixelsToPixels(frameTileset[0x16].Pixels, dst, dstWidth, new Rectangle((r.Width - 1) * 8 + r.X, (r.Height - 3) * 8 + r.Y, 8, 8), true, true);
                PixelsToPixels(frameTileset[0x0B].Pixels, dst, dstWidth, new Rectangle((r.Width - 1) * 8 + r.X, (r.Height - 2) * 8 + r.Y, 8, 8), true, true);
                PixelsToPixels(frameTileset[0x1B].Pixels, dst, dstWidth, new Rectangle((r.Width - 1) * 8 + r.X, (r.Height - 1) * 8 + r.Y, 8, 8), true, true);
            }
            if ((r.Width - 2) * 8 + r.X < dstWidth)
                PixelsToPixels(frameTileset[0x1A].Pixels, dst, dstWidth, new Rectangle((r.Width - 2) * 8 + r.X, (r.Height - 1) * 8 + r.Y, 8, 8), true, true);
        }
        public static int[] DrawMenuFrame(Size dstSize, byte[] graphics, int[] palette)
        {
            int[] dst = new int[(dstSize.Width * 8) * (dstSize.Height * 8)];
            DrawMenuFrame(dst, dstSize.Width * 8, new Rectangle(new Point(0, 0), dstSize), graphics, palette);
            return dst;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tiles"></param>
        /// <param name="g"></param>
        /// <param name="region">The location (in pixels) and size (in 8x8 tiles) of the menu frame.</param>
        public static void DrawMenuFrame(Bitmap[] tiles, Graphics g, int x, int y, int width, int height)
        {
            width *= 8; height *= 8;
            // upper-left corner
            g.DrawImage(tiles[0 * 5 + 0], 0 + x, 0 + y, 8, 8);
            g.DrawImage(tiles[0 * 5 + 1], 8 + x, 0 + y, 8, 8);
            g.DrawImage(tiles[1 * 5 + 0], 0 + x, 8 + y, 8, 8);
            // upper-right corner
            g.DrawImage(tiles[0 * 5 + 3], width - 16 + x, 0 + y, 8, 8);
            g.DrawImage(tiles[0 * 5 + 4], width - 8 + x, 0 + y, 8, 8);
            g.DrawImage(tiles[1 * 5 + 4], width - 8 + x, 8 + y, 8, 8);
            // lower-left corner
            g.DrawImage(tiles[3 * 5 + 0], 0 + x, height - 16 + y, 8, 8);
            g.DrawImage(tiles[4 * 5 + 0], 0 + x, height - 8 + y, 8, 8);
            g.DrawImage(tiles[4 * 5 + 1], 8 + x, height - 8 + y, 8, 8);
            // lower-right corner
            g.DrawImage(tiles[3 * 5 + 4], width - 8 + x, height - 16 + y, 8, 8);
            g.DrawImage(tiles[4 * 5 + 4], width - 8 + x, height - 8 + y, 8, 8);
            g.DrawImage(tiles[4 * 5 + 3], width - 16 + x, height - 8 + y, 8, 8);
            // tiles between
            for (int a = 2; a < (width / 8) - 2; a++)
            {
                g.DrawImage(tiles[0 * 5 + 2], a * 8 + x, y);
                g.DrawImage(tiles[4 * 5 + 2], a * 8 + x, height - 8 + y);
            }
            for (int b = 2; b < (height / 8) - 2; b++)
            {
                g.DrawImage(tiles[2 * 5 + 0], x, b * 8 + y);
                g.DrawImage(tiles[2 * 5 + 4], width - 8 + x, b * 8 + y);
            }
        }
        /// <summary>
        /// Creates an 8x8 tile class.
        /// </summary>
        /// <param name="num">The tile's number or index.</param>
        /// <param name="status">The tile's status or properties.</param>
        /// <param name="graphics">The graphics to draw to the tile from.</param>
        /// <param name="palettes">The palette set used when drawing the tile.</param>
        /// <param name="tileSize">The byte size of the tile. Either 0x10 or 0x20 (2bpp and 4bpp, respectively).</param>
        /// <param name="paletteIndexOffset">The palette index to start reading at.</param>
        /// <returns></returns>
        public static Subtile DrawSubtile(ushort num, byte status, byte[] graphics, int[][] palettes, byte tileSize)
        {
            byte paletteIndex = (byte)((status >> 2) & 0x07);
            if (paletteIndex >= palettes.Length) paletteIndex = 0;
            bool priorityOne = (status & 0x20) == 0x20;
            bool mirrored = (status & 0x40) == 0x40;
            bool inverted = (status & 0x80) == 0x80;
            bool twobpp = tileSize == 0x10;
            int offset = num * tileSize;
            if (offset >= graphics.Length) offset = 0;
            int[] palette;
            if (tileSize == 0x10)
            {
                palette = new int[4];
                for (int i = 0; i < 4; i++)
                    palette[i] = palettes[paletteIndex / 4][((paletteIndex % 4) * 4) + i];
            }
            else
                palette = palettes[paletteIndex];
            Subtile tile = new Subtile(num, graphics, offset, palette, mirrored, inverted, priorityOne, twobpp);
            tile.Palette = paletteIndex;
            return tile;
        }
        /// <summary>
        /// Creates an 8x8 tile class.
        /// </summary>
        /// <param name="num">The tile's number or index.</param>
        /// <param name="status">The tile's status or properties.</param>
        /// <param name="graphics">The graphics to draw to the tile from.</param>
        /// <param name="palette">The palette used when drawing the tile.</param>
        /// <param name="tileSize">The byte size of the tile. Either 0x10 or 0x20 (2bpp and 4bpp, respectively).</param>
        /// <param name="paletteIndexOffset">The palette index to start reading at.</param>
        /// <returns></returns>
        public static Subtile DrawSubtile(ushort num, byte status, byte[] graphics, int[] palette, byte tileSize)
        {
            byte paletteIndex = (byte)((status >> 2) & 0x07);
            bool priorityOne = (status & 0x20) == 0x20;
            bool mirrored = (status & 0x40) == 0x40;
            bool inverted = (status & 0x80) == 0x80;
            bool twobpp = tileSize == 0x10;
            int offset = num * tileSize;
            if (offset >= graphics.Length) offset = 0;
            Subtile tile = new Subtile(num, graphics, offset, palette, mirrored, inverted, priorityOne, twobpp);
            tile.Palette = paletteIndex;
            return tile;
        }
        /// <summary>
        /// Creates an 8x8 tile class.
        /// </summary>
        /// <param name="num">The tile's number or index.</param>
        /// <param name="status">The tile's status or properties.</param>
        /// <param name="graphics">The graphics to draw to the tile from.</param>
        /// <param name="palettes">The palette set used when drawing the tile.</param>
        /// <param name="tileSize">The byte size of the tile. Either 0x10 or 0x20 (2bpp and 4bpp, respectively).</param>
        /// <param name="paletteIndexOffset">The palette index to start reading at.</param>
        /// <returns></returns>
        public static Subtile DrawSubtile(ushort num, byte paletteIndex, bool priorityOne, bool mirrored, bool inverted, byte[] graphics, int[][] palettes, byte tileSize)
        {
            if (paletteIndex >= palettes.Length)
                paletteIndex = 0;
            bool twobpp = tileSize == 0x10;
            int offset = num * tileSize;
            if (offset >= graphics.Length)
                offset = 0;
            int[] palette;
            if (tileSize == 0x10)
            {
                palette = new int[4];
                for (int i = 0; i < 4; i++)
                    palette[i] = palettes[paletteIndex / 4][((paletteIndex % 4) * 4) + i];
            }
            else
                palette = palettes[paletteIndex];
            Subtile tile = new Subtile(num, graphics, offset, palette, mirrored, inverted, priorityOne, twobpp);
            tile.Palette = paletteIndex;
            return tile;
        }
        /// <summary>
        /// Creates an 8x8 tile class.
        /// </summary>
        /// <param name="num">The tile's number or index.</param>
        /// <param name="status">The tile's status or properties.</param>
        /// <param name="graphics">The graphics to draw to the tile from.</param>
        /// <param name="palette">The palette used when drawing the tile.</param>
        /// <param name="tileSize">The byte size of the tile. Either 0x10 or 0x20 (2bpp and 4bpp, respectively).</param>
        /// <param name="paletteIndexOffset">The palette index to start reading at.</param>
        /// <returns></returns>
        public static Subtile DrawSubtile(ushort num, byte paletteIndex, bool priorityOne, bool mirrored, bool inverted, byte[] graphics, int[] palette, byte tileSize)
        {
            bool twobpp = tileSize == 0x10;
            int offset = num * tileSize;
            if (offset >= graphics.Length) offset = 0;
            Subtile tile = new Subtile(num, graphics, offset, palette, mirrored, inverted, priorityOne, twobpp);
            tile.Palette = paletteIndex;
            return tile;
        }
        public static Subtile DrawSubtileM7(ushort num, byte paletteIndex, byte[] graphics, int[][] palettes, byte tileSize)
        {
            if (paletteIndex >= palettes.Length) paletteIndex = 0;
            int offset = num * tileSize;
            if (offset >= graphics.Length) offset = 0;
            int[] palette;
            if (tileSize == 0x10)
            {
                palette = new int[4];
                for (int i = 0; i < 4; i++)
                    palette[i] = palettes[paletteIndex / 4][((paletteIndex % 4) * 4) + i];
            }
            else
                palette = palettes[paletteIndex];
            Subtile tile = new Subtile(num, graphics, offset, palette);
            tile.Palette = paletteIndex;
            return tile;
        }
        /// <summary>
        /// Modify a single pixel in a block of 2bpp or 4bpp graphics.
        /// Returns the color index of the new pixel, whether changed or unchanged.
        /// </summary>
        /// <param name="src">The graphics to modify.</param>
        /// <param name="srcOffset">The initial offset of the graphics to modify.</param>
        /// <param name="palette">The palette used by the graphics.</param>
        /// <param name="graphics">The picture box's graphics to paint to.</param>
        /// <param name="zoom">The current zoom level of the graphics image.</param>
        /// <param name="action">The type of modification: erase, draw, or select.</param>
        /// <param name="x">The X coord, in pixels, of the pixel to modify.</param>
        /// <param name="y">The Y coord, in pixels, of the pixel to modify.</param>
        /// <param name="offset">The base tile index to start from. Determined by current graphic set index.</param>
        /// <param name="color">The color index in the palette assigned to the pixel.</param>
        /// <param name="width">The width,in 8x8 tile units, of the graphic block.</param>
        /// <param name="height">The height,in 8x8 tile units, of the graphic block.</param>
        /// <param name="format">The format for 2bpp or 4bpp, 0x10 or 0x20, respectively.</param>
        public static int EditPixelBPP(byte[] src, int srcOffset, int[] palette, Graphics graphics, int zoom, Drawing action,
            int x, int y, int index, int color, int width, int height, byte format)
        {
            return EditPixelBPP(src, srcOffset, palette, graphics, zoom,
                action, x, y, index, color, color, width, height, format);
        }
        public static int EditPixelBPP(byte[] src, int srcOffset, int[] palette, Graphics graphics, int zoom, Drawing action,
            int x, int y, int index, int color, int colorBack, int width, int height, byte format)
        {
            if (x < 0 || x >= (width * 8) * zoom ||
                y < 0 || y >= (height * 8) * zoom ||
                action == Drawing.None)
                return color;
            if (srcOffset < 0 || index < 0)
                return color;
            //
            int bit = 0;
            int offset = GetBPPOffset(x, y, srcOffset, index, zoom, format, ref bit, width);
            if (format == 0x20 && offset + 17 >= src.Length)
                return color;
            if (format == 0x10 && offset + 1 >= src.Length)
                return color;
            if (format == 0x21 && offset + 17 >= src.Length)
                return color;
            Rectangle c;
            switch (action)
            {
                case Drawing.Draw:
                    SetBPPColor(src, x, y, srcOffset, index, zoom, format, color, width);
                    break;
                case Drawing.Erase:
                    SetBPPColor(src, x, y, srcOffset, index, zoom, format, 0, width);
                    break;
                case Drawing.Dropper:
                    color = GetBPPColor(src, x, y, srcOffset, index, zoom, format, width);
                    break;
                case Drawing.ReplaceColor:
                    int selectColor = GetBPPColor(src, x, y, srcOffset, index, zoom, format, width);
                    // if pixel not color to replace, return
                    if (selectColor != colorBack)
                        return color;
                    c = new Rectangle(x / zoom * zoom, y / zoom * zoom, zoom, zoom);
                    if (graphics != null)
                        graphics.FillRectangle(new SolidBrush(Color.FromArgb(palette[color])), c);
                    SetBPPColor(src, x, y, srcOffset, index, zoom, format, color, width);
                    break;
                case Drawing.Fill:
                    int fillColor = color;
                    color = GetBPPColor(src, x, y, srcOffset, index, zoom, format, width);
                    if (color == fillColor) return color;
                    Fill(src, color, fillColor, x, y, width, height, "", srcOffset, index, zoom, format);
                    break;
                case Drawing.FillAll:
                    fillColor = color;
                    color = GetBPPColor(src, x, y, srcOffset, index, zoom, format, width);
                    if (color == fillColor) return color;
                    for (int b = 0; b < height * 8; b++)
                    {
                        for (int a = 0; a < width * 8; a++)
                        {
                            int seeColor = GetBPPColor(src, a, b, srcOffset, index, 1, format, width);
                            // if fillable, fill pixel and create spawn travelling west
                            if (seeColor == color)
                                SetBPPColor(src, a, b, srcOffset, index, 1, format, fillColor, width);
                        }
                    }
                    break;
            }
            return color;
        }
        public static void Fill(int[][] src, int layer, bool chkall, int value, int[] fillValues, int x, int y, int width, int height, int vwidth, int vheight, string dir)
        {
            // first, fill this/these tile(s)
            int[] otherlayers;
            if (layer == 0)
                otherlayers = new int[] { 1, 2 };
            else if (layer == 1)
                otherlayers = new int[] { 0, 2 };
            else
                otherlayers = new int[] { 0, 1 };
            int a = 0;
            int b = 0;
            for (b = 0; b < vheight && y + b < height; b++)
            {
                if (src[layer][(y + b) * width + x] != value)
                    break;
                if (chkall &&
                    (src[otherlayers[0]][(y + b) * width + x] != 0 ||
                     src[otherlayers[1]][(y + b) * width + x] != 0))
                    break;
                for (a = 0; a < vwidth && x + a < width; a++)
                {
                    if (src[layer][(y + b) * width + x + a] != value)
                        break;
                    if (chkall &&
                        (src[otherlayers[0]][(y + b) * width + x] != 0 ||
                         src[otherlayers[1]][(y + b) * width + x] != 0))
                        break;
                    src[layer][(y + b) * width + x + a] = fillValues[b * vwidth + a];
                }
            }
            // look WEST, if not travelling east or at boundary
            if (dir != "east" && x - vwidth + 1 > 0)
            {
                // see what tile is to the west
                bool[] fillable = new bool[] { true, true, true };
                for (int l = 0; l < 3; l++)
                {
                    if (src[l] == null)
                        continue;
                    for (int c = 0; c < vwidth && x - c > 0; c++)
                        if (l == layer && src[l][y * width + x - c - 1] != value)
                            fillable[l] = false;
                        else if (l != layer && src[l][y * width + x - c - 1] != 0)
                            fillable[l] = false;
                }
                // if fillable, fill tile and create spawn travelling west
                if (fillable[layer])
                    if (!chkall)
                        Fill(src, layer, chkall, value, fillValues, x - vwidth, y, width, height, vwidth, vheight, "west");
                    else if (fillable[otherlayers[0]] && fillable[otherlayers[1]])
                        Fill(src, layer, chkall, value, fillValues, x - vwidth, y, width, height, vwidth, vheight, "west");
            }
            //  look EAST, if not travelling west or at boundary, and at least 1st row all fillable
            if (dir != "west" && x < width - vwidth && a == vwidth)
            {
                // see what color is to the east
                bool[] fillable = new bool[] { true, true, true };
                for (int l = 0; l < 3; l++)
                {
                    if (src[l] == null)
                        continue;
                    for (int c = 0; c < vwidth && x + c < width - vwidth; c++)
                        if (l == layer && src[l][y * width + x + c + vwidth] != value)
                            fillable[l] = false;
                        else if (l != layer && src[l][y * width + x + c + vwidth] != 0)
                            fillable[l] = false;
                }
                // if fillable, fill pixel and create spawn travelling east
                if (fillable[layer])
                    if (!chkall)
                        Fill(src, layer, chkall, value, fillValues, x + vwidth, y, width, height, vwidth, vheight, "east");
                    else if (fillable[otherlayers[0]] && fillable[otherlayers[1]])
                        Fill(src, layer, chkall, value, fillValues, x + vwidth, y, width, height, vwidth, vheight, "east");
            }
            //  look NORTH, if not travelling south or at boundary
            if (dir != "south" && y - vheight + 1 > 0)
            {
                // see what color is to the north
                bool[] fillable = new bool[] { true, true, true };
                for (int l = 0; l < 3; l++)
                {
                    if (src[l] == null)
                        continue;
                    for (int d = 0; d < vheight && y - d > 0; d++)
                        if (l == layer && src[l][(y - d - 1) * width + x] != value)
                            fillable[l] = false;
                        else if (l != layer && src[l][(y - d - 1) * width + x] != 0)
                            fillable[l] = false;
                }
                // if fillable, fill pixel and create spawn travelling north
                if (fillable[layer])
                    if (!chkall)
                        Fill(src, layer, chkall, value, fillValues, x, y - vheight, width, height, vwidth, vheight, "north");
                    else if (fillable[otherlayers[0]] && fillable[otherlayers[1]])
                        Fill(src, layer, chkall, value, fillValues, x, y - vheight, width, height, vwidth, vheight, "north");
            }
            //  look SOUTH, if not travelling north or at boundary, and at least 1st column all fillable
            if (dir != "north" && y < height - vheight && b == vheight)
            {
                // see what color is to the south
                bool[] fillable = new bool[] { true, true, true };
                for (int l = 0; l < 3; l++)
                {
                    if (src[l] == null)
                        continue;
                    for (int d = 0; d < vheight && y + d < height - vheight; d++)
                        if (l == layer && src[l][(y + d + vheight) * width + x] != value)
                            fillable[l] = false;
                        else if (l != layer && src[l][(y + d + vheight) * width + x] != 0)
                            fillable[l] = false;
                }
                // if fillable, fill pixel and create spawn travelling south
                if (fillable[layer])
                    if (!chkall)
                        Fill(src, layer, chkall, value, fillValues, x, y + vheight, width, height, vwidth, vheight, "south");
                    else if (fillable[otherlayers[0]] && fillable[otherlayers[1]])
                        Fill(src, layer, chkall, value, fillValues, x, y + vheight, width, height, vwidth, vheight, "south");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="src">BPP graphics</param>
        /// <param name="color">Color to fill</param>
        /// <param name="fillColor">Color to fill with</param>
        /// <param name="x">X coord to start travelling from</param>
        /// <param name="y">Y coord to start travelling from</param>
        /// <param name="dir">Direction travelling from</param>
        private static void Fill(byte[] src, int color, int fillColor, int x, int y, int width, int height,
            string dir, int srcOffset, int index, int zoom, byte format)
        {
            // the color seen when looking in a direction
            int seeColor = 0;
            // first, fill this pixel
            SetBPPColor(src, x, y, srcOffset, index, zoom, format, fillColor, width);
            // look WEST, if not travelling east or at boundary
            if (dir != "east" && x / zoom > 0)
            {
                // see what color is to the west
                seeColor = GetBPPColor(src, x - (1 * zoom), y, srcOffset, index, zoom, format, width);
                // if fillable, fill pixel and create spawn travelling west
                if (seeColor == color)
                    Fill(src, color, fillColor, x - (1 * zoom), y, width, height, "west", srcOffset, index, zoom, format);
            }
            //  look EAST, if not travelling west or at boundary
            if (dir != "west" && x < (width * 8 * zoom) - (1 * zoom))
            {
                // see what color is to the east
                seeColor = GetBPPColor(src, x + (1 * zoom), y, srcOffset, index, zoom, format, width);
                // if fillable, fill pixel and create spawn travelling east
                if (seeColor == color)
                    Fill(src, color, fillColor, x + (1 * zoom), y, width, height, "east", srcOffset, index, zoom, format);
            }
            //  look NORTH, if not travelling south or at boundary
            if (dir != "south" && y / zoom > 0)
            {
                // see what color is to the north
                seeColor = GetBPPColor(src, x, y - (1 * zoom), srcOffset, index, zoom, format, width);
                // if fillable, fill pixel and create spawn travelling north
                if (seeColor == color)
                    Fill(src, color, fillColor, x, y - (1 * zoom), width, height, "north", srcOffset, index, zoom, format);
            }
            //  look SOUTH, if not travelling north or at boundary
            if (dir != "north" && y < (height * 8 * zoom) - (1 * zoom))
            {
                // see what color is to the south
                seeColor = GetBPPColor(src, x, y + (1 * zoom), srcOffset, index, zoom, format, width);
                // if fillable, fill pixel and create spawn travelling south
                if (seeColor == color)
                    Fill(src, color, fillColor, x, y + (1 * zoom), width, height, "south", srcOffset, index, zoom, format);
            }
        }
        private static int GetBPPOffset(int x, int y, int srcOffset, int index, int zoom, byte format, ref int bit, int width)
        {
            int offset = (y / (8 * zoom)) * width + (x / (8 * zoom));
            if ((format & 0x01) != 0x01)
            {
                byte row = (byte)(y / zoom % 8);
                byte col = (byte)(x / zoom % 8);
                bit = (byte)(col ^ 7);
                // for font dialogue characters only
                if (srcOffset == 0x18)
                    x += (8 * zoom);
                if (index == 1)
                    y += (8 * zoom);
                //
                offset *= format;
                offset += row * 2;
                offset += index * format;
                offset += srcOffset;
            }
            else
            {
                format &= 0xFE;
                offset *= format;
                offset += ((y / zoom) % 8) * 4 + ((x / zoom) % 8 / 2);
            }
            return offset;
        }
        public static int GetBPPOffset(int x, int y, int width, byte format)
        {
            int bit = 0;
            return GetBPPOffset(x, y, 0, 0, 1, format, ref bit, width);
        }
        /// <summary>
        /// Returns the color index of the BPP pixel at a given coordinate.
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="format"></param>
        /// <param name="bit"></param>
        public static int GetBPPColor(byte[] src, int x, int y, int srcOffset, int index, int zoom, byte format, int width)
        {
            int color = 0, bit = 0;
            int offset = GetBPPOffset(x, y, srcOffset, index, zoom, format, ref bit, width);
            if (format == 0x20 && offset + 17 >= src.Length)
                return -1;
            if (format == 0x10 && offset + 1 >= src.Length)
                return -1;
            if (format == 0x21 && offset + 17 >= src.Length)
                return -1;
            if ((format & 0x01) != 0x01)
            {
                if (Bits.GetBit(src, offset, bit)) color |= 1;
                if (Bits.GetBit(src, offset + 1, bit)) color |= 2;
                if (format == 0x20)
                {
                    if (Bits.GetBit(src, offset + 16, bit)) color |= 4;
                    if (Bits.GetBit(src, offset + 17, bit)) color |= 8;
                }
            }
            else
            {
                if ((x / zoom) % 2 == 0)
                    color = src[offset] & 0x0F;
                else
                    color = (src[offset] & 0xF0) >> 4;
            }
            return color;
        }
        private static void SetBPPColor(byte[] src, int x, int y, int srcOffset, int index, int zoom, byte format, int color, int width)
        {
            int bit = 0;
            int offset = GetBPPOffset(x, y, srcOffset, index, zoom, format, ref bit, width);
            if (format == 0x20 && offset + 17 >= src.Length)
                return;
            if (format == 0x10 && offset + 1 >= src.Length)
                return;
            if (format == 0x21 && offset + 17 >= src.Length)
                return;
            if ((format & 0x01) != 0x01)
            {
                Bits.SetBit(src, offset, bit, (color & 1) == 1);
                Bits.SetBit(src, offset + 1, bit, (color & 2) == 2);
                if (format == 0x20)
                {
                    Bits.SetBit(src, offset + 16, bit, (color & 4) == 4);
                    Bits.SetBit(src, offset + 17, bit, (color & 8) == 8);
                }
            }
            else
            {
                if ((x / zoom) % 2 == 0)
                    Bits.SetByteBits(src, offset, (byte)color, 0x0F);
                else
                    Bits.SetByteBits(src, offset, (byte)(color << 4), 0xF0);
            }
        }
        public static byte[] GetBPPRegion(byte[] src, int x, int y, int width, int height, int zoom, byte format)
        {
            byte[] buffer;
            if (format == 0x10)
                buffer = new byte[(width * height) / 4];
            else
                buffer = new byte[(width * height) / 2];
            for (int Y = y, y_ = 0; y_ < width; Y++, y_++)
            {
                for (int X = x, x_ = 0; x_ < height; X++, x_++)
                {
                    int offset = GetBPPOffset(X, Y, width, format);
                    int bufferOffset = GetBPPOffset(x_, y_, width, format);
                    buffer[bufferOffset] = src[offset];
                }
            }
            return buffer;
        }
        /// <summary>
        /// Flip horizontally an array of pixels.
        /// </summary>
        /// <param name="src">The pixels to flip horizontally.</param>
        /// <param name="srcWidth">The width of the pixel array.</param>
        /// <param name="srcHeight">The height of the pixel array.</param>
        public static void FlipHorizontal(int[] src, int srcWidth, int srcHeight)
        {
            int temp = 0;
            for (int y = 0; y < srcHeight; y++)
            {
                for (int a = 0, b = srcWidth - 1; a < srcWidth / 2; a++, b--)
                {
                    temp = src[(y * srcWidth) + a];
                    src[(y * srcWidth) + a] = src[(y * srcWidth) + b];
                    src[(y * srcWidth) + b] = temp;
                }
            }
        }
        public static void FlipHorizontal(byte[] src, int srcWidth, int srcHeight)
        {
            byte temp = 0;
            for (int y = 0; y < srcHeight; y++)
            {
                for (int a = 0, b = srcWidth - 1; a < srcWidth / 2; a++, b--)
                {
                    temp = src[(y * srcWidth) + a];
                    src[(y * srcWidth) + a] = src[(y * srcWidth) + b];
                    src[(y * srcWidth) + b] = temp;
                }
            }
        }
        /// <summary>
        /// Flip horizontally a region in a block of SNES graphics.
        /// </summary>
        /// <param name="src">The source SNES graphics.</param>
        /// <param name="srcWidth">The width of the graphics being read.</param>
        /// <param name="x">The X coord, in pixels, of the region being modified.</param>
        /// <param name="y">The Y coord, in pixels, of the region being modified.</param>
        /// <param name="width">The width, in pixels, of the region being modified.</param>
        /// <param name="height">The height, in pixels, of the region being modified.</param>
        /// <param name="zoom">The zoom factor.</param>
        /// <param name="format">The format, 2bpp or 4bpp (0x10 and 0x20), of the source graphics.</param>
        public static void FlipHorizontal(byte[] src, int srcWidth, int X, int Y, int width, int height, int zoom, byte format)
        {
            for (int y = Y; y < Y + height; y++)
            {
                for (int a = X, b = (width + X) - 1; a - X < width / 2; a++, b--)
                {
                    int tempA = GetBPPColor(src, a, y, 0, 0, zoom, format, srcWidth);
                    int tempB = GetBPPColor(src, b, y, 0, 0, zoom, format, srcWidth);
                    SetBPPColor(src, a, y, 0, 0, zoom, format, tempB, srcWidth);
                    SetBPPColor(src, b, y, 0, 0, zoom, format, tempA, srcWidth);
                }
            }
        }
        /// <summary>
        /// Flip vertically an array of pixels.
        /// </summary>
        /// <param name="src">The pixels to flip vertically.</param>
        /// <param name="srcWidth">The width of the pixel array.</param>
        /// <param name="srcHeight">The height of the pixel array.</param>
        public static void FlipVertical(int[] src, int srcWidth, int srcHeight)
        {
            int temp = 0;
            for (int x = 0; x < srcWidth; x++)
            {
                for (int a = 0, b = srcHeight - 1; a < srcHeight / 2; a++, b--)
                {
                    temp = src[(a * srcWidth) + x];
                    src[(a * srcWidth) + x] = src[(b * srcWidth) + x];
                    src[(b * srcWidth) + x] = temp;
                }
            }
        }
        public static void FlipVertical(byte[] src, int srcWidth, int srcHeight)
        {
            byte temp = 0;
            for (int x = 0; x < srcWidth; x++)
            {
                for (int a = 0, b = srcHeight - 1; a < srcHeight / 2; a++, b--)
                {
                    temp = src[(a * srcWidth) + x];
                    src[(a * srcWidth) + x] = src[(b * srcWidth) + x];
                    src[(b * srcWidth) + x] = temp;
                }
            }
        }
        /// <summary>
        /// Flip vertically a region in a block of SNES graphics.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="zoom"></param>
        /// <param name="format"></param>
        public static void FlipVertical(byte[] src, int srcWidth, int X, int Y, int width, int height, int zoom, byte format)
        {
            for (int x = X; x < X + width; x++)
            {
                for (int a = 0, b = (height + Y) - 1; a - Y < height / 2; a++, b--)
                {
                    int tempA = GetBPPColor(src, x, a, 0, 0, zoom, format, srcWidth);
                    int tempB = GetBPPColor(src, x, b, 0, 0, zoom, format, srcWidth);
                    SetBPPColor(src, x, a, 0, 0, zoom, format, tempB, srcWidth);
                    SetBPPColor(src, x, b, 0, 0, zoom, format, tempA, srcWidth);
                }
            }
        }
        /// <summary>
        /// Flip horizontally a region in an array of pixels.
        /// </summary>
        /// <param name="src">The pixels to flip horizontally.</param>
        /// <param name="srcWidth">The width of the entire pixel array.</param>
        /// <param name="regX">The X coord to start at.</param>
        /// <param name="regY">The Y coord to start at.</param>
        /// <param name="regWidth">The width of the region to modify.</param>
        /// <param name="regHeight">The height of the region to modify.</param>
        public static void FlipHorizontal(int[] src, int srcWidth, int regX, int regY, int regWidth, int regHeight)
        {
            int temp = 0;
            for (int y = regY; y < regY + regHeight; y++)
            {
                for (int a = regX, b = regX + (regWidth - 1); a < regX + (regWidth / 2); a++, b--)
                {
                    temp = src[(y * srcWidth) + a];
                    src[(y * srcWidth) + a] = src[(y * srcWidth) + b];
                    src[(y * srcWidth) + b] = temp;
                }
            }
        }
        /// <summary>
        /// Flip vertically a region in an array of pixels.
        /// </summary>
        /// <param name="src">The pixels to flip vertically.</param>
        /// <param name="srcWidth">The width of the pixel array.</param>
        /// <param name="regX">The X coord to start at.</param>
        /// <param name="regY">The Y coord to start at.</param>
        /// <param name="regWidth">The width of the region to modify.</param>
        /// <param name="regHeight">The height of the region to modify.</param>
        public static void FlipVertical(int[] src, int srcWidth, int regX, int regY, int regWidth, int regHeight)
        {
            int temp = 0;
            for (int x = regX; x < regX + regWidth; x++)
            {
                for (int a = regY, b = regY + (regHeight - 1); a < regY + (regHeight / 2); a++, b--)
                {
                    temp = src[(a * srcWidth) + x];
                    src[(a * srcWidth) + x] = src[(b * srcWidth) + x];
                    src[(b * srcWidth) + x] = temp;
                }
            }
        }
        /// <summary>
        /// Flip horizontally a 16x16 tile.
        /// </summary>
        /// <param name="tile">The tile to flip horizontally.</param>
        public static void FlipHorizontal(Tile tile)
        {
            for (int i = 0; i < 4; i++)
            {
                tile.Subtiles[i].Mirror = !tile.Subtiles[i].Mirror;
                FlipHorizontal(tile.Subtiles[i].Pixels, 8, 8);
                FlipHorizontal(tile.Subtiles[i].Colors, 8, 8);
            }
            Subtile temp = tile.Subtiles[0].Copy();
            tile.Subtiles[1].CopyTo(tile.Subtiles[0]);
            temp.CopyTo(tile.Subtiles[1]);
            temp = tile.Subtiles[2].Copy();
            tile.Subtiles[3].CopyTo(tile.Subtiles[2]);
            temp.CopyTo(tile.Subtiles[3]);
        }
        /// <summary>
        /// Flip vertically a 16x16 tile.
        /// </summary>
        /// <param name="tile">The tile to flip vertically.</param>
        public static void FlipVertical(Tile tile)
        {
            for (int i = 0; i < 4; i++)
            {
                tile.Subtiles[i].Invert = !tile.Subtiles[i].Invert;
                FlipVertical(tile.Subtiles[i].Pixels, 8, 8);
                FlipVertical(tile.Subtiles[i].Colors, 8, 8);
            }
            Subtile temp = tile.Subtiles[0].Copy();
            tile.Subtiles[2].CopyTo(tile.Subtiles[0]);
            temp.CopyTo(tile.Subtiles[2]);
            temp = tile.Subtiles[1].Copy();
            tile.Subtiles[3].CopyTo(tile.Subtiles[1]);
            temp.CopyTo(tile.Subtiles[3]);
        }
        /// <summary>
        /// Flip horizontally a set of 16x16 tiles.
        /// </summary>
        /// <param name="tiles">The tiles to flip horizontally.</param>
        /// <param name="width">The width, in 16x16 tile units, of the tile set.</param>
        /// <param name="height">The height, in 16x16 tile units, of the tile set.</param>
        public static void FlipHorizontal(Tile[] tiles, int width, int height)
        {
            Tile temp;
            for (int y = 0; y < height; y++)
            {
                int a = 0;
                for (int b = width - 1; a < width / 2; a++, b--)
                {
                    // first, flip the tiles
                    temp = tiles[(y * width) + a].Copy();
                    tiles[(y * width) + a] = tiles[(y * width) + b].Copy();
                    tiles[(y * width) + a].Index = (y * width) + a;
                    tiles[(y * width) + b] = temp.Copy();
                    tiles[(y * width) + b].Index = (y * width) + b;
                    // now flip subtiles in both tiles
                    Tile tile = tiles[(y * width) + a];
                    for (int c = 0; c < 2; c++)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            tile.Subtiles[i].Mirror = !tile.Subtiles[i].Mirror;
                            FlipHorizontal(tile.Subtiles[i].Pixels, 8, 8);
                            FlipHorizontal(tile.Subtiles[i].Colors, 8, 8);
                        }
                        Subtile subtile = tile.Subtiles[0].Copy();
                        tile.Subtiles[1].CopyTo(tile.Subtiles[0]);
                        subtile.CopyTo(tile.Subtiles[1]);
                        subtile = tile.Subtiles[2].Copy();
                        tile.Subtiles[3].CopyTo(tile.Subtiles[2]);
                        subtile.CopyTo(tile.Subtiles[3]);
                        // set to flip subtiles in the other one
                        tile = tiles[(y * width) + b];
                    }
                }
                // if width was odd, have to flip middle tile manually
                if (width % 2 == 1)
                {
                    temp = tiles[y * width + a];
                    FlipHorizontal(temp);
                }
            }
        }
        /// <summary>
        /// Flip vertically a set of 16x16 tiles.
        /// </summary>
        /// <param name="tiles">The tiles to flip vertically.</param>
        /// <param name="width">The width, in 16x16 tile units, of the tile set.</param>
        /// <param name="height">The height, in 16x16 tile units, of the tile set.</param>
        public static void FlipVertical(Tile[] tiles, int width, int height)
        {
            Tile temp;
            for (int x = 0; x < width; x++)
            {
                int a = 0;
                for (int b = height - 1; a < height / 2; a++, b--)
                {
                    // first, flip the tiles
                    temp = tiles[(a * width) + x];
                    tiles[(a * width) + x] = tiles[(b * width) + x];
                    tiles[(b * width) + x] = temp;
                    // now flip subtiles in both tiles
                    Tile tile = tiles[(a * width) + x];
                    for (int c = 0; c < 2; c++)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            tile.Subtiles[i].Invert = !tile.Subtiles[i].Invert;
                            FlipVertical(tile.Subtiles[i].Pixels, 8, 8);
                            FlipVertical(tile.Subtiles[i].Colors, 8, 8);
                        }
                        Subtile subtile = tile.Subtiles[0].Copy();
                        tile.Subtiles[2].CopyTo(tile.Subtiles[0]);
                        subtile.CopyTo(tile.Subtiles[2]);
                        subtile = tile.Subtiles[1].Copy();
                        tile.Subtiles[3].CopyTo(tile.Subtiles[1]);
                        subtile.CopyTo(tile.Subtiles[3]);
                        // set to flip subtiles in the other one
                        tile = tiles[(b * width) + x];
                    }
                }
                // if height was odd, have to flip middle tile manually
                if (height % 2 == 1)
                {
                    temp = tiles[(a * width) + x];
                    FlipVertical(temp);
                }
            }
        }
        /// <summary>
        /// Flip horizontally a set of 16x16 tiles.
        /// </summary>
        /// <param name="tiles">The tiles to flip horizontally.</param>
        /// <param name="width">The width, in 16x16 tile units, of the tile set.</param>
        /// <param name="height">The height, in 16x16 tile units, of the tile set.</param>
        /// <param name="subtiles">Whether or not to flip by subtiles or by tile.</param>
        public static void FlipHorizontal(Tile[] tiles, int width, int height, bool subtiles)
        {
            Tile temp;
            for (int y = 0; y < height; y++)
            {
                int a = 0;
                for (int b = width - 1; a < width / 2; a++, b--)
                {
                    // first, flip the tiles
                    temp = tiles[(y * width) + a].Copy();
                    tiles[(y * width) + a] = tiles[(y * width) + b].Copy();
                    tiles[(y * width) + b] = temp.Copy();
                    Tile tile = tiles[(y * width) + a];
                    if (subtiles)
                    {
                        // now flip subtiles in both tiles
                        for (int c = 0; c < 2; c++)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                tile.Subtiles[i].Mirror = !tile.Subtiles[i].Mirror;
                                FlipHorizontal(tile.Subtiles[i].Pixels, 8, 8);
                                FlipHorizontal(tile.Subtiles[i].Colors, 8, 8);
                            }
                            Subtile subtile = tile.Subtiles[0].Copy();
                            tile.Subtiles[1].CopyTo(tile.Subtiles[0]);
                            subtile.CopyTo(tile.Subtiles[1]);
                            subtile = tile.Subtiles[2].Copy();
                            tile.Subtiles[3].CopyTo(tile.Subtiles[2]);
                            subtile.CopyTo(tile.Subtiles[3]);
                            // set to flip subtiles in the other one
                            tile = tiles[(y * width) + b];
                        }
                    }
                    else
                        tile.Mirror = true;
                }
                // if width was odd, have to flip middle tile manually
                if (width % 2 == 1)
                {
                    temp = tiles[y * width + a];
                    if (subtiles)
                        FlipHorizontal(temp);
                    else
                        temp.Mirror = true;
                }
            }
        }
        /// <summary>
        /// Flip vertically a set of 16x16 tiles.
        /// </summary>
        /// <param name="tiles">The tiles to flip vertically.</param>
        /// <param name="width">The width, in 16x16 tile units, of the tile set.</param>
        /// <param name="height">The height, in 16x16 tile units, of the tile set.</param>
        /// <param name="subtiles">Whether or not to flip by subtiles or by tile.</param>
        public static void FlipVertical(Tile[] tiles, int width, int height, bool subtiles)
        {
            Tile temp;
            for (int x = 0; x < width; x++)
            {
                int a = 0;
                for (int b = height - 1; a < height / 2; a++, b--)
                {
                    // first, flip the tiles
                    temp = tiles[(a * width) + x];
                    tiles[(a * width) + x] = tiles[(b * width) + x];
                    tiles[(b * width) + x] = temp;
                    // now flip subtiles in both tiles
                    Tile tile = tiles[(a * width) + x];
                    if (subtiles)
                    {
                        for (int c = 0; c < 2; c++)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                tile.Subtiles[i].Invert = !tile.Subtiles[i].Invert;
                                FlipVertical(tile.Subtiles[i].Pixels, 8, 8);
                                FlipVertical(tile.Subtiles[i].Colors, 8, 8);
                            }
                            Subtile subtile = tile.Subtiles[0].Copy();
                            tile.Subtiles[2].CopyTo(tile.Subtiles[0]);
                            subtile.CopyTo(tile.Subtiles[2]);
                            subtile = tile.Subtiles[1].Copy();
                            tile.Subtiles[3].CopyTo(tile.Subtiles[1]);
                            subtile.CopyTo(tile.Subtiles[3]);
                            // set to flip subtiles in the other one
                            tile = tiles[(b * width) + x];
                        }
                    }
                    else
                        tile.Invert = true;
                }
                // if height was odd, have to flip middle tile manually
                if (height % 2 == 1)
                {
                    temp = tiles[(a * width) + x];
                    if (subtiles)
                        FlipVertical(temp);
                    else
                        temp.Invert = true;
                }
            }
        }
        /// <summary>
        /// Returns the closest palette to a pixel array's colors from a set of palettes.
        /// </summary>
        /// <param name="palettes">The palette set / palettes.</param>
        /// <param name="array">The pixel array.</param>
        /// <returns></returns>
        public static int GetClosestPaletteIndex(int[][] palettes, int[] array)
        {
            int closestAvg = 248;
            int closestIndex = 0;
            Color[] colors;
            int[] dst = new int[array.Length];
            int[] avgs = new int[palettes.Length];
            for (int index = 0; index < palettes.Length; index++)
            {
                colors = new Color[palettes[index].Length];
                double distance = 500.0;
                double temp;
                double r, g, b;
                double dbl_test_red;
                double dbl_test_green;
                double dbl_test_blue;
                for (int i = 0; i < palettes[index].Length; i++)
                    colors[i] = Color.FromArgb(palettes[index][i]);
                int[] diffs = new int[array.Length];
                for (int i = 0; i < array.Length; i++)
                {
                    distance = 500;
                    r = Convert.ToDouble(Color.FromArgb(array[i]).R);
                    g = Convert.ToDouble(Color.FromArgb(array[i]).G);
                    b = Convert.ToDouble(Color.FromArgb(array[i]).B);
                    int nearest_color = 0;
                    Color o;
                    for (int v = 1; v < colors.Length; v++)
                    {
                        o = colors[v];
                        dbl_test_red = Math.Pow(Convert.ToDouble(((Color)o).R) - r, 2.0);
                        dbl_test_green = Math.Pow(Convert.ToDouble(((Color)o).G) - g, 2.0);
                        dbl_test_blue = Math.Pow(Convert.ToDouble(((Color)o).B) - b, 2.0);
                        temp = Math.Sqrt(dbl_test_blue + dbl_test_green + dbl_test_red);
                        // explore the result and store the nearest color
                        if (temp == 0.0)
                        {
                            nearest_color = v;
                            break;
                        }
                        else if (temp < distance)
                        {
                            distance = temp;
                            nearest_color = v;
                        }
                    }
                    if (dst[i] != 0)
                    {
                        dst[i] = nearest_color;
                        diffs[i] = Math.Abs(dst[i] - array[i]);
                    }
                }
                // get the average color difference
                int mean = 0;
                foreach (int average in diffs)
                    mean += average;
                if (mean / diffs.Length < closestAvg)
                    closestIndex = index;
            }
            return closestIndex;
        }
        /// <summary>
        /// Returns the closest palette to a region in a pixel array's colors from a set of palettes.
        /// </summary>
        /// <param name="palettes">The palette set / palettes.</param>
        /// <param name="array">The pixel array.</param>
        /// <param name="region">The region to analyze.</param>
        /// <returns></returns>
        public static int GetClosestPaletteIndex(int[][] palettes, int[] src, Rectangle source, Rectangle destination)
        {
            int closestAvg = 248;
            int closestIndex = 0;
            Color[] colors;
            for (int index = 0; index < palettes.Length; index++)
            {
                colors = new Color[palettes[index].Length];
                double distance = 500.0;
                double temp = 500.0;
                double r, g, b;
                double dbl_test_red;
                double dbl_test_green;
                double dbl_test_blue;
                for (int i = 0; i < palettes[index].Length; i++)
                    colors[i] = Color.FromArgb(palettes[index][i]);
                int[] diffs = new int[destination.Width * destination.Height];
                for (int y = destination.Y, y_ = 0; y < destination.Y + destination.Height && y_ < destination.Height; y++, y_++)
                {
                    for (int x = destination.X, x_ = 0; x < destination.X + destination.Width && x_ < destination.Width; x++, x_++)
                    {
                        distance = 500;
                        r = Convert.ToDouble(Color.FromArgb(src[y * source.Width + x]).R);
                        g = Convert.ToDouble(Color.FromArgb(src[y * source.Width + x]).G);
                        b = Convert.ToDouble(Color.FromArgb(src[y * source.Width + x]).B);
                        int nearest_color = 0;
                        Color o;
                        for (int v = 1; v < colors.Length; v++)
                        {
                            o = colors[v];
                            dbl_test_red = Math.Pow(Convert.ToDouble(((Color)o).R) - r, 2.0);
                            dbl_test_green = Math.Pow(Convert.ToDouble(((Color)o).G) - g, 2.0);
                            dbl_test_blue = Math.Pow(Convert.ToDouble(((Color)o).B) - b, 2.0);
                            temp = Math.Sqrt(dbl_test_blue + dbl_test_green + dbl_test_red);
                            // explore the result and store the nearest color
                            if (temp == 0.0)
                            {
                                nearest_color = v;
                                break;
                            }
                            else if (temp < distance)
                            {
                                distance = temp;
                                nearest_color = v;
                            }
                        }
                        // get the difference between applied snes palette color and original bitmap color
                        diffs[y_ * destination.Width + x_] = (int)temp;
                    }
                }
                // get the average color difference
                int dividend = 0; int divisor = 0;
                foreach (int difference in diffs)
                {
                    dividend += difference;
                    divisor++;
                }
                if (dividend / divisor < closestAvg)
                {
                    closestAvg = dividend / divisor;
                    closestIndex = index;
                }
            }
            return closestIndex;
        }
        /// <summary>
        /// Returns a byte that indicates whether or not a pixel array is a mirror and/or inversion of another.
        /// </summary>
        /// <param name="tile_a">The independent array being compared to.</param>
        /// <param name="tile_b">The dependent array being compared to.</param>
        /// <returns></returns>
        public static byte GetFlippedStatus(int[] tile_a, int[] tile_b)
        {
            if (tile_a.Length != tile_b.Length) return 0;
            byte status = 0;
            // first create a mirror of tile_a which will be checked later
            int[] tile_a_both = new int[tile_a.Length]; tile_a.CopyTo(tile_a_both, 0);
            int[] tile_a_mirrored = new int[tile_a.Length]; tile_a.CopyTo(tile_a_mirrored, 0);
            int temp = 0;
            for (int y = 0; y < 8; y++)
            {
                for (int a = 0, b = 7; a < 4; a++, b--)
                {
                    temp = tile_a_mirrored[(y * 8) + a];
                    tile_a_mirrored[(y * 8) + a] = tile_a_mirrored[(y * 8) + b];
                    tile_a_mirrored[(y * 8) + b] = temp;
                    // do the same for this one
                    temp = tile_a_both[(y * 8) + a];
                    tile_a_both[(y * 8) + a] = tile_a_both[(y * 8) + b];
                    tile_a_both[(y * 8) + b] = temp;
                }
            }
            // now create an inverted tile_a which will be checked later
            int[] tile_a_inverted = new int[tile_a.Length]; tile_a.CopyTo(tile_a_inverted, 0);
            temp = 0;
            for (int x = 0; x < 8; x++)
            {
                for (int a = 0, b = 7; a < 4; a++, b--)
                {
                    temp = tile_a_inverted[(a * 8) + x];
                    tile_a_inverted[(a * 8) + x] = tile_a_inverted[(b * 8) + x];
                    tile_a_inverted[(b * 8) + x] = temp;
                    // do the same for this one
                    temp = tile_a_both[(a * 8) + x];
                    tile_a_both[(a * 8) + x] = tile_a_both[(b * 8) + x];
                    tile_a_both[(b * 8) + x] = temp;
                }
            }
            // finally compare them
            if (Bits.Compare(tile_b, tile_a_mirrored))
                status |= 0x40;
            if (Bits.Compare(tile_b, tile_a_inverted))
                status |= 0x80;
            if (Bits.Compare(tile_b, tile_a_both))
                status |= 0xC0;
            return status;
        }
        /// <summary>
        /// Returns a pixel array from a region in another pixel array.
        /// </summary>
        /// <param name="array">The array to get the region from.</param>
        /// <param name="region">The region of the array to get.</param>
        /// <param name="srcWidth">The width of the array being read.</param>
        /// <param name="srcHeight">The height of the array being read.</param>
        /// <returns></returns>
        public static int[] GetPixelRegion(int[] array, Rectangle region, int srcWidth, int srcHeight)
        {
            int[] temp = new int[region.Width * region.Height];
            for (int y = 0; y < region.Height; y++)
            {
                if (y + region.Y >= srcHeight) continue;
                for (int x = 0; x < region.Width; x++)
                {
                    if (x + region.X >= srcWidth) continue;
                    temp[y * region.Width + x] = array[(y + region.Y) * srcWidth + (x + region.X)];
                }
            }
            return temp;
        }
        /// <summary>
        /// Returns a pixel array from a region in another pixel array.
        /// </summary>
        /// <param name="array">The array to read.</param>
        /// <param name="dstWidth">The width of the array to read.</param>
        /// <param name="dstHeight">The height of the array to read.</param>
        /// <param name="regWidth">The width of the region to create.</param>
        /// <param name="regHeight">The height of the region to create.</param>
        /// <param name="regX">The X coord to read from in the array.</param>
        /// <param name="regY">The Y coord to read from in the array.</param>
        /// <returns></returns>
        public static int[] GetPixelRegion(int[] array, int dstWidth, int dstHeight, int regWidth, int regHeight, int regX, int regY)
        {
            int[] temp = new int[regWidth * regHeight];
            for (int y = 0; y < regHeight; y++)
            {
                if (y + regY >= dstHeight) continue;
                for (int x = 0; x < regWidth; x++)
                {
                    if (x + regX >= dstWidth) continue;
                    temp[y * regWidth + x] = array[(y + regY) * dstWidth + (x + regX)];
                }
            }
            return temp;
        }
        /// <summary>
        /// Returns a pixel array from a region in a block of 4bpp or 2bpp graphics.
        /// </summary>
        /// <param name="snes">The block of graphics.</param>
        /// <param name="format">The format of the graphics. 0x10 or 0x20 for 2bpp and 4bpp, respectively.</param>
        /// <param name="palette">The palette to apply to the pixel array.</param>
        /// <param name="srcWidth">The width, in 8x8 tile units, of the graphics to draw from.</param>
        /// <param name="regX">The X coord, in 8x8 tile units, in the graphics to start drawing from.</param>
        /// <param name="regY">The Y coord, in 8x8 tile units, in the graphics to start drawing from.</param>
        /// <param name="regWidth">The width, in 8x8 tile units, of the region to draw from.</param>
        /// <param name="regHeight">The height, in 8x8 tile units, of the region to draw from.</param>
        /// <param name="offset">The offset to start reading the SNES graphics from.</param>
        /// <returns></returns>
        public static int[] GetPixelRegion(byte[] snes, int format, int[] palette, int srcWidth, int regX, int regY, int regWidth, int regHeight, int offset)
        {
            Subtile temp;
            int[] pixels = new int[(regWidth * 8) * (regHeight * 8)];
            for (int y = regY; y < regY + regHeight; y++)
            {
                for (int x = regX; x < regX + regWidth; x++)
                {
                    int index = y * regWidth + x;
                    if ((index * (format & 0xFE)) + offset >= snes.Length)
                        continue;
                    if (format == 0x21)
                        temp = new Subtile(index, snes, index * (format & 0xFE) + offset, palette);
                    else
                        temp = new Subtile(index, snes, index * (format & 0xFE) + offset, palette, false, false, false, format == 0x10);
                    Do.PixelsToPixels(temp.Pixels, pixels, regWidth * 8, new Rectangle(x * 8, y * 8, 8, 8));
                }
            }
            return pixels;
        }
        /// <summary>
        /// Converts an image into a pixel array.
        /// </summary>
        /// <param name="image">The image to convert.</param>
        /// <returns></returns>
        public static int[] ImageToPixels(Bitmap image)
        {
            Size size = image.Size;
            int w = image.Width / 8 * 8;
            int h = image.Height / 8 * 8;
            int[] temp = new int[w * h];
            for (int y = 0; y < size.Height && y < h; y++)
            {
                for (int x = 0; x < size.Width && x < w; x++)
                    temp[y * w + x] = image.GetPixel(x, y).ToArgb();
            }
            return temp;
        }
        /// <summary>
        /// Converts an image into a pixel array.
        /// </summary>
        /// <param name="image">The image to convert.</param>
        /// <param name="transparent">The color in the image that is transparent.</param>
        /// <returns></returns>
        public static int[] ImageToPixels(Bitmap image, Color transparent)
        {
            Size size = image.Size;
            int w = image.Width / 8 * 8;
            int h = image.Height / 8 * 8;
            int[] temp = new int[w * h];
            for (int y = 0; y < size.Height && y < h; y++)
            {
                for (int x = 0; x < size.Width && x < w; x++)
                {
                    if (image.GetPixel(x, y).ToArgb() == transparent.ToArgb())
                        continue;
                    temp[y * w + x] = image.GetPixel(x, y).ToArgb();
                }
            }
            return temp;
        }
        /// <summary>
        /// Converts an image into a pixel array.
        /// </summary>
        /// <param name="image">The image to convert.</param>
        /// <param name="maximumSize">The size of the converted image.</param>
        /// <returns></returns>
        public static int[] ImageToPixels(Bitmap image, Size size)
        {
            int w = image.Width / 8 * 8;
            int h = image.Height / 8 * 8;
            int[] temp = new int[w * h];
            for (int y = 0; y < size.Height && y < h; y++)
            {
                for (int x = 0; x < size.Width && x < w; x++)
                    temp[y * w + x] = image.GetPixel(x, y).ToArgb();
            }
            return temp;
        }
        /// <summary>
        /// Converts a region of an image into a pixel array.
        /// </summary>
        /// <param name="image">The image to convert.</param>
        /// <param name="maximumSize">The size of the converted image.</param>
        /// <param name="region">The region of the image to convert.</param>
        /// <returns></returns>
        public static int[] ImageToPixels(Bitmap image, Size size, Rectangle region)
        {
            int[] temp = new int[region.Width * region.Height];
            for (int y = 0, y_ = region.Y; y < size.Height && y < region.Height && y_ < image.Height; y++, y_++)
            {
                for (int x = 0, x_ = region.X; x < size.Width && x < region.Width && x_ < image.Width; x++, x_++)
                    temp[y * region.Width + x] = image.GetPixel(x_, y_).ToArgb();
            }
            return temp;
        }
        /// <summary>
        /// Combines several bitmaps into a single bitmap.
        /// </summary>
        /// <param name="images">The collection of bitmaps.</param>
        /// <param name="maxwidth">The maximum width of the bitmap sheet.</param>
        /// <param name="maxheight">The maximum height of the bitmap sheet.</param>
        /// <param name="tilesize">The default tile size. Typically 8 or 16.</param>
        /// <param name="padedges">Each image will be padded to fit a multiple of the tile size.</param>
        /// <returns></returns>
        public static void ImagesToTilemaps(ref Bitmap[] images, ref int[] palette, int moldIndex, byte format,
            ref byte[] graphics_, ref Tile[] tiles_, ref byte[][] tilemaps_, bool newPalette)
        {
            int count = images.Length;
            int width = Math.Min(256, images[0].Width / 16 * 16);
            if (images[0].Width % 16 != 0) width += 16;
            int height = Math.Min(256, images[0].Height / 16 * 16);
            if (images[0].Height % 16 != 0) height += 16;
            // pad dimensions to multiples of 16
            Bitmap[] resized = new Bitmap[count];//(width, height);
            for (int i = 0; i < resized.Length; i++)
            {
                resized[i] = new Bitmap(width, height);
                Graphics temp = Graphics.FromImage(resized[i]);
                temp.DrawImage(images[i], 0, 0, Math.Min(256, images[i].Width), Math.Min(256, images[i].Height));
            }
            // declare stuff
            int[][] pixels = new int[count][];
            byte[][] graphics = new byte[count][];
            byte[][] graphicsCulled = new byte[count][];
            byte[][] tilesets = new byte[count][];
            Tile[][] tiles = new Tile[count][];//[(width / 16) * (height / 16)];
            byte[][] tilemaps = new byte[count][];
            int graphicsLength = 0;
            int[] reducedPalette = null;
            if (moldIndex < count)
            {
                pixels[moldIndex] = ImageToPixels(resized[moldIndex]);
                // convert to BPP, create culled graphics
                graphics[moldIndex] = new byte[0x10000]; // a BPP format copy of the original image
                if (!newPalette)
                    reducedPalette = palette;
                else
                    reducedPalette = Do.ReduceColorDepth(pixels[moldIndex], format == 0x10 ? 4 : 16, palette[0]);
                PixelsToBPP(pixels[moldIndex], graphics[moldIndex], new Size(width / 8, height / 8), reducedPalette, format);
                graphicsCulled[moldIndex] = CullGraphics(graphics[moldIndex], palette, format, false);
                graphicsLength += graphicsCulled[moldIndex].Length;
            }
            for (int i = 0; i < count; i++)
            {
                if (i == moldIndex && moldIndex < count)
                    continue;
                // convert to pixels
                pixels[i] = ImageToPixels(resized[i]);
                // convert to BPP, create culled graphics
                graphics[i] = new byte[0x10000]; // a BPP format copy of the original image
                if (reducedPalette == null)
                    reducedPalette = Do.ReduceColorDepth(pixels[i], format == 0x10 ? 4 : 16, palette[0]);
                PixelsToBPP(pixels[i], graphics[i], new Size(width / 8, height / 8), reducedPalette, format);
                graphicsCulled[i] = CullGraphics(graphics[i], palette, format, false);
                graphicsLength += graphicsCulled[i].Length;
            }
            // combine all images' graphics into one array
            byte[] culledGraphics = new byte[graphicsLength];
            for (int i = 0, position = 0; i < graphicsCulled.Length; i++)
            {
                graphicsCulled[i].CopyTo(culledGraphics, position);
                position += graphicsCulled[i].Length;
            }
            // convert to raw tileset
            for (int i = 0; i < tilesets.Length; i++)
            {
                tilesets[i] = new byte[0x1000];
                for (int y = 0; y < height / 8; y++)
                {
                    for (int x = 0; x < width / 8; x++)
                    {
                        int index = y * (width / 8) + x;
                        Subtile subtileA = DrawSubtile((ushort)index, 0x20, graphics[i], reducedPalette, format);
                        for (int a = 0; a < culledGraphics.Length / format; a++)
                        {
                            Subtile subtileB = DrawSubtile((ushort)a, 0x20, culledGraphics, reducedPalette, format);
                            if (Bits.Compare(subtileA.Pixels, subtileB.Pixels))
                            {
                                tilesets[i][index * 2] = (byte)a;
                                break;
                            }
                        }
                    }
                }
            }
            // convert raw tileset data to array of Tile16x16[]
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = new Tile[(width / 16) * (height / 16)];
                for (int a = 0; a < tiles[i].Length; a++)
                    tiles[i][a] = new Tile(a);
            }
            int tilesLength = 0;
            for (int i = 0; i < tiles.Length; i++)
            {
                for (int y = 0; y < height / 16; y++)
                {
                    for (int x = 0; x < width / 16; x++)
                    {
                        int index = y * (width / 16) + x;
                        for (int z = 0; z < 4; z++)
                        {
                            int offset = y * (width / 2) + (x * 4);
                            if (z % 2 == 1)
                                offset += 2;
                            if (z / 2 == 1)
                                offset += (width / 8) * 2;
                            ushort tile = tilesets[i][offset];
                            byte prop = tilesets[i][offset + 1];
                            Subtile source = DrawSubtile(tile, prop, culledGraphics, reducedPalette, format);
                            tiles[i][index].Subtiles[z] = source;
                        }
                    }
                }
                // cull tileset
                CullTileset(ref tiles[i]);
                tilesLength += tiles[i].Length;
            }
            // combine into one tileset
            Tile[] culledTiles = new Tile[tilesLength];
            for (int i = 0, position = 0; i < tiles.Length; i++)
            {
                tiles[i].CopyTo(culledTiles, position);
                position += tiles[i].Length;
            }
            // draw tilemap
            for (int i = 0; i < tilemaps.Length; i++)
            {
                tilemaps[i] = new byte[(width / 16) * (height / 16)];
                //pixels[i] = ImageToPixels(resized[i]);
                for (int y = 0; y < height / 16; y++)
                {
                    for (int x = 0; x < width / 16; x++)
                    {
                        int index = y * (width / 16) + x;
                        Rectangle region = new Rectangle(x * 16, y * 16, 16, 16);
                        int[] regionA = GetPixelRegion(graphics[i], format, reducedPalette, width / 8, x * 2, y * 2, 2, 2, 0);
                        for (int a = 0; a < culledTiles.Length; a++)
                        {
                            if (Bits.Compare(regionA, culledTiles[a].Pixels))
                            {
                                tilemaps[i][index] = (byte)a;
                                break;
                            }
                        }
                    }
                }
            }
            images[0] = resized[0];
            palette = reducedPalette;
            graphics_ = culledGraphics;
            tiles_ = culledTiles;
            tilemaps_ = tilemaps;
        }
        /// <summary>
        /// Draws a pixel array from a set of colors in a palette set.
        /// </summary>
        /// <param name="colorWidth">The width of each color block.</param>
        /// <param name="colorHeight">The height of each color block.</param>
        /// <param name="cols">The number of color rows the pixel array will have.</param>
        /// <param name="rows">The number of color columns the pixel array will have</param>
        /// <returns></returns>
        public static int[] PaletteToPixels(int[] palette, int colorWidth, int colorHeight, int cols, int rows, int startCol)
        {
            Size size = new Size(colorWidth * cols, colorHeight * rows);
            int[] pixels = new int[size.Width * size.Height];
            for (int i = 0; i < rows; i++)
            {
                for (int a = startCol; a < cols; a++)
                {
                    for (int y = 0; y < colorHeight; y++)
                    {
                        for (int x = 0; x < colorWidth; x++)
                            pixels[((y + (i * colorHeight)) * size.Width) + x + (a * colorWidth)] = palette[i * cols + a];
                    }
                }
            }
            return pixels;
        }
        /// <summary>
        /// Draws a pixel array from a set of colors in a palette set.
        /// </summary>
        /// <param name="palette">The set of palettes.</param>
        /// <param name="colorWidth">The width of each color block.</param>
        /// <param name="colorHeight">The height of each color block.</param>
        /// <param name="cols">The number of color rows the pixel array will have.</param>
        /// <param name="rows">The number of color columns the pixel array will have</param>
        /// <param name="start">The palette to start drawing from</param>
        /// <param name="startColor">The column to start drawing from</param>
        /// <returns></returns>
        public static int[] PaletteToPixels(int[][] palette, int colorWidth, int colorHeight, int cols, int rows, int startRow, int startCol)
        {
            Size size = new Size(colorWidth * cols, colorHeight * rows);
            int[] pixels = new int[size.Width * size.Height];
            for (int i = 0; i < rows - startRow && i < palette.Length; i++)
            {
                for (int a = startCol; a < cols; a++)
                {
                    for (int y = 0; y < colorHeight; y++)
                    {
                        for (int x = 0; x < colorWidth; x++)
                            pixels[((y + (i * colorHeight)) * size.Width) + x + (a * colorWidth)] = palette[i + startRow][a];
                    }
                }
            }
            return pixels;
        }
        /// <summary>
        /// Converts a raw pixel array to either 4bpp or 2bpp format.
        /// Returns the respective palette indexes for the tiles it converted.
        /// </summary>
        /// <param name="src">The pixel array to convert.</param>
        /// <param name="dst">The data array to write the converted graphics to.</param>
        /// <param name="size">The size (in 8x8 tiles) of the image.</param>
        /// <param name="palette">The palettes to apply to the image.</param>
        /// <param name="format">The format of the graphics, either 0x10 or 0x20 (2bpp and 4bpp, respectively).</param>
        /// <returns></returns>
        public static int[] PixelsToBPP(int[] src, byte[] dst, Size size, int[][] palettes, byte format)
        {
            int[] indexes = new int[size.Width * size.Height];
            Do.PixelsToBPP_Worker = new BackgroundWorker();
            Do.PixelsToBPP_Worker.WorkerReportsProgress = true;
            Do.PixelsToBPP_Worker.WorkerSupportsCancellation = true;
            Do.PixelsToBPP_Worker.DoWork += (s, e) =>
                PixelsToBPP_Worker_DoWork(s, e, src, dst, size, palettes, format, indexes);
            Do.PixelsToBPP_Worker.ProgressChanged += new ProgressChangedEventHandler(PixelsToBPP_Worker_ProgressChanged);
            Do.PixelsToBPP_Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(PixelsToBPP_Worker_RunWorkerCompleted);
            Do.ProgressBar = new ProgressBar("CONVERTING IMPORTED IMAGE TO BPP FORMAT...", size.Width * size.Height, PixelsToBPP_Worker);
            ProgressBar.Show();
            PixelsToBPP_Worker.RunWorkerAsync();
            while (PixelsToBPP_Worker.IsBusy)
            {
                if (PixelsToBPP_Worker.CancellationPending)
                    return null;
                Application.DoEvents();
            }
            return indexes;
        }
        private static void PixelsToBPP_Worker_DoWork(object sender, DoWorkEventArgs e, int[] src, byte[] dst, Size size, int[][] palettes, byte format, int[] indexes)
        {
            Point p;
            int offset;
            byte bit;
            for (int y_ = 0; y_ < size.Height; y_++)
            {
                for (int x_ = 0; x_ < size.Width; x_++)
                {
                    int i = y_ * size.Width + x_;
                    if (PixelsToBPP_Worker.CancellationPending)
                        return;
                    PixelsToBPP_Worker.ReportProgress(i);
                    Rectangle regionDest = new Rectangle(x_ * 8, y_ * 8, 8, 8);
                    Rectangle regionSrc = new Rectangle(0, 0, size.Width * 8, size.Height * 8);
                    indexes[i] = GetClosestPaletteIndex(palettes, src, regionSrc, regionDest);
                    ApplyPaletteToPixels(src, palettes[indexes[i]], regionSrc, regionDest);
                    for (int y = 0; y < 8; y++)
                    {
                        for (int x = 0; x < 8; x++)
                        {
                            p = new Point(i % size.Width * 8 + x, i / size.Width * 8 + y);
                            bit = (byte)(x ^ 7);
                            offset = (i * format) + (y * 2);
                            if (format == 0x20)
                            {
                                Bits.SetBit(dst, offset, bit, (src[p.Y * (size.Width * 8) + p.X] & 1) == 1);
                                Bits.SetBit(dst, offset + 1, bit, (src[p.Y * (size.Width * 8) + p.X] & 2) == 2);
                                Bits.SetBit(dst, offset + 16, bit, (src[p.Y * (size.Width * 8) + p.X] & 4) == 4);
                                Bits.SetBit(dst, offset + 17, bit, (src[p.Y * (size.Width * 8) + p.X] & 8) == 8);
                            }
                            else
                            {
                                Bits.SetBit(dst, offset, bit, (src[p.Y * (size.Width * 8) + p.X] & 1) == 1);
                                Bits.SetBit(dst, offset + 1, bit, (src[p.Y * (size.Width * 8) + p.X] & 2) == 2);
                            }
                        }
                    }
                }
            }
        }
        private static void PixelsToBPP_Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar.PerformStep("CONVERTING TILE #" + e.ProgressPercentage);
        }
        private static void PixelsToBPP_Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ProgressBar.Close();
        }
        private static BackgroundWorker PixelsToBPP_Worker;
        /// <summary>
        /// Converts a raw pixel array to either 4bpp or 2bpp format.
        /// </summary>
        /// <param name="src">The pixel array to convert.</param>
        /// <param name="dst">The data array to write the converted graphics to.</param>
        /// <param name="size">The size (in 8x8 tiles) of the image.</param>
        /// <param name="palette">The palette to apply to the image.</param>
        /// <param name="format">The format of the graphics, either 0x10 or 0x20 (2bpp and 4bpp, respectively).</param>
        /// <returns></returns>
        public static void PixelsToBPP(int[] src, byte[] dst, Size size, int[] palette, byte format)
        {
            Point p;
            int offset;
            byte bit;
            for (int y_ = 0; y_ < size.Height; y_++)
            {
                for (int x_ = 0; x_ < size.Width; x_++)
                {
                    int i = y_ * size.Width + x_;
                    Rectangle regionDest = new Rectangle(x_ * 8, y_ * 8, 8, 8);
                    Rectangle regionSrc = new Rectangle(0, 0, size.Width * 8, size.Height * 8);
                    ApplyPaletteToPixels(src, palette, regionSrc, regionDest);
                    for (int y = 0; y < 8; y++)
                    {
                        for (int x = 0; x < 8; x++)
                        {
                            p = new Point(i % size.Width * 8 + x, i / size.Width * 8 + y);
                            bit = (byte)(x ^ 7);
                            offset = (i * format) + (y * 2);
                            if (format == 0x20)
                            {
                                Bits.SetBit(dst, offset, bit, (src[p.Y * (size.Width * 8) + p.X] & 1) == 1);
                                Bits.SetBit(dst, offset + 1, bit, (src[p.Y * (size.Width * 8) + p.X] & 2) == 2);
                                Bits.SetBit(dst, offset + 16, bit, (src[p.Y * (size.Width * 8) + p.X] & 4) == 4);
                                Bits.SetBit(dst, offset + 17, bit, (src[p.Y * (size.Width * 8) + p.X] & 8) == 8);
                            }
                            else
                            {
                                Bits.SetBit(dst, offset, bit, (src[p.Y * (size.Width * 8) + p.X] & 1) == 1);
                                Bits.SetBit(dst, offset + 1, bit, (src[p.Y * (size.Width * 8) + p.X] & 2) == 2);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Converts a pixel array into an image.
        /// </summary>
        /// <param name="array">The pixel array.</param>
        /// <param name="width">The image width.</param>
        /// <param name="height">The image height.</param>
        /// <returns></returns>
        public static Bitmap PixelsToImage(int[] array, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
            IntPtr pNative = bitmapData.Scan0;
            Marshal.Copy(array, 0, pNative, width * height);
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }
        /// <summary>
        /// Converts a pixel array into an image.
        /// </summary>
        /// <param name="array">The pixel array.</param>
        /// <param name="size">The image size.</param>
        /// <returns></returns>
        public static Bitmap PixelsToImage(int[] array, Size size)
        {
            return PixelsToImage(array, size.Width, size.Height);
        }
        /// <summary>
        /// Draws a pixel array to a region in another pixel array.
        /// </summary>
        /// <param name="src">The pixel array to draw from.</param>
        /// <param name="dst">The pixel array to draw to.</param>
        /// <param name="dstWidth">The width of the pixel array being drawn to.</param>
        /// <param name="region">The region of the pixel array to draw to.</param>
        public static void PixelsToPixels(int[] src, int[] dst, int dstWidth, Rectangle region)
        {
            PixelsToPixels(src, dst, dstWidth, region, false, false);
        }
        public static void PixelsToPixels(int[] src, int[] dst, int dstWidth, int srcX, int srcY, int srcWidth, int srcHeight, bool drawAsTransparent)
        {
            PixelsToPixels(src, dst, dstWidth, new Rectangle(srcX, srcY, srcWidth, srcHeight), drawAsTransparent, false);
        }
        public static void PixelsToPixels(int[] src, int[] dst, int dstWidth, int srcX, int srcY, int srcWidth, int srcHeight)
        {
            PixelsToPixels(src, dst, dstWidth, new Rectangle(srcX, srcY, srcWidth, srcHeight), false, false);
        }
        public static void PixelsToPixels(int[] src, int[] dst, int dstWidth, Rectangle region, bool drawAsTransparent)
        {
            PixelsToPixels(src, dst, dstWidth, region, true, false);
        }
        public static void PixelsToPixels(int[] src, int[] dst, int dstWidth, Rectangle region, bool drawAsTransparent, bool colorMath)
        {
            for (int y = region.Y, y_ = 0; y < region.Y + region.Height; y++, y_++)
            {
                for (int x = region.X, x_ = 0; x < region.X + region.Width; x++, x_++)
                {
                    if (y < 0 || x < 0) continue;
                    if (y * dstWidth + x >= dst.Length) continue;
                    if (y_ * region.Width + x_ >= src.Length) continue;
                    if (src[y_ * region.Width + x_] == 0 && drawAsTransparent) continue;
                    if (colorMath)
                        dst[y * dstWidth + x] = ColorMath(src[y_ * region.Width + x_], dst[y * dstWidth + x], false, false);
                    else
                        dst[y * dstWidth + x] = src[y_ * region.Width + x_];
                }
            }
        }
        /// <summary>
        /// Draws a region in a pixel array to a region in another pixel array.
        /// </summary>
        /// <param name="src">The pixel array to draw from.</param>
        /// <param name="dst">The pixel array to draw to.</param>
        /// <param name="srcWidth">The width of the pixel array being drawn from.</param>
        /// <param name="dstWidth">The width of the pixel array being drawn to.</param>
        /// <param name="srcRegion">The region of the pixel array being drawn from.</param>
        /// <param name="dstRegion">The region of the pixel array being drawn to.</param>
        public static void PixelsToPixels(int[] src, int[] dst, int srcWidth, int dstWidth, Rectangle srcRegion, Rectangle dstRegion)
        {
            int[] tempSrc = new int[src.Length]; src.CopyTo(tempSrc, 0);
            for (int y = dstRegion.Y, y_ = srcRegion.Y; y < dstRegion.Y + dstRegion.Height && y_ < srcRegion.Y + srcRegion.Height; y++, y_++)
            {
                for (int x = dstRegion.X, x_ = srcRegion.X; x < dstRegion.X + dstRegion.Width && x_ < srcRegion.X + srcRegion.Width; x++, x_++)
                {
                    if (y * dstWidth + x >= dst.Length) continue;
                    if (y_ * srcWidth + x_ >= tempSrc.Length) continue;
                    dst[y * dstWidth + x] = tempSrc[y_ * srcWidth + x_];
                }
            }
        }
        /// <summary>
        /// Combines an array of reds, greens and blues into an array of colors.
        /// </summary>
        /// <param name="reds"></param>
        /// <param name="greens"></param>
        /// <param name="blues"></param>
        /// <returns></returns>
        public static int[] RGBToColors(int[] reds, int[] greens, int[] blues)
        {
            int[] palette = new int[reds.Length];
            for (int i = 0; i < palette.Length; i++)
            {
                palette[i] = Color.FromArgb(reds[i], greens[i], blues[i]).ToArgb();
            }
            return palette;
        }
        /// <summary>
        /// Combines an array of reds, greens and blues into an array of colors.
        /// </summary>
        /// <param name="reds"></param>
        /// <param name="greens"></param>
        /// <param name="blues"></param>
        /// <param name="count">The number of palettes in the set.</param>
        /// <param name="size">The number of colors in each palette.</param>
        /// <returns></returns>
        public static int[][] RGBToColors(int[] reds, int[] greens, int[] blues, int count, int size)
        {
            int[][] palettes = new int[count][];
            for (int a = 0; a < palettes.Length; a++)
            {
                palettes[a] = new int[size];
                for (int i = 0; i < palettes[a].Length; i++)
                {
                    palettes[a][i] = Color.FromArgb(reds[a * size + i], greens[a * size + i], blues[a * size + i]).ToArgb();
                }
            }
            return palettes;
        }
        /// <summary>
        /// Converts an array of colors into a raw 15-bit SNES palette format.
        /// </summary>
        /// <param name="rgbPalette">The palette to convert</param>
        /// <param name="paletteSize">The number of colors in the palette.</param>
        /// <returns></returns>
        public static byte[] RGBToSnesPalette(int[] rgbPalette, int paletteSize)
        {
            byte[] snesPalette = new byte[paletteSize * 2];
            for (int i = 0; i < paletteSize; i++)
            {
                int r = (int)(Color.FromArgb(rgbPalette[i]).R / 8);
                int g = (int)(Color.FromArgb(rgbPalette[i]).G / 8);
                int b = (int)(Color.FromArgb(rgbPalette[i]).B / 8);
                ushort color = (ushort)((b << 10) | (g << 5) | r);
                Bits.SetShort(snesPalette, i * 2, color);
            }
            return snesPalette;
        }
        /// <summary>
        /// Converts a raw 15-bit SNES format palette into a set of RGB palettes.
        /// </summary>
        /// <param name="snesPalette">The raw SNES palette data.</param>
        /// <param name="paletteSize">The number of colors in each palette.</param>
        /// <param name="reds">The array of red values in each palette.</param>
        /// <param name="greens">The array of green values in each palette.</param>
        /// <param name="blues">The array of blue values in each palette.</param>
        public static void SnesPaletteToRGB(byte[] snesPalette, int index, int[] reds, int[] greens, int[] blues)
        {
            for (int i = 0; i < reds.Length; i++)
            {
                ushort color = Bits.GetShort(snesPalette, (index * reds.Length) + (i * 2));
                reds[i] = (color % 0x20) * 8;
                greens[i] = ((color >> 5) % 0x20) * 8;
                blues[i] = ((color >> 10) % 0x20) * 8;
            }
        }
        /// <summary>
        /// Converts a 16x16 tileset into a pixel array.
        /// </summary>
        /// <param name="tileset">The tiles to draw from.</param>
        /// <param name="x">The X coordinate in the pixel array to begin drawing at.</param>
        /// <param name="y">The Y coordinate in the pixel array to begin drawing at.</param>
        /// <param name="width">The width, in 16x16 tile units, of the tileset.</param>
        /// <param name="height">The height, in 16x16 tile units, of the tileset.</param>
        /// <param name="startAtTile">The tile to start drawing from in the tileset</param>
        /// <param name="priority1">Sets whether to draw the tileset as a blue priority 1 silhouette.</param>
        /// <returns></returns>
        public static int[] TilesetToPixels(Tile[] tileset, int width, int height, int startAtTile, bool priority1)
        {
            int[] pixels = new int[(width * 16) * (height * 16)];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = (y * width + x) + startAtTile;
                    if (index >= tileset.Length)
                        continue;
                    if (!priority1)
                    {
                        for (int z = 0; z < 4; z++)
                        {
                            int X = (x * 16) + ((z % 2) * 8);
                            int Y = (y * 16) + ((z / 2) * 8);
                            Do.PixelsToPixels(tileset[index].Subtiles[z].Pixels,
                                pixels, width * 16, new Rectangle(X, Y, 8, 8));
                        }
                    }
                    else
                    {
                        Do.PixelsToPixels(tileset[index].Pixels_P1, pixels, width * 16,
                            new Rectangle(x * 16, y * 16, 16, 16));
                    }
                }
            }
            return pixels;
        }
        /// <summary>
        /// Determines whether a rectangle is within the bounds of a source rectangle.
        /// </summary>
        /// <param name="src">The source rectangle.</param>
        /// <param name="var">The rectangle to check.</param>
        /// <returns></returns>
        public static bool WithinBounds(Rectangle regionA, Rectangle regionB)
        {
            if (regionB.Left < regionA.Left || regionB.Right > regionA.Right ||
                regionB.Top < regionA.Top || regionB.Bottom > regionA.Bottom)
                return false;
            return true;
        }
        #endregion
        #region Coloring
        public static void RGBtoHSL(double r, double g, double b, out double h, out double s, out double l)
        {
            h = s = l = 0;
            double max = Math.Max(r, Math.Max(g, b));
            double min = Math.Min(r, Math.Min(g, b));
            l = (min + max) / 2.0;
            if (l <= 0.0)
                return;
            double dif = max - min; s = dif;
            if (s > 0.0)
                s /= (l <= 0.5) ? (max + min) : (2.0 - max - min);
            else return;
            double r2 = (max - r) / dif;
            double g2 = (max - g) / dif;
            double b2 = (max - b) / dif;
            if (r == max)
                h = (g == min ? 5.0 + b2 : 1.0 - g2);
            else if (g == max)
                h = (b == min ? 1.0 + r2 : 3.0 - b2);
            else
                h = (r == min ? 3.0 + g2 : 5.0 - r2);
            h /= 6.0;
        }
        public static void HSLtoRGB(double h, double s, double l, out double r, out double g, out double b)
        {
            r = g = b = l;   // default to gray
            double v = (l <= 0.5) ? (l * (1.0 + s)) : (l + s - l * s);
            if (v > 0)
            {
                double m = l + l - v;
                double sv = (v - m) / v;
                h *= 6.0;
                int sextant = (int)h;
                double fract = h - sextant;
                double vsf = v * sv * fract;
                double mid1 = m + vsf;
                double mid2 = v - vsf;
                switch (sextant)
                {
                    case 0:
                        r = v;
                        g = mid1;
                        b = m;
                        break;
                    case 1:
                        r = mid2;
                        g = v;
                        b = m;
                        break;
                    case 2:
                        r = m;
                        g = v;
                        b = mid1;
                        break;
                    case 3:
                        r = m;
                        g = mid2;
                        b = v;
                        break;
                    case 4:
                        r = mid1;
                        g = m;
                        b = v;
                        break;
                    case 5:
                        r = v;
                        g = m;
                        b = mid2;
                        break;
                }
            }
        }
        public static int HSLtoRGB(double h, double sl, double l)
        {
            double r = l;
            double g = l;
            double b = l;   // default to gray
            double v = (l <= 0.5) ? (l * (1.0 + sl)) : (l + sl - l * sl);
            if (v > 0)
            {
                double m = l + l - v;
                double sv = (v - m) / v;
                h *= 6.0;
                int sextant = (int)h;
                double fract = h - sextant;
                double vsf = v * sv * fract;
                double mid1 = m + vsf;
                double mid2 = v - vsf;
                switch (sextant)
                {
                    case 0:
                        r = v;
                        g = mid1;
                        b = m;
                        break;
                    case 1:
                        r = mid2;
                        g = v;
                        b = m;
                        break;
                    case 2:
                        r = m;
                        g = v;
                        b = mid1;
                        break;
                    case 3:
                        r = m;
                        g = mid2;
                        b = v;
                        break;
                    case 4:
                        r = mid1;
                        g = m;
                        b = v;
                        break;
                    case 5:
                        r = v;
                        g = m;
                        b = mid2;
                        break;
                }
            }
            return Color.FromArgb((int)r & 0xF8, (int)g & 0xF8, (int)b & 0xF8).ToArgb();
        }
        public static void RGBtoHSL(int[] r, int[] g, int[] b, out double[] h, out double[] s, out double[] l)
        {
            h = new double[r.Length];
            s = new double[r.Length];
            l = new double[r.Length];
            for (int i = 0; i < h.Length; i++)
            {
                double r_ = r[i];
                double g_ = g[i];
                double b_ = b[i];
                h[i] = s[i] = l[i] = 0;
                double max = Math.Max(r_, Math.Max(g_, b_));
                double min = Math.Min(r_, Math.Min(g_, b_));
                l[i] = (min + max) / 2.0;
                if (l[i] <= 0.0)
                    return;
                double dif = max - min; s[i] = dif;
                if (s[i] > 0.0)
                    s[i] /= (l[i] <= 0.5) ? (max + min) : (2.0 - max - min);
                else return;
                double r2 = (max - r_) / dif;
                double g2 = (max - g_) / dif;
                double b2 = (max - b_) / dif;
                if (r_ == max)
                    h[i] = (g_ == min ? 5.0 + b2 : 1.0 - g2);
                else if (g[i] == max)
                    h[i] = (b_ == min ? 1.0 + r2 : 3.0 - b2);
                else
                    h[i] = (r_ == min ? 3.0 + g2 : 5.0 - r2);
                h[i] /= 6.0;
            }
        }
        /// <summary>
        /// Reduces the color depth of a pixel array. Returns a newly created palette.
        /// </summary>
        /// <param name="src">The pixel array.</param>
        /// <param name="depth">The new color depth.</param>
        /// <param name="transparent">The transparent color.</param>
        public static int[] ReduceColorDepth(int[] src, int depth, int transparent)
        {
            List<int> colors = new List<int>();
            Color darkest = Color.FromArgb(255, 255, 255, 255);
            Color lightest = Color.FromArgb(255, 0, 0, 0);
            foreach (int pixel in src)
            {
                Color color = Color.FromArgb(pixel);
                // skip if opacity not full
                if (color.A < 255)
                    continue;
                // find the brightest and darkest colors, the new palette needs them
                if (color.GetBrightness() > lightest.GetBrightness())
                    lightest = color;
                if (color.GetBrightness() < darkest.GetBrightness())
                    darkest = color;
                if (!colors.Contains(pixel))
                    colors.Add(color.ToArgb());
            }
            int[] palette = new int[depth];
            // if color amount less than depth, simply add all colors to palette and return
            if (colors.Count < depth)
            {
                palette[0] = transparent;
                for (int i = 1, a = 0; a < colors.Count; i++, a++)
                    palette[i] = colors[a];
            }
            // find the median colors in the list of colors for a total based on the depth
            else
            {
                colors.Sort();
                int increment = colors.Count / (depth - 1);
                for (int i = 0, p = 2; i < colors.Count && p < palette.Length - 1; i += increment, p++)
                    palette[p] = colors[i];
                palette[0] = transparent;
                palette[1] = lightest.ToArgb();
                palette[depth - 1] = darkest.ToArgb();
            }
            for (int i = 0; i < palette.Length; i++)
                palette[i] &= unchecked((int)0xFFF8F8F8);
            return palette;
        }
        /// <summary>
        /// Colorize a pixel array.
        /// </summary>
        /// <param name="src">The pixel array to colorize.</param>
        /// <param name="h">Hue (ie. the color).</param>
        /// <param name="s">Saturation (ie. intensity of color).</param>
        /// <param name="alpha">The opacity of the array.</param>
        public static void Colorize(int[] src, double h, double s, double l_, int alpha)
        {
            h /= 360.0;
            l_ /= 255.0;
            for (int i = 0; i < src.Length; i++)
            {
                if (src[i] == 0)
                    continue;
                Color color = Color.FromArgb(src[i]);
                double l = Math.Max(0, color.GetBrightness() + l_);
                double r = 0, g = 0, b = 0;
                double temp1, temp2;
                if (l == 0)
                {
                    r = g = b = 0;
                }
                else
                {
                    if (s == 0)
                    {
                        r = g = b = l;
                    }
                    else
                    {
                        temp2 = ((l <= 0.5) ? l * (1.0 + s) : l + s - (l * s));
                        temp1 = 2.0 * l - temp2;
                        double[] t3 = new double[] { h + 1.0 / 3.0, h, h - 1.0 / 3.0 };
                        double[] clr = new double[] { 0, 0, 0 };
                        for (int a = 0; a < 3; a++)
                        {
                            if (t3[a] < 0)
                                t3[a] += 1.0;
                            if (t3[a] > 1)
                                t3[a] -= 1.0;
                            if (6.0 * t3[a] < 1.0)
                                clr[a] = temp1 + (temp2 - temp1) * t3[a] * 6.0;
                            else if (2.0 * t3[a] < 1.0)
                                clr[a] = temp2;
                            else if (3.0 * t3[a] < 2.0)
                                clr[a] = (temp1 + (temp2 - temp1) * ((2.0 / 3.0) - t3[a]) * 6.0);
                            else
                                clr[a] = temp1;
                        }
                        r = clr[0];
                        g = clr[1];
                        b = clr[2];
                    }
                }
                src[i] = Color.FromArgb(alpha, (byte)(r * 255), (byte)(g * 255), (byte)(b * 255)).ToArgb();
            }
        }
        public static void Colorize(int[] src, double h, double s)
        {
            Colorize(src, h, s, 0.0, 255);
        }
        public static Bitmap Fill(Bitmap image, Color fill)
        {
            int[] pixels = ImageToPixels(image);
            for (int i = 0; i < pixels.Length; i++)
                if (pixels[i] >> 24 != 0)
                    pixels[i] = fill.ToArgb();
            return PixelsToImage(pixels, image.Width, image.Height);
        }
        public static Color HSLtoRGBColor(double h, double s, double l)
        {
            double r = 0, g = 0, b = 0;
            double temp1, temp2;
            if (l == 0)
            {
                r = g = b = 0;
            }
            else
            {
                if (s == 0)
                {
                    r = g = b = l;
                }
                else
                {
                    temp2 = ((l <= 0.5) ? l * (1.0 + s) : l + s - (l * s));
                    temp1 = 2.0 * l - temp2;
                    double[] t3 = new double[] { h + 1.0 / 3.0, h, h - 1.0 / 3.0 };
                    double[] clr = new double[] { 0, 0, 0 };
                    for (int a = 0; a < 3; a++)
                    {
                        if (t3[a] < 0)
                            t3[a] += 1.0;
                        if (t3[a] > 1)
                            t3[a] -= 1.0;
                        if (6.0 * t3[a] < 1.0)
                            clr[a] = temp1 + (temp2 - temp1) * t3[a] * 6.0;
                        else if (2.0 * t3[a] < 1.0)
                            clr[a] = temp2;
                        else if (3.0 * t3[a] < 2.0)
                            clr[a] = (temp1 + (temp2 - temp1) * ((2.0 / 3.0) - t3[a]) * 6.0);
                        else
                            clr[a] = temp1;
                    }
                    r = clr[0];
                    g = clr[1];
                    b = clr[2];
                }
            }
            return Color.FromArgb((int)(r * 255.0) & 0xF8, (int)(g * 255.0) & 0xF8, (int)(b * 255.0) & 0xF8);
        }
        /// <summary>
        /// Apply a gradient effect to a pixel array.
        /// </summary>
        /// <param name="src">The pixel array to modify.</param>
        /// <param name="width">Width, in pixels, of the array.</param>
        /// <param name="height">Height, in pixels, of the array.</param>
        /// <param name="lo">Brightness level to start at.</param>
        /// <param name="hi">Brightness level to end at.</param>
        /// <param name="vert">If set, gradient moves vertically; otherwise horizontally.</param>
        /// <param name="dark">If set, gradient darkens; otherwise lightens.</param>
        public static void Gradient(int[] src, int width, int height, double lo, double hi, bool vert)
        {
            double range = Math.Abs(hi - lo);
            double l = lo;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (src[y * width + x] == 0)
                        continue;
                    Color c = Color.FromArgb(src[y * width + x]);
                    int r = (int)Math.Min(255, Math.Max(0, c.R + l));
                    int g = (int)Math.Min(255, Math.Max(0, c.G + l));
                    int b = (int)Math.Min(255, Math.Max(0, c.B + l));
                    src[y * width + x] = Color.FromArgb((byte)r, (byte)g, (byte)b).ToArgb();
                    if (!vert)
                        GradientAdjust(ref l, width, lo > hi, range);
                }
                if (vert)
                    GradientAdjust(ref l, height, lo > hi, range);
                else
                    l = 0;
            }
        }
        private static void GradientAdjust(ref double l, int unit, bool dark, double range)
        {
            if (dark)
                l -= range / (double)unit;
            else
                l += range / (double)unit;
        }
        public static void Border(int[] src, int width, int height, int size, Color color)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (src[y * width + x] == 0)
                        continue;
                    //Color c = Color.FromArgb(src[y * width + x]);
                    //int r = Math.Min(255, c.R + 32);
                    //int g = Math.Min(255, c.G + 32);
                    //int b = Math.Min(255, c.B + 32);
                    //int n = Color.FromArgb(r, g, b).ToArgb();
                    int n = color.ToArgb();
                    for (int e = size; e > 0; e--)
                    {
                        if (x - e < 0 || src[y * width + x - e] == 0)
                            src[y * width + x] = n;
                        if (x + e >= width || src[y * width + x + e] == 0)
                            src[y * width + x] = n;
                        if (y - e < 0 || src[(y - e) * width + x] == 0)
                            src[y * width + x] = n;
                        if (y + e >= height || src[(y + e) * width + x] == 0)
                            src[y * width + x] = n;
                    }
                }
            }
        }
        public static void Opacity(int[] src, int width, int height, byte opacity)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (src[y * width + x] == 0)
                        continue;
                    src[y * width + x] &= 0xFFFFFF;
                    src[y * width + x] |= opacity << 24;
                }
            }
        }
        public static void Stipple(int[] src, int width, int height)
        {
            int e = 2;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (src[y * width + x] == 0)
                        continue;
                    if (x - e >= 0 && src[y * width + x - e] != 0 &&
                        x + e < width && src[y * width + x + e] != 0 &&
                        y - e >= 0 && src[(y - e) * width + x] != 0 &&
                        y + e < height && src[(y + e) * width + x] != 0)
                        src[y * width + x] = 0;
                    //else if (x + e >= width || src[y * width + x + e] == 0)
                    //    src[y * width + x] = 0;
                    //else if (y - e < 0 || src[(y - e) * width + x] == 0)
                    //    src[y * width + x] = 0;
                    //else if (y + e >= height || src[(y + e) * width + x] == 0)
                    //    src[y * width + x] = 0;
                }
            }
        }
        public static void Tint(int[] src, Color tint)
        {
            for (int i = 0; i < src.Length; i++)
            {
                if (src[i] == 0) continue;
                Color color = Color.FromArgb(src[i]);
                int r = Math.Min(255, color.R + (tint.R / 2));
                int g = Math.Min(255, color.G + (tint.G / 2));
                int b = Math.Min(255, color.B + (tint.B / 2));
                src[i] = Color.FromArgb(r, g, b).ToArgb();
            }
        }
        public static Bitmap Hilite(Bitmap image, int width, int height)
        {
            int[] src = Do.ImageToPixels(image);
            int[] dst = Hilite(src, width, height);
            return Do.PixelsToImage(dst, width, height);
        }
        public static int[] Hilite(int[] src, int width, int height)
        {
            int[] dst = new int[src.Length];
            for (int i = 0; i < src.Length; i++)
            {
                if (src[i] == 0) continue;
                Color color = Color.FromArgb(src[i]);
                int l = (int)(color.GetBrightness() * 255);
                int r = Math.Min(255, 255 + l);
                int g = Math.Min(255, l);
                int b = Math.Min(255, 255 + l);
                dst[i] = Color.FromArgb(r, g, b).ToArgb();
            }
            return dst;
        }
        #endregion
        #region Text
        /// <summary>
        /// Returns a value indicating whether a collection of objects contains a specific object.
        /// </summary>
        /// <param name="value">The object to search for.</param>
        /// <param name="collection">The collection of objects to search.</param>
        /// <returns></returns>
        public static bool Contains(object value, object[] collection)
        {
            foreach (object item in collection)
                if (item.GetType() == typeof(ArrayList))
                {
                    foreach (object arrayItem in (ArrayList)item)
                        if (arrayItem == value)
                            return true;
                }
                else if (item == value)
                    return true;
            return false;
        }
        public static bool Contains(object value, ArrayList collection)
        {
            foreach (object item in collection)
                if (item.GetType() == typeof(ArrayList))
                {
                    foreach (object arrayItem in (ArrayList)item)
                        if (arrayItem == value)
                            return true;
                }
                else if (item == value)
                    return true;
            return false;
        }
        public static bool Contains(string original, string value, StringComparison comparisionType)
        {
            return original.IndexOf(value, comparisionType) >= 0;
        }
        public static bool Contains(string original, string value, StringComparison comparisionType, out int index)
        {
            index = original.IndexOf(value, comparisionType);
            return index >= 0;
        }
        public static bool Contains(string original, string value, StringComparison comparisionType, bool matchWholeWord)
        {
            int index = original.IndexOf(value, comparisionType);
            if (!matchWholeWord)
                return index >= 0;
            else if (index >= 0)
            {
                if (index + value.Length < original.Length && Char.IsLetter(original, index + value.Length))
                    return false;
                if (index - 1 >= 0 && Char.IsLetter(original, index - 1))
                    return false;
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// Searches for an occurrence of a tile within a tileset and returns the index of the occurrence if found.
        /// Otherwise it returns the index of the tile being searched for.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static int Contains(Tile value, Tile[] collection)
        {
            foreach (Tile item in collection)
                if (item == value)
                    return value.Index;
                else if (Bits.Compare(item.Pixels, value.Pixels))
                    return item.Index;
            return value.Index;
        }
        /// <summary>
        /// Converts an ASCII format string into a raw char array using a keystroke table.
        /// </summary>
        /// <param name="text">The ASCII string to convert.</param>
        /// <param name="keystrokes">The keystroke table to use.</param>
        /// <param name="length">The maximum length of the converted char array.</param>
        /// <returns></returns>
        public static char[] ASCIIToRaw(string text, string[] keystrokes, int length)
        {
            char[] temp = new char[length];
            int i = 0;
            for (; i < temp.Length && i < text.Length; i++)
            {
                for (int a = 0; a < keystrokes.Length; a++)
                {
                    if (keystrokes[a] == text.Substring(i, 1))
                        temp[i] = (char)a;
                }
            }
            // pad with spaces
            for (; i < temp.Length; i++)
                temp[i] = '\x20';
            return temp;
        }
        public static char[] ASCIIToRaw(string text, string[] keystrokes)
        {
            return ASCIIToRaw(text, keystrokes, text.Length);
        }
        /// <summary>
        /// Convert a raw char array to viewable ASCII format using a table of keystrokes.
        /// </summary>
        /// <param name="chars">The char array to convert.</param>
        /// <param name="keystrokes">The keystroke table to use.</param>
        /// <returns></returns>
        public static string RawToASCII(char[] chars, string[] keystrokes)
        {
            string temp = "";
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] >= keystrokes.Length)
                    continue;
                if (keystrokes[chars[i]] == "")
                    temp += "_";
                temp += keystrokes[chars[i]];
            }
            return temp;
        }
        /// <summary>
        /// Search for a string in an array of string, and add every instance to a specified listbox.
        /// Each item in the listbox will be tagged with the respective index;
        /// </summary>
        /// <param name="names">The list of names to search.</param>
        /// <param name="name">The string to search for.</param>
        /// <param name="listBox">The listbox to write to.</param>
        /// <param name="ignoreCase">Specifies whether to ignore case when searching.</param>
        public static void Search(ComboBox.ObjectCollection names, string name, ListBox listBox, bool ignoreCase)
        {
            listBox.BeginUpdate();
            listBox.Items.Clear();
            if (name == "")
            {
                listBox.EndUpdate();
                return;
            }
            for (int i = 0; i < names.Count; i++)
            {
                if (((string)names[i]).IndexOf(name, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    listBox.Items.Add(names[i]);
            }
            listBox.EndUpdate();
            listBox.BringToFront();
        }
        public static void Search(object listControl, string name, bool ignoreCase)
        {
        }
        public static int IndexOf(string[] collection, string item)
        {
            for (int i = 0; i < collection.Length; i++)
                if (item == collection[i])
                    return i;
            return -1;
        }
        public static string EventScriptToText(EventScript eventScript, int lines, int length)
        {
            return EventScriptToText(eventScript, eventScript.BaseOffset, lines, length);
        }
        public static string EventScriptToText(EventScript eventScript, int offset, int lines, int length)
        {
            if (eventScript == null)
                return "<INVALID EVENT SCRIPT>\n";
            StringBuilder sb = new StringBuilder();
            List<EventCommand> scriptCmds;
            List<ActionCommand> actionQueues;
            EventCommand esc;
            ActionCommand asc;
            string command;
            int line = 0;
            scriptCmds = eventScript.Commands;
            for (int i = 0; i < scriptCmds.Count && line < lines; i++)
            {
                esc = scriptCmds[i];
                if (esc.Offset + esc.Length <= offset)
                    continue;
                command = esc.ToString();
                if (command.Length > length)
                    command = command.Remove(length) + "...";
                sb.Append(command + "\n");
                line++;
                if (esc.Queue != null)
                {
                    actionQueues = esc.Queue.Commands;
                    for (int a = 0; a < actionQueues.Count && line < lines; a++, line++)
                    {
                        asc = actionQueues[a];
                        command = asc.ToString();
                        if (command.Length > length)
                            command = command.Remove(length) + "...";
                        sb.Append("   " + command + "\n");
                    }
                }
                line++;
            }
            if (line >= lines)
            {
                sb.AppendLine("...");
            }
            return sb.ToString();
        }
        public static string WordWrap(string text, int width)
        {
            return WordWrap(text, width, 0);
        }
        public static string WordWrap(string text, int width, int indent)
        {
            int pos, next;
            string pad = String.Empty.PadLeft(indent, ' ');
            StringBuilder sb = new StringBuilder();
            // Lucidity check
            if (width < 1)
                return text;
            // Parse each line of text
            for (pos = 0; pos < text.Length; pos = next)
            {
                // Find end of line
                int eol = text.IndexOf(Environment.NewLine, pos);
                if (eol == -1)
                    next = eol = text.Length;
                else
                    next = eol + Environment.NewLine.Length;
                // Copy this line of text, breaking into smaller lines as needed
                if (eol > pos)
                {
                    do
                    {
                        int len = eol - pos;
                        if (len > width)
                            len = BreakLine(text, pos, width);
                        sb.Append(text, pos, len);
                        sb.Append(Environment.NewLine + pad);
                        // Trim whitespace following break
                        pos += len;
                        while (pos < eol && Char.IsWhiteSpace(text[pos]))
                            pos++;
                    }
                    while (eol > pos);
                }
                else
                    sb.Append(Environment.NewLine + pad); // Empty line
            }
            // Removes extra line
            sb.Remove(sb.Length - indent - Environment.NewLine.Length, indent + Environment.NewLine.Length);
            return sb.ToString();
        }
        private static int BreakLine(string text, int pos, int max)
        {
            // Find last whitespace in line
            int i = max;
            while (i >= 0 && !Char.IsWhiteSpace(text[pos + i]))
                i--;
            // If no whitespace found, break at maximum length
            if (i < 0)
                return max;
            // Find start of whitespace
            while (i >= 0 && Char.IsWhiteSpace(text[pos + i]))
                i--;
            // Return length of text before whitespace
            return i + 1;
        }
        #endregion
        #region Data Managing
        private static BackgroundWorker Export_Worker;
        private static BackgroundWorker Import_Worker;
        /// <summary>
        /// Exports an element to a file.
        /// </summary>
        /// <param name="element">The element to export.</param>
        /// <param name="fileName">Ignored. Set this to null when passing parameter.</param>
        /// <param name="fullPath">The full local path, including the filename with the extension.</param>
        public static void Export(object element, string fileName, string fullPath)
        {
            if (element.GetType() == typeof(byte[]))
            {
                FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write((byte[])element);
                bw.Close();
                fs.Close();
            }
            else if (element.GetType() == typeof(Bitmap))
                ((Bitmap)element).Save(fullPath, ImageFormat.Png);
            else
            {
                BinaryFormatter bf = new BinaryFormatter();
                Stream s = File.Create(fullPath);
                bf.Serialize(s, element);
                s.Close();
            }
        }
        /// <summary>
        /// Exports an element to a file.
        /// </summary>
        /// <param name="element">The element to export.</param>
        /// <param name="fileName">The filename to save as, with the extension.</param>
        public static void Export(object element, string fileName)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = fileName;
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            if (element.GetType() == typeof(byte[]))
            {
                saveFileDialog.Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*";
                saveFileDialog.Title = "Export";
                if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                    return;
                FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write((byte[])element);
                bw.Close();
                fs.Close();
            }
            else if (element.GetType() == typeof(Bitmap))
            {
                saveFileDialog.Filter = "Image file (*.png)|*.png|All files (*.*)|*.*";
                saveFileDialog.Title = "Save Image";
                if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                    return;
                ((Bitmap)element).Save(saveFileDialog.FileName, ImageFormat.Png);
            }
            else
            {
                saveFileDialog.Filter = "Data file (*.dat)|*.dat|All files (*.*)|*.*";
                saveFileDialog.Title = "Export";
                if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                    return;
                BinaryFormatter bf = new BinaryFormatter();
                Stream s = File.Create(saveFileDialog.FileName);
                bf.Serialize(s, element);
                s.Close();
            }
        }
        /// <summary>
        /// Exports a set of elements to files to a specified folder.
        /// </summary>
        /// <param name="elements">The elements to export.</param>
        /// <param name="fileName">The base filename to export as, without an extension.</param>
        /// <param name="folder">The folder to create.</param>
        /// <param name="type">The type of element. Preferably in all caps and singular form.</param>
        /// <param name="showProgressBar">Sets whether or not to show the progress bar when exporting.</param>
        public static void Export(object[] elements, string fileName, string folder, string type, bool showProgressBar)
        {
            // first, open and create directory
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.SelectedPath = Settings.Default.LastDirectory;
            folderBrowserDialog1.Description = "Select directory to export to";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                Settings.Default.LastDirectory = folderBrowserDialog1.SelectedPath;
            else
                return;
            string fullPath = folderBrowserDialog1.SelectedPath + "\\" + folder + "\\" + fileName;
            Export(elements, fullPath, type, showProgressBar);
        }
        /// <summary>
        /// Exports a set of elements to files to a specified full path of a local folder.
        /// </summary>
        /// <param name="elements">The elements to export.</param>
        /// <param name="fullPath">The local path of the folder to export to, plus the filename without the index or extension.</param>
        /// <param name="type">The type of element. Preferably in all caps and singular form.</param>
        /// <param name="showProgressBar">Sets whether or not to show the progress bar when exporting.</param>
        public static void Export(object[] elements, string fullPath, string type, bool showProgressBar)
        {
            FileInfo fi = new FileInfo(fullPath);
            DirectoryInfo di = new DirectoryInfo(fi.DirectoryName);
            if (!di.Exists)
                di.Create();
            // set the backgroundworker properties
            Do.Export_Worker = new BackgroundWorker();
            Do.Export_Worker.WorkerReportsProgress = true;
            Do.Export_Worker.WorkerSupportsCancellation = true;
            Do.Export_Worker.DoWork += (s, e) => Export_Worker_DoWork(s, e, elements, fullPath);
            Do.Export_Worker.ProgressChanged += (s, e) => Export_Worker_ProgressChanged(s, e, type);
            Do.Export_Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Export_Worker_RunWorkerCompleted);
            if (showProgressBar)
            {
                ProgressBar = new ProgressBar("EXPORTING " + type + "S...", elements.Length, Export_Worker);
                ProgressBar.Show();
            }
            Export_Worker.RunWorkerAsync();
            while (Export_Worker.IsBusy)
                Application.DoEvents();
        }
        private static void Export_Worker_DoWork(object sender, DoWorkEventArgs e, object[] elements, string fullPath)
        {
            // Create the files
            for (int i = 0; i < elements.Length; i++)
            {
                if (Export_Worker.CancellationPending)
                    return;
                Export_Worker.ReportProgress(i);
                object element = elements[i];
                // if saving images
                if (element.GetType() == typeof(Bitmap))
                {
                    ((Bitmap)element).Save(
                        fullPath + "." + i.ToString("d" + elements.Length.ToString().Length) + ".png", ImageFormat.Png);
                }
                // if a byte[] array, then export to .bin
                else if (element.GetType() == typeof(byte[]))
                {
                    FileStream fs = new FileStream(
                        fullPath + "." + i.ToString("d" + elements.Length.ToString().Length) + ".bin",
                        FileMode.Create, FileAccess.ReadWrite);
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.Write((byte[])elements[i]);
                    bw.Close();
                    fs.Close();
                }
                // otherwise, export to .dat
                else
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    Stream s = File.Create(
                        fullPath + "." + i.ToString("d" + elements.Length.ToString().Length) + ".dat");
                    bf.Serialize(s, element);
                    s.Close();
                }
            }
        }
        private static void Export_Worker_ProgressChanged(object sender, ProgressChangedEventArgs e, string type)
        {
            if (ProgressBar != null && ProgressBar.Visible)
                ProgressBar.PerformStep("EXPORTING " + type + " #" + e.ProgressPercentage);
        }
        private static void Export_Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (ProgressBar != null && ProgressBar.Visible)
                ProgressBar.Close();
        }
        /// <summary>
        /// Imports a file into an element.
        /// </summary>
        /// <param name="element">The element to import to.</param>
        /// <param name="fileName">Ignored. Set this to null when passing parameter.</param>
        /// <param name="fullPath">The full local path, including the filename.</param>
        public static object Import(object element, string fullPath)
        {
            if (element.GetType() == typeof(byte[]))
            {
                FileStream fs = File.OpenRead(fullPath);
                BinaryReader br = new BinaryReader(fs);
                element = br.ReadBytes((int)fs.Length);
                br.Close();
                fs.Close();
            }
            else if (element.GetType() == typeof(Bitmap))
            {
                element = new Bitmap(Image.FromFile(fullPath));
            }
            else
            {
                Stream s = File.OpenRead(fullPath);
                BinaryFormatter bf = new BinaryFormatter();
                element = bf.Deserialize(s);
                s.Close();
            }
            return element;
        }
        public static object[] Import(object[] elements, string[] fullPaths)
        {
            if (elements.GetType() == typeof(byte[][]))
            {
                elements = new byte[fullPaths.Length][];
                for (int i = 0; i < fullPaths.Length; i++)
                {
                    FileStream fs = File.OpenRead(fullPaths[i]);
                    BinaryReader br = new BinaryReader(fs);
                    elements[i] = br.ReadBytes((int)fs.Length);
                    br.Close();
                    fs.Close();
                }
            }
            else if (elements.GetType() == typeof(Bitmap[]))
            {
                elements = new Bitmap[fullPaths.Length];
                for (int i = 0; i < fullPaths.Length; i++)
                {
                    elements[i] = new Bitmap(Image.FromFile(fullPaths[i]));
                }
            }
            else
            {
                elements = new object[fullPaths.Length];
                for (int i = 0; i < fullPaths.Length; i++)
                {
                    Stream s = File.OpenRead(fullPaths[i]);
                    BinaryFormatter bf = new BinaryFormatter();
                    elements[i] = bf.Deserialize(s);
                    s.Close();
                }
            }
            return elements;
        }
        /// <summary>
        /// Imports a file into an element.
        /// </summary>
        /// <param name="element">The element to import to.</param>
        /// <param name="fileName">The file to import from.</param>
        public static object Import(object element)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (element.GetType() == typeof(byte[]))
                openFileDialog.Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*";
            else if (element.GetType() == typeof(Bitmap))
                openFileDialog.Filter = "Image files (*.gif,*.jpg,*.png)|*.gif;*.jpg;*.png|All files (*.*)|*.*";
            else
                openFileDialog.Filter = "Data files (*.dat)|*.dat|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Title = "Import";
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return null;
            return Import(element, openFileDialog.FileName);
        }
        public static object[] Import(object[] elements)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if (elements.GetType() == typeof(byte[][]))
                openFileDialog.Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*";
            else if (elements.GetType() == typeof(Bitmap[]))
                openFileDialog.Filter = "Image files (*.gif,*.jpg,*.png)|*.gif;*.jpg;*.png|All files (*.*)|*.*";
            else
                openFileDialog.Filter = "Data files (*.dat)|*.dat|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Title = "Import";
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return null;
            return Import(elements, openFileDialog.FileNames);
        }
        /// <summary>
        /// Imports a file into an element from a set of files in a specified folder.
        /// </summary>
        /// <param name="element">The elements to import to.</param>
        /// <param name="fileName">The base filename to import as, without an extension.</param>
        /// <param name="folder">The folder to import from.</param>
        /// <param name="type">The type of element. Preferably in all caps and singular form.</param>
        /// <param name="showProgressBar">Sets whether or not to show the progress bar when importing.</param>
        public static void Import(object[] elements, string fileName, string folder, string type, bool showProgressBar)
        {
            // first, open and create directory
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.SelectedPath = Settings.Default.LastDirectory;
            folderBrowserDialog1.Description = "Select directory to import from";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                Settings.Default.LastDirectory = folderBrowserDialog1.SelectedPath;
            else
                return;
            string fullPath = folderBrowserDialog1.SelectedPath + "\\" + folder + "\\" + fileName;
            Import(elements, fullPath, type, showProgressBar);
        }
        /// <summary>
        /// Imports files into a set of elements from a set of files in a specified full path of a local folder.
        /// </summary>
        /// <param name="element">The elements to import to.</param>
        /// <param name="fullPath">The local path of the folder to import from, plus the filename without the index or extension.</param>
        /// <param name="type">The type of element. Preferably in all caps and singular form.</param>
        /// <param name="showProgressBar">Sets whether or not to show the progress bar when importing.</param>
        public static void Import(object[] elements, string fullPath, string type, bool showProgressBar)
        {
            // set the backgroundworker properties
            Do.Import_Worker = new BackgroundWorker();
            Do.Import_Worker.WorkerReportsProgress = true;
            Do.Import_Worker.WorkerSupportsCancellation = true;
            Do.Import_Worker.DoWork += (s, e) => Import_Worker_DoWork(s, e, elements, fullPath);
            Do.Import_Worker.ProgressChanged += (s, e) => Import_Worker_ProgressChanged(s, e, type);
            Do.Import_Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Import_Worker_RunWorkerCompleted);
            if (showProgressBar)
            {
                ProgressBar = new ProgressBar("EXPORTING " + type + "S...", elements.Length, Export_Worker);
                ProgressBar.Show();
            }
            Import_Worker.RunWorkerAsync();
            while (Import_Worker.IsBusy)
                Application.DoEvents();
        }
        private static void Import_Worker_DoWork(object sender, DoWorkEventArgs e, object[] elements, string fullPath)
        {
            // Create the files
            for (int i = 0; i < elements.Length; i++)
            {
                if (Import_Worker.CancellationPending)
                    return;
                Import_Worker.ReportProgress(i);
                // if a byte[] array, then import as .bin
                if (elements[i].GetType() == typeof(byte[]))
                {
                    if (!File.Exists(fullPath + "." + i.ToString("d" + elements.Length.ToString().Length) + ".bin"))
                        continue;
                    FileStream fs = File.OpenRead(fullPath + "." + i.ToString("d" + elements.Length.ToString().Length) + ".bin");
                    BinaryReader br = new BinaryReader(fs);
                    elements[i] = new byte[fs.Length];
                    br.ReadBytes((int)fs.Length).CopyTo((byte[])elements[i], 0);
                    br.Close();
                    fs.Close();
                }
                // otherwise, import as .dat
                else
                {
                    if (!File.Exists(fullPath + "." + i.ToString("d" + elements.Length.ToString().Length) + ".dat"))
                        continue;
                    Stream s = File.OpenRead(fullPath + "." + i.ToString("d" + elements.Length.ToString().Length) + ".dat");
                    BinaryFormatter bf = new BinaryFormatter();
                    elements[i] = bf.Deserialize(s);
                    s.Close();
                }
            }
        }
        private static void Import_Worker_ProgressChanged(object sender, ProgressChangedEventArgs e, string type)
        {
            if (ProgressBar != null && ProgressBar.Visible)
                ProgressBar.PerformStep("IMPORTING " + type + " #" + e.ProgressPercentage);
        }
        private static void Import_Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (ProgressBar != null && ProgressBar.Visible)
                ProgressBar.Close();
        }
        public static void WriteToTXT(string text, string filename)
        {
            StreamWriter writer = File.CreateText(filename);
            writer.Write(text);
            writer.Close();
        }
        #endregion
        #region .NET Controls
        public static void AddControl(Control parent, Form child)
        {
            child.TopLevel = false;
            parent.Controls.Add(child);
            //child.WindowState = FormWindowState.Maximized;
            child.Show();
            child.BringToFront();
        }
        public static void RemoveControl(Form child)
        {
            child.WindowState = FormWindowState.Normal;
            child.Parent = null;
            child.TopLevel = true;
            child.Location = new Point(5, 5);
        }
        /// <summary>
        /// Add a shortcut key to a toolstrip.
        /// </summary>
        /// <param name="toolStrip">The toolstrip to add to.</param>
        /// <param name="keys">The shortcut key.</param>
        /// <param name="eventHandler">The event handler to invoke when the shortcut is activated.</param>
        public static void AddShortcut(ToolStrip toolStrip, Keys keys, EventHandler eventHandler)
        {
            ToolStripMenuItem shortcut = new ToolStripMenuItem();
            shortcut.ShortcutKeys = keys;
            shortcut.Visible = false;
            shortcut.Click += eventHandler;
            toolStrip.Items.Add(shortcut);
            //ToolStripMenuItem screencap = new ToolStripMenuItem();
            //screencap.ShortcutKeys = Keys.F3;
            //screencap.Visible = false;
            //screencap.Click += CaptureScreen_Click;
        }
        public static void AddShortcut(ToolStrip toolStrip, Keys keys, ToolStripButton checkable)
        {
            ToolStripMenuItem shortcut = new ToolStripMenuItem();
            shortcut.ShortcutKeys = keys;
            shortcut.Visible = false;
            shortcut.Click += (s, e) => CheckButtonEvent(s, e, checkable);
            toolStrip.Items.Add(shortcut);
        }
        private static void CheckButtonEvent(object sender, EventArgs e, ToolStripButton button)
        {
            button.Checked = !button.Checked;
        }
        /// <summary>
        /// Resizes a label control to fit its text, since the .NET auto-sizing for label sucks.
        /// </summary>
        /// <param name="label">The label control to resize.</param>
        public static void AutoSizeLabel(Label label)
        {
            Size size = TextRenderer.MeasureText(label.Text, label.Font);
            label.Width = size.Width + 4;
            label.Height = size.Height + 4;
        }
        public static void SelectAllNodes(TreeNodeCollection nodes, bool selected)
        {
            foreach (TreeNode tn in nodes)
            {
                tn.Checked = selected;
                SelectAllNodes(tn.Nodes, selected);
            }
        }
        public static void SelectAll(Control control, bool selected)
        {
            if (control.GetType() == typeof(CheckBox))
                ((CheckBox)control).Checked = selected;
            foreach (Control child in control.Controls)
                SelectAll(child, selected);
        }
        /// <summary>
        /// Enable or disable all or some controls within a parent control, starting at the parent control.
        /// Returns the controls that already have the enable status set.
        /// </summary>
        /// <param name="main">The main parent controls.</param>
        /// <param name="enable">Enable or disable the controls.</param>
        /// <param name="childOnly">If set to true, only controls that contain no child controls will be modified.</param>
        /// <param name="skip">The controls to ignore when changing enabled status.</param>
        public static ArrayList EnableControls(object main, bool enable, bool childOnly, bool firstLoop, params object[] skip)
        {
            if (firstLoop)
                set = new ArrayList();
            if (main.GetType() == typeof(ToolStrip))
                foreach (ToolStripItem item in ((ToolStrip)main).Items)
                {
                    if (!Contains(item, skip))
                        if (item.Enabled == enable)
                            set.Add(item);
                        else
                            item.Enabled = enable;
                }
            else
                foreach (Control parent in ((Control)main).Controls)
                {
                    if (parent.Controls.Count == 0 || !childOnly && !Contains(parent, skip))
                        if (parent.Enabled == enable)
                            set.Add(parent);
                        else
                            parent.Enabled = enable;
                    EnableControls(parent, enable, childOnly, false, skip);
                }
            return set;
        }
        private static ArrayList set = new ArrayList();
        // Get / set the scrollbar position of the treeview
        [DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int GetScrollPos(int hWnd, int nBar);
        [DllImport("user32.dll")]
        public static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);
        private const int SB_HORZ = 0x0;
        private const int SB_VERT = 0x1;
        public static Point GetTreeViewScrollPos(TreeView treeView)
        {
            return new Point(
                GetScrollPos((int)treeView.Handle, SB_HORZ),
                GetScrollPos((int)treeView.Handle, SB_VERT));
        }
        public static void SetTreeViewScrollPos(TreeView treeView, Point scrollPosition)
        {
            SetScrollPos((IntPtr)treeView.Handle, SB_HORZ, scrollPosition.X, true);
            SetScrollPos((IntPtr)treeView.Handle, SB_VERT, scrollPosition.Y, true);
        }
        public static void RemoveClickEvent(ToolStripMenuItem b)
        {
            FieldInfo f1 = typeof(Control).GetField("EventClick",
                BindingFlags.Static | BindingFlags.NonPublic);
            object obj = f1.GetValue(b);
            PropertyInfo pi = b.GetType().GetProperty("Events",
                BindingFlags.NonPublic | BindingFlags.Instance);
            EventHandlerList list = (EventHandlerList)pi.GetValue(b, null);
            list.RemoveHandler(obj, list[obj]);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listView"></param>
        /// <param name="unsorted">Return the item's original unsorted index</param>
        /// <returns></returns>
        public static int GetSelectedIndex(ListView listView, bool unsorted)
        {
            for (int i = 0; i < listView.Items.Count; i++)
                if (listView.Items[i].Selected)
                    return unsorted ? (int)listView.Items[i].Tag : i;
            return -1;
        }
        public static int GetSelectedIndex(ListView listView)
        {
            return GetSelectedIndex(listView, false);
        }
        public static void ResetToolStripButtons(ToolStrip toolstrip, ToolStripButton skip1, ToolStripButton skip2)
        {
            foreach (ToolStripItem item in toolstrip.Items)
                if (item.GetType() == typeof(ToolStripButton))
                    if (item != skip1 && item != skip2)
                        ((ToolStripButton)item).Checked = false;
        }
        public static void ResetToolStripButtons(ToolStrip toolstrip, ToolStripButton skip1)
        {
            ResetToolStripButtons(toolstrip, skip1, null);
        }
        public static void ResetToolStripButtons(ToolStrip toolstrip)
        {
            ResetToolStripButtons(toolstrip, null, null);
        }
        public static void AlertLabel(ToolStripLabel labelAlert, string message, Color color)
        {
            new Thread(unused => AlertLabelThread(labelAlert, message, color)).Start();
        }
        private static void AlertLabelThread(ToolStripLabel labelAlert, string message, Color color)
        {
            Color backcolor = labelAlert.BackColor;
            labelAlert.Visible = true;
            labelAlert.Text = message;
            for (int i = 0; i < 3; i++)
            {
                labelAlert.BackColor = color;
                Thread.Sleep(500);
                labelAlert.BackColor = backcolor;
                Thread.Sleep(500);
            }
            //int r = color.R;
            //int g = color.G;
            //int b = color.B;
            //while (color.ToArgb() != backcolor.ToArgb())
            //{
            //    if (r > backcolor.R)
            //        r--;
            //    else if (r < backcolor.R)
            //        r++;
            //    if (g > backcolor.G)
            //        g--;
            //    else if (g < backcolor.G)
            //        g++;
            //    if (b > backcolor.B)
            //        b--;
            //    else if (b < backcolor.B)
            //        b++;
            //    color = Color.FromArgb(r, g, b);
            //    labelAlert.BackColor = color;
            //    Thread.Sleep(1);
            //}
            //labelAlert.BackColor = SystemColors.Control;
            Thread.Sleep(500);
            labelAlert.Text = "";
            labelAlert.Visible = false;
        }
        public static void DrawString(Graphics g, Point p, string text, Color forecolor, Color backcolor, Font font)
        {
            RectangleF rdst = new RectangleF(new PointF(p.X, p.Y),
                g.MeasureString(text, font, new PointF(0, 0), StringFormat.GenericDefault));
            g.FillRectangle(new SolidBrush(Color.FromArgb(192, backcolor)), rdst);
            g.DrawString(text, font,
                new SolidBrush(forecolor), new PointF(rdst.X, rdst.Y));
        }
        public static string BitArrayToString(byte[] array, int bytesperline, bool tagoffset, bool tagsuboffset, int offsetstart)
        {
            string text = "ROM    | ANIM   | DATA\r\n-------+--------+-------------------------------------------------\r\n";
            for (int i = 0; i < array.Length; i++)
            {
                if (i != 0 && i % bytesperline == 0)
                    text += "\r\n";
                if (i % bytesperline == 0 && tagoffset)
                    text += (i + offsetstart).ToString("X6") + " | ";
                if (i % bytesperline == 0 && tagsuboffset)
                    text += i.ToString("X6") + " | ";
                text += array[i].ToString("X2") + " ";
            }
            return text;
        }
        public static int GetStringHeight(string text, int containerWidth, Font font)
        {
            int height = 0;
            return height;
        }
        //
        private const int SW_SHOWNOACTIVATE = 4;
        private const int HWND_TOPMOST = -1;
        private const uint SWP_NOACTIVATE = 0x0010;
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        private static extern bool SetWindowPos(
             int hWnd,           // window handle
             int hWndInsertAfter,    // placement-order handle
             int X,          // horizontal position
             int Y,          // vertical position
             int cx,         // width
             int cy,         // height
             uint uFlags);       // window positioning flags
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public static void ShowInactiveTopmost(Form frm)
        {
            ShowWindow(frm.Handle, SW_SHOWNOACTIVATE);
            SetWindowPos(frm.Handle.ToInt32(), HWND_TOPMOST,
            frm.Left, frm.Top, frm.Width, frm.Height,
            SWP_NOACTIVATE);
        }
        public static Rectangle GetVisibleBounds(Control control)
        {
            Control c = control;
            Rectangle r = c.RectangleToScreen(c.ClientRectangle);
            while (c != null)
            {
                r = Rectangle.Intersect(r, c.RectangleToScreen(c.ClientRectangle));
                c = c.Parent;
            }
            r = control.RectangleToClient(r);
            return r;
        }
        //
        public static void SortListView(ListView listView, ListViewColumnSorter lvwColumnSorter, int column)
        {
            if (column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }
            // Perform the sort with these new sort options.
            listView.Sort();
        }
        #endregion
        #region ZONEDOCTOR Functions
        public static bool Compare(Subtile subtileA, Subtile subtileB)
        {
            if (Bits.Compare(subtileA.Pixels, subtileB.Pixels) &&
                subtileA.Palette == subtileB.Palette &&
                subtileA.Index == subtileB.Index &&
                subtileA.Priority1 == subtileB.Priority1 &&
                Bits.Compare(subtileA.Colors, subtileB.Colors) &&
                subtileA.Mirror == subtileB.Mirror &&
                subtileA.Invert == subtileB.Invert)
                return true;
            return false;
        }
        public static bool Compare(Tile tileA, Tile tileB)
        {
            for (int i = 0; i < 4; i++)
                if (!Compare(tileA.Subtiles[i], tileB.Subtiles[i]))
                    return false;
            return true;
        }
        public static void Play(SoundPlayer soundPlayer, byte[] wav, bool looping)
        {
            if (wav == null)
                return;
            soundPlayer.Stream = new MemoryStream(wav);
            if (looping)
                soundPlayer.PlayLooping();
            else
                soundPlayer.Play();
        }
        public static bool Contains(List<HexEditor.Change> items, int offset)
        {
            foreach (HexEditor.Change change in items)
                if (change.Offset == offset)
                    return true;
            return false;
        }
        public static HexEditor.Change FindOffset(List<HexEditor.Change> items, int offset)
        {
            foreach (HexEditor.Change change in items)
                if (offset >= change.Offset && offset <= change.Offset + change.Values.Length)
                    return change;
            return null;
        }
        public static long GenerateChecksum(params object[] OBJECTS)
        {
            try
            {
                byte[] bytes;
                int check = 0;
                MemoryStream ms;
                BinaryFormatter bf;
                foreach (object OBJECT in OBJECTS)
                {
                    if (OBJECT.GetType() == typeof(byte[]))
                        bytes = (byte[])OBJECT;
                    else if (OBJECT.GetType() == typeof(byte[][]))
                    {
                        foreach (byte[] array in (byte[][])OBJECT)
                        {
                            for (int i = 0; array != null && i < array.Length; i++)
                                check += (byte)(array[i] * i + array[i]);
                        }
                        continue;
                    }
                    // Event script
                    else if (OBJECT.GetType() == typeof(List<EventScript>))
                    {
                        foreach (EventScript es in (List<EventScript>)OBJECT)
                        {
                            bytes = es.Script;
                            for (int i = 0; i < bytes.Length; i++)
                                check += (byte)(bytes[i] * i + bytes[i]);
                        }
                        continue;
                    }
                    else
                    {
                        ms = new MemoryStream();
                        bf = new BinaryFormatter();
                        bf.Serialize(ms, OBJECT);
                        bytes = ms.ToArray();
                    }
                    for (int i = 0; i < bytes.Length; i++)
                        check += (byte)(bytes[i] * i + bytes[i]);
                }
                return check;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //return 0;
            }
        }
        public static Tile Contains(Tile[] tileset, Tile tile)
        {
            for (int i = 0; i < tileset.Length; i++)
            {
                if (Compare(tileset[i], tile))
                    return tileset[i];
            }
            return null;
        }
        public static void StopWatchStart()
        {
            StopWatch = new Stopwatch();
            StopWatch.Start();
        }
        public static string StopWatchStop(bool showMessage)
        {
            StopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = StopWatch.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            if (showMessage)
                MessageBox.Show(elapsedTime);
            return elapsedTime;
        }
        public static string StopWatchStop()
        {
            return StopWatchStop(false);
        }
        public static void AddHistory(Form form, int index, TreeNode node, string action, bool noreadoffset)
        {
            try
            {
                if (node == null)
                    return;
                string text;
                if (!noreadoffset)
                    text = action + " | index " + index + ", offset 0x" + node.Text.Substring(1, 6) + " | ";
                else
                    text = action + " | index " + index + ", \"" + node.Text.Substring(0, Math.Min(30, node.Text.Length)) + "\" | ";
                text += "Form \"" + form.Name + "\" | " + DateTime.Now.ToString() + "\r\n";
                Model.History = Model.History.Insert(0, text);
            }
            catch { }
        }
        public static void AddHistory(Form form, int index, TreeNode node, string action)
        {
            AddHistory(form, index, node, action, false);
        }
        public static void AddHistory(Form form, int index, string action)
        {
            try
            {
                string text = action + " | index " + index + " | ";
                text += "Form \"" + form.Name + "\" | " + DateTime.Now.ToString();
                Model.History = Model.History.Insert(0, text);
            }
            catch { }
        }
        public static void AddHistory(string message)
        {
            string text = message + "\r\n";
            Model.History = Model.History.Insert(0, text);
        }
        public static void CompareImages()
        {
            // first, open and create directory
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.SelectedPath = Settings.Default.LastDirectory;
            folderBrowserDialog1.Description = "Select source directory of images";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                Settings.Default.LastDirectory = folderBrowserDialog1.SelectedPath;
            else
                return;
            string source = folderBrowserDialog1.SelectedPath;
            folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.SelectedPath = Settings.Default.LastDirectory;
            folderBrowserDialog1.Description = "Select source directory of images";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                Settings.Default.LastDirectory = folderBrowserDialog1.SelectedPath;
            else
                return;
            string target = folderBrowserDialog1.SelectedPath;
            string[] sourceFiles = Directory.GetFiles(source);
            string[] targetFiles = Directory.GetFiles(target);
            string results = "";
            for (int i = 0; i < sourceFiles.Length && i < targetFiles.Length; i++)
            {
                FileStream sourceFile = File.OpenRead(sourceFiles[i]);
                FileStream targetFile = File.OpenRead(targetFiles[i]);
                BinaryReader sourceReader = new BinaryReader(sourceFile);
                BinaryReader targetReader = new BinaryReader(targetFile);
                if (sourceFile.Length != targetFile.Length)
                {
                    results += "Mismatched index: " + i + "\r\n";
                    continue;
                }
                byte[] sourceBytes = sourceReader.ReadBytes((int)sourceFile.Length);
                byte[] targetBytes = targetReader.ReadBytes((int)targetFile.Length);
                if (!Bits.Compare(sourceBytes, targetBytes))
                    results += "Mismatched index: " + i + "\r\n";
            }
            if (results == "")
                MessageBox.Show("Found no mismatched indexes.", "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                NewMessage.Show("MISMATCHED INDEXES", "Found the following mismatched indexes", results);
        }
        public static void CaptureScreens(Form form)
        {
            Rectangle bounds = form.Bounds;
            Bitmap screen = new Bitmap(bounds.Width, bounds.Height);
            Graphics graphics = Graphics.FromImage(screen);
            graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
            screen.Save(form.Name + ".png", ImageFormat.Png);
            // thumbnail
            Bitmap resized = ResizeImage(screen, 150, true);
            resized.Save(form.Name + "_thumb.png", ImageFormat.Png);
        }
        private static Bitmap ResizeImage(Bitmap source, int newHeight, bool sharpen)
        {
            double ratio = (double)newHeight / (double)source.Height;
            int newWidth = (int)((double)source.Width * ratio);
            Bitmap resized = new Bitmap(newWidth, newHeight);
            Graphics graphics = Graphics.FromImage(resized);
            graphics.DrawImage(source, 0, 0, newWidth, newHeight);
            if (sharpen)
                return SharpenImage(resized, 16.0);
            else
                return resized;
        }
        public static Bitmap SharpenImage(Bitmap image, double weight)
        {
            ConvolutionMatrix matrix = new ConvolutionMatrix(3);
            matrix.SetAll(1);
            matrix.Matrix[0, 0] = 0;
            matrix.Matrix[1, 0] = -2;
            matrix.Matrix[2, 0] = 0;
            matrix.Matrix[0, 1] = -2;
            matrix.Matrix[1, 1] = weight;
            matrix.Matrix[2, 1] = -2;
            matrix.Matrix[0, 2] = 0;
            matrix.Matrix[1, 2] = -2;
            matrix.Matrix[2, 2] = 0;
            matrix.Factor = weight - 8;
            return Convolution3x3(image, matrix);
        }
        private class ConvolutionMatrix
        {
            public int MatrixSize = 3;
            public double[,] Matrix;
            public double Factor = 1;
            public double Offset = 1;
            public ConvolutionMatrix(int size)
            {
                MatrixSize = 3;
                Matrix = new double[size, size];
            }
            public void SetAll(double value)
            {
                for (int i = 0; i < MatrixSize; i++)
                    for (int j = 0; j < MatrixSize; j++)
                        Matrix[i, j] = value;
            }
        }
        private static Bitmap Convolution3x3(Bitmap b, ConvolutionMatrix m)
        {
            Bitmap newImg = (Bitmap)b.Clone();
            Color[,] pixelColor = new Color[3, 3];
            int A, R, G, B;
            for (int y = 0; y < b.Height - 2; y++)
            {
                for (int x = 0; x < b.Width - 2; x++)
                {
                    pixelColor[0, 0] = b.GetPixel(x, y);
                    pixelColor[0, 1] = b.GetPixel(x, y + 1);
                    pixelColor[0, 2] = b.GetPixel(x, y + 2);
                    pixelColor[1, 0] = b.GetPixel(x + 1, y);
                    pixelColor[1, 1] = b.GetPixel(x + 1, y + 1);
                    pixelColor[1, 2] = b.GetPixel(x + 1, y + 2);
                    pixelColor[2, 0] = b.GetPixel(x + 2, y);
                    pixelColor[2, 1] = b.GetPixel(x + 2, y + 1);
                    pixelColor[2, 2] = b.GetPixel(x + 2, y + 2);
                    A = pixelColor[1, 1].A;
                    R = (int)((((pixelColor[0, 0].R * m.Matrix[0, 0]) +
                                 (pixelColor[1, 0].R * m.Matrix[1, 0]) +
                                 (pixelColor[2, 0].R * m.Matrix[2, 0]) +
                                 (pixelColor[0, 1].R * m.Matrix[0, 1]) +
                                 (pixelColor[1, 1].R * m.Matrix[1, 1]) +
                                 (pixelColor[2, 1].R * m.Matrix[2, 1]) +
                                 (pixelColor[0, 2].R * m.Matrix[0, 2]) +
                                 (pixelColor[1, 2].R * m.Matrix[1, 2]) +
                                 (pixelColor[2, 2].R * m.Matrix[2, 2]))
                                        / m.Factor) + m.Offset);
                    if (R < 0)
                        R = 0;
                    else if (R > 255)
                        R = 255;
                    G = (int)((((pixelColor[0, 0].G * m.Matrix[0, 0]) +
                                 (pixelColor[1, 0].G * m.Matrix[1, 0]) +
                                 (pixelColor[2, 0].G * m.Matrix[2, 0]) +
                                 (pixelColor[0, 1].G * m.Matrix[0, 1]) +
                                 (pixelColor[1, 1].G * m.Matrix[1, 1]) +
                                 (pixelColor[2, 1].G * m.Matrix[2, 1]) +
                                 (pixelColor[0, 2].G * m.Matrix[0, 2]) +
                                 (pixelColor[1, 2].G * m.Matrix[1, 2]) +
                                 (pixelColor[2, 2].G * m.Matrix[2, 2]))
                                        / m.Factor) + m.Offset);
                    if (G < 0)
                        G = 0;
                    else if (G > 255)
                        G = 255;
                    B = (int)((((pixelColor[0, 0].B * m.Matrix[0, 0]) +
                                 (pixelColor[1, 0].B * m.Matrix[1, 0]) +
                                 (pixelColor[2, 0].B * m.Matrix[2, 0]) +
                                 (pixelColor[0, 1].B * m.Matrix[0, 1]) +
                                 (pixelColor[1, 1].B * m.Matrix[1, 1]) +
                                 (pixelColor[2, 1].B * m.Matrix[2, 1]) +
                                 (pixelColor[0, 2].B * m.Matrix[0, 2]) +
                                 (pixelColor[1, 2].B * m.Matrix[1, 2]) +
                                 (pixelColor[2, 2].B * m.Matrix[2, 2]))
                                        / m.Factor) + m.Offset);
                    if (B < 0)
                        B = 0;
                    else if (B > 255)
                        B = 255;
                    newImg.SetPixel(x + 1, y + 1, Color.FromArgb(A, R, G, B));
                }
            }
            return newImg;
        }
        #endregion
        #region Math Functions
        public static double PercentIncrease(double percent, double value)
        {
            return value + (value * (percent / 100.0));
        }
        public static double PercentDecrease(double percent, double value)
        {
            return value - (value * (percent / 100.0));
        }
        #endregion
        public static bool SpecialCaseVehicle(int offset)
        {
            foreach (int pointer in Model.SpecialPointers_Vehicle)
                if (offset == pointer)
                    return true;
            return false;
        }
        public static bool SpecialCaseMap(int offset)
        {
            foreach (int pointer in Model.SpecialPointers_Map)
                if (offset == pointer)
                    return true;
            return false;
        }
        public static bool CancelVehicleQueue(int offset)
        {
            foreach (int pointer in Model.SpecialPointers_Cancel)
                if (offset == pointer)
                    return true;
            return false;
        }
    }
}

