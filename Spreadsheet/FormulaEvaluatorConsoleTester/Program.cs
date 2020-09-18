using System;
using System.Text.RegularExpressions;

namespace FormulaEvaluatorConsoleTester
{
    class Program
    {
        public static int variableEvaluator (String s)
        {
            return 2;
        }
        static void Main(string[] args)
        {
            Console.WriteLine(Regex.IsMatch("A_1", "^[a-zA-Z_]+[0-9]+$"));
            Console.ReadKey();
        }

        
    }
}
