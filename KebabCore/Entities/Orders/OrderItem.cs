using KebabCore.Entities.Menu;

namespace KebabCore.Entities.Orders
{
    public class OrderItem
    {
        public MenuItem MenuItem { get; set; }
        public byte Quantity { get; set; } = 1;
        public string? Comment { get; set; }
    }
}