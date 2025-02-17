using System;
namespace F{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string[] Input = input.Split(' ');
            char lastdigit1 =(Input[0][Input[0].Length-1]);
            long number1 = Convert.ToInt64(lastdigit1-'0');
            char lastdigit2 =(Input[1][Input[1].Length-1]);
            long number2 = Convert.ToInt64(lastdigit2-'0');
            
           
          

            Console.WriteLine(number1+number2);
   
        }
    }
}