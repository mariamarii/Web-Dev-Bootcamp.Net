using System;

namespace K
{
    class K
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string[] inputs = input.Split(' ');
            long number1 = long.Parse(inputs[0]);
            long number2 = long.Parse(inputs[1]);
            long number3 = long.Parse(inputs[2]);

            long minValue = Math.Min(number1, Math.Min(number2, number3));
            long maxValue = Math.Max(number1, Math.Max(number2, number3));

            Console.WriteLine($"{minValue} {maxValue}");
        }
    }
}