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

        public Order() { }
        public Order(Guid orderId, int paymentMethod, int orderForm, int statusId, string? comment, DateTime creationDate, List<OrderItem> orderItem)
        {
            OrderId = orderId;
            PaymentMethod = paymentMethod;
            OrderForm = orderForm;
            StatusId = statusId;
            Comment = comment;
            CreationDate = creationDate;
            OrderItem = orderItem;
        }
    }
}
