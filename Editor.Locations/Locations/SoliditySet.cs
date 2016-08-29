using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ZONEDOCTOR
{
    public class SoliditySet
    {
        // local variables
        private LocationMap locationMap; public LocationMap LocationMap { get { return locationMap; } }
        private SolidityTile[] tiles; public SolidityTile[] Tiles { get { return tiles; } set { tiles = value; } }
        private bool worldMap; public bool WorldMap { get { return worldMap; } }
        private byte[] tileset; public byte[] Tileset { get { return tileset; } set { tileset = value; } }
        // constructor
        public SoliditySet(LocationMap locationMap, bool worldMap)
        {
            this.locationMap = locationMap;
            this.worldMap = worldMap;
            if (locationMap.Index == 0)
                tileset = Model.WOBSolidity;
            else if (locationMap.Index == 1)
                tileset = Model.WORSolidity;
            else
                tileset = Model.SoliditySets[locationMap.SoliditySet];
            tiles = new SolidityTile[tileset.Length / 2];
            for (int i = 0; i < tileset.Length / 2; i++)
                tiles[i] = new SolidityTile(tileset, i, worldMap);
        }
        // assemblers
        public void Assemble()
        {
            foreach (SolidityTile tile in tiles)
                tile.Assemble(tileset);
            Model.EditSoliditySets[locationMap.SoliditySet] = true;
            Buffer.BlockCopy(tileset, 0, Model.SoliditySets[locationMap.SoliditySet], 0, 0x200);
        }
        // universal functions
        public void Clear(int count)
        {
            if (count == 1)
            {
                Model.SoliditySets[locationMap.SoliditySet] = tileset = new byte[0x200];
                Model.EditSoliditySets[locationMap.SoliditySet] = true;
            }
            else
            {
                tileset = new byte[0x200];
                for (int i = 0; i < count; i++)
                {
                    Model.SoliditySets[i] = new byte[0x200];
                    Model.EditSoliditySets[i] = true;
                }
            }
        }
    }
}
