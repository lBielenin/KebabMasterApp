using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KebabCore.Entities.Menu
{
    [Table("MenuItems", Schema = "Menu")]
    public class MenuItem
    {
        [Key]
        public Guid MenuItemId { get; set; }
        public Guid MenuId { get; set; }
        public Menu Menu { get; set; }
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        //public List<Item> Items { get; set; }
    }
}