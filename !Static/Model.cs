using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms; // remove later
using System.IO;
using System.Security.Cryptography;
using ZONEDOCTOR.ScriptsEditor;
using ZONEDOCTOR.ScriptsEditor.Commands;
using ZONEDOCTOR.Properties;
using ZONEDOCTOR._Static;
using System.Xml.Linq;
using System.Collections.Specialized;
using System.Linq;

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
                    locationNames = Parsing.GetLocationNames();
                
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
        private static byte[][] tilemaps = new byte[NUM_TILEMAPS][];
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
                {
                    Decompress(tilemaps, Model.BASE_TILEMAP_PTR, Model.BASE_TILEMAP, "TILE MAP", 0x4000, 3, 0, tilemaps.Length, TilemapSizes);
                }
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
        public static bool[] EditTilemaps = new bool[NUM_TILEMAPS]; 
        public static ushort[] TilemapSizes = new ushort[NUM_TILEMAPS]; 
        public static bool[] EditSoliditySets = new bool[43];
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
                    locations = new Location[NUM_LOCATIONS];

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
                            MessageBox.Show("Could not create backup ROM.\n\nThe backup ROM directory has been moved, renamed, or no longer exists.", Model.APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                RemoveHeader();
                hexEditor = new HexEditor(rom, Bits.Copy(rom));
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Zone Doctor was unable to load the rom.\n\n" + e.Message,
                    Model.APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Zone Doctor was unable to write to the file.\n\n" + ex.Message, Model.APPNAME);
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

                //madsiur, CE Edition
                if (i == arrays.Length - 1)
                {
                    // madsiur: if we are compressing tilemaps
                    if (tileMaps && Model.IsExpanded)
                    {
                        original[i] = Expansion.DEFAULT_TILEMAP;
                    }
                    else
                    {
                        original[i] = Bits.GetBytes(rom, offset, offsetEnd - offset);
                    }
                }
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
                        Model.APPNAME);
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
            Model.LoadVarCompDataAbsPtrs();
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

        //madsiur, CE Edition
        public static void CreateListCollections()
        {
            ELists = new List<EList>();
            ELists.Add(new EList("Locations", Lists.Copy(LevelNames)));
            ELists.Add(new EList("Songs", Lists.Copy(Lists.MusicNames)));
        }
        // madsiur, CE Edition
        public static void UpdateElistLocation()
        {
            ELists.RemoveAll(x => x.Name.Equals("Locations"));
            ELists.Add(new EList("Locations", Lists.Copy(LevelNames)));
            Project.ReplaceEList(ELists.Find(x => x.Name.Equals("Locations")));
        }

        public static void UpdateElistLocationEntry(int ind, string text)
        {
            int index = ELists.FindIndex(x => x.Name.Equals("Locations"));
            ELists[index].Labels[ind] = text;
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
                case "Levels": elist.Labels.CopyTo(LevelNames, 0); break;
                case "Songs": elist.Labels.CopyTo(Lists.MusicNames, 0); break;
            }
        }
        public static bool CheckLoadedProject()
        {
            if (project == null)
            {
                if (MessageBox.Show("No project file has been loaded. Would you like to load a project file?",
                    Model.APPNAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                        Model.APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return false;
                }
            }
            return true;
        }
        #endregion
        #endregion

        #region CE expansion variables

        // App Name
        public const string APPNAME = "Zone Doctor CE";

        // CE Expansions
        public static bool IsExpanded;
        public static bool IsChestsExpanded;

        // Settings file
        public static XDocument SettingsFile;

        // Map names
        public static string[] LevelNames;

        // Number of entries
        public static int NUM_LOCATIONS;
        public static int NUM_TILEMAPS;
        public static int NUM_LOC_NAMES;

        // Size of data
        public static int SIZE_EVENT_PTR;
        public static int SIZE_EVENT_DATA;
        public static int SIZE_NPC_PTR;
        public static int SIZE_NPC_DATA;
        public static int SIZE_SHORT_EXIT_PTR;
        public static int SIZE_SHORT_EXIT_DATA;
        public static int SIZE_LONG_EXIT_PTR;
        public static int SIZE_LONG_EXIT_DATA;
        public static int SIZE_CHEST_PTR;
        public static int SIZE_CHEST_DATA;
        public static int SIZE_TILEMAP_PTR;
        public static int SIZE_TILEMAP_DATA;
        public static int SIZE_LOCATION;
        public static int SIZE_LOC_NAMES;
        public static int SIZE_LOC_NAMES_PTR;

        // ROM Offsets
        public static int BASE_EVENT_PTR;
        public static int BASE_EVENT;
        public static int BASE_NPC_PTR;
        public static int BASE_NPC;
        public static int BASE_SHORT_EXIT_PTR;
        public static int BASE_SHORT_EXIT;
        public static int BASE_LONG_EXIT_PTR;
        public static int BASE_LONG_EXIT;
        public static int BASE_CHEST_PTR;
        public static int BASE_CHEST;
        public static int BASE_TILEMAP_PTR;
        public static int BASE_TILEMAP;
        public static int BASE_LOCATION;
        public static int BASE_LOC_NAMES_PTR;
        public static int BASE_LOC_NAMES;

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

        #region CE Functions

        /// <summary>
        /// //madsiur, CE Edition
        /// Init sizes, number of entries and offset by fetching from the ROM.
        /// </summary>
        /// <param name="isExpanded">True if ROM has expansion</param>
        public static void InitExpansionFields(bool isExpanded)
        {
            //if ROM is expanded
            if (isExpanded)
            {
                SIZE_EVENT_PTR = 0x402;
                SIZE_EVENT_DATA = 0x3000;
                SIZE_NPC_PTR = 0x402;
                SIZE_NPC_DATA = 0x8BFF;
                SIZE_SHORT_EXIT_PTR = 0x402;
                SIZE_SHORT_EXIT_DATA = 0x3BFF;
                SIZE_LONG_EXIT_PTR = 0x402;
                SIZE_LONG_EXIT_DATA = 0xBFF;
                SIZE_CHEST_PTR = 0x400;
                SIZE_CHEST_DATA = 0x1000;
                SIZE_TILEMAP_PTR = 0x5FD;

                SIZE_LOCATION = 0x4200;
                SIZE_LOC_NAMES = 0x2500;

                NUM_TILEMAPS = 511;
                NUM_LOCATIONS = 511;
                NUM_LOC_NAMES = 256;
            }
            else
            {
                SIZE_EVENT_PTR = 0x342;
                SIZE_EVENT_DATA = 0x16CE;
                SIZE_NPC_PTR = 0x342;
                SIZE_NPC_DATA = 0x4D6E;
                SIZE_SHORT_EXIT_PTR = 0x402;
                SIZE_SHORT_EXIT_DATA = 0x1AFE;
                SIZE_LONG_EXIT_PTR = 0x402;
                SIZE_LONG_EXIT_DATA = 0x57E;
                SIZE_CHEST_PTR = 0x340;
                SIZE_CHEST_DATA = 0x827;
                SIZE_TILEMAP_PTR = 0x41D;
                SIZE_TILEMAP_DATA = 0x42E50;
                SIZE_LOCATION = 0x3580;
                SIZE_LOC_NAMES = 0x500;

                NUM_TILEMAPS = 351;
                NUM_LOCATIONS = 415;
                NUM_LOC_NAMES = 73;
            }

            // fetch offsets from the ROM
            BASE_EVENT_PTR = Bits.ToAbs(Bits.GetInt24(rom, 0x00BCB5));
            BASE_EVENT = Bits.ToAbs(BASE_EVENT_PTR + SIZE_EVENT_PTR);
            BASE_NPC_PTR = Bits.ToAbs(Bits.GetInt24(rom, 0x0052C3));
            BASE_NPC = Bits.ToAbs(BASE_NPC_PTR + SIZE_NPC_PTR);
            BASE_SHORT_EXIT_PTR = Bits.ToAbs(Bits.GetInt24(rom, 0x001A84));
            BASE_SHORT_EXIT = Bits.ToAbs(BASE_SHORT_EXIT_PTR + SIZE_SHORT_EXIT_PTR);
            BASE_LONG_EXIT_PTR = Bits.ToAbs(Bits.GetInt24(rom, 0x0018F1));
            BASE_LONG_EXIT = Bits.ToAbs(BASE_LONG_EXIT_PTR + SIZE_LONG_EXIT_PTR);
            BASE_CHEST_PTR = Bits.ToAbs(Bits.GetInt24(rom, 0x004BE1));
            BASE_CHEST = Bits.ToAbs(BASE_CHEST_PTR + SIZE_CHEST_PTR);
            BASE_TILEMAP_PTR = Bits.ToAbs(Bits.GetInt24(rom, 0x002893));
            BASE_TILEMAP = Bits.ToAbs((rom[0x0028A4] << 16) + Bits.GetShort(rom, 0x002898));
            BASE_LOCATION = Bits.ToAbs(Bits.GetInt24(rom, 0x001CC0));
            BASE_LOC_NAMES_PTR = Bits.ToAbs(Bits.GetInt24(rom, 0x008009));
            BASE_LOC_NAMES = Bits.ToAbs((rom[0x007FFE] << 16) + Bits.GetShort(rom, 0x00800D));

            tilemaps = new byte[NUM_TILEMAPS][];
            EditTilemaps = new bool[NUM_TILEMAPS];
            TilemapSizes = new ushort[NUM_TILEMAPS];
        }

        public static bool ExpandROM(int romOffset, int tilemapOffset, int tilemapSize, bool isZplus)
        {
            try
            {
                // if user was using FF6LE+ or ZD+ before
                if (isZplus)
                {
                    SIZE_EVENT_DATA = 0x1C46;
                    SIZE_SHORT_EXIT_DATA = 0x2132;
                    SIZE_NPC_DATA = 0x5606;
                    SIZE_TILEMAP_DATA = 0x060000 > tilemapSize ? 0x60000 : tilemapSize; // Approximate but it is trimmed later on.
                }
                
                // Get original rom
                byte[] EventPtrs = Bits.GetBytes(rom, BASE_EVENT_PTR, SIZE_EVENT_PTR);
                byte[] Eventrom = Bits.GetBytes(rom, BASE_EVENT, SIZE_EVENT_DATA);
                byte[] NpcPtrs = Bits.GetBytes(rom, BASE_NPC_PTR, SIZE_NPC_PTR);
                byte[] Npcrom = Bits.GetBytes(rom, BASE_NPC, SIZE_NPC_DATA);
                byte[] ShortExitPtrs = Bits.GetBytes(rom, BASE_SHORT_EXIT_PTR, SIZE_SHORT_EXIT_PTR);
                byte[] ShortExitrom = Bits.GetBytes(rom, BASE_SHORT_EXIT, SIZE_SHORT_EXIT_DATA);
                byte[] LongExitPtrs = Bits.GetBytes(rom, BASE_LONG_EXIT_PTR, SIZE_SHORT_EXIT_PTR);
                byte[] LongExitrom = Bits.GetBytes(rom, BASE_LONG_EXIT, SIZE_LONG_EXIT_DATA);
                byte[] ChestPtrs = Bits.GetBytes(rom, BASE_CHEST_PTR, SIZE_CHEST_PTR);
                byte[] Chestrom = Bits.GetBytes(rom, BASE_CHEST, SIZE_CHEST_DATA);
                byte[] TilemapPtrs = Bits.GetBytes(rom, BASE_TILEMAP_PTR, SIZE_TILEMAP_PTR);
                byte[] Tilemaprom = Bits.GetBytes(rom, BASE_TILEMAP, SIZE_TILEMAP_DATA);
                byte[] Locationrom = Bits.GetBytes(rom, BASE_LOCATION, SIZE_LOCATION);
                byte[] LocNamesPtr = Bits.GetBytes(rom, BASE_LOC_NAMES_PTR, NUM_LOC_NAMES * 2);
                byte[] LocNames = Bits.GetBytes(rom, BASE_LOC_NAMES, SIZE_LOC_NAMES);
                
                // Erase original rom in ROM (except location names)
                Bits.Fill(rom, Expansion.FILLER, BASE_EVENT_PTR, SIZE_EVENT_PTR + SIZE_EVENT_DATA);
                Bits.Fill(rom, Expansion.FILLER, BASE_NPC_PTR, SIZE_NPC_PTR + SIZE_NPC_DATA);
                Bits.Fill(rom, Expansion.FILLER, BASE_SHORT_EXIT_PTR, SIZE_SHORT_EXIT_PTR + SIZE_SHORT_EXIT_DATA);
                Bits.Fill(rom, Expansion.FILLER, BASE_LONG_EXIT_PTR, SIZE_LONG_EXIT_PTR + SIZE_LONG_EXIT_DATA);
                Bits.Fill(rom, Expansion.FILLER, BASE_CHEST_PTR, SIZE_CHEST_PTR + SIZE_CHEST_DATA);
                Bits.Fill(rom, Expansion.FILLER, BASE_TILEMAP_PTR, SIZE_TILEMAP_PTR + SIZE_TILEMAP_DATA + 3);
                Bits.Fill(rom, Expansion.FILLER, BASE_LOCATION, SIZE_LOCATION);

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
                ushort LastLocNamePtr = (ushort)Bits.findArrayMax(LocNamesPtr, 2);

                // Create expansion pointers arrays
                byte[] ExpEventPtrs = new byte[inctPtr];
                byte[] ExpNpcPtrs = new byte[inctPtr];
                byte[] ExpShortExitPtrs = new byte[inctPtr];
                byte[] ExpLongExitPtrs = new byte[inctPtr];
                byte[] ExpChestPtrs = new byte[inctPtr];
                byte[] ExpTilemapPtrs = new byte[(511 - NUM_TILEMAPS) * 3];
                
                // Fill each array with highest pointer
                Bits.FillShort(ExpEventPtrs, LastEventPtr);
                Bits.FillShort(ExpNpcPtrs, LastNpcPtr);
                Bits.FillShort(ExpShortExitPtrs, LastShortExitPtr);
                Bits.FillShort(ExpLongExitPtrs, LastLongExitPtr);
                Bits.FillShort(ExpChestPtrs, LastChestPtr);

                // Last pointer points to nothing so we need to resize tilemap rom accordingly.
                Tilemaprom = Bits.GetBytes(Tilemaprom, 0, LastTilemapPtr);
                
                // Since we're going to copy default tilemap numerous times, increase each pointer by its length
                for (int i = 0; i < ExpTilemapPtrs.Length; i += 3)
                {
                    LastTilemapPtr += Expansion.DEFAULT_TILEMAP.Length;

                    ExpTilemapPtrs[i] = (byte)(LastTilemapPtr & 0xFF);
                    ExpTilemapPtrs[i + 1] = (byte)((LastTilemapPtr >> 8) & 0xFF);
                    ExpTilemapPtrs[i + 2] = (byte)((LastTilemapPtr >> 16) & 0xFF);
                }

                Bits.SetBytes(rom, Expansion.NEW_LOC_NAME, LocNamesPtr);
                Bits.SetBytes(rom, Expansion.NEW_LOC_NAME + 0x200, LocNames);

                ushort j = LastLocNamePtr;

                while (rom[j] != 0)
                    j++;

                for (int i = NUM_LOC_NAMES; i < 256; i++)
                {
                    Bits.SetShort(rom, Expansion.NEW_LOC_NAME + i * 2, j);
                    Bits.SetBytes(rom, Expansion.NEW_LOC_NAME + 0x200 + j, Expansion.DEFAULT_LOC_NAME);
                    j += (ushort)Expansion.DEFAULT_LOC_NAME.Length;
                }

                // put back the banks offsets to absolute value because we are going to write to the ROM
                romOffset = Bits.ToAbs(romOffset);
                tilemapOffset = Bits.ToAbs(tilemapOffset);

                // Inset events, npcs, exits, chests ptrs and rom
                Bits.setData(rom, BASE_EVENT_PTR, EventPtrs, ExpEventPtrs, Eventrom);
                Bits.setData(rom, romOffset, NpcPtrs, ExpNpcPtrs, Npcrom);
                Bits.setData(rom, romOffset + Expansion.NEW_SHORT_EXIT, ShortExitPtrs, ExpShortExitPtrs, ShortExitrom);
                Bits.setData(rom, romOffset + Expansion.NEW_LONG_EXIT, LongExitPtrs, ExpLongExitPtrs, LongExitrom);
                Bits.setData(rom, romOffset + Expansion.NEW_CHEST, ChestPtrs, ExpChestPtrs, Chestrom);
                
                // Insert locations rom
                int offset = Expansion.NEW_LOCATION;
                Bits.SetBytes(rom, offset, Locationrom);
                
                offset += NUM_LOCATIONS * 33;
                
                for (int i = 0; i < Expansion.NEW_ENTRIES; i++)
                {
                    Bits.SetBytes(rom, offset, Expansion.DEFAULT_LOCATION);
                    offset += Expansion.DEFAULT_LOCATION.Length;
                }
                
                // Get last tilemap pointer
                LastTilemapPtr = Bits.findArrayMax(TilemapPtrs, 3);

                // Insert Tilemaps ptrs and rom
                offset = BASE_TILEMAP_PTR;
                Bits.SetBytes(rom, offset, TilemapPtrs);
                offset += SIZE_TILEMAP_PTR;
                Bits.SetBytes(rom, offset, ExpTilemapPtrs);
                offset = tilemapOffset;
                Bits.SetBytes(rom, offset, Tilemaprom);
                offset = tilemapOffset + LastTilemapPtr;
                
                for (int i = 0; i < (511 - NUM_TILEMAPS); i++)
                {
                    Bits.SetBytes(rom, offset, Expansion.DEFAULT_TILEMAP);
                    offset += Expansion.DEFAULT_TILEMAP.Length;
                }

                // We ned the new banks to HiROM
                romOffset = Bits.ToHiROM(romOffset);

                // ASM code changes for events, NPCs, Exits and Chests (LDAs)
                Bits.setAsmArray(rom, Expansion.ROM_EVENT, Expansion.ROM_EVENT_VAR, Bits.ToHiROM(BASE_EVENT_PTR));
                Bits.setAsmArray(rom, Expansion.ROM_NPC, Expansion.ROM_NPC_VAR, romOffset);
                Bits.setAsmArray(rom, Expansion.ROM_SHORT_EXIT, Expansion.ROM_SHORT_EXIT_VAR, romOffset + Expansion.NEW_SHORT_EXIT);
                Bits.setAsmArray(rom, Expansion.ROM_LONG_EXIT, Expansion.ROM_LONG_EXIT_VAR, romOffset + Expansion.NEW_LONG_EXIT);
                Bits.setAsmArray(rom, Expansion.ROM_CHEST, Expansion.ROM_CHEST_VAR_EXP, romOffset + Expansion.NEW_CHEST);
                
                // LDA change for Locations
                Bits.SetInt24(rom, Bits.ToAbs(Expansion.ROM_LOCATION + 1), Bits.ToHiROM(Expansion.NEW_LOCATION));
                
                // ADC and LDA changes for tilemaps
                Bits.setAsmArray(rom, Expansion.ROM_TILEMAP_SHORT, (ushort)0x0000);
                Bits.setAsmArray(rom, Expansion.ROM_TILEMAP_BYTE, Bits.ToHiROM((byte)(tilemapOffset >> 16)));

                // ADC and LDA changes for loc names
                Bits.SetInt24(rom, 0x008009, Bits.ToHiROM(Expansion.NEW_LOC_NAME));
                Bits.setAsmArray(rom, Expansion.ROM_LOC_NAME_SHORT, 0x4400);
                Bits.setAsmArray(rom, Expansion.ROM_LOC_NAME_BYTE, 0xDA);

                // load expanded variables values
                InitExpansionFields(true);
                IsExpanded = true;
                LevelNames = Lists.expandedLocations;

                SIZE_TILEMAP_DATA = tilemapSize;

                locations = new Location[NUM_LOCATIONS];

                for (int i = 0; i < locations.Length; i++)
                    locations[i] = new Location(i);

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    "Expansion has failed for an unknow reason.\n\n Error: " + e.Message,
                    Model.APPNAME, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool ExpandChests()
        {
            try
            {
                Bits.setAsmArray(rom, Expansion.ROM_EXP_CHEST_SHORT_MEM, 0x1E20);
                Bits.setAsmArray(rom, Expansion.ROM_EXP_CHEST_SHORT_NUM, 0x03FF);
                Bits.SetShort(rom, 0x00BB1F, 0x0060);
                IsChestsExpanded = true;

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Unable to perform chest expansion.\n\n Error: " + e.Message, Model.APPNAME,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

        }

        public static List<int[]> ValidateChestExpansion()
        {
            List<int[]> faults = new List<int[]>();

            for (int i = 0; i < Expansion.ROM_EXP_CHEST_SHORT_MEM.Length; i++)
            {
                Bits.IsMatchingShort(rom, 0x1E40, Expansion.ROM_EXP_CHEST_SHORT_MEM[i], ref faults);
            }

            for (int i = 0; i < Expansion.ROM_EXP_CHEST_SHORT_NUM.Length; i++)
            {
                Bits.IsMatchingShort(rom, 0x01FF, Expansion.ROM_EXP_CHEST_SHORT_NUM[i], ref faults);
            }

            Bits.IsMatchingShort(rom, 0x0030, 0x00BB1F, ref faults);

            return faults;
        }
        public static List<int[]> ValidateROM(bool isZplus)
        {
            List<int[]> faults = new List<int[]>();
            if (!isZplus)
            {
                for (int i = 0; i < Expansion.ROM_EVENT.Length; i++)
                {
                    Bits.IsMatchingOffset(rom, BASE_EVENT_PTR + Expansion.ROM_EVENT_VAR[i],
                        Expansion.ROM_EVENT[i], ref faults);
                }
            }

            for (int i = 0; i < Expansion.ROM_CHEST.Length; i++)
            {
                Bits.IsMatchingOffset(rom, BASE_CHEST_PTR + Expansion.ROM_CHEST_VAR[i],
                    Expansion.ROM_CHEST[i], ref faults);
            }

            if (!isZplus)
            {
                for (int i = 0; i < Expansion.ROM_NPC.Length; i++)
                {
                    Bits.IsMatchingOffset(rom, BASE_NPC_PTR + Expansion.ROM_NPC_VAR[i],
                        Expansion.ROM_NPC[i], ref faults);
                }
            }

            if (!isZplus)
            {
                for (int i = 0; i < Expansion.ROM_LONG_EXIT.Length; i++)
                {
                    Bits.IsMatchingOffset(rom, BASE_LONG_EXIT_PTR + Expansion.ROM_LONG_EXIT_VAR[i],
                        Expansion.ROM_LONG_EXIT[i], ref faults);
                }
            }

            if (!isZplus)
            {
                for (int i = 0; i < Expansion.ROM_SHORT_EXIT.Length; i++)
                {
                    Bits.IsMatchingOffset(rom, BASE_SHORT_EXIT_PTR + Expansion.ROM_SHORT_EXIT_VAR[i],
                        Expansion.ROM_SHORT_EXIT[i], ref faults);
                }
            }

            if (!isZplus)
            {
                for (int i = 0; i < Expansion.ROM_TILEMAP_INT.Length; i++)
                {
                    Bits.IsMatchingOffset(rom, BASE_TILEMAP_PTR + Expansion.ROM_TILEMAP_INT_VAR[i],
                        Expansion.ROM_TILEMAP_INT[i], ref faults);
                }


                for (int i = 0; i < Expansion.ROM_TILEMAP_SHORT.Length; i++)
                {
                    Bits.IsMatchingShort(rom, Expansion.ROM_TILEMAP_SHORT_VAL[i], Expansion.ROM_TILEMAP_SHORT[i], ref faults);
                }

                for (int i = 0; i < Expansion.ROM_TILEMAP_BYTE.Length; i++)
                {
                    Bits.IsMatchingByte(rom, Expansion.ROM_TILEMAP_BYTE_VAL[i], Expansion.ROM_TILEMAP_BYTE[i], ref faults);
                }
            }

            for (int i = 0; i < Expansion.ROM_LOC_NAME_SHORT.Length; i++)
            {
                Bits.IsMatchingShort(rom, Expansion.ROM_LOC_NAME_SHORT_VAL[i], Expansion.ROM_LOC_NAME_SHORT[i], ref faults);
            }

            for (int i = 0; i < Expansion.ROM_LOC_NAME_BYTE.Length; i++)
            {
                Bits.IsMatchingByte(rom, Expansion.ROM_LOC_NAME_BYTE_VAL[i], Expansion.ROM_LOC_NAME_BYTE[i], ref faults);
            }

            if (Bits.ToHiROM(BASE_LOCATION) != (int)0xED8F00)
            {
                faults.Add(new[] { Expansion.ROM_LOCATION + 1, Bits.ToHiROM(BASE_LOCATION), 0xED8F00 });
            }

            return faults;
        }

        public static void CheckExpansion()
        {
            IsExpanded = false;
            IsChestsExpanded = false;

            if (File.Exists(Settings.Default.SettingsFile))
            {
                try
                {
                    Model.SettingsFile = XDocument.Load(Settings.Default.SettingsFile);
                    XElement root = Model.SettingsFile.Element("Settings");
                    IsExpanded = bool.Parse(root.Element("MapExpansion").Value);
                    IsChestsExpanded = bool.Parse(root.Element("ChestExpansion").Value);

                    if (IsExpanded)
                    {
                        SIZE_TILEMAP_DATA = int.Parse(root.Element("NumBanksTilemap").Value) << 16;
                        XElement mapNames = root.Element("MapNames");
                        LevelNames = new string[mapNames.Elements().Count()];

                        for (int i = 0; i < mapNames.Elements().Count(); i++)
                        {
                            LevelNames[i] = mapNames.Elements().ElementAt(i).Value;
                        }
                    }

                    InitExpansionFields(IsExpanded);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Corrupted XML file. Default values will be loaded.\n\n Error: " + e.Message, Model.APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    InitExpansionFields(false);

                    if (IsExpanded)
                    {
                        LevelNames = Lists.expandedLocations;
                    }
                    else
                    {
                        LevelNames = Lists.originalLocations;
                    }
                }
            }
            else
            {
                InitExpansionFields(false);
                LevelNames = Lists.originalLocations;
            }

        }

        public static void BuildSettingXml(int tilemapBank, int dataBank, int tilemapBankNum, bool mapExpansion, bool chestExpansion, bool isZplus, string[] locNames)
        {
            SettingsFile = new XDocument(new XElement("Settings",
                new XElement("MapExpansion", mapExpansion.ToString()),
                new XElement("ChestExpansion", chestExpansion.ToString()),
                new XElement("FF6LEPlus", isZplus.ToString()),
                new XElement("DataBank", dataBank.ToString("X2")),
                new XElement("TilemapBank", tilemapBank.ToString("X2")),
                new XElement("NumBanksTilemap", tilemapBankNum.ToString()),
                new XElement("MapNames")));

            for (int i = 0; i < locNames.Length; i++)
            {
                string name = LevelNames[i].Length < 6 ? LevelNames[i]: Bits.IsValidMapId(LevelNames[i].Substring(0, 6))
                        ? LevelNames[i].Substring(6, LevelNames[i].Length - 6).Trim()
                        : LevelNames[i].Trim();
                SettingsFile.Element("Settings").Element("MapNames").Add(new XElement("Name", name));
            }
        }

        public static void SaveXML()
        {
            if (File.Exists(Settings.Default.SettingsFile) && SettingsFile != null)
            {
                SettingsFile.Element("Settings").Element("MapNames").RemoveNodes();

                for (int i = 0; i < LevelNames.Length; i++)
                {
                    string name = Bits.IsValidMapId(LevelNames[i].Substring(0, 6))
                        ? LevelNames[i].Substring(6, LevelNames[i].Length - 6).Trim()
                        : LevelNames[i].Trim();
                    SettingsFile.Element("Settings").Element("MapNames").Add(new XElement("Name", name));
                }

                try
                {
                    SettingsFile.Save(Settings.Default.SettingsFile);
                }
                catch (Exception e)
                {
                    MessageBox.Show(
                        "Unable to save XML settings file. You may not have write rights or file may not exist.\n\n  Error: " +
                        e.Message, Model.APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static string[] ConvertLocNames(StringCollection collection)
        {
            string[] locNames = new string[collection.Count];
            collection.CopyTo(locNames, 0);
            return locNames;
        }

        public static string[] GetLevelNames()
        {
            string[] locNames = new string[Model.LevelNames.Length];

            for (int i = 0; i < locNames.Length; i++)
            {
                locNames[i] = "[$" + i.ToString("X3") + "] " + Model.LevelNames[i];
            }

            return locNames;
        }

        #endregion
    }
}
