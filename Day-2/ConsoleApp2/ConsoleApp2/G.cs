using System;

namespace ConsoleApp2
{

    class G
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            long number = long.Parse(input);
            Console.WriteLine((number * (number + 1)) / 2);


        }
    }
}