using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearProgrammingSolver
{
    internal class Equation
    {
        public decimal[] Coefficients { get; set; }
        public decimal KnownTerm { get; set; }
        public int TotalVariables { get => Coefficients.Length; }

        public Equation(decimal[] coefficients, decimal knownTerm)
        {
            Coefficients = coefficients;
            KnownTerm = knownTerm;
        }

        public Equation(decimal[] coefficients) : this(coefficients, 0)
        {
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Coefficients.Length; i++)
            {
                decimal coefficient = Coefficients[i];
                if (i > 0)
                {
                    if (coefficient >= 0)
                    {
                        sb.Append(" + ");
                    }
                    else
                    {
                        sb.Append(" - ");
                        coefficient = Math.Abs(coefficient);
                    }
                }

                sb.Append(coefficient);
                sb.Append("x");
                sb.Append(i + 1);
            }

            sb.Append(" = ");

            if (KnownTerm < 0)
            { 
                sb.Append(" - ");
                KnownTerm = Math.Abs(KnownTerm);
            }
            sb.Append(KnownTerm);

            return sb.ToString();
        }

      
    }
}
