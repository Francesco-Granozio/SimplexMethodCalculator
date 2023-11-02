using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearProgrammingSolver
{
    internal class SimplexSolver
    {
        private LinearProgrammingProblem lp;
        private bool hasObjectiveFunctionSignChanged = false;

        public SimplexSolver(LinearProgrammingProblem lp)
        {
            this.lp = lp;
        }

        public void Solve()
        {
            ConvertProblemToStandardForm();
        }

        private void ConvertProblemToStandardForm()
        {

            // innanzitutto controllo se la funzione è di max, e nel caso la trasformo in -min -f(x)

            if (!lp.IsMinFunction && !hasObjectiveFunctionSignChanged)
            {
                hasObjectiveFunctionSignChanged = true;
                lp.ObjectiveFunction.InvertSigns();
            }

            // trasformo le equazioni e i relativi termini noti >= 0
            
            for (int i = 0; i < lp.Costraints.Length; i++)
            {
                if (lp.Costraints[i].KnownTerm < 0)
                {
                    lp.Costraints[i].InvertSigns();
                }
            }

            AddSlackAndSurplusVariables();
        }

        private void AddSlackAndSurplusVariables()
        {

        }

        public override string ToString()
        {
            string s = lp.ToString();

            if (hasObjectiveFunctionSignChanged)
            {
                s = s.Replace("max", "-min");
            }

            return s;
        }
    }
}
