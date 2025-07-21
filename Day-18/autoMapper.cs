using System;
using System.Reflection;

public class Admin
{
    public string Username { get; set; }
    public string Email { get; set; }
    public int AccessLevel { get; set; }
}

public class User
{
    public string Username { get; set; }
    public string Email { get; set; }
}

public class Automapper
{
    public static void Map(object first, object second)
    {
        var  firstProperties= first.GetType().GetProperties();
        var secondProperities = second.GetType().GetProperties();

        foreach (var fProp in firstProperties)
        {
            foreach (var sProp in secondProperities)
            {
                if (fProp.Name == sProp.Name &&
                    fProp.PropertyType == sProp.PropertyType)
                {
                    sProp.SetValue(second, fProp.GetValue(first));
                    break;
                }
            }
        }
    }
}

class Program
{
    static void Main()
    {
        var admin = new Admin { Username = "mariam_admin", Email = "mariam@gmail.com", AccessLevel = 2 };
        var user = new User();

        Automapper.Map(admin, user);
        Console.WriteLine($"[Admin to User] Username: {user.Username}, Email: {user.Email}");

       
    }
}