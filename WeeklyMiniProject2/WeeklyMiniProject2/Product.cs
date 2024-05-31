namespace WeeklyMiniProject2
{
    public class Product
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Product(string category, string name, double price)
        {
            Category = category;
            Name = name;
            Price = price;
        }
    }
}