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
        private LinearProgrammingProblemControls linearProgrammingProblemControls = new LinearProgrammingProblemControls();

        private int numVariabili;
        private int numVincoli;
        private List<TextBox> textBoxVariabiliFunzioneObiettivo = new List<TextBox>();
        private List<Label> labelVariabiliFunzioneObiettivo = new List<Label>();

        private List<TextBox> textBoxVincoli = new List<TextBox>();
        private List<Label> labelVincoli = new List<Label>();

        private List<ComboBox> comboBoxSegniVincoli = new List<ComboBox>();
        private List<TextBox> textBoxKnownTerms = new List<TextBox>();

        public FormInserimento()
        {
            InitializeComponent();
        }


        private TextBox CreateCoefficientTextBox(int x, int y, int xOffset, int yOffset)
        {
            Random random = new Random();
            return new TextBox
            {
                Name = $"textBoxVariabile{xOffset}",
                BackColor = Color.FromArgb(224, 224, 224),
                Text = random.Next(1, 10).ToString(),
                Size = new Size(50, 33),
                Location = new Point(x + (xOffset * 120), y + (yOffset * 40)),
                Font = new Font("Segoe UI", 14),
                Visible = true
            };
        }

        private Label CreateCoefficientLabel(int x, int y, int xOffset, int yOffset)
        {
            string text;

            if (xOffset == numVariabili - 1)
            {
                text = "x" + (xOffset + 1);
            }
            else
            {
                text = "x" + (xOffset + 1) + " +";
            }


            return new Label
            {
                Name = $"labelVariabile{xOffset}",
                Text = text,
                Size = new Size(50, 33),
                Location = new Point(x + (xOffset * 120), y + (yOffset * 40)),
                Font = new Font("Segoe UI", 14),
                Visible = true,
                AutoSize = true
            };
        }

        private ComboBox CreateInequalitySignComboBox(int x, int y, int xOffset, int yOffset)
        {
            {
                return new ComboBox
                {
                    Items = { "<=", "=", ">=" },
                    SelectedItem = "<=",
                    Name = $"comboBoxSegnoVincolo{numVincoli}",
                    Size = new Size(43, 33),
                    Location = new Point(x + (xOffset * 120), y + (yOffset * 40)),
                    Font = new Font("Segoe UI", 12),
                    Visible = true,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    BackColor = Color.FromArgb(253, 253, 253)
                };
            }
        }

        private void FormInserimento_Load(object sender, EventArgs e)
        {
            numVariabili = (int)numericUpDown_totalVariables.Value;
            numVincoli = (int)numericUpDown_totalConstraints.Value;
            EquationControls equationControls = new EquationControls();

            for (int i = 0; i < numVariabili; i++)
            {
                // Create a new textbox and add it to the list
                TextBox textBoxVariable = CreateCoefficientTextBox(168, 78, i, 0);
                Label labelVariable = CreateCoefficientLabel(218, 80, i, 0);

                equationControls.AddVariable("x" + (i + 1), textBoxVariable, labelVariable);

                textBoxVariabiliFunzioneObiettivo.Add(textBoxVariable);
                labelVariabiliFunzioneObiettivo.Add(labelVariable);


                // Add the label and textbox to the panel
                panel1.Controls.Add(labelVariable);
                panel1.Controls.Add(textBoxVariable);

            }
            linearProgrammingProblemControls.ObjectiveFunctionControls = equationControls;

            for (int i = 0; i < numVincoli; i++)
            {
                ComboBox comboBoxSegnoVincolo = CreateInequalitySignComboBox(370, 148, 0, i);
                TextBox textBoxKnownTerm = CreateCoefficientTextBox(430, 148, 0, i);


                InequalityControls inequalityControls = new InequalityControls();
                inequalityControls.ComboboxSign = comboBoxSegnoVincolo;
                inequalityControls.TextBoxKnownTerm = textBoxKnownTerm;


                for (int j = 0; j < numVariabili; j++)
                {
                    TextBox textBoxVariable = CreateCoefficientTextBox(45, 150, i, j);
                    Label labelVariable = CreateCoefficientLabel(95, 152, i, j);

                    inequalityControls.AddVariable("x" + (j + 1), textBoxVariable, labelVariable);

                    textBoxVincoli.Add(textBoxVariable);
                    labelVincoli.Add(labelVariable);

                    // Add the label and textbox to the panel
                    panel1.Controls.Add(labelVariable);
                    panel1.Controls.Add(textBoxVariable);

                }

                comboBoxSegniVincoli.Add(comboBoxSegnoVincolo);
                textBoxKnownTerms.Add(textBoxKnownTerm);

                panel1.Controls.Add(comboBoxSegnoVincolo);
                panel1.Controls.Add(textBoxKnownTerm);

                linearProgrammingProblemControls.AddConstraintControls(inequalityControls);
            }

            Console.WriteLine(linearProgrammingProblemControls);

            comboBox_function.SelectedItem = "max";
            comboBox1.SelectedItem = "≤";

            List<decimal> objectiveCoefficients = new List<decimal> { 4, 2, 1 };
            Inequality[] constraints = new Inequality[]
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

            labelVariabiliFunzioneObiettivo[oldValue - 1].Text += " +";

            if (oldValue < numVariabili)
            {
                // Aggiungi i nuovi textbox e label per la funzione obiettivo
                for (int i = oldValue; i < numVariabili; i++)
                {
                    // Crea un nuovo textbox e aggiungilo alla lista
                    TextBox textBox = CreateCoefficientTextBox(168, 78, i, 0);
                    textBoxVariabiliFunzioneObiettivo.Add(textBox);

                    panel1.Controls.Add(textBox);

                    // Crea un nuovo label e imposta il suo testo
                    Label label = CreateCoefficientLabel(218, 80, i, 0);
                    labelVariabiliFunzioneObiettivo.Add(label);

                    panel1.Controls.Add(label);
                }

                int lastLabelIndex = numVincoli * oldValue - 1;  // Indice dell'ultimo label prima dell'aggiunta

                for (int k = lastLabelIndex - numVincoli + 1; k <= lastLabelIndex; k++)
                {
                    labelVincoli[k].Text += " +";
                }

                for (int i = 0; i < numVincoli; i++)
                {

                    foreach (ComboBox comboBox in comboBoxSegniVincoli)
                    {
                        comboBox.Location = new Point(comboBox.Location.X + (differenza * 40), comboBox.Location.Y);
                    }

                    foreach (TextBox textBox in textBoxKnownTerms)
                    {
                        textBox.Location = new Point(textBox.Location.X + (differenza * 40), textBox.Location.Y);
                    }

                    for (int j = oldValue; j < numVariabili; j++)
                    {

                        TextBox textBoxVariable = CreateCoefficientTextBox(45, 150, j, i);
                        Label labelVariable = CreateCoefficientLabel(95, 152, j, i);

                        textBoxVincoli.Add(textBoxVariable);
                        labelVincoli.Add(labelVariable);

                        // Add the label and textbox to the panel
                        panel1.Controls.Add(labelVariable);
                        panel1.Controls.Add(textBoxVariable);

                    }


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

                List<Label> labelsToRemove = labelVincoli.GetRange(labelVincoli.Count - (numVincoli * differenza), numVincoli * differenza);

                // Rimuovi gli elementi dalla lista
                labelVincoli.RemoveRange(labelVincoli.Count - (numVincoli * differenza), numVincoli * differenza);

                // Rimuovi gli elementi da panel1.Controls
                foreach (Label labelToRemove in labelsToRemove)
                {
                    panel1.Controls.Remove(labelToRemove);
                }

                List<TextBox> textBoxesToRemove = textBoxVincoli.GetRange(textBoxVincoli.Count - (numVincoli * differenza), numVincoli * differenza);

                // Rimuovi gli elementi dalla lista
                textBoxVincoli.RemoveRange(textBoxVincoli.Count - (numVincoli * differenza), numVincoli * differenza);

                // Rimuovi gli elementi da panel1.Controls
                foreach (TextBox textBoxToRemove in textBoxesToRemove)
                {
                    panel1.Controls.Remove(textBoxToRemove);
                }

                labelsToRemove = labelVincoli.GetRange(labelVincoli.Count - numVincoli, numVincoli);

                // Rimuovi il " +" dagli elementi della lista
                foreach (Label labelToRemove in labelsToRemove)
                {
                    labelToRemove.Text = labelToRemove.Text.TrimEnd(' ', '+');
                }


                foreach (ComboBox comboBox in comboBoxSegniVincoli)
                {
                    comboBox.Location = new Point(comboBox.Location.X - (differenza * 120), comboBox.Location.Y);
                }

                foreach (TextBox textBox in textBoxKnownTerms)
                {
                    textBox.Location = new Point(textBox.Location.X - (differenza * 120), textBox.Location.Y);
                }


            }

        }

        private void numericUpDown_totalConstraints_ValueChanged(object sender, EventArgs e)
        {
            numVariabili = (int)numericUpDown_totalVariables.Value;
            int oldValue = numVincoli;
            numVincoli = (int)numericUpDown_totalConstraints.Value;
            int differenza = Math.Abs(numVincoli - oldValue);

            if (oldValue < numVincoli)
            {
                for (int i = 0; i < differenza; i++)
                {
                    ComboBox comboBoxSegnoVincolo = CreateInequalitySignComboBox(370, 148, 0, i + oldValue);
                    TextBox textBoxKnownTerm = CreateCoefficientTextBox(430, 148, 0, i + oldValue);

                    for (int j = 0; j < numVariabili; j++)
                    {
                        TextBox textBoxVariable = CreateCoefficientTextBox(45, 150, j, i + oldValue);
                        Label labelVariable = CreateCoefficientLabel(95, 152, j, i + oldValue);

                        textBoxVincoli.Add(textBoxVariable);
                        labelVincoli.Add(labelVariable);

                        // Add the label and textbox to the panel
                        panel1.Controls.Add(labelVariable);
                        panel1.Controls.Add(textBoxVariable);

                    }

                    comboBoxSegniVincoli.Add(comboBoxSegnoVincolo);
                    textBoxKnownTerms.Add(textBoxKnownTerm);

                    panel1.Controls.Add(comboBoxSegnoVincolo);
                    panel1.Controls.Add(textBoxKnownTerm);
                }
            }
            
            else if (oldValue > numVincoli)
            {

                for (int j = 0; j < labelVincoli.Count; j++)
                {
                    Console.WriteLine(labelVincoli[j]);
                }

                for (int i = 0; i < differenza; i++)
                {
                    for (int j = 0; j < numVariabili; j++)
                    {

                        Console.WriteLine(textBoxVincoli.Count + j - 1 - numVariabili * j);


                        RemoveElement(textBoxVincoli, panel1, textBoxVincoli.Count + j - 1 - numVariabili * j);
                        RemoveElement(labelVincoli, panel1, labelVincoli.Count + j - 1 - numVariabili * j);

                        
                    }

                    RemoveElement(comboBoxSegniVincoli, panel1, comboBoxSegniVincoli.Count - 1);
                    RemoveElement(textBoxKnownTerms, panel1, textBoxKnownTerms.Count - 1);

                }
            }


        }

        private void RemoveElement<T>(List<T> list, Panel panel, int index) where T : Control
        {
            if (index >= 0 && index < list.Count)
            {
                T removedControl = list[index];
                list.RemoveAt(index);
                panel.Controls.Remove(removedControl);
            }
                
        }
    }
}
