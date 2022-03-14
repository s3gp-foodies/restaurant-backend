namespace foodies_app.Entities
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection <AllergyCategory> AllergyCategory { get; set; }
        public virtual Category Category { get; set; }
    }
}
