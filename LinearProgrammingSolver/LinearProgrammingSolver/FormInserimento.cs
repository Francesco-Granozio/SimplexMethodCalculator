using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinearProgrammingSolver
{
    public partial class FormInserimento : Form
    {
        public FormInserimento()
        {
            InitializeComponent();
        }

        private void FormInserimento_Load(object sender, EventArgs e)
        {
            List<decimal> objectiveCoefficients = new List<decimal> { 4, 2, 1 };
            Inequality []constraints = new Inequality[]
            {
                new Inequality(new List<decimal> { -3, 1 }, InequalitySign.LessThanOrEqual, 1),
                new Inequality(new List<decimal> { 1, -2, 1 }, InequalitySign.LessThanOrEqual, 5),
                new Inequality(new List<decimal> { -1, 1, 1 }, InequalitySign.LessThanOrEqual, 3)
            };

            LinearProgrammingProblem lp = new LinearProgrammingProblem(
                new Equation(objectiveCoefficients),
                false,
                constraints,
                objectiveCoefficients.Count,
                constraints.Length);


            SimplexSolver solver = new SimplexSolver(lp);
            solver.Solve();
            Console.WriteLine(solver);


        }
    }
}
