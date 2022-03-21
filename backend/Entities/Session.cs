namespace foodies_app.Entities;

public class Session
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public AppUser User { get; set; }
    public int UserId { get; set; }
    public ICollection<Order> Orders { get; set; }
}