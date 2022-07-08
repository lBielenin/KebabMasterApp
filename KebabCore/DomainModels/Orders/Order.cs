using KebabCore.Models.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabCore.DomainModels.Orders
{
    public class Order
    {
        public Guid OrderId { get; init; }
        public int PaymentMethod { get; init; }
        public int OrderForm { get; init; }
        public int StatusId { get; init; }
        public byte? OrderNumber { get; init; }
        public List<OrderItem> OrderItem { get; init; }
    }
}
