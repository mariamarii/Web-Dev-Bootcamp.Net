using System;

public class HelloWorld
{
    public static void Main(string[] args)
    {
        double input = double.Parse(Console.ReadLine());
        int intInput = (int)input;
        if (intInput == input)
        {
            Console.WriteLine($"int {intInput}");
        }
        else
        {
            Console.WriteLine($"float {intInput} {input-intInput:F3}");
        }
    }
}