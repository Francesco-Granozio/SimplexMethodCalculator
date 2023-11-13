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
        private static readonly int STARTING_VARIABLES = 3;
        private static readonly int STARTING_CONSTRAINTS = 3; 

        private Random random = new Random();

        public FormInserimento()
        {
            InitializeComponent();
        }


        private TextBox CreateCoefficientTextBox(int x, int y, int xOffset, int yOffset)
        {
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
            numericUpDown_totalVariables.Value = STARTING_VARIABLES;
            numericUpDown_totalConstraints.Value = STARTING_CONSTRAINTS;
            
            numVariabili = (int)numericUpDown_totalVariables.Value;
            numVincoli = (int)numericUpDown_totalConstraints.Value;
            EquationControls equationControls = new EquationControls();

            for (int i = 0; i < numVariabili; i++)
            {
                // Create a new textbox and add it to the list
                TextBox textBoxVariable = CreateCoefficientTextBox(168, 78, i, 0);
                Label labelVariable = CreateCoefficientLabel(218, 80, i, 0);

                equationControls.AddVariable("x" + (i + 1), textBoxVariable, labelVariable, panel1);
            }

            linearProgrammingProblemControls.ObjectiveFunctionControls = equationControls;

            for (int i = 0; i < numVincoli; i++)
            {
                ComboBox comboBoxSegnoVincolo = CreateInequalitySignComboBox(370, 148, 0, i);
                TextBox textBoxKnownTerm = CreateCoefficientTextBox(430, 148, 0, i);

                
                InequalityControls inequalityControls = new InequalityControls();
                inequalityControls.AddInequalityControls(comboBoxSegnoVincolo, textBoxKnownTerm, panel1);

                for (int j = 0; j < numVariabili; j++)
                {
                    TextBox textBoxVariable = CreateCoefficientTextBox(45, 150, j, i);
                    Label labelVariable = CreateCoefficientLabel(95, 152, j, i);
                    
                    inequalityControls.AddVariable("x" + (j + 1), textBoxVariable, labelVariable, panel1);
                }
                linearProgrammingProblemControls.AddConstraintControls(inequalityControls);
            }

            comboBox_function.SelectedItem = "max";
            comboBox1.SelectedItem = "≤";

            /*List<decimal> objectiveCoefficients = new List<decimal> { 4, 2, 1 };
            Inequality[] constraints = new Inequality[]
            {
                new Inequality(new List<decimal> { -3, 1 }, InequalitySign.LessThanOrEqual, 1),
                new Inequality(new List<decimal> { 1, -2, 1 }, InequalitySign.LessThanOrEqual, 5),
                new Inequality(new List<decimal> { -1, 1, 1 }, InequalitySign.LessThanOrEqual, 3)
            };

            List<decimal> objectiveCoefficients = new List<decimal> { 3, 9, -3 };
            Inequality[] constraints = new Inequality[]
            {
                new Inequality(new List<decimal> { 1, -1/3m, 2/3m }, InequalitySign.LessThanOrEqual, 1),
                new Inequality(new List<decimal> { -4, 4, 4 }, InequalitySign.LessThanOrEqual, 8),
                new Inequality(new List<decimal> { -3, 0, 2 }, InequalitySign.LessThanOrEqual, 4)
            };


            LinearProgrammingProblem lp = new LinearProgrammingProblem(
                new Equation(objectiveCoefficients),
                false,
                constraints,
                objectiveCoefficients.Count,
                constraints.Length);

            SimplexSolver solver = new SimplexSolver(lp);
            solver.Solve();
            //Console.WriteLine(solver);
            */
        }

        private void numericUpDown_totalVariables_ValueChanged(object sender, EventArgs e)
        {
            // Ottieni il nuovo numero di variabili
            int oldValue = numVariabili;
            numVariabili = (int)numericUpDown_totalVariables.Value;
            int differenza = Math.Abs(numVariabili - oldValue);
            
            if (oldValue < numVariabili)
            {
                (_, Label labelLastCoefficient) = linearProgrammingProblemControls.ObjectiveFunctionControls["x" + oldValue];
                labelLastCoefficient.Text += " +";
                
                EquationControls eq = new EquationControls();
                // Aggiungi i nuovi textbox e label per la funzione obiettivo
                for (int i = oldValue; i < numVariabili; i++)
                {
                    // Crea un nuovo textbox e aggiungilo alla lista
                    TextBox textBox = CreateCoefficientTextBox(168, 78, i, 0);

                    // Crea un nuovo label e imposta il suo testo
                    Label label = CreateCoefficientLabel(218, 80, i, 0);

                    linearProgrammingProblemControls.AddObjectiveFunctionVariable("x" + (i + 1), textBox, label, panel1);
                }
                
                //aggiorno i vecchi ultimi label

                List<(TextBox, Label)> items = linearProgrammingProblemControls.GetRow(oldValue);

                foreach ((TextBox, Label) item in items)
                {
                    item.Item2.Text += " +";
                }

                List<ComboBox> comboBoxes = linearProgrammingProblemControls.GetRowSigns();
                List<TextBox> textBoxes = linearProgrammingProblemControls.GetRowKnownTerms();

                for (int i = 0; i < numVincoli; i++)
                {
                    
                    foreach (ComboBox comboBox in comboBoxes)
                    {
                        comboBox.Location = new Point(comboBox.Location.X + (differenza * 40), comboBox.Location.Y);
                    }

                    foreach (TextBox textBox in textBoxes)
                    {
                        textBox.Location = new Point(textBox.Location.X + (differenza * 40), textBox.Location.Y);
                    }

                   
                    for (int j = oldValue; j < numVariabili; j++)
                    {

                        TextBox textBoxVariable = CreateCoefficientTextBox(45, 150, j, i);
                        Label labelVariable = CreateCoefficientLabel(95, 152, j, i);

                        linearProgrammingProblemControls.ConstraintsControls[i].AddVariable("x" + (j + 1), textBoxVariable, labelVariable, panel1);
                    }
                }
                
            }

            else if (oldValue > numVariabili)
            {
                for (int i = numVariabili; i < oldValue; i++)
                {

                    linearProgrammingProblemControls.RemoveObjectiveFunctionVariable("x" + (i + 1), panel1);
                    linearProgrammingProblemControls.RemoveConstraintRow(i + 1, panel1);
                }
                
                (_, Label labelLastCoefficient) = linearProgrammingProblemControls.ObjectiveFunctionControls["x" + numVariabili];
                labelLastCoefficient.Text = "x" + numVariabili;

                List<(TextBox, Label)> lastRowCefficient = linearProgrammingProblemControls.GetRow(numVariabili);

                foreach (var item in lastRowCefficient)
                {
                    item.Item2.Text = "x" + numVariabili;
                }

                List<ComboBox> comboBoxes = linearProgrammingProblemControls.GetRowSigns();
                List<TextBox> textBoxes = linearProgrammingProblemControls.GetRowKnownTerms();

                foreach (ComboBox comboBox in comboBoxes)
                {
                    comboBox.Location = new Point(comboBox.Location.X - (differenza * 120), comboBox.Location.Y);
                }

                foreach (TextBox textBox in textBoxes)
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
                InequalityControls inequalityControls = null;
                
                for (int i = 0; i < differenza; i++)
                {
                    ComboBox comboBoxSegnoVincolo = CreateInequalitySignComboBox(370, 148, numVariabili - STARTING_VARIABLES, i + oldValue);
                    TextBox textBoxKnownTerm = CreateCoefficientTextBox(430, 148, numVariabili - STARTING_VARIABLES, i + oldValue);
                    
                    inequalityControls = new InequalityControls();
                    inequalityControls.AddInequalityControls(comboBoxSegnoVincolo, textBoxKnownTerm, panel1);
                    
                    for (int j = 0; j < numVariabili; j++)
                    {
                        TextBox textBoxVariable = CreateCoefficientTextBox(45, 150, j, i + oldValue);
                        Label labelVariable = CreateCoefficientLabel(95, 152, j, i + oldValue);

                        inequalityControls.AddVariable("x" + (j + 1), textBoxVariable, labelVariable, panel1);
                    }
                    linearProgrammingProblemControls.AddConstraintControls(inequalityControls);
                }

            }

            else if (oldValue > numVincoli)
            {
                for (int i = 0; i < differenza; i++)
                {
                    linearProgrammingProblemControls.RemoveLastConstraintControls(panel1);
                }
            }
        }
    }
}
