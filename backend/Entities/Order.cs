namespace foodies_app.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Session Session { get; set; }
        public int SessionId { get; set; }
        public bool Completed { get; set; }
        public virtual Table Table { get; set; }    

    }
}
