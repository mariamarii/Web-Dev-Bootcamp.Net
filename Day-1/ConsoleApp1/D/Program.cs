using System;
namespace D{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string[] Input = input.Split(' ');

            long number1 = long.Parse(Input[0]);
            long number2 = long.Parse(Input[1]);
            long number3 = long.Parse(Input[2]);
            long number4 = long.Parse(Input[3]);
          

            Console.WriteLine($"Difference = {(number1*number2)-(number3*number4)}");
   
        }
    }
}