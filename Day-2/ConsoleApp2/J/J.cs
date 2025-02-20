using System;
namespace I
{

    class I
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string[] inputs = input.Split(' ');
            long number1 = long.Parse(inputs[0]);
            long number2 = long.Parse(inputs[1]);
            
            if(number1 %  number2 == 0 || number2 % number1 == 0)
                Console.WriteLine("Multiples");
            else
                Console.WriteLine("No Multiples");
            
                
            
        }
    }
}