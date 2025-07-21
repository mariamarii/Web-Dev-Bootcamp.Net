using System;

namespace ConsoleApp1
{

    public class AdjacentList
    
    {
        private Dictionary<string, List<string>> adjacencyList ;

        public AdjacentList()
        {
            adjacencyList = new Dictionary<string, List<string>>();
        }
        public void AddEdge(string from, string to)
        {
            if (!adjacencyList.ContainsKey(from))
            {
                adjacencyList.Add(from, new List<string>());
            }
            adjacencyList[from].Add(to);
        }
        public void RemoveEdge(string from, string to)
        {
            if (adjacencyList.ContainsKey(from))
            {
                adjacencyList[from].Remove(to);
                if (adjacencyList[from].Count == 0)
                {
                    adjacencyList.Remove(from);
                }
            }
        }
        public void Print()
        {
            foreach (var element in adjacencyList)
            {
                Console.Write(element.Key + " -> ");
                Console.WriteLine(string.Join(", ", element.Value));
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            AdjacentList list = new AdjacentList();
            list.AddEdge("a", "b");
            list.AddEdge("a", "c");
            list.AddEdge("a", "d");
            list.AddEdge("a", "e");
            list.AddEdge("b", "c");
            list.AddEdge("c", "d");
            list.AddEdge("d", "e");
            list.Print();
            list.RemoveEdge("a", "c");
            list.RemoveEdge("a", "d");
            Console.WriteLine("----------------------------------------");
            list.Print();
            
            
        }
    }
}