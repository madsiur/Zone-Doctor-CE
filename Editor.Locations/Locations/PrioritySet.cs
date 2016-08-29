using System;
using System.Collections.Generic;
using System.Text;

namespace ZONEDOCTOR
{
    [Serializable()]
    public class PrioritySet
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public int Index { get { return index; } set { index = value; } }
        // local variables
        private bool mainscreenL1; public bool MainscreenL1 { get { return mainscreenL1; } set { mainscreenL1 = value; } }
        private bool mainscreenL2; public bool MainscreenL2 { get { return mainscreenL2; } set { mainscreenL2 = value; } }
        private bool mainscreenL3; public bool MainscreenL3 { get { return mainscreenL3; } set { mainscreenL3 = value; } }
        private bool mainscreenOBJ; public bool MainscreenOBJ { get { return mainscreenOBJ; } set { mainscreenOBJ = value; } }
        private bool subscreenL1; public bool SubscreenL1 { get { return subscreenL1; } set { subscreenL1 = value; } }
        private bool subscreenL2; public bool SubscreenL2 { get { return subscreenL2; } set { subscreenL2 = value; } }
        private bool subscreenL3; public bool SubscreenL3 { get { return subscreenL3; } set { subscreenL3 = value; } }
        private bool subscreenOBJ; public bool SubscreenOBJ { get { return subscreenOBJ; } set { subscreenOBJ = value; } }
        private bool colorMathL1; public bool ColorMathL1 { get { return colorMathL1; } set { colorMathL1 = value; } }
        private bool colorMathL2; public bool ColorMathL2 { get { return colorMathL2; } set { colorMathL2 = value; } }
        private bool colorMathL3; public bool ColorMathL3 { get { return colorMathL3; } set { colorMathL3 = value; } }
        private bool colorMathOBJ; public bool ColorMathOBJ { get { return colorMathOBJ; } set { colorMathOBJ = value; } }
        private bool colorMathBG; public bool ColorMathBG { get { return colorMathBG; } set { colorMathBG = value; } }
        private byte colorMathHalfIntensity; public byte ColorMathHalfIntensity { get { return colorMathHalfIntensity; } set { colorMathHalfIntensity = value; } }
        private byte colorMathMinusSubscreen; public byte ColorMathMinusSubscreen { get { return colorMathMinusSubscreen; } set { colorMathMinusSubscreen = value; } }
        // accessor functions
        public bool IsDrawingMainscreen(int layer)
        {
            switch (layer)
            {
                case 0:
                    return mainscreenL1;
                case 1:
                    return mainscreenL2;
                case 2:
                    return mainscreenL3;
                case 3:
                    return mainscreenOBJ;
                default:
                    return false;
            }
        }
        public bool IsDrawingSubscreen(int layer)
        {
            switch (layer)
            {
                case 0:
                    return subscreenL1;
                case 1:
                    return subscreenL2;
                case 2:
                    return subscreenL3;
                case 3:
                    return subscreenOBJ;
                default:
                    return false;
            }
        }
        public bool IsDrawingColorMath(int layer)
        {
            switch (layer)
            {
                case 0:
                    return colorMathL1;
                case 1:
                    return colorMathL2;
                case 2:
                    return colorMathL3;
                case 3:
                    return colorMathOBJ;
                case 4:
                    return colorMathBG;
                default:
                    return false;
            }
        }
        // constructor
        public PrioritySet(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            int offset = (index * 3) + 0xFE00;
            int temp = rom[offset++];
            if ((temp & 0x01) == 0x01) colorMathL1 = true;
            if ((temp & 0x02) == 0x02) colorMathL2 = true;
            if ((temp & 0x04) == 0x04) colorMathL3 = true;
            if ((temp & 0x10) == 0x10) colorMathOBJ = true;
            if ((temp & 0x20) == 0x20) colorMathBG = true;
            if ((temp & 0x40) == 0x40) colorMathHalfIntensity = 1; else colorMathHalfIntensity = 0;
            if ((temp & 0x80) == 0x80) colorMathMinusSubscreen = 1; else colorMathMinusSubscreen = 0;
            temp = rom[offset++];
            if ((temp & 0x01) == 0x01) mainscreenL1 = true;
            if ((temp & 0x02) == 0x02) mainscreenL2 = true;
            if ((temp & 0x04) == 0x04) mainscreenL3 = true;
            if ((temp & 0x10) == 0x10) mainscreenOBJ = true;
            temp = rom[offset++];
            if ((temp & 0x01) == 0x01) subscreenL1 = true;
            if ((temp & 0x02) == 0x02) subscreenL2 = true;
            if ((temp & 0x04) == 0x04) subscreenL3 = true;
            if ((temp & 0x10) == 0x10) subscreenOBJ = true;
        }
        public void Assemble()
        {
            int offset = (index * 3) + 0xFE00;
            Bits.SetBit(rom, offset, 0, colorMathL1);
            Bits.SetBit(rom, offset, 1, colorMathL2);
            Bits.SetBit(rom, offset, 2, colorMathL3);
            Bits.SetBit(rom, offset, 4, colorMathOBJ);
            Bits.SetBit(rom, offset, 5, colorMathBG);
            Bits.SetBit(rom, offset, 6, colorMathHalfIntensity == 1);
            Bits.SetBit(rom, offset, 7, colorMathMinusSubscreen == 1);
            offset++;
            Bits.SetBit(rom, offset, 0, mainscreenL1);
            Bits.SetBit(rom, offset, 1, mainscreenL2);
            Bits.SetBit(rom, offset, 2, mainscreenL3);
            Bits.SetBit(rom, offset, 4, mainscreenOBJ);
            offset++;
            Bits.SetBit(rom, offset, 0, subscreenL1);
            Bits.SetBit(rom, offset, 1, subscreenL2);
            Bits.SetBit(rom, offset, 2, subscreenL3);
            Bits.SetBit(rom, offset, 4, subscreenOBJ);
        }
    }
}