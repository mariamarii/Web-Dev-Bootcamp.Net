using System;

namespace M
{
    class M
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine(); 
            char inputchar= Convert.ToChar(input);
            if(char.IsUpper(inputchar))
                Console.WriteLine(char.ToLower(inputchar));
            else
            {
                Console.WriteLine(char.ToUpper(inputchar));
            }
            

            
        }
    }
}