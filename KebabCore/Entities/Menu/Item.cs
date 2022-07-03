using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabCore.Entities.Menu
{
    [Table("Items", Schema = "Menu")]
    public class Item
    {
        [Key]
        public Guid ItemId { get; init; }    
        public string Name { get; init; }
        public string Ingredients { get; init; }
    }
}
