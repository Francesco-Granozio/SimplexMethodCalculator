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

        public List<(TextBox, Label)> GetRow(int index)
        {
            List<(TextBox, Label)> row = new List<(TextBox, Label)>();

            foreach (var constraintControl in ConstraintsControls)
            {
                row.Add(constraintControl["x" + index]);
            }
            return row;
        }

        public List<ComboBox> GetRowSigns() 
        {
            List<ComboBox> rowSigns = new List<ComboBox>();

            foreach (var constraintControl in ConstraintsControls)
            {
                rowSigns.Add(constraintControl.ComboboxSign);
            }
            return rowSigns;
        }

        public List<TextBox> GetRowKnownTerms()
        {
            List<TextBox> rowKnownTerms = new List<TextBox>();

            foreach (var constraintControl in ConstraintsControls)
            {
                rowKnownTerms.Add(constraintControl.TextBoxKnownTerm);
            }
            return rowKnownTerms;
           
        }

        public void AddObjectiveFunctionVariable(string variableName, TextBox textBox, Label label, Panel panel)
        {
            ObjectiveFunctionControls.AddVariable(variableName, textBox, label, panel);
        }

        public void RemoveConstraintRow(int index, Panel panel)
        {
            foreach (var constraintControl in ConstraintsControls)
            {
                constraintControl.RemoveVariable("x" + index, panel);
            }
        }

        public void RemoveObjectiveFunctionVariable(string variableName, Panel panel)
        {
            ObjectiveFunctionControls.RemoveVariable(variableName, panel);
        }

        public void AddConstraintControls(InequalityControls constraintControls)
        {
            ConstraintsControls.Add(constraintControls);
        }

        public void RemoveLastConstraintControls(Panel panel)
        {
            InequalityControls constraintControls = ConstraintsControls[ConstraintsControls.Count - 1];

            panel.Controls.Remove(constraintControls.ComboboxSign);
            panel.Controls.Remove(constraintControls.TextBoxKnownTerm);

            ConstraintsControls.RemoveAt(ConstraintsControls.Count - 1);

            foreach (var variableControl in constraintControls.variableControls)
            {
                panel.Controls.Remove(variableControl.Value.Item1);
                panel.Controls.Remove(variableControl.Value.Item2);
            }
            
            constraintControls.RemoveAllVariables();

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
