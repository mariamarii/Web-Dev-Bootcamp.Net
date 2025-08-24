namespace WebApplication1.Models;

public class Employee
{
    public int id { get; set; }
    public string Name { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    
    public int departmentId { get; set; }
    public  virtual Department department { get; set; }
    
    public int RoleId { get; set; }
    
    public  virtual Role role { get; set; }
    
    

    public Login login { get; set; }
}