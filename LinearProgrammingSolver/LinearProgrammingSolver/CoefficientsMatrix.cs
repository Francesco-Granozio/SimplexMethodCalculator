using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearProgrammingSolver
{
    public class CoefficientsMatrix
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

            Coefficients = new decimal[numConstraints, numVariables];

            foreach (var constraint in lp.Costraints)
            {
                int constraintIndex = Array.IndexOf(lp.Costraints, constraint);
                
                for (int i = 0; i < columns.Count; i++)
                {
                    Coefficients[constraintIndex, i] = constraint.Coefficients[columns[i]];
                    
                }
                
            }
        }

        public CoefficientsMatrix(decimal[,] coefficients)
        {
             Coefficients = coefficients;
        }

        public CoefficientsMatrix Clone()
        {
            return new CoefficientsMatrix(Coefficients.Clone() as decimal[,]);
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
                    sb.Append(Coefficients[i, j].ToString("0.##"));

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

    public static class CoefficientsMatrixExtension
    {

        public static decimal[] Multiply(this CoefficientsMatrix matrix, decimal[] vector)
        {
            int numRows = matrix.TotalRows;
            int numCols = matrix.TotalColumns;

            if (numCols != vector.Length)
            {
                throw new ArgumentException("The number of columns in the matrix must match the length of the vector for multiplication.");
            }

            decimal[] result = new decimal[numRows];

            for (int i = 0; i < numRows; i++)
            {
                decimal sum = 0;
                for (int j = 0; j < numCols; j++)
                {
                    sum += matrix[i, j] * vector[j];
                }
                result[i] = sum;
            }

            return result;
        }

        public static decimal Multiply(this decimal[] vector1, decimal[] vector2)
        {
            if (vector1.Length != vector2.Length)
            {
                throw new ArgumentException("Both vectors must have the same length for multiplication.");
            }

            decimal result = 0;

            for (int i = 0; i < vector1.Length; i++)
            {
                result += vector1[i] * vector2[i];
            }

            return result;
        }

        public static decimal[] Multiply(this decimal[] vector, CoefficientsMatrix matrix)
        {
            int vectorLength = vector.Length;
            int matrixColumns = matrix.TotalColumns;

            if (vectorLength != matrix.Coefficients.GetLength(0))
            {
                throw new ArgumentException("The vector length must be equal to the number of matrix rows.");
            }

            decimal[] result = new decimal[matrixColumns];

            for (int j = 0; j < matrixColumns; j++)
            {
                decimal dotProduct = 0;
                for (int i = 0; i < vectorLength; i++)
                {
                    dotProduct += vector[i] * matrix[i, j];
                }
                result[j] = dotProduct;
            }

            return result;
        }

        public static CoefficientsMatrix Invert(this CoefficientsMatrix matrix)
        {
            int n = matrix.Coefficients.GetLength(0);
            if (n != matrix.Coefficients.GetLength(1))
            {
                throw new InvalidOperationException("The matrix is not square.");
            }

            decimal[,] augmentedMatrix = new decimal[n, 2 * n];

            // Copy the original matrix and create an augmented matrix with the identity matrix
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    augmentedMatrix[i, j] = matrix.Coefficients[i, j];
                    augmentedMatrix[i, j + n] = (i == j) ? 1 : 0;
                }
            }

            // Perform Gaussian elimination to transform the left side of the augmented matrix into the identity matrix
            for (int i = 0; i < n; i++)
            {
                decimal pivot = augmentedMatrix[i, i];
                if (pivot == 0)
                {
                    throw new InvalidOperationException("The matrix is singular and cannot be inverted.");
                }

                for (int j = 0; j < 2 * n; j++)
                {
                    augmentedMatrix[i, j] /= pivot;
                }

                for (int k = 0; k < n; k++)
                {
                    if (k != i)
                    {
                        decimal factor = augmentedMatrix[k, i];
                        for (int j = 0; j < 2 * n; j++)
                        {
                            augmentedMatrix[k, j] -= factor * augmentedMatrix[i, j];
                        }
                    }
                }
            }

            decimal[,] invertedMatrix = new decimal[n, n];

            // Extract the inverted matrix from the right side of the augmented matrix
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    invertedMatrix[i, j] = augmentedMatrix[i, j + n];
                }
            }

            return new CoefficientsMatrix(invertedMatrix);
        }
    }

}
