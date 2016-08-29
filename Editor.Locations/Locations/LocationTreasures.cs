using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ZONEDOCTOR
{
    [Serializable()]
    public class LocationTreasures
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public int Index { get { return index; } set { index = value; } }
        // local variables
        private List<Treasure> treasures = new List<Treasure>();
        private int currentTreasure;
        private int selectedTreasure;
        private Treasure treasure;
        // accessors
        public int CurrentTreasure
        {
            get { return currentTreasure; }
            set
            {
                if (this.treasures.Count > value)
                {
                    treasure = (Treasure)treasures[value];
                    this.currentTreasure = value;
                }
            }
        }
        public List<Treasure> Treasures { get { return treasures; } set { treasures = value; } }
        public Treasure Treasure { get { return treasure; } }
        public int SelectedTreasure { get { return selectedTreasure; } set { selectedTreasure = value; } }
        // treasures properties
        public byte X { get { return treasure.X; } set { treasure.X = value; } }
        public byte Y { get { return treasure.Y; } set { treasure.Y = value; } }
        public byte PropertyNum { get { return treasure.PropertyNum; } set { treasure.PropertyNum = value; } }
        public ushort CheckMem { get { return treasure.CheckMem; } set { treasure.CheckMem = value; } }
        public byte CheckBit { get { return treasure.CheckBit; } set { treasure.CheckBit = value; } }
        public byte Type { get { return treasure.Type; } set { treasure.Type = value; } }
        // constructor, functions
        public LocationTreasures(int index)
        {
            this.index = index;
            Disassemble();
        }
        private void Disassemble()
        {
            int pointerOffset = (index * 2) + 0x2D82F4;
            ushort offsetStart = Bits.GetShort(rom, pointerOffset); pointerOffset += 2;
            ushort offsetEnd = Bits.GetShort(rom, pointerOffset);
            // no treasures for location
            if (offsetStart >= offsetEnd)
                return;
            int offset = offsetStart + 0x2D8634;
            while (offset < offsetEnd + 0x2D8634)
            {
                Treasure tTreasure = new Treasure();
                tTreasure.Disassemble(offset);
                treasures.Add(tTreasure);
                offset += 5;
            }
        }
        public void Assemble(ref int offsetStart)
        {
            int pointerOffset = (index * 2) + 0x2D82F4;
            // set the new pointer for the fields
            Bits.SetShort(rom, pointerOffset, offsetStart);  
            // no exit fields for location
            if (treasures.Count == 0)
                return; 
            int offset = offsetStart + 0x2D8634;
            foreach (Treasure t in treasures)
            {
                t.Assemble(offset);
                offset += 5;
            }
            offsetStart = (ushort)(offset - 0x2D8634);
        }
        // list managers
        public void Remove()
        {
            if (currentTreasure < treasures.Count)
            {
                treasures.Remove(treasures[currentTreasure]);
                this.currentTreasure = 0;
            }
        }
        public void Clear()
        {
            treasures.Clear();
            this.currentTreasure = 0;
        }
        public void New(int index, Point p)
        {
            Treasure e = new Treasure();
            e.Clear();
            e.X = (byte)p.X;
            e.Y = (byte)p.Y;
            if (index < treasures.Count)
                treasures.Insert(index, e);
            else
                treasures.Add(e);
        }
        public void New(int index, Treasure copy)
        {
            if (index < treasures.Count)
                treasures.Insert(index, copy);
            else
                treasures.Add(copy);
        }
        public void Reverse(int index)
        {
            treasures.Reverse(index, 2);
        }
    }
    [Serializable()]
    public class Treasure
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        // treasures properties
        private byte x; public byte X { get { return x; } set { x = value; } }
        private byte y; public byte Y { get { return y; } set { y = value; } }
        private byte propertyNum; public byte PropertyNum { get { return propertyNum; } set { propertyNum = value; } }
        private ushort checkMem; public ushort CheckMem { get { return checkMem; } set { checkMem = value; } }
        private byte checkBit; public byte CheckBit { get { return checkBit; } set { checkBit = value; } }
        private byte type; public byte Type { get { return type; } set { type = value; } }
        // assemblers
        public void Disassemble(int offset)
        {
            x = rom[offset++];
            y = rom[offset++];
            checkMem = (ushort)(((Bits.GetShort(rom, offset) & 0x1FF) >> 3) + 0x1E40);
            checkBit = (byte)(rom[offset] & 0x07); offset++;
            switch (rom[offset] >> 4)
            {
                case 0: type = 0; break;
                case 1: type = 1; break;
                case 2: type = 2; break;
                case 4: type = 3; break;
                case 8: type = 4; break;
            }
            offset++;
            propertyNum = rom[offset];
        }
        public void Assemble(int offset)
        {
            rom[offset] = x; offset++;
            rom[offset] = y; offset++;
            Bits.SetShortBits(rom, offset, (ushort)((checkMem - 0x1E40) << 3), 0x01F8);
            Bits.SetByteBits(rom, offset, checkBit, 0x07); offset++;
            rom[offset] &= 0x0F;
            switch (type)
            {
                case 0: break;
                case 1: Bits.SetBit(rom, offset, 4, true); break;
                case 2: Bits.SetBit(rom, offset, 5, true); break;
                case 3: Bits.SetBit(rom, offset, 6, true); break;
                case 4: Bits.SetBit(rom, offset, 7, true); break;
            }
            offset++;
            rom[offset] = propertyNum;
        }
        // universal functions
        public void Clear()
        {
            x = 0;
            y = 0;
            checkMem = 0x1E40;
            checkBit = 0;
            type = 0;
            propertyNum = 0;
        }
        public Treasure Copy()
        {
            Treasure copy = new Treasure();
            copy.CheckBit = checkBit;
            copy.CheckMem = checkMem;
            copy.PropertyNum = propertyNum;
            copy.Type = type;
            copy.X = x;
            copy.Y = y;
            return copy;
        }
    }
}
