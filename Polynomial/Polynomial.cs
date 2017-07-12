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
            bool sucsess = double.TryParse(ConfigurationManager.AppSettings["epsilon"], out eps);
            if (!sucsess) eps = 0.001;
        }

        /// <summary>
        /// Ctor making a Polynom from its double coefficients
        /// </summary>
        /// <param name="c">Array of double coefficients or coefficients</param>
        public Polynomial(params double[] c)
        {
            if(c == null || c.Length == 0) throw new ArgumentException();
            coefficients = new double[Array.FindLastIndex(c, d => Math.Abs(d) >= eps) + 1];
            Array.Copy(c, coefficients, coefficients.Length);
        }

        /// <summary>
        /// Makes a polynom 1.
        /// </summary>
        public Polynomial() : this(1.0) {}

        /// <summary>
        /// A method to get the degree of a Polynom
        /// </summary>
        /// <returns>The degree of a Polynom</returns>
        public int GetDegree()
        {
            return Coefficients.Length - 1;
        }

        /// <summary>
        /// CAlculates Polynom value in point x.
        /// </summary>
        /// <param name="x">Point</param>
        /// <returns>Polynom value in point x</returns>
        public double GetValueInPoint(double x)
        {
            double ans = 0;
            for (int i = 0; i < Coefficients.Length; i++)
                ans += Coefficients[i] * Math.Pow(x, i);
            return ans;
        }

        /// <summary>
        /// Makes string representation of polynom
        /// </summary>
        /// <returns>String representation of polynom</returns>
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

        /// <summary>
        /// A method for hashing
        /// </summary>
        /// <returns>int Hash-code</returns>
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

        /// <summary>
        /// A method for comparing polynom with object
        /// </summary>
        /// <param name="obj">object to compare with</param>
        /// <returns>1 if equals, 0 else</returns>
        public override bool Equals(object obj)
        {
            if(obj == null || obj.GetType() != typeof(Polynomial)) return false;
            IStructuralEquatable iArray = ((Polynomial)obj).Coefficients;
            return iArray.Equals(Coefficients, StructuralComparisons.StructuralEqualityComparer);
        }

        /// <summary>
        /// A method to adding polynoms
        /// </summary>
        /// <param name="lhs">first polynom</param>
        /// <param name="rhs">second polynom</param>
        /// <returns>The sum of polynoms</returns>
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

        /// <summary>
        /// A method to adding polynoms
        /// </summary>
        /// <param name="lhs">first polynom</param>
        /// <param name="rhs">second polynom</param>
        /// <returns>The sum of polynoms</returns>
        public static Polynomial Sum(Polynomial lhs, Polynomial rhs)
        {
            return lhs + rhs;
        }

        /// <summary>
        /// A method to subtracti polynoms
        /// </summary>
        /// <param name="lhs">first polynom</param>
        /// <param name="rhs">second polynom</param>
        /// <returns>The subtraction of polynoms</returns>
        public static Polynomial operator -(Polynomial lhs, Polynomial rhs)
        {
            return lhs + (-1) * rhs;
        }

        /// <summary>
        /// A method to subtracti polynoms
        /// </summary>
        /// <param name="lhs">first polynom</param>
        /// <param name="rhs">second polynom</param>
        /// <returns>The subtraction of polynoms</returns>
        public static Polynomial Sub(Polynomial lhs, Polynomial rhs)
        {
            return lhs - rhs;
        }

        /// <summary>
        /// A method to multiplying polynoms
        /// </summary>
        /// <param name="lhs">first polynom</param>
        /// <param name="rhs">second polynom</param>
        /// <returns>The multiplication of polynoms</returns>
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

        /// <summary>
        /// A method to multiplying polynom and number
        /// </summary>
        /// <param name="a">polynom</param>
        /// <param name="d">number</param>
        /// <returns>The multiplication of polynom and number</returns>
        public static Polynomial operator *(Polynomial a, double d)
        {
            CheckNullArguments(a);
            int degree = a.GetDegree();
            double[] c = new double[degree + 1];
            for (int i = 0; i <= a.GetDegree(); i++)
                c[i] = d * a.Coefficients[i];
            return new Polynomial(c);
        }

        /// <summary>
        /// A method to multiplying polynom and number
        /// </summary>
        /// <param name="a">polynom</param>
        /// <param name="d">number</param>
        /// <returns>The multiplication of polynom and number</returns>
        public static Polynomial operator *(double d, Polynomial a)
        {
            return a * d;
        }

        /// <summary>
        /// A method for comparing polynoms
        /// </summary>
        /// <param name="lhs">first polynom</param>
        /// <param name="rhs">second polynom</param>
        /// <returns>1 if equals, 0 else</returns>
        public static bool operator ==(Polynomial lhs, Polynomial rhs)
        {
            if (ReferenceEquals(lhs, rhs)) return true;
            if (ReferenceEquals(lhs, null)) return false;
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// A method for comparing polynoms
        /// </summary>
        /// <param name="lhs">first polynom</param>
        /// <param name="rhs">second polynom</param>
        /// <returns>0 if equals, 1 else</returns>
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
