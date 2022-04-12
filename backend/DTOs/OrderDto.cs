using foodies_app.Entities;

namespace foodies_app.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public DateTime OrderTime { get; set; }
    public Status Status { get; set; }
    public bool Completed { get; set; }

}