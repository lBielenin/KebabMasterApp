using KebabCore.Models.Orders;

namespace KebabCore.DomainModels.Orders
{
    public class Order
    {
        public Guid OrderId { get; init; }
        public int PaymentMethod { get; init; }
        public int OrderForm { get; init; }
        public int StatusId { get; init; }
        public string? Comment { get; set; }
        public List<OrderItem> OrderItem { get; init; }
    }
}
