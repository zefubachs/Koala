using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEngine.Functions;
public class StringFunctions
{
    [Function]
    public static string Lower(string input) => input.ToLower();
    [Function]
    public static string Upper(string input) => input.ToUpper();
}
