namespace foodies_app.Entities
{
    public class OrderItem
    {   
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }

        public  Status Status{ get; set; }
        public virtual MenuItem Item { get; set; }
    }
    public enum Status
    {
        submitted,
        inprogress,
        complete
    }
}
