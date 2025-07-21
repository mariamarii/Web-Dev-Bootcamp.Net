using System;


namespace ConsoleApp1
{
    public class Emps
    {
        public string Name { get; set; }
        public bool Gender { get; set; } // true = female, false = male
    }

    class Program
    {
        static void Main(string[] args)
        {
            Queue<Emps> emps = new Queue<Emps>();
            emps.Enqueue(new Emps { Name = "mariam", Gender = true });
            emps.Enqueue(new Emps { Name = "mohamed", Gender = false });
            emps.Enqueue(new Emps { Name = "ahmed", Gender = false });

            
            Func<Queue<Emps>, Emps> FirstMale = (queue) =>
            {
                foreach (var emp in queue)
                {
                    if (emp.Gender == false) 
                        return emp;
                }
                return null;
            };

            Emps firstMale = FirstMale(emps);
            if (firstMale != null)
                Console.WriteLine($"First male employee: {firstMale.Name}");
            else
                Console.WriteLine("No male employee found.");
        }
    }
}