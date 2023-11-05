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
        private decimal[] knownTerms;

        public Simplex(CoefficientsMatrix coefficientsMatrix, decimal[] cTransposed, List<int> baseVariables, List<int> nonBaseVariables,
            decimal[] c_b_Transposed, CoefficientsMatrix A_b, CoefficientsMatrix A_b_Inverse, decimal[] knownTerms)
        {
            this.coefficientsMatrix = coefficientsMatrix;
            this.cTransposed = cTransposed;
            this.baseVariables = new List<int>(baseVariables);
            this.nonBaseVariables = new List<int>(nonBaseVariables);
            this.c_b_Transposed = c_b_Transposed.Clone() as decimal[];
            this.A_b = A_b.Clone();
            this.A_b_Inverse = A_b_Inverse.Clone();
            this.knownTerms = knownTerms;
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

        
        public (bool isOptimal, int index, decimal value) OptTest()
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

            // se tutti sono <= 0 la base è ottima
            
            return z_j_c_j.Any(x => x > 0) ? (false, z_j_c_j.IndexOf(z_j_c_j.Max()), z_j_c_j.Max()) : (true, -1, -1);

        }
    
        public (bool isUnlimited, decimal[] yj) UnlimitednessTest(int j)
        {
            decimal[] yj = A_b_Inverse.Multiply(coefficientsMatrix.GetColumn(nonBaseVariables[j]));

            return yj.All(x => x <= 0) ? (true, yj) : (false, yj);
        }

        public (int index, decimal value) MinRatioTest(int index, decimal[] yj)
        {
            decimal[] bSigned = A_b_Inverse.Multiply(knownTerms);
            List<decimal> ratios = new List<decimal>();

            for (int i = 0; i < yj.Length; i++)
            {
                if (yj[i] > 0)
                {
                    decimal ratio = bSigned[i] / yj[i];
                    ratios.Add(ratio);
                }
                else
                {
                    ratios.Add(decimal.MaxValue); // Usiamo un valore massimo per evitare divisioni per zero
                }
            }

            decimal minRatio = ratios.Min();
            return (baseVariables[ratios.IndexOf(minRatio)], minRatio);
        }
    }
}
