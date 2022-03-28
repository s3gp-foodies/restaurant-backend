namespace foodies_app.Entities
{
    public class OrderItem
    {   
        public int Id { get; set; }
        public int Quantity { get; set; }

        public Status Status{ get; set; }
        public MenuItem Item { get; set; }
        public int ItemId { get; set; }

        public Order Order { get; set; }
        public int OrderId { get; set; }
        
    }
    public enum Status
    {
        submitted,
        inprogress,
        complete
    }
}
