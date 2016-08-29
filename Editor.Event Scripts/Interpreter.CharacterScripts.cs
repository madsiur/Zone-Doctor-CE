using System;
using System.Collections.Generic;
using System.Text;

namespace ZONEDOCTOR.ScriptsEditor.Commands
{
    public partial class Interpreter
    {
        #region Static Data
        private static string[] CharacterCommands = new string[]
        {
            "","","","","","","","","","","","","","","","",			// 0x00
            "","","","","","","","","","","","","","","","",			// 0x10
            "","","","","","","","","","","","","","","","",			// 0x20
            "","","","","","","","","","","","","","","","",			// 0x30
            "","","","","","","","","","","","","","","","",			// 0x40
            "","","","","","","","","","","","","","","","",			// 0x50
            "","","","","","","","","","","","","","","","",			// 0x60
            "","","","","","","","","","","","","","","","",			// 0x70
            			            			
            "Move up 1 tile",			// 0x80
            "Move right 1 tile",			// 0x81
            "Move down 1 tile",			// 0x82
            "Move left 1 tile",			// 0x83
            "Move up 2 tiles",			// 0x84
            "Move right 2 tiles",			// 0x85
            "Move down 2 tiles",			// 0x86
            "Move left 2 tiles",			// 0x87
            "Move up 3 tiles",			// 0x88
            "Move right 3 tiles",			// 0x89
            "Move down 3 tiles",			// 0x8A
            "Move left 3 tiles",			// 0x8B
            "Move up 4 tiles",			// 0x8C
            "Move right 4 tiles",			// 0x8D
            "Move down 4 tiles",			// 0x8E
            "Move left 4 tiles",			// 0x8F
            			
            "Move up 5 tiles",			// 0x90
            "Move right 5 tiles",			// 0x91
            "Move down 5 tiles",			// 0x92
            "Move left 5 tiles",			// 0x93
            "Move up 6 tiles",			// 0x94
            "Move right 6 tiles",			// 0x95
            "Move down 6 tiles",			// 0x96
            "Move left 6 tiles",			// 0x97
            "Move up 7 tiles",			// 0x98
            "Move right 7 tiles",			// 0x99
            "Move down 7 tiles",			// 0x9A
            "Move left 7 tiles",			// 0x9B
            "Move up 8 tiles",			// 0x9C
            "Move right 8 tiles",			// 0x9D
            "Move down 8 tiles",			// 0x9E
            "Move left 8 tiles",			// 0x9F
            			
            "Move right/up 1x1 tiles",			// 0xA0
            "Move right/down 1x1 tiles",			// 0xA1
            "Move left/down 1x1 tiles",			// 0xA2
            "Move left/up 1x1 tiles",			// 0xA3
            "Move right/up 1x2 tiles",			// 0xA4
            "Move right/up 2x1 tiles",			// 0xA5
            "Move right/down 2x1 tiles",			// 0xA6
            "Move right/down 1x2 tiles",			// 0xA7
            "Move left/down 1x2 tiles",			// 0xA8
            "Move left/down 2x1 tiles",			// 0xA9
            "Move left/up 2x1 tiles",			// 0xAA
            "Move left/up 1x2 tiles",			// 0xAB
            "",			// 0xAC
            "",			// 0xAD
            "",			// 0xAE
            "",			// 0xAF
            			
            "If ($1E80(${0}) [${1}, bit {2}] {3}) {4}, branch to ${5}",			// 0xB0
            "If ($1E80(${0}) [${1}, bit {2}] {3}) {4}, branch to ${5}",			// 0xB1
            "If ($1E80(${0}) [${1}, bit {2}] {3}) {4}, branch to ${5}",			// 0xB2
            "If ($1E80(${0}) [${1}, bit {2}] {3}) {4}, branch to ${5}",			// 0xB3
            "If ($1E80(${0}) [${1}, bit {2}] {3}) {4}, branch to ${5}",			// 0xB4
            "If ($1E80(${0}) [${1}, bit {2}] {3}) {4}, branch to ${5}",			// 0xB5
            "If ($1E80(${0}) [${1}, bit {2}] {3}) {4}, branch to ${5}",			// 0xB6
            "If ($1E80(${0}) [${1}, bit {2}] {3}) {4}, branch to ${5}",			// 0xB7
            "If ($1E80(${0}) [${1}, bit {2}] {3}) {4}, branch to ${5}",			// 0xB8
            "If ($1E80(${0}) [${1}, bit {2}] {3}) {4}, branch to ${5}",			// 0xB9
            "If ($1E80(${0}) [${1}, bit {2}] {3}) {4}, branch to ${5}",			// 0xBA
            "If ($1E80(${0}) [${1}, bit {2}] {3}) {4}, branch to ${5}",			// 0xBB
            "If ($1E80(${0}) [${1}, bit {2}] {3}) {4}, branch to ${5}",			// 0xBC
            "If ($1E80(${0}) [${1}, bit {2}] {3}) {4}, branch to ${5}",			// 0xBD
            "If ($1E80(${0}) [${1}, bit {2}] {3}) {4}, branch to ${5}",			// 0xBE
            "If ($1E80(${0}) [${1}, bit {2}] {3}) {4}, branch to ${5}",			// 0xBF
            			
            "Set event speed to slowest",			// 0xC0
            "Set event speed to slow",			// 0xC1
            "Set event speed to normal",			// 0xC2
            "Set event speed to fast",			// 0xC3
            "Set event speed to faster",			// 0xC4
            "Set event speed to fastest",			// 0xC5
            "Set entity to walk when moving",			// 0xC6
            "Set entity to stay still when moving",			// 0xC7
            "Set object layering priority to {0}, (low nibble {1})",			// 0xC8
            "Place object on vehicle ${0}",			// 0xC9
            "",			// 0xCA
            "",			// 0xCB
            "Turn object upward",			// 0xCC
            "Turn object right",			// 0xCD
            "Turn object downward",			// 0xCE
            "Turn object left",			// 0xCF
            			
            "Make object visible",			// 0xD0
            "Make object disappear",			// 0xD1
            "Return",			// 0xD2
            "",			// 0xD3
            "If ($08 & 0x80 == 0), branch to ${0}",			// 0xD4
            "Set position to ({0}, {1})",			// 0xD5
            "",			// 0xD6
            "Center screen on entity",			// 0xD7
            "Unfade screen",			// 0xD8
            "Fade screen",			// 0xD9
            "",			// 0xDA
            "",			// 0xDB
            "Make entity jump low",			// 0xDC
            "Make entity jump high",			// 0xDD
            "",			// 0xDE
            "",			// 0xDF
            			
            "Pause for 4 * {0} ({1}) frames",			// 0xE0
            "Set event bit $1E80($0{0}) [${1}, bit {2}]",			// 0xE1
            "Set event bit $1E80($1{0}) [${1}, bit {2}]",			// 0xE2
            "Set event bit $1E80($2{0}) [${1}, bit {2}]",			// 0xE3
            "Clear event bit $1E80($0{0}) [${1}, bit {2}]",			// 0xE4
            "Clear event bit $1E80($1{0}) [${1}, bit {2}]",			// 0xE5
            "Clear event bit $1E80($2{0}) [${1}, bit {2}]",			// 0xE6
            "",			// 0xE7
            "",			// 0xE8
            "",			// 0xE9
            "",			// 0xEA
            "",			// 0xEB
            "",			// 0xEC
            "",			// 0xED
            "",			// 0xEE
            "",			// 0xEF
            			
            "",			// 0xF0
            "",			// 0xF1
            "",			// 0xF2
            "",			// 0xF3
            "",			// 0xF4
            "",			// 0xF4
            "",			// 0xF6
            "",			// 0xF7
            "",			// 0xF8
            "Branch out of queue to ${0}",			// 0xF9
            "Pseudo-randomly choose to branch backwards {0} bytes (${1}){2}",			// 0xFA
            "Pseudo-randomly choose to branch forwards {0} bytes (${1}){2}",			// 0xFB
            "Branch backwards {0} bytes (${1}){2}",			// 0xFC
            "Branch forwards {0} bytes (${1}){2}",			// 0xFD
            "Return",			// 0xFE
            "End queue"			// 0xFF
        };
        #endregion
        public string InterpretCharacterCommand(ActionCommand asc)
        {
            string[] vars = new string[16];
            switch (asc.Opcode)
            {
                case 0xB0:
                case 0xB1:
                case 0xB2:
                case 0xB3:
                case 0xB4:
                case 0xB5:
                case 0xB6:
                case 0xB7:
                    int memory = Bits.GetShort(asc.CommandData, 1);
                    vars[0] = (memory & 0x7FFF).ToString("X3");
                    vars[1] = (((memory & 0x7FFF) / 8) + 0x1E80).ToString("X4");
                    vars[2] = (memory & 7).ToString();
                    vars[3] = ((memory & 0x8000) == 0x8000 ? "is set" : "is clear");
                    int temp = asc.Opcode & 7;
                    vars[4] = "";
                    if (temp >= 1) AppendMemoryCheck(asc, 3, ref vars[4], (asc.Opcode & 8) == 8);
                    if (temp >= 2) AppendMemoryCheck(asc, 5, ref vars[4], (asc.Opcode & 8) == 8);
                    if (temp >= 3) AppendMemoryCheck(asc, 7, ref vars[4], (asc.Opcode & 8) == 8);
                    if (temp >= 4) AppendMemoryCheck(asc, 9, ref vars[4], (asc.Opcode & 8) == 8);
                    if (temp >= 5) AppendMemoryCheck(asc, 11, ref vars[4], (asc.Opcode & 8) == 8);
                    if (temp >= 6) AppendMemoryCheck(asc, 13, ref vars[4], (asc.Opcode & 8) == 8);
                    if (temp >= 7) AppendMemoryCheck(asc, 15, ref vars[4], (asc.Opcode & 8) == 8);
                    //
                    int offset = Bits.GetInt24(asc.CommandData, ((asc.Opcode & 7) * 2) + 3);
                    vars[5] = (offset + 0xCA0000).ToString("X6");
                    if (offset == 0x005EB3)
                        vars[5] += " (simply returns)";
                    break;
                case 0xB8:
                case 0xB9:
                case 0xBA:
                case 0xBB:
                case 0xBC:
                case 0xBD:
                case 0xBE:
                case 0xBF:
                    goto case 0xB7;
                case 0xC8:
                    vars[0] = (asc.Param1 >> 4).ToString();
                    vars[1] = (asc.Param1 & 0x0F).ToString();
                    break;
                case 0xC9:
                    vars[0] = asc.Param1.ToString("X2");
                    break;
                case 0xD4:
                    vars[0] = (Bits.GetInt24(asc.CommandData, 1) + 0xCA0000).ToString("X6");
                    break;
                case 0xD5:
                    vars[0] = asc.Param1.ToString();
                    vars[1] = asc.Param2.ToString();
                    break;
                case 0xE0:
                    vars[0] = asc.Param1.ToString();
                    vars[1] = (asc.Param1 * 4).ToString();
                    break;
                case 0xE1:
                    vars[0] = asc.Param1.ToString("X2");
                    vars[1] = (0x1E80 + (asc.Param1 / 8)).ToString("X4");
                    vars[2] = (asc.Param1 & 7).ToString();
                    break;
                case 0xE2:
                    vars[0] = asc.Param1.ToString("X2");
                    vars[1] = (0x1EA0 + (asc.Param1 / 8)).ToString("X4");
                    vars[2] = (asc.Param1 & 7).ToString();
                    break;
                case 0xE3:
                    vars[0] = asc.Param1.ToString("X2");
                    vars[1] = (0x1EC0 + (asc.Param1 / 8)).ToString("X4");
                    vars[2] = (asc.Param1 & 7).ToString();
                    break;
                case 0xE4:
                    goto case 0xE1;
                case 0xE5:
                    goto case 0xE2;
                case 0xE6:
                    goto case 0xE3;
                case 0xF9:
                    vars[0] = (Bits.GetInt24(asc.CommandData, 1) + 0xCA0000).ToString("X6");
                    break;
                case 0xFA:
                case 0xFB:
                case 0xFC:
                case 0xFD:
                    vars[0] = asc.Param1.ToString();
                    vars[1] = (asc.Offset + 0xC00000 - asc.Param1).ToString("X6");
                    if (asc.Opcode < 0xFC)
                        vars[2] = " with 50 % chance";
                    break;
                default:
                    string command = "";
                    if (asc.Opcode >= 0x00 && asc.Opcode <= 0x3F)
                    {
                        command = "Do graphical action ${0}";
                        vars[0] = asc.Opcode.ToString("X2");
                        if (asc.Opcode == 0x09)
                            vars[0] += " (kneeling)";
                        else if (asc.Opcode == 0x16)
                            vars[0] += " (facing forward, head cocked left)";
                    }
                    else if (asc.Opcode >= 0x40 && asc.Opcode <= 0x7F)
                    {
                        command = "Do graphical action ${0} flipped horizontally";
                        vars[0] = (asc.Opcode & 0x3F).ToString("X2");
                    }
                    else if (CharacterCommands[asc.Opcode] == "")
                        command = BitConverter.ToString(asc.CommandData);
                    else
                        command = CharacterCommands[asc.Opcode];
                    return string.Format(command, vars);
            }
            return string.Format(CharacterCommands[asc.Opcode], vars);
        }
        private string GetBits(byte src, string[] names, int length)
        {
            string bits = "";
            string[] bit = new string[8];
            bool pre = false;
            for (int i = 1, j = 0; j < length; i *= 2, j++)
            {
                if ((src & i) == i)
                {
                    if (pre && j > 0)
                        bit[j] = ", ";
                    bit[j] += names[j];
                    pre = true;
                }
                else bit[j] = "";
            }
            for (int k = 0; k < 8; k++)
                bits += bit[k];
            return bits;
        }
        private void AppendMemoryCheck(ActionCommand aqc, int offset, ref string command, bool and)
        {
            string[] vars = new string[16];
            string check = "{0} ($1E80(${1}) [${2}, bit {3}] {4}";
            //
            vars[0] = (and ? " and" : " or");
            ushort ushort_ = Bits.GetShort(aqc.CommandData, offset);
            vars[1] = (ushort_ & 0x7FFF).ToString("X3");
            vars[2] = (((ushort_ & 0x7FFF) / 8) + 0x1E80).ToString("X4");
            vars[3] = (ushort_ & 7).ToString();
            vars[4] = ((ushort_ & 0x8000) == 0x8000 ? "is set" : "is clear");
            //
            command += string.Format(check, vars);
        }
    }
}