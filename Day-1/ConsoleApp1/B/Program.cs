
namespace B{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string[] Input = input.Split(' ');

            int number1 = int.Parse(Input[0]);
            long number2 = long.Parse(Input[1]);
            char number3 = char.Parse(Input[2]);
            float number4 = float.Parse(Input[3]);
            double number5= double.Parse(Input[4]);

            Console.WriteLine(number1);
            Console.WriteLine(number2);
            Console.WriteLine(number3);
            Console.WriteLine(number4);
            Console.WriteLine(number5);
        }
    }
}