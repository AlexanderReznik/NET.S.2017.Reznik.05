using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DoubleExtensions
{
    [StructLayout(LayoutKind.Explicit)]
    struct LongDoubleConverter
    {
        [FieldOffset(0)] public long LongValue;
        [FieldOffset(0)] public double DoubleValue;
    }

    public static class BinaryRepresentation
    {
        public static string DoubleToIEEE754(this double d)
        {
            BitArray bits = new BitArray(BitConverter.GetBytes(d));
            return BitArrayToString(bits);
        }

        public static string DoubleToIEEE754Updated(this double d)
        {
            long l = GetLongFromDouble(d);
            return GetBitsFromLong(l);
        }

        private static string BitArrayToString(BitArray ba)
        {
            StringBuilder ans = new StringBuilder(ba.Length);
            for (int i = ba.Length - 1; i >= 0; i--)
                ans.Append(ba[i] ? '1' : '0');
            return ans.ToString();
        }

        private static long GetLongFromDouble(double d)
        {
            LongDoubleConverter c = new LongDoubleConverter();
            c.DoubleValue = d;
            return c.LongValue;
        }

        private static string GetBitsFromLong(long l)
        {
            long mask = 1;
            StringBuilder answer = new StringBuilder();
            for (int i = 63; i >= 0; i--)
                answer.Append((l >> i) & mask);
            return answer.ToString();
        }
    }
}
