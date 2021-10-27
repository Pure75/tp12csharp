using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Reducto
{
    public class Polynomial
    {
        // List of monomials
        private List<Monomial> _monomials;
        public ReadOnlyCollection<Monomial> Monomials => _monomials.AsReadOnly(); // Don't delete this line
        private bool IsZero => _monomials.Count == 0;

        // Constructor
        public Polynomial()
        {
            _monomials = new List<Monomial>();
        }

        public Polynomial(Monomial m)
        {
            _monomials = new List<Monomial>();
            if (m.Coef != 0)
            {
                _monomials.Add(m);
            }
        }
        
        // ===== Operator overload =====
        
        // Unary -
        public static Polynomial operator -(Polynomial p)
        {
            /* FIXME */
            Polynomial p2 = new Polynomial();
            foreach (var mono in p._monomials)
            {
                p2._monomials.Add(-mono);
            }
            return p2;
        }
        
        // Binary +
        public static Polynomial operator +(Polynomial p1, Polynomial p2)
        {
            Polynomial res = new Polynomial();
            foreach (var p2Monomial in new List<Monomial>(p2._monomials))
            {
                
                if (p1._monomials.Any(mono => Monomial.HasSameDegree(mono, p2Monomial)))
                {
                    int ind = p1._monomials.FindIndex(mono => Monomial.HasSameDegree(mono, p2Monomial));
                    if((p1._monomials[ind] + p2Monomial).Coef != 0) res._monomials.Add(p1._monomials[ind] + p2Monomial);
                }
                else
                {
                    res._monomials.Add(p2Monomial);
                }
            }

            foreach (var p1Monomial in new List<Monomial>(p1._monomials))
            {
                if (p2._monomials.Any(mono => Monomial.HasSameDegree(mono,p1Monomial)))
                {
                    
                }
                else
                {
                    res._monomials.Add(p1Monomial);
                }
            }
            return res;
        }
        
        // Binary -
        public static Polynomial operator -(Polynomial p1, Polynomial p2)
        {
            Polynomial res = new Polynomial();
            foreach (var p2Monomial in new List<Monomial>(p2._monomials))
            {
                
                if (p1._monomials.Any(mono => Monomial.HasSameDegree(mono, p2Monomial)))
                {
                    int ind = p1._monomials.FindIndex(mono => Monomial.HasSameDegree(mono, p2Monomial));
                    if((p1._monomials[ind] - p2Monomial).Coef != 0) res._monomials.Add(p1._monomials[ind] - p2Monomial);
                }
                else
                {
                    res._monomials.Add(-p2Monomial);
                }
            }

            foreach (var p1Monomial in new List<Monomial>(p1._monomials))
            {
                if (p2._monomials.Any(mono => Monomial.HasSameDegree(mono,p1Monomial)))
                {
                    
                }
                else
                {
                    res._monomials.Add(p1Monomial);
                }
            }
            return res;
        }

        // Binary *
        public static Polynomial operator *(Polynomial p1, Polynomial p2)
        {
            List<Monomial> Multi = new List<Monomial>();
            /* FIXME */
            foreach (Monomial p1Monomial in new List<Monomial>(p1._monomials))
            {

                foreach (Monomial p2Monomial in new List<Monomial>(p2._monomials))
                {
                    if(p1Monomial.Coef*p2Monomial.Coef!=0)
                    {
                        Multi.Add(new Monomial(p1Monomial.Coef * p2Monomial.Coef, p1Monomial.Degree + p2Monomial.Degree));
                    }
                }

            }

            Polynomial ret = new Polynomial();
            foreach (var mono in Multi)
            {
                ret += new Polynomial(mono);
            }
            return ret;
        }
        // Binary /
        public static Polynomial operator /(Polynomial p1, Polynomial p2)
        {
            List<Monomial> Multi = new List<Monomial>();
            
            /* FIXME */
            foreach (Monomial p1Monomial in new List<Monomial>(p1._monomials))
            {

                foreach (Monomial p2Monomial in new List<Monomial>(p2._monomials))
                {
                    
                    if(p2Monomial.Coef!=0)
                    {
                        if (p1Monomial.Degree >= p2Monomial.Degree)
                        {
                            Multi.Add(new Monomial(p1Monomial.Coef / p2Monomial.Coef,
                                p1Monomial.Degree - p2Monomial.Degree));
                        }
                    }
                }
            }

            Polynomial ret = new Polynomial();
            foreach (var mono in Multi)
            {
                ret += new Polynomial(mono);
            }
            return ret;
        }
        
        // Binary ^
        public static Polynomial operator ^(Polynomial p1, Polynomial p2)
        {
            Polynomial res = new Polynomial();
            
            if(p2._monomials.Count == 0) return new Polynomial(new Monomial(1,0));
            else if (p2._monomials.Count > 1 || p2._monomials[0].Coef < 0 || p2._monomials[0].Degree !=0) throw new ArithmeticException();
            else if (p2._monomials[0].Coef == 0) return new Polynomial(new Monomial(1,0));
            else
            {
                foreach (var VARIABLE in new List<Monomial>(p1._monomials))
                {
                    res._monomials.Add(new Monomial((int)Math.Pow(VARIABLE.Coef, p2._monomials[0].Coef),
                        VARIABLE.Degree * p2._monomials[0].Coef));
                }
            }
            return res;
        }
    }
}