using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ZONEDOCTOR
{
    public class SolidityTile
    {
        // universal variables
        private int index; public int Index { get { return index; } set { index = value; } }
        // local variables
        private byte[] soliditySet;
        private bool worldMap;
        private bool solid;
        private bool east;
        private bool west;
        private bool south;
        private bool north;
        private bool alwaysFaceUp;
        private bool solidTier1;
        private bool solidTier2;
        private bool door;
        private bool passableQuadrants;
        private int stairs;
        private bool b0b3;
        private bool b0b4;
        private bool b1b4;
        private bool b1b5;
        // accessors
        public bool WorldMap { get { return worldMap; } }
        public bool Solid { get { return solid; } set { solid = value; } }
        public bool East { get { return east; } set { east = value; } }
        public bool West { get { return west; } set { west = value; } }
        public bool North { get { return north; } set { north = value; } }
        public bool South { get { return south; } set { south = value; } }
        public bool AlwaysFaceUp { get { return alwaysFaceUp; } set { alwaysFaceUp = value; } }
        public bool SolidTier1 { get { return solidTier1; } set { solidTier1 = value; } }
        public bool SolidTier2 { get { return solidTier2; } set { solidTier2 = value; } }
        public bool Door { get { return door; } set { door = value; } }
        public bool PassableQuadrants { get { return passableQuadrants; } set { passableQuadrants = value; } }
        public int Stairs { get { return stairs; } set { stairs = value; } }
        public bool B0b3 { get { return b0b3; } set { b0b3 = value; } }
        public bool B0b4 { get { return b0b4; } set { b0b4 = value; } }
        public bool B1b4 { get { return b1b4; } set { b1b4 = value; } }
        public bool B1b5 { get { return b1b5; } set { b1b5 = value; } }
        // world map properties
        private int battlefield;
        private int airshipShadow;
        private bool blockChocobo;
        private bool blockAirship;
        private bool hideSpriteLegs;
        private bool enableRandomBattle;
        private bool veldt;
        private bool phoenixCave;
        private bool kefkaTower;
        private bool b0b7;
        private bool b1b3;
        // world map accessors
        public int Battlefield { get { return battlefield; } set { battlefield = value; } }
        public int AirshipShadow { get { return airshipShadow; } set { airshipShadow = value; } }
        public bool BlockChocobo { get { return blockChocobo; } set { blockChocobo = value; } }
        public bool BlockAirship { get { return blockAirship; } set { blockAirship = value; } }
        public bool HideSpriteLegs { get { return hideSpriteLegs; } set { hideSpriteLegs = value; } }
        public bool EnableRandomBattle { get { return enableRandomBattle; } set { enableRandomBattle = value; } }
        public bool Veldt { get { return veldt; } set { veldt = value; } }
        public bool PhoenixCave { get { return phoenixCave; } set { phoenixCave = value; } }
        public bool KefkaTower { get { return kefkaTower; } set { kefkaTower = value; } }
        public bool B0b7 { get { return b0b7; } set { b0b7 = value; } }
        public bool B1b3 { get { return b1b3; } set { b1b3 = value; } }
        // constructors
        public SolidityTile(byte[] soliditySet, int index, bool worldMap)
        {
            this.soliditySet = soliditySet;
            this.index = index;
            this.worldMap = worldMap;
            Disassemble(soliditySet, worldMap);
        }
        public SolidityTile()
        {
        }
        // assemblers
        private void Disassemble(byte[] soliditySet, bool worldMap)
        {
            if (!worldMap)
            {
                int byte0 = soliditySet[index];
                int byte1 = soliditySet[index + 0x100];
                solidTier1 = (byte0 & 0x01) == 0x01;
                solidTier2 = (byte0 & 0x02) == 0x02;
                solid = (byte0 & 0x04) == 0x04;
                b0b3 = (byte0 & 0x08) == 0x08;
                b0b4 = (byte0 & 0x10) == 0x10;
                door = (byte0 & 0x20) == 0x20;
                stairs = Math.Min((byte0 & 0xC0) >> 6, 2);
                //
                west = (byte1 & 0x01) == 0x01;
                east = (byte1 & 0x02) == 0x02;
                north = (byte1 & 0x04) == 0x04;
                south = (byte1 & 0x08) == 0x08;
                b1b4 = (byte1 & 0x10) == 0x10;
                b1b5 = (byte1 & 0x20) == 0x20;
                alwaysFaceUp = (byte1 & 0x40) == 0x40;
                passableQuadrants = (byte1 & 0x80) == 0x80;
            }
            else
            {
                int byte0 = soliditySet[index * 2];
                int byte1 = soliditySet[index * 2 + 1];
                blockChocobo = (byte0 & 0x01) == 0x01;
                blockAirship = (byte0 & 0x02) == 0x02;
                airshipShadow = (byte0 & 0x0C) >> 2;
                solid = (byte0 & 0x10) == 0x10;
                hideSpriteLegs = (byte0 & 0x20) == 0x20;
                enableRandomBattle = (byte0 & 0x40) == 0x40;
                b0b7 = (byte0 & 0x80) == 0x80;
                //
                battlefield = byte1 & 0x07;
                b1b3 = (byte1 & 0x08) == 0x08;
                b1b4 = (byte1 & 0x10) == 0x10;
                veldt = (byte1 & 0x20) == 0x20;
                phoenixCave = (byte1 & 0x40) == 0x40;
                kefkaTower = (byte1 & 0x80) == 0x80;
            }
        }
        public void Assemble(byte[] soliditySet)
        {
            if (!worldMap)
            {
                soliditySet[index] = (byte)(stairs << 6);
                Bits.SetBit(soliditySet, index, 0, solidTier1);
                Bits.SetBit(soliditySet, index, 1, solidTier2);
                Bits.SetBit(soliditySet, index, 2, solid);
                Bits.SetBit(soliditySet, index, 3, b0b3);
                Bits.SetBit(soliditySet, index, 4, b0b4);
                Bits.SetBit(soliditySet, index, 5, door);
                //
                Bits.SetBit(soliditySet, index + 0x100, 0, west);
                Bits.SetBit(soliditySet, index + 0x100, 1, east);
                Bits.SetBit(soliditySet, index + 0x100, 2, north);
                Bits.SetBit(soliditySet, index + 0x100, 3, south);
                Bits.SetBit(soliditySet, index + 0x100, 4, b1b4);
                Bits.SetBit(soliditySet, index + 0x100, 5, b1b5);
                Bits.SetBit(soliditySet, index + 0x100, 6, alwaysFaceUp);
                Bits.SetBit(soliditySet, index + 0x100, 7, passableQuadrants);
            }
            else
            {
                soliditySet[index * 2] = (byte)(airshipShadow << 2);
                Bits.SetBit(soliditySet, index * 2, 0, blockChocobo);
                Bits.SetBit(soliditySet, index * 2, 1, blockAirship);
                Bits.SetBit(soliditySet, index * 2, 4, solid);
                Bits.SetBit(soliditySet, index * 2, 5, hideSpriteLegs);
                Bits.SetBit(soliditySet, index * 2, 6, enableRandomBattle);
                Bits.SetBit(soliditySet, index * 2, 7, b0b7);
                //
                soliditySet[index * 2 + 1] = (byte)battlefield;
                Bits.SetBit(soliditySet, index * 2 + 1, 3, b1b3);
                Bits.SetBit(soliditySet, index * 2 + 1, 4, b1b4);
                Bits.SetBit(soliditySet, index * 2 + 1, 5, veldt);
                Bits.SetBit(soliditySet, index * 2 + 1, 6, phoenixCave);
                Bits.SetBit(soliditySet, index * 2 + 1, 7, kefkaTower);
            }
        }
        // universal functions
        public SolidityTile Copy()
        {
            SolidityTile copy = new SolidityTile(soliditySet, index, worldMap);
            copy.AirshipShadow = airshipShadow;
            copy.AlwaysFaceUp = alwaysFaceUp;
            copy.B0b3 = b0b3;
            copy.B0b4 = b0b4;
            copy.B0b7 = b0b7;
            copy.B1b3 = b1b3;
            copy.B1b4 = b1b4;
            copy.B1b5 = b1b5;
            copy.Battlefield = battlefield;
            copy.BlockAirship = blockAirship;
            copy.BlockChocobo = blockChocobo;
            copy.Door = door;
            copy.East = east;
            copy.EnableRandomBattle = enableRandomBattle;
            copy.HideSpriteLegs = hideSpriteLegs;
            copy.KefkaTower = kefkaTower;
            copy.North = north;
            copy.PassableQuadrants = passableQuadrants;
            copy.PhoenixCave = phoenixCave;
            copy.Solid = solid;
            copy.SolidTier1 = solidTier1;
            copy.SolidTier2 = solidTier2;
            copy.South = south;
            copy.Stairs = stairs;
            copy.Veldt = veldt;
            copy.West = west;
            return copy;
        }
    }
}
