using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearProgrammingSolver
{
    internal class LinearProgrammingProblem
    {
        public Equation ObjectiveFunction { get; private set; }
        public bool IsMinFunction { get; private set; }
        public Inequality[] Costraints { get; private set; } 
        public int TotalVariables { get; private set; }
        public int TotalCostraints { get; private set; }
        public InequalitySign[] VariablesSignCostraints { get; private set; }

        public LinearProgrammingProblem(Equation objectiveFunction, bool isMinFunction, Inequality[] costraints, 
            int totalVariables, int totalCostraints, InequalitySign[] variablesSignCostraints)
        {
            ObjectiveFunction = objectiveFunction;
            IsMinFunction = isMinFunction;
            Costraints = costraints;
            TotalVariables = TotalVariables;
            TotalCostraints = totalCostraints;
            VariablesSignCostraints = variablesSignCostraints;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            

            return sb.ToString();
        }
    }
}

