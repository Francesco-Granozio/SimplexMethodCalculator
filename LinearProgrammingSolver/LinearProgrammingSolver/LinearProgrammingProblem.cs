using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearProgrammingSolver
{
    public class LinearProgrammingProblem
    {
        public Equation ObjectiveFunction { get; private set; }
        public bool IsMinFunction { get; set; }
        public Inequality[] Costraints { get; set; } 
        public int TotalVariables { get; set; }
        public int TotalCostraints { get; private set; }
        
        public decimal[] KnownTerms
        {
            get
            {
                decimal[] knownTerms = new decimal[Costraints.Length];

                for (int i = 0; i < Costraints.Length; i++)
                {
                    knownTerms[i] = Costraints[i].KnownTerm;
                }

                return knownTerms;
            }
        }

        public LinearProgrammingProblem(Equation objectiveFunction, bool isMinFunction, Inequality[] costraints, 
            int totalVariables, int totalCostraints)
        {
            ObjectiveFunction = objectiveFunction;
            IsMinFunction = isMinFunction;
            Costraints = costraints;
            TotalVariables = totalVariables;
            TotalCostraints = totalCostraints;

            for (int i = 0; i < Costraints.Length; i++)
            {
                if (Costraints[i].Coefficients.Count < TotalVariables)
                {
                    Costraints[i].Coefficients.AddRange(Enumerable.Repeat(0m, TotalVariables - Costraints[i].Coefficients.Count));
                }
            }
            Console.WriteLine(this);
        }

        public LinearProgrammingProblem Clone()
        {
            Equation clonedObjectiveFunction = ObjectiveFunction.Clone();
            Inequality[] clonedCostraints = Costraints.Select(c => c.Clone()).ToArray();

            return new LinearProgrammingProblem(clonedObjectiveFunction, IsMinFunction, clonedCostraints, TotalVariables, TotalCostraints);
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(IsMinFunction ? "min Z = " : "max Z = ");

            sb.Append(ObjectiveFunction.ToString().Split('=')[0]).Append("\nsubject to\n");
            
            foreach (var costraint in Costraints)
            {
                sb.Append(costraint).Append("\n");
            }
            
            sb.Append("and ").Append(string.Join(", ", Enumerable.Range(1, TotalVariables).Select(i => $"x{i} >= 0")));
            
            return sb.ToString();
        }
    }

}

