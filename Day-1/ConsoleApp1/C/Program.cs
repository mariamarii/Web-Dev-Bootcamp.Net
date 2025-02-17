
namespace C{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string[] Input = input.Split(' ');

            long number1 = long.Parse(Input[0]);
            long number2 = long.Parse(Input[1]);
          

            Console.WriteLine($"{number1} + {number2} = {number1 + number2}");
            Console.WriteLine($"{number1} * {number2} = {number1 * number2}");
            Console.WriteLine($"{number1} - {number2} = {number1 - number2}");
          
        }
    }
}