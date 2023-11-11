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
        public ComboBox ComboboxSign { get; set; }
        public TextBox TextBoxKnownTerm { get; set; }

        public InequalityControls() : base()
        {
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
}
