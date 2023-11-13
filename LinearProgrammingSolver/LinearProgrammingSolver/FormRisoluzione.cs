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
    public partial class FormRisoluzione : Form
    {
        private LinearProgrammingProblem linearProgrammingProblem; 
        
        public FormRisoluzione(LinearProgrammingProblem linearProgrammingProblem)
        {
            this.linearProgrammingProblem = linearProgrammingProblem;
            InitializeComponent();
        }

        private void FormRisoluzione_Load(object sender, EventArgs e)
        {
            SimplexSolver simplexSolver = new SimplexSolver(linearProgrammingProblem);
            simplexSolver.Solve();

            richTextBox_original_problem.Text = simplexSolver.nonStandardLp.ToString();
            
            if (simplexSolver.HasObjectiveFunctionSignChanged)
            {
                richTextBox_standard_form_problem.Text = "-";
            }

            richTextBox_standard_form_problem.Text += simplexSolver.standardLp.ToString();

        }
    }
}
