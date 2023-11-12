using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public void RemoveLastConstraintControls(Panel panel)
        {

            InequalityControls constraintControls = ConstraintsControls.Last();
            Console.WriteLine(constraintControls);

            panel.Controls.Remove(constraintControls.ComboboxSign);
            panel.Controls.Remove(constraintControls.TextBoxKnownTerm);

            ConstraintsControls.RemoveAt(ConstraintsControls.Count - 1);

            foreach (var variableControl in constraintControls.variableControls)
            {
                panel.Controls.Remove(variableControl.Value.Item1);
                panel.Controls.Remove(variableControl.Value.Item2);
            }
            constraintControls.RemoveAllVariables();

            Console.WriteLine(this);
        }


        public InequalityControls GetConstraintControls(int index)
        {
            return ConstraintsControls[index];
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
