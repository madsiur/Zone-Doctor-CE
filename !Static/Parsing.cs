using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ZONEDOCTOR._Static
{
    public static class Parsing
    {
        public static string[] DialogueTable = new string[]
            {
                "/", "*", "TERRA", "LOCKE", "CYAN", "SHADOW", "EDGAR", "SABIN",
                "CELES", "STRAGO", "RELM", "SETZER", "MOG", "GAU", "GOGO", "UMARO",
                "", "", "", "*", "", "", "", "",
                "", "", "", "", "", "", "", "",
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

        public static string[] GetLocationNames()
        {
            int offset;

            byte temp;
            string[] names = new string[Model.NUM_LOC_NAMES];
            for (int i = 0; i < Model.NUM_LOC_NAMES; i++)
            {
                offset = Bits.GetShort(Model.ROM, i * 2 + Model.BASE_LOC_NAMES_PTR) + Model.BASE_LOC_NAMES;
                names[i] = "";

                string s = "s";
                if (i == 254)
                {
                    s.ToString();
                }
                while (Model.ROM[offset] != 0)
                {
                    temp = Model.ROM[offset];

                    names[i] += DialogueTable[temp];

                    offset++;
                }
            }

            return names;
        }

        public static void SaveLocationNames(string[] locs)
        {
            int offset = 0;
            for (int i = 0; i < Model.NUM_LOC_NAMES; i++)
            {
                char[] chrName = locs[i].ToCharArray();
                string[] strName = new string[chrName.Length];
                byte[] temp = new byte[chrName.Length + 1];
                bool isValid = true;
                bool skip = false;


                for (int a = 0; a < chrName.Length; a++)
                {
                    strName[a] = chrName[a].ToString();
                }

                if (strName.Length == 1 && (strName.Equals("") || strName.Equals(" ")))
                {
                    temp = new byte[1];
                    temp[0] = 0x00;
                    skip = true;
                }

                if (!skip)
                {
                    for (int k = 0; k < strName.Length; i++)
                    {
                        for (int j = 0; j < DialogueTable.Length; j++)
                        {
                            if (strName[k].Equals(DialogueTable[j]))
                            {
                                temp[k] = (byte) j;
                                j = DialogueTable.Length;
                            }

                            if (j == DialogueTable.Length - 1)
                            {
                                isValid = false;
                            }
                        }
                    }
                }

                if (!isValid)
                {
                    temp = new byte[1];
                    temp[0] = 0x00;
                    MessageBox.Show("Inavlid location name: '" + locs[i] + "', index " + i.ToString("X2") +
                                    ". A empty entry will be written in ROM.");

                }

                if (offset + temp.Length < Model.SIZE_LOC_NAMES)
                {
                    Bits.SetShort(Model.ROM, i * 2 + Model.BASE_LOC_NAMES_PTR, (ushort)offset);
                    Bits.SetBytes(Model.ROM, offset + Model.BASE_LOC_NAMES, temp);
                    offset += temp.Length;
                }
                else
                {
                    MessageBox.Show("Cannot save location names beyond index " + i.ToString("X2") + ".Max space alloted for location names is " + Model.SIZE_LOC_NAMES.ToString("X4") + ".");
                    i = Model.NUM_LOC_NAMES;

                }
                


            }
        }

        public static bool IsLocNameValid(string locName)
        {
            char[] chArray = locName.ToCharArray();
            string[] strArray = new string[chArray.Length];
            bool isValid = true;

            for(int a = 0; a < chArray.Length; a++)
            {
                strArray[a] = chArray[a].ToString();
            }

            if (strArray.Length > 37)
            {
                isValid = false;
            }

            for(int i  = 0; i < strArray.Length; i++)
            {
                for(int j = 0; j < DialogueTable.Length; j++)
                {
                    if(strArray[i].Equals(DialogueTable[j]))
                    {
                        j = DialogueTable.Length;
                    }

                    if(j == DialogueTable.Length - 1)
                    {
                        isValid = false;
                    }
                }
            }

            return isValid;
        }
    }
}
