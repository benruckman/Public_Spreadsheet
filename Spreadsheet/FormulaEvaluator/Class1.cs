using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FormulaEvaluator
{
    public static class Evaluator
    {
        public delegate int Lookup(String v);


        /*
         * Method for evaluating intger arithmetic expressions
         * 
         */
        public static int Evaluate(String exp, Lookup variableEvaluator)
        {
            //Seperate String into Substrings (Code from PS1
            string[] substrings = Regex.Split(exp, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");

            //Declare Value and Operator Stack
            Stack<int> valueStack = new Stack<int>();
            Stack<int> operatorStack = new Stack<int>();

            //Iterate through tokens in substring
            foreach(String t in substrings)
            {
                //TODO: Implement algorithm
                //trim token of whitespace
                

            }
        }

        /*
         * Solves arithmetic expressions with two values and one operator
         * term1: left term in expression
         * term2: right term in expression
         * operation: String of operation to be applied, ***assumes whitespace has already been trimmed
         * throws argument expression
         */
        private static int simpleExpressionSolver (int term1, int term2, char operation)
        {
            if (operation == '+')
                return term1 + term2;
            else if (operation == '-')
                return term1 - term2;
            else if (operation == '*')
                return term1 * term2;
            else if (operation == '/')
                return term1 / term2;
            else
                throw new ArgumentException();
        }
            

        //TODO: Follow PS1 Instructions
    }

   
   
}
