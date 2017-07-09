using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Polynomial.NUnitTests
{
    public class PolynomialTests
    {
        [TestCase(new double[] { 1, 2, 3, 4 }, ExpectedResult = 3)]
        [TestCase(new double[] { 1, 2, 3, 4, 5 }, ExpectedResult = 4)]
        [TestCase(new double[] { 1 }, ExpectedResult = 0)]
        [TestCase(new double[] { 1, 2, 3 }, ExpectedResult = 2)]
        public int PolinomialDegree(double[] a)
        {
            return (new Polynomial(a)).GetDegree();
        }
        [TestCase(new double[] {1, 2, 3}, new double[] {1, 2, 3}, ExpectedResult = "6x^2 + 4x + 2")]
        public string PolinomialPlus(double[] a, double[] b)
        {
            return (new Polynomial(a) + new Polynomial(b)).ToString();
        }
        [TestCase(new double[] { 1, 2, 3 }, new double[] { 1, 2, 3 }, ExpectedResult = "0")]
        public string PolinomialMinus(double[] a, double[] b)
        {
            return (new Polynomial(a) - new Polynomial(b)).ToString();
        }
        [TestCase(new double[] { 1, 2, 3 }, new double[] { 1, 2, 3 })]
        public void PolinomialEquality(double[] a, double[] b)
        {
            Assert.IsTrue(new Polynomial(a) == new Polynomial(b));
        }
        [TestCase(new double[] { 1, 2, 3 }, new double[] { 1, 2, 3 }, ExpectedResult = "9x^4 + 12x^3 + 10x^2 + 4x + 1")]
        public string PolinomialMultiply(double[] a, double[] b)
        {
            return (new Polynomial(a) * new Polynomial(b)).ToString();
        }
        [TestCase(new double[] { 1, 2, 3 }, 2, ExpectedResult = "6x^2 + 4x + 2")]
        public string PolinomialMultiplyByNymber(double[] a, double q)
        {
            return (new Polynomial(a) * q).ToString();
        }
    }
}
