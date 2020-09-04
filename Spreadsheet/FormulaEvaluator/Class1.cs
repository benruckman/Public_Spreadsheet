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
            String currentToken;
            foreach(String t in substrings)
            {
                currentToken = t.Trim();

                if (!Regex.IsMatch(currentToken, "^[a-zA-Z]+[0-9]+$")
                    && !Regex.IsMatch(currentToken, "^[0-9]+")
                    && currentToken != "*" && currentToken != "/" && currentToken != "+" 
                    && currentToken != "-" && currentToken != "(" && currentToken != ")" && currentToken != "")
                {
                    throw new ArgumentException("Invalid Character in Expression");
                }

                //If currentToken is an integer
                if(Regex.IsMatch(currentToken, "^[0-9]+"))
                {
                    IntegerHandler(operatorStack, valueStack, int.Parse(currentToken));
                }
                //If currentToken is a variable
                else if(Regex.IsMatch(currentToken, "^[a-zA-Z]+[0-9]+$"))
                {
                    IntegerHandler(operatorStack, valueStack, variableEvaluator(currentToken));
                }
                //If currentToken is a + or -
                else if(currentToken == "+" || currentToken == "-")
                {
                    AdditionSubtractionHandler(operatorStack, valueStack);
                    operatorStack.Push(currentToken);
                }
                //If currentToken is * or /
                else if(currentToken == "*" | currentToken == "/")
                {
                    operatorStack.Push(currentToken);
                }
                //If currentToken is a (
                else if(currentToken == "(")
                {
                    operatorStack.Push(currentToken);
                }
                //If currentToken is a )
                else if(currentToken == ")")
                {
                    ClosedParenthesesHandler(operatorStack, valueStack);
                }
            }

            //After every token has been gone through
            if(operatorStack.Count() == 0)
            {
                return valueStack.Pop();
            }
            else
            {
                AdditionSubtractionHandler(operatorStack, valueStack);
                return valueStack.Pop();
            }
        }

        /*
         * Solves arithmetic expressions with two values and one operator
         * term1: left term in expression
         * term2: right term in expression
         * operation: String of operation to be applied
         * throws argument expression
         */
        private static int SimpleExpressionSolver (int term1, int term2, String operation)
        {
            if (operation == "+")
            {
                return term1 + term2;
            }
            else if (operation == "-")
            {
                return term1 - term2;
            }
            else if (operation == "*")
            {
                return term1 * term2;
            }
            else if (operation == "/")
            {
                return term1 / term2;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        /*
         * Handles algorithm when current token is an integer
         * operatorStack: reference to stack containing all of the seen operators in expression
         * valueStack: reference to stack containg all of the seen values in expression
         * currentToken
         */
        private static void IntegerHandler (Stack<String> operatorStack, Stack<int> valueStack, int currentToken)
        {
            if (operatorStack.Count() > 0 && (operatorStack.Peek() == "/" | operatorStack.Peek() == "*"))
            {
                valueStack.Push(SimpleExpressionSolver(valueStack.Pop(), currentToken, operatorStack.Pop()));
            }
            else
            {
                valueStack.Push(currentToken);
            }
        }

        /*
         * Handles algorithm when current token is either + or -
         * operatorStack: reference to stack containing all of the seen operators in expression
         * valueStack: reference to stack containg all of the seen values in expression
         * currentToken
         */
        private static void AdditionSubtractionHandler(Stack<String> operatorStack, Stack<int> valueStack)
        {
            if (operatorStack.Count() == 0)
            {
                return;
            }
            else if (operatorStack.Peek() == "(")
            {
                return;
            }
            else if (operatorStack.Peek() == "+" | operatorStack.Peek() == "-")
            {
                //pop'd in this order to ensure left to right order is maintened
                int term2 = valueStack.Pop();
                int term1 = valueStack.Pop();
                valueStack.Push(SimpleExpressionSolver(term1, term2, operatorStack.Pop()));
            }
            else 
            { 
                throw new ArgumentException("Error, wrong expression found in operator stack"); 
            }
        }

        private static void ClosedParenthesesHandler(Stack<String> operatorStack, Stack<int> valueStack)
        {
            if(operatorStack.Count() > 0 && operatorStack.Peek() == "+" | operatorStack.Peek() == "-")
            {
                //pop'd in this order to ensure left to right order is maintened
                int term2 = valueStack.Pop();
                int term1 = valueStack.Pop();
                valueStack.Push(SimpleExpressionSolver(term1, term2, operatorStack.Pop()));
            }

            //Operator should be (
            if (operatorStack.Pop() != "(")
            {
                throw new ArgumentException("Error, ( parenthese did not appear when it should have");
            }

            if(operatorStack.Count() > 0 && operatorStack.Peek() == "*" | operatorStack.Peek() == "/")
            {
                valueStack.Push(SimpleExpressionSolver(valueStack.Pop(), valueStack.Pop(), operatorStack.Pop()));
            }
        }
    }

   
   
}
