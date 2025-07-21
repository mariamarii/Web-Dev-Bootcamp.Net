using System;

namespace ConsoleApp1
{
    public class TreeNode
    {
        public int Value;
        public TreeNode Left;
        public TreeNode Right;

        public TreeNode(int value)
        {
            Value = value;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            
            TreeNode root = new TreeNode(79)
            {
                Left = new TreeNode(50)
                {
                    Left = new TreeNode(20),
                    Right = new TreeNode(65)
                },
                Right = new TreeNode(30)
                {
                    Left = new TreeNode(35),
                    Right = new TreeNode(2)
                }
            };

            int min = MinTree(root);
            Console.WriteLine($"minimum value in the tree: {min}");
        }

        static int MinTree(TreeNode node)
        {
            if (node == null)
                return int.MaxValue;

            int leftMin = MinTree(node.Left);
            int rightMin = MinTree(node.Right);

            return Math.Min(node.Value, Math.Min(leftMin, rightMin));
        }
    }
}