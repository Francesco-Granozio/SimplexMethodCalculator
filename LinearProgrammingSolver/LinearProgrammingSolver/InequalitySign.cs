using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearProgrammingSolver
{
    public enum InequalitySign
    {
        LessThanOrEqual,    // <=
        GreaterThanOrEqual, // >=
        Equal              // =
    }

    public static class InequalitySignMapper
    {
        public static InequalitySign ToInequalitySign(this string sign)
        {
            switch (sign)
            {
                case "<=":
                    return InequalitySign.LessThanOrEqual;
                case ">=":
                    return InequalitySign.GreaterThanOrEqual;
                case "=":
                    return InequalitySign.Equal;
                default:
                    throw new ArgumentException($"Invalid inequality sign: {sign}");
            }
        }
    }

}
