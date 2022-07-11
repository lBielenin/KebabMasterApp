using System.ComponentModel.DataAnnotations.Schema;

namespace KebabCore.Models.Orders
{
    public class OrderItem
    {
        public Guid OrderItemId { get; set; }
        public Guid OrderId { get; set; }
        public Guid MenuItemId { get; set; }
        public int Quantity { get; set; }
    }
}