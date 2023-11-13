using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearProgrammingSolver
{
    public class Equation
    {
        public List<decimal> Coefficients { get; set; }
        public decimal KnownTerm { get; set; }
        public int TotalVariables { get => Coefficients.Count; }

        public Equation(List<decimal> coefficients, decimal knownTerm)
        {

            Coefficients = coefficients;
            KnownTerm = knownTerm;
        }

        public Equation(List<decimal> coefficients) : this(coefficients, 0)
        {
        }

        public virtual void InvertSigns()
        {
            Coefficients = Coefficients.Select(coefficient => -coefficient).ToList();
            KnownTerm = -KnownTerm;
        }


        public virtual decimal this[string variableName]
        {
            get
            {
                int variableIndex = int.Parse(variableName.Substring(1)) - 1;
                return Coefficients[variableIndex];
            }
            set
            {
                int variableIndex = int.Parse(variableName.Substring(1)) - 1;
                Coefficients[variableIndex] = value;
            }
        }



        public Equation Clone()
        {
            List<decimal> clonedCoefficients = new List<decimal>(Coefficients);
            return new Equation(clonedCoefficients, KnownTerm);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Coefficients.Count; i++)
            {
                decimal coefficient = Coefficients[i];
                //if (coefficient != 0) // Escludi i coefficienti nulli o zero
                //{
                    if (sb.Length > 0) // Aggiungi "+" solo se non è il primo termine
                    {
                        if (coefficient > 0)
                        {
                            sb.Append(" + ");
                        }
                        else
                        {
                            sb.Append(" - ");
                            coefficient = Math.Abs(coefficient);
                        }
                    }

                    if (coefficient != 1 && coefficient != -1)
                    {
                        sb.Append(coefficient.ToString("0.##"));
                    }

                    sb.Append("x");
                    sb.Append(i + 1);
                //}
            }

            sb.Append(" = ").Append(KnownTerm);

            return sb.ToString();
        }

    }
}
