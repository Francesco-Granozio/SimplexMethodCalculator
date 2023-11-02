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
            LinearProgrammingProblem lp = new LinearProgrammingProblem(
                new Equation(new decimal[] { 5, 10, 8 }), 
                false, 
                new Inequality[] { new Inequality(new decimal[] { 3, 5, 2 }, InequalitySign.GreaterThanOrEqual, -60m),
                                   new Inequality(new decimal[] { 4, 4, 4 }, InequalitySign.GreaterThanOrEqual, 72m),
                                   new Inequality(new decimal[] { 2, 4, 5 }, InequalitySign.GreaterThanOrEqual, -100m)
                                 },
                3, 
                3);
            
            Console.WriteLine(lp);
            Console.WriteLine();
            SimplexSolver solver = new SimplexSolver(lp);
            solver.Solve();
            Console.WriteLine(solver);
        }
    }
}
