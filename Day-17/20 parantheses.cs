

public class Solution {
    public bool IsValid(string s) {
        Stack<char> stack = new Stack<char>();
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == ')' || s[i] == ']' || s[i] == '}')
            {
                if(stack.Count == 0)
                    return false;
                else
                {
                    char x = stack.Pop();
                    if((x=='(' && s[i]!=')')|| (x=='[' && s[i]!=']') || (x=='{' && s[i]!='}')) 
                        return false;
                }
            }
            else
            {
                stack.Push(s[i]);
            }
        }
        if(stack.Count !=0)
            return false;
        else
        {
            return true;
        }
            
    }
   
}