namespace WebApplication1.Models;

public class Login
{
    public int Id { get; set; }
   public string username { get; set; }
   public string password { get; set; }
   
   public int employeeId { get; set; }
   public virtual Employee employee { get; set; }
    
}