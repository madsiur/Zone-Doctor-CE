using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ZONEDOCTOR
{
    [Serializable()]
    public class LocationExits
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public int Index { get { return index; } set { index = value; } }
        // local variables
        private List<Exit> exits = new List<Exit>();
        private int currentExit;
        private int selectedExit;
        private Exit exit;
        // accessors
        public int CurrentExit
        {
            get { return currentExit; }
            set
            {
                if (this.exits.Count > value)
                {
                    exit = (Exit)exits[value];
                    this.currentExit = value;
                }
            }
        }
        public List<Exit> Exits { get { return exits; } }
        public Exit Exit { get { return exit; } }
        public int SelectedExit { get { return selectedExit; } set { selectedExit = value; } }
        public int Count { get { return exits.Count; } }
        // exit properties
        public byte X { get { return exit.X; } set { exit.X = value; } }
        public byte Y { get { return exit.Y; } set { exit.Y = value; } }
        public byte F { get { return exit.F; } set { exit.F = value; } }
        public byte Width { get { return exit.Width; } set { exit.Width = value; } }
        // destination properties
        public bool ToWorldMap { get { return exit.ToWorldMap; } set { exit.ToWorldMap = value; } }
        public ushort Destination { get { return exit.Destination; } set { exit.Destination = value; } }
        public byte DstX { get { return exit.DstX; } set { exit.DstX = value; } }
        public byte DstY { get { return exit.DstY; } set { exit.DstY = value; } }
        public byte DstF { get { return exit.DstF; } set { exit.DstF = value; } }
        public bool ShowMessage { get { return exit.ShowMessage; } set { exit.ShowMessage = value; } }
        // unknown bits
        public bool refreshParentMap { get { return exit.RefreshParentMap; } set { exit.RefreshParentMap = value; } }
        public bool B3b2 { get { return exit.B3b2; } set { exit.B3b2 = value; } }
        public bool B3b4 { get { return exit.B3b4; } set { exit.B3b4 = value; } }
        public bool B3b5 { get { return exit.B3b5; } set { exit.B3b5 = value; } }
        // constructor, functions
        public LocationExits(int locationNum)
        {
            this.index = locationNum;
            Disassemble();
        }
        public LocationExits()
        {
        }
        private void Disassemble()
        {
            int offset;
            ushort offsetStart = 0;
            ushort offsetEnd = 0;
            Exit tExit;
            // short exits
            int pointerOffset = (index * 2) + 0x1FBB00;
            offsetStart = Bits.GetShort(rom, pointerOffset); pointerOffset += 2;
            offsetEnd = Bits.GetShort(rom, pointerOffset);
            if (offsetStart < offsetEnd)
            {
                offset = offsetStart + 0x1FBB00;
                while (offset < offsetEnd + 0x1FBB00)
                {
                    tExit = new Exit();
                    tExit.Disassemble(offset, false);
                    exits.Add(tExit);
                    offset += 6;
                }
            }
            // long exits
            pointerOffset = (index * 2) + 0x2DF480;
            offsetStart = Bits.GetShort(rom, pointerOffset); pointerOffset += 2;
            offsetEnd = Bits.GetShort(rom, pointerOffset);
            if (offsetStart < offsetEnd)
            {
                offset = offsetStart + 0x2DF480;
                while (offset < offsetEnd + 0x2DF480)
                {
                    tExit = new Exit();
                    tExit.Disassemble(offset, true);
                    exits.Add(tExit);
                    offset += 7;
                }
            }
        }
        public void Assemble(ref int offsetShort, ref int offsetLong)
        {
            Bits.SetShort(rom, (index * 2) + 0x1FBB00, offsetShort);  // set the new pointer for the fields
            Bits.SetShort(rom, (index * 2) + 0x2DF480, offsetLong);  // set the new pointer for the fields
            foreach (Exit exit in exits)
            {
                int offset;
                if (exit.Width == 0)
                {
                    offset = offsetShort + 0x1FBB00;
                    exit.Assemble(offset);
                    offsetShort += 6;
                }
                else
                {
                    offset = offsetLong + 0x2DF480;
                    exit.Assemble(offset);
                    offsetLong += 7;
                }
            }
        }
        // list managers
        public void Remove()
        {
            if (currentExit < exits.Count)
            {
                exits.Remove(exits[currentExit]);
                this.currentExit = 0;
            }
        }
        public void Clear()
        {
            exits.Clear();
            this.currentExit = 0;
        }
        public void New(int index, Point p)
        {
            Exit e = new Exit();
            e.X = (byte)p.X;
            e.Y = (byte)p.Y;
            if (index < exits.Count)
                exits.Insert(index, e);
            else
                exits.Add(e);
        }
        public void New(int index, Exit copy)
        {
            if (index < exits.Count)
                exits.Insert(index, copy);
            else
                exits.Add(copy);
        }
    }
    [Serializable()]
    public class Exit
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        // exit properties
        private byte x; public byte X { get { return x; } set { x = value; } }
        private byte y; public byte Y { get { return y; } set { y = value; } }
        private byte f; public byte F { get { return f; } set { f = value; } }
        private byte width; public byte Width { get { return width; } set { width = value; } }
        private bool showMessage; public bool ShowMessage { get { return showMessage; } set { showMessage = value; } }
        // destination properties
        private bool toWorldMap; public bool ToWorldMap { get { return toWorldMap; } set { toWorldMap = value; } }
        private ushort destination; public ushort Destination { get { return destination; } set { destination = value; } }
        private byte dstX; public byte DstX { get { return dstX; } set { dstX = value; } }
        private byte dstY; public byte DstY { get { return dstY; } set { dstY = value; } }
        private byte dstF; public byte DstF { get { return dstF; } set { dstF = value; } }
        // unknown bits
        private bool refreshParentMap; public bool RefreshParentMap { get { return refreshParentMap; } set { refreshParentMap = value; } }
        private bool b3b2; public bool B3b2 { get { return b3b2; } set { b3b2 = value; } }
        private bool b3b4; public bool B3b4 { get { return b3b4; } set { b3b4 = value; } }
        private bool b3b5; public bool B3b5 { get { return b3b5; } set { b3b5 = value; } }
        // assemblers
        public void Disassemble(int offset, bool wide)
        {
            x = rom[offset++];
            y = rom[offset++];
            if (wide)
            {
                width = (byte)(rom[offset] & 0x7F);
                f = (rom[offset] & 0x80) == 0x80 ? (byte)1 : (byte)0; offset++;
            }
            toWorldMap = (ushort)(Bits.GetShort(rom, offset) & 0x1FF) == 0x1FF;
            if (!toWorldMap)
            {
                destination = (ushort)(Bits.GetShort(rom, offset) & 0x1FF);
                offset++;
            }
            else
            {
                offset++;
                destination = (rom[offset] & 0x02) == 0x02 ? (ushort)0 : (ushort)1;
            }
            refreshParentMap = (rom[offset] & 0x02) == 0x02;
            b3b2 = (rom[offset] & 0x04) == 0x04;
            dstF = (byte)((rom[offset] & 0x30) >> 4);
            showMessage = (rom[offset] & 0x08) == 0x08; offset++;
            dstX = rom[offset++];
            dstY = rom[offset];
        }
        public void Assemble(int offset)
        {
            rom[offset] = x; offset++;
            rom[offset] = y; offset++;
            if (width > 0)
            {
                rom[offset] = width;
                Bits.SetBit(rom, offset, 7, f == 1); offset++;
            }
            if (!toWorldMap)
                Bits.SetShort(rom, offset, destination);
            else
                Bits.SetShort(rom, offset, 0x1FF);
            offset++;
            Bits.SetBit(rom, offset, 1, refreshParentMap);
            Bits.SetBit(rom, offset, 2, b3b2);
            Bits.SetByteBits(rom, offset, (byte)(dstF << 4), 0x30);
            Bits.SetBit(rom, offset, 3, showMessage); offset++;
            rom[offset] = dstX; offset++;
            rom[offset] = dstY;
        }
        // universal functions
        public Exit Copy()
        {
            Exit copy = new Exit();
            copy.X = x;
            copy.Y = y;
            copy.Width = width;
            copy.F = f;
            copy.ToWorldMap = toWorldMap;
            copy.Destination = destination;
            copy.DstX = dstX;
            copy.DstY = dstY;
            copy.DstF = dstF;
            copy.ShowMessage = showMessage;
            copy.RefreshParentMap = refreshParentMap;
            copy.B3b2 = b3b2;
            copy.B3b4 = b3b4;
            copy.B3b5 = b3b5;
            return copy;
        }
    }
}