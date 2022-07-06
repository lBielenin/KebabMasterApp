using KebabCore.DomainModels.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabApplication.DTO
{
    public class OrderItemDTO
    {
        public MenuItem MenuItem { get; set; }

        public byte Quantity { get; set; } = 1;
    }
}
