using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ZONEDOCTOR
{
    [Serializable()]
    public class Dialogue : Element
    {
        #region Variables
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public override int Index { get { return index; } set { index = value; } }
        // class variables
        private char[] text;
        // accessors
        public char[] Text { get { return text; } }
        #endregion
        // constructor
        public Dialogue(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            text = GetText();
        }
        // class functions
        public string GetDialogue()
        {
            return new string(text);
        }
        private char[] GetText()
        {
            string dialogue = "";
            int startingIndexInBank0E = Bits.GetShort(rom, 0x0CE600);
            int offset;
            if (index < startingIndexInBank0E)
                offset = Bits.GetShort(rom, index * 2 + 0x0CE602) + 0x0D0000;
            else
                offset = Bits.GetShort(rom, index * 2 + 0x0CE602) + 0x0E0000;
            //int startingIndex1 = Bits.GetShort(rom, 0xDF80);
            //int startingIndex2 = Bits.GetShort(rom, 0xDF83);
            //int startingIndex3 = Bits.GetShort(rom, 0xDF86);
            //int offset;
            //if (index < startingIndex1)
            //    offset = ((rom[0xDF82] - 0xC0) << 16) + Bits.GetShort(rom, index * 2 + 0x0CE602);
            //else if (index < startingIndex2)
            //    offset = ((rom[0xDF85] - 0xC0) << 16) + Bits.GetShort(rom, index * 2 + 0x0CE602);
            //else
            //    offset = ((rom[0xDF88] - 0xC0) << 16) + Bits.GetShort(rom, index * 2 + 0x0CE602);
            //
            while (rom[offset] != 0)
            {
                if (rom[offset] == 0x00)
                    break;
                else if (rom[offset] == 0x11)
                {
                    offset++;
                    while (rom[offset] != 0x12)
                        offset++;
                }
                else if (rom[offset] == 0x14)
                    offset += 2;
                else if (rom[offset] == 0x16)
                {
                    offset++;
                    while (rom[offset] != 0x12)
                        offset++;
                }
                else
                    dialogue += Lists.DialogueTable[rom[offset++]];
            }
            return dialogue.ToCharArray();
        }
        public string GetStub(bool textCodeFormat)
        {
            string temp = GetDialogue();
            if (temp.Length > 40)
            {
                temp = temp.Substring(0, 37);
                return temp + "...";
            }
            else
                return temp;
        }
        public int GetOptionCount()
        {
            int count = 0;
            foreach (char char_ in text)
                if (char_ == '^')
                    count++;
            return count;
        }
        // universal functions
        public override string ToString()
        {
            return new string(text);
        }
        public override void Clear()
        {
            text = new char[0];
        }
    }
}
