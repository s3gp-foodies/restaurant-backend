namespace foodies_app.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public Session Session { get; set; }
        public int SessionId { get; set; }
        public Status Status { get; set; }
        public bool Completed { get; set; }
        public List<OrderItem> Items { get; set; }
        

    }
}
