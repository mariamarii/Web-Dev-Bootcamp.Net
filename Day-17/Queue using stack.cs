Queue q = new Queue();
q.Push(1);
q.Push(2);
q.Push(3);
q.Push(4);
q.Push(5);
q.Push(6);
Console.WriteLine(q.Peek()); 
Console.WriteLine(q.Pop());  
Console.WriteLine(q.ToString());

Console.WriteLine(q.Empty()); 

public class Queue {
    private Stack<int> input;
    private Stack<int> output;

    public Queue() {
        input = new Stack<int>();
        output = new Stack<int>();
    }

    public void Push(int x) {
        input.Push(x);
    }

    public int Pop() {
        ReverseStack();
        return output.Pop();
    }

    public int Peek() {
        ReverseStack();
        return output.Peek();
    }

    public bool Empty() {
        return input.Count == 0 && output.Count == 0;
    }

    private void ReverseStack() {
        if (output.Count == 0) {
            while (input.Count > 0) {
                output.Push(input.Pop());
            }
        }
    }
    public override string ToString() {
        List<int> elements = new List<int>();

        elements.AddRange(output);

        elements.AddRange(input.Reverse());

        return string.Join(", ", elements);
    }
}
