using System;
using System.Collections.Generic;
using System.Text;

namespace ZONEDOCTOR.ScriptsEditor.Commands
{
    public partial class Interpreter
    {
        #region Static Data
        private static string[] EventCommands = new string[]
        {
            "Begin action queue for character $00 (Actor in slot 0), {0}",			// 0x00
            "Begin action queue for character $01 (Actor in slot 1), {0}",			// 0x01
            "Begin action queue for character $02 (Actor in slot 2), {0}",			// 0x02
            "Begin action queue for character $03 (Actor in slot 3), {0}",			// 0x03
            "Begin action queue for character $04 (Actor in slot 4), {0}",			// 0x04
            "Begin action queue for character $05 (Actor in slot 5), {0}",			// 0x05
            "Begin action queue for character $06 (Actor in slot 6), {0}",			// 0x06
            "Begin action queue for character $07 (Actor in slot 7), {0}",			// 0x07
            "Begin action queue for character $08 (Actor in slot 8), {0}",			// 0x08
            "Begin action queue for character $09 (Actor in slot 9), {0}",			// 0x09
            "Begin action queue for character $0A (Actor in slot 10), {0}",			// 0x0A
            "Begin action queue for character $0B (Actor in slot 11), {0}",			// 0x0B
            "Begin action queue for character $0C (Actor in slot 12), {0}",			// 0x0C
            "Begin action queue for character $0D (Actor in slot 13), {0}",			// 0x0D
            "Begin action queue for character $0E (Actor in slot 14), {0}",			// 0x0E
            "Begin action queue for character $0F (Actor in slot 15), {0}",			// 0x0F
			
            "Begin action queue for character $10 (NPC 0), {0}",			// 0x10
            "Begin action queue for character $11 (NPC 1), {0}",			// 0x11
            "Begin action queue for character $12 (NPC 2), {0}",			// 0x12
            "Begin action queue for character $13 (NPC 3), {0}",			// 0x13
            "Begin action queue for character $14 (NPC 4), {0}",			// 0x14
            "Begin action queue for character $15 (NPC 5), {0}",			// 0x15
            "Begin action queue for character $16 (NPC 6), {0}",			// 0x16
            "Begin action queue for character $17 (NPC 7), {0}",			// 0x17
            "Begin action queue for character $18 (NPC 8), {0}",			// 0x18
            "Begin action queue for character $19 (NPC 9), {0}",			// 0x19
            "Begin action queue for character $1A (NPC 10), {0}",			// 0x1A
            "Begin action queue for character $1B (NPC 11), {0}",			// 0x1B
            "Begin action queue for character $1C (NPC 12), {0}",			// 0x1C
            "Begin action queue for character $1D (NPC 13), {0}",			// 0x1D
            "Begin action queue for character $1E (NPC 14), {0}",			// 0x1E
            "Begin action queue for character $1F (NPC 15), {0}",			// 0x1F
			
            "Begin action queue for character $20 (NPC 16), {0}",			// 0x20
            "Begin action queue for character $21 (NPC 17), {0}",			// 0x21
            "Begin action queue for character $22 (NPC 18), {0}",			// 0x22
            "Begin action queue for character $23 (NPC 19), {0}",			// 0x23
            "Begin action queue for character $24 (NPC 20), {0}",			// 0x24
            "Begin action queue for character $25 (NPC 21), {0}",			// 0x25
            "Begin action queue for character $26 (NPC 22), {0}",			// 0x26
            "Begin action queue for character $27 (NPC 23), {0}",			// 0x27
            "Begin action queue for character $28 (NPC 24), {0}",			// 0x28
            "Begin action queue for character $29 (NPC 25), {0}",			// 0x29
            "Begin action queue for character $2A (NPC 26), {0}",			// 0x2A
            "Begin action queue for character $2B (NPC 27), {0}",			// 0x2B
            "Begin action queue for character $2C (NPC 28), {0}",			// 0x2C
            "Begin action queue for character $2D (NPC 29), {0}",			// 0x2D
            "Begin action queue for character $2E (NPC 30), {0}",			// 0x2E
            "Begin action queue for character $2F (NPC 31), {0}",			// 0x2F
			
            "Begin action queue for character $30 (Camera), {0}",			// 0x30
            "Begin action queue for character $31 (Party Character 0), {0}",			// 0x31
            "Begin action queue for character $32 (Party Character 1), {0}",			// 0x32
            "Begin action queue for character $33 (Party Character 2), {0}",			// 0x33
            "Begin action queue for character $34 (Party Character 3), {0}",			// 0x34
            "Pause execution until action queue for object ${0} ({1}) (is complete)",			// 0x35
            "Disable ability to pass through other objects for object ${0} ({1})",			// 0x36
            "Assign graphics ${0} to object ${1} ({2})",			// 0x37
            "Hold screen",			// 0x38
            "Free screen",			// 0x39
            "Enable player to move while event commands execute",			// 0x3A
            "Position character in a \"ready-to-go\" stance",			// 0x3B
            "Set up the party as follows: $({0}), ({1}), ({2}), ({3})",			// 0x3C
            "Create object ${0}",			// 0x3D
            "Delete object ${0}",			// 0x3E
            "{0} character ${1} ({2}) {3}",			// 0x3F
			
            "Assign properties ${0} to character ${1} ({2})",			// 0x40
            "Show object ${0}",			// 0x41
            "Hide object ${0}",			// 0x42
            "Assign palette ${0} to character ${1} ({2})",			// 0x43
            "Place character ${0} ({1}) on vehicle ${2} ({3})",			// 0x44
            "Refresh objects",			// 0x45
            "Make party {0} the current party",			// 0x46
            "Make character in slot 0 the lead character",			// 0x47
            "Display dialogue message ${0}, continue executing commands ({1}) ({2}) \"{3}...\"",			// 0x48
            "If dialogue window is up, wait for keypress then dismiss",			// 0x49
            "",			// 0x4A
            "Display dialogue message ${0}, wait for button press ({1}) ({2}) \"{3}...\"",			// 0x4B
            "Center screen on party and invoke battle, enemy set ${0}, background ${1} ({2}), ({3})({4})",			// 0x4C
            "Invoke battle, enemy set ${0}, background ${1} ({2}), ({3})({4})",			// 0x4D
            "Invoke battle, random encounter as determined by zone",			// 0x4E
            "Exit the current location",			// 0x4F
			
            "Tint screen (cumulative) with color ${0}",			// 0x50
            "Modify background color range from [{0}, {1}]: {2} ({3}) at intensity {4}",			// 0x51
            "Tint characters (cumulative) with color ${0}",			// 0x52
            "Modify object color range from [{0}, {1}]: {2} ({3}) at intensity {4}",			// 0x53
            "End effects of commands for modifed color components and screen flashes",			// 0x54
            "Flash screen with color component(s) ${0} ({1}), at intensity {2}",			// 0x55
            "Increase color component(s) {0} ({1}), at intensity {2}",			// 0x56
            "Decrease color component(s) {0} ({1}), at intensity {2}",			// 0x57
            "Shake screen ${0}",			// 0x58
            "Unfade screen at speed ${0}",			// 0x59
            "Fade screen at speed ${0}",			// 0x5A
            "",			// 0x5B
            "Pause execution until fade in or fade out is complete",			// 0x5C
            "Scroll BG0, speed {0} x {1}",			// 0x5D
            "Scroll BG1, speed {0} x {1}",			// 0x5E
            "Scroll BG2, speed {0} x {1}",			// 0x5F
			
            "Change background layer ${0} to palette ${1}",			// 0x60
            "Colorize color range [${0}, ${1}] to color ${2}",			// 0x61
            "Mosaic screen, speed {0}",			// 0x62
            "Create spotlight effect with radius {0}",			// 0x63
            "{0}, ${1}",			// 0x64
            "{0}, ${1}",			// 0x65
            "",			// 0x66
            "",			// 0x67
            "",			// 0x68
            "",			// 0x69
            "Load map ${0} ({1}) {2}, (upper bits ${3}), place party at ({4}, {5}), facing {6}, {7}",			// 0x6A
            "Load map ${0} ({1}) {2}, (upper bits ${3}), place party at ({4}, {5}), facing {6}, {7}",			// 0x6B
            "Set parent map to ${0} ({1}), parent coordinates to ({2}, {3}), facing {4}",			// 0x6C
            "",			// 0x6D
            "",			// 0x6E
            "",			// 0x6F
			
            "Scroll BG0 (256 x {0} + {1})",			// 0x70
            "Scroll BG1 (256 x {0} + {1})",			// 0x71
            "Scroll BG2 (256 x {0} + {1})",			// 0x72
            "Replace current map's {0} at ({1}, {2}) with the following ({3} x {4}) chunk, {5}",			// 0x73
            "Replace current map's {0} at ({1}, {2}) with the following ({3} x {4}) chunk, {5}",			// 0x74
            "Refresh map after alteration",			// 0x75
            "",			// 0x76
            "Perform level averaging on character ${0} ({1}) and calculate new maximum HP/MP",			// 0x77
            "Enable ability to pass through other objects for object ${0} ({1})",			// 0x78
            "Place party {0} on map ${1} ({2})",			// 0x79
            "Change event address for object ${0} to address ${1}",			// 0x7A
            "Restore backup party (in $055D) to active status",			// 0x7B
            "Enable activation of event for object ${0} ({1}) if it comes into contact with any party",			// 0x7C
            "Disable activation of event for object ${0} ({1}) if it comes into contact with any party",			// 0x7D
            "Move characters to ({0}, {1})",			// 0x7E
            "Change character ${0}'s name to ${1} ({2})",			// 0x7F
			
            "Add item ${0} ({1}) to inventory",			// 0x80
            "Remove item ${0} ({1}) from inventory",			// 0x81
            "Store party 1 as backup party (in $055D)",			// 0x82
            "",			// 0x83
            "Give {0} GP to party",			// 0x84
            "Take {0} GP from party",			// 0x85
            "Give esper ${0} ({1}) to party",			// 0x86
            "Take esper ${0} ({1}) from party",			// 0x87
            "Remove the following status ailments from character ${0} ({1}): {2}",			// 0x88
            "Inflict the following status ailments on character ${0} ({1}): {2}",			// 0x89
            "Toggle the following status ailments for character ${0} ({1}): {2}",			// 0x8A
            "For character ${0} ({1}), take {2} and {3}",			// 0x8B
            "For character ${0} ({1}), take {2} and {3}",			// 0x8C
            "Remove all equipment from character ${0} ({1})",			// 0x8D
            "Invoke battle: enemy set obtained from treasure chest's monster-in-a-box, background is area's default",			// 0x8E
            "Unlock all of Cyan's SwdTechs",			// 0x8F
			
            "Grant Sabin the Bum Rush",			// 0x90
            "Pause for 15 units",			// 0x91
            "Pause for 30 units",			// 0x92
            "Pause for 45 units",			// 0x93
            "Pause for 60 units",			// 0x94
            "Pause for 120 units",			// 0x95
            "Restore screen from fade",			// 0x96
            "Fade screen to black",			// 0x97
            "Invoke name change screen for character ${0} ({1})",			// 0x98
            "Invoke party selection screen ({0} groups) (force characters: [{1}]{2})",			// 0x99
            "Invoke Colosseum item selection screen",			// 0x9A
            "Invoke shop ${0}",			// 0x9B
            "Place optimum equipment on character ${0} ({1})",			// 0x9C
            "Invoke party fighting order screen (from final battle)",			// 0x9D
            "",			// 0x9E
            "",			// 0x9F
			
            "Set timer {0} to ${1} [{2}m: {3}s: {4}j] (flags ${5}), jump to subroutine ${6}",			// 0xA0
            "Reset timer {0}",			// 0xA1
            "",			// 0xA2
            "",			// 0xA3
            "",			// 0xA4
            "",			// 0xA5
            "Delete any rotating pyramids",			// 0xA6
            "Create a rotating pyramid around character ${0} ({1})",			// 0xA7
            "Show floating island soaring into the sky",			// 0xA8
            "Show title screen",			// 0xA9
            "Show intro with Magitek Armor walking through snowfields",			// 0xAA
            "Invoke game loading screen",			// 0xAB
            "",			// 0xAC
            "Show world getting torn apart",			// 0xAD
            "Show train car ride out of Magitek Factory",			// 0xAE
            "Invoke random Colosseum battle",			// 0xAF
			
            "Execute the following commands until $B1, {0} times",			// 0xB0
            "End block of repeating commands",			// 0xB1
            "Call subroutine ${0}",			// 0xB2
            "Call subroutine ${0}, {1}",			// 0xB3
            "Pause for {0} units",			// 0xB4
            "Pause for 15 * {0} ({1}) units",			// 0xB5
            "Indexed branch based on prior dialogue selection [{0}]",			// 0xB6
            "If bit $1DC9(${0}) [${1}, bit {2}] is clear, branch to ${3}",			// 0xB7
            "Set bit $1DC9(${0}) [${1}, bit {2}]",			// 0xB8
            "Clear bit $1DC9(${0}) [${1}, bit {2}]",			// 0xB9
            "Play ending cinematic ${0}",			// 0xBA
            "",			// 0xBB
            "If bit $1E80(${0}) [${1}, bit {2}] {3}, return",			// 0xBC
            "Pseudo-randomly jump to ${0} 50% of the time",			// 0xBD
            "{0}",			// 0xBE
            "Show airship scene from ending",			// 0xBF
			
            "If ($1E80(${0}) [${1}, bit {2}] {3}){4}, branch to ${5}",			// 0xC0
            "If ($1E80(${0}) [${1}, bit {2}] {3}){4}, branch to ${5}",			// 0xC1
            "If ($1E80(${0}) [${1}, bit {2}] {3}){4}, branch to ${5}",			// 0xC2
            "If ($1E80(${0}) [${1}, bit {2}] {3}){4}, branch to ${5}",			// 0xC3
            "If ($1E80(${0}) [${1}, bit {2}] {3}){4}, branch to ${5}",			// 0xC4
            "If ($1E80(${0}) [${1}, bit {2}] {3}){4}, branch to ${5}",			// 0xC5
            "If ($1E80(${0}) [${1}, bit {2}] {3}){4}, branch to ${5}",			// 0xC6
            "If ($1E80(${0}) [${1}, bit {2}] {3}){4}, branch to ${5}",			// 0xC7
            "If ($1E80(${0}) [${1}, bit {2}] {3}){4}, branch to ${5}",			// 0xC8
            "If ($1E80(${0}) [${1}, bit {2}] {3}){4}, branch to ${5}",			// 0xC9
            "If ($1E80(${0}) [${1}, bit {2}] {3}){4}, branch to ${5}",			// 0xCA
            "If ($1E80(${0}) [${1}, bit {2}] {3}){4}, branch to ${5}",			// 0xCB
            "If ($1E80(${0}) [${1}, bit {2}] {3}){4}, branch to ${5}",			// 0xCC
            "If ($1E80(${0}) [${1}, bit {2}] {3}){4}, branch to ${5}",			// 0xCD
            "If ($1E80(${0}) [${1}, bit {2}] {3}){4}, branch to ${5}",			// 0xCE
            "If ($1E80(${0}) [${1}, bit {2}] {3}){4}, branch to ${5}",			// 0xCF
			
            "Set event bit $1E80($0{0}) [${1}, bit {2}]",			// 0xD0
            "Clear event bit $1E80($0{0}) [${1}, bit {2}]",			// 0xD1
            "Set event bit $1E80($1{0}) [${1}, bit {2}]",			// 0xD2
            "Clear event bit $1E80($1{0}) [${1}, bit {2}]",			// 0xD3
            "Set event bit $1E80($2{0}) [${1}, bit {2}]",			// 0xD4
            "Clear event bit $1E80($2{0}) [${1}, bit {2}]",			// 0xD5
            "Set event bit $1E80($3{0}) [${1}, bit {2}]",			// 0xD6
            "Clear event bit $1E80($3{0}) [${1}, bit {2}]",			// 0xD7
            "Set event bit $1E80($4{0}) [${1}, bit {2}]",			// 0xD8
            "Clear event bit $1E80($4{0}) [${1}, bit {2}]",			// 0xD9
            "Set event bit $1E80($5{0}) [${1}, bit {2}]",			// 0xDA
            "Clear event bit $1E80($5{0}) [${1}, bit {2}]",			// 0xDB
            "Set event bit $1E80($6{0}) [${1}, bit {2}]",			// 0xDC
            "Clear event bit $1E80($6{0}) [${1}, bit {2}]",			// 0xDD
            "Load CaseWord with the characters in the currently active party?",			// 0xDE
            "Load CaseWord with the characters who have been created?",			// 0xDF
			
            "Load CaseWord with characters encountered so far?",			// 0xE0
            "Load CaseWord with characters who are collected (excludes Veldt-jumped Gau)",			// 0xE1
            "Set bit in CaseWord for the lead character in the current party",			// 0xE2
            "Load CaseWord with available characters?",			// 0xE3
            "Set CaseWord bit corresponding to the number of the currently active party",			// 0xE4
            "",			// 0xE5
            "",			// 0xE6
            "Show portrait for character ${0} ({1})",			// 0xE7
            "Set word $1FC2(${0}) [${1}] to ${2}",			// 0xE8
            "Increment word $1FC2(${0}) [${1}] by ${2}",			// 0xE9
            "Decrement word $1FC2(${0}) [${1}] by ${2}",			// 0xEA
            "Compare word $1FC2(${0}) [${1}] to ${2}, set $1A0 for equal to, $1A1 for greater than, $1A2 for less than",			// 0xEB
            "",			// 0xEC
            "",			// 0xED
            "",			// 0xEE
            "Play song {0} ({1}), (high bit {2}) {3}",			// 0xEF
			
            "Play song {0} ({1}), (high bit {2}) {3}",			// 0xF0
            "Fade in song {0} ({1}), (high bit {2}) {3}",			// 0xF1
            "Fade out current song with transition time {0}",			// 0xF2
            "Fade in previously faded out song with transition time {0}",			// 0xF3
            "Play sound effect {0}",			// 0xF4
            "Play sound effect {0}, with transition time {1} and speaker balance ${2} ($80 is center, values increase left to right)",			// 0xF5
            "Subcommand ${0}: {1}",			// 0xF6
            "End most recent loop of currently playing song",			// 0xF7
            "",			// 0xF8
            "Pause execution until the music passes through predetermined point ${0}",			// 0xF9
            "Stop temporarily played song",			// 0xFA
            "Apply a special effect (echo?) to the currently playing sound effect",			// 0xFB
            "Manual script change to {0}",			// 0xFC
            "No operation",			// 0xFD
            "Return",			// 0xFE
            "End script"			// 0xFF
        };
        private static string[] ButtonNames = new string[] { "left", "right", "down", "up", "X", "A", "Y", "B" };
        private static string[] BitNames = new string[] { "0", "1", "2", "3", "4", "5", "6", "7" };
        private static string[] LayerNames = new string[] { "L1", "L2", "L3", "L4", "Sprites", "BG", "½ intensity", "Minus sub" };
        #endregion
        public string InterpretCommand(EventCommand esc)
        {
            if (esc.NonEmbeddedVehicle)
                return "NON-EMBEDDED VEHICLE SCRIPT";
            if (esc.NonEmbeddedMap)
                return "NON-EMBEDDED MAP SCRIPT";
            //
            string[] vars = new string[16];
            switch (esc.Opcode)
            {
                case 0x35:
                case 0x36:
                    vars[0] = esc.Param1.ToString("X2");
                    vars[1] = GetObjectName(esc.Param1);
                    break;
                case 0x37:
                    vars[0] = esc.Param2.ToString("X2");
                    vars[1] = esc.Param1.ToString("X2");
                    vars[2] = GetObjectName(esc.Param1);
                    break;
                case 0x3C:
                    vars[0] = GetObjectName(esc.Param1);
                    vars[1] = GetObjectName(esc.Param2);
                    vars[2] = GetObjectName(esc.Param3);
                    vars[3] = GetObjectName(esc.Param4);
                    break;
                case 0x3D:
                case 0x3E:
                    vars[0] = esc.Param1.ToString("X2");
                    break;
                case 0x3F:
                    vars[0] = esc.Param2 != 0 ? "Assign" : "Remove";
                    vars[1] = esc.Param1.ToString("X2");
                    vars[2] = GetObjectName(esc.Param1);
                    if (esc.Param2 != 0)
                        vars[3] = "to party " + esc.Param2.ToString();
                    else
                        vars[3] = "from party";
                    break;
                case 0x40:
                    vars[0] = esc.Param2.ToString("X2");
                    vars[1] = esc.Param1.ToString("X2");
                    vars[2] = GetObjectName(esc.Param1);
                    break;
                case 0x41:
                case 0x42:
                    goto case 0x3E;
                case 0x43:
                    goto case 0x40;
                case 0x44:
                    vars[0] = esc.Param1.ToString("X2");
                    vars[1] = GetObjectName(esc.Param1);
                    vars[2] = esc.Param2.ToString("X2");
                    vars[3] = GetVehicleName(esc.Param2);
                    break;
                case 0x46:
                    vars[0] = esc.Param1.ToString();
                    break;
                case 0x48:
                    int temp = Bits.GetShort(esc.CommandData, 1);
                    vars[0] = (temp & 0x3FFF).ToString("X4");
                    vars[1] = (temp & 0x4000) == 0x4000 ? "Show text only" : "Show text with window";
                    vars[2] = (temp & 0x8000) == 0x8000 ? "At bottom of screen" : "At top of screen";
                    string stub = Model.Dialogues[temp & 0x3FFF].GetDialogue();
                    vars[3] = stub.Substring(0, Math.Min(30, stub.Length));
                    break;
                case 0x4B:
                    goto case 0x48;
                case 0x4C:
                case 0x4D:
                    vars[0] = esc.Param1.ToString("X2");
                    vars[1] = (esc.Param2 & 0x3F).ToString("X2");
                    vars[2] = Lists.BattlefieldNames[esc.Param2 & 0x3F];
                    vars[3] = ((esc.Param2 & 0x80) == 0x80) ? "mosaic effect disabled" : "mosaic effect enabled";
                    vars[4] = ((esc.Param2 & 0x40) == 0x80) ? "swoosh sound disabled" : "swoosh sound enabled";
                    break;
                case 0x50:
                    goto case 0x3E;
                case 0x51:
                    vars[0] = esc.Param2.ToString("X2");
                    vars[1] = esc.Param3.ToString("X2");
                    if ((esc.Param1 & 0xE0) == 0x20)
                        vars[2] = "Add";
                    else if ((esc.Param1 & 0xE0) == 0xA0)
                        vars[2] = "Subtract";
                    else
                        vars[2] = "Do <unknown operation>";
                    vars[3] = GetColorName(esc.Param1);
                    vars[4] = (esc.Param1 & 3).ToString();
                    break;
                case 0x52:
                    goto case 0x3E;
                case 0x53:
                    goto case 0x51;
                case 0x55:
                case 0x56:
                case 0x57:
                    int x = (esc.Param1 & 0xF0) >> 4;
                    int y = (esc.Param1 & 0x0F);
                    vars[0] = x.ToString();
                    vars[1] = Lists.ColorNames[x];
                    vars[2] = y.ToString();
                    break;
                case 0x58:
                case 0x59:
                case 0x5A:
                    goto case 0x3E;
                case 0x5D:
                case 0x5E:
                case 0x5F:
                    vars[0] = esc.Param1.ToString();
                    vars[1] = esc.Param2.ToString();
                    break;
                case 0x60:
                    vars[0] = esc.Param1.ToString("X2");
                    vars[1] = esc.Param2.ToString("X2");
                    break;
                case 0x61:
                    vars[0] = esc.Param2.ToString("X2");
                    vars[1] = esc.Param3.ToString("X2");
                    vars[2] = esc.Param1.ToString("X2");
                    break;
                case 0x62:
                case 0x63:
                    vars[0] = esc.Param1.ToString();
                    break;
                case 0x64:
                case 0x65:
                    vars[0] = esc.Param1.ToString("X2");
                    vars[1] = esc.Param2.ToString("X2");
                    break;
                case 0x6A:
                case 0x6B:
                    ushort map = Bits.GetShort(esc.CommandData, 1);
                    vars[0] = (map & 0x01FF).ToString("X4");
                    vars[1] = GetLocationName(map);
                    vars[2] = esc.Opcode == 0x6A ? "after fade out" : "instantly";
                    vars[3] = (map & 0xFE00).ToString("X4");
                    vars[4] = esc.Param3.ToString();
                    vars[5] = esc.Param4.ToString();
                    if ((map & 0x3000) == 0x3000) vars[6] = "left";
                    else if ((map & 0x3000) == 0x2000) vars[6] = "down";
                    else if ((map & 0x3000) == 0x1000) vars[6] = "right";
                    else if ((map & 0x3000) == 0x0000) vars[6] = "up";
                    if ((esc.Param5 & 0x03) == 0x00)
                        vars[7] = "none";
                    if ((esc.Param5 & 0x01) == 0x01)
                        vars[7] = "party is in the airship";
                    else if ((esc.Param5 & 0x02) == 0x02)
                        vars[7] = "party is in overworld chocobo mode";
                    else
                        vars[7] = "flags $" + esc.Param5.ToString("X2");
                    break;
                case 0x6C:
                    ushort parent = Bits.GetShort(esc.CommandData, 1);
                    vars[0] = parent.ToString("X4");
                    vars[1] = GetLocationName(parent);
                    vars[2] = esc.Param3.ToString();
                    vars[3] = esc.Param4.ToString();
                    if (esc.Param5 == 3) vars[4] = "right";
                    else if (esc.Param5 == 2) vars[4] = "up";
                    else if (esc.Param5 == 1) vars[4] = "left";
                    else if (esc.Param5 == 0) vars[4] = "down";
                    break;
                case 0x70:
                case 0x71:
                case 0x72:
                    vars[0] = esc.Param1.ToString();
                    vars[1] = esc.Param2.ToString();
                    break;
                case 0x73:
                case 0x74:
                    if ((esc.Param2 & 0x80) == 0x80)
                        vars[0] = "layer 3";
                    else if ((esc.Param2 & 0x40) == 0x40)
                        vars[0] = "layer 2";
                    else
                        vars[0] = "layer 1";
                    vars[1] = esc.Param1.ToString();
                    vars[2] = (esc.Param2 & 0x3F).ToString();
                    vars[3] = esc.Param4.ToString();
                    vars[4] = esc.Param3.ToString();
                    if (esc.Opcode == 0x73)
                        vars[5] = "refresh immediately";
                    else if (esc.Opcode == 0x74)
                        vars[5] = "wait for opcode $75 to refresh";
                    else
                        vars[5] = "error-contact Imzogelmo";
                    break;
                case 0x77:
                    vars[0] = esc.Param1.ToString("X2");
                    vars[1] = Model.CharacterNames.GetUnsortedName(esc.Param1);
                    break;
                case 0x78:
                    vars[0] = esc.Param1.ToString("X2");
                    vars[1] = GetObjectName(esc.Param1);
                    break;
                case 0x79:
                    ushort party = Bits.GetShort(esc.CommandData, 2);
                    vars[0] = esc.Param1.ToString();
                    vars[1] = party.ToString("X4");
                    vars[2] = GetLocationName(party);
                    break;
                case 0x7A:
                    vars[0] = esc.Param1.ToString("X2");
                    vars[1] = Bits.GetInt24(esc.CommandData, 2).ToString("X6");
                    break;
                case 0x7C:
                case 0x7D:
                    vars[0] = esc.Param1.ToString("X2");
                    vars[1] = GetObjectName(esc.Param1);
                    break;
                case 0x7E:
                    vars[0] = esc.Param1.ToString();
                    vars[1] = esc.Param2.ToString();
                    break;
                case 0x7F:
                    vars[0] = esc.Param1.ToString("X2");
                    vars[1] = esc.Param2.ToString("X2");
                    vars[2] = Model.CharacterNames.GetUnsortedName(esc.Param2);
                    break;
                case 0x80:
                case 0x81:
                    vars[0] = esc.Param1.ToString("X2");
                    vars[1] = Model.ItemNames.GetUnsortedName(esc.Param1);
                    break;
                case 0x84:
                case 0x85:
                    vars[0] = Bits.GetShort(esc.CommandData, 1).ToString();
                    break;
                case 0x86:
                case 0x87:
                    vars[0] = esc.Param1.ToString("X2");
                    if (esc.Param1 >= 0x36 && esc.Param1 <= 0x50)
                        vars[1] = Model.EsperNames.GetUnsortedName(esc.Param1 - 0x36);
                    else
                        vars[1] = "<Invalid Esper>";
                    break;
                case 0x88:
                case 0x89:
                case 0x8A:
                    vars[0] = esc.Param1.ToString("X2");
                    vars[1] = GetObjectName(esc.Param1);
                    int status = Bits.GetShort(esc.CommandData, 2) ^ 0xFFFF;
                    x = 15;
                    vars[2] = "";
                    do
                    {
                        if (status >= (1 << x))
                        {
                            status -= (1 << x);
                            vars[2] += Lists.StatusNames[x];
                            if (status != 0)
                                vars[2] += ", ";
                        }
                        x--;
                    }
                    while (status != 0);
                    break;
                case 0x8B:
                case 0x8C:
                    vars[0] = esc.Param1.ToString("X2");
                    vars[1] = GetObjectName(esc.Param1);
                    vars[2] = esc.Opcode == 0x8B ? "HP" : "MP";
                    if (esc.Param2 == 0x7F)
                        vars[3] = "set to maximum";
                    else if ((esc.Param2 & 0x80) == 0x80)
                        vars[3] = "subtract " + (1 << (esc.Param2 & 0x7F)) + " (set to 1 if that would put HP below 0)";
                    else
                        vars[3] = "add " + (1 << (esc.Param2 & 0x7F));
                    break;
                case 0x8D:
                    goto case 0x36;
                case 0x98:
                    goto case 0x36;
                case 0x99:
                    int characters = Bits.GetShort(esc.CommandData, 2);
                    vars[0] = esc.Param1.ToString();
                    vars[1] = characters.ToString("X4");
                    x = 0;
                    vars[2] = "";
                    do
                    {
                        if ((characters & (1 << x)) != 0)
                        {
                            vars[2] += " (" + Model.CharacterNames.GetUnsortedName(x) + ")";
                            characters ^= (ushort)(1 << x);
                            if (characters != 0)
                                vars[2] += ",";
                        }
                        x++;
                    }
                    while ((x < 16) && (characters != 0));
                    break;
                case 0x9B:
                    goto case 0x3E;
                case 0x9C:
                    goto case 0x36;
                case 0xA0:
                    int ticks = Bits.GetShort(esc.CommandData, 1);
                    int timer = Bits.GetInt24(esc.CommandData, 3);
                    vars[0] = ((timer & 0x0C0000) >> 18).ToString();
                    vars[1] = ticks.ToString("X4");
                    vars[2] = (ticks / 3600).ToString();
                    vars[3] = ((ticks % 3600) / 60).ToString("d2");
                    vars[4] = (ticks % 60).ToString();
                    vars[5] = ((timer & 0xF00000) >> 20).ToString("X1");
                    vars[6] = ((timer & 0x3FFFF) + 0xCA0000).ToString("X6");
                    break;
                case 0xA1:
                    vars[0] = esc.Param1.ToString();
                    break;
                case 0xA7:
                    goto case 0x36;
                case 0xB0:
                    vars[0] = esc.Param1.ToString();
                    break;
                case 0xB2:
                    int subroutine = Bits.GetInt24(esc.CommandData, 1);
                    vars[0] = (subroutine + 0xCA0000).ToString("X6");
                    if (subroutine == 0x00CFBD)
                        vars[0] = " (heals all HP/MP/Statuses except M-Tek & Dog Block)";
                    else if (subroutine == 0x02E499)
                        vars[0] = " (heals all HP/MP/Statuses except Dog Block)";
                    else if (subroutine == 0x012DFA)
                        vars[0] = " (clears general purpose event bits)";
                    break;
                case 0xB3:
                    subroutine = Bits.GetInt24(esc.CommandData, 2);
                    vars[0] = (subroutine + 0xCA0000).ToString("X6");
                    vars[1] = esc.Param1.ToString() + " times";
                    if (subroutine == 0x00CFBD)
                        vars[1] += " (heals all HP/MP/Statuses except M-Tek & Dog Block)";
                    else if (subroutine == 0x02E499)
                        vars[1] += " (heals all HP/MP/Statuses except Dog Block)";
                    else if (subroutine == 0x012DFA)
                        vars[1] += " (clears general purpose event bits)";
                    break;
                case 0xB4:
                    vars[0] = esc.Param1.ToString();
                    break;
                case 0xB5:
                    vars[0] = esc.Param1.ToString();
                    vars[1] = (esc.Param1 * 15).ToString();
                    break;
                case 0xB6:
                    vars[0] = "";
                    int counter = 0;
                    while (counter < esc.Register)
                    {
                        int pointer = Bits.GetInt24(esc.CommandData, (counter * 3) + 1);
                        vars[0] += "$" + (pointer + 0xCA0000).ToString("X6") + ", ";
                        counter++;
                    }
                    if (vars[0].Length >= 2)
                        vars[0] = vars[0].Remove(vars[0].Length - 2, 2);
                    break;
                case 0xB7:
                    int offset = Bits.GetInt24(esc.CommandData, 2);
                    vars[0] = esc.Param1.ToString("X2");
                    vars[1] = (0x1DC9 + (esc.Param1 / 8)).ToString("X4");
                    vars[2] = (esc.Param1 & 7).ToString();
                    vars[3] = (offset + 0xCA0000).ToString("X6");
                    break;
                case 0xB8:
                case 0xB9:
                    vars[0] = esc.Param1.ToString("X2");
                    vars[1] = (0x1DC9 + (esc.Param1 / 8)).ToString("X4");
                    vars[2] = (esc.Param1 & 7).ToString();
                    break;
                case 0xBA:
                    goto case 0x3E;
                case 0xBC:
                    int memory = Bits.GetShort(esc.CommandData, 1);
                    vars[0] = (memory & 0x7FFF).ToString("X3");
                    vars[1] = ((memory & 0x7FFF) / 8 + 0x1E80).ToString("X4");
                    vars[2] = (memory & 7).ToString();
                    vars[3] = ((memory & 0x8000) == 0x8000 ? "is set" : "is clear");
                    break;
                case 0xBD:
                    vars[0] = (Bits.GetInt24(esc.CommandData, 1) + 0xCA0000).ToString("X6");
                    break;
                case 0xBE:
                    counter = 0;
                    vars[0] = "";
                    while (counter < (esc.Param1 & 0x7F))
                    {
                        int value = Bits.GetInt24(esc.CommandData, (counter * 3) + 2);
                        vars[0] += "If $" + ((value & 0xF00000) >> 20).ToString("X2") + " is in the current CaseWord, ";
                        vars[0] += "call subroutine $" + ((value & 0x0FFFFF) + 0xCA0000).ToString("X6") + "; else ";
                        counter++;
                    }
                    if (vars[0].Length >= 6)
                        vars[0] = vars[0].Remove(vars[0].Length - 6, 6);
                    if ((esc.Param1 & 0x80) == 0x80)
                        vars[0] += " (0x80 is set)";
                    break;
                case 0xC0:
                case 0xC1:
                case 0xC2:
                case 0xC3:
                case 0xC4:
                case 0xC5:
                case 0xC6:
                case 0xC7:
                    memory = Bits.GetShort(esc.CommandData, 1);
                    vars[0] = (memory & 0x7FFF).ToString("X3");
                    vars[1] = (((memory & 0x7FFF) / 8) + 0x1E80).ToString("X4");
                    vars[2] = (memory & 7).ToString();
                    vars[3] = ((memory & 0x8000) == 0x8000 ? "is set" : "is clear");
                    temp = esc.Opcode & 7;
                    vars[4] = "";
                    if (temp >= 1) AppendMemoryCheck(esc, 3, ref vars[4], (esc.Opcode & 8) == 8);
                    if (temp >= 2) AppendMemoryCheck(esc, 5, ref vars[4], (esc.Opcode & 8) == 8);
                    if (temp >= 3) AppendMemoryCheck(esc, 7, ref vars[4], (esc.Opcode & 8) == 8);
                    if (temp >= 4) AppendMemoryCheck(esc, 9, ref vars[4], (esc.Opcode & 8) == 8);
                    if (temp >= 5) AppendMemoryCheck(esc, 11, ref vars[4], (esc.Opcode & 8) == 8);
                    if (temp >= 6) AppendMemoryCheck(esc, 13, ref vars[4], (esc.Opcode & 8) == 8);
                    if (temp >= 7) AppendMemoryCheck(esc, 15, ref vars[4], (esc.Opcode & 8) == 8);
                    //
                    offset = Bits.GetInt24(esc.CommandData, ((esc.Opcode & 7) * 2) + 3);
                    vars[5] = " " + (offset + 0xCA0000).ToString("X6");
                    if (offset == 0x005EB3)
                        vars[5] += " (simply returns)";
                    break;
                case 0xC8:
                case 0xC9:
                case 0xCA:
                case 0xCB:
                case 0xCC:
                case 0xCD:
                case 0xCE:
                case 0xCF:
                    goto case 0xC7;
                case 0xD0:
                case 0xD1:
                case 0xD2:
                case 0xD3:
                case 0xD4:
                case 0xD5:
                case 0xD6:
                case 0xD7:
                case 0xD8:
                case 0xD9:
                case 0xDA:
                case 0xDB:
                case 0xDC:
                case 0xDD:
                    vars[0] = esc.Param1.ToString("X2");
                    vars[1] = (0x1E80 + (esc.Param1 / 8)).ToString("X4");
                    vars[2] = (esc.Param1 & 7).ToString();
                    break;
                case 0xE7:
                    goto case 0x36;
                case 0xE8:
                case 0xE9:
                case 0xEA:
                    vars[0] = esc.Param1.ToString("X2");
                    vars[1] = (0x1FC2 + (esc.Param1 * 2)).ToString("X4");
                    vars[2] = Bits.GetShort(esc.CommandData, 2).ToString("X4");
                    break;
                case 0xEB:
                    goto case 0xE8;
                case 0xEF:
                case 0xF0:
                case 0xF1:
                    vars[0] = (esc.Param1 & 0x7F).ToString();
                    if ((esc.Param1 & 0x7F) <= 84)
                        vars[1] = Lists.MusicNames[esc.Param1 & 0x7F];
                    else
                        vars[1] = "<invalid song>";
                    vars[2] = ((esc.Param1 & 0x80) == 0x80 ? "set" : "clear");
                    if (esc.Opcode == 0xEF)
                        vars[3] = "volume " + ((float)esc.Param2 / 255.0).ToString("f2") + "%";
                    else if (esc.Opcode == 0xF0)
                        vars[3] = "full volume";
                    else
                        vars[3] = "with transition time " + esc.Param2;
                    break;
                case 0xF2:
                case 0xF3:
                case 0xF4:
                    vars[0] = esc.Param1.ToString();
                    break;
                case 0xF5:
                    vars[0] = esc.Param1.ToString();
                    vars[1] = esc.Param2.ToString();
                    vars[2] = esc.Param3.ToString("X2");
                    break;
                case 0xF6:
                    vars[0] = esc.Param1.ToString("X2");
                    string[] varsF6 = new string[16];
                    string commandF6 = "";
                    switch (esc.Param1)
                    {
                        case 0x10:
                        case 0x11:
                            commandF6 = "Play song ${0} ({1}) at volume ${2} ({3})";
                            varsF6[0] = esc.Param2.ToString("X2");
                            varsF6[1] = Lists.MusicNames[esc.Param2 & 0x7F];
                            varsF6[2] = esc.Param3.ToString("X2");
                            varsF6[3] = (esc.Param1 == 0x10 ? "may have other effects" : "with unknown other effects");
                            break;
                        case 0x80:
                            commandF6 = "Set volume of currently playing song and sound effects to ${0}, transition time {1}";
                            varsF6[0] = esc.Param3.ToString("X2");
                            varsF6[1] = esc.Param2.ToString();
                            break;
                        case 0x81:
                            commandF6 = "Change volume of currently playing song to ${0}, transition time {1}";
                            varsF6[0] = esc.Param3.ToString("X2");
                            varsF6[1] = esc.Param2.ToString();
                            break;
                        case 0x82:
                            commandF6 = "Change volume of currently playing sound effect to ${0}, transition time {1}";
                            varsF6[0] = esc.Param3.ToString("X2");
                            varsF6[1] = esc.Param2.ToString();
                            break;
                        case 0x83:
                            commandF6 = "Change pan control of currently playing sound effect to ${0}, transition time {1} ($80 is center, values increase left to right)";
                            varsF6[0] = esc.Param3.ToString("X2");
                            varsF6[1] = esc.Param2.ToString();
                            break;
                        case 0x84:
                            commandF6 = "Change tempo of currently playing song by {0}, transition time {1}";
                            varsF6[0] = esc.Param3.ToString("d3");
                            varsF6[1] = esc.Param2.ToString();
                            break;
                        case 0x85:
                            commandF6 = "Change pitch of currently playing song by {0}, transition time {1}";
                            varsF6[0] = esc.Param3.ToString("d3");
                            varsF6[1] = esc.Param2.ToString();
                            break;
                        case 0xF1:
                            commandF6 = "Stop currently playing song, unused bytes ${0}, {1}";
                            varsF6[0] = esc.Param3.ToString("X2");
                            varsF6[1] = esc.Param2.ToString("X2");
                            break;
                        case 0xF2:
                            commandF6 = "Stop currently playing sound effect, unused bytes ${0}, {1}";
                            varsF6[0] = esc.Param3.ToString("X2");
                            varsF6[1] = esc.Param2.ToString("X2");
                            break;
                        default:
                            commandF6 = "unknown bytes ${0}, {1}";
                            varsF6[0] = esc.Param3.ToString("X2");
                            varsF6[1] = esc.Param2.ToString("X2");
                            break;
                    }
                    if (commandF6 == "")
                        commandF6 = "{{" + BitConverter.ToString(esc.CommandData) + "}}";
                    vars[1] = string.Format(commandF6, varsF6);
                    break;
                case 0xF9:
                    goto case 0x3E;
                case 0xFC:
                    if (esc.Param1 == 0x00)
                        vars[0] = "Event Script";
                    else if (esc.Param1 == 0x01)
                        vars[0] = "Character Script";
                    else if (esc.Param1 == 0x02)
                        vars[0] = "Vehicle Script";
                    else if (esc.Param1 == 0x03)
                        vars[0] = "Map Script";
                    break;
                default:
                    if (esc.Opcode <= 0x34)
                    {
                        vars[0] = (esc.Param1 & 0x7F).ToString() + " bytes long";
                        vars[0] += (esc.Param1 & 0x80) == 0x80 ? " (Wait until complete)" : "";
                    }
                    else if (EventCommands[esc.Opcode] == "")
                        vars[0] = BitConverter.ToString(esc.CommandData);
                    break;
            }
            string command = EventCommands[esc.Opcode];
            if (command == "")
                command = "{{" + BitConverter.ToString(esc.CommandData) + "}}";
            return string.Format(command, vars);
        }
        private string TrimTailSpaces(string src)
        {
            int lastIndex = src.Length - 1;
            while (src[lastIndex] == ' ' || src[lastIndex] == '\0')
                lastIndex--;
            return src.Substring(0, lastIndex + 1);
        }
        private string GetObjectName(byte value)
        {
            if (value >= 0 && value <= 0x0D)
                return Model.CharacterNames.GetUnsortedName(value);
            else if (value >= 0x0E && value <= 0x0F)
                return "Actor in slot " + value;
            else if (value >= 0x10 && value <= 0x2F)
                return "NPC " + (value - 0x10).ToString();
            else if (value == 0x30)
                return "Camera";
            else if (value >= 0x31 && value <= 0x34)
                return "Party Character " + (value - 0x31).ToString();
            else if (value == 0xFF)
                return "<EMPTY>";
            else
                return "<Invalid Character>";
        }
        private string GetVehicleName(byte value)
        {
            string temp = "";
            if ((value & 0x7F) == 0x00)
                temp = "No vehicle";
            else if ((value & 0x7F) == 0x20)
                temp = "Chocobo";
            else if ((value & 0x7F) == 0x40)
                temp = "Magitek Armor";
            else if ((value & 0x7F) == 0x60)
                temp = "Raft";
            if ((value & 0x80) == 0x80)
                temp += " (Character is shown)";
            else
                temp += " (Character is not shown)";
            return temp;
        }
        private string GetColorName(byte value)
        {
            byte x = (byte)((value & 0x1C) >> 1);
            byte y = 0;
            if ((x & 0x80) == 0x80) // red
                y |= 0x02;
            if ((x & 0x40) == 0x40) // green
                y |= 0x04;
            if ((x & 0x20) == 0x20) // blue
                y |= 0x08;
            return Lists.ColorNames[y];
        }
        private string GetLocationName(ushort value)
        {
            if ((value & 0x01FF) <= 0x019E)
                return Lists.LocationNames[(value & 0x01FF)];
            else if ((value & 0x01FF) == 0x01FE)
                return "previous world map, skipping reposition function";
            else if ((value & 0x01FF) == 0x01FF)
                return "world map";
            else
                return "<unknown or out-of-range map index>";
        }
        private void AppendMemoryCheck(EventCommand esc, int offset, ref string command, bool and)
        {
            string[] vars = new string[16];
            string check = "{0} ($1E80(${1}) [${2}, bit {3}] {4})";
            //
            vars[0] = (and ? " and" : " or");
            int memory = Bits.GetShort(esc.CommandData, offset);
            vars[1] = (memory & 0x7FFF).ToString("X3");
            vars[2] = (((memory & 0x7FFF) / 8) + 0x1E80).ToString("X4");
            vars[3] = (memory & 7).ToString();
            vars[4] = ((memory & 0x8000) == 0x8000 ? "is set" : "is clear");
            //
            command += string.Format(check, vars);
        }
    }
}