using System.ComponentModel.DataAnnotations;

namespace KebabCore.DomainModels.Menu
{
    public class Menu
    {
        [Key]
        public Guid MenuId { get; set; }
        public DateTime CreationDate { get; set; }
        public List<MenuItem> MenuItems { get; set; }

        public Menu()
        {

        }
        public Menu(Guid id, List<MenuItem> items, DateTime creationDate)
        {
            MenuId = id;
            MenuItems = items;
            CreationDate = creationDate;
        }
    }
}
