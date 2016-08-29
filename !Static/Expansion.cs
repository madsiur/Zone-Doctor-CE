﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZONEDOCTOR._Static
{
    public static class Expansion
    {
        // Size of original data
        public static readonly int SIZE_EVENT_PTR = 0x342;
        public static readonly int SIZE_EVENT_DATA = 0x16CE;
        public static readonly int SIZE_NPC_PTR = 0x342;
        public static readonly int SIZE_NPC_DATA = 0x4D6E;
        public static readonly int SIZE_SHORT_EXIT_PTR = 0x402;
        public static readonly int SIZE_SHORT_EXIT_DATA = 0x1AFE;
        public static readonly int SIZE_LONG_EXIT_PTR = 0x402;
        public static readonly int SIZE_LONG_EXIT_DATA = 0x57E;
        public static readonly int SIZE_CHEST_PTR = 0x340;
        public static readonly int SIZE_CHEST_DATA = 0x827;
        public static readonly int SIZE_TILEMAP_PTR = 0x41D;
        public static readonly int SIZE_TILEMAP_DATA = 0x42E50;
        public static readonly int SIZE_LOCATION = 0x3580;

        // Original offsets
        public static readonly int BASE_EVENT_PTR = 0xC40000;
        public static readonly int BASE_EVENT = BASE_EVENT_PTR + SIZE_EVENT_PTR;
        public static readonly int BASE_NPC_PTR = 0xC41A10;
        public static readonly int BASE_NPC = BASE_NPC_PTR + SIZE_NPC_PTR;
        public static readonly int BASE_SHORT_EXIT_PTR = 0xDFBB00;
        public static readonly int BASE_SHORT_EXIT = BASE_SHORT_EXIT_PTR + SIZE_SHORT_EXIT_PTR;
        public static readonly int BASE_LONG_EXIT_PTR = 0xEDF480;
        public static readonly int BASE_LONG_EXIT = BASE_LONG_EXIT_PTR + SIZE_LONG_EXIT_PTR;
        public static readonly int BASE_CHEST_PTR = 0xED82F4;
        public static readonly int BASE_CHEST = BASE_CHEST_PTR + SIZE_CHEST_PTR;
        public static readonly int BASE_TILEMAP_PTR = 0xD9CD90;
        public static readonly int BASE_TILEMAP = 0xD9D1B0;
        public static readonly int BASE_LOCATION = 0xED8F00;

        // In-bank position of new data [with pointers]
        public static readonly int NEW_SHORT_EXIT = 0x9000;
        public static readonly int NEW_LONG_EXIT = 0xD000;
        public static readonly int NEW_CHEST = 0xE000;

        // New position of Locations data
        public static readonly int NEW_LOCATION = 0x1A0000;

        // Offset to modify, mainly LDAs. The "VAR" arrays are values needed to be added to the LDA found with the offset array. (actual params of a LDA is at offset + 1)
        // The "VAL" arrays contains values to change for shorts and bytes. The ASM command offset does not require the +1 for those.
        public static readonly int ROM_LOCATION = 0xC01CBF;

        public static readonly int[] ROM_EVENT = {0xC0BCAE, 0xC0BCB4, 0xC0BCBD, 0xC0BCD3, 0xC0BCED};
        public static readonly int[] ROM_EVENT_VAR = {2, 0, 0, 2, 4};

        public static readonly int[] ROM_NPC =
        {
            0xC052BC, 0xC052C2, 0xC052D4, 0xC052DB, 0xC052E2, 0xC052EB, 0xC052F8, 0xC05305,
            0xC05321, 0xC0532C, 0xC0533A, 0xC0535A, 0xC05369, 0xC05373, 0xC0537F, 0xC0538A,
            0xC05397, 0xC053AD, 0xC053BC, 0xC053D0, 0xC053E3
        };

        public static readonly int[] ROM_NPC_VAR = {2, 0, 0, 1, 2, 2, 2, 2, 4, 4, 5, 5, 6, 7, 7, 7, 8, 8, 8, 8};

        public static readonly int[] ROM_CHEST =
        {
            0xC04C08, 0xC04C0E, 0xC04BDA, 0xC04BE0, 0xC04BEC, 0xC04BF4, 0xC015DD,
            0xC015E3, 0xC015F1, 0xC015F7, 0xC015FE, 0xC0160
        };

        public static readonly int[] ROM_CHEST_VAR = {404, 402, 2, 0, 400, 401, 2, 0, 400, 401, 402, 402};

        public static readonly int[] ROM_LONG_EXIT =
        {
            0xC018EA, 0xC018F0, 0xC01903, 0xC0190B, 0xC01916, 0xC0191E,
            0xC0192F, 0xC0193A, 0xC01942, 0xC01963, 0xC0196F, 0xC0197B,
            0xC0198A, 0xC0199F, 0xC019A9, 0xC019B6, 0xC019C4, 0xC019E5,
            0xC01A14
        };

        public static readonly int[] ROM_LONG_EXIT_VAR = {2, 0, 2, 1, 0, 0, 0, 1, 1, 3, 3, 3, 5, 5, 4, 4, 4, 3, 4};

        public static readonly int[] ROM_SHORT_EXIT =
        {
            0xC01A7D, 0xC01A83, 0xC01A8F, 0xC01AAA, 0xC01AB6, 0xC01AC2,
            0xC01AD1, 0xC01AE6, 0xC01AF0, 0xC01AFD, 0xC01B0B, 0xC01B2C,
            0xC01B5E, 0xEE20E9, 0xEE20EF, 0xEE20FE, 0xEE2106, 0xEE2110,
            0xEE213C
        };

        public static readonly int[] ROM_SHORT_EXIT_VAR = {2, 0, 0, 2, 2, 2, 4, 4, 3, 3, 3, 2, 3, 0, 2, 0, 1, 2, 4};

        public static readonly int[] ROM_TILEMAP_INT = {0xC02892, 0xC0289F, 0xC028E6, 0xC028F3, 0xC0293C, 0xC02949};
        public static readonly int[] ROM_TILEMAP_INT_VAR = {0, 2, 0, 2, 0, 2};
        public static readonly int[] ROM_TILEMAP_BYTE = {0xC028A4, 0xC028F8, 0xC0294E};
        public static readonly byte[] ROM_TILEMAP_BYTE_VAL = {0xD9, 0xD9, 0xD9};
        public static readonly int[] ROM_TILEMAP_SHORT = {0xC02898, 0xC028EC, 0xC02942};
        public static readonly ushort[] ROM_TILEMAP_SHORT_VAL = {0xD1B0, 0xD1B0, 0xD1B0};

        // Number of new locations and Tilemaps
        public static readonly int NEW_ENTRIES = 96;

        // Value used to overwrite old content
        public static readonly byte FILLER = 0xFF;

        // "Dummy" Location data (maps 417 to 512). Same GFX sets, physical map and Tilesets as the 256x256 map "Choose your scenario". 
        // All layer properties are blank (00). Tilemap assigned is $162 (new) instead of $103.
        public static readonly byte[] DEFAULT_LOCATION = {0x00, 0x00, 0x00, 0x00, 0x13, 0x00, 0x00, 0xAA,
                                                         0xC8, 0x06, 0x00, 0x64, 0x34, 0x62, 0x01, 0x00,
                                                         0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                         0x00, 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                         0x00};

        // "Dummy" Tilemap (Tilemaps 340 to 416). Same data as Tilemap $103 used in map "Choose your scenario".
        public static readonly byte[] DEFAULT_TILEMAP = {0x10, 0x01, 0x05, 0x1E, 0xDE, 0x6F, 0x01, 0xEF,
                                                        0x57, 0xED, 0xFF, 0x0F, 0xF8, 0x31, 0xF8, 0x53,
                                                        0xF8, 0x10, 0x75, 0xF8, 0x97, 0xF8, 0xB9, 0x18,
                                                        0xCE, 0x60, 0x00, 0xDE, 0xF8, 0x00, 0xF9, 0x22,
                                                        0xF9, 0x00, 0x44, 0xC1, 0xCA, 0x00, 0x4B, 0xA9,
                                                        0x61, 0x11, 0x60, 0xF9, 0x82, 0xF9, 0xA4, 0xF9,
                                                        0xC6, 0xF9, 0xF0, 0xE8, 0xC1, 0x22, 0xBA, 0x1D,
                                                        0xFA, 0x3F, 0x2A, 0x41, 0x42, 0x43, 0x44, 0xFF,
                                                        0x45, 0x46, 0x47, 0x48, 0x49, 0x4A, 0x4B, 0x4C,
                                                        0xF7, 0x4D, 0x4E, 0x4F, 0x56, 0x72, 0x51, 0x52,
                                                        0x53, 0x54, 0xFF, 0x55, 0x56, 0x57, 0x58, 0x59,
                                                        0x5A, 0x5B, 0x5C, 0xF7, 0x5D, 0x5E, 0x5F, 0x76,
                                                        0x72, 0x61, 0x62, 0x63, 0x64, 0xFF, 0x65, 0x66,
                                                        0x67, 0x68, 0x69, 0x6A, 0x6B, 0x6C, 0xF7, 0x6D,
                                                        0x6E, 0x6F, 0x96, 0x72, 0x71, 0x72, 0x73, 0x74,
                                                        0xFF, 0x75, 0x76, 0x77, 0x78, 0x79, 0x7A, 0x7B,
                                                        0x7C, 0xF7, 0x7D, 0x7E, 0x7F, 0xB6, 0x72, 0x81,
                                                        0x82, 0x83, 0x84, 0xFF, 0x85, 0x86, 0x87, 0x88,
                                                        0x89, 0x8A, 0x8B, 0x8C, 0xF7, 0x8D, 0x8E, 0x8F,
                                                        0xD6, 0x42, 0x95, 0x96, 0x92, 0x93, 0xFD, 0x94,
                                                        0x01, 0x23, 0x97, 0x98, 0x99, 0x9A, 0x9B, 0x9C,
                                                        0x4F, 0x9D, 0x9E, 0x9A, 0x9B, 0x10, 0x13, 0xFC,
                                                        0x12, 0xA6, 0x21, 0x43, 0xCF, 0xA7, 0xA8, 0xA9,
                                                        0xAA, 0x30, 0x43, 0x1C, 0x13, 0xB5, 0xB2, 0x0C,
                                                        0x41, 0xB3, 0x3C, 0x13, 0xB6, 0xB8, 0x61, 0xB3,
                                                        0x3C, 0xFB, 0x5E, 0xFB, 0x80, 0xFB, 0x00, 0xA2,
                                                        0xFB, 0xC4, 0xFB, 0xE6, 0xFB, 0x08, 0xFC, 0x2A,
                                                        0xFC, 0x4C, 0xFC, 0x6E, 0xFC, 0x90, 0xFC, 0x00,
                                                        0xB2, 0xFC, 0xD4, 0xFC, 0xF6, 0xFC, 0x18, 0xFD,
                                                        0x3A, 0xFD, 0x5C, 0xFD, 0x3E, 0xFA, 0xC0, 0xFD,
                                                        0x00, 0xE2, 0xFD, 0x04, 0xFE, 0x26, 0xFE, 0x48,
                                                        0xFE, 0x6A, 0xFE, 0x8C, 0xFE, 0xAE, 0xFE, 0xD0,
                                                        0x6E, 0x00, 0xFF, 0xFE, 0x21, 0xFF, 0x43, 0xFF,
                                                        0x65, 0xFF, 0x87, 0xFF, 0xA9, 0xFF, 0xCB, 0x7F};
    }



    }
    }
}
