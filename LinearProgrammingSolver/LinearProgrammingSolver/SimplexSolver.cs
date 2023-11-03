using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearProgrammingSolver
{
    internal class SimplexSolver
    {
        private LinearProgrammingProblem nonStandardLp, standardLp;
        private bool hasObjectiveFunctionSignChanged = false;
        private CoefficientsMatrix coefficientsMatrix;
        private decimal[] cTransposed;
        private int[] baseVariables;
        private int[] nonBaseVariables;
        private bool isSolved = false;

        public SimplexSolver(LinearProgrammingProblem lp)
        {
            this.nonStandardLp = lp.Clone();
            this.standardLp = lp.Clone();
        }

        public void Solve()
        {
            ConvertProblemToStandardForm();
        }

        private void ConvertProblemToStandardForm()
        {

            // innanzitutto controllo se la funzione è di max, e nel caso la trasformo in -min -f(x)

            if (!standardLp.IsMinFunction && !hasObjectiveFunctionSignChanged)
            {
                hasObjectiveFunctionSignChanged = true;
                standardLp.ObjectiveFunction.InvertSigns();
            }

            // trasformo le equazioni e i relativi termini noti >= 0
            
            for (int i = 0; i < standardLp.Costraints.Length; i++)
            {
                if (standardLp.Costraints[i].KnownTerm < 0)
                {
                    standardLp.Costraints[i].InvertSigns();
                }
            }

            // aggiungo le variabili di slack e di surplus
            
            AddSlackAndSurplusVariables();

            //costruisco la matrice dei coefficienti tecnologici e il vettore c dei coefficienti di costo

            coefficientsMatrix = new CoefficientsMatrix(standardLp);

            cTransposed = standardLp.ObjectiveFunction.Coefficients.Concat(
                Enumerable.Repeat(0m, standardLp.TotalVariables - standardLp.ObjectiveFunction.TotalVariables)).ToArray();

            // cerco una base ammissibile
            //TODO: implementare il metodo delle 2 fasi
            
            (baseVariables, nonBaseVariables) = FindBase();

           
            isSolved = true;
        }

        private (int[], int[]) FindBase()
        {
            int numRows = coefficientsMatrix.TotalRows;
            int numCols = coefficientsMatrix.TotalColumns;
            int numIdentityColumns = Math.Min(numRows, numCols);

            List<int> baseColumns = new List<int>();
            List<int> nonBaseColumns = new List<int>();

            for (int col = 0; col < numIdentityColumns; col++)
            {
                // Verifica se la colonna è una colonna della matrice identità
                bool isBaseColumn = true;

                for (int row = 0; row < numRows; row++)
                {
                    decimal element = coefficientsMatrix[row, col];

                    if (row == col)
                    {
                        if (element != 1)
                        {
                            isBaseColumn = false;
                            break;
                        }
                    }
                    else
                    {
                        if (element != 0)
                        {
                            isBaseColumn = false;
                            break;
                        }
                    }
                }

                if (isBaseColumn)
                {
                    baseColumns.Add(col);
                }
                else
                {
                    nonBaseColumns.Add(col);
                }
            }

            Console.WriteLine("Base columns: " + string.Join(", ", baseColumns));
            return (baseColumns.ToArray(), nonBaseColumns.ToArray());
        }


        private void AddSlackAndSurplusVariables()
        {
            for (int i = 0, index = 0; i < nonStandardLp.Costraints.Length; i++)
            {
                if (nonStandardLp.Costraints[i].InequalityType == InequalitySign.LessThanOrEqual)
                {
                    for (int j = 0; j < index; j++)
                    {
                        standardLp.Costraints[i].Coefficients.Add(0);
                    }

                    standardLp.Costraints[i].Coefficients.Add(1);
                    index++;
                    standardLp.Costraints[i].InequalityType = InequalitySign.Equal;
                    standardLp.TotalVariables++;
                }
                else if (nonStandardLp.Costraints[i].InequalityType == InequalitySign.GreaterThanOrEqual)
                {

                    for (int j = 0; j < index; j++)
                    {
                        standardLp.Costraints[i].Coefficients.Add(0);
                    }
                    
                    standardLp.Costraints[i].Coefficients.Add(-1);
                    index++;
                    standardLp.Costraints[i].InequalityType = InequalitySign.Equal;
                    standardLp.TotalVariables++;
                }
            }
        }

        public override string ToString()
        {
            if (!isSolved)
            {
                return "The problem has not been solved yet";
            }

            StringBuilder sb = new StringBuilder(nonStandardLp.ToString());
            sb.Append("\n\nconverted in standard form:\n\n");
            sb.Append(hasObjectiveFunctionSignChanged ? standardLp.ToString().Replace("max ", "-min ") : standardLp.ToString());

            sb.Append("\n\nCoefficients matrix (A):\n\n");
            sb.Append(coefficientsMatrix);

            sb.Append("\n\nCost coefficients (c^T):\n\n");
            sb.Append("[").Append(cTransposed.Any() ? string.Join(", ", cTransposed) : "").Append("]");


            sb.Append("\n\nBase variables index:\n\n");
            sb.Append("[").Append(baseVariables.Any() ? string.Join(", ", baseVariables) : "").Append("]");

            return sb.ToString();
        }

    }
}
