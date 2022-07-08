using KebabCore.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KebabCore.DomainModels.Menu
{
    public class Item
    {
        [Key]
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        [Column("CategoryId")]
        public Category Category { get; set; }
        public string Description { get; set; }
        public List<MenuItem> MenuItems { get; set; }
    }
}
