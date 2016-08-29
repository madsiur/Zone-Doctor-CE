﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ZONEDOCTOR
{
    /// <summary>
    /// A buffer of copied 16x16 tiles.
    /// </summary>
    public class CopyBuffer
    {
        // class variables and accessors
        private int width; public int Width { get { return width; } }
        private int height; public int Height { get { return height; } }
        private int[] copy; public int[] Copy { get { return copy; } set { copy = value; } }
        private int[][] copies; public int[][] Copies { get { return copies; } set { copies = value; } }
        private Tile[] tiles; public Tile[] Tiles { get { return tiles; } set { tiles = value; } }
        private int[] pixels; public int[] Pixels { get { return pixels; } set { pixels = value; } }
        // public accessors
        public byte[] BYTE_copy
        {
            get
            {
                byte[] arr = new byte[copy.Length];
                for (int i = 0; i < arr.Length; i++)
                    arr[i] = (byte)copy[i];
                return arr;
            }
        }
        public Size Size { get { return new Size(width, height); } }
        /// <summary>
        /// A bitmap of the copied tiles.
        /// </summary>
        public Bitmap Image
        {
            get
            {
                {
                    if (tiles != null && pixels == null)
                        return Do.PixelsToImage(Do.TilesetToPixels(tiles, width / 16, height / 16, 0, false), width, height);
                    if (tiles == null && pixels != null)
                        return Do.PixelsToImage(pixels, width, height);
                    return null;
                }
            }
        }
        // constructors
        public CopyBuffer(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
        public CopyBuffer(int width, int height, int[] copy)
        {
            this.width = width;
            this.height = height;
            this.copy = copy;
        }
        public CopyBuffer(int width, int height, int[][] copies)
        {
            this.width = width;
            this.height = height;
            this.copies = copies;
        }
        public CopyBuffer(int width, int height, Tile[] tiles)
        {
            this.width = width;
            this.height = height;
            this.tiles = tiles;
        }
    }
}
