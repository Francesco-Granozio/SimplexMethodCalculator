using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearProgrammingSolver
{
    internal class CoefficientsMatrix
    {
        public decimal[,] Coefficients { get; set; }
        public int TotalRows { get => Coefficients.GetLength(0); }
        public int TotalColumns { get => Coefficients.GetLength(1); }

        public CoefficientsMatrix(LinearProgrammingProblem lp)
        {

            int numConstraints = lp.Costraints.Length;
            int numVariables = lp.TotalVariables;

            Coefficients = new decimal[numConstraints, numVariables];

            foreach (var constraint in lp.Costraints)
            {
                int constraintIndex = Array.IndexOf(lp.Costraints, constraint);

                for (int i = 0; i < constraint.Coefficients.Count; i++)
                {
                    Coefficients[constraintIndex, i] = constraint.Coefficients[i];
                }
            }
        }

        public CoefficientsMatrix(LinearProgrammingProblem lp, List<int> columns)
        {
            int numConstraints = lp.Costraints.Length;
            int numVariables = columns.Count;

            foreach (var constraint in lp.Costraints)
            {
                Console.WriteLine($"Constraint: {constraint}");
                for (int i = 0; i < columns.Count; i++)
                {
                    Console.WriteLine($"Indice variabili base: {columns[i]}");
                    Console.WriteLine(constraint["x" + columns[i]]);
                }
                
            }
        }

        public decimal this[int i, int j]
        {
            get
            {
                return Coefficients[i, j];
            }
            set
            {
                Coefficients[i, j] = value;
            }
        }

        public decimal[] GetRow(int rowIndex)
        {
            int numCols = Coefficients.GetLength(1);
            decimal[] row = new decimal[numCols];

            for (int j = 0; j < numCols; j++)
            {
                row[j] = Coefficients[rowIndex, j];
            }

            return row;
        }

        public decimal[] GetColumn(int columnIndex)
        {
            int numRows = Coefficients.GetLength(0);
            decimal[] column = new decimal[numRows];

            for (int i = 0; i < numRows; i++)
            {
                column[i] = Coefficients[i, columnIndex];
            }

            return column;
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            int numRows = Coefficients.GetLength(0);
            int numCols = Coefficients.GetLength(1);

            for (int i = 0; i < numRows; i++)
            {
                if (i > 0)
                {
                    sb.AppendLine();
                }

                sb.Append("[");
                for (int j = 0; j < numCols; j++)
                {
                    sb.Append(Coefficients[i, j]);

                    if (j < numCols - 1)
                    {
                        sb.Append(", ");
                    }
                }
                sb.Append("]");
            }

            sb.Append("]");
            return sb.ToString();
        }

    }
}
