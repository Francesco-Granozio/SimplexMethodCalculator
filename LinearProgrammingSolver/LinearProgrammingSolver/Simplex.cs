using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearProgrammingSolver
{
    internal class Simplex
    {
        private CoefficientsMatrix coefficientsMatrix;
        private decimal[] cTransposed;
        private List<int> baseVariables = new List<int>();
        private List<int> nonBaseVariables = new List<int>();
        private decimal[] c_b_Transposed;
        private CoefficientsMatrix A_b;
        private CoefficientsMatrix A_b_Inverse;

        public Simplex(CoefficientsMatrix coefficientsMatrix, decimal[] cTransposed, List<int> baseVariables, List<int> nonBaseVariables,
            decimal[] c_b_Transposed, CoefficientsMatrix A_b, CoefficientsMatrix A_b_Inverse)
        {
            this.coefficientsMatrix = coefficientsMatrix;
            this.cTransposed = cTransposed;
            this.baseVariables = new List<int>(baseVariables);
            this.nonBaseVariables = new List<int>(nonBaseVariables);
            this.c_b_Transposed = c_b_Transposed.Clone() as decimal[];
            this.A_b = A_b.Clone();
            this.A_b_Inverse = A_b_Inverse.Clone();
        }

        private StringBuilder OptTestToString(int j)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"j = {j}\n");
            sb.Append($"z{j} - c{j} = c_b^T * A_b^-1 * a_{j} - c_{j} =\n= ");

            sb.Append("[");

            for (int i = 0; i < c_b_Transposed.Length; i++)
            {
                sb.Append(c_b_Transposed[i].ToString("0.##"));

                if (i < c_b_Transposed.Length - 1)
                {
                    sb.Append(", ");
                }
            }
            sb.Append("]").Append(" *\n ").Append(A_b_Inverse).Append(" * ");

            sb.Append("[");
            decimal[] column = coefficientsMatrix.GetColumn(nonBaseVariables[j]);
            for (int i = 0; i < column.Length; i++)
            {
                sb.Append(column[i].ToString("0.##"));
                if (i < column.Length - 1)
                {
                    sb.Append(", ");
                }
            }
            sb.Append("]").Append(" - ").Append(cTransposed[nonBaseVariables[j]].ToString("0.##"));

            return sb;
        }

        
        public (int index, decimal value) OptTest()
        {
            List<decimal> z_j_c_j = new List<decimal>();
            decimal result;

            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < nonBaseVariables.Count; j++)
            {
                sb.Append(OptTestToString(j));

                result = c_b_Transposed.Multiply(A_b_Inverse).Multiply(coefficientsMatrix.GetColumn(nonBaseVariables[j])) - cTransposed[nonBaseVariables[j]];
                z_j_c_j.Add(result);
                sb.Append(" = ").Append(result).Append("\n\n");
            }
            Console.WriteLine(sb);
            
            return (z_j_c_j.IndexOf(z_j_c_j.Max()), z_j_c_j.Max());
        }
    }
}
