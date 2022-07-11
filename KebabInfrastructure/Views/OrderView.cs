using KebabCore.Enums;

namespace KebabInfrastructure.Views
{
    public class OrderView
    {
        public Guid OrderId { get; set; }
        public DateTime OrderCreationDate { get; set; }
        public string Name { get; set; }
        public Category CategoryName { get; set; }
        public int OrderNumber { get; set; }
        public int Quantity { get; set; }
        public Status StatusName { get; set; }
        public PaymentForm PaymentForm { get; set; }
        public OrderForm OrderForm { get; set; }

    }
}
