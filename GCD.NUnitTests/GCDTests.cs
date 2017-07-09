using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GCD.NUnitTests
{
    public class GCDTests
    {
        [TestCase(1200, 60, ExpectedResult = 60)]
        [TestCase(116150, 232704, ExpectedResult = 202)]
        [TestCase(15,4, ExpectedResult = 1)]
        public long EuclidAlgorithm_2Args(long a, long b)
        {
            return GCD.EuclidAlgorithm(a, b);
        }
        [TestCase(8, -15)]        
        [TestCase(-8, 15)]
        public void EuclidAlgorithm_2Args_ThrowsArgumentOutOfRangeException(long a, long b)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => GCD.EuclidAlgorithm(a, b));
        }

        [TestCase(1200, 60, 120, ExpectedResult = 60)]
        [TestCase(116150, 232704, 404, ExpectedResult = 202)]
        [TestCase(15, 4, 315, ExpectedResult = 1)]
        public long EuclidAlgorithm_3Args(long a, long b, long c)
        {
            return GCD.EuclidAlgorithm(a, b);
        }
        [TestCase(8, -15, 888)]
        [TestCase(-8, 15, 888)]
        [TestCase(8, 15, -888)]
        public void EuclidAlgorithm_3Args_ThrowsArgumentOutOfRangeException(long a, long b, long c)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => GCD.EuclidAlgorithm(a, b, c));
        }

        [TestCase(2, 4, 8, 16, 32, ExpectedResult = 2)]
        [TestCase(116150, 232704, 404, 8080, 202,  ExpectedResult = 202)]
        [TestCase(15, 4, 315, 78, 56565, ExpectedResult = 1)]
        public long EuclidAlgorithm_ManyArgs(long a, long b, long c, long d, long e)
        {
            return GCD.EuclidAlgorithm(a, b, c, d, e);
        }
        [TestCase(8, -15, 888, 12, 23)]
        [TestCase(-8, 15, 888, 12, 23)]
        [TestCase(8, 15, -888, 12, 23)]
        [TestCase(8, 15, 888, -12, 23)]
        [TestCase(8, 15, 888, 12, -23)]
        public void EuclidAlgorithm_ManyArgs_ThrowsArgumentOutOfRangeException(long a, long b, long c, long d, long e)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => GCD.EuclidAlgorithm(a, b, c, d, e));
        }


        [TestCase(1200, 60, ExpectedResult = 60)]
        [TestCase(116150, 232704, ExpectedResult = 202)]
        [TestCase(15, 4, ExpectedResult = 1)]
        public long SteinAlgorithm_2Args(long a, long b)
        {
            return GCD.SteinAlgorithm(a, b);
        }
        [TestCase(8, -15)]
        [TestCase(-8, 15)]
        public void SteinAlgorithm_2Args_ThrowsArgumentOutOfRangeException(long a, long b)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => GCD.SteinAlgorithm(a, b));
        }

        [TestCase(1200, 60, 120, ExpectedResult = 60)]
        [TestCase(116150, 232704, 404, ExpectedResult = 202)]
        [TestCase(15, 4, 315, ExpectedResult = 1)]
        public long SteinAlgorithm_3Args(long a, long b, long c)
        {
            return GCD.SteinAlgorithm(a, b);
        }
        [TestCase(8, -15, 888)]
        [TestCase(-8, 15, 888)]
        [TestCase(8, 15, -888)]
        public void SteinAlgorithm_3Args_ThrowsArgumentOutOfRangeException(long a, long b, long c)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => GCD.SteinAlgorithm(a, b, c));
        }

        [TestCase(2, 4, 8, 16, 32, ExpectedResult = 2)]
        [TestCase(116150, 232704, 404, 8080, 202, ExpectedResult = 202)]
        [TestCase(15, 4, 315, 78, 56565, ExpectedResult = 1)]
        public long SteinAlgorithm_ManyArgs(long a, long b, long c, long d, long e)
        {
            return GCD.SteinAlgorithm(a, b, c, d, e);
        }
        [TestCase(8, -15, 888, 12, 23)]
        [TestCase(-8, 15, 888, 12, 23)]
        [TestCase(8, 15, -888, 12, 23)]
        [TestCase(8, 15, 888, -12, 23)]
        [TestCase(8, 15, 888, 12, -23)]
        public void SteinAlgorithm_ManyArgs_ThrowsArgumentOutOfRangeException(long a, long b, long c, long d, long e)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => GCD.SteinAlgorithm(a, b, c, d, e));
        }
    }
}
