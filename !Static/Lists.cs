using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ZONEDOCTOR
{
    public static class Lists
    {
        #region Variables
        #region Other
        public static string[] DialogueTable = new string[]
        {
                "", " ", "TERRA", "LOCKE", "CYAN", "SHADOW", "EDGAR", "SABIN", 
                "CELES", "STRAGO", "RELM", "SETZER", "MOG", "GAU", "GOGO", "UMARO", 
                "<D>", "", "", "", "", "^", "", "<centerEnd>", 
                "", "<N>", "<I>", "<S>", "", "", "", "", 
                "A", "B", "C", "D", "E", "F", "G", "H",
                "I", "J", "K", "L", "M", "N", "O", "P", 
                "Q", "R", "S", "T", "U", "V", "W", "X", 
                "Y", "Z", "a", "b", "c", "d", "e", "f", 
                "g", "h", "i", "j", "k", "l", "m", "n",
                "o", "p", "q", "r", "s", "t", "u", "v", 
                "w", "x", "y", "z", "0", "1", "2", "3",
                "4", "5", "6", "7", "8", "9", "!", "?", 
                "", ":", "\"", "'", "-", ".", ",", "...",
                ";", "#", "+", "(", ")", "%", "~", "", 
                "@", "<note>", "=", "\"", "74", "75", "<pearl>", "<death>",
                "<lit>", "<wind>", "<earth>", "<ice>", "<fire>", "<water>", "<poison>", " ", 
                "e ", " t", ": ", "th", "t ", "he", "s ", "er", 
                " a", "re", "in", "ou", "d ", " w", " s", "an", 
                "o ", " h", " o", "r ", "n ", "at", "to", " i",
                ", ", "ve", "ng", "ha", " m", "Th", "st", "on", 
                "yo", " b", "me", "y ", "en", "it", "ar", "ll",
                "ea", "I ", "ed", " f", " y", "hi", "is", "es", 
                "or", "l ", " c", "ne", "'s", "nd", "le", "se", 
                " I", "a ", "te", " l", "pe", "as", "ur", "u ", 
                "al", " p", "g ", "om", " d", "f ", " g", "ow",
                "rs", "be", "ro", "us", "ri", "wa", "we", "Wh", 
                "et", " r", "nt", "m ", "ma", "I'", "li", "ho",
                "of", "Yo", "h ", " n", "ee", "de", "so", "gh", 
                "ca", "ra", "n'", "ta", "ut", "el", "! ", "fo",
                "ti", "We", "lo", "e!", "ld", "no", "ac", "ce", 
                "k ", " u", "oo", "ke", "ay", "w ", "!!", "ag",
                "il", "ly", "co", ". ", "ch", "go", "ge", "e..."
        };
        public static string[] NameTable = new string[]
        {
            "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P",
            "Q","R","S","T","U","V","W","X","Y","Z","a","b","c","d","e","f",
            "g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v",
            "w","x","y","z","0","1","2","3","4","5","6","7","8","9","!","?",
            "ú",":","\"","'","-",".",".","ú",";","#","+","(",")","%","~","ú",
            "ú","ú","="
        };
        public static int[] SpecialPointers_Vehicle = new int[]
        {
            0x0A004F, 0x0A0059, 0x0A0068, 0x0A0078, 
            0x0A007F, 0x0A0088, 0x0A008F, 0x0A0096, 
            0x0A5AD4, 0x0A5EB4, 0x0A5EB5, 0x0A5EC2, 
            0x0A5ECF, 0x0A5EDC, 0x0A5EE3, 0x0A5EF0, 
            0x0A5F0B, 0x0A5F18, 0x0A5F39, 0x0A8C15, 
            0x0A8C58
        };
        public static int[] SpecialPointers_Map = new int[]
        {
            0x0A694F, 0x0B0BB7, 0x0BD2F0, 0x0BD301, 
            0x0BD308
        };
        public static int[] SpecialPointers_Cancel = new int[]
        {
            0x0A8CAE, 0x0AF4CD, 0x0AF4E3
        };
        public static int[] SpecialPointers_ASMPointers = new int[]
        {
            0x004832, 0x004842, 0x004C7C, 0x004C8F, 
            0x004C9F, 0x004CAA, 0x004CB9, 0x007545, 
            0x007555, 0x00BC34, 0x00BC44, 0x00BCFA, 
            0x00BD0A, 0x00BE91, 0x00C442, 0x00C451, 
            0x00C660, 0x00C671, 0x2EB269, 0x2EB26C, 
            0x2EB26F, 0x2EB272, 0x2EB275, 0x2EB278,
            0x2EB27B
        };
        #endregion
        #region Maps
        public static string[] ObjectNames = new string[]
            {
            "Actor in slot 0",			// 0x00
            "Actor in slot 1",			// 0x01
            "Actor in slot 2",			// 0x02
            "Actor in slot 3",			// 0x03
            "Actor in slot 4",			// 0x04
            "Actor in slot 5",			// 0x05
            "Actor in slot 6",			// 0x06
            "Actor in slot 7",			// 0x07
            "Actor in slot 8",			// 0x08
            "Actor in slot 9",			// 0x09
            "Actor in slot 10",			// 0x0A
            "Actor in slot 11",			// 0x0B
            "Actor in slot 12",			// 0x0C
            "Actor in slot 13",			// 0x0D
            "Actor in slot 14",			// 0x0E
            "Actor in slot 15",			// 0x0F
			
            "NPC 0 ($10)",			// 0x10
            "NPC 1 ($11)",			// 0x11
            "NPC 2 ($12)",			// 0x12
            "NPC 3 ($13)",			// 0x13
            "NPC 4 ($14)",			// 0x14
            "NPC 5 ($15)",			// 0x15
            "NPC 6 ($16)",			// 0x16
            "NPC 7 ($17)",			// 0x17
            "NPC 8 ($18)",			// 0x18
            "NPC 9 ($19)",			// 0x19
            "NPC 10 ($1A)",			// 0x1A
            "NPC 11 ($1B)",			// 0x1B
            "NPC 12 ($1C)",			// 0x1C
            "NPC 13 ($1D)",			// 0x1D
            "NPC 14 ($1E)",			// 0x1E
            "NPC 15 ($1F)",			// 0x1F
			
            "NPC 16 ($20)",			// 0x20
            "NPC 17 ($21)",			// 0x21
            "NPC 18 ($22)",			// 0x22
            "NPC 19 ($23)",			// 0x23
            "NPC 20 ($24)",			// 0x24
            "NPC 21 ($25)",			// 0x25
            "NPC 22 ($26)",			// 0x26
            "NPC 23 ($27)",			// 0x27
            "NPC 24 ($28)",			// 0x28
            "NPC 25 ($29)",			// 0x29
            "NPC 26 ($2A)",			// 0x2A
            "NPC 27 ($2B)",			// 0x2B
            "NPC 28 ($2C)",			// 0x2C
            "NPC 29 ($2D)",			// 0x2D
            "NPC 30 ($2E)",			// 0x2E
            "NPC 31 ($2F)",			// 0x2F
			
            "Camera",			// 0x30
            "Party Character 0",			// 0x31
            "Party Character 1",			// 0x32
            "Party Character 2",			// 0x33
            "Party Character 3",			// 0x34
            };
        #endregion
        #region Audio
        public static string[] MusicNames = new string[]
        {
            "(current music)",
            "Prelude",
            "Opening Theme #1",
            "Opening Theme #2",
            "Opening Theme #3",
            "Awakening",
            "Terra",
            "Shadow",
            "Strago",
            "Gau",
            "Edgar and Sabin",
            "Coin Song",
            "Cyan",
            "Locke",
            "Forever Rachel",
            "Relm",
            "Setzer",
            "Epitaph",
            "Celes",
            "Techno de Chocobo",
            "The Decisive Battle",
            "Johnny C. Bad",
            "Kefka",
            "The Mines of Narshe",
            "The Phantom Forest",
            "Wild West",
            "Save Them!",
            "The Empire Gestahl",
            "Troops March On",
            "Under Martial Law",
            "a waterfall",
            "Metamorphosis",
            "The Phantom Train",
            "Another World of Beasts",
            "Grand Finale #2",
            "Mt. Koltz",
            "Battle Theme",
            "Fanfare (slow)",
            "The Wedding Waltz #1",
            "Aria de Mezzo Caraterre",
            "The Serpent Trench",
            "Slam Shuffle",
            "Kids Run Through the City Corner",
            "crazy man's house",
            "Grand Finale #1",
            "Gogo",
            "Returners",
            "Fanfare",
            "Umaro",
            "Mog",
            "The Unforgiven",
            "The Fierce Battle",
            "The Day After",
            "Blackjack",
            "Catastrophe",
            "The Magic House",
            "Nighty Night",
            "wind",
            "windy shores",
            "Dancing Mad 1 - 3",
            "The Raft and the Flowing River",
            "Spinach Rag",
            "Rest in Peace",
            "train running",
            "The Dream of a Train",
            "Overture #1",
            "Overture #2",
            "Overture #3",
            "The Wedding Waltz #2",
            "The Wedding Waltz #3",
            "The Wedding Waltz #4",
            "Devil's Lab",
            "burning house",
            "machine running",
            "Inside the Burning House",
            "New Continent",
            "Searching for Friends",
            "Fanatics",
            "Last Dungeon & Aura",
            "Dark World",
            "Dancing Mad 4",
            "...silence",
            "Dancing Mad 5",
            "Ending Theme #1",
            "Ending Theme #2",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____"
        };
        #endregion
        #region Battles
        public static string[] BattlefieldNames = new string[]
        {
	        "Grass",
	        "Brown Forest",
	        "Desert",
	        "Green Forest",
	        "Building",
	        "World of Ruin",
	        "The Veldt",
	        "Falling through the Clouds",
	        "Dark Town",
	        "Grey Cave",
	        "Brown Cave",
	        "Mountain Top",
	        "Mountain Cave",
	        "Raft on a River",
	        "Imperial Base",
	        "On Top of Train Car",
	        "Inside of Train Car",
	        "Blue/Purple Cave",
	        "Icy Field",
	        "Bright Town",
	        "Factory",
	        "Floating Island",
	        "Kefka's Domain",
	        "Opera Stage",
	        "Opera House Rafters",
	        "Flaming House",
	        "Castle",
	        "Magitek Research Facility w/ Tubes",
	        "Colloseum",
	        "Magitek Research Facility",
	        "Village",
	        "Waterfall",
	        "Owzer's House",
	        "Running on Train Tracks",
	        "Bridge near Sealed Gate",
	        "Underwater",
	        "Zozo",
	        "Airship, World of Balance, centered",
	        "Tomb",
	        "Doma",
	        "Kefka's Domain",
	        "Airship, World of Ruin, right",
	        "Red Cave",
	        "Light Building",
	        "Riding Car out of Magitek Research Facility",
	        "Fanatics' Tower",
	        "Cyan's Dream World",
	        "Desert",
	        "Airship, World of Balance, right",
	        "--",
	        "--",
	        "Statue 1",
	        "Statue 2",
	        "Statue 3",
	        "Kefka's Background",
	        "Tentacles",
	        "--",
	        "--",
	        "--",
	        "--",
	        "--",
	        "--",
	        "--",
	        "Default for this area"
        };
        #endregion
        #region Levels
        public static string[] LocationNames = new string[]
        {
            "World of Balance, World Map",
            "World of Ruin, World Map",
            "Serpent Trench, World Map",
            "\"On that day...\"",
            "Shadow's Dream 1",
            "Mog's Explanation",
            "Blackjack, Upper Deck (general use)",
            "Blackjack, Inside",
            "Blackjack, Back Room",
            "\"Choose A Scenario\"",
            "Blackjack, Upper Deck (IAF sequence)",
            "Falcon, Upper Deck",
            "Falcon, Inside",
            "Falcon, Roof, Landing on Kefka's Tower",
            "Chocobo Stable, Outside (WOB, forested)",
            "Chocobo Stable, Inside",
            "Chocobo Stable, Outside (WOR, forested)",
            "Falcon, Upper Deck",
            "Narshe, Northern Cliff, Intro",
            "Narshe, Town, Beginning (WOB)",
            "Narshe, Town (WOB)",
            "Narshe, North of Town, Beginning (WOB)",
            "Narshe, Snow Battlefield (WOB)",
            "Narshe, Northern Cliff (WOB)",
            "Narshe, Weapons Shop",
            "Narshe, Armor Shop",
            "Narshe, Item Shop",
            "Narshe, Relics Shop",
            "Narshe, Inn",
            "Narshe, Pub",
            "Narshe, Elder's House",
            "___Chocobo Stable",
            "Narshe, Outside (WOR)",
            "Narshe, Outside, North (WOR)",
            "Narshe, Outside, Battlefield (WOR)",
            "Narshe, Outside, Northern Cliff (WOR)",
            "Narshe, Mines Cart Rail Rooms (WOR)",
            "Narshe, Mines Battle Room (WOR)",
            "Narshe, Mines 1 (WOR)",
            "Narshe, Outside, North, Beginning",
            "Narshe, Mines 1, Beginning (WOB)",
            "Narshe, Mines 1 (WOB)",
            "Narshe, Mines Esper Room, Beginning (WOB)",
            "Narshe, Mines Esper Room, Empty (WOB)",
            "Narshe, Moogle's Room (WOR)",
            "___",
            "___",
            "___",
            "Narshe, Mines 2, From Secret (WOB)",
            "Narshe, Mines Checkpoint (WOB)",
            "Narshe, Mines 2 (WOB)",
            "Narshe, Mines Battle Room (WOB)",
            "Narshe, Moogle's Room (WOB)",
            "Cave to Figaro Castle, Room 2",
            "Figaro Castle, Submerging",
            "Figaro Castle, Outside",
            "Figaro Desert, Kefka & Troops",
            "Figaro Castle, King's Bedroom",
            "Figaro Castle, Throne Room",
            "Figaro Castle, Inside",
            "Figaro Castle, Library",
            "Figaro Castle, Basement 1",
            "Figaro Castle, Basement 2",
            "Figaro Castle, Basement 3",
            "Figaro Castle, Engine Room",
            "Figaro Castle, Treasure Room",
            "Figaro Castle, Regal Crown Room",
            "Figaro Castle, Outside, Nighttime",
            "Cave to South Figaro (WOR)",
            "Cave to South Figaro, Rooms",
            "Cave to South Figaro (WOB)",
            "Cave to South Figaro, Entrance",
            "Cave to South Figaro, Room 2",
            "Cave to South Figaro, Room 1",
            "South Figaro, Outside (WOR)",
            "South Figaro, Outside (WOB)",
            "South Figaro, Inn/Relics",
            "South Figaro, Arsenal",
            "South Figaro, Pub",
            "___",
            "South Figaro, Chocobo Stable",
            "South Figaro, Rich Man's House",
            "___",
            "South Figaro, Basement 1",
            "South Figaro, Basement 1 Clock Room",
            "South Figaro, Item Shop",
            "South Figaro, Duncan's House",
            "South Figaro, Basement 1 Monster Room",
            "South Figaro, Basement 1 Save Point",
            "South Figaro, Basement 2",
            "Cave to Figaro Castle, Entrance (WOR)",
            "South Figaro, Dock (WOR)",
            "Cave to Figaro Castle, Room 1",
            "Sabin's House, Outside",
            "Sabin's House, Inside",
            "Mt.Kolts, Entrance",
            "Mt.Kolts, Outside",
            "Mt.Kolts, Outside Bridge",
            "Mt.Kolts, Outside Vargas Area",
            "___",
            "Mt.Kolts, Cave",
            "Mt.Kolts, Exit",
            "Mt.Kolts, Cliffs",
            "Mt.Kolts, Cave Save Point",
            "Narshe, School",
            "Narshe, School, Adv. Battle Tactics",
            "Narshe, School, Battle Tactics",
            "Narshe, School, Environmental Science",
            "Returner's Hideout, Entrance",
            "Returner's Hideout, Inside",
            "Returner's Hideout, Rooms",
            "Returner's Hideout, Inn",
            "Tunnel to Lete River",
            "Lete River",
            "Lete River Cave",
            "Gau's Father's House, Outside",
            "Gau's Father's House, Inside",
            "Military Base Camp",
            "___",
            "Military Base Camp 2",
            "Doma Castle, Outside (WOB)",
            "Doma Castle, Outside Poisoning (WOB)",
            "___",
            "Doma Castle, Inside",
            "Doma Castle, Cyan's Room",
            "Cyan's Dream, Doma Castle, Outside",
            "Cyan's Dream, Doma Castle Rooms",
            "Duncan's House, Outside",
            "Duncan's House, Inside",
            "Ending, sky with birds",
            "Ending, Phantom Forest",
            "Gau's Father's House, Outside (WOR)",
            "Phantom Forest 1",
            "Phantom Forest 2",
            "Phantom Forest 3",
            "Phantom Forest 4",
            "Ending, mountain bridge",
            "Phantom Train, Docking Station 4",
            "Phantom Train, Docking Station 2",
            "Phantom Train, Docking Station 3",
            "Phantom Train, Docking Station 1",
            "Phantom Train, Outside 2",
            "Phantom Train, Outside 1",
            "Cyan's Dream, Phantom Train, Outside",
            "Cyan's Dream, Phantom Train, Inside",
            "Phantom Train, Inside 1",
            "Phantom Train, Engineer's Room",
            "Phantom Train, Restaurant",
            "Mobliz, Outside during Light of Judgement",
            "Phantom Train, Inside 2",
            "Mobliz, Basement 2, After Phunbaba 1",
            "Phantom Train, Inside 3",
            "Phantom Train, Inside 4",
            "Phantom Train, Inside Rooms",
            "Mobliz, Basement 2",
            "Waterfall Cave",
            "Waterfall Cliff",
            "Mobliz, Outside (WOB)",
            "Mobliz, Outside (WOR)",
            "Veldt Shore",
            "Mobliz, Inn",
            "Mobliz, Arsenal",
            "Mobliz, Relics",
            "Mobliz, Mail House",
            "Mobliz, Item Shop",
            "Mobliz, Basement 1",
            "Waterfall Entrance",
            "Veldt Cave to Waterfall",
            "Veldt Waterfall",
            "Nikeah, Outside",
            "Veldt Shore",
            "Nikeah, Inn",
            "Nikeah, Pub",
            "Nikeah, Chocobo Stable",
            "Shadow's Dream 2, Phantom Forest",
            "Serpent Trench Cave 1",
            "Mt.Zozo, Outside Bridge",
            "Mt.Zozo, Outside 1",
            "Mt.Zozo, Outside 2",
            "Mt.Zozo, Inside",
            "Mt.Zozo, Cyan's Room",
            "Mt.Zozo, Cliff",
            "Ending, Relm, Shadow, Strago",
            "Mobliz, Basement 2, Before Kefka Fight",
            "Mobliz, Outside, Before Kefka Fight",
            "Crazy Man's House, Outside",
            "Crazy Man's House, Inside",
            "Nikeah, Ferry (WOB)",
            "Kohlingen, Outside (WOB)",
            "Kohlingen, Outside (WOR)",
            "Ending, Rapids",
            "Kohlingen, Inn (WOR)",
            "___Kohlingen",
            "___Kohlingen, Arsenal",
            "Kohlingen, General Store",
            "Kohlingen, Chemist's House",
            "Maranda, Flower Girl's House (WOR)",
            "Kohlingen, Rachel's House",
            "Jidoor, Outside",
            "___nothing",
            "Jidoor, Auction House",
            "Jidoor, Item Shop",
            "Jidoor, Relics",
            "Jidoor, Armor Shop",
            "Jidoor, Weapon Shop",
            "Jidoor, Chocobo Stable",
            "Jidoor, Inn",
            "Owzer's House, Basement",
            "Owzer's House, Art Room",
            "Owzer's House, F1",
            "Ending, Cyan 1",
            "Ending, Umaro, Mog 1",
            "Ending, Gogo 1",
            "Ending, Gau 1",
            "Cliff, Setzer waits for Darill",
            "Ending, Falcon flying through sky",
            "Esper World, Lake",
            "Esper World, Outside",
            "Esper World, Gate",
            "Esper World, House",
            "___boat dock",
            "Zozo, Outside",
            "___nothing",
            "___nothing",
            "___nothing",
            "Zozo, Inside House",
            "Zozo, Terra's Room",
            "___nothing",
            "___nothing",
            "___nothing",
            "___nothing",
            "Opera House, Theater",
            "Opera House, Switch Room",
            "Opera House, Theater Play 2",
            "Opera House, Theater Play 1",
            "Opera House, Ceiling",
            "Opera House, Castle Balcony",
            "Opera House, Main",
            "Opera House, Dressing Room",
            "___Opera House",
            "Vector, after Magitek Factory",
            "Imperial Castle, Cranes Activating",
            "Vector, Outside",
            "Imperial Castle",
            "Imperial Castle, Outside Roof",
            "Vector, Inn",
            "Vector, Weapon Shop",
            "Vector, Pub",
            "Vector, Armor Shop",
            "Vector, Healer's House",
            "Imperial Castle, Inside",
            "Imperial Castle, Banquet",
            "Imperial Castle, Bedroom",
            "Vector, Outside, Burning",
            "Ending, Cyan, Mog, Gogo 2",
            "Ending, Setzer",
            "Ending, Umaro 2",
            "___Magitek Factory",
            "Ending, Edgar and Sabin",
            "Ending, Falcon taking flight from Kefka's Tower",
            "Ending, Locke and Celes 1",
            "Ending, Shadow, Strago, Gau 2",
            "Magitek Factory, Room 1",
            "Magitek Factory, Room 2",
            "Magitek Factory, Room 3 (Espers)",
            "___Magitek Factory",
            "Magitek Factory, Elevator",
            "___Magitek Factory",
            "Ending, Terra",
            "Magitek Factory, Room 4",
            "Magitek Factory, Room 5 (Save)",
            "Magitek Res. Facility, Room 1",
            "Magitek Factory, Minecart Dock",
            "Magitek Res. Facility, Room 2 (Number 042)",
            "Magitek Res. Facility, Espers",
            "___dummied",
            "Zone Eater's Stomach, Room 1",
            "Zone Eater's Stomach, Room 4",
            "Zone Eater's Stomach, Room 6 (Gogo)",
            "Zone Eater's Stomach, Room 2",
            "Zone Eater's Stomach, Room 3,5",
            "Narshe, Umaro's Cave 1",
            "Narshe, Umaro's Cave 2",
            "Narshe, Umaro's Lair",
            "Maranda, Outside (WOR)",
            "Doma Castle, Outside, Abandoned",
            "___nothing",
            "Kefka's Tower, Inside From Guardian",
            "Maranda, Inn (WOR)",
            "Maranda, Weapon Shop (WOR)",
            "Maranda, Armor Shop (WOR)",
            "Kefka's Tower, Guardian's Room",
            "Kefka's Tower, Junk Room",
            "Kefka's Tower, Inside from Factory Room",
            "Kefka's Tower, Inside 2",
            "Kefka's Tower, Inside 3",
            "Kefka's Tower, Factory Room (Top Level)",
            "Darill's Tomb, Outside",
            "Darill's Tomb, Basement 1",
            "Darill's Tomb, Basement 2",
            "Darill's Tomb, Basement 3",
            "Darill's Tomb, Staircase",
            "Ending, Thamasa, Repairing burned house",
            "Kefka's Tower, Inside 1",
            "Kefka's Tower, Assassin Room",
            "Tzen, Outside (WOR)",
            "Tzen, Outside, Before House Collapse (WOR)",
            "Tzen, Item Shop (WOR)",
            "Tzen, Inn (WOR)",
            "Tzen, Weapon Shop (WOR)",
            "Tzen, Armor Shop (WOR)",
            "Tzen, Inside Collapsing House",
            "Tzen, Relics (WOR)",
            "Phoenix Cave, Big Lava Room, Drained",
            "Pheonix Cave, Big Lava Room",
            "Phoenix Cave, Main Room, Drained",
            "Phoenix Cave, Main Room",
            "Cyan's Dream, Three Stooges",
            "Pheonix Cave, Outside Entrance",
            "Cyan's Dream, Magitek Caves, Outside",
            "Cyan's Dream, Magitek Caves, Inside",
            "Cyan's Dream, Phantom Train, Inside 1",
            "Cyan's Dream, Phantom Train, Inside 2",
            "Albrook, Outside (WOB)",
            "Albrook, Outside (WOR)",
            "Albrook, Inn",
            "Albrook, Weapon Shop",
            "Albrook, Armor Shop",
            "Albrook, Item Shop",
            "Kefka's Tower, Room with Movers",
            "Albrook, Pub",
            "Kefka's Tower, Atma's Room",
            "Albrook, Ship Deck",
            "___Kefka's Tower, factory",
            "Kefka's Tower, Outside",
            "Kefka's Tower, Gold Dragon Room",
            "Kefka's Tower, Kefka's Lair 1",
            "Kefka's Tower, 4Ton Switch Room",
            "Kefka's Tower, Inside from Central Group",
            "Kefka's Tower, Inside from Eastern Group",
            "Thamasa, Outside, at Leo's Grave",
            "Thamasa, Outside, Kefka attacks",
            "___Thamasa",
            "Thamasa, Outside (WOB)",
            "Thamasa, Outside (WOR)",
            "Thamasa, Arsenal",
            "Thamasa, Inn",
            "Thamasa, Item Shop",
            "Thamasa, Elder's House",
            "Thamasa, Strago's House",
            "Thamasa, Relics",
            "Thamasa, Inside Burning House",
            "Kefka's Tower, Broken Capsules",
            "Cave in the Veldt (WOR)",
            "Kefka's Tower, Red Carpet Rooms",
            "Kefka's Tower, Three Switch Room",
            "Kefka's Tower, Left Statue Room",
            "Kefka's Tower, Kefka's Lair 2",
            "Floating Continent, Save Point",
            "Fanatic's Tower, Level 2",
            "Fanatic's Tower, Level 3",
            "Fanatic's Tower, Level 4",
            "Fanatic's Tower, Entrance",
            "Fanatic's Tower, Level 1",
            "Fanatic's Tower, Roof",
            "Fanatic's Tower, Treasure Room 2",
            "Fanatic's Tower, Gem Box Room",
            "Fanatic's Tower, Treasure Room 3",
            "Fanatic's Tower, Treasure Room 4",
            "Fanatic's Tower, Treasure Room 5",
            "Fanatic's Tower, Treasure Room 1",
            "Esper Cave, Statue Room",
            "Esper Cave, Outside 2",
            "Esper Cave, Outside 1",
            "Esper Cave, Outside 3",
            "Esper Cave",
            "Floating Continent, Destruction",
            "Imperial Base",
            "Imperial Base, House",
            "___Imperial Base",
            "Overhead View of World Map",
            "Mountains Destruction",
            "Cave to the Sealed Gate, Room 1",
            "Cave to the Sealed Gate, Basement 1",
            "Cave to the Sealed Gate, Basement 3",
            "Cave to the Sealed Gate, Basement 2",
            "Cave to the Sealed Gate, Save Point",
            "___Cave to the Sealed Gate, Basement 4",
            "___Cave to the Sealed Gate",
            "Grasslands Destruction",
            "Highlands Destruction",
            "Sealed Gate",
            "Floating Continent shadow cast above Jidoor",
            "Floating Continent, Breaking Apart",
            "Floating Continent, Outside",
            "Floating Continent Breakaway 1",
            "Cid's Island, Outside",
            "Cid's Island, Inside House",
            "Cid's Island, Beach",
            "Cid's Island, Cliff",
            "Cid's Island, Beach, no fish",
            "Ancient Cave 1",
            "Ancient Cave 2",
            "___Cid's Island, Beach",
            "Hidon's Cave",
            "Hidon's Cave, Entrance",
            "Ancient Castle, Inside",
            "Ancient Castle, Outside",
            "Ancient Castle, Eastern Room",
            "Kefka's Tower, Pipe Room",
            "Kefka's Tower, Factory Room (Bottom Level)",
            "Kefka's Tower, Final Room",
            "Kefka's Tower, Guardian Path Save Point",
            "Colosseum",
            "___"
        };
        #endregion
        #region Events
        public static string[] EventLabels = new string[4096];
        public static string[] Vehicles = new string[]
        {
            "No vehicle","Chocobo","Magitek armor","Raft"
        };
        public static string[] StatusNames = new string[]
        {
	        "Dark","Zombie",
	        "Poison","M-Tek", 
	        "Vanish (Clear)","Imp",
	        "Petrify (Stone)","Death (Wound)",
	        "Rage","Frozen",
	        "Instant Death Protection","Morph (Esper-form)", 
	        "Chant (Casting Spell)","Hide",
	        "Dog Block","Float"
        };
        public static string[] ColorNames = new string[] 
        {
            "Black","Black",
            "Red","Red",
            "Green","Green",
            "Yellow [Red + Green]","Yellow [Red + Green]",
            "Blue","Blue",
            "Magenta [Red + Blue]","Magenta [Red + Blue]",
            "Cyan [Green + Blue]","Cyan [Green + Blue]",
            "White [Red + Green + Blue]","White [Red + Green + Blue]"
        };
        public static string[] EventCategoryNames = new string[]
        {
            "Objects",
            "Party",
            "Battle",
            "Locations",
            "Menus",
            "Dialogues",
            "Events",
            "Jump to",
            "Screen",
            "Audio",
            "Memory",
            "Event bits",
            "Caseword",
            "Pause",
            "Return"
        };
        public static string[] CharacterCategoryNames = new string[]
        {
            "Move up",
            "Move right",
            "Move down",
            "Move left",
            "Move diagonally",
            "Screen",
            "If memory (OR)",
            "If memory (AND)",
            "Properties",
            "Action",
            "Event bits",
            "Jump to",
            "Return"
        };
        public static string[] MapCategoryNames = new string[]
        {
            "Move up",
            "Move right",
            "Move down",
            "Move left",
            "Move diagonally",
            "Screen",
            "If memory (OR)",
            "If memory (AND)",
            "Properties",
            "Action",
            "Event bits",
            "Jump to",
            "Maps",
            "Return"
        };
        public static string[] VehicleCategoryNames = new string[]
        {
            "Screen",
            "If memory (OR)",
            "If memory (AND)",
            "Properties",
            "Action",
            "Events",
            "Memory bits",
            "Maps",
            "Return"
        };
        public static string[] SubCommands = new string[]
        {
            "Play song at volume (other/unknown effects)",
            "Play song at volume (other/unknown effects)",
            "Set volume of currently playing song/sound...",
            "Change volume of currently playing song...",
            "Change volume of currently playing sound...",
            "Change pan control of currently playing sound...",
            "Change tempo of currently playing song...",
            "Change pitch of currently playing song...",
            "Stop currently playing song, unused bytes...",
            "Stop currently playing sound, unused bytes...",
            "Unknown bytes..."
        };
        public static int[][] EventOpcodes = new int[][]
        {
            new int[] // Objects
            {
                0x00,0x35,0x36,0x37,0x38,0x39,0x3A,0x3B,
                0x3C,0x3D,0x3E,0x3F,0x40,0x41,0x42,0x43,
                0x44,0x45,0x46,0x47,0x78,0x7A,0x7C,0x7D
            },               
            new int[] // Party
            {
                0x77,0x7B,0x7F,0x80,0x81,0x82,0x84,0x85,
                0x86,0x87,0x88,0x89,0x8A,0x8B,0x8C,0x8D,
                0x8F,0x90,0x9C
            },             
            new int[] // Battle
            {
                0x4C,0x4D,0x4E,0x8E
            },              
            new int[] // Locations
            {
                0x4F,0x6A,0x6B,0x6C,0x73,0x74,0x75,0x79,
                0x7E
            },              
            new int[] // Menus
            {
                0x98,0x99,0x9A,0x9B,0x9D
            },
            new int[] // Dialogues
            {
                0x48,0x49,0x4B,0xB6
            },
            new int[] // Events
            {
                0xA6,0xA7,0xA8,0xA9,0xAA,0xAB,0xAC,0xAD,
                0xAE,0xAF,0xBA,0xBF
            },
            new int[] // Jump to
            {
                0xA1,0xB0,0xB1,0xB2,0xB3,0xB7,0xB8,0xB9,
                0xBC,0xBD,0xBE
            },
            new int[] // Screen
            {
                0x50,0x51,0x52,0x53,0x54,0x55,0x56,0x57,
                0x58,0x59,0x5A,0x5C,0x5D,0x5E,0x5F,0x60,
                0x61,0x62,0x63,0x70,0x71,0x72,0x96,0x97
            },
            new int[] // Audio
            {
                0xEF,0xF0,0xF1,0xF2,0xF3,0xF4,0xF5,0xF6,
                0xF7,0xF9,0xFA,0xFB,0xFD
            },
            new int[] // Memory
            {
                0xC0,0xC1,0xC2,0xC3,0xC4,0xC5,0xC6,0xC7,
                0xC8,0xC9,0xCA,0xCB,0xCC,0xCD,0xCE,0xCF
            },
            new int[] // Event bits
            {
                0xD0,0xD1,0xD2,0xD3,0xD4,0xD5,0xD6,0xD7,
                0xD8,0xD9,0xDA,0xDB,0xDC,0xDD
            },
            new int[] // Caseword
            {
                0xDE,0xDF,0xE0,0xE1,0xE2,0xE3,0xE4,0xE7,
                0xE8,0xE9,0xEA,0xEB
            },
            new int[] // Pause
            {
                0x91,0x92,0x93,0x94,0x95,0xA0,0xB4,0xB5
            },
            new int[] // Return
            {
                0xFE,0xFF
            }
        };
        public static int[][] CharacterOpcodes = new int[][]
        {
            new int[] // Move up
            { 
                0x80,0x84,0x88,0x8C,0x90,0x94,0x98,0x9C,             
            },
            new int[] // Move right
            { 
                0x81,0x85,0x89,0x8D,0x91,0x95,0x99,0x9D,               
            },
            new int[] // Move down
            { 
                0x82,0x86,0x8A,0x8E,0x92,0x96,0x9A,0x9E,               
            },
            new int[] // Move left
            { 
                0x83,0x87,0x8B,0x8F,0x93,0x97,0x9B,0x9F,                
            },
            new int[] // Move diagonally
            { 
                0xA0,0xA1,0xA2,0xA3,0xA4,0xA5,0xA6,0xA7,
                0xA8,0xA9,0xAA,0xAB,        
            },
            new int[] // Screen
            {
                0xD8,0xD9
            },
            new int[] // If memory (OR)
            { 
                0xB0,0xB1,0xB2,0xB3,0xB4,0xB5,0xB6,0xB7,                  
            },
            new int[] // If memory (AND)
            { 
                0xB8,0xB9,0xBA,0xBB,0xBC,0xBD,0xBE,0xBF,                 
            },
            new int[] // Properties
            { 
                0xC0,0xC1,0xC2,0xC3,0xC4,0xC5,0xC6,0xC7,
                0xC8,0xC9,          
            },
            new int[] // Action
            { 
                0x00,0xCC,0xCD,0xCE,0xCF,0xD0,0xD1,0xD4,
                0xD5,0xD7,0xD8,0xD9,0xDC,0xDD,0xE0,         
            },
            new int[] // Event bits
            { 
                0xE1,0xE2,0xE3,0xE4,0xE5,0xE6,                 
            },
            new int[] // Jump to
            { 
                0xF9,0xFA,0xFB,0xFC,0xFD,0xFF,                  
            },
            new int[] // Return
            { 
                0xFE,                  
            }
        };
        public static int[][] MapOpcodes = new int[][]
        {
            new int[] // Move up
            { 
                0x80,0x84,0x88,0x8C,0x90,0x94,0x98,0x9C,             
            },
            new int[] // Move right
            { 
                0x81,0x85,0x89,0x8D,0x91,0x95,0x99,0x9D,               
            },
            new int[] // Move down
            { 
                0x82,0x86,0x8A,0x8E,0x92,0x96,0x9A,0x9E,               
            },
            new int[] // Move left
            { 
                0x83,0x87,0x8B,0x8F,0x93,0x97,0x9B,0x9F,                
            },
            new int[] // Move diagonally
            { 
                0xA0,0xA1,0xA2,0xA3,0xA4,0xA5,0xA6,0xA7,
                0xA8,0xA9,0xAA,0xAB,        
            },
            new int[] // Screen
            {
                0xD8,0xD9
            },
            new int[] // If memory (OR)
            { 
                0xB0,0xB1,0xB2,0xB3,0xB4,0xB5,0xB6,0xB7,                  
            },
            new int[] // If memory (AND)
            { 
                0xB8,0xB9,0xBA,0xBB,0xBC,0xBD,0xBE,0xBF,                 
            },
            new int[] // Properties
            { 
                0xC0,0xC1,0xC2,0xC3,0xC4,0xC5,0xC6,0xC7,
                0xC8,0xC9,          
            },
            new int[] // Action
            { 
                0x00,0xCC,0xCD,0xCE,0xCF,0xD0,0xD1,0xD4,
                0xD5,0xD7,0xD8,0xD9,0xDC,0xDD,0xE0,         
            },
            new int[] // Event bits
            { 
                0xE1,0xE2,0xE3,0xE4,0xE5,0xE6,                 
            },
            new int[] // Jump to
            { 
                0xF9,0xFA,0xFB
            },
            new int[] // Maps
            { 
                0xD2,0xD3,0xFD,0xFE,                  
            },
            new int[] // Return
            { 
                0xFF,
            }
        };
        public static int[][] VehicleOpcodes = new int[][]
        {
            new int[] // Screen
            {
                0xD8,0xD9
            },
            new int[] // If memory (OR)
            { 
                0xB0,0xB1,0xB2,0xB3,0xB4,0xB5,0xB6,0xB7
            },
            new int[] // If memory (AND)
            { 
                0xB8,0xB9,0xBA,0xBB,0xBC,0xBD,0xBE,0xBF
            },
            new int[] // Properties
            { 
                0xC0,0xC1,0xC2,0xC3,0xC4,0xC5,0xF4,0xF7,
                0xFD
            },
            new int[] // Action
            { 
                0x00,0xC6,0xC7,0xD0,0xD1,0xDA,0xDB,0xDC,
                0xCA,0xE0
            },
            new int[] // Events
            {
                0xF3,0xF5,0xF8,0xFA,0xFB,0xFC,0xFE
            },
            new int[] // Memory bits
            { 
                0xC8,0xC9
            },
            new int[] // Maps
            { 
                0xD2,0xD3,0xDD,0xDF
            },
            new int[] // Return
            { 
                0xFF
            }
        };
        public static string[] EventNames(string category)
        {
            switch (category)
            {
                case "Objects":
                    return new string[]
                    {
						"Action queue...",
						"Pause until action queue for object is complete...",
						"Disable ability to pass through other objects...",
						"Assign graphics to object...",
						"Hold screen",
						"Free screen",
						"Enable player to move while event commands execute",
						"Position character in a \"ready-to-go\" stance",
						"Set up the party as follows...",
						"Create object...",
						"Delete object...",
						"Assign/remove character in party...",
						"Assign properties to character...",
						"Show object...",
						"Hide object...",
						"Assign palette to character...",
						"Place character on vehicle...",
						"Refresh objects",
						"Make party # the current party...",
						"Make character in slot 0 the lead character",
						"Enable ability to pass through other objects...",
						"Change event address for object to address...",
						"Enable activation of event for object...",
						"Disable activation of event for object...",
                    };
                case "Party":
                    return new string[] 
                    {
						"Do level avg'g on character and get new max HP/MP...",
						"Restore backup party (in $055D) to active status",
						"Change character's name to...",
						"Add item to inventory...",
						"Remove item from inventory...",
						"Store party 1 as backup party (in $055D)",
						"Give GP to party...",
						"Take GP from party...",
						"Give esper to party...",
						"Take esper from party...",
						"Remove the following status ailments from character...",
						"Inflict the following status ailments on character...",
						"Toggle the following status ailments for character...",
						"For character, take HP and set to maximum...",
						"For character, take MP and set to maximum...",
						"Remove all equipment from character...",
						"Unlock all of Cyan's SwdTechs",
						"Grant Sabin the Bum Rush",
						"Place optimum equipment on character...",
                    };
                case "Battle":
                    return new string[] 
                    { 
						"Center screen on party and invoke battle...",
						"Invoke battle...",
						"Invoke battle, random encounter by zone",
						"Invoke battle: enemy set from monster-in-a-box",
                    };
                case "Locations":
                    return new string[] 
                    { 
						"Exit the current location",
						"Load map after fade out...",
						"Load map instantly...",
						"Set parent map...",
						"Replace current map's Layer 1 with following chunk...",
						"Replace current map's Layer 2 with following chunk...",
						"Refresh map after alteration",
						"Place party # on map...",
						"Move characters to coords...",
                    };
                case "Menus":
                    return new string[] 
                    { 
						"Invoke name change screen for character...",
						"Invoke party selection screen...",
						"Invoke Colosseum item selection screen",
						"Invoke shop...",
						"Invoke party fighting order screen (from final battle)",
                    };
                case "Dialogues":
                    return new string[] 
                    { 
						"Display dialogue, continue executing commands...",
						"If dialogue window up, wait for keypress then dismiss",
						"Display dialogue, wait for button press...",
						"Indexed branch based on prior dialogue selection...",
                    };
                case "Events":
                    return new string[] 
                    { 
						"Delete any rotating pyramids",
						"Create a rotating pyramid around character...",
						"Show floating island soaring into the sky",
						"Show title screen",
						"Show intro with Magitek Armor walking in snowfields",
						"Invoke game loading screen",
						"Command $AC",
						"Show world getting torn apart",
						"Show train car ride out of Magitek Factory",
						"Invoke random Colosseum battle",
						"Play ending cinematic...",
						"Show airship scene from ending",
                    };
                case "Jump to":
                    return new string[] 
                    { 
						"Reset timer...",
						"Execute the following commands until $B1 # times...",
						"End block of repeating commands",
						"Call subroutine...",
						"Call subroutine # times...",
						"If bit $1DC9 is clear, branch to...",
						"Set bit $1DC9...",
						"Clear bit $1DC9...",
						"Return if bit $1E80...",
						"Pseudo-randomly jump to offset 50% of the time...",
						"If # is in the current CaseWord, jump to subroutine...",
                    };
                case "Screen":
                    return new string[] 
                    { 
						"Tint screen (cumulative) with color...",
						"Modify background color range...",
						"Tint characters (cumulative) with color...",
						"Modify object color range from...",
						"End effects of modifed colors and screen flashes",
						"Flash screen with color component(s)...",
						"Increase color component(s)...",
						"Decrease color component(s)...",
						"Shake screen...",
						"Unfade screen at speed...",
						"Fade screen at speed...",
						"Pause execution until fade in or fade out is complete",
						"Scroll Layer 1, speed...",
						"Scroll Layer 2, speed...",
						"Scroll Layer 3, speed...",
						"Change background layer to palette...",
						"Colorize color range to color...",
						"Mosaic screen, speed...",
						"Create spotlight effect with radius...",
						"Scroll Layer 1...",
						"Scroll Layer 2...",
						"Scroll Layer 3...",
						"Restore screen from fade",
						"Fade screen to black",
                    };
                case "Audio":
                    return new string[] 
                    { 
						"Play song at volume...",
						"Play song at full volume...",
						"Fade in song with transition time...",
						"Fade out current song with transition time...",
						"Fade in previously faded out song with time...",
						"Play sound effect...",
						"Play sound effect (w/time & speaker balance)...",
						"Play song at volume (subcommands)...",
						"End most recent loop of currently playing song",
						"Pause execution until music passes through point...",
						"Stop temporarily played song",
						"Apply a special effect (echo?) to the current sound",
						"No operation",
                    };
                case "Memory":
                    return new string[] 
                    {
						"If $1E80 bit is set/clear, branch to...",
						"If $1E80 bit is set/clear, branch to (2 OR condit.)...",
						"If $1E80 bit is set/clear, branch to (3 OR condit.)...",
						"If $1E80 bit is set/clear, branch to (4 OR condit.)...",
						"If $1E80 bit is set/clear, branch to (5 OR condit.)...",
						"If $1E80 bit is set/clear, branch to (6 OR condit.)...",
						"If $1E80 bit is set/clear, branch to (7 OR condit.)...",
						"If $1E80 bit is set/clear, branch to (8 OR condit.)...",
						"If $1E80 bit is set/clear, branch to...",
						"If $1E80 bit is set/clear, branch to (2 AND condit.)...",
						"If $1E80 bit is set/clear, branch to (3 AND condit.)...",
						"If $1E80 bit is set/clear, branch to (4 AND condit.)...",
						"If $1E80 bit is set/clear, branch to (5 AND condit.)...",
						"If $1E80 bit is set/clear, branch to (6 AND condit.)...",
						"If $1E80 bit is set/clear, branch to (7 AND condit.)...",
						"If $1E80 bit is set/clear, branch to (8 AND condit.)...",
                    };
                case "Event bits":
                    return new string[] 
                    {
						"Set event bit $1E80($0...",
						"Clear event bit $1E80($0...",
						"Set event bit $1E80($1...",
						"Clear event bit $1E80($1...",
						"Set event bit $1E80($2...",
						"Clear event bit $1E80($2...",
						"Set event bit $1E80($3...",
						"Clear event bit $1E80($3...",
						"Set event bit $1E80($4...",
						"Clear event bit $1E80($4...",
						"Set event bit $1E80($5...",
						"Clear event bit $1E80($5...",
						"Set event bit $1E80($6...",
						"Clear event bit $1E80($6...",
                    };
                case "Caseword":
                    return new string[] 
                    {
						"Load CaseWord with characters in the current party?",
						"Load CaseWord with characters who have been created?",
						"Load CaseWord with characters encountered so far?",
						"Load CaseWord with characters who are collected",
						"Set bit in CaseWord for lead character in current party",
						"Load CaseWord with available characters?",
						"Set CaseWord bit corresp. to number of current party",
						"Show portrait for character...",
						"Set word $1FC2...",
						"Increment word $1FC2...",
						"Decrement word $1FC2...",
						"Compare word $1FC2...",
                    };
                case "Pause":
                    return new string[] 
                    { 
						"Pause for 15 units",
						"Pause for 30 units",
						"Pause for 45 units",
						"Pause for 60 units",
						"Pause for 120 units",
						"Set timer, jump to subroutine if it expires...",
						"Pause for # units...",
						"Pause for 15 * # of units...",
                    };
                case "Return":
                    return new string[] 
                    {
						"Return",
						"Return all",
                    };
                default:
                    return new string[] { };
            }
        }
        // Character and Map scripts
        public static string[] CharacterNames(string category)
        {
            switch (category)
            {
                case "Move up":
                    return new string[] 
                    { 
						"Move up 1 tile",
						"Move up 2 tiles",
						"Move up 3 tiles",
						"Move up 4 tiles",
						"Move up 5 tiles",
						"Move up 6 tiles",
						"Move up 7 tiles",
						"Move up 8 tiles",
                    };
                case "Move right":
                    return new string[] 
                    { 
						"Move right 1 tile",
						"Move right 2 tiles",
						"Move right 3 tiles",
						"Move right 4 tiles",
						"Move right 5 tiles",
						"Move right 6 tiles",
						"Move right 7 tiles",
						"Move right 8 tiles",
                    };
                case "Move down":
                    return new string[] 
                    { 
						"Move down 1 tile",
						"Move down 2 tiles",
						"Move down 3 tiles",
						"Move down 4 tiles",
						"Move down 5 tiles",
						"Move down 6 tiles",
						"Move down 7 tiles",
						"Move down 8 tiles",
                    };
                case "Move left":
                    return new string[] 
                    { 
						"Move left 1 tile",
						"Move left 2 tiles",
						"Move left 3 tiles",
						"Move left 4 tiles",
						"Move left 5 tiles",
						"Move left 6 tiles",
						"Move left 7 tiles",
						"Move left 8 tiles",
                    };
                case "Move diagonally":
                    return new string[] 
                    { 
						"Move right/up 1x1 tiles",
						"Move right/down 1x1 tiles",
						"Move left/down 1x1 tiles",
						"Move left/up 1x1 tiles",
						"Move right/up 1x2 tiles",
						"Move right/up 2x1 tiles",
						"Move right/down 2x1 tiles",
						"Move right/down 1x2 tiles",
						"Move left/down 1x2 tiles",
						"Move left/down 2x1 tiles",
						"Move left/up 2x1 tiles",
						"Move left/up 1x2 tiles",
                    };
                case "Screen":
                    return new string[]
                    {
						"Unfade screen",
						"Fade screen",
                    };
                case "If memory (OR)":
                    return new string[] 
                    { 
						"If $1E80 bit is set/clear, branch to...",
						"If $1E80 bit is set/clear, branch to (2 OR condit.)...",
						"If $1E80 bit is set/clear, branch to (3 OR condit.)...",
						"If $1E80 bit is set/clear, branch to (4 OR condit.)...",
						"If $1E80 bit is set/clear, branch to (5 OR condit.)...",
						"If $1E80 bit is set/clear, branch to (6 OR condit.)...",
						"If $1E80 bit is set/clear, branch to (7 OR condit.)...",
						"If $1E80 bit is set/clear, branch to (8 OR condit.)...",
                    };
                case "If memory (AND)":
                    return new string[] 
                    { 
						"If $1E80 bit is set/clear, branch to...",
						"If $1E80 bit is set/clear, branch to (2 AND condit.)...",
						"If $1E80 bit is set/clear, branch to (3 AND condit.)...",
						"If $1E80 bit is set/clear, branch to (4 AND condit.)...",
						"If $1E80 bit is set/clear, branch to (5 AND condit.)...",
						"If $1E80 bit is set/clear, branch to (6 AND condit.)...",
						"If $1E80 bit is set/clear, branch to (7 AND condit.)...",
						"If $1E80 bit is set/clear, branch to (8 AND condit.)...",
                    };
                case "Properties":
                    return new string[] 
                    { 
						"Set event speed to slowest",
						"Set event speed to slow",
						"Set event speed to normal",
						"Set event speed to fast",
						"Set event speed to faster",
						"Set event speed to fastest",
						"Set entity to walk when moving",
						"Set entity to stay still when moving",
						"Set object layering priority...",
						"Place object on vehicle...",
                    };
                case "Action":
                    return new string[] 
                    { 
                    	"Graphical action...",
						"Turn object upward",
						"Turn object right",
						"Turn object downward",
						"Turn object left",
						"Make object visible",
						"Make object disappear",
						"If ($08 & 0x80 == 0), branch to...",
						"Set position to...",
						"Center screen on entity",
						"Unfade screen",
						"Fade screen",
						"Make entity jump low",
						"Make entity jump high",
						"Pause for 4 * # frames...",
                    };
                case "Event bits":
                    return new string[] 
                    { 
						"Set event bit $1E80($0...",
						"Set event bit $1E80($1...",
						"Set event bit $1E80($2...",
						"Clear event bit $1E80($0...",
						"Clear event bit $1E80($1...",
						"Clear event bit $1E80($2...",
                    };
                case "Jump to":
                    return new string[] 
                    { 
						"Branch out of queue to...",
						"Pseudo-randomly choose to branch backwards...",
						"Pseudo-randomly choose to branch forwards...",
						"Branch backwards...",
						"Branch forwards...",
						"End queue",
                    };
                case "Return":  // Character scripts only
                    return new string[] 
                    { 
						"Return",
                    };
                default:
                    return new string[] { };
            }
        }
        public static string[] MapNames(string category)
        {
            switch (category)
            {
                case "Move up":
                    return new string[] 
                    { 
						"Move up 1 tile",
						"Move up 2 tiles",
						"Move up 3 tiles",
						"Move up 4 tiles",
						"Move up 5 tiles",
						"Move up 6 tiles",
						"Move up 7 tiles",
						"Move up 8 tiles",
                    };
                case "Move right":
                    return new string[] 
                    { 
						"Move right 1 tile",
						"Move right 2 tiles",
						"Move right 3 tiles",
						"Move right 4 tiles",
						"Move right 5 tiles",
						"Move right 6 tiles",
						"Move right 7 tiles",
						"Move right 8 tiles",
                    };
                case "Move down":
                    return new string[] 
                    { 
						"Move down 1 tile",
						"Move down 2 tiles",
						"Move down 3 tiles",
						"Move down 4 tiles",
						"Move down 5 tiles",
						"Move down 6 tiles",
						"Move down 7 tiles",
						"Move down 8 tiles",
                    };
                case "Move left":
                    return new string[] 
                    { 
						"Move left 1 tile",
						"Move left 2 tiles",
						"Move left 3 tiles",
						"Move left 4 tiles",
						"Move left 5 tiles",
						"Move left 6 tiles",
						"Move left 7 tiles",
						"Move left 8 tiles",
                    };
                case "Move diagonally":
                    return new string[] 
                    { 
						"Move right/up 1x1 tiles",
						"Move right/down 1x1 tiles",
						"Move left/down 1x1 tiles",
						"Move left/up 1x1 tiles",
						"Move right/up 1x2 tiles",
						"Move right/up 2x1 tiles",
						"Move right/down 2x1 tiles",
						"Move right/down 1x2 tiles",
						"Move left/down 1x2 tiles",
						"Move left/down 2x1 tiles",
						"Move left/up 2x1 tiles",
						"Move left/up 1x2 tiles",
                    };
                case "Screen":
                    return new string[]
                    {
						"Unfade screen",
						"Fade screen",
                    };
                case "If memory (OR)":
                    return new string[] 
                    { 
						"If $1E80 bit is set/clear, branch to...",
						"If $1E80 bit is set/clear, branch to (2 OR condit.)...",
						"If $1E80 bit is set/clear, branch to (3 OR condit.)...",
						"If $1E80 bit is set/clear, branch to (4 OR condit.)...",
						"If $1E80 bit is set/clear, branch to (5 OR condit.)...",
						"If $1E80 bit is set/clear, branch to (6 OR condit.)...",
						"If $1E80 bit is set/clear, branch to (7 OR condit.)...",
						"If $1E80 bit is set/clear, branch to (8 OR condit.)...",
                    };
                case "If memory (AND)":
                    return new string[] 
                    { 
						"If $1E80 bit is set/clear, branch to...",
						"If $1E80 bit is set/clear, branch to (2 AND condit.)...",
						"If $1E80 bit is set/clear, branch to (3 AND condit.)...",
						"If $1E80 bit is set/clear, branch to (4 AND condit.)...",
						"If $1E80 bit is set/clear, branch to (5 AND condit.)...",
						"If $1E80 bit is set/clear, branch to (6 AND condit.)...",
						"If $1E80 bit is set/clear, branch to (7 AND condit.)...",
						"If $1E80 bit is set/clear, branch to (8 AND condit.)...",
                    };
                case "Properties":
                    return new string[] 
                    { 
						"Set event speed to slowest",
						"Set event speed to slow",
						"Set event speed to normal",
						"Set event speed to fast",
						"Set event speed to faster",
						"Set event speed to fastest",
						"Set entity to walk when moving",
						"Set entity to stay still when moving",
						"Set object layering priority...",
						"Place object on vehicle...",
                    };
                case "Action":
                    return new string[] 
                    { 
                    	"Graphical action...",
						"Turn object upward",
						"Turn object right",
						"Turn object downward",
						"Turn object left",
						"Make object visible",
						"Make object disappear",
						"If ($08 & 0x80 == 0), branch to...",
						"Set position to...",
						"Center screen on entity",
						"Unfade screen",
						"Fade screen",
						"Make entity jump low",
						"Make entity jump high",
						"Pause for 4 * # frames...",
                    };
                case "Event bits":
                    return new string[] 
                    { 
						"Set event bit $1E80($0...",
						"Set event bit $1E80($1...",
						"Set event bit $1E80($2...",
						"Clear event bit $1E80($0...",
						"Clear event bit $1E80($1...",
						"Clear event bit $1E80($2...",
                    };
                case "Jump to":
                    return new string[] 
                    { 
						"Branch out of queue to...",
						"Pseudo-randomly choose to branch backwards...",
						"Pseudo-randomly choose to branch forwards...",
                    };
                case "Maps":    // Map scripts only
                    return new string[] 
                    { 
						"Load map (end queue)...",
						"Load map...",
						"Show Figaro Castle submerging",
						"Show Figaro Castle emerging",
                    };
                case "Return":
                    return new string[] 
                    { 
						"End queue",
                    };
                default:
                    return new string[] { };
            }
        }
        public static string[] VehicleNames(string category)
        {
            switch (category)
            {
                case "Screen":
                    return new string[]
                    {
						"Unfade screen",
						"Fade screen",
                    };
                case "If memory (OR)":
                    return new string[] 
                    { 
						"If $1E80 bit is set/clear, branch to...",
						"If $1E80 bit is set/clear, branch to (2 OR condit.)...",
						"If $1E80 bit is set/clear, branch to (3 OR condit.)...",
						"If $1E80 bit is set/clear, branch to (4 OR condit.)...",
						"If $1E80 bit is set/clear, branch to (5 OR condit.)...",
						"If $1E80 bit is set/clear, branch to (6 OR condit.)...",
						"If $1E80 bit is set/clear, branch to (7 OR condit.)...",
						"If $1E80 bit is set/clear, branch to (8 OR condit.)...",
                    };
                case "If memory (AND)":
                    return new string[] 
                    { 
						"If $1E80 bit is set/clear, branch to...",
						"If $1E80 bit is set/clear, branch to (2 AND condit.)...",
						"If $1E80 bit is set/clear, branch to (3 AND condit.)...",
						"If $1E80 bit is set/clear, branch to (4 AND condit.)...",
						"If $1E80 bit is set/clear, branch to (5 AND condit.)...",
						"If $1E80 bit is set/clear, branch to (6 AND condit.)...",
						"If $1E80 bit is set/clear, branch to (7 AND condit.)...",
						"If $1E80 bit is set/clear, branch to (8 AND condit.)...",
                    };
                case "Properties":
                    return new string[] 
                    { 
						"Modify vehicle script behavior...",
						"Set vehicle's facing direction to...",
						"Set vehicle's propulsion direction to...",
						"Vehicle script command...",
						"Set mode 7 height perspective to...",
						"Set vehicle height to...",
						"Change graphic to Falcon",
						"Change graphic to pidgeon",
						"Change graphic to Esper Terra",
                    };
                case "Action":
                    return new string[] 
                    { 
						"Move vehicle as follows...",
						"Propel vehicle at speed...",
						"Place airship at position...",
						"Show vehicle",
						"Hide vehicle",
						"Show flashing arrows indicating direction turning",
						"Latch the input for direction turned",
						"Hide flashing arrows that indicate direction turning",
						"Invoke battle...",
						"Pause for # units",
                    };
                case "Events":
                    return new string[]
                    {
						"Show part of world getting zapped ($F3)",
						"Show part of world getting zapped ($F5)",
						"Show part of world getting blown up",
						"Show airship emerging from the ocean",
						"Show airship smoking",
						"Show airship crashing",
						"Show scene with airship heading to Vector",
                    };
                case "Memory bits":
                    return new string[] 
                    { 
						"Set bit $1E80...",
						"Clear bit $1E80...",
                    };
                case "Maps":
                    return new string[] 
                    { 
						"Load map (break queue)...",
						"Load map...",
						"Hide mini-map",
						"Show mini-map",
                    };
                case "Return":
                    return new string[] 
                    { 
						"End queue",
                    };
                default:
                    return new string[] { };
            }
        }
        #endregion
        #endregion
        #region Functions
        // numerize
        public static string Numerize(string item, int index, int length)
        {
            return "{" + index.ToString("d" + length) + "}  " + item;
        }
        public static string Numerize(string[] list, int index, int length)
        {
            return "{" + index.ToString("d" + length) + "}  " + list[index];
        }
        public static string Numerize(string[] list, int index)
        {
            if (index >= list.Length)
                return "ERROR: OUT OF BOUNDS INDEX";
            int length = (list.Length - 1).ToString().Length;
            return "{" + index.ToString("d" + length) + "}  " + list[index];
        }
        public static string[] Numerize(int length, string[] list)
        {
            string[] temp = new string[list.Length];
            for (int i = 0; i < list.Length; i++)
                temp[i] = "{" + i.ToString("d" + length) + "}  " + list[i];
            return temp;
        }
        public static string[] Numerize(string[] list)
        {
            int length = (list.Length - 1).ToString().Length;
            string[] temp = new string[list.Length];
            for (int i = 0; i < list.Length; i++)
                temp[i] = "{" + i.ToString("d" + length) + "}  " + list[i];
            return temp;
        }
        public static string[] Numerize(int start, int end, string[] list)
        {
            int length = (list.Length - 1).ToString().Length;
            string[] temp = new string[end - start];
            for (int i = start; i < list.Length && i < end; i++)
                temp[i - start] = "{" + i.ToString("d" + length) + "}  " + list[i];
            return temp;
        }
        public static string[] Numerize(StringCollection list)
        {
            return Numerize(Convert(list));
        }
        public static string Numerize(StringCollection list, int index)
        {
            return Numerize(Convert(list), index);
        }
        public static string[] Numerize(object[] list)
        {
            return Numerize(Convert(list));
        }
        public static string RemoveTag(string item)
        {
            if (item.StartsWith("{"))
            {
                while (item.Length > 0 && !item.StartsWith(" "))
                    item = item.Remove(0, 1);
                item = item.Trim();
            }
            return item;
        }
        // conversion
        public static string[] Convert(StringCollection list)
        {
            string[] temp = new string[list.Count];
            list.CopyTo(temp, 0);
            return temp;
        }
        public static string[] Convert(object[] list)
        {
            string[] temp = new string[list.Length];
            for (int i = 0; i < list.Length; i++)
                temp[i] = list[i].ToString();
            return temp;
        }
        /// <summary>
        /// Converts any array to a string array.
        /// </summary>
        /// <param name="list">The array to convert.</param>
        /// <param name="length">The number of elements that the string array will contain.</param>
        /// <param name="startIndex">The index of each string to start at.</param>
        /// <returns></returns>
        public static string[] Convert(object[] list, int length, int startIndex)
        {
            string[] temp = new string[length];
            for (int i = 0; i < list.Length && i < length; i++)
                temp[i] = list[i].ToString().Substring(startIndex);
            return temp;
        }
        public static string[] Convert(object[] list, int length)
        {
            return Convert(list, length, 0);
        }
        public static string[] Convert(ComboBox.ObjectCollection list)
        {
            object[] array = new object[list.Count];
            list.CopyTo(array, 0);
            return Convert(array, list.Count, 0);
        }
        // transformation
        public static string[] Resize(string[] list, int count)
        {
            string[] temp = new string[count];
            for (int i = 0; i < list.Length && i < count; i++)
                temp[i] = list[i];
            return temp;
        }
        public static string[] Copy(string[] source)
        {
            if (source == null)
                return null;
            string[] temp = new string[source.Length];
            source.CopyTo(temp, 0);
            return temp;
        }
        public static string ToTitleCase(string source)
        {
            TextInfo textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(source.ToLower());
        }
        public static int IndexOf(string[] list, string item)
        {
            for (int i = 0; i < list.Length; i++)
                if (list[i] == item)
                    return i;
            return -1;
        }
        #endregion
    }
}
