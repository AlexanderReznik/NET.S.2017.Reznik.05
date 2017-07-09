using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCD
{
    public static class GCD
    {
        #region
        /// <summary>
        /// Gets the greatest common divisor of two numbers using Euclid Algorithm
        /// </summary>
        /// <param name="a">The first number</param>
        /// <param name="b">The second number</param>
        /// <returns>GCD</returns>
        public static long EuclidAlgorithm(long a, long b)
        {
            Check(a, b);
            return Euclid(a, b);
        }
        /// <summary>
        /// Gets the greatest common divisor of three numbers using Euclid Algorithm
        /// </summary>
        /// <param name="a">The first number</param>
        /// <param name="b">The second number</param>
        /// <param name="c">The third number</param>
        /// <returns>GCD</returns>
        public static long EuclidAlgorithm(long a, long b, long c)
        {
            Check(a, b, c);
            return Euclid(Euclid(a, b), c);
        }
        /// <summary>
        /// Gets the greatest common divisor of numbers using Euclid Algorithm
        /// </summary>
        /// <param name="numbers">Numbers to get gcd</param>
        /// <returns>GCD</returns>
        public static long EuclidAlgorithm(params long[] numbers)
        {
            Check(numbers);
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                numbers[i + 1] = Euclid(numbers[i], numbers[i + 1]);
            }
            return numbers[numbers.Length - 1];
        }
        #endregion
        #region
        /// <summary>
        /// Gets the greatest common divisor of two numbers using Stein Algorithm
        /// </summary>
        /// <param name="a">The first number</param>
        /// <param name="b">The second number</param>
        /// <returns>GCD</returns>
        public static long SteinAlgorithm(long a, long b)
        {
            Check(a, b);
            return Stein(a, b);
        }
        /// <summary>
        /// Gets the greatest common divisor of three numbers using Stein Algorithm
        /// </summary>
        /// <param name="a">The first number</param>
        /// <param name="b">The second number</param>
        /// <param name="c">The third number</param>
        /// <returns>GCD</returns>
        public static long SteinAlgorithm(long a, long b, long c)
        {
            Check(a, b, c);
            return Stein(Stein(a, b), c);
        }
        /// <summary>
        /// Gets the greatest common divisor of numbers using Stein Algorithm
        /// </summary>
        /// <param name="numbers">Numbers to get gcd</param>
        /// <returns>GCD</returns>
        public static long SteinAlgorithm(params long[] numbers)
        {
            Check(numbers);
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                numbers[i + 1] = Stein(numbers[i], numbers[i + 1]);
            }
            return numbers[numbers.Length - 1];
        }
        #endregion
        #region
        private static void Check(long a)
        {
            if (a < 0)
                throw new ArgumentOutOfRangeException($"{nameof(a)} cannot be negative.");
        }
        private static void Check(long a, long b)
        {
            Check(a);
            Check(b);
        }
        private static void Check(long a, long b, long c)
        {
            Check(a, b);
            Check(c);
        }
        private static void Check(long[] numbers)
        {
            if(numbers.Length < 2)
                throw new ArgumentException("Number of params must be more than 1");
            for(int i = 0; i < numbers.Length; i++)
                if (numbers[i] < 0) throw new ArgumentOutOfRangeException("All arguments cannot be negative");
        }
        private static long Euclid(long a, long b)
        {
            while (a > 0 && b > 0)
                if (a >= b)
                    a %= b;
                else
                    b %= a;
            return a | b;
        }
        private static long Stein(long a, long b)
        {
            if (a == 0) return b;
            if (b == 0) return a;
            if (a == b) return a;

            bool val1IsEven = (a & 1u) == 0;
            bool val2IsEven = (b & 1u) == 0;

            if (val1IsEven && val2IsEven)
                return Stein(a >> 1, b >> 1) << 1;
            else if (val1IsEven)
                return Stein(a >> 1, b);
            else if (val2IsEven)
                return Stein(a, b >> 1);
            else if (a > b)
                return Stein((a - b) >> 1, b);
            else
                return Stein(a, (b - a) >> 1);
        }
        #endregion
    }
}
