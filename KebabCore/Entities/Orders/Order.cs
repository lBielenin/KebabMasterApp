using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabCore.Entities.Orders
{
    [Table("Orders", Schema = "Orders")]
    public class Order
    {
        public Guid OrderId { get; init; }
        public int PaymentMethod { get; init; }
        public byte OrderNumber { get; init; }
        public List<OrderItem> OrderItem { get; init; }
        //public int StatusId { get; init; } = 1;
        //public string? Order { get; set; }
    }
}
