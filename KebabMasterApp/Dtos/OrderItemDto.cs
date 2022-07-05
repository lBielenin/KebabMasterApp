using KebabCore.Entities.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabMasterApp.Dtos
{
    public class OrderItemDto
    {
        public Guid OrderItemId { get; set; } = Guid.NewGuid();
        public Guid MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }
        public Guid MenuId { get; set; }
        public byte Quantity { get; set; } = 1;
        public string? Comment { get; set; }
    }
}
