using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearProgrammingSolver
{
    internal class LinearProgrammingProblemControls
    {
        public EquationControls ObjectiveFunctionControls { get; set; }
        public List<InequalityControls> ConstraintsControls { get; set; }

        public LinearProgrammingProblemControls()
        {
            ObjectiveFunctionControls = new EquationControls();
            ConstraintsControls = new List<InequalityControls>();
        }

        public void AddConstraintControls(InequalityControls constraintControls)
        {
            ConstraintsControls.Add(constraintControls);
        }

        public void RemoveConstraintControls(InequalityControls constraintControls)
        {
            ConstraintsControls.Remove(constraintControls);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Objective Function: ");
            sb.Append(ObjectiveFunctionControls.ToString());
            sb.Append("\n");

            sb.Append("Constraints: ");
            sb.Append("\n");

            foreach (var constraintControls in ConstraintsControls)
            {
                sb.Append(constraintControls.ToString());
                sb.Append("\n");
            }

            return sb.ToString();
        }
    }
}
