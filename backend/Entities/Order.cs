namespace foodies_app.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }   
        
        public bool Completed { get; set; }
        public virtual Table Table { get; set; }    

    }
}
