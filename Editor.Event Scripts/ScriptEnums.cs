using System;
using System.Collections.Generic;
using System.Text;

namespace ZONEDOCTOR.ScriptsEditor
{
    static class ScriptEnums
    {
        // Average Length: 1
        private static int[] EventLengths = new int[]
        {
         // 0 1 2 3 4 5 6 7   8 9 A B C D E F
            0,0,0,0,0,0,0,0,  0,0,0,0,0,0,0,0, // 0x00
            0,0,0,0,0,0,0,0,  0,0,0,0,0,0,0,0, // 0x10
            0,0,0,0,0,0,0,0,  0,0,0,0,0,0,0,0, // 0x20
            0,0,0,0,0,2,2,3,  1,1,1,1,5,2,2,3, // 0x30
            3,2,2,3,3,1,2,1,  3,1,1,3,3,3,1,1, // 0x40
            2,4,2,4,1,2,2,2,  2,2,2,1,1,3,3,3, // 0x50
            3,4,2,2,3,3,6,6,  6,6,6,6,6,2,3,6, // 0x60
            3,3,3,0,0,1,4,2,  2,4,5,1,2,2,3,3, // 0x70
            2,2,1,1,3,3,2,2,  4,4,4,3,3,2,1,1, // 0x80
            1,1,1,1,1,1,1,1,  2,4,1,2,2,1,1,1, // 0x90
            6,2,1,4,1,5,1,2,  1,1,1,1,1,1,1,1, // 0xA0
            2,1,4,5,2,2,0,5,  2,2,2,1,3,4,0,1, // 0xB0
            6,8,10,12,14,16,18,20,  6,8,10,12,14,16,18,20, // 0xC0
            2,2,2,2,2,2,2,2,  2,2,2,2,2,2,1,1, // 0xD0
            1,1,1,1,1,1,1,2,  4,4,4,4,1,3,1,3, // 0xE0
            2,3,2,2,2,4,4,1,  1,2,1,1,2,1,1,1  // 0xF0
        };
        private static int[] CharacterLengths = new int[]
        {
         // 0 1 2 3 4 5 6 7   8 9 A B C D E F
            1,1,1,1,1,1,1,1,  1,1,1,1,1,1,1,1, // 0x00
            1,1,1,1,1,1,1,1,  1,1,1,1,1,1,1,1, // 0x10
            1,1,1,1,1,1,1,1,  1,1,1,1,1,1,1,1, // 0x20
            1,1,1,1,1,1,1,1,  1,1,1,1,1,1,1,1, // 0x30
            
            1,1,1,1,1,1,1,1,  1,1,1,1,1,1,1,1, // 0x40
            1,1,1,1,1,1,1,1,  1,1,1,1,1,1,1,1, // 0x50
            1,1,1,1,1,1,1,1,  1,1,1,1,1,1,1,1, // 0x60
            1,1,1,1,1,1,1,1,  1,1,1,1,1,1,1,1, // 0x70
            1,1,1,1,1,1,1,1,  1,1,1,1,1,1,1,1, // 0x80
            1,1,1,1,1,1,1,1,  1,1,1,1,1,1,1,1, // 0x90
            1,1,1,1,1,1,1,1,  1,1,1,1,0,0,0,0, // 0xA0
            6,8,10,12,14,16,18,20,  6,8,10,12,14,16,18,20, // 0xB0
            1,1,1,1,1,1,1,1,  2,2,0,0,1,1,1,1, // 0xC0
            1,1,1,1,4,3,1,1,  1,1,1,1,1,1,0,0, // 0xD0
            2,2,2,2,2,2,2,1,  0,0,0,0,0,0,0,0, // 0xE0
            0,0,0,0,0,0,0,0,  0,4,2,2,2,2,1,1  // 0xF0
        };
        private static int[] MapLengths = new int[]
        {
         // 0 1 2 3 4 5 6 7   8 9 A B C D E F
            1,1,1,1,1,1,1,1,  1,1,1,1,1,1,1,1, // 0x00
            1,1,1,1,1,1,1,1,  1,1,1,1,1,1,1,1, // 0x10
            1,1,1,1,1,1,1,1,  1,1,1,1,1,1,1,1, // 0x20
            1,1,1,1,1,1,1,1,  1,1,1,1,1,1,1,1, // 0x30
            
            1,1,1,1,1,1,1,1,  1,1,1,1,1,1,1,1, // 0x40
            1,1,1,1,1,1,1,1,  1,1,1,1,1,1,1,1, // 0x50
            1,1,1,1,1,1,1,1,  1,1,1,1,1,1,1,1, // 0x60
            1,1,1,1,1,1,1,1,  1,1,1,1,1,1,1,1, // 0x70
            1,1,1,1,1,1,1,1,  1,1,1,1,1,1,1,1, // 0x80
            1,1,1,1,1,1,1,1,  1,1,1,1,1,1,1,1, // 0x90
            1,1,1,1,1,1,1,1,  1,1,1,1,0,0,0,0, // 0xA0
            6,8,10,12,14,16,18,20,  6,8,10,12,14,16,18,20, // 0xB0
            1,1,1,1,1,1,1,3,  2,2,0,0,1,1,1,1, // 0xC0
            1,1,6,6,4,3,0,1,  1,1,1,1,1,1,0,1, // 0xD0
            2,2,2,2,2,2,2,0,  0,0,0,0,0,0,0,0, // 0xE0
            0,0,0,0,0,0,0,0,  0,4,2,2,1,1,1,1  // 0xF0
        };
        private static int[] VehicleLengths = new int[]
        {
         // 0 1 2 3 4 5 6 7   8 9 A B C D E F
            2,2,2,2,2,2,2,2,  2,2,2,2,2,2,2,2, // 0x00
            2,2,2,2,2,2,2,2,  2,2,2,2,2,2,2,2, // 0x10
            2,2,2,2,2,2,2,2,  2,2,2,2,2,2,2,2, // 0x20
            2,2,2,2,2,2,2,2,  2,2,2,2,2,2,2,2, // 0x30
            
            2,2,2,2,2,2,2,2,  2,2,2,2,2,2,2,2, // 0x40
            2,2,2,2,2,2,2,2,  2,2,2,2,2,2,2,2, // 0x50
            2,2,2,2,2,2,2,2,  2,2,2,2,2,2,2,2, // 0x60
            2,2,2,2,2,2,2,2,  2,2,2,2,2,2,2,2, // 0x70
            0,0,0,0,0,0,0,0,  0,0,0,0,0,0,0,0, // 0x80
            0,0,0,0,0,0,0,0,  0,0,0,0,0,0,0,0, // 0x90
            0,0,0,0,0,0,0,0,  0,0,0,0,0,0,0,0, // 0xA0
            6,8,10,12,14,16,18,20,  6,8,10,12,14,16,18,20, // 0xB0
            2,3,3,3,3,3,3,3,  3,3,3,3,3,3,3,3, // 0xC0
            1,1,6,6,1,1,1,1,  1,1,1,1,1,1,3,1, // 0xD0
            2,1,1,1,1,1,1,1,  1,1,1,1,1,1,1,1, // 0xE0
            1,1,1,1,1,1,1,1,  1,0,1,1,1,1,1,1  // 0xF0
        };
        public static int GetEventCommandLength(int opcode, params int[] parameters)
        {
            int length = EventLengths[opcode];
            if (length == 0) // Now we have to be dealing with Event Command that triggers an action queue
            {
                switch (opcode)
                {
                    case 0x73:
                    case 0x74:
                        if (parameters.Length > 2)
                            length = 5 + (parameters[1] * parameters[2]);
                        else
                            length = 5;
                        break;
                    case 0xB6:  // dialogue option branching
                        if (parameters.Length > 1)
                            length = 1 + (parameters[1] * 3);
                        else
                            length = 1;
                        break;
                    case 0xBE:
                        length = 2 + ((parameters[0] & 0x7F) * 3);
                        break;
                    default:
                        length = 2 + (parameters[0] & 0x7F); // Max value of 127 0x7F
                        break;
                }
            }
            if (length == 0)
                throw new Exception("Invalid Length");
            return length;
        }
        public static int GetCharacterCommandLength(int opcode, int option)
        {
            int length = CharacterLengths[opcode];
            if (length == 0)
                return 1;//throw new Exception("Invalid Length");
            return length;
        }
        public static int GetMapCommandLength(int opcode, int option)
        {
            int length = MapLengths[opcode];
            if (length == 0)
                return 1;//throw new Exception("Invalid Length");
            return length;
        }
        public static int GetVehicleCommandLength(int opcode, int option)
        {
            int length = VehicleLengths[opcode];
            if (length == 0)
                return 1;//throw new Exception("Invalid Length");
            return length;
        }
    }
}
