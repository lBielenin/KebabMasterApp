namespace KebabCore.Entities
{
    public class Item
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public List<Product> Products { get; set; }
    }
}