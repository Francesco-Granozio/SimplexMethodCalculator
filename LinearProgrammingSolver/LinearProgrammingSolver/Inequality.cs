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

        public Inequality(List<decimal> coefficients, InequalitySign inequalityType, decimal knownTerm) : base(coefficients, knownTerm)
        {
            InequalityType = inequalityType;
        }

        public Inequality(List<decimal> coefficients, InequalitySign inequalityType) : this(coefficients, inequalityType, 0)
        {
        }

        public override void InvertSigns()
        {
            base.InvertSigns();

            if (InequalityType == InequalitySign.LessThanOrEqual)
            {
                InequalityType = InequalitySign.GreaterThanOrEqual;
            }
            else if (InequalityType == InequalitySign.GreaterThanOrEqual)
            {
                InequalityType = InequalitySign.LessThanOrEqual;
            }
        }

        public Inequality Clone()
        {
            List<decimal> clonedCoefficients = new List<decimal>(Coefficients);
            return new Inequality(clonedCoefficients, InequalityType, KnownTerm);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(base.ToString().Split(new string[] { " =" }, StringSplitOptions.None)[0]);

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

            sb.Append(KnownTerm);

            return sb.ToString();
        }
    }
}
