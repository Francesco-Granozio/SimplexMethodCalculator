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
            Equation eq = new Equation(new decimal[] { -5, -10, -4 });

            Console.WriteLine(eq);
        }
    }
}
