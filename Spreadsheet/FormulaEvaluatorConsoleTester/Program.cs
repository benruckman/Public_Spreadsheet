using System;

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
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("2+1+2", variableEvaluator));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("2-1+2", variableEvaluator));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("3-2-1", variableEvaluator));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("2+1+2-3-3", variableEvaluator));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("2+1+a4", variableEvaluator));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("2+1+a4-bb67", variableEvaluator));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("2*2", variableEvaluator));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("2*2+1", variableEvaluator));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("1+2*2", variableEvaluator));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("2*2-1", variableEvaluator));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("1-2*2", variableEvaluator));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("4/2+1", variableEvaluator));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("1+6/2", variableEvaluator));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("8/3-1", variableEvaluator));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("1-7/5", variableEvaluator));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("(2+1)*2", variableEvaluator));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("2+(2-1)", variableEvaluator));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("( 2 / 2) (2+1) ", variableEvaluator));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("((2+1)+1)*3", variableEvaluator));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("((2+a4)+1)*3", variableEvaluator));
            //Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("((2+a)+1)*3", variableEvaluator));
            Console.WriteLine("test");
            //Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("2 / (3*2 - 6)", variableEvaluator));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("6 / 3", variableEvaluator));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("6 / yy18", variableEvaluator));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("10 / 0", variableEvaluator));
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        
    }
}
