using System;

namespace L
{
    class L
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string[] inputs = input.Split(' ');
            string father1 = inputs[1];
            input = Console.ReadLine();
            inputs = input.Split(' ');
            string father2 = inputs[1];
            
            if (father1 == father2)
                Console.WriteLine("ARE Brothers");
            else
            {
                Console.WriteLine("NOT");
            }

  
        }
    }
}