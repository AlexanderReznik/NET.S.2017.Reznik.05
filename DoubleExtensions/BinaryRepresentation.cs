using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleExtensions
{
    public static class BinaryRepresentation
    {
        public static string DoubleToIEEE754(double d)
        {
            BitArray bits = new BitArray(BitConverter.GetBytes(d));
            return BitArrayToString(bits);
        }

        private static string BitArrayToString(BitArray ba)
        {
            StringBuilder ans = new StringBuilder(ba.Length);
            for (int i = ba.Length - 1; i >= 0; i--)
                ans.Append(ba[i] ? '1' : '0');
            return ans.ToString();
        }
    }
}
