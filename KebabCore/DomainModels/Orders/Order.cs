using KebabCore.Models.Orders;

namespace KebabCore.DomainModels.Orders
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public int PaymentMethod { get; set; }
        public int OrderForm { get; set; }
        public int StatusId { get; set; }
        public string? Comment { get; set; }
        public DateTime CreationDate { get; set; }
        public List<OrderItem> OrderItem { get; set; }
    }
}
