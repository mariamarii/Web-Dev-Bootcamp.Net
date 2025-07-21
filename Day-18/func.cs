using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Emps
    {
        public string Name { get; set; }
        public bool Gender { get; set; } // true = female, false = male
    }

    public static class ExtQueue
    {
        public static Emps FindFirst(this Queue<Emps> queue, Func<Emps, bool> isMale)
        {
            foreach (var emp in queue)
            {
                if (isMale(emp))
                    return emp;
            }
            return null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Queue<Emps> emps = new Queue<Emps>();
            emps.Enqueue(new Emps { Name = "mariam", Gender = true });
            emps.Enqueue(new Emps { Name = "mohamed", Gender = false });
            emps.Enqueue(new Emps { Name = "ahmed", Gender = false });
            
            Emps firstMale = emps.FindFirst(emp => emp.Gender == false);

            if (firstMale != null)
                Console.WriteLine($"First male employee: {firstMale.Name}");
            else
                Console.WriteLine("No male employee found.");
        }
    }
}