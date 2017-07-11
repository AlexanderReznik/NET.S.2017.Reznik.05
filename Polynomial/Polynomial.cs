using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Polynomial
{
    public class Polynomial
    {
        private readonly double[] coefficients;
        private static readonly double eps = 0.0001;

        private double[] Coefficients
        {
            get
            {
                if (coefficients == null) throw new ArgumentException();
                return coefficients;
            }
        }

        static Polynomial()
        {
            bool sucsess = false;//double.TryParse(ConfigurationManager.ConnectionStrings["epsilon"].ConnectionString, out eps);
            if (!sucsess) eps = 0.001;
        }

        public Polynomial(params double[] c)
        {
            if(c == null || c.Length == 0) throw new ArgumentException();
            coefficients = new double[Array.FindLastIndex(c, d => Math.Abs(d) >= eps) + 1];
            Array.Copy(c, coefficients, coefficients.Length);
        }

        public Polynomial() : this(1.0) {}

        public int GetDegree()
        {
            return Coefficients.Length - 1;
        }

        public double GetValueInPoint(double x)
        {
            double ans = 0;
            for (int i = 0; i < Coefficients.Length; i++)
                ans += Coefficients[i] * Math.Pow(x, i);
            return ans;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            int i = Array.FindLastIndex(Coefficients, d => Math.Abs(d) >= eps);
            if (i == -1) return "0";
            sb.Append($"{Coefficients[i]}x^{i--}");
            for (; i >= 2; i--)
            {
                if (Math.Abs(Coefficients[i]) >= eps) sb.Append($" + {Coefficients[i]}x^{i}");
            }
            if (Math.Abs(Coefficients[1]) >= eps) sb.Append($" + {Coefficients[1]}x");
            if (Math.Abs(Coefficients[0]) >= eps) sb.Append($" + {Coefficients[0]}");

            return sb.ToString();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int x = 0;
                for (int i = 0; i < Coefficients.Length; i++)
                    x ^= (int)Coefficients[i];
                return x;
            }
        }

        public override bool Equals(object obj)
        {
            if(obj == null || obj.GetType() != typeof(Polynomial)) return false;
            IStructuralEquatable iArray = ((Polynomial)obj).Coefficients;
            return iArray.Equals(Coefficients, StructuralComparisons.StructuralEqualityComparer);
        }

        public static Polynomial operator +(Polynomial lhs, Polynomial rhs)
        {
            CheckNullArguments(lhs, rhs);
            int degree = Math.Max(lhs.GetDegree(), rhs.GetDegree());
            double[] c = new double[degree + 1];
            for (int i = 0; i <= degree; i++)
            {
                c[i] += lhs.GetDegree() >= i ? lhs.Coefficients[i] : 0;
                c[i] += rhs.GetDegree() >= i ? rhs.Coefficients[i] : 0;
            }
            return  new Polynomial(c);
        }

        public static Polynomial operator -(Polynomial lhs, Polynomial rhs)
        {
            return lhs + (-1) * rhs;
        }

        public static Polynomial operator *(Polynomial lhs, Polynomial rhs)
        {
            CheckNullArguments(lhs, rhs);
            int degree = lhs.GetDegree() + rhs.GetDegree();
            double[] c = new double[degree + 1];
            for (int i = 0; i <= lhs.GetDegree(); i++)
            for (int j = 0; j <= rhs.GetDegree(); j++)
                c[i + j] += lhs.Coefficients[i] * rhs.Coefficients[j];
            return new Polynomial(c);
        }

        public static Polynomial operator *(Polynomial a, double d)
        {
            CheckNullArguments(a);
            int degree = a.GetDegree();
            double[] c = new double[degree + 1];
            for (int i = 0; i <= a.GetDegree(); i++)
                c[i] = d * a.Coefficients[i];
            return new Polynomial(c);
        }

        public static Polynomial operator *(double d, Polynomial a)
        {
            return a * d;
        }

        public static bool operator ==(Polynomial lhs, Polynomial rhs)
        {
            if (ReferenceEquals(lhs, rhs)) return true;
            if (ReferenceEquals(lhs, null)) return false;
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Polynomial lhs, Polynomial rhs)
        {
            return lhs != rhs;
        }

        private static void CheckNullArguments(Polynomial lhs, Polynomial rhs)
        {
            if(ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null)) throw new ArgumentNullException();
        }
        private static void CheckNullArguments(Polynomial a)
        {
            if (ReferenceEquals(a, null)) throw new ArgumentNullException();
        }
    }
}
