using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabCore.Entities
{
    public class Menu
    {
        public Guid MenuId { get; init; }
        public DateTime CreationDate { get; init; }
        public List<Item> Items { get; init; }

        private Menu(Guid id, List<Item> items, DateTime creationDate)
        {
            MenuId = id;
            Items = items;
            CreationDate = creationDate;
        }
        public static Menu CreateNewMenu(List<Item> items, DateTime? creationDate)
        {
            if (creationDate is null)
                creationDate = DateTime.Now;
            return new Menu(Guid.NewGuid(), items, creationDate.Value);
        }
    }
}
