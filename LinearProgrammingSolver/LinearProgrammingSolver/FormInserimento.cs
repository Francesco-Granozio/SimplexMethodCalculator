using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace LinearProgrammingSolver
{
    public partial class FormInserimento : Form
    {

        private int numVariabili;
        private int numVincoli;
        private List<TextBox> textBoxVariabiliFunzioneObiettivo = new List<TextBox>();
        private List<Label> labelVariabiliFunzioneObiettivo = new List<Label>();

        public FormInserimento()
        {
            InitializeComponent();
        }


        private TextBox CreateCoefficientTextBox(int offset)
        {
            return new TextBox
            {
                BackColor = Color.FromArgb(224, 224, 224),
                Text = "1",
                Size = new Size(50, 33),
                Location = new Point(168 + (offset * 120), 78),
                Font = new Font("Segoe UI", 14),
                Visible = true
            };
        }

        private Label CreateCoefficientLabel(int offset)
        {
            string text;

            if (offset == numVariabili - 1)
            {
                text = "x" + (offset + 1);
            }
            else
            {
                text = "x" + (offset + 1) + " +";
            }


            return new Label
            {
                Text = text,
                Size = new Size(50, 33),
                Location = new Point(218 + (offset * 120), 80),
                Font = new Font("Segoe UI", 14),
                Visible = true,
                AutoSize = true
            };
        }

        private void FormInserimento_Load(object sender, EventArgs e)
        {
            numVariabili = (int)numericUpDown_totalVariables.Value;
            numVincoli = (int)numericUpDown_totalConstraints.Value;

            for (int i = 0; i < numVariabili; i++)
            {
                // Create a new textbox and add it to the list
                TextBox textBoxVariable = CreateCoefficientTextBox(i);
                Label labelVariable = CreateCoefficientLabel(i);

                textBoxVariabiliFunzioneObiettivo.Add(textBoxVariable);
                labelVariabiliFunzioneObiettivo.Add(labelVariable);

                // Add the label and textbox to the panel
                panel1.Controls.Add(labelVariable);
                panel1.Controls.Add(textBoxVariable);
            }

            comboBox_function.SelectedItem = "max";
            comboBox1.SelectedItem = "≤";

            List<decimal> objectiveCoefficients = new List<decimal> { 4, 2, 1 };
            Inequality []constraints = new Inequality[]
            {
                new Inequality(new List<decimal> { -3, 1 }, InequalitySign.LessThanOrEqual, 1),
                new Inequality(new List<decimal> { 1, -2, 1 }, InequalitySign.LessThanOrEqual, 5),
                new Inequality(new List<decimal> { -1, 1, 1 }, InequalitySign.LessThanOrEqual, 3)
            };

            /*List<decimal> objectiveCoefficients = new List<decimal> { 3, 9, -3 };
            Inequality[] constraints = new Inequality[]
            {
                new Inequality(new List<decimal> { 1, -1/3m, 2/3m }, InequalitySign.LessThanOrEqual, 1),
                new Inequality(new List<decimal> { -4, 4, 4 }, InequalitySign.LessThanOrEqual, 8),
                new Inequality(new List<decimal> { -3, 0, 2 }, InequalitySign.LessThanOrEqual, 4)
            };*/


            LinearProgrammingProblem lp = new LinearProgrammingProblem(
                new Equation(objectiveCoefficients),
                false,
                constraints,
                objectiveCoefficients.Count,
                constraints.Length);

            SimplexSolver solver = new SimplexSolver(lp);
            solver.Solve();
            //Console.WriteLine(solver);

        }

        private void numericUpDown_totalVariables_ValueChanged(object sender, EventArgs e)
        {
            // Ottieni il nuovo numero di variabili
            int oldValue = numVariabili;
            numVariabili = (int)numericUpDown_totalVariables.Value;
            int differenza = Math.Abs(numVariabili - oldValue);

            labelVariabiliFunzioneObiettivo[oldValue - 1].Text = "x" + oldValue + " +";

            if (oldValue < numVariabili)
            {
                // Aggiungi i nuovi textbox
                for (int i = oldValue; i < numVariabili; i++)
                {
                    // Crea un nuovo textbox e aggiungilo alla lista
                    TextBox textBox = CreateCoefficientTextBox(i);
                    textBoxVariabiliFunzioneObiettivo.Add(textBox);

                    panel1.Controls.Add(textBox);

                    // Crea un nuovo label e imposta il suo testo
                    Label label = CreateCoefficientLabel(i);
                    Console.WriteLine(label.Text);
                    labelVariabiliFunzioneObiettivo.Add(label);

                    panel1.Controls.Add(label);
                }

            }
            else if (oldValue > numVariabili)
            {
                
                for (int i = numVariabili; i < oldValue; i++)
                {
                    TextBox textBoxToRemove = textBoxVariabiliFunzioneObiettivo[numVariabili];
                    Label labelToRemove = labelVariabiliFunzioneObiettivo[numVariabili];

                    textBoxVariabiliFunzioneObiettivo.Remove(textBoxToRemove);
                    labelVariabiliFunzioneObiettivo.Remove(labelToRemove);

                    panel1.Controls.Remove(textBoxToRemove);
                    panel1.Controls.Remove(labelToRemove);
                }
                
                labelVariabiliFunzioneObiettivo[numVariabili - 1].Text = "x" + numVariabili;
                
            }
            
        }

        private void numericUpDown_totalConstraints_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
