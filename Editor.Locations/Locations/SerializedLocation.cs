using System;
using System.Collections.Generic;
using System.Text;

namespace ZONEDOCTOR
{
    [Serializable()]
    class SerializedLocation
    {
        public LocationMap LocationMap;
        public byte[] TilesetL1;
        public byte[] TilesetL2;
        public byte[] TilemapL1;
        public byte[] TilemapL2;
        public byte[] TilemapL3;
        public byte[] SoliditySet;
        public LocationNPCs LocationNPCs;
        public LocationTreasures LocationTreasures;
        public LocationExits LocationExits;
        public LocationEvents LocationEvents;
        public SerializedLocation()
        {
        }
    }
}
