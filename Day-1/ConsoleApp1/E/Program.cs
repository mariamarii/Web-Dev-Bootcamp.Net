using System;


namespace E{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            double number1 = double.Parse(input);
            const double PI = 3.141592653;
            Console.WriteLine($"{number1*number1*PI:F9}");

        }
    }
}