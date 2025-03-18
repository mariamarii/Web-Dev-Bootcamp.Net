using System;

public class HelloWorld
{
    public static void Main(string[] args)
    {
        string[] input = Console.ReadLine().Split();
        int A = int.Parse(input[0]);
        int B = int.Parse(input[1]);
        int C = int.Parse(input[2]);

       
        int[] numbers = { A, B, C };

        Array.Sort(numbers);
        foreach (int num in numbers)
        {
            Console.WriteLine(num);
        }

        Console.WriteLine();
        Console.WriteLine(A);
        Console.WriteLine(B);
        Console.WriteLine(C);
    }
}