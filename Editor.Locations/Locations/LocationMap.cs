using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ZONEDOCTOR
{
    [Serializable()]
    public class LocationMap
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public int Index { get { return index; } set { index = value; } }
        // layering properties
        private byte messageBox; public byte MessageBox { get { return messageBox; } set { messageBox = value; } }
        private byte music; public byte Music { get { return music; } set { music = value; } }
        private bool heatWaveL1; public bool HeatWaveL1 { get { return heatWaveL1; } set { heatWaveL1 = value; } }
        private bool heatWaveL2; public bool HeatWaveL2 { get { return heatWaveL2; } set { heatWaveL2 = value; } }
        private bool searchLights; public bool SearchLights { get { return searchLights; } set { searchLights = value; } }
        private bool warpEnabledX; public bool WarpEnabledX { get { return warpEnabledX; } set { warpEnabledX = value; } }
        private bool warpEnabled; public bool WarpEnabled { get { return warpEnabled; } set { warpEnabled = value; } }
        private bool heatWaveL3; public bool HeatWaveL3 { get { return heatWaveL3; } set { heatWaveL3 = value; } }
        private bool b1b6; public bool B1b6 { get { return b1b6; } set { b1b6 = value; } }
        private bool timerMemory; public bool TimerMemory { get { return timerMemory; } set { timerMemory = value; } }
        private byte spriteMask; public byte SpriteMask { get { return spriteMask; } set { spriteMask = value; } }
        private byte windowMask; public byte WindowMask { get { return windowMask; } set { windowMask = value; } }
        private bool b6b7; public bool B6b7 { get { return b6b7; } set { b6b7 = value; } }
        private byte battleBG; public byte BattleBG { get { return battleBG; } set { battleBG = value; } }
        private byte battleZone; public byte BattleZone { get { return battleZone; } set { battleZone = value; } }
        private bool randomBattle; public bool RandomBattle { get { return randomBattle; } set { randomBattle = value; } }
        private byte maskHighX; public byte MaskHighX { get { return maskHighX; } set { maskHighX = value; } }
        private byte maskHighY; public byte MaskHighY { get { return maskHighY; } set { maskHighY = value; } }
        private byte xNegL2; public byte XNegL2 { get { return xNegL2; } set { xNegL2 = value; } }
        private byte yNegL2; public byte YNegL2 { get { return yNegL2; } set { yNegL2 = value; } }
        private byte xNegL3; public byte XNegL3 { get { return xNegL3; } set { xNegL3 = value; } }
        private byte yNegL3; public byte YNegL3 { get { return yNegL3; } set { yNegL3 = value; } }
        private byte scrolling; public byte Scrolling { get { return scrolling; } set { scrolling = value; } }
        private byte prioritySet; public byte PrioritySet { get { return prioritySet; } set { prioritySet = value; } }
        private bool waveL2; public bool WaveL2 { get { return waveL2; } set { waveL2 = value; } }
        // mapping properties
        private byte animationL2; public byte AnimationL2 { get { return animationL2; } set { animationL2 = value; } }
        private byte animationL3; public byte AnimationL3 { get { return animationL3; } set { animationL3 = value; } }
        private byte animationBG; public byte AnimationBG { get { return animationBG; } set { animationBG = value; } }
        private byte graphicSetA; public byte GraphicSetA { get { return graphicSetA; } set { graphicSetA = value; } }
        private byte graphicSetB; public byte GraphicSetB { get { return graphicSetB; } set { graphicSetB = value; } }
        private byte graphicSetC; public byte GraphicSetC { get { return graphicSetC; } set { graphicSetC = value; } }
        private byte graphicSetD; public byte GraphicSetD { get { return graphicSetD; } set { graphicSetD = value; } }
        private byte graphicSetL3; public byte GraphicSetL3 { get { return graphicSetL3; } set { graphicSetL3 = value; } }
        private bool topPriorityL3; public bool TopPriorityL3 { get { return topPriorityL3; } set { topPriorityL3 = value; } }
        private bool worldMapBG; public bool WorldMapBG { get { return worldMapBG; } set { worldMapBG = value; } }
        private byte tilesetL1; public byte TilesetL1 { get { return tilesetL1; } set { tilesetL1 = value; } }
        private byte tilesetL2; public byte TilesetL2 { get { return tilesetL2; } set { tilesetL2 = value; } }
        private ushort tilemapL1; public ushort TilemapL1 { get { return tilemapL1; } set { tilemapL1 = value; } }
        private ushort tilemapL2; public ushort TilemapL2 { get { return tilemapL2; } set { tilemapL2 = value; } }
        private ushort tilemapL3; public ushort TilemapL3 { get { return tilemapL3; } set { tilemapL3 = value; } }
        private ushort solidityMap; public ushort SoliditySet { get { return solidityMap; } set { solidityMap = value; } }
        private byte paletteSet; public byte PaletteSet { get { return paletteSet; } set { paletteSet = value; } }
        private byte[] width = new byte[3]; public byte[] Width { get { return width; } set { width = value; } }
        private byte[] height = new byte[3]; public byte[] Height { get { return height; } set { height = value; } }
        // accessors
        public int[] Width_t
        {
            get
            {
                if (index < 3)
                    return new int[] { 256, 256, 256 };
                else
                    return new int[]
                    {
                        (int)(16 * Math.Pow(2, width[0])),
                        (int)(16 * Math.Pow(2, width[1])),
                        (int)(16 * Math.Pow(2, width[2]))
                    };
            }
        }
        public int[] Height_t
        {
            get
            {
                if (index < 3)
                    return new int[] { 256, 256, 256 };
                else
                    return new int[]
                    {
                        (int)(16 * Math.Pow(2, height[0])),
                        (int)(16 * Math.Pow(2, height[1])),
                        (int)(16 * Math.Pow(2, height[2]))
                    };
            }
        }
        public int[] Width_p
        {
            get
            {
                return new int[] { Width_t[0] * 16, Width_t[1] * 16, Width_t[2] * 16 };
            }
        }
        public int[] Height_p
        {
            get
            {
                return new int[] { Height_t[0] * 16, Height_t[1] * 16, Height_t[2] * 16 };
            }
        }
        public Size MaximumSize()
        {
            int w = width[0];
            int h = height[0];
            for (int i = 1; i < 3; i++)
            {
                if (width[i] > w)
                    w = width[i];
                if (height[i] > h)
                    h = height[i];
            }
            return new Size((int)(256 * Math.Pow(2, w)), (int)(256 * Math.Pow(2, h)));
        }
        // constructors
        public LocationMap(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            int offset = (index * 33) + 0x2D8F00;
            messageBox = rom[offset++];
            warpEnabledX = (rom[offset] & 0x01) == 0x01;
            warpEnabled = (rom[offset] & 0x02) == 0x02;
            heatWaveL3 = (rom[offset] & 0x04) == 0x04;
            heatWaveL2 = (rom[offset] & 0x08) == 0x08;
            heatWaveL1 = (rom[offset] & 0x10) == 0x10;
            searchLights = (rom[offset] & 0x20) == 0x20;
            b1b6 = (rom[offset] & 0x40) == 0x40;
            timerMemory = (rom[offset++] & 0x80) == 0x80;
            //
            battleBG = (byte)(rom[offset] & 0x7F);
            topPriorityL3 = (rom[offset++] & 0x80) == 0x80;
            worldMapBG = (rom[offset++] == 0x15);
            solidityMap = rom[offset++];
            randomBattle = (rom[offset++] & 0x80) == 0x80;
            windowMask = (byte)(rom[offset] & 0x03);
            b6b7 = (rom[offset++] & 0x80) == 0x80;
            // mapping
            graphicSetA = (byte)(rom[offset] & 0x7F);
            graphicSetB = (byte)(((Bits.GetShort(rom, offset) * 2) & 0x7F00) / 0x100); offset++;
            graphicSetC = (byte)(((Bits.GetShort(rom, offset) * 4) & 0x7F00) / 0x100); offset++;
            graphicSetD = (byte)(((Bits.GetShort(rom, offset) * 8) & 0x7F00) / 0x100); offset++;
            graphicSetL3 = (byte)((Bits.GetShort(rom, offset) / 16) & 0x3F); offset++;
            tilesetL1 = (byte)((Bits.GetShort(rom, offset) / 4) & 0x7F); offset++;
            tilesetL2 = (byte)((rom[offset] / 2) & 0x7F); offset++;
            tilemapL1 = (ushort)(Bits.GetShort(rom, offset) & 0x3FF); offset++;
            tilemapL2 = (ushort)((Bits.GetShort(rom, offset) / 2 & 0x7FE) / 2); offset++;
            tilemapL3 = (ushort)(Bits.GetShort(rom, offset) / 16); offset++;
            if (tilemapL1 > 0x15E) tilemapL1 = 0x15E;
            if (tilemapL2 > 0x15E) tilemapL2 = 0x15E;
            offset++;
            // layer movement
            spriteMask = rom[offset++];
            xNegL2 = rom[offset++];
            yNegL2 = rom[offset++];
            xNegL3 = rom[offset++];
            yNegL3 = rom[offset++];
            scrolling = rom[offset++];
            // mapping
            height[0] = (byte)(rom[offset] >> 4 & 0x03);
            width[0] = (byte)(rom[offset] >> 6);
            height[1] = (byte)(rom[offset] & 0x03);
            width[1] = (byte)(rom[offset] >> 2 & 0x03); offset++;
            height[2] = (byte)(rom[offset] >> 4 & 0x03);
            width[2] = (byte)(rom[offset] >> 6); offset++;
            paletteSet = rom[offset++];
            animationBG = rom[offset++];
            animationL2 = (byte)(rom[offset] & 0x1F);
            animationL3 = (byte)((rom[offset] & 0xE0) >> 5); offset++;
            //
            music = rom[offset]; offset += 2;
            maskHighX = rom[offset++];
            maskHighY = rom[offset++];
            prioritySet = rom[offset];
            battleZone = rom[index + 0x0F5600];
        }
        public void Assemble()
        {
            int offset = (index * 33) + 0x2D8F00;
            rom[offset] = messageBox; offset++;
            Bits.SetBit(rom, offset, 0, warpEnabledX);
            Bits.SetBit(rom, offset, 1, warpEnabled);
            Bits.SetBit(rom, offset, 2, heatWaveL3);
            Bits.SetBit(rom, offset, 3, heatWaveL2);
            Bits.SetBit(rom, offset, 4, heatWaveL1);
            Bits.SetBit(rom, offset, 5, searchLights);
            Bits.SetBit(rom, offset, 6, b1b6);
            Bits.SetBit(rom, offset++, 7, timerMemory);
            //
            rom[offset] = battleBG;
            Bits.SetBit(rom, offset, 7, topPriorityL3); offset++;
            rom[offset] = worldMapBG ? (byte)0x15 : (byte)0; offset++;
            rom[offset] = (byte)solidityMap; offset++;
            Bits.SetBit(rom, offset, 7, randomBattle); offset++;
            rom[offset] = windowMask;
            Bits.SetBit(rom, offset++, 7, b6b7);
            //
            rom[offset] = graphicSetA;
            Bits.SetShortBits(rom, offset, (ushort)(graphicSetB << 7), 0x3F80); offset++;
            Bits.SetShortBits(rom, offset, (ushort)(graphicSetC << 6), 0x1FC0); offset++;
            Bits.SetShortBits(rom, offset, (ushort)(graphicSetD << 5), 0x0FE0); offset++;
            Bits.SetShortBits(rom, offset, (ushort)(graphicSetL3 << 4), 0x03F0); offset++;
            Bits.SetShortBits(rom, offset, (ushort)(tilesetL1 << 2), 0x01FC); offset++;
            Bits.SetShortBits(rom, offset, (ushort)(tilesetL2 << 1), 0x00FE); offset++;
            Bits.SetShortBits(rom, offset, (ushort)(tilemapL1), 0x03FF); offset++;
            Bits.SetShortBits(rom, offset, (ushort)(tilemapL2 << 2), 0x1FF8); offset++;
            Bits.SetShortBits(rom, offset, (ushort)(tilemapL3 << 4), 0xFFF0); offset++;
            offset++;
            //
            rom[offset++] = spriteMask;
            rom[offset] = xNegL2; offset++;
            rom[offset] = yNegL2; offset++;
            rom[offset] = xNegL3; offset++;
            rom[offset] = yNegL3; offset++;
            rom[offset] = scrolling; offset++;
            //
            rom[offset] = (byte)(height[0] << 4);
            rom[offset] |= (byte)(width[0] << 6);
            rom[offset] |= height[1];
            rom[offset] |= (byte)(width[1] << 2); offset++;
            Bits.SetByteBits(rom, offset, (byte)(height[2] << 4), 0x30);
            Bits.SetByteBits(rom, offset, (byte)(width[2] << 6), 0xC0); offset++;
            rom[offset] = paletteSet; offset++;
            rom[offset] = animationBG; offset++;
            rom[offset] = animationL2;
            rom[offset] |= (byte)(animationL3 << 5); offset++;
            //
            rom[offset] = music; offset += 2;
            rom[offset] = maskHighX; offset++;
            rom[offset] = maskHighY; offset++;
            rom[offset] = prioritySet;
            rom[index + 0x0F5600] = battleZone;
        }
        // universal functions
        public void Clear()
        {
            messageBox = 0;
            warpEnabledX = false;
            warpEnabled = false;
            heatWaveL3 = false;
            heatWaveL2 = false;
            heatWaveL1 = false;
            searchLights = false;
            b1b6 = false;
            timerMemory = false;
            battleBG = 0;
            topPriorityL3 = false;
            worldMapBG = false;
            solidityMap = 0;
            randomBattle = false;
            windowMask = 0;
            b6b7 = false;
            graphicSetA = 0;
            graphicSetB = 0;
            graphicSetC = 0;
            graphicSetD = 0;
            graphicSetL3 = 0;
            tilesetL1 = 0;
            tilesetL2 = 0;
            tilemapL1 = 0;
            tilemapL2 = 0;
            tilemapL3 = 0;
            tilemapL1 = 0;
            tilemapL2 = 0;
            xNegL2 = 0;
            yNegL2 = 0;
            xNegL3 = 0;
            yNegL3 = 0;
            scrolling = 0;
            height[0] = 0;
            width[0] = 0;
            height[1] = 0;
            width[1] = 0;
            height[2] = 0;
            width[2] = 0;
            paletteSet = 0;
            animationL2 = 0;
            animationL3 = 0;
            music = 0;
            maskHighX = 0;
            maskHighY = 0;
            prioritySet = 0;
            battleZone = 0;
        }
    }
}
