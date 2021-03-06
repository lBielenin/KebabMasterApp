using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KebabCore.DomainModels.Menu
{
    public class MenuItem
    {
        [Key]
        public Guid MenuItemId { get; set; }
        public Guid MenuId { get; set; }
        public Guid ItemId { get; set; }
        public decimal Price { get; set; }
    }
}