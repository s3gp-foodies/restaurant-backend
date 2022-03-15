namespace foodies_app.Entities
{
    public class Table
    {
        public Guid Id { get; set; }
        public int TableNumber { get; set; }
        public bool Occupied { get; set; }
    }
}
