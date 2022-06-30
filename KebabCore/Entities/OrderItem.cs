namespace KebabCore.Entities
{
    public class OrderItem
    {
        public Item Item { get; set; }
        public byte Quantity { get; set; }
        public string? Comment { get; set; }
    }
}