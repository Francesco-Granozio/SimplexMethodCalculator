using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinearProgrammingSolver
{
    internal class InequalityControls : EquationControls
    {
        public ComboBox ComboboxSign { get; private set; }
        public TextBox TextBoxKnownTerm { get; private set; }

        public InequalityControls() : base()
        {
        }

        public void AddInequalityControls(ComboBox comboBox, TextBox textBox, Panel panel)
        {
            ComboboxSign = comboBox;
            TextBoxKnownTerm = textBox;

            panel.Controls.Add(comboBox);
            panel.Controls.Add(textBox);
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

            sb.Append("Sign: ");
            sb.Append(ComboboxSign.SelectedItem);
            sb.Append(", Known Term: ");
            sb.Append(TextBoxKnownTerm.Text);

            return sb.ToString();
        }
    }

    internal static class InequalityMapper
    {
        public static Inequality ToInequality(this InequalityControls inequalityControls)
        {
            List<decimal> coefficients = new List<decimal>();

            foreach (var item in inequalityControls.variableControls)
            {
                coefficients.Add(decimal.Parse(item.Value.Item1.Text));
            }


            return new Inequality(coefficients, inequalityControls.ComboboxSign.Text.ToInequalitySign(), decimal.Parse(inequalityControls.TextBoxKnownTerm.Text));
        }
    }
}
