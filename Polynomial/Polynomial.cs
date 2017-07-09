using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polynomial
{
    public class Polynomial
    {
        private double[] coefficients;

        public Polynomial(params double[] c)
        {
            if(c == null || c.Length == 0) throw new ArgumentException();
            coefficients = new double[Array.FindLastIndex(c, d => d != 0) + 1];
            Array.Copy(c, coefficients, coefficients.Length);
        }

        public Polynomial() : this(1.0) {}

        public int GetDegree()
        {
            Check();
            return coefficients.Length - 1;
        }

        public double GetValueInPoint(double x)
        {
            double ans = 0;
            for (int i = 0; i < coefficients.Length; i++)
                ans += coefficients[i] * Math.Pow(x, i);
            return ans;
        }

        public override string ToString()
        {
            Check();
            StringBuilder sb = new StringBuilder();
            int i = Array.FindLastIndex(coefficients, d => d != 0);
            if (i == -1) return "0";
            sb.Append($"{coefficients[i]}x^{i--}");
            for (; i >= 2; i--)
            {
                if (coefficients[i] != 0) sb.Append($" + {coefficients[i]}x^{i}");
            }
            if (coefficients[1] != 0) sb.Append($" + {coefficients[1]}x");
            if (coefficients[0] != 0) sb.Append($" + {coefficients[0]}");

            return sb.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Check();
            if(obj == null || obj.GetType() != typeof(Polynomial)) return false;
            IStructuralEquatable iArray = ((Polynomial)obj).coefficients;
            return iArray.Equals(coefficients, StructuralComparisons.StructuralEqualityComparer);
        }

        public static Polynomial operator +(Polynomial a, Polynomial b)
        {
            int degree = Math.Max(a.GetDegree(), b.GetDegree());
            double[] c = new double[degree + 1];
            for (int i = 0; i <= degree; i++)
            {
                c[i] += a.GetDegree() >= i ? a.coefficients[i] : 0;
                c[i] += b.GetDegree() >= i ? b.coefficients[i] : 0;
            }
            return  new Polynomial(c);
        }

        public static Polynomial operator -(Polynomial a, Polynomial b)
        {
            int degree = Math.Max(a.GetDegree(), b.GetDegree());
            double[] c = new double[degree + 1];
            for (int i = 0; i <= degree; i++)
            {
                c[i] += a.GetDegree() >= i ? a.coefficients[i] : 0;
                c[i] -= b.GetDegree() >= i ? b.coefficients[i] : 0;
            }
            return new Polynomial(c);
        }

        public static Polynomial operator *(Polynomial a, Polynomial b)
        {
            int degree = a.GetDegree() + b.GetDegree();
            double[] c = new double[degree + 1];
            for (int i = 0; i <= a.GetDegree(); i++)
            for (int j = 0; j <= b.GetDegree(); j++)
                c[i + j] += a.coefficients[i] * b.coefficients[j];
            return new Polynomial(c);
        }

        public static Polynomial operator *(Polynomial a, double d)
        {
            int degree = a.GetDegree();
            double[] c = new double[degree + 1];
            for (int i = 0; i <= a.GetDegree(); i++)
                c[i] = d * a.coefficients[i];
            return new Polynomial(c);
        }

        public static Polynomial operator *(double d, Polynomial a)
        {
            return a * d;
        }

        public static bool operator ==(Polynomial a, Polynomial b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Polynomial a, Polynomial b)
        {
            return !a.Equals(b);
        }

        private void Check()
        {
            if(coefficients == null) throw new ArgumentException();
        }
    }
}
