using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabCore.Entities.Orders
{
    public class Order
    {
        public Guid Id { get; init; }
        public PaymentForm Payment { get; init; }
        public byte OrderNumber { get; init; }
        public List<OrderItem> OrderItem { get; init; }
        //public string? Order { get; set; }
    }
}
