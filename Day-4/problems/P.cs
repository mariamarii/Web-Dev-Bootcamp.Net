using System;

namespace P
{
    class P
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine(); 
            char inputchar= Convert.ToChar(input[0]);
            int number = Convert.ToInt32(inputchar-'0');
            if(number%2==0)
                Console.WriteLine("EVEN");
            else
            {
                Console.WriteLine("ODD");
            }
            

            
        }
    }
}