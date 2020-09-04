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
            Stack<String> operatorStack = new Stack<String>();

            //Iterate through tokens in substring
            foreach(String t in substrings)
            {
                //If t is an integer
                if(Regex.IsMatch(t.Trim(), "[0-9]+"))
                {
                    IntegerHandler(operatorStack, valueStack, int.Parse(t));
                }
                //If t is a variable
                else if (Regex.IsMatch(t, "^[a-zA-Z]+[0-9]+$"))
                {
                    IntegerHandler(operatorStack, valueStack, variableEvaluator(t));
                }
                //If t is a + or -

            }
        }

        /*
         * Solves arithmetic expressions with two values and one operator
         * term1: left term in expression
         * term2: right term in expression
         * operation: String of operation to be applied, ***assumes whitespace has already been trimmed
         * throws argument expression
         */
        private static int SimpleExpressionSolver (int term1, int term2, String operation)
        {
            if (operation == "+")
                return term1 + term2;
            else if (operation == "-")
                return term1 - term2;
            else if (operation == "*")
                return term1 * term2;
            else if (operation == "/")
                return term1 / term2;
            else
                throw new ArgumentException();
        }

        /*
         * Handles algorithm when current token is an integer
         * operatorStack: reference to stack containing all of the seen operators in expression
         * valueStack: reference to stack containg all of the seen values in expression
         */
        private static void IntegerHandler (Stack<String> operatorStack, Stack<int> valueStack, int currentToken)
        {
            if (operatorStack.Peek() == "/" | operatorStack.Peek() == "*")
            {
                valueStack.Push(SimpleExpressionSolver(valueStack.Pop(), currentToken, operatorStack.Pop()));
            }
            else valueStack.Push(currentToken);
        }
            

        //TODO: Follow PS1 Instructions
    }

   
   
}
