using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ZONEDOCTOR
{
    public static class Comp
    {
        public static int Compress(byte[] source, byte[] dest)
        {
            byte[] buf = new byte[0x12000];
            byte[] b = new byte[16];
            byte p, n, run, bp;
            int maxx = 0, maxrun, x, w, start;
            ulong bpos, bpos2, fsize, size;
            fsize = (ulong)dest.Length;
            source.CopyTo(buf, 0);
            int pTempPtr = 2;
            size = 0;
            bpos = 0; bp = 0;
            bpos2 = 2014;
            n = 0; p = 0;
            while (bpos < fsize)
            {
                maxrun = 0;
                if (bpos < 2048)
                    start = (int)bpos;
                else
                    start = 2048;
                for (x = 1; x <= start; x++)
                {
                    run = 0;
                    while ((run < 31 + 3) &&
                        (buf[bpos - (ulong)x + run] == buf[bpos + run]) &&
                        (bpos + run < fsize))
                    {
                        run++;
                    }
                    if (run > maxrun)
                    {
                        maxrun = run;
                        maxx = (int)((bpos2 - (ulong)x) & 2047);
                    }
                }
                if (maxrun >= 3)
                {
                    w = ((maxrun - 3) << 11) + maxx;
                    b[bp] = (byte)(w & 255);
                    b[bp + 1] = (byte)(w >> 8);
                    bp += 2;
                    bpos += (ulong)maxrun;
                    bpos2 = (bpos2 + (ulong)maxrun) & 2047;
                }
                else
                {
                    n = (byte)(n | (1 << p));
                    b[bp] = buf[bpos];
                    bp++; bpos++;
                    bpos2 = (bpos2 + 1) & 2047;
                }
                p = (byte)((p + 1) & 7);
                if (p == 0)
                {
                    dest[pTempPtr++] = n;
                    for (int tc = 0; tc < bp; tc++)
                        dest[pTempPtr++] = b[tc];
                    size += (ulong)(bp + 1);
                    n = 0; bp = 0;
                }
            }
            if (p != 0)
            {
                dest[pTempPtr++] = n;
                for (int tc = 0; tc < bp; tc++)
                    dest[pTempPtr++] = b[tc];
                size += (ulong)(bp + 1);
                n = 0; bp = 0;
            }
            size += 2;
            Bits.SetShort(dest, 0, (ushort)size);
            return (int)size;
        }
        public static byte[] Decompress(byte[] data, int offset, int length)
        {
            ushort empty = 0;
            return Decompress(data, offset, length, ref empty);
        }
        public static byte[] Decompress(byte[] data, int offset, int length, ref ushort orig_size)
        {
            byte[] temp = new byte[0x100000];		// 1MB should be enough to hold any decompressed data in a 3MB ROM.
            int tempPtr = 0;
            byte[] buf = new byte[0x12000];
            byte[] buf2 = new byte[2048];
            byte n, x, b;
            uint size, w, num, i;
            ulong bpos, bpos2;
            uint finalCount = 0;
            size = Bits.GetShort(data, offset); offset += 2;
            bpos = 0; bpos2 = 2014;
            do
            {
                n = data[bpos + (ulong)offset]; bpos++;
                for (x = 0; x < 8; x++)
                {
                    if (((n >> x) & 1) == 1)
                    {
                        b = data[bpos + (ulong)offset]; bpos++;
                        temp[tempPtr++] = b;
                        finalCount++;
                        buf2[bpos2 & 2047] = b; bpos2++;
                    }
                    else
                    {
                        w = (uint)(data[bpos + (ulong)offset] + (data[bpos + 1 + (ulong)offset] << 8));
                        bpos += 2;
                        num = (w >> 11) + 3;
                        w = w & 2047;
                        for (i = 0; i < num; i++)
                        {
                            b = buf2[(w + i) & 2047];
                            temp[tempPtr++] = b;
                            finalCount++;
                            buf2[bpos2 & 2047] = b; bpos2++;
                        }
                    }
                    if (bpos >= size)
                        x = 8;
                }
            }
            while (bpos < size);
            byte[] dest = new byte[length];
            Buffer.BlockCopy(temp, 0, dest, 0, length);
            orig_size = (ushort)(finalCount & 0xFF00);
            return dest;
        }
    }
}
