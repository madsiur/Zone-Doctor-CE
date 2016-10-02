using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ZONEDOCTOR
{
    [Serializable()]
    public class LocationNPCs
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public int Index { get { return index; } set { index = value; } }
        // local variables
        private List<NPC> npcs = new List<NPC>();
        private int currentNPC;
        private int selectedNPC;
        private NPC npc;
        // accessors
        public int CurrentNPC
        {
            get { return currentNPC; }
            set
            {
                if (this.npcs.Count > value)
                {
                    npc = (NPC)npcs[value];
                    this.currentNPC = value;
                }
            }
        }
        public List<NPC> Npcs { get { return npcs; } }
        public NPC Npc { get { return npc; } }
        public int SelectedNPC { get { return selectedNPC; } set { selectedNPC = value; } }
        public int Count { get { return npcs.Count; } }
        // npc properties
        public int EventPointer { get { return npc.EventPointer; } set { npc.EventPointer = value; } }
        public byte PaletteNum { get { return npc.PaletteNum; } set { npc.PaletteNum = value; } }
        public bool SolidifyActionPath { get { return npc.SolidifyActionPath; } set { npc.SolidifyActionPath = value; } }
        public byte X { get { return npc.X; } set { npc.X = value; } }
        public byte Y { get { return npc.Y; } set { npc.Y = value; } }
        public byte F { get { return npc.F; } set { npc.F = value; } }
        public ushort CheckMem { get { return npc.CheckMem; } set { npc.CheckMem = value; } }
        public byte CheckBit { get { return npc.CheckBit; } set { npc.CheckBit = value; } }
        public byte Speed { get { return npc.Speed; } set { npc.Speed = value; } }
        public byte SpriteNum { get { return npc.SpriteNum; } set { npc.SpriteNum = value; } }
        public byte Action { get { return npc.Action; } set { npc.Action = value; } }
        public bool WalkUnder { get { return npc.WalkUnder; } set { npc.WalkUnder = value; } }
        public bool WalkOver { get { return npc.WalkOver; } set { npc.WalkOver = value; } }
        public byte Vehicle { get { return npc.Vehicle; } set { npc.Vehicle = value; } }
        public bool DontFaceOnTrigger { get { return npc.DontFaceOnTrigger; } set { npc.DontFaceOnTrigger = value; } }
        // unknown bits
        public bool B4b7 { get { return npc.B4b7; } set { npc.B4b7 = value; } }
        public bool B8b3 { get { return npc.B8b3; } set { npc.B8b3 = value; } }
        public bool B8b4 { get { return npc.B8b4; } set { npc.B8b4 = value; } }
        public bool B8b5 { get { return npc.B8b5; } set { npc.B8b5 = value; } }
        public bool B8b6 { get { return npc.B8b6; } set { npc.B8b6 = value; } }
        public bool B8b7 { get { return npc.B8b7; } set { npc.B8b7 = value; } }
        // drawing
        public int[] Pixels { get { return npc.Pixels; } set { npc.Pixels = value; } }
        // constructors, functions
        public LocationNPCs(int index)
        {
            this.index = index;
            Disassemble();
        }

        // madsiur
        // hardcoded value to variable for expansion purpose
        private void Disassemble()
        {
            //int location = (int)(Bits.GetInt24(rom, 0x0052C3) - 0xC00000);

            // madsiur: hardcoded value to variable for expansion purpose (3.18.4-0.1)
            int location = Model.BASE_NPC_PTR;
            

            int pointerOffset = (index * 2) + location;
            ushort offsetStart = Bits.GetShort(rom, pointerOffset); pointerOffset += 2;
            ushort offsetEnd = Bits.GetShort(rom, pointerOffset);
            // no npc fields for location
            if (offsetStart >= offsetEnd) 
                return;
            int offset = offsetStart + location;
            while (offset < offsetEnd + location)
            {
                NPC tNPC = new NPC();
                tNPC.Disassemble(offset);
                npcs.Add(tNPC);
                offset += 9;
            }
        }
        
        public void Assemble(ref int offsetStart)
        {
            //int location = (int)(Bits.GetInt24(rom, 0x0052C3) - 0xC00000);

            // madsiur: hardcoded value to variable for expansion purpose (3.18.4-0.1)
            int location = Model.BASE_NPC_PTR;

            int pointerOffset = (index * 2) + location;
            // set the new pointer for the fields
            Bits.SetShort(rom, pointerOffset, offsetStart);  
            // no npc fields for location
            if (npcs.Count == 0) 
                return;
            int offset = offsetStart + location;
            foreach (NPC n in npcs)
            {
                n.Assemble(offset);
                offset += 9;
            }
            offsetStart = (ushort)(offset - location);
        }
        // list managers
        public void Remove()
        {
            if (currentNPC < npcs.Count)
            {
                npcs.Remove(npcs[currentNPC]);
                this.currentNPC = 0;
            }
        }
        public void Clear()
        {
            npcs.Clear();
            this.currentNPC = 0;
        }
        public void New(int index, Point p)
        {
            NPC e = new NPC();
            e.Clear();
            e.X = (byte)p.X;
            e.Y = (byte)p.Y;
            if (index < npcs.Count)
                npcs.Insert(index, e);
            else
                npcs.Add(e);
        }
        public void New(int index, NPC copy)
        {
            if (index < npcs.Count)
                npcs.Insert(index, copy);
            else
                npcs.Add(copy);
        }
        public void Reverse(int index)
        {
            npcs.Reverse(index, 2);
        }
    }
    [Serializable()]
    public class NPC
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        // npc properties
        private int eventPointer; public int EventPointer { get { return eventPointer; } set { eventPointer = value; } }
        private byte paletteNum; public byte PaletteNum { get { return paletteNum; } set { paletteNum = value; } }
        private bool solidifyActionPath; public bool SolidifyActionPath { get { return solidifyActionPath; } set { solidifyActionPath = value; } }
        private byte x; public byte X { get { return x; } set { x = value; } }
        private byte y; public byte Y { get { return y; } set { y = value; } }
        private byte f; public byte F { get { return f; } set { f = value; } }
        private ushort checkMem; public ushort CheckMem { get { return checkMem; } set { checkMem = value; } }
        private byte checkBit; public byte CheckBit { get { return checkBit; } set { checkBit = value; } }
        private byte speed; public byte Speed { get { return speed; } set { speed = value; } }
        private byte spriteNum; public byte SpriteNum { get { return spriteNum; } set { spriteNum = value; } }
        private byte action; public byte Action { get { return action; } set { action = value; } }
        private bool walkUnder; public bool WalkUnder { get { return walkUnder; } set { walkUnder = value; } }
        private bool walkOver; public bool WalkOver { get { return walkOver; } set { walkOver = value; } }
        private byte vehicle; public byte Vehicle { get { return vehicle; } set { vehicle = value; } }
        private bool dontFaceOnTrigger; public bool DontFaceOnTrigger { get { return dontFaceOnTrigger; } set { dontFaceOnTrigger = value; } }
        // unknown bits
        private bool b4b7; public bool B4b7 { get { return b4b7; } set { b4b7 = value; } }
        private bool b8b3; public bool B8b3 { get { return b8b3; } set { b8b3 = value; } }
        private bool b8b4; public bool B8b4 { get { return b8b4; } set { b8b4 = value; } }
        private bool b8b5; public bool B8b5 { get { return b8b5; } set { b8b5 = value; } }
        private bool b8b6; public bool B8b6 { get { return b8b6; } set { b8b6 = value; } }
        private bool b8b7; public bool B8b7 { get { return b8b7; } set { b8b7 = value; } }
        // assemblers
        public void Disassemble(int offset)
        {
            eventPointer = (int)(Bits.GetInt24(rom, offset) & 0x03FFFF); offset += 2;
            paletteNum = (byte)((rom[offset] & 0x1C) >> 2);
            solidifyActionPath = (rom[offset] & 0x20) == 0x20;
            checkMem = (ushort)((Bits.GetShort(rom, offset) >> 9) + 0x1EE0);
            checkBit = (byte)((Bits.GetShort(rom, offset) >> 6) & 0x07); offset += 2;
            x = (byte)(rom[offset] & 0x7F);
            b4b7 = (rom[offset] & 0x80) == 0x80; offset++;
            y = (byte)(rom[offset] & 0x3F);
            speed = (byte)((rom[offset] & 0xC0) >> 6); offset++;
            spriteNum = rom[offset++];
            action = (byte)(rom[offset] & 0x0F);
            walkUnder = (rom[offset] & 0x10) == 0x10;
            walkOver = (rom[offset] & 0x20) == 0x20;
            vehicle = (byte)((rom[offset] & 0xC0) >> 6); offset++;
            f = (byte)(rom[offset] & 0x03);
            dontFaceOnTrigger = (rom[offset] & 0x04) == 0x04;
            b8b3 = (rom[offset] & 0x08) == 0x08;
            b8b4 = (rom[offset] & 0x10) == 0x10;
            b8b5 = (rom[offset] & 0x20) == 0x20;
            b8b6 = (rom[offset] & 0x40) == 0x40;
            b8b7 = (rom[offset] & 0x80) == 0x80;
            offset++;
        }
        public void Assemble(int offset)
        {
            rom[offset] = (byte)(eventPointer & 0xFF); offset++;
            rom[offset] = (byte)((eventPointer & 0xFF00) >> 8); offset++;
            rom[offset] = (byte)((eventPointer & 0x030000) >> 16);
            Bits.SetByteBits(rom, offset, (byte)(paletteNum << 2), 0x1C);
            Bits.SetBit(rom, offset, 5, solidifyActionPath);
            Bits.SetShortBits(rom, offset, (ushort)((checkMem - 0x1EE0) << 9), 0xFE00);
            Bits.SetShortBits(rom, offset, (ushort)(checkBit << 6), 0x01C0); offset += 2;
            rom[offset] = x;
            Bits.SetBit(rom, offset, 7, b4b7); offset++;
            rom[offset] = y;
            Bits.SetByteBits(rom, offset, (byte)(speed << 6), 0xC0); offset++;
            rom[offset] = spriteNum; offset++;
            rom[offset] = action;
            Bits.SetBit(rom, offset, 4, walkUnder);
            Bits.SetBit(rom, offset, 5, walkOver);
            Bits.SetByteBits(rom, offset, (byte)(vehicle << 6), 0xC0); offset++;
            rom[offset] = f;
            Bits.SetBit(rom, offset, 2, dontFaceOnTrigger);
            Bits.SetBit(rom, offset, 3, b8b3);
            Bits.SetBit(rom, offset, 4, b8b4);
            Bits.SetBit(rom, offset, 5, b8b5);
            Bits.SetBit(rom, offset, 6, b8b6);
            Bits.SetBit(rom, offset, 7, b8b7);
        }
        // universal functions
        public void Clear()
        {
            eventPointer = 0;
            paletteNum = 0;
            solidifyActionPath = false;
            checkMem = 0x1EE0;
            checkBit = 0;
            x = 0;
            y = 0;
            speed = 0;
            spriteNum = 0;
            action = 0;
            walkUnder = false;
            walkOver = false;
            vehicle = 0;
            f = 0;
            dontFaceOnTrigger = false;
        }
        public NPC Copy()
        {
            NPC copy = new NPC();
            copy.EventPointer = eventPointer;
            copy.PaletteNum = paletteNum;
            copy.SolidifyActionPath = solidifyActionPath;
            copy.X = x;
            copy.Y = y;
            copy.CheckMem = checkMem;
            copy.CheckBit = checkBit;
            copy.Speed = speed;
            copy.SpriteNum = spriteNum;
            copy.Action = action;
            copy.WalkUnder = walkUnder;
            copy.WalkOver = walkOver;
            copy.Vehicle = vehicle;
            copy.F = f;
            copy.DontFaceOnTrigger = dontFaceOnTrigger;
            copy.B4b7 = b4b7;
            copy.B8b3 = b8b3;
            copy.B8b4 = b8b4;
            copy.B8b5 = b8b5;
            copy.B8b6 = b8b6;
            copy.B8b7 = b8b7;
            return copy;
        }
        // drawing
        private int[] pixels;
        public int[] Pixels
        {
            get
            {
                if (pixels != null) 
                    return pixels;
                pixels = new int[16 * 32];
                int[] tile = new int[8 * 8];
                int pointer = Bits.GetShort(rom, spriteNum * 2 + 0x00D0F2);
                int bank = rom[spriteNum * 2 + 0x00D23C]; bank -= 0xC0;
                int size = rom[spriteNum * 2 + 0x00D23D];
                int offset = (bank << 16) + pointer;
                switch (f)
                {
                    case 0: offset += !b8b6 ? 0x180 : 0; break; //north
                    case 1: offset += !b8b6 ? 0x300 : 0x80; break;  //east
                    case 2: offset += !b8b6 ? 0 : 0x100; break; //south
                    case 3: offset += !b8b6 ? 0x300 : 0x180; break;  //west
                }
                if (b8b5)
                    offset -= 0x180;
                if (b8b6)
                    offset += 0x20;
                Size s = new Size(2, 3);
                byte[] sprite = Bits.GetBytes(rom, offset, 0x200);
                int[] palette = Palette(paletteNum);
                Subtile temp;
                int i;
                for (int y = 0; y < s.Height; y++)
                {
                    for (int x = 0; x < s.Width; x++)
                    {
                        i = y * 2 + x;
                        temp = new Subtile(i, sprite, i * 0x20, palette, false, false, false, false);
                        Do.PixelsToPixels(temp.Pixels, pixels, 16, new Rectangle(x * 8, (y + (b8b6 ? 1 : 0)) * 8, 8, 8));
                    }
                }
                if (f == 1)    // mirror if facing east
                {
                    int o = 0;
                    for (int y = 0; y < 32; y++)
                    {
                        for (int a = 0, b = 15; a < 8; a++, b--)
                        {
                            o = pixels[(y * 16) + a];
                            pixels[(y * 16) + a] = pixels[(y * 16) + b];
                            pixels[(y * 16) + b] = o;
                        }
                    }
                }
                return pixels;
            }
            set
            {
                pixels = value;
            }
        }
        private int[] Palette(byte index)
        {
            int[] temp = new int[16];
            int offset = (index * 0x20) + 0x268000;
            for (int i = 0; i < 16; i++) // 7 palettes in set
            {
                ushort color = Bits.GetShort(rom, offset + (i * 2));
                int r = (byte)((color % 0x20) * 8);
                int g = (byte)(((color >> 5) % 0x20) * 8);
                int b = (byte)(((color >> 10) % 0x20) * 8);
                temp[i] = Color.FromArgb(255, r, g, b).ToArgb();
            }
            return temp;
        }
    }
}
