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
        private List<int> baseVariables = new List<int>();
        private List<int> nonBaseVariables = new List<int>();
        private decimal[] c_b_Transposed;
        private CoefficientsMatrix A_b;
        private CoefficientsMatrix A_b_Inverse;
        private bool isSolved = false;

        public SimplexSolver(LinearProgrammingProblem lp)
        {
            this.nonStandardLp = lp.Clone();
            this.standardLp = lp.Clone();
        }

        public void Solve()
        {
            ConvertProblemToStandardForm();
            SetupSimplex();

            // cerco una base ammissibile
            //TODO: implementare il metodo delle 2 fasi

            FindBase();

            // creo la matrice di base

            A_b = new CoefficientsMatrix(standardLp, baseVariables);

            A_b_Inverse = A_b.Clone().Invert();


            Simplex simplex = new Simplex(coefficientsMatrix, cTransposed, baseVariables, nonBaseVariables, c_b_Transposed, A_b, A_b_Inverse, standardLp.KnownTerms);

            (bool isOptimal, int index, decimal value) = simplex.OptTest();

            Console.WriteLine(isOptimal ? $"The base is optimal" : $"x{index + 1} goes out of the base with value: {value}");

            if (!isOptimal)
            {
                (bool isUnlimited, decimal[] yj) = simplex.UnlimitednessTest(index);

                Console.WriteLine(isUnlimited ? $"The problem is unlimited" : $"yj = {yj}");

                if (!isUnlimited)
                {
                    (int exitIndex, decimal enteringValue) = simplex.MinRatioTest(index, yj);
                    Console.WriteLine($"x{exitIndex + 1} goes out of the base x{index + 1} goes in the base with value: {enteringValue}");
                }
            }
                

            isSolved = true;
        }


        private void SetupSimplex()
        {
            //costruisco la matrice dei coefficienti tecnologici e il vettore c dei coefficienti di costo

            coefficientsMatrix = new CoefficientsMatrix(standardLp);

            cTransposed = standardLp.ObjectiveFunction.Coefficients.Concat(
                Enumerable.Repeat(0m, standardLp.TotalVariables - standardLp.ObjectiveFunction.TotalVariables)).ToArray();
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
        }

        private void FindBase()
        {
            int numCols = coefficientsMatrix.TotalColumns;
            int identityMatrixSize = standardLp.Costraints.Length;
            
            for (int i = 0; i < numCols; i++)
            {
                decimal[] currentColumn = coefficientsMatrix.GetColumn(i);

                for (int rowIndex = 0; rowIndex < identityMatrixSize; rowIndex++)
                {
                    if (currentColumn[rowIndex] == 1)
                    {
                        for (int j = 0; j < currentColumn.Length; j++)
                        {
                            if (j != rowIndex && currentColumn[j] != 0)
                            {
                                break;
                            }
                            else if (j == currentColumn.Length - 1)
                            {
                                baseVariables.Add(i);
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < numCols; i++)
            {
                if (!baseVariables.Contains(i))
                {
                    nonBaseVariables.Add(i);
                }
            }
            c_b_Transposed = cTransposed.Where((x, i) => baseVariables.Contains(i)).ToArray();
        }

        private void AddSlackAndSurplusVariables()
        {
            int numConstraints = nonStandardLp.Costraints.Length;
            int numAddedVariables = 0; // Contatore delle variabili di slack/surplus aggiunte

            for (int i = 0; i < numConstraints; i++)
            {
                if (nonStandardLp.Costraints[i].InequalityType == InequalitySign.LessThanOrEqual)
                {
                    for (int j = 0; j < numAddedVariables; j++)
                    {
                        standardLp.Costraints[i].Coefficients.Add(0);
                    }

                    standardLp.Costraints[i].Coefficients.Add(1);
                    numAddedVariables++;
                    standardLp.Costraints[i].InequalityType = InequalitySign.Equal;
                    standardLp.TotalVariables++;
                }
                else if (nonStandardLp.Costraints[i].InequalityType == InequalitySign.GreaterThanOrEqual)
                {
                    for (int j = 0; j < numAddedVariables; j++)
                    {
                        standardLp.Costraints[i].Coefficients.Add(0);
                    }

                    standardLp.Costraints[i].Coefficients.Add(-1);
                    numAddedVariables++;
                    standardLp.Costraints[i].InequalityType = InequalitySign.Equal;
                    standardLp.TotalVariables++;
                }
            }

            // Aggiungi zeri rimanenti solo dopo l'aggiunta delle variabili di slack/surplus

            for (int i = 0, numZerosToAdd = standardLp.TotalVariables - nonStandardLp.TotalVariables; i < numConstraints; i++)
            {
                for (int j = 0; j < numZerosToAdd; j++, numZerosToAdd--)
                {
                    standardLp.Costraints[i].Coefficients.Add(0);
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

            sb.Append("\n\nNon base variables index:\n\n");
            sb.Append("[").Append(nonBaseVariables.Any() ? string.Join(", ", nonBaseVariables) : "").Append("]");

            sb.Append("\n\nCost coefficients of base variables (c_b^T):\n\n");
            sb.Append("[").Append(c_b_Transposed.Any() ? string.Join(", ", c_b_Transposed) : "").Append("]");

            sb.Append("\n\nCoefficients matrix of base variables (A_b):\n\n");
            sb.Append(A_b);

            return sb.ToString();
        }

    }
}
