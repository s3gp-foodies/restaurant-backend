namespace foodies_app.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderdDate { get; set; }   
        
        public decimal TotalPrice { get; set; }
        public bool Completed { get; set; }
        public virtual Table Table { get; set; }    

    }
}
