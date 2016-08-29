using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms; // remove later
using System.IO;
using System.Security.Cryptography;
using ZONEDOCTOR.ScriptsEditor;
using ZONEDOCTOR.ScriptsEditor.Commands;
using ZONEDOCTOR.Properties;
using ZONEDOCTOR._Static;

namespace ZONEDOCTOR
{
    public static class Model
    {
        private static Settings settings = Settings.Default;
        private static Program program;
        public static Program Program { get { return program; } set { program = value; } }
        private static byte[] rom;
        public static byte[] ROM { get { return rom; } set { rom = value; } }
        private static ProjectDB project;
        public static ProjectDB Project
        {
            get
            {
                if (project == null)
                    project = new ProjectDB();
                return project;
            }
            set
            {
                project = value;
            }
        }
        private static HexEditor hexEditor;
        public static HexEditor HexEditor { get { return hexEditor; } set { hexEditor = value; } }
        public static string History
        {
            get
            {
                return settings.History;
            }
            set
            {
                StringReader reader = new StringReader(value);
                string lines = "";
                string line;
                for (int i = 0; i < 256 && (line = reader.ReadLine()) != null; i++)
                    lines += line + "\r\n";
                settings.History = lines;
            }
        }
        public static bool Crashing = false;
        // rom signature
        private static byte[] header;
        private static int romLength = 0;
        private static string fileName;
        private static long checkSum = 0;
        private static bool locked = false;
        private static bool published = false;
        public static bool Locked { get { return locked; } set { locked = value; } } // Indicates that this rom is locked and cannot be edited, not for public static use
        public static bool Published { get { return published; } set { published = value; } } // If true, show Author Splash screen on load
        private static byte[] romHash;
        public static byte[] ROMHash { get { return romHash; } set { romHash = value; } }
        // lists
        public static List<EList> ELists;
        #region Variables and Accessors
        #region Dialogues
        private static string[] locationNames;
        public static string[] LocationNames
        {
            get
            {
                if (locationNames == null)
                {
                    locationNames = new string[73];
                    for (int i = 0; i < locationNames.Length; i++)
                    {
                        locationNames[i] = "";
                        int offset = Bits.GetShort(rom, i * 2 + 0x268400) + 0x0EF100;
                        while (rom[offset] != 0)
                        {
                            byte temp = rom[offset++];
                            locationNames[i] += Lists.DialogueTable[temp];
                        }
                    }
                }
                return locationNames;
            }
            set { locationNames = value; }
        }
        private static Dialogue[] dialogues;
        public static Dialogue[] Dialogues
        {
            get
            {
                if (dialogues == null)
                {
                    // create dialogues
                    dialogues = new Dialogue[3326];
                    for (int i = 0; i < dialogues.Length; i++)
                        dialogues[i] = new Dialogue(i);
                }
                return dialogues;
            }
            set { dialogues = value; }
        }
        public static Dialogue[] GetDialogues(int start, int end)
        {
            if (dialogues != null)
                return dialogues;
            // create dialogues
            Dialogue[] temp = new Dialogue[end - start];
            for (int i = start; i < end; i++)
                temp[i] = new Dialogue(i);
            return temp;
        }
        #endregion
        #region Locations
        // compressed data
        private static byte[][] graphicSets = new byte[82][];
        private static byte[][] graphicSetsL3 = new byte[19][];
        private static byte[][] tilesets = new byte[75][];
        private static byte[][] tilemaps = new byte[351][];
        private static byte[][] soliditySets = new byte[43][];
        private static byte[] animatedGraphics;
        private static byte[] wobGraphicSet;
        private static byte[] wobTilemap;
        private static byte[] wobSolidity;
        private static byte[] worGraphicSet;
        private static byte[] worTilemap;
        private static byte[] worSolidity;
        private static byte[] stGraphicSet;
        private static byte[] stTilemap;
        private static byte[] stPaletteSet;
        public static byte[][] GraphicSets
        {
            get
            {
                if (graphicSets[0] == null)
                {
                    for (int i = 0; i < 82; i++)
                    {
                        int pointer = (i * 3) + 0x1FDA00;
                        int offset = (int)(Bits.GetInt24(rom, pointer) + 0x1FDB00);
                        graphicSets[i] = Bits.GetBytes(rom, offset, 0x2000);
                    }
                }
                return graphicSets;
            }
            set { graphicSets = value; }
        }
        public static byte[][] GraphicSetsL3
        {
            get
            {
                if (graphicSetsL3[0] == null)
                {
                    for (int i = 0; i < 19; i++)
                    {
                        int pointer = (i * 3) + 0x26CD60;
                        int offset = (int)(Bits.GetInt24(rom, pointer) + 0x268780);
                        graphicSetsL3[i] = Comp.Decompress(rom, offset, 0x1040);
                    }
                }
                return graphicSetsL3;
            }
            set { graphicSetsL3 = value; }
        }
        public static byte[][] Tilesets
        {
            get
            {
                if (tilesets[0] == null)
                    Decompress(tilesets, 0x1FBA00, 0x1E0000, "TILE SET", 0x800, 3, 0, tilesets.Length, null);
                return tilesets;
            }
            set { tilesets = value; }
        }
        public static byte[][] Tilemaps
        {
            get
            {
                if (tilemaps[0] == null)
                    Decompress(tilemaps, 0x19CD90, 0x19D1B0, "TILE MAP", 0x4000, 3, 0, tilemaps.Length, TilemapSizes);
                return tilemaps;
            }
            set { tilemaps = value; }
        }
        public static byte[][] SoliditySets
        {
            get
            {
                if (soliditySets[0] == null)
                    Decompress(soliditySets, 0x19CD10, 0x19A800, "SOLIDITY SET", 0x200, 2, 0, soliditySets.Length, null);
                return soliditySets;
            }
            set { soliditySets = value; }
        }
        public static byte[] AnimatedGraphics
        {
            get
            {
                if (animatedGraphics == null)
                    animatedGraphics = Bits.GetBytes(rom, 0x260000, 0x8000);
                return animatedGraphics;
            }
            set { animatedGraphics = value; }
        }
        public static byte[] WOBTilemap
        {
            get
            {
                if (wobTilemap == null)
                    wobTilemap = Comp.Decompress(rom, (int)Model.HiROMToSMC(Model.m_varCompDataAbsPtrs[Model.IDX_VARP_MAP_WOB]), 0x10000);
                return wobTilemap;
            }
            set { wobTilemap = value; }
        }
        public static byte[] WORTilemap
        {
            get
            {
                if (worTilemap == null)
                    worTilemap = Comp.Decompress(rom, (int)Model.HiROMToSMC(Model.m_varCompDataAbsPtrs[Model.IDX_VARP_MAP_WOR]), 0x10000);
                return worTilemap;
            }
            set { worTilemap = value; }
        }
        public static byte[] WOBGraphicSet
        {
            get
            {
                if (wobGraphicSet == null)
                    wobGraphicSet = Comp.Decompress(rom, (int)Model.HiROMToSMC(Model.m_varCompDataAbsPtrs[Model.IDX_VARP_TILE_WOB]), 0x2480);
                return wobGraphicSet;
            }
            set { wobGraphicSet = value; }
        }
        public static byte[] WORGraphicSet
        {
            get
            {
                if (worGraphicSet == null)
                    worGraphicSet = Comp.Decompress(rom, (int)Model.HiROMToSMC(Model.m_varCompDataAbsPtrs[Model.IDX_VARP_TILE_WOR]), 0x2480);
                return worGraphicSet;
            }
            set { worGraphicSet = value; }
        }
        public static byte[] WOBSolidity
        {
            get
            {
                if (wobSolidity == null)
                    wobSolidity = Bits.GetBytes(rom, 0x2E9B14, 0x200);
                return wobSolidity;
            }
            set { wobSolidity = value; }
        }
        public static byte[] WORSolidity
        {
            get
            {
                if (worSolidity == null)
                    worSolidity = Bits.GetBytes(rom, 0x2E9D14, 0x200);
                return worSolidity;
            }
            set { worSolidity = value; }
        }
        public static byte[] STGraphicSet
        {
            get
            {
                if (stGraphicSet == null)
                    stGraphicSet = Comp.Decompress(rom, 0x2FB631, 0x2480);
                return stGraphicSet;
            }
            set { stGraphicSet = value; }
        }
        public static byte[] STTilemap
        {
            get
            {
                if (stTilemap == null)
                    stTilemap = Comp.Decompress(rom, 0x2F9D17, 0x4000);
                return stTilemap;
            }
            set { stTilemap = value; }
        }
        public static byte[] STPaletteSet
        {
            get
            {
                if (stPaletteSet == null)
                    stPaletteSet = Comp.Decompress(rom, 0x18E6BA, 0x200);
                return stPaletteSet;
            }
            set { stPaletteSet = value; }
        }
        public static bool[] EditGraphicSets = new bool[82];
        public static bool[] EditTilesets = new bool[75];
        public static bool[] EditTilemaps = new bool[351];
        public static bool[] EditSoliditySets = new bool[43];
        public static ushort[] TilemapSizes = new ushort[351];
        public static bool EditWOBGraphicSet;
        public static bool EditWOBTilemap;
        public static bool EditWORGraphicSet;
        public static bool EditWORTilemap;
        public static bool EditSTGraphicSet;
        public static bool EditSTTilemap;
        // properties
        private static Location[] locations;
        private static PaletteSet[] paletteSets;
        private static PrioritySet[] prioritySets;
        public static Location[] Locations
        {
            get
            {
                if (locations == null)
                {
                    locations = new Location[415];
                    for (int i = 0; i < locations.Length; i++)
                        locations[i] = new Location(i);
                }
                return locations;
            }
            set { locations = value; }
        }
        public static PaletteSet[] PaletteSets
        {
            get
            {
                if (paletteSets == null)
                {
                    paletteSets = new PaletteSet[51];
                    for (int i = 0; i < paletteSets.Length; i++)
                    {
                        if (i == 48)
                            paletteSets[i] = new PaletteSet(rom, i, 0x12EC00, 8, 16, 32);
                        else if (i == 49)
                            paletteSets[i] = new PaletteSet(rom, i, 0x12ED00, 8, 16, 32);
                        else if (i == 50)
                            paletteSets[i] = new PaletteSet(STPaletteSet, 0, 0, 8, 16, 32);
                        else
                            paletteSets[i] = new PaletteSet(rom, i, (i * 0x100) + 0x2DC480, 8, 16, 32);
                    }
                }
                return paletteSets;
            }
            set { paletteSets = value; }
        }
        public static PrioritySet[] PrioritySets
        {
            get
            {
                if (prioritySets == null)
                {
                    prioritySets = new PrioritySet[19];
                    for (int i = 0; i < prioritySets.Length; i++)
                        prioritySets[i] = new PrioritySet(i);
                }
                return prioritySets;
            }
            set { prioritySets = value; }
        }
        #endregion
        #region Scripts
        private static List<EventScript> eventScripts;
        public static List<EventScript> EventScripts
        {
            get
            {
                if (eventScripts == null)
                {
                    int offset = 0x0CFF01;
                    // first check if haven't stored special offsets yet
                    if (rom[0x0CFF00] != 1)
                    {
                        rom[0x0CFF00] = 1;
                        foreach (int pointer in Lists.SpecialPointers_Vehicle)
                        {
                            Bits.SetInt24(rom, offset, pointer);
                            offset += 3;
                        }
                        foreach (int pointer in Lists.SpecialPointers_Map)
                        {
                            Bits.SetInt24(rom, offset, pointer);
                            offset += 3;
                        }
                        foreach (int pointer in Lists.SpecialPointers_Cancel)
                        {
                            Bits.SetInt24(rom, offset, pointer);
                            offset += 3;
                        }
                    }
                    offset = 0x0A0000;
                    int index = 0;
                    eventScripts = new List<EventScript>();
                    int offsetEnd = 0x0CE600;
                    while (rom[--offsetEnd] == 0xFF) ;
                    while (offset <= offsetEnd)
                        eventScripts.Add(new EventScript(index++, ref offset));
                }
                return eventScripts;
            }
            set { eventScripts = value; }
        }
        public static EventScript GetEventScript(int offset)
        {
            try
            {
                foreach (EventScript script in EventScripts)
                {
                    foreach (EventCommand command in script.Commands)
                    {
                        if (command.Queue != null)
                        {
                            foreach (ActionCommand action in command.Queue.Commands)
                            {
                                if (action.Offset + action.CommandData.Length > offset || action.Offset >= offset)
                                {
                                    return script;
                                }
                            }
                        }
                        if (command.Offset + command.Length > offset || command.Offset >= offset)
                        {
                            return script;
                        }
                    }
                }
            }
            catch
            {
            }
            return null;
        }
        private static int[] specialPointers_Vehicle;
        public static int[] SpecialPointers_Vehicle
        {
            get
            {
                if (specialPointers_Vehicle == null)
                {
                    specialPointers_Vehicle = new int[21];
                    for (int i = 0; i < 21; i++)
                        specialPointers_Vehicle[i] = Bits.GetInt24(rom, i * 3 + 0x0CFF01);
                }
                return specialPointers_Vehicle;
            }
            set { specialPointers_Vehicle = value; }
        }
        private static int[] specialPointers_Map;
        public static int[] SpecialPointers_Map
        {
            get
            {
                if (specialPointers_Map == null)
                {
                    specialPointers_Map = new int[5];
                    for (int i = 0; i < 5; i++)
                        specialPointers_Map[i] = Bits.GetInt24(rom, i * 3 + 0x0CFF40);
                }
                return specialPointers_Map;
            }
            set { specialPointers_Map = value; }
        }
        private static int[] specialPointers_Cancel;
        public static int[] SpecialPointers_Cancel
        {
            get
            {
                if (specialPointers_Cancel == null)
                {
                    specialPointers_Cancel = new int[3];
                    for (int i = 0; i < 3; i++)
                        specialPointers_Cancel[i] = Bits.GetInt24(rom, i * 3 + 0x0CFF4F);
                }
                return specialPointers_Cancel;
            }
            set { specialPointers_Cancel = value; }
        }
        private static int[] specialPointers_ASM;
        public static int[] SpecialPointers_ASM
        {
            get
            {
                if (specialPointers_ASM == null)
                {
                    specialPointers_ASM = new int[25];
                    for (int i = 0; i < 25; i++)
                        specialPointers_ASM[i] = Bits.GetShort(rom, Lists.SpecialPointers_ASMPointers[i]) + 0x0A0000;
                }
                return specialPointers_ASM;
            }
            set { specialPointers_ASM = value; }
        }
        public static int SpecialDelta = 0;
        /// <summary>
        /// Updates all special pointers that pointer after the offset of a given command by a delta value.
        /// </summary>
        /// <param name="eac">The command whose offset will be compared.</param>
        /// <param name="delta">The amount to modify the special pointer by.</param>
        public static void UpdateSpecialPointers(EventActionCommand eac, int delta)
        {
            // update all special offsets
            for (int i = 0; i < Model.SpecialPointers_Vehicle.Length; i++)
                if (Model.SpecialPointers_Vehicle[i] > eac.Offset)
                    Model.SpecialPointers_Vehicle[i] += delta;
            for (int i = 0; i < Model.SpecialPointers_Map.Length; i++)
                if (Model.SpecialPointers_Map[i] > eac.Offset)
                    Model.SpecialPointers_Map[i] += delta;
            for (int i = 0; i < Model.SpecialPointers_Cancel.Length; i++)
                if (Model.SpecialPointers_Cancel[i] > eac.Offset)
                    Model.SpecialPointers_Cancel[i] += delta;
            // update the ASM offsets (bank C0 only)
            for (int i = 0; i < Model.SpecialPointers_ASM.Length; i++)
                if (Model.SpecialPointers_ASM[i] > eac.Offset)
                    Model.SpecialPointers_ASM[i] += delta;
        }
        #endregion
        #region Stats Names
        private static SortedList esperNames;
        private static SortedList characterNames;
        private static SortedList monsterNames;
        private static SortedList spellNames;
        private static SortedList attackNames;
        private static SortedList itemNames;
        public static SortedList EsperNames
        {
            get
            {
                if (esperNames == null)
                {
                    esperNames = new SortedList(27, 8, 0x26F6E1);
                }
                return esperNames;
            }
            set
            {
                esperNames = value;
            }
        }
        public static SortedList CharacterNames
        {
            get
            {
                if (characterNames == null)
                {
                    characterNames = new SortedList(64, 6, 0x0478C0);
                }
                return characterNames;
            }
            set
            {
                characterNames = value;
            }
        }
        public static SortedList MonsterNames
        {
            get
            {
                if (monsterNames == null)
                {
                    monsterNames = new SortedList(0, 0, 0);
                    monsterNames.SortAlphabetically();
                }
                return monsterNames;
            }
            set
            {
                monsterNames = value;
                if (monsterNames != null)
                    monsterNames.SortAlphabetically();
            }
        }
        public static SortedList SpellNames
        {
            get
            {
                if (spellNames == null)
                {
                    spellNames = new SortedList(0, 0, 0);
                    spellNames.SortAlphabetically();
                }
                return spellNames;
            }
            set
            {
                spellNames = value;
                if (spellNames != null)
                    spellNames.SortAlphabetically();
            }
        }
        public static SortedList AttackNames
        {
            get
            {
                if (attackNames == null)
                {
                    attackNames = new SortedList(0, 0, 0);
                    attackNames.SortAlphabetically();
                }
                return attackNames;
            }
            set
            {
                attackNames = value;
                if (attackNames != null)
                    attackNames.SortAlphabetically();
            }
        }
        public static SortedList ItemNames
        {
            get
            {
                if (itemNames == null)
                {
                    itemNames = new SortedList(256, 13, 0x12B301);
                    //itemNames.SortAlphabetically();
                }
                return itemNames;
            }
            set
            {
                itemNames = value;
                //if (itemNames != null)
                //    itemNames.SortAlphabetically();
            }
        }
        public static string[] ObjectNames
        {
            get
            {
                string[] names = new string[256];
                for (int i = 0; i < names.Length; i++)
                {
                    if (i >= 0 && i <= 0x0D)
                        names[i] = Model.CharacterNames.GetUnsortedName(i);
                    else if (i >= 0x0E && i <= 0x0F)
                        names[i] = "Actor in slot " + i;
                    else if (i >= 0x10 && i <= 0x2F)
                        names[i] = "NPC " + (i - 0x10).ToString();
                    else if (i == 0x30)
                        names[i] = "Camera";
                    else if (i >= 0x31 && i <= 0x34)
                        names[i] = "Party Character " + (i - 0x31).ToString();
                    else if (i == 0xFF)
                        names[i] = "<EMPTY>";
                    else
                        names[i] = "<Invalid Character>";
                }
                return names;
            }
        }
        #endregion
        private static byte[] numerals;
        public static byte[] Numerals
        {
            get
            {
                if (numerals == null)
                {
                    numerals = Comp.Decompress(rom, 0x12E000, 0x2000);
                }
                return numerals;
            }
            set { numerals = value; }
        }
        #endregion
        #region Functions
        #region File Handling
        public static bool VerifyRom()
        {
            // not implemented yet!
            if (!locked) // and verified
                return true;
            return false;
        }
        public static void CalculateAndSetNewRomChecksum()
        {
            int check = 0;
            for (int i = 0; i < rom.Length; i++)
                check += rom[i];
            check &= 0xFFFF;
            Bits.SetShort(rom, 0x00FFDE, (ushort)check);
        }
        public static void CreateNewMD5Checksum()
        {
            MD5 md5Hasher = MD5.Create();
            if (rom != null)
                romHash = md5Hasher.ComputeHash(rom);
        }
        public static string GameCode()
        {
            Encoding encoding = Encoding.UTF8;
            return encoding.GetString(Bits.GetBytes(rom, 0xFFB2, 4));
        }
        public static string GetEditorNameWithoutPath()
        {
            int len = GetEditorPath().LastIndexOf('\\') + 1;
            return GetEditorPath().Substring(len, GetEditorPath().Length - len);
        }
        public static string GetEditorPath()
        {
            return Application.ExecutablePath;
        }
        public static string GetEditorPathWithoutFileName()
        {
            return GetEditorPath().Substring(0, GetEditorPath().LastIndexOf('\\') + 1);
        }
        public static string GetFileNameWithoutPath()
        {
            try
            {
                return fileName.Substring(fileName.LastIndexOf('\x5c') + 1);
            }
            catch
            {
                return null;
            }
        }
        public static string GetFileNameWithoutPathOrExtension()
        {
            string ret = fileName.Substring(fileName.LastIndexOf('\x5c') + 1);
            return ret.Substring(0, ret.LastIndexOf('.'));
        }
        public static long GetFileSize()
        {
            return romLength;
        }
        public static string GetPathWithoutFileName()
        {
            return fileName.Substring(0, fileName.LastIndexOf('\x5c') + 1);
        }
        public static string GetRomName()
        {
            Encoding encoding = Encoding.UTF8;
            if ((romLength & 0x200) == 0x200)
                return encoding.GetString(Bits.GetBytes(rom, 0x101C0, 21));
            return encoding.GetString(Bits.GetBytes(rom, 0xFFC0, 21));
        }
        public static bool HeaderPresent()
        {
            return header != null;
        }
        public static bool ReadRom()
        {
            try
            {
                FileInfo fInfo = new FileInfo(fileName);
                romLength = (int)fInfo.Length;
                FileStream fStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fStream);
                rom = br.ReadBytes((int)romLength);
                br.Close();
                fStream.Close();
                if (settings.CreateBackupROM)
                {
                    DateTime currentTime = DateTime.Now;
                    string backup = " (open @ " +
                        currentTime.Year.ToString("d4") + currentTime.Month.ToString("d2") + currentTime.Day.ToString("d2") + "_" +
                        currentTime.Hour.ToString("d2") + "h" + currentTime.Minute.ToString("d2") + "m" + currentTime.Second.ToString("d2") + "s" +
                        ").bak";
                    BinaryWriter bw;
                    if (settings.BackupROMDirectory == "")
                    {
                        bw = new BinaryWriter(File.Create(fileName + backup));
                        bw.Write(rom);
                        bw.Close();
                    }
                    else
                    {
                        DirectoryInfo di = new DirectoryInfo(settings.BackupROMDirectory);
                        if (di.Exists)
                        {
                            bw = new BinaryWriter(File.Create(settings.BackupROMDirectory + GetFileNameWithoutPath() + backup));
                            bw.Write(rom);
                            bw.Close();
                        }
                        else
                            MessageBox.Show("Could not create backup ROM.\n\nThe backup ROM directory has been moved, renamed, or no longer exists.", "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                RemoveHeader();
                hexEditor = new HexEditor(rom, Bits.Copy(rom));
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Zone Doctor was unable to load the rom.\n\n" + e.Message,
                    "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                fileName = "Invalid File";
                return false;
            }
        }
        public static bool RemoveHeader()
        {
            header = null;
            if ((romLength & 0x200) != 0x200)
                return false;
            try
            {
                romLength -= 0x200;
                header = Bits.GetBytes(rom, 0, 0x200);
                byte[] temp = Bits.GetBytes(rom, 0x200, romLength);
                rom = temp;
                return true;
            }
            catch
            {
                MessageBox.Show("Error removing header; please remove manually.");
                return false;
            }
        }
        public static bool AddHeader()
        {
            if (header == null) return false;
            try
            {
                romLength += 0x200;
                byte[] temp = new byte[romLength];
                header.CopyTo(temp, 0);
                rom.CopyTo(temp, 0x200);
                rom = temp;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static string RomChecksum()
        {
            int chunk0 = 0;
            int chunk1 = 0;
            for (int i = 0; i < rom.Length; i++)
            {
                if (i < 0x200000)
                    chunk0 += rom[i];
                else
                    chunk1 += rom[i];
            }
            checkSum = (chunk0 + chunk1 + chunk1) & 0xFFFF;
            //
            if ((ushort)checkSum == Bits.GetShort(rom, 0x00FFDE))
                return "0x" + checkSum.ToString("X") + " (OK)";
            else
                return "0x" + checkSum.ToString("X") + " (FAIL)";
        }
        public static ushort RomChecksumBin()
        {
            int chunk0 = 0;
            int chunk1 = 0;
            for (int i = 0; i < rom.Length; i++)
                if (i < 0x200000)
                    chunk0 += rom[i];
                else
                    chunk1 += rom[i];
            checkSum = (chunk0 + chunk1 + chunk1) & 0xFFFF;
            //
            return (ushort)checkSum;
        }
        public static void SetRomChecksum()
        {
            int chunk0 = 0;
            int chunk1 = 0;
            for (int i = 0; i < rom.Length; i++)
            {
                if (i < 0x200000)
                    chunk0 += rom[i];
                else
                    chunk1 += rom[i];
            }
            checkSum = (chunk0 + chunk1 + chunk1) & 0xFFFF;
            //
            Bits.SetShort(rom, 0xFFDE, (int)(checkSum & 0xFFFF));
            Bits.SetShort(rom, 0xFFDC, (int)(checkSum ^ 0xFFFF));
        }
        public static string FileName { get { return fileName; } set { fileName = value; } }
        public static bool VerifyMD5Checksum()
        {
            MD5 md5Hasher = MD5.Create();
            byte[] hash;
            if (romHash != null)
                hash = md5Hasher.ComputeHash(rom);
            else
                return true;
            for (int i = 0; i < romHash.Length && i < hash.Length; i++)
                if (romHash[i] != hash[i])
                    return false;
            return true;
        }
        public static bool WriteRom()
        {
            try
            {
                SetRomChecksum();
                AddHeader();
                BinaryWriter binWriter = new BinaryWriter(File.Open(fileName, FileMode.Create));
                binWriter.Write(rom);
                binWriter.Close();
                if (Settings.Default.CreateBackupROMSave)
                {
                    DateTime currentTime = DateTime.Now;
                    string backup = " (save @ " +
                        currentTime.Year.ToString("d4") + currentTime.Month.ToString("d2") + currentTime.Day.ToString("d2") + "_" +
                        currentTime.Hour.ToString("d2") + "h" + currentTime.Minute.ToString("d2") + "m" + currentTime.Second.ToString("d2") + "s" +
                        ").bak";
                    BinaryWriter bw;
                    if (settings.BackupROMDirectory == "")
                    {
                        bw = new BinaryWriter(File.Create(fileName + backup));
                        bw.Write(rom);
                        bw.Close();
                    }
                    else
                    {
                        DirectoryInfo di = new DirectoryInfo(settings.BackupROMDirectory);
                        if (di.Exists)
                        {
                            bw = new BinaryWriter(File.Create(settings.BackupROMDirectory + GetFileNameWithoutPath() + backup));
                            bw.Write(rom);
                            bw.Close();
                        }
                        else
                            MessageBox.Show("Could not create backup ROM.\n\nThe backup ROM directory has been moved, renamed, or no longer exists.", "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                RemoveHeader();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Zone Doctor was unable to write to the file.\n\n" + ex.Message, "ZONE DOCTOR");
                RemoveHeader();
                return false;
            }
        }
        #endregion
        #region Compression
        public static void Decompress(byte[][] arrays, int pointerOffset, int offsetStart, string label, int maxSize, int pointerSize, int start, int end, ushort[] orig_sizes)
        {
            ProgressBar progressBar = new ProgressBar(rom, "DECOMPRESSING " + label + "S...", arrays.Length);
            progressBar.Show();
            for (int i = start; i < arrays.Length && i < end; i++)
            {
                int pointer = (i * pointerSize) + pointerOffset;
                int offset;
                if (pointerSize == 2)
                    offset = (int)(Bits.GetShort(rom, pointer) + offsetStart);
                else
                    offset = (int)(Bits.GetInt24(rom, pointer) + offsetStart);
                if (orig_sizes == null)
                    arrays[i] = Comp.Decompress(rom, offset, maxSize);
                else
                    arrays[i] = Comp.Decompress(rom, offset, maxSize, ref orig_sizes[i]);
                progressBar.PerformStep("DECOMPRESSING " + label + " #" + i.ToString("d" + arrays.Length.ToString().Length));
            }
            progressBar.Close();
        }
        public static void DecompressWorldMaps()
        {
            object dummy;
            dummy = WOBTilemap;
            dummy = WORTilemap;
            dummy = WOBGraphicSet;
            dummy = WORGraphicSet;
            dummy = WOBMiniMap;
            dummy = WORMiniMap;
        }
        /// <summary>
        /// Compresses a collection of arrays and stores the compressed results to the ROM.
        /// Returns the amount of bytes available.
        /// </summary>
        /// <param name="arrays">The arrays to compress.</param>
        /// <param name="edit">The conditions which determine whether or not to recompress an array.</param>
        /// <param name="bankStart">The bank where the compressed data begins.</param>
        /// <param name="lastOffset">The final offset in the ROM of the allocated data containing the compressed arrays.</param>
        /// <param name="label">The label to use in the progress bar. All caps and singular.</param>
        /// <param name="bankIndexes">Each parameter is the index at which the collection of arrays begins writing to that respective bank.
        /// ie. the first parameter is always 0, the second parameter is where the index begins in the second bank, etc.</param>
        public static int Compress(byte[][] arrays, byte[] dest, bool[] edit, int pointerOffset, int offsetStart, int offsetEnd, string label, int pointerSize, int maxSize, bool tileMaps)
        {
            int size, offset, pointer;
            byte[] compressed;
            // store original
            byte[][] original = new byte[arrays.Length][];
            for (int i = 0; i < arrays.Length; i++)
            {
                pointer = (i * pointerSize) + pointerOffset;
                if (pointerSize == 2)
                    offset = (int)(Bits.GetShort(rom, pointer) + offsetStart);
                else
                    offset = (int)(Bits.GetInt24(rom, pointer) + offsetStart);
                if (i == arrays.Length - 1)
                    original[i] = Bits.GetBytes(rom, offset, offsetEnd - offset);
                else
                    original[i] = Bits.GetBytes(rom, offset, Bits.GetShort(rom, offset));
            }
            // create a progress bar
            ProgressBar progressBar = new ProgressBar(rom, "COMPRESSING " + label + "S", arrays.Length);
            progressBar.Show();
            // now start compressing the data and storing to ROM
            pointer = 0; offset = offsetStart;
            for (int i = 0; i < arrays.Length; i++)
            {
                if (edit != null && edit[i])
                {
                    if (!tileMaps)
                        compressed = new byte[maxSize];
                    else
                        compressed = new byte[TilemapSizes[i]];
                    size = Comp.Compress(arrays[i], compressed);
                }
                else
                {
                    compressed = original[i];
                    size = compressed.Length;
                }
                if (offset + size > offsetEnd) // not enough space
                {
                    MessageBox.Show("Could not save all " + label + "S. " +
                        "Stopped saving at " + label + " #" + i.ToString(),
                        "ZONE DOCTOR");
                    break;
                }
                else if (dest != null)
                {
                    Bits.SetShort(dest, pointerOffset + (i * pointerSize), (ushort)((offset - offsetStart) & 0xFFFF));
                    if (pointerSize == 3)
                        dest[pointerOffset + (i * pointerSize) + 2] = (byte)((offset - offsetStart) >> 16);
                    Bits.SetBytes(dest, offset, compressed);
                }
                offset += size;
                progressBar.PerformStep("COMPRESSING " + label + " #" + i.ToString("X3"));
            }
            progressBar.Close();
            return offsetEnd - offset;
        }
        public static int Compress(byte[] array, byte[] dest, bool edit, int offsetStart, int offsetEnd, int maxSize, string label)
        {
            int size = 0;
            byte[] compressed = new byte[maxSize];
            byte[] original = Bits.GetBytes(rom, offsetStart, offsetEnd - offsetStart);
            if (edit)
            {
                size = Comp.Compress(array, compressed);
                if (offsetStart + size > offsetEnd)
                    MessageBox.Show(
                        "Recompressed " + label + " exceeds allotted space.\n" +
                        "The " + label + " was not saved.",
                        "WARNING: NOT ENOUGH SPACE FOR " + label + "S.",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                else if (dest != null)
                    Bits.SetBytes(dest, offsetStart, compressed, 0, size);
            }
            else
            {
                size = original.Length;
                if (dest != null)
                    Bits.SetBytes(dest, offsetStart, original);
            }
            return offsetEnd - (offsetStart + size);
        }
        #endregion
        #region Assemblers
        public static void LoadAll()
        {
            object dummy;
            dummy = LocationNames;
            dummy = Dialogues;
            dummy = GraphicSets;
            dummy = GraphicSetsL3;
            dummy = Project;
            dummy = Tilesets;
            dummy = Tilemaps;
            dummy = SoliditySets;
            dummy = AnimatedGraphics;
            dummy = WOBGraphicSet;
            dummy = WOBTilemap;
            dummy = WOBSolidity;
            dummy = WORGraphicSet;
            dummy = WORTilemap;
            dummy = WORSolidity;
            dummy = STGraphicSet;
            dummy = STTilemap;
            dummy = STPaletteSet;
        }
        public static void ClearModel()
        {
            locationNames = null;
            dialogues = null;
            eventScripts = null;
            graphicSets[0] = null;
            graphicSetsL3[0] = null;
            tilesets[0] = null;
            tilemaps[0] = null;
            soliditySets[0] = null;
            animatedGraphics = null;
            wobGraphicSet = null;
            wobTilemap = null;
            wobSolidity = null;
            worGraphicSet = null;
            worTilemap = null;
            worSolidity = null;
            stGraphicSet = null;
            stTilemap = null;
            stPaletteSet = null;
        }
        public static void CreateListCollections()
        {
            ELists = new List<EList>();
            ELists.Add(new EList("Locations", Lists.Copy(Lists.LocationNames)));
            ELists.Add(new EList("Songs", Lists.Copy(Lists.MusicNames)));
        }
        public static void ResetListCollections()
        {
            foreach (EList elist in ELists)
                TransferListCollection(elist, elist.Name);
        }
        public static void RefreshListCollections()
        {
            foreach (EList elist in project.ELists)
                TransferListCollection(elist, elist.Name);
        }
        private static void TransferListCollection(EList elist, string name)
        {
            switch (name)
            {
                case "Battlefields": elist.Labels.CopyTo(Lists.BattlefieldNames, 0); break;
                case "Levels": elist.Labels.CopyTo(Lists.LocationNames, 0); break;
                case "Songs": elist.Labels.CopyTo(Lists.MusicNames, 0); break;
            }
        }
        public static bool CheckLoadedProject()
        {
            if (project == null)
            {
                if (MessageBox.Show("No project file has been loaded. Would you like to load a project file?",
                    "ZONE DOCTOR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Project temp = program.Project;
                    if (temp == null)
                        temp = new Project();
                    if (project == null)
                        temp.LoadProject();
                }
                if (project == null)
                {
                    MessageBox.Show("A project file must be loaded to edit labels or keystrokes.",
                        "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return false;
                }
            }
            return true;
        }
        #endregion
        #endregion
        #region LJ 2011-12-28: interoperability fix for FF3usME
        public readonly static int OFFS_MAP_GFXPIX_PTR = 0x2EB200;      // offset for headerless ROM
        public readonly static int OFFS_WOB_MAP_DT_TL = 0x2ED434;		// at 0x2ED634 we have 15643 bytes for data, at 0x2F134F we have 8449 bytes for tiles, end at 0x2F3450
        public readonly static int OFFS_WOR_MAP_DT_TL = 0x2F4A46;		// at 0x2F4C46 we have 12993 bytes for data, at 0x2F7F07 we have 8208 bytes for tiles, end at 0x2F9F17
        public readonly static int OFFS_ST_MAP_DT_TL = 0x2F9E17;		// at 0x2F9F17 we have 6426 bytes for data, at 0x2FB831 we have 4083 bytes for tiles, end at 0x2FC824
        public readonly static int OFFS_WOB_WOR_MMAP = 0x2FE49B;		// at 0x2FE69B we have 1048 bytes for WoB mini-map, at 0x2FEAB3 we have 1139 bytes for WoR mini-map, end at 2FEF26
        public readonly static int OFFS_WOB_MMAP = 0x2FE49B;		    // at 0x2FE69B we have 1048 bytes for WoB mini-map end at 0x2FEAB3
        public readonly static int OFFS_WOR_MMAP = 0x2FE8B3;		    // at 0x2FEAB3 we have 1139 bytes for WoR mini-map, end at 2FEF26

        public readonly static int LEN_MAP_GFXPIX_PTR = 0x60;			// byte wize, 3 * NB_VAR_COMP_PTR
        public readonly static int LEN_WOB_MAP_DT_TL = (15643 + 8449);	// at 0x2ED634 we have 15643 bytes for data, at 0x2F134F we have 8449 bytes for tiles
        public readonly static int LEN_WOR_MAP_DT_TL = (8208 + 12993);	// at 0x2F7F07 we have 8208 bytes for tiles, at 0x2F4C46 we have 12993 bytes for data
        public readonly static int LEN_ST_MAP_DT_TL = (6426 + 4083);	// at 0x2F9F17 we have 6426 bytes for data, at 0x2FB831 we have 4083 bytes for tiles
        public readonly static int LEN_WOB_WOR_MMAP = (1048 + 1139);   // at 0x2FE69B we have 1048 bytes for WoB mini-map, at 0x2FEAB3 we have 1139 bytes for WoR mini-map
        public const int LEN_MAX_MMAP_DATA = 0x800;			    // decompressed mini maps are 0x800 bytes long
        public const int NB_VAR_COMP_PTR = 0x20;			    // 0x20 times 24-bit pointers to compressed stuff like mini-maps, maps, tiles...
        public readonly static int NB_MMAPS = 2;				        // nb. mini-maps
        public readonly static int NB_REGMAPS = 2;				        // nb. regular maps
        public readonly static int NB_MAP_TILE_BANKS = 2;				// nb. maps tile banks
        public readonly static int IDX_VARP_TILE_ASHP1 = 0x04;			// Various abs. pointer to compressed data: tiles for Black Jack
        public readonly static int IDX_VARP_MAP_WOB = 0x05;			// Various abs. pointer to compressed data: WoB map data
        public readonly static int IDX_VARP_TILE_WOB = 0x06;			// Various abs. pointer to compressed data: WoB tile data
        public readonly static int IDX_VARP_TILE_WOR = 0x0A;			// Various abs. pointer to compressed data: WoR tile data
        public readonly static int IDX_VARP_MAP_WOR = 0x0B;			// Various abs. pointer to compressed data: WoR map data
        public readonly static int IDX_VARP_MAP_WOR2 = 0x0C;			// Various abs. pointer to compressed data: WoR map data (duplicate)
        public readonly static int IDX_VARP_MAP_ANIM = 0x17;			// Various abs. pointer to compressed data: tiles for map animations (Figaro castle, Terra flying, sand dust, Narshe boat, ...)
        public readonly static int IDX_VARP_TILE_CHOC = 0x18;			// Various abs. pointer to compressed data: tiles for Chocobo
        public readonly static int IDX_VARP_MMAP_WOB = 0x19;			// Various abs. pointer to compressed data: WoB mini-map data
        public readonly static int IDX_VARP_MMAP_WOR = 0x1A;			// Various abs. pointer to compressed data: WoR mini-map data
        public readonly static int IDX_VARP_TILE_ASHP2 = 0x1B;			// Various abs. pointer to compressed data: tiles for Falcon
        public static ulong[] m_varCompDataAbsPtrs = new ulong[NB_VAR_COMP_PTR];	// various absolute pointer redirection to compressed stuff like mini-maps, maps, tiles...
        private static byte[] wobMiniMap;
        public static byte[] WOBMiniMap
        {
            get
            {
                if (wobMiniMap == null)
                {
                    wobMiniMap = new byte[LEN_MAX_MMAP_DATA];
                    wobMiniMap = Comp.Decompress(rom, (int)Model.HiROMToSMC(Model.m_varCompDataAbsPtrs[Model.IDX_VARP_MMAP_WOB]), 0x800);
                }
                return wobMiniMap;
            }
            set { wobMiniMap = value; }
        }
        private static byte[] worMiniMap;
        public static byte[] WORMiniMap
        {
            get
            {
                if (worMiniMap == null)
                {
                    worMiniMap = new byte[LEN_MAX_MMAP_DATA];
                    worMiniMap = Comp.Decompress(rom, (int)Model.HiROMToSMC(Model.m_varCompDataAbsPtrs[Model.IDX_VARP_MMAP_WOR]), 0x800);
                }
                return worMiniMap;
            }
            set { worMiniMap = value; }
        }

        // LJ 2011-12-28: atm, all things dialog are not (and should not) be handled with FF3LE, consequence of this will be to put the expanded data in bank $F1, not following the expanded dialog in $F0 like FF3usME and FF3Ed
        public static int m_savedExpandedBytes = 0;	                // running counter for all the things that couldn't fit in original bound (dialog, maps, ...) (useless for FF3LE)
        // public readonly static int OFFS_FF3ED_DTE_D_EX = 0x300000;		// FF3Edit exclusive 3rd Town Dialog page data location
        public readonly static int OFFS_FF3ED_DTE_D_EX = 0x310000;		// this is bank $F1
        public static void LoadVarCompDataAbsPtrs()
        {
            byte[] lGfxPtr24bits = new byte[LEN_MAP_GFXPIX_PTR];

            lGfxPtr24bits = Bits.GetBytes(rom, OFFS_MAP_GFXPIX_PTR, LEN_MAP_GFXPIX_PTR);

            genFnts_24bitsToDword(lGfxPtr24bits, m_varCompDataAbsPtrs, NB_VAR_COMP_PTR);
        }
        public static void SaveVarCompDataAbsPtrs()
        {
            byte[] lGfxPtr24bits = new byte[LEN_MAP_GFXPIX_PTR];

            genFnts_dwordTo24bits(m_varCompDataAbsPtrs, lGfxPtr24bits, NB_VAR_COMP_PTR);

            Bits.SetBytes(rom, OFFS_MAP_GFXPIX_PTR, lGfxPtr24bits);
        }
        public static ulong HiROMToSMC(ulong iRomAddr)
        {
            return (iRomAddr - 0xC00000);
        }
        public static ulong SMCToHiROM(ulong iSmcAddrHeaderless)
        {
            return (iSmcAddrHeaderless + 0xC00000);
        }
        public static void genFnts_24bitsToDword(byte[] i24bitsArray, ulong[] iDwordArray, int iNb)
        {
            int lCnt;
            int lByteArrayCnt = 0;

            for (lCnt = 0; lCnt < iNb; lCnt++, lByteArrayCnt += 3)
            {
                iDwordArray[lCnt] = (ulong)i24bitsArray[lByteArrayCnt + 2] << 16;
                iDwordArray[lCnt] |= (ulong)i24bitsArray[lByteArrayCnt + 1] << 8;
                iDwordArray[lCnt] |= (ulong)i24bitsArray[lByteArrayCnt];
            }
        }
        public static void genFnts_dwordTo24bits(ulong[] iDwordArray, byte[] i24bitsArray, int iNb)
        {
            int lCnt;
            int lByteArrayCnt = 0;

            for (lCnt = 0; lCnt < iNb; lCnt++, lByteArrayCnt += 3)
            {
                i24bitsArray[lByteArrayCnt + 2] = (byte)((iDwordArray[lCnt] >> 16) & 0xFF);
                i24bitsArray[lByteArrayCnt + 1] = (byte)((iDwordArray[lCnt] >> 8) & 0xFF);
                i24bitsArray[lByteArrayCnt] = (byte)(iDwordArray[lCnt] & 0xFF);
            }
        }
        #endregion

        private static int DatabankOff = 0;
        private static int TilemapBankOff = 0;
        private static int TilemapsSize = 0;
        public static void ExpandMaps(string strdataBank, string strtilemapBank, int numBanks)
        {
            string message = String.Empty;
            bool error = false;
            byte dataBank = 0;
            byte tilemapBank = 0;

            if (strdataBank.Equals(String.Empty))
            {
                message = "Please enter a valid bank for data expansion.";
                error = true;
            }

            if (strtilemapBank.Equals(String.Empty) && !error)
            {
                message = "Please enter a valid starting bank for tilemaps expansion.";
                error = true;
            }

            if (!error)
            {
                if (!Byte.TryParse(strdataBank, out dataBank))
                {
                    message = "Bank number must be between $00 to $6F or from $C0 to $FF.";
                    error = true;
                }

                if (!Byte.TryParse(strtilemapBank, out tilemapBank) && !error)
                {
                    message = "Bank number must be between $00 to $" + (0x70 - numBanks).ToString("X2") +
                              " or from $C0 to $" + (0xFF - numBanks - 1).ToString("X2") + ".";
                    error = true;
                }
            }

            if (!error)
            {
                if (!Bits.IsValidBank(dataBank))
                {
                    message = "Bank number must be between $00 to $6F or from $C0 to $FF.";
                    error = true;
                }
                else if (!Bits.IsValidBank(tilemapBank))
                {
                    message = "Bank number must be between $00 to $" + (0x70 - numBanks).ToString("X2") +
                              " or from $C0 to $" + (0xFF - numBanks - 1).ToString("X2") + ".";
                    error = true;
                }
            }

            if (!error)
            {
                DatabankOff = Bits.ToHiROM(dataBank << 16);
                TilemapBankOff = Bits.ToHiROM(tilemapBank << 16);
                TilemapsSize = numBanks << 16;

                DialogResult dialog =
                    MessageBox.Show("You want to move / expand data from $" + DatabankOff.ToString("X6") + " to $" +
                                    (DatabankOff + 0xFFFF).ToString("X6") +
                                    " and move / expand tilemaps data from $" + TilemapBankOff.ToString("X6") + "to $" +
                                    (TilemapBankOff + TilemapsSize - 1).ToString("X6"), "ZONE DOCTOR", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                if (dialog == DialogResult.Yes)
                {
                    List<int[]> faults = new List<int[]>();
                    validateROM(ref faults);

                    if (faults.Count == 0)
                    {
                        expandROM();
                    }
                    else
                    {
                        message = "You have the following error(s) in your ROM:\n\n";

                        for (int i = 0; i < faults.Count && i < 8; i++)
                        {
                            message += "\n" + (i + 1) + "- At offset $" + faults[i][2].ToString("X6") + ", value of $" +
                                       faults[i][1].ToString("X6") + " found instead of expected $" + faults[i][0].ToString("X6") + "\n";
                        }

                        if (faults.Count > 8)
                        {
                            message += "\nDisplaying only the first 8 errors. You have more.\n";
                        }

                        dialog = MessageBox.Show(message + "\nThere will be problems with the ROM. Continue anyway?", "ZONE DOCTOR", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Exclamation);

                        if (dialog == DialogResult.Yes)
                        {
                            expandROM();
                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("Error! " + message, "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void expandROM()
        {
            // Get original data
            byte[] EventPtrs = Bits.GetBytes(rom, Expansion.BASE_EVENT_PTR, Expansion.SIZE_EVENT_PTR);
            byte[] EventData = Bits.GetBytes(rom, Expansion.BASE_EVENT, Expansion.SIZE_EVENT_DATA);
            byte[] NpcPtrs = Bits.GetBytes(rom, Expansion.BASE_NPC_PTR, Expansion.SIZE_NPC_PTR);
            byte[] NpcData = Bits.GetBytes(rom, Expansion.BASE_NPC, Expansion.SIZE_NPC_DATA);
            byte[] ShortExitPtrs = Bits.GetBytes(rom, Expansion.BASE_SHORT_EXIT_PTR, Expansion.SIZE_SHORT_EXIT_PTR);
            byte[] ShortExitData = Bits.GetBytes(rom, Expansion.BASE_SHORT_EXIT, Expansion.SIZE_SHORT_EXIT_DATA);
            byte[] LongExitPtrs = Bits.GetBytes(rom, Expansion.BASE_LONG_EXIT_PTR, Expansion.SIZE_SHORT_EXIT_PTR);
            byte[] LongExitData = Bits.GetBytes(rom, Expansion.BASE_LONG_EXIT, Expansion.SIZE_LONG_EXIT_DATA);
            byte[] ChestPtrs = Bits.GetBytes(rom, Expansion.BASE_CHEST_PTR, Expansion.SIZE_CHEST_PTR);
            byte[] ChestData = Bits.GetBytes(rom, Expansion.BASE_CHEST, Expansion.SIZE_CHEST_DATA);
            byte[] TilemapPtrs = Bits.GetBytes(rom, Expansion.BASE_TILEMAP_PTR, Expansion.SIZE_TILEMAP_PTR);
            byte[] TilemapData = Bits.GetBytes(rom, Expansion.BASE_TILEMAP, Expansion.SIZE_TILEMAP_DATA);
            byte[] LocationData = Bits.GetBytes(rom, Expansion.BASE_LOCATION, Expansion.SIZE_LOCATION);

            // Erase original data in ROM
            Bits.Fill(rom, Expansion.FILLER, Expansion.BASE_EVENT_PTR, Expansion.SIZE_EVENT_PTR + Expansion.SIZE_EVENT_DATA);
            Bits.Fill(rom, Expansion.FILLER, Expansion.BASE_NPC_PTR, Expansion.SIZE_NPC_PTR + Expansion.SIZE_NPC_DATA);
            Bits.Fill(rom, Expansion.FILLER, Expansion.BASE_SHORT_EXIT_PTR, Expansion.SIZE_SHORT_EXIT_PTR + Expansion.SIZE_SHORT_EXIT_DATA);
            Bits.Fill(rom, Expansion.FILLER, Expansion.BASE_LONG_EXIT_PTR, Expansion.SIZE_LONG_EXIT_PTR + Expansion.SIZE_LONG_EXIT_DATA);
            Bits.Fill(rom, Expansion.FILLER, Expansion.BASE_CHEST_PTR, Expansion.SIZE_CHEST_PTR + Expansion.SIZE_CHEST_DATA);
            Bits.Fill(rom, Expansion.FILLER, Expansion.BASE_TILEMAP_PTR, Expansion.SIZE_TILEMAP_PTR + Expansion.SIZE_TILEMAP_DATA + 3);
            Bits.Fill(rom, Expansion.FILLER, Expansion.BASE_LOCATION, Expansion.SIZE_LOCATION);

            // Pointer incremention value (for 2 bytes pointers)
            ushort inctPtr = (ushort)(Expansion.NEW_ENTRIES * 2);

            // Increment all pointers (except chests and tilemaps)
            Bits.IncShort(EventPtrs, inctPtr);
            Bits.IncShort(NpcPtrs, inctPtr);
            Bits.IncShort(ShortExitPtrs, inctPtr);
            Bits.IncShort(LongExitPtrs, inctPtr);

            // Get highests / last pointers
            ushort LastEventPtr = (ushort)Bits.findArrayMax(EventPtrs, 2);
            ushort LastNpcPtr = (ushort)Bits.findArrayMax(NpcPtrs, 2);
            ushort LastShortExitPtr = (ushort)Bits.findArrayMax(ShortExitPtrs, 2);
            ushort LastLongExitPtr = (ushort)Bits.findArrayMax(LongExitPtrs, 2);
            ushort LastChestPtr = (ushort)Bits.findArrayMax(ChestPtrs, 2);
            int LastTilemapPtr = Bits.findArrayMax(TilemapPtrs, 3);

            // Create expansion pointers arrays
            byte[] ExpEventPtrs = new byte[inctPtr];
            byte[] ExpNpcPtrs = new byte[inctPtr];
            byte[] ExpShortExitPtrs = new byte[inctPtr];
            byte[] ExpLongExitPtrs = new byte[inctPtr];
            byte[] ExpChestPtrs = new byte[inctPtr];
            byte[] ExpTilemapPtrs = new byte[Expansion.NEW_ENTRIES * 3];

            // Fill each array with highest pointer
            Bits.FillShort(ExpEventPtrs, LastEventPtr);
            Bits.FillShort(ExpNpcPtrs, LastNpcPtr);
            Bits.FillShort(ExpShortExitPtrs, LastShortExitPtr);
            Bits.FillShort(ExpLongExitPtrs, LastLongExitPtr);
            Bits.FillShort(ExpChestPtrs, LastChestPtr);

            // Since we're going to copy default tilemap numerous times, increase each pointer by its length
            for (int i = 0; i < ExpTilemapPtrs.Length; i += 3)
            {
                LastTilemapPtr += Expansion.DEFAULT_TILEMAP.Length;

                ExpTilemapPtrs[i] = (byte)(LastTilemapPtr & 0xFF);
                ExpTilemapPtrs[i + 1] = (byte)((LastTilemapPtr >> 8) & 0xFF);
                ExpTilemapPtrs[i + 2] = (byte)((LastTilemapPtr >> 16) & 0xFF);
            }

            // put back the banks offsets to absolute value because we are going to write to the ROM
            DatabankOff = Bits.ToAbs(DatabankOff);
            TilemapBankOff = Bits.ToAbs(TilemapBankOff);

            // Inset events, npcs, exits, chests ptrs and data
            Bits.setData(rom, Expansion.BASE_EVENT_PTR, EventPtrs, ExpEventPtrs, EventData);
            Bits.setData(rom, DatabankOff, NpcPtrs, ExpNpcPtrs, NpcData);
            Bits.setData(rom, DatabankOff + Expansion.NEW_SHORT_EXIT, ShortExitPtrs, ExpShortExitPtrs, ShortExitData);
            Bits.setData(rom, DatabankOff + Expansion.NEW_LONG_EXIT, LongExitPtrs, ExpLongExitPtrs, LongExitData);
            Bits.setData(rom, DatabankOff + Expansion.NEW_CHEST, ChestPtrs, ExpChestPtrs, ChestData);
            
            /*int offset = Expansion.BASE_EVENT_PTR;
            Bits.SetBytes(rom, offset, EventPtrs);
            offset += Expansion.SIZE_EVENT_PTR;
            Bits.SetBytes(rom, offset, ExpEventPtrs);
            offset += inctPtr;
            Bits.SetBytes(rom, offset, EventData);
            
            offset = DatabankOff;
            Bits.SetBytes(rom, offset, NpcPtrs);
            offset += Expansion.SIZE_NPC_PTR;
            Bits.SetBytes(rom, offset, ExpNpcPtrs);
            offset += inctPtr;
            Bits.SetBytes(rom, offset, NpcData);
            
            offset = DatabankOff + Expansion.NEW_SHORT_EXIT;
            Bits.SetBytes(rom, offset, ShortExitPtrs);
            offset += Expansion.SIZE_SHORT_EXIT_PTR;
            Bits.SetBytes(rom, offset, ExpShortExitPtrs);
            offset += inctPtr;
            Bits.SetBytes(rom, offset, ShortExitData);
            
            offset = DatabankOff + Expansion.NEW_LONG_EXIT;
            Bits.SetBytes(rom, offset, LongExitPtrs);
            offset += Expansion.SIZE_LONG_EXIT_PTR;
            Bits.SetBytes(rom, offset, ExpLongExitPtrs);
            offset += inctPtr;
            Bits.SetBytes(rom, offset, LongExitData);
            
            offset = DatabankOff + Expansion.NEW_CHEST;
            Bits.SetBytes(rom, offset, ChestPtrs);
            offset += Expansion.SIZE_CHEST_PTR;
            Bits.SetBytes(rom, offset, ExpChestPtrs);
            offset += inctPtr;
            Bits.SetBytes(rom, offset, ChestData);*/

            // Insert locations data
            int offset = Expansion.NEW_LOCATION;
            Bits.SetBytes(rom, offset, LocationData);
            offset += Expansion.SIZE_LOCATION;

            for (int i = 0; i < Expansion.NEW_ENTRIES; i++)
            {
                Bits.SetBytes(rom, offset, Expansion.DEFAULT_LOCATION);
                offset += Expansion.DEFAULT_LOCATION.Length;
            }

            // Get last tilemap pointer
            LastTilemapPtr = Bits.findArrayMax(TilemapPtrs, 3);

            // Insert Tilemaps ptrs and data
            offset = Expansion.BASE_TILEMAP_PTR;
            Bits.SetBytes(rom, offset, TilemapPtrs);
            offset += Expansion.SIZE_TILEMAP_PTR;
            Bits.SetBytes(rom, offset, ExpTilemapPtrs);
            offset = TilemapBankOff;
            Bits.SetBytes(rom, offset, TilemapData);
            offset = TilemapBankOff + LastTilemapPtr;

            for (int i = 0; i < Expansion.NEW_ENTRIES; i++)
            {
                Bits.SetBytes(rom, offset, Expansion.DEFAULT_TILEMAP);
                offset += Expansion.DEFAULT_TILEMAP.Length;
            }

            // ASM code changes (LDAs and ADCs)

        }

        private static void validateROM(ref List<int[]> faults)
        {
            for (int i = 0; i < Expansion.ROM_EVENT.Length; i++)
            {
                Bits.IsMatchingOffset(rom, Expansion.BASE_EVENT + Expansion.ROM_EVENT_VAR[i],
                    Expansion.ROM_EVENT[i], ref faults);
            }

            for (int i = 0; i < Expansion.ROM_CHEST.Length; i++)
            {
                Bits.IsMatchingOffset(rom, Expansion.BASE_CHEST + Expansion.ROM_CHEST_VAR[i],
                    Expansion.ROM_CHEST[i], ref faults);
            }

            for (int i = 0; i < Expansion.ROM_NPC.Length; i++)
            {
                Bits.IsMatchingOffset(rom, Expansion.BASE_NPC + Expansion.ROM_NPC_VAR[i],
                    Expansion.ROM_NPC[i], ref faults);
            }

            for (int i = 0; i < Expansion.ROM_LONG_EXIT.Length; i++)
            {
                Bits.IsMatchingOffset(rom, Expansion.BASE_LONG_EXIT + Expansion.ROM_LONG_EXIT_VAR[i],
                    Expansion.ROM_LONG_EXIT[i], ref faults);
            }

            for (int i = 0; i < Expansion.ROM_SHORT_EXIT.Length; i++)
            {
                Bits.IsMatchingOffset(rom, Expansion.BASE_SHORT_EXIT + Expansion.ROM_SHORT_EXIT_VAR[i],
                    Expansion.ROM_SHORT_EXIT[i], ref faults);
            }

            for (int i = 0; i < Expansion.ROM_TILEMAP_INT.Length; i++)
            {
                Bits.IsMatchingOffset(rom, Expansion.BASE_TILEMAP + Expansion.ROM_TILEMAP_INT_VAR[i],
                    Expansion.ROM_TILEMAP_INT[i], ref faults);
            }

            for (int i = 0; i < Expansion.ROM_TILEMAP_SHORT.Length; i++)
            {
                Bits.IsMatchingOffset(rom, Expansion.ROM_TILEMAP_SHORT_VAL[i], Expansion.ROM_TILEMAP_SHORT[i], ref faults);
            }

            for (int i = 0; i < Expansion.ROM_TILEMAP_BYTE.Length; i++)
            {
                Bits.IsMatchingOffset(rom, Expansion.ROM_TILEMAP_BYTE_VAL[i], Expansion.ROM_TILEMAP_BYTE[i], ref faults);
            }

            int locationROM = Bits.GetInt24(rom, Expansion.ROM_LOCATION);

            if (locationROM != Expansion.BASE_LOCATION)
            {
                faults.Add(new[] { Expansion.BASE_LOCATION, locationROM, Expansion.ROM_LOCATION });
            }
        }
    }
}
