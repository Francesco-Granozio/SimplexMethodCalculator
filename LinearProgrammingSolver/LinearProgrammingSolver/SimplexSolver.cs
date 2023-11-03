using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearProgrammingSolver
{
    internal class SimplexSolver
    {
        private LinearProgrammingProblem nonStandardLp, standardLp;
        private bool hasObjectiveFunctionSignChanged = false;
        private CoefficientsMatrix coefficientsMatrix;
        private bool isSolved = false;

        public SimplexSolver(LinearProgrammingProblem lp)
        {
            this.nonStandardLp = lp.Clone();
            this.standardLp = lp.Clone();
        }

        public void Solve()
        {
            ConvertProblemToStandardForm();
        }

        private void ConvertProblemToStandardForm()
        {

            // innanzitutto controllo se la funzione è di max, e nel caso la trasformo in -min -f(x)

            if (!standardLp.IsMinFunction && !hasObjectiveFunctionSignChanged)
            {
                hasObjectiveFunctionSignChanged = true;
                standardLp.ObjectiveFunction.InvertSigns();
            }

            // trasformo le equazioni e i relativi termini noti >= 0
            
            for (int i = 0; i < standardLp.Costraints.Length; i++)
            {
                if (standardLp.Costraints[i].KnownTerm < 0)
                {
                    standardLp.Costraints[i].InvertSigns();
                }
            }

            // aggiungo le variabili di slack e di surplus
            
            AddSlackAndSurplusVariables();
            coefficientsMatrix = new CoefficientsMatrix(standardLp);

            isSolved = true;
        }

        private void AddSlackAndSurplusVariables()
        {
            for (int i = 0, index = 0; i < nonStandardLp.Costraints.Length; i++)
            {
                if (nonStandardLp.Costraints[i].InequalityType == InequalitySign.LessThanOrEqual)
                {
                    for (int j = 0; j < index; j++)
                    {
                        standardLp.Costraints[i].Coefficients.Add(0);
                    }

                    standardLp.Costraints[i].Coefficients.Add(1);
                    index++;
                    standardLp.Costraints[i].InequalityType = InequalitySign.Equal;
                    standardLp.TotalVariables++;
                }
                else if (nonStandardLp.Costraints[i].InequalityType == InequalitySign.GreaterThanOrEqual)
                {

                    for (int j = 0; j < index; j++)
                    {
                        standardLp.Costraints[i].Coefficients.Add(0);
                    }
                    
                    standardLp.Costraints[i].Coefficients.Add(-1);
                    index++;
                    standardLp.Costraints[i].InequalityType = InequalitySign.Equal;
                    standardLp.TotalVariables++;
                }
            }
        }

        public override string ToString()
        {
            if (!isSolved)
            {
                return "The problem has not been solved yet";
            }

            StringBuilder sb = new StringBuilder(nonStandardLp.ToString());
            sb.Append("\n\nconverted in standard form:\n\n");
            sb.Append(hasObjectiveFunctionSignChanged ? standardLp.ToString().Replace("max ", "-min ") : standardLp.ToString());

            sb.Append("\n\nCoefficients matrix:\n\n");
            sb.Append(coefficientsMatrix);

            sb.Append("\n\nCost coefficients:\n\n");
            sb.Append("[");

            if (standardLp.ObjectiveFunction.Coefficients.Any())
            {
                sb.Append(string.Join(", ", standardLp.ObjectiveFunction.Coefficients));
                sb.Append(", ");
            }

            int numZerosToAdd = standardLp.TotalVariables - standardLp.ObjectiveFunction.TotalVariables;

            for (int i = 0; i < numZerosToAdd; i++)
            {
                sb.Append("0");
                if (i < numZerosToAdd - 1)
                {
                    sb.Append(", ");
                }
            }

            sb.Append("]");

            return sb.ToString();
        }

    }
}
