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

        public SimplexSolver(LinearProgrammingProblem lp)
        {
            this.nonStandardLp = lp;
            this.standardLp = lp;
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
        }

        private void AddSlackAndSurplusVariables()
        {
            for (int i = 0; i < nonStandardLp.Costraints.Length; i++)
            {
                if (nonStandardLp.Costraints[i].InequalityType == InequalitySign.LessThanOrEqual)
                {
                    standardLp.Costraints[i].Coefficients.Add(1);
                    standardLp.Costraints[i].InequalityType = InequalitySign.Equal;
                    standardLp.TotalVariables++;
                }
                else if (nonStandardLp.Costraints[i].InequalityType == InequalitySign.GreaterThanOrEqual)
                {
                    standardLp.Costraints[i].Coefficients.Add(-1);
                    standardLp.Costraints[i].InequalityType = InequalitySign.Equal;
                    standardLp.TotalVariables++;
                }
            }
        }
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (hasObjectiveFunctionSignChanged)
            {
                sb.Append("-min ");
            }

            sb.Append(standardLp.ObjectiveFunction.ToString().Split('=')[0]).Append("\nsubject to\n");
            int lastVariableIndex = standardLp.TotalVariables;

            foreach (var costraint in standardLp.Costraints)
            {
                var coefficients = costraint.Coefficients;
                List<string> constraintVariables = new List<string>();

                for (int i = 0; i < coefficients.Count; i++)
                {
                    if (coefficients[i] != 0)
                    {
                        lastVariableIndex++;
                        constraintVariables.Add($"x{lastVariableIndex}");
                    }
                }

                string constraintText = string.Join(" + ", constraintVariables);
                sb.Append(constraintText).Append(" = ").Append(costraint.KnownTerm).Append("\n");

            }
            return sb.ToString();
        }
    }
}
