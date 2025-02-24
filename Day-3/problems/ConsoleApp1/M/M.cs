using System;

namespace M
{
    class M
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine(); 
            char ascii= Convert.ToChar(input);
            

            if (ascii >= 48 && ascii <= 57)
            {
                Console.WriteLine("IS DIGIT");
            }
            else if (ascii >= 65 && ascii <= 90)
            {
                Console.WriteLine("ALPHA");
                Console.WriteLine("IS CAPITAL");
            }
            else 
            {
                Console.WriteLine("ALPHA");
                Console.WriteLine("IS SMALL");
            }
            
        }
    }
}