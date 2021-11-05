using System;
using System.Collections.Generic;

namespace calculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<string> inputList = new List<string>() {
                "1 + 1",
                "2 * 2",
                "1 + 2 + 3",
                "6 / 2",
                "11 + 23",
                "11.1 + 23",
                "1 + 1 * 3",
                "( 11.5 + 15.4 ) + 10.1",
                "23 - ( 29.3 - 12.5 )",
                "( 1 / 2 ) - 1 + 1",
                "10 - ( 2 + 3 * ( 7 - 5 ) )", // nested bracket
                "2.1 ^ 3 + ( 11 - 19 ) * 4" // power operator
            };

            try 
            {
                foreach(var input in inputList)
                {
                    Console.WriteLine(input + " = " + Calculate(input));
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error is found: " + ex.Message);
            }
        }

        public static double Calculate(string input)
        {
            // split string input into array
            var inputArray = input.Split(" ");

            // declare 2 stacks used to store numbers and operators
            Stack<double> numberStack = new Stack<double>();
            Stack<string> operatorStack = new Stack<string>();

            foreach(var entry in inputArray)
            {
                double number;
                bool isDouble = Double.TryParse(entry, out number);

                if (isDouble == true)
                {
                    numberStack.Push(number);
                }
                else {
                    if (entry == "(")
                    {
                        operatorStack.Push(entry);
                    }
                    else if (entry == ")")
                    {
                        // there may be several operations to be done within parenthesis
                        // iterate until left parenthesis is found
                        while (operatorStack.Peek() != "("){
                            var op = operatorStack.Pop();
                            var num1 = numberStack.Pop();
                            var num2 = numberStack.Pop();

                            var result = PerformCalculation(op, num2, num1);

                            // insert result back to number stack for next calculation
                            numberStack.Push(result);
                        }

                        // remove "(" from stack
                        operatorStack.Pop();
                    }
                    else 
                    {
                        // if operatorStack is not empty and the current entry has lower precedence start calculate
                        while(operatorStack.Count != 0 && IsHigherPrecedence(operatorStack.Peek(), entry)){
                            var op = operatorStack.Pop();
                            var num1 = numberStack.Pop();
                            var num2 = numberStack.Pop();

                            var result = PerformCalculation(op, num2, num1);

                            // insert result back to number stack for next calculation
                            numberStack.Push(result);
                        }

                        operatorStack.Push(entry);
                    }
                }
            }

            // start calculating leftovers from the stack
            while(operatorStack.Count != 0)
            {
                var op = operatorStack.Pop();
                var num1 = numberStack.Pop();
                var num2 = numberStack.Pop();

                var result = PerformCalculation(op, num2, num1);

                // insert result back to number stack for next calculation
                numberStack.Push(result);
            }

            return Math.Round(numberStack.Pop(), 3);
        }

        private static double PerformCalculation(string op, double num1, double num2)
        {
            if (op == "+")
            {
                return num1 + num2;
            }
            else if (op == "-")
            {
                return num1 - num2;
            }
            else if (op == "*")
            {
                return num1 * num2;
            }
            else if (op == "/")
            {
                return num1 / num2;
            }
            else if (op == "^")
            {
                double result = num1;
                for (int i = 1; i < num2; i++)
                {
                    result = result * num1;
                }

                return result;
            }
            else
            {
                throw new ArgumentException("Unexpected operator: " + op);
            }
        }

        private static bool IsHigherPrecedence(string op1, string op2)
        {
            bool isOp1HigherPrecedence = true;

            // handles nested parenthesis
            if (op1 == "(" || op1 == ")") return false;

            // checks if op1 has lower precedence than op2
            // operator precedence: ^ >> * / >> - +
            if (((op1 == "+" || op1 == "-") && (op2 == "*" || op2 == "/")) 
                || (op2 == "^" && op1 != "^"))
            {
                isOp1HigherPrecedence =  false;
            }

            return isOp1HigherPrecedence;
        }
    }
}
