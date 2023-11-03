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
            return hasObjectiveFunctionSignChanged ? standardLp.ToString().Replace("max ", "-min ") : standardLp.ToString();
        }

    }
}
