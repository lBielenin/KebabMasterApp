using System.ComponentModel.DataAnnotations.Schema;

namespace KebabCore.Models.Orders
{
    public class OrderItem
    {
        public Guid OrderItemId { get; set; }
        public Guid OrderId { get; set; }
        public Guid MenuItemId { get; set; }
        public int Quantity { get; set; }

        public OrderItem() { }
        public OrderItem(Guid orderItemId, Guid orderId, Guid menuItemId, int quantity)
        {
            OrderItemId = orderItemId;
            OrderId = orderId;
            MenuItemId = menuItemId;
            Quantity = quantity;
        }
    }
}