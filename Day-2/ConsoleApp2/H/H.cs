using System;

namespace H
{
    class H
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string[] inputs = input.Split(' ');
            long number1 = long.Parse(inputs[0]);
            long number2 = long.Parse(inputs[1]);

            Console.WriteLine($"floor {number1} / {number2} = {number1 / number2}");
            Console.WriteLine($"ceil {number1} / {number2} = {(number1 + number2 - 1) / number2}");
            Console.WriteLine($"round {number1} / {number2} = {Math.Round((double)number1 / number2, MidpointRounding.AwayFromZero)}");
        }
    }
}