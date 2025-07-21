using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Emps
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }
    }

    public static class StackExtensions
    {
        public static Emps FindFirst(this Stack<Emps> stack, Predicate<Emps> p)
        {
            foreach (var emp in stack)
            {
                if (p(emp))
                    return emp;
            }
            return null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Stack<Emps> emps = new Stack<Emps>();
            emps.Push(new Emps { Id = 1, Name = "mariam", Gender = true });
            emps.Push(new Emps { Id = 2, Name = "mohammed", Gender = false });
            emps.Push(new Emps { Id = 3, Name = "ahmed", Gender = false });

            

            Emps result = emps.FindFirst(emp => emp.Name == "mohammed");

            if (result != null)
            {
                Console.WriteLine(result.Id);
                Console.WriteLine(result.Name);
            }
            else
            {
                Console.WriteLine("No matching employee found.");
            }
        }
    }
}