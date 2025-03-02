using System;

namespace Q
{
    class Q
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string[] inputs = input.Split(' ');
            double number1 = double.Parse(inputs[0]);
            double number2 = double.Parse(inputs[1]);
            if (number1 == 0 && number2 == 0)
            {
                Console.WriteLine("Origem");
            }
            else if (number1 == 0 && number2 != 0)
            {
                Console.WriteLine("Eixo Y");
            }
            else if (number1 != 0 && number2 == 0)
            {
                Console.WriteLine("Eixo X");
            }
            else if (number1 > 0 && number2 > 0)
            {
                Console.WriteLine("Q1");
            }
            else if (number1 > 0 && number2 < 0)
            {
                Console.WriteLine("Q4");
            }
            else if (number1 < 0 && number2 > 0)
            {
                Console.WriteLine("Q2");
            }
            else
            {
                Console.WriteLine("Q3");
            }
        }
    }
}