using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ZONEDOCTOR.Undo
{
    class TilemapEdit : Command
    {
        private Locations updater;
        private Tilemap tilemap;
        public Tilemap Tilemap { get { return tilemap; } set { tilemap = value; } }
        private Point start, stop;
        private State state = State.Instance;
        private int[][] changes = new int[3][];
        private bool allLayers;
        private int layer;
        private bool pasting;
        private bool autoRedo = false; public bool AutoRedo() { return this.autoRedo; }
        public TilemapEdit(Locations updater, Tilemap tileMap, int layer, Point start, Point stop, 
            int[][] changes, bool pasting, bool allLayers)
        {
            this.updater = updater;
            this.tilemap = tileMap;
            this.allLayers = allLayers;
            if (start.Y < stop.Y)
            {
                this.start = start;
                this.stop = stop;
            }
            else if (start == stop && start.X <= stop.X)
            {
                this.start = start;
                this.stop = stop;
            }
            else if (stop.Y < start.Y)
            {
                this.start = stop;
                this.stop = start;
            }
            this.layer = layer;
            for (int i = 0; i < 3; i++)
            {
                if (changes[i] == null) continue;
                this.changes[i] = new int[changes[i].Length];
                changes[i].CopyTo(this.changes[i], 0);
            }
            this.pasting = pasting;
            Execute();
        }
        public void Execute()
        {
            int deltaX = (stop.X / 16) - (start.X / 16);
            int deltaY = (stop.Y / 16) - (start.Y / 16);
            int tileX, tileY, temp;
            bool empty;
            for (int y = 0; y < deltaY; y++)
            {
                for (int x = 0; x < deltaX; x++)
                {
                    tileX = start.X + (x * 16);
                    tileY = start.Y + (y * 16);
                    if (allLayers)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            if (tilemap.GetType() == typeof(LocationTilemap))
                            {
                                if (l == 0 && updater.LocationMap.TilemapL1 == 0)
                                    break;
                                if (l == 1 && updater.LocationMap.TilemapL2 == 0)
                                    break;
                                if (l == 2 && updater.LocationMap.TilemapL3 == 0)
                                    break;
                            }
                            if (changes[l] == null) continue;
                            temp = tilemap.GetTileNum(l, tileX, tileY); // Save the current tileNum for later undo
                            empty = changes[l][x + y * deltaX] == 0;
                            if (temp != changes[l][x + y * deltaX])
                            {
                                tilemap.SetTileNum(changes[l][x + y * deltaX], l, tileX, tileY); // Set the tile to the new tileNum
                                changes[l][x + y * deltaX] = temp; // Replace the change with the old tileNum, so that all we have to do is Execute to undo this command
                            }
                            if (pasting && empty && state.Move)
                                tilemap.SetTileNum(temp, l, tileX, tileY);
                        }
                    }
                    else
                    {
                        if (tilemap.GetType() == typeof(LocationTilemap))
                        {
                            if (layer == 0 && updater.LocationMap.TilemapL1 == 0)
                                break;
                            if (layer == 1 && updater.LocationMap.TilemapL2 == 0)
                                break;
                            if (layer == 2 && updater.LocationMap.TilemapL3 == 0)
                                break;
                        }
                        if (changes[layer] == null) continue;
                        temp = tilemap.GetTileNum(layer, tileX, tileY); // Save the current tileNum for later undo
                        empty = changes[layer][x + y * deltaX] == 0;
                        if (temp != changes[layer][x + y * deltaX])
                        {
                            tilemap.SetTileNum(changes[layer][x + y * deltaX], layer, tileX, tileY); // Set the tile to the new tileNum
                            changes[layer][x + y * deltaX] = temp; // Replace the change with the old tileNum, so that all we have to do is Execute to undo this command
                        }
                        if (pasting && empty && state.Move)
                            tilemap.SetTileNum(temp, layer, tileX, tileY);
                    }
                }
            }
        }
    }
}
