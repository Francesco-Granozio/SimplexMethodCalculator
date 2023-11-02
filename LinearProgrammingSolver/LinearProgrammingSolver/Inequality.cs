using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearProgrammingSolver
{
    internal class Inequality : Equation
    {
        public InequalitySign InequalityType { get; set; }

        public Inequality(decimal[] coefficients, InequalitySign inequalityType, decimal knownTerm) : base(coefficients, knownTerm)
        {
            InequalityType = inequalityType;
        }

        public Inequality(decimal[] coefficients, InequalitySign inequalityType) : this(coefficients, inequalityType, 0)
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

            // Aggiungi il segno in base all'InequalityType
            switch (InequalityType)
            {
                case InequalitySign.LessThanOrEqual:
                    sb.Append(" <= ");
                    break;
                case InequalitySign.GreaterThanOrEqual:
                    sb.Append(" >= ");
                    break;
                case InequalitySign.Equal:
                    sb.Append(" = ");
                    break;
            }

            // Aggiungi il termine noto con il suo segno
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
