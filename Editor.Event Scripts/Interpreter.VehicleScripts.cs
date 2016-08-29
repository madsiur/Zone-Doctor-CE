using System;
using System.Collections.Generic;
using System.Text;

namespace ZONEDOCTOR.ScriptsEditor.Commands
{
    public partial class Interpreter
    {
        #region Static Data
        private static string[] VehicleCommands = new string[]
        {
            "","","","","","","","","","","","","","","","",			// 0x00
            "","","","","","","","","","","","","","","","",			// 0x10
            "","","","","","","","","","","","","","","","",			// 0x20
            "","","","","","","","","","","","","","","","",			// 0x30
            "","","","","","","","","","","","","","","","",			// 0x40
            "","","","","","","","","","","","","","","","",			// 0x50
            "","","","","","","","","","","","","","","","",			// 0x60
            "","","","","","","","","","","","","","","","",			// 0x70
            "","","","","","","","","","","","","","","","",			// 0x80
            "","","","","","","","","","","","","","","","",			// 0x90
            "","","","","","","","","","","","","","","","",			// 0xA0
            			
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
            			
            "Modify vehicle script behavior (${0}): {1}",			// 0xC0
            "Set vehicle's facing direction to ${0} ({1} degrees) (0 is north, goes counter-clockwise)",			// 0xC1
            "Set vehicle's propulsion direction to ${0} ({1} degrees) (0 is north, goes counter-clockwise)",			// 0xC2
            "Vehicle script command $",			// 0xC3
            "Set mode 7 height perspective to ${0}",			// 0xC4
            "Set vehicle height to ${0} (max is $7E), (unknown byte ${1})",			// 0xC5
            "Propel vehicle at speed ${0}",			// 0xC6
            "Place airship at position ({0})",			// 0xC7
            "Set bit $1E80(${0}) [${1}, bit {2}]",			// 0xC8
            "Clear bit $1E80(${0}) [${1}, bit {2}]",			// 0xC9
            "Invoke battle, enemy set ${0}, background ${1} ({2}), other flags ${3}",			// 0xCA
            "Invoke battle, enemy set ${0}, background ${1} ({2}), other flags ${3}",			// 0xCB
            "Invoke battle, enemy set ${0}, background ${1} ({2}), other flags ${3}",			// 0xCC
            "Invoke battle, enemy set ${0}, background ${1} ({2}), other flags ${3}",			// 0xCD
            "Invoke battle, enemy set ${0}, background ${1} ({2}), other flags ${3}",			// 0xCE
            "Invoke battle, enemy set ${0}, background ${1} ({2}), other flags ${3}",			// 0xCF
            			
            "Show vehicle",			// 0xD0
            "Hide vehicle",			// 0xD1
            "Load map ${0} ({1}), position ({2}, {3}), mode ${4}",			// 0xD2
            "Load map ${0} ({1}), position ({2}, {3}), mode ${4}",			// 0xD3
            "Unfade screen",			// 0xD4
            "Unfade screen",			// 0xD5
            "Unfade screen",			// 0xD6
            "Unfade screen",			// 0xD7
            "Unfade screen",			// 0xD8
            "Fade screen",			// 0xD9
            "Show flashing arrows indicating the direction you're turning",			// 0xDA
            "Latch (lock in) the input for direction turned ($1EB6, bit 7 holds the value)",			// 0xDB
            "Hide flashing arrows that indicate the direction you're turning",			// 0xDC
            "Hide mini-map",			// 0xDD
            "",			// 0xDE
            "Show mini-map",			// 0xDF
            			
            "Pause for {0} units",			// 0xE0
            "",			// 0xE1
            "",			// 0xE2
            "",			// 0xE3
            "",			// 0xE4
            "",			// 0xE5
            "",			// 0xE6
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
            "Show part of world getting zapped",			// 0xF3
            "Change graphic to Falcon",			// 0xF4
            "Show part of world getting zapped",			// 0xF5
            "",			// 0xF6
            "Change graphic to pidgeon",			// 0xF7
            "Show part of world getting blown up",			// 0xF8
            "",			// 0xF9
            "Show airship emerging from the ocean",			// 0xFA
            "Show airship smoking",			// 0xFB
            "Show airship crashing",			// 0xFC
            "Change graphic to Esper Terra",			// 0xFD
            "Show scene with airship heading to Vector",			// 0xFE
            "End vehicle script"			// 0xFF
        };
        #endregion
        public string InterpretVehicleCommand(ActionCommand aqc)
        {
            string[] vars = new string[16];
            switch (aqc.Opcode)
            {
                case 0xB0:
                case 0xB1:
                case 0xB2:
                case 0xB3:
                case 0xB4:
                case 0xB5:
                case 0xB6:
                case 0xB7:
                    int memory = Bits.GetShort(aqc.CommandData, 1);
                    vars[0] = (memory & 0x7FFF).ToString("X3");
                    vars[1] = (((memory & 0x7FFF) / 8) + 0x1E80).ToString("X4");
                    vars[2] = (memory & 7).ToString();
                    vars[3] = ((memory & 0x8000) == 0x8000 ? "is set" : "is clear");
                    int temp = aqc.Opcode & 7;
                    vars[4] = "";
                    if (temp >= 1) AppendMemoryCheck(aqc, 3, ref vars[4], (aqc.Opcode & 8) == 8);
                    if (temp >= 2) AppendMemoryCheck(aqc, 5, ref vars[4], (aqc.Opcode & 8) == 8);
                    if (temp >= 3) AppendMemoryCheck(aqc, 7, ref vars[4], (aqc.Opcode & 8) == 8);
                    if (temp >= 4) AppendMemoryCheck(aqc, 9, ref vars[4], (aqc.Opcode & 8) == 8);
                    if (temp >= 5) AppendMemoryCheck(aqc, 11, ref vars[4], (aqc.Opcode & 8) == 8);
                    if (temp >= 6) AppendMemoryCheck(aqc, 13, ref vars[4], (aqc.Opcode & 8) == 8);
                    if (temp >= 7) AppendMemoryCheck(aqc, 15, ref vars[4], (aqc.Opcode & 8) == 8);
                    //
                    int offset = Bits.GetInt24(aqc.CommandData, ((aqc.Opcode & 7) * 2) + 3);
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
                case 0xC0:
                    vars[0] = aqc.Param1.ToString("X2");
                    if (aqc.Param1 == 0x10)
                        vars[1] = "Change mode to mode 7 persepective";
                    else if (aqc.Param1 == 0x20)
                        vars[1] = "Allow ship to propel without changing direction facing";
                    else
                        vars[1] = "Unknown mode change";
                    break;
                case 0xC1:
                case 0xC2:
                    int direction = Bits.GetShort(aqc.CommandData, 1);
                    vars[0] = direction.ToString("X4");
                    vars[1] = direction.ToString();
                    break;
                case 0xC4:
                    vars[0] = Bits.GetShort(aqc.CommandData, 1).ToString("X4");
                    break;
                case 0xC5:
                    vars[0] = aqc.Param2.ToString("X2");
                    vars[1] = aqc.Param1.ToString("X2");
                    break;
                case 0xC6:
                    goto case 0xC4;
                case 0xC7:
                    vars[0] = aqc.Param1 + ", " + aqc.Param2 + ")";
                    break;
                case 0xC8:
                case 0xC9:
                    vars[0] = aqc.Param1.ToString("X2");
                    vars[1] = (0x1E80 + aqc.Param1 / 8).ToString("X4");
                    vars[2] = (aqc.Param1 & 7).ToString();
                    break;
                case 0xCA:
                case 0xCB:
                case 0xCC:
                case 0xCD:
                case 0xCE:
                case 0xCF:
                    byte formation = (byte)(aqc.Param2 & 0x3F);
                    vars[0] = aqc.Param1.ToString("X2");
                    vars[1] = formation.ToString("X2");
                    vars[2] = Lists.BattlefieldNames[formation];
                    vars[3] = (aqc.Param2 - aqc.Param1).ToString("X2");
                    break;
                case 0xD2:
                case 0xD3:
                    ushort map = Bits.GetShort(aqc.CommandData, 1);
                    vars[0] = (map & 0x01FF).ToString("X4");
                    vars[1] = GetLocationName(map);
                    vars[2] = aqc.Param3.ToString();
                    vars[3] = aqc.Param4.ToString();
                    vars[4] = aqc.Param5.ToString("X2");
                    break;
                case 0xE0:
                    vars[0] = aqc.Param1.ToString();
                    break;
                default:
                    if (aqc.Opcode <= 0x80)
                    {
                        formation = aqc.Opcode;
                        string[] move = new string[]
                        {
                            "double speed of turns",
                            "decrease speed by $100",
                            "move forward",
                            "turn right",
                            "turn left",
                            "go up",
                            "go down"
                        };
                        vars[0] = "";
                        int x = 6;
                        do
                        {
                            if (formation >= (1 << x))
                            {
                                formation -= (byte)(1 << x);
                                vars[0] += move[x];
                                if (formation != 0)
                                    vars[0] += ", ";
                            }
                            x--;
                        }
                        while (formation != 0);
                        vars[1] = aqc.Param1.ToString();
                        return string.Format("Move vehicle as follows: ({0}), {1} units", vars);
                    }
                    else if (VehicleCommands[aqc.Opcode] == "")
                        return BitConverter.ToString(aqc.CommandData);
                    else
                        return VehicleCommands[aqc.Opcode];
            }
            return string.Format(VehicleCommands[aqc.Opcode], vars);
        }
    }
}