using KebabCore.Enums;

namespace KebabCore.Views
{
    public class MenuView
    {
        public Guid MenuId { get; set; }
        public Guid MenuItemId { get; set; }
        public Guid ItemId { get; set; }
        public string ItemName { get; set; }
        public DateTime MenuCreationDate { get; set; }
        public Category ItemCategory { get; set; }
        public string ItemDescription { get; set; }
        public decimal ItemPrice { get; set; }

    }
}
