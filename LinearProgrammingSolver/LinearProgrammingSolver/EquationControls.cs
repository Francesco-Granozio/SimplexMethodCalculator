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
        public Dictionary<string, (TextBox, Label)> variableControls { get; set; }
        public int Count { get => variableControls.Count; }

        public EquationControls()
        {
            variableControls = new Dictionary<string, (TextBox, Label)>();
        }

        public void AddVariable(string variableName, TextBox textBox, Label label, Panel panel)
        {
            variableControls.Add(variableName, (textBox, label));
            panel.Controls.Add(textBox);
            panel.Controls.Add(label);
        }

        public void RemoveVariable(string variableName, Panel panel)
        {
            if (variableControls.TryGetValue(variableName, out var controls))
            {
                TextBox textBox = controls.Item1;
                Label label = controls.Item2;

                // Rimuovi i controlli dal pannello
                panel.Controls.Remove(textBox);
                panel.Controls.Remove(label);

                // Rimuovi la variabile dal dizionario
                variableControls.Remove(variableName);
            }
        }

        public void RemoveAllVariables()
        {
            variableControls.Clear();
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
