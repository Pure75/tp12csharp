using System;

namespace Reducto
{
    public class Monomial
    {
        private readonly int _coef;
        private readonly int _degree;
        public bool IsZero => _coef == 0;
        public int Coef => _coef;
        public int Degree => _degree;

        // Constructor for Monomial
        // - A monomial of coefficient 0 is of degree -1
        // - A monomial of degree negative (and of coefficient different than 0) is an ArgumentException
        public Monomial(int coef, int degree = 0)
        {
            _coef = coef;
            _degree = degree;
            if (coef == 0) _degree = -1;
            else if (_degree < 0) throw new ArgumentException("Degree < 0");
        }
        
        // Operator
        public static bool HasSameDegree(Monomial m1, Monomial m2)
        {
            return m1.Degree == m2.Degree;
        }

        public static bool operator ==(Monomial m1, Monomial m2)
        {
            return HasSameDegree(m1, m2) && m1.Coef == m2.Coef;
        }

        public static bool operator !=(Monomial m1, Monomial m2)
        {
            return !(m1 == m2);
        }

        public static Monomial operator -(Monomial m)
        {
            int x = -(m.Coef);
            int y = (m.Degree);
            Monomial res = new Monomial(x,y);
            return res;
        }
            
        public static Monomial operator +(Monomial m1, Monomial m2)
        {
            if (m1.Coef == 0) return m2;
            else if (m2.Coef == 0) return m1;
            else if (m1.Degree != m2.Degree) throw new ArithmeticException("Degrees are different");
            else
            {
                int x = m1.Coef + m2.Coef;
                Monomial res = new Monomial(x,m1.Degree);
                return res;
            }
        }

        public static Monomial operator -(Monomial m1, Monomial m2)
        {
            if (m1.Coef == 0) return -m2;
            else if (m2.Coef == 0) return m1;
            else if (m1.Degree != m2.Degree) throw new ArithmeticException("Degrees are different");
            else
            {
                int x = m1.Coef - m2.Coef;
                Monomial res = new Monomial(x,m1.Degree);
                return res;
            }
        }
        
        public static Monomial operator *(Monomial m1, Monomial m2)
        {
            int x = m1.Coef * m2.Coef;
            int y = m1.Degree + m2.Degree;
            Monomial res = new Monomial(x,y);
            return res;
        }
        public static Monomial operator /(Monomial m1, Monomial m2)
        {
            if (m2.IsZero) throw new ArithmeticException("Divison by 0");
            else
            {
                int x = m1.Coef / m2.Coef;
                int y = m1.Degree - m2.Degree;
                Monomial res = new Monomial(x,y);
                return res;
            }
        }
    }
}