using System.ComponentModel.DataAnnotations.Schema;

namespace KebabCore.Models.Orders
{
    public class OrderItem
    {
        public Guid OrderItemId { get; set; } = Guid.NewGuid();
        public Guid MenuItemId { get; set; }

        public int StatusId { get; init; } = 1;

        public byte Quantity { get; set; } = 1;
        public string? Comment { get; set; }
    }
}