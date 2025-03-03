using System;

namespace O
{
    class O
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            char[] operators = { '+', '-', '*', '/' };
            int opIndex = input.IndexOfAny(operators);
            string num1Str = input.Substring(0, opIndex);
            string op = input.Substring(opIndex, 1);
            string num2Str = input.Substring(opIndex + 1);
            long num1 = long.Parse(num1Str);
            long num2 = long.Parse(num2Str);


            switch (op)
            {
                case "+":
                    Console.WriteLine(num1 + num2);
                    break;
                case "-":
                    Console.WriteLine(num1 - num2);
                    break;
                case "*":
                    Console.WriteLine(num1 * num2);
                    break;
                case "/":
                    Console.WriteLine(num1 / num2); 
                    break;
                
            }
            
           

            
        }
    }
}