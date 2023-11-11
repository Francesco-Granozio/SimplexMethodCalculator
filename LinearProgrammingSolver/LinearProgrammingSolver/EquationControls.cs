using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinearProgrammingSolver
{
    internal class EquationControls
    {
        private Dictionary<string, (TextBox, Label)> variableControls { get; set; }
        public int Count { get => variableControls.Count; }

        public EquationControls()
        {
            variableControls = new Dictionary<string, (TextBox, Label)>();
        }

        public void AddVariable(string variableName, TextBox textBox, Label label)
        {
            variableControls.Add(variableName, (textBox, label));
        }

        public void RemoveVariable(string variableName)
        {
            variableControls.Remove(variableName);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var variableControl in variableControls)
            {
                sb.Append(variableControl.Key);
                sb.Append(": ");
                sb.Append(variableControl.Value.Item1.Text);
                sb.Append(", ");
            }

            return sb.ToString();
        }
    }
}
