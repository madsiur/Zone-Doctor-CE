using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ZONEDOCTOR
{
    public static class Bits
    {
        public static int Register = 0;
        private static void ShowError(int offset, int length)
        {
            MessageBox.Show(
                "Error accessing data at $" + offset.ToString("X6") + " in " + length.ToString("X6") + " byte array.\n\n" + "Please report this.",
                "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #region get functions
        public static bool GetBit(byte[] data, int offset, int bit)
        {
            try
            {
                if ((data[offset] & (byte)(Math.Pow(2, bit))) == (byte)(Math.Pow(2, bit)))
                    return true;
                return false;
            }
            catch
            {
                ShowError(offset, data.Length);
                throw new Exception();
            }
        }
        public static bool GetBit(byte data, int bit)
        {
            try
            {
                if ((data & (byte)Math.Pow(2, bit)) == (byte)Math.Pow(2, bit))
                    return true;
                return false;
            }
            catch
            {
                throw new Exception();
            }
        }
        public static bool GetBit(int data, int bit)
        {
            try
            {
                if ((data & (int)Math.Pow(2, bit)) == (int)Math.Pow(2, bit))
                    return true;
                return false;
            }
            catch
            {
                throw new Exception();
            }
        }
        public static int GetByte(ref string text)
        {
            string number = "";
            while (text.StartsWith("\n"))
                text = text.Remove(0, 1);
            while (text.StartsWith("$"))
                text = text.Remove(0, 1);
            number = text.Substring(0, 2);
            text = text.Remove(0, 2);
            return Convert.ToInt32(number, 16);
        }
        public static ushort GetShort(byte[] data, int offset)
        {
            ushort ret = 0;
            try
            {
                ret += (ushort)(data[offset + 1] << 8);
                ret += (ushort)(data[offset]);
            }
            catch
            {
                ShowError(offset, data.Length);
                throw new Exception();
            }
            return ret;
        }
        public static ushort GetShortReversed(byte[] data, int offset)
        {
            ushort ret = 0;
            try
            {
                ret += (ushort)(data[offset] << 8);
                ret += (ushort)(data[offset + 1]);
            }
            catch
            {
                ShowError(offset, data.Length);
                throw new Exception();
            }
            return ret;
        }
        public static int GetInt24(byte[] data, int offset)
        {
            int ret = 0;
            try
            {
                ret += (int)(data[offset + 2] << 16);
                ret += (int)(data[offset + 1] << 8);
                ret += (int)data[offset];
            }
            catch
            {
                ShowError(offset, data.Length);
                throw new Exception();
            }
            return ret;
        }
        public static int GetInt24Reversed(byte[] data, int offset)
        {
            int ret = 0;
            try
            {
                ret += (int)(data[offset] << 16);
                ret += (int)(data[offset + 1] << 8);
                ret += (int)data[offset + 2];
            }
            catch
            {
                ShowError(offset, data.Length);
                throw new Exception();
            }
            return ret;
        }
        public static int GetInt32(byte[] data, int offset)
        {
            return
                (data[offset + 3] << 24) +
                (data[offset + 2] << 16) +
                (data[offset + 1] << 8) +
                data[offset];
        }
        // arrays
        public static byte[] GetBytes(byte[] data, int offset)
        {
            byte[] toGet = new byte[data.Length - offset];
            try
            {
                for (int i = 0; i < data.Length && i < toGet.Length; i++)
                {
                    toGet[i] = data[offset + i];
                }
                return toGet;
            }
            catch
            {
                ShowError(offset, data.Length);
                throw new Exception();
            }
        }
        public static byte[] GetBytes(byte[] data, int offset, int size)
        {
            byte[] toGet = new byte[size];
            try
            {
                for (int i = 0; i < size; i++)
                {
                    toGet[i] = data[offset + i];
                }
                return toGet;
            }
            catch
            {
                ShowError(offset, data.Length);
                throw new Exception();
            }
        }
        public static ushort[] GetShorts(byte[] data, int offset, int size)
        {
            ushort[] toGet = new ushort[size];
            try
            {
                for (int i = 0; i < size; i++)
                {
                    toGet[i] = (ushort)(data[offset + 1 + (i * 2)] << 16);
                    toGet[i] += data[offset + (i * 2)];
                }
                return toGet;
            }
            catch
            {
                ShowError(offset, data.Length);
                throw new Exception();
            }
        }
        public static int[] GetInts(int[] data, int offset, int size)
        {
            int[] toGet = new int[size];
            try
            {
                for (int i = 0; i < size; i++)
                {
                    toGet[i] = data[offset + i];
                }
                return toGet;
            }
            catch
            {
                ShowError(offset, data.Length);
                throw new Exception();
            }
        }
        public static string GetString(byte[] data, int offset, int length)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
                sb.Append((char)data[offset++]);
            return sb.ToString();
        }
        // string readers
        public static int GetShort(ref string text)
        {
            string number = "";
            if (text.StartsWith("$"))
                text = text.Remove(0, 1);
            number += text.Substring(0, 2);
            text = text.Remove(0, 2);
            if (text.StartsWith("$"))
                text = text.Remove(0, 1);
            number += text.Substring(0, 2);
            text = text.Remove(0, 2);
            return Convert.ToInt16(number, 16);
        }
        public static int GetInt32(ref string text)
        {
            // find first occurence of number
            string number = "";
            int index = 0;
            string pattern = "[0-9\\-]";
            while (index < text.Length)
                if (Regex.IsMatch(text[index].ToString(), pattern))
                    break;
                else
                    index++;
            while (index < text.Length && Regex.IsMatch(text[index].ToString(), pattern))
            {
                number += text[index].ToString();
                index++;
            }
            text = text.Remove(0, index);
            return Convert.ToInt32(number, 10);
        }
        public static int GetInt32(ref string text, int start)
        {
            int index = start;
            string number = "";
            while (index < text.Length && Regex.IsMatch(text[index].ToString(), "[0-9]"))
            {
                number += text[index].ToString();
                index++;
            }
            return Convert.ToInt32(number, 10);
        }
        public static int GetInt32(string text)
        {
            return GetInt32(ref text);
        }
        #endregion
        #region set functions
        public static void SetBit(byte[] data, int offset, int bit, bool value)
        {
            try
            {
                if (bit < 8)
                {
                    if (value)
                        data[offset] |= (byte)(Math.Pow(2, bit));
                    else if (!value)
                        data[offset] &= (byte)((byte)(Math.Pow(2, bit)) ^ 0xFF);
                }
                else
                {
                    ushort number = Bits.GetShort(data, offset);
                    if (value)
                        number |= (ushort)(Math.Pow(2, bit));
                    else
                        number &= (ushort)((ushort)(Math.Pow(2, bit)) ^ 0xFFFF);
                    Bits.SetShort(data, offset, number);
                }
            }
            catch
            {
                ShowError(offset, data.Length);
                throw new Exception();
            }
        }
        public static void SetBit(ref byte data, int bit, bool value)
        {
            try
            {
                if (value)
                    data |= (byte)(Math.Pow(2, bit));
                else if (!value)
                    data &= (byte)((byte)(Math.Pow(2, bit)) ^ 0xFF);
            }
            catch
            {
                throw new Exception();
            }
        }
        public static void SetBitsByByte(byte[] data, int offset, byte bits, bool value)
        {
            try
            {
                if (value)
                    data[offset] |= bits;
                else if (!value)
                    data[offset] &= (byte)(bits ^ 0xFF);
            }
            catch
            {
                ShowError(offset, data.Length);
                throw new Exception();
            }
        }
        public static void SetByte(byte[] data, int offset, byte set)
        {
            try
            {
                data[offset] = set;
            }
            catch
            {
                ShowError(offset, data.Length);
                throw new Exception();
            }
        }
        public static void SetByteBits(byte[] data, int offset, byte set, byte bits)
        {
            // "check" are the bits to set exclusively
            try
            {
                // clear the bits to set
                data[offset] &= (byte)(bits ^ 0xFF);
                // set the byte bits
                data[offset] |= (byte)set;
            }
            catch
            {
            }
        }
        public static void SetShortBits(byte[] data, int offset, ushort set, ushort check)
        {
            // "check" are the bits to set exclusively
            try
            {
                // clear the bits to set
                data[offset] &= (byte)(check ^ 0xFF);
                data[offset + 1] &= (byte)((check >> 8) ^ 0xFF);
                // set the ushort bits
                data[offset] |= (byte)set;
                data[offset + 1] |= (byte)(set >> 8);
            }
            catch
            {
            }
        }
        public static void SetShort(byte[] data, int offset, int set)
        {
            try
            {
                data[offset] = (byte)(set & 0xff);
                data[offset + 1] = (byte)(set >> 8);
            }
            catch
            {
                ShowError(offset, data.Length);
                throw new Exception();
            }
        }
        public static void SetInt24(byte[] data, int offset, int value)
        {
            data[offset++] = (byte)(value & 0xFF);
            data[offset++] = (byte)((value >> 8) & 0xFF);
            data[offset] = (byte)((value >> 16) & 0xFF);
        }
        public static void SetInt24(byte[] data, int offset, int value, int bits)
        {
            data[offset] &= (byte)((bits & 0xFF) ^ 0xFF);
            data[offset + 1] &= (byte)(((bits >> 8) & 0xFF) ^ 0xFF);
            data[offset + 2] &= (byte)(((bits >> 16) & 0xFF) ^ 0xFF);
            data[offset++] |= (byte)(value & 0xFF);
            data[offset++] |= (byte)((value >> 8) & 0xFF);
            data[offset] |= (byte)((value >> 16) & 0xFF);
        }
        public static void SetInt32(byte[] data, int offset, int value)
        {
            data[offset++] = (byte)(value & 0xFF);
            data[offset++] = (byte)((value >> 8) & 0xFF);
            data[offset++] = (byte)((value >> 16) & 0xFF);
            data[offset] = (byte)((value >> 24) & 0xFF);
        }
        // arrays
        public static void SetBytes(byte[] data, int offset, byte[] src)
        {
            try
            {
                for (int i = 0; i < src.Length && i < data.Length; i++)
                {
                    data[offset + i] = src[i];
                }
            }
            catch
            {
                ShowError(offset, data.Length);
                throw new Exception();
            }
        }
        public static void SetBytes(byte[] data, int offset, byte[] toSet, int copyStart, int copyEnd)
        {
            try
            {
                for (int i = copyStart; i < toSet.Length && i <= copyEnd; i++)
                {
                    data[offset + i] = toSet[i];
                }
            }
            catch
            {
                ShowError(offset, data.Length);
                throw new Exception();
            }
        }
        public static void SetBytes(byte[] src, byte value)
        {
            for (int i = 0; i < src.Length; i++)
                src[i] = value;
        }
        public static void SetChars(byte[] data, int offset, char[] toSet)
        {
            try
            {
                for (int i = 0; i < toSet.Length; i++)
                {
                    data[offset + i] = (byte)toSet[i];
                }
            }
            catch
            {
                ShowError(offset, data.Length);
                throw new Exception();
            }
        }
        public static void SetChars(char[] dst, int offset, char[] src)
        {
            try
            {
                for (int i = 0; i < src.Length; i++)
                {
                    dst[offset + i] = src[i];
                }
            }
            catch
            {
                ShowError(offset, dst.Length);
                throw new Exception();
            }
        }
        #endregion
        // operations
        public static void Clear(IList src)
        {
            for (int i = 0; i < src.Count; i++)
                src[i] = 0;
        }
        public static bool Compare(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    return false;
            }
            return true;
        }
        public static bool Compare(char[] a, char[] b)
        {
            if (a.Length != b.Length)
                return false;
            return Compare(a, b, 0, 0);
        }
        public static bool Compare(char[] a, char[] b, int loc_a, int loc_b)
        {
            for (int c = loc_a, d = loc_b; c < a.Length && d < b.Length; c++, d++)
            {
                if (a[c] != b[d])
                    return false;
            }
            return true;
        }
        public static bool Compare(int[] a, int[] b)
        {
            if (a.Length != b.Length)
                return false;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    return false;
            }
            return true;
        }
        public static bool Compare(ushort[] a, ushort[] b)
        {
            if (a.Length != b.Length)
                return false;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    return false;
            }
            return true;
        }
        public static byte[] Copy(byte[] source)
        {
            if (source == null)
                return null;
            byte[] temp = new byte[source.Length];
            source.CopyTo(temp, 0);
            return temp;
        }
        public static ushort[] Copy(ushort[] source)
        {
            if (source == null)
                return null;
            ushort[] temp = new ushort[source.Length];
            source.CopyTo(temp, 0);
            return temp;
        }
        public static int[] Copy(int[] source)
        {
            if (source == null)
                return null;
            int[] temp = new int[source.Length];
            source.CopyTo(temp, 0);
            return temp;
        }
        public static bool[] Copy(bool[] source)
        {
            if (source == null)
                return null;
            bool[] temp = new bool[source.Length];
            source.CopyTo(temp, 0);
            return temp;
        }
        public static bool Empty(ushort[] source)
        {
            for (int i = 0; i < source.Length; i++)
                if (source[i] != 0)
                    return false;
            return true;
        }
        public static bool Empty(byte[] source)
        {
            for (int i = 0; i < source.Length; i++)
                if (source[i] != 0)
                    return false;
            return true;
        }
        public static bool Empty(int[] source)
        {
            for (int i = 0; i < source.Length; i++)
                if (source[i] != 0)
                    return false;
            return true;
        }
        public static void Fill(int[] src, int value)
        {
            for (int i = 0; i < src.Length; i++)
                src[i] = value;
        }
        public static void Fill(int[] src, int value, bool onlyEmpty)
        {
            for (int i = 0; i < src.Length; i++)
                if (!onlyEmpty || (onlyEmpty && src[i] == 0))
                    src[i] = value;
        }
        public static void Fill(byte[] src, byte value)
        {
            for (int i = 0; i < src.Length; i++)
                src[i] = value;
        }
        public static void Fill(byte[] src, byte value, int start, int size)
        {
            for (int i = start; i < size + start; i++)
                src[i] = value;
        }
        public static void Fill(char[] src, char value)
        {
            for (int i = 0; i < src.Length; i++)
                src[i] = value;
        }
        public static int Find(byte[] data, byte[] value, int startOffset)
        {
            for (int i = startOffset; i < data.Length; i++)
            {
                if (value.Length + i > data.Length)
                    return -1;
                if (Compare(value, GetBytes(data, i, value.Length)))
                    return i;
            }
            return -1;
        }
        public static short Reverse(short value)
        {
            byte a = (byte)(value >> 8);
            byte b = (byte)(value & 255);
            return (short)((b << 8) + a);
        }
        public static int Reverse(int value)
        {
            byte a = (byte)(value >> 24);
            byte b = (byte)(value >> 16);
            byte c = (byte)(value >> 8);
            byte d = (byte)value;
            return (d << 24) + (c << 16) + (b << 8) + a;
        }
        public static byte StringToByte(string value, int index)
        {
            string substring = value.Substring(index * 2, 2);
            byte equipment = Convert.ToByte(substring, 16);
            return equipment;
        }
        public static void Switch(ref int valueA, ref int valueB)
        {
            int temp = valueA;
            valueA = valueB;
            valueB = temp;
        }

        //madsiur, general conversion and validation methods
        public static byte ToHiROM(byte value)
        {
            if (value < 0x40)
                value += 0xC0;

            return value;
        }

        public static int ToHiROM(int value)
        {
            if (value < 0x400000)
                value += 0xC00000;

            return value;
        }

        public static byte ToAbs(byte value)
        {
            if (value >= 0xC0)
                value -= 0xC0;

            return value;
        }

        public static int ToAbs(int value)
        {
            if (value >= 0xC00000)
                value -= 0xC00000;

            return value;
        }

        public static bool IsValidBank(byte value)
        {
            return (value <= 0x6F) || (value >= 0xC0 && value <= 0xFF);
        }

        public static bool IsValidOffset(int value)
        {
            return (value <= 0x6FFFFF) || (value >= 0xC00000 && value <= 0xFFFFFF);
        }

        public static bool IsMatchingOffset(byte[] data, int offset, int offsetROM, ref List<int[]> faults)
        {
            int offsetB = GetInt24(data, offsetROM + 1);

            if (offset != offsetB)
            {
                faults.Add(new [] { offsetROM, offset, offsetB });
                return false;
            }

            return true;
        }

        public static bool IsMatchingShort(byte[] data, ushort val, int offsetROM, ref List<int[]> faults)
        {
            ushort valB = GetShort(data, offsetROM);

            if (val != valB)
            {
                faults.Add(new[] { offsetROM, val, valB });
                return false;
            }

            return true;
        }

        public static int findArrayMax(byte[] data, int size)
        {
            if (size < 1 || size > 3)
                return 0;

            int max = 0;

            for (int i = 0; i < data.Length; i += size)
            {
                int current;

                if (size == 1)
                {
                    current = data[i];
                    
                    if (current > max)
                        max = current;
                }
                else if (size == 2)
                {
                    current = GetShort(data, i);

                    if (current != 0xFFFF && current > max)
                        max = current;
                }
                else
                {
                    current = GetInt24(data, i);

                    if (current != 0xFFFFFF && current > max)
                        max = current;
                }
            }

            return max;
        }

        public static void FillShort(byte[] data, ushort val)
        {
            int i = 0;
            try
            {
                for (i = 0; i < data.Length; i += 2)
                {
                    data[i] = (byte)(val & 0xff);
                    data[i+1] = (byte)(val >> 8);
                }
            }
            catch (Exception)
            {
                ShowError(i, data.Length);
                throw new Exception();
            }
        }

        public static void IncShort(byte[] data, ushort inc)
        {
            int i = 0;

            try
            {
                for (i = 0; i < data.Length; i += 2)
                {
                    ushort val = 0;
                    val += (ushort) (data[i + 1] << 8);
                    val += (ushort) (data[i]);
                    val += inc;
                    data[i] = (byte) (val & 0xff);
                    data[i + 1] = (byte) (val >> 8);
                }
            }
            catch (Exception)
            {
                ShowError(i, data.Length);
                throw new Exception();
            }
        }

        public static void setAsmArray(byte[] data, int[] asmArray, int[] varArray, int offset)
        {
            if (asmArray.Length != varArray.Length)
            {
                throw new Exception("ASM and variation arrays are not equal: " + asmArray.Length + " - " + varArray.Length);
            }

            foreach (int i in asmArray)
            {
                SetInt24(data, asmArray[i] + 1, offset + varArray[i]);
            }
        }

        public static void setAsmArray(byte[] data, int[] asmArray, ushort[] valArray)
        {
            foreach (int i in asmArray)
            {
                SetShort(data, asmArray[i], valArray[i]);
            }
        }

        public static void setAsmArray(byte[] data, int[] asmArray, byte[] valArray)
        {
            foreach (int i in asmArray)
            {
                data[asmArray[i]] = valArray[i];
            }
        }

        public static void setData(byte[] dest, int offset, byte[] data, byte[] data2, byte[] data3)
        {
            SetBytes(dest, offset, data);
            offset += data.Length;
            Bits.SetBytes(dest, offset, data2);
            offset += data2.Length;
            Bits.SetBytes(dest, offset, data3);
        }
    }
}
