using System;


namespace ConsoleApp1
{
    public class Emps
    
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; } 
    }

    class Program
    {
        static void Main(string[] args)
        {
            Stack<Emps> emps = new Stack<Emps>();
            emps.Push(new Emps {Id= 1, Name = "mariam", Gender = true });
            emps.Push(new Emps { Id= 2,Name = "mohammed", Gender = false });
            emps.Push(new Emps {Id= 3,Name = "ahmed", Gender = false });


            Predicate<Emps> FirstMohamed = (emp) => emp.Name == "mohammed";

            foreach (Emps emp in emps)
            {
                if (FirstMohamed(emp))
                {
                    Console.WriteLine(emp.Id);
                    Console.WriteLine(emp.Name);
                    break;
                   
                }

                
            }

        }
        
    }
}