namespace WebApplication3.Models;

public class Cart
{
    
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public virtual ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    

}