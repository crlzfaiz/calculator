using System;
using System.Collections.Generic;

namespace calculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(Calculate("( 2 + 3 * ( 7 - 5 ) ) - -10"));
            
            // while loop to check if continue running
        }

        public static double Calculate(string sum){
            // split string into array
            var dataArray = sum.Split(" ");

            // declare 2 stacks used to store values and operators
            Stack<double> valueStack = new Stack<double>();
            Stack<string> operatorStack = new Stack<string>();

            foreach(var data in dataArray){
                double value;
                bool isDouble = Double.TryParse(data, out value);

                if (isDouble == true){
                    valueStack.Push(value);
                }
                else {
                    if (data == "("){
                        operatorStack.Push(data);
                    }
                    else if (data == ")") {
                        // there may be several operations to be done within bracket
                        // check for the opening bracket to check if anymore calculation required
                        while (operatorStack.Peek() != "("){
                            var operand = operatorStack.Pop();
                            var num1 = valueStack.Pop();
                            var num2 = valueStack.Pop();

                            // TOOD: check if multiplication/division LHS 
                            var result = PerformCalculation(operand, num2, num1);

                            // insert result back to value stack for next calculation
                            valueStack.Push(result);
                        }

                        // remove "(" from stack
                        operatorStack.Pop();
                    }
                    else {
                        while(operatorStack.Count != 0 && IsHigherPrecedence(operatorStack.Peek(), data)){
                            var operand = operatorStack.Pop();
                            var num1 = valueStack.Pop();
                            var num2 = valueStack.Pop();

                            // TOOD: check if multiplication/division LHS 
                            var result = PerformCalculation(operand, num2, num1);

                            // insert result back to value stack for next calculation
                            valueStack.Push(result);
                        }

                        operatorStack.Push(data);
                    }
                }
            }

            // start calculating leftovers from the stack
            while(operatorStack.Count != 0){
                var operand = operatorStack.Pop();
                var num1 = valueStack.Pop();
                var num2 = valueStack.Pop();

                // TOOD: check if multiplication/division LHS 
                var result = PerformCalculation(operand, num2, num1);

                // insert result back to value stack for next calculation
                valueStack.Push(result);
            }

            return valueStack.Pop();
        }

        private static double PerformCalculation(string operators, double num1, double num2){
            if (operators == "+")
            {
                return num1 + num2;
            }
            else if (operators == "-")
            {
                return num1 - num2;
            }
            else if (operators == "*")
            {
                return num1 * num2;
            }
            else if (operators == "/")
            {
                return num1 / num2;
            }
            else
            {
                throw new ArgumentException("Unexpected operator: " + operators);
            }
        }

        private static bool IsHigherPrecedence(string operator1, string operator2){
            bool operator1HigherPrecedence = true;

            if (operator1 == "(" || operator1 == ")") return false;

            bool isOperator1PlusMinus = 
                operator1 == "+" || operator1 == "-" ? true : false;

            bool isOperator2PlusMinus = 
                operator2 == "+" || operator2 == "-" ? true : false;

            if (isOperator1PlusMinus == true && isOperator2PlusMinus == false){
                operator1HigherPrecedence =  false;
            }

            return operator1HigherPrecedence;
        }
    }
}
