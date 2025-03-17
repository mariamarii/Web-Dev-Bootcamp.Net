using System;

public class HelloWorld
{
    public static void Main(string[] args)
    {
        double input=double.Parse( Console.ReadLine());
        if ( input<=25 && input>=0)
            Console.WriteLine("Interval [0,25]");
        else if (input<=50 && input > 25)
            Console.WriteLine("Interval (25,50]");
        else if (input<=75 && input > 50)
            Console.WriteLine("Interval (50,75]");
        else if (input<=100 && input > 75)
            Console.WriteLine("Interval (75,100]");
        else
        {
            Console.WriteLine("Out of Intervals");
        }
    }
}