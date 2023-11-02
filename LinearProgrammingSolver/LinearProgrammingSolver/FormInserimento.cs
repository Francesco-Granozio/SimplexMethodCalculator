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
            List<decimal> objectiveCoefficients = new List<decimal> { 5, 10, 8 };
            Inequality []constraints = new Inequality[]
            {
                new Inequality(new List<decimal> { 3, 5, 2 }, InequalitySign.GreaterThanOrEqual, -60m),
                new Inequality(new List<decimal> { 4, 4, 4 }, InequalitySign.GreaterThanOrEqual, 72m),
                new Inequality(new List<decimal> { 2, 4, 5 }, InequalitySign.GreaterThanOrEqual, -100m)
            };

            LinearProgrammingProblem lp = new LinearProgrammingProblem(
                new Equation(objectiveCoefficients),
                false,
                constraints,
                objectiveCoefficients.Count,
                constraints.Length);

            Console.WriteLine(lp);
            Console.WriteLine();
            SimplexSolver solver = new SimplexSolver(lp);
            solver.Solve();
            Console.WriteLine(solver);
        }
    }
}
