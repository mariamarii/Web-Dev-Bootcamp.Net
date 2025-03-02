using System;

namespace R
{
    class R
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            long age = long.Parse(input);
            long year = age / 365;
            age = age % 365;
            long month = age / 30;
            long day = age % 30;
            Console.WriteLine($"{year} years\n{month} months\n{day} days");
            

        }
    }
}