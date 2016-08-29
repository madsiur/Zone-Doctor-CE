using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using ZONEDOCTOR.Properties;

namespace ZONEDOCTOR
{
    public class SortedList
    {
        public string[] names;
        public int[] unsorted;
        public string[] Names { get { return this.names; } set { this.names = value; } }
        private bool IsSortedAlphabetically
        {
            get
            {
                int index = 0;
                for (int i = 0; i < names.Length - 1; i++)
                {
                    if (names[i].Substring(index).CompareTo(names[i + 1].Substring(index)) > 0)
                        return false;
                }
                return true;
            }
        }
        private bool IsSortedNumerically
        {
            get
            {
                for (int i = 0; i < unsorted.Length - 1; i++)
                    if (unsorted[i] > unsorted[i + 1])
                        return false;
                return true;
            }
        }
        // constructor
        public SortedList(int count, int length, int offset)
        {
            names = new string[count];
            unsorted = new int[count];
            for (int i = 0; i < count; i++)
            {
                names[i] = "";
                for (int a = 0; a < length; a++)
                {
                    byte temp = Model.ROM[i * length + offset + a];
                    if (temp == 0 || temp == 0xFE)
                        names[i] += " ";
                    else if (temp > 0x7F && temp < 0xD3 && temp != 0xFF)
                        names[i] += Lists.NameTable[temp - 0x80];
                }
                unsorted[i] = i;
            }
        }
        // accessors
        /// <summary>
        /// Get the sorted index of an item in an alphabetically sorted name list.
        /// </summary>
        /// <param name="index">The unsorted index of the item</param>
        /// <returns></returns>
        public int GetSortedIndex(int index)
        {
            if (!IsSortedAlphabetically)
                SortAlphabetically();
            for (int i = 0; i < names.Length; i++)
            {
                if (unsorted[i] == index)
                    return i;
            }
            return 0;
        }
        /// <summary>
        /// Get the unsorted index of an item in an alphabetically sorted name list.
        /// </summary>
        /// <param name="index">The sorted index of the item.</param>
        /// <returns></returns>
        public int GetUnsortedIndex(int index)
        {
            if (!IsSortedAlphabetically)
                SortAlphabetically();
            if (index < unsorted.Length)
                return unsorted[index];
            return 0;
        }
        /// <summary>
        /// Returns the name in the unsorted list.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetUnsortedName(int index)
        {
            for (int i = 0; i < unsorted.Length; i++)
                if (index == unsorted[i])
                    return names[i];
            return "";
        }
        public string GetUnsortedName(int index, string[] keystrokes)
        {
            string name = GetUnsortedName(index);
            return Do.RawToASCII(name.ToCharArray(), keystrokes);
        }
        public string GetUnsortedNameSubstring(int index, int startIndex)
        {
            string name = GetUnsortedName(index);
            return name.Substring(startIndex);
        }
        public void SetName(int index, string name)
        {
            for (int i = 0; i < names.Length; i++)
                if (index == unsorted[i])
                    names[i] = name;
        }
        // functions
        public string NumerizeUnsorted(int index)
        {
            return NumerizeUnsorted(index, 0);
        }
        /// <summary>
        /// Returns the name tagged with {index} based on its unsorted index.
        /// </summary>
        /// <param name="index">The unsorted index of the name.</param>
        /// <returns></returns>
        public string NumerizeUnsorted(int index, int substringIndex)
        {
            int length = (names.Length - 1).ToString().Length;
            string name = GetUnsortedName(index).Substring(substringIndex).Trim(new char[] { ' ', '\0' });
            return "{" + index.ToString("d" + length) + "}  " + name;
        }
        public string NumerizeUnsorted(int index, string[] keystrokes)
        {
            return NumerizeUnsorted(index, 0, keystrokes);
        }
        public string NumerizeUnsorted(int index, int substringIndex, string[] keystrokes)
        {
            int length = (names.Length - 1).ToString().Length;
            string name = GetUnsortedName(index).Substring(substringIndex).Trim(new char[] { ' ', '\0' });
            return "{" + index.ToString("d" + length) + "}  " + Do.RawToASCII(name.ToCharArray(), keystrokes);
        }
        public string NumerizeSorted(int index)
        {
            return NumerizeSorted(index, 0);
        }
        /// <summary>
        /// Returns the name tagged with {index} based on its sorted index.
        /// </summary>
        /// <param name="index">The sorted index of the name.</param>
        /// <returns></returns>
        public string NumerizeSorted(int index, int substringIndex)
        {
            int length = (names.Length - 1).ToString().Length;
            string name = names[index].Substring(substringIndex).Trim(new char[] { ' ', '\0' });
            return "{" + index.ToString("d" + length) + "}  " + name;
        }
        public void SortAlphabetically()
        {
            if (IsSortedAlphabetically)
                return;
            int startIndex = 0;
            string name;
            int index;
            int length = names.Length;
            for (int a = 0; a < length - 1; a++)
            {
                for (int b = 0; b < length - 1 - a; b++)
                {
                    if (names[b + 1].Substring(startIndex).CompareTo(names[b].Substring(startIndex)) < 0)
                    {
                        name = names[b];
                        names[b] = names[b + 1];
                        names[b + 1] = name;
                        //
                        index = unsorted[b];
                        unsorted[b] = unsorted[b + 1];
                        unsorted[b + 1] = index;
                    }
                }
            }
        }
        public void SortNumerically()
        {
            string name;
            int index;
            int length = names.Length;
            for (int a = 0; a < length - 1; a++)
            {
                for (int b = 0; b < length - 1 - a; b++)
                {
                    if (unsorted[b + 1] > unsorted[b])
                    {
                        index = unsorted[b];
                        unsorted[b] = unsorted[b + 1];
                        unsorted[b + 1] = index;
                        //
                        name = names[b];
                        names[b] = names[b + 1];
                        names[b + 1] = name;
                    }
                }
            }
        }
    }
}
