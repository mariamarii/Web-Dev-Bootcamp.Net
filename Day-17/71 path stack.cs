public class Solution {
    public string SimplifyPath(string path) {
        Stack<string> pathstack = new Stack<string>();
        string[] parts = path.Split('/');

        foreach (string part in parts) {
            if (part == "" || part == ".") {
                continue;
            } else if (part == "..") {
                if (pathstack.Count > 0) {
                    pathstack.Pop();
                }
            } else {
                pathstack.Push(part);
            }
        }

  
        var result = new List<string>(pathstack.Reverse());
        return "/" + string.Join("/", result);
    }
}