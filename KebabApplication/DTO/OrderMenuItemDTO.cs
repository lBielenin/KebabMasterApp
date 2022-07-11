using KebabCore.Enums;
using System.ComponentModel;

namespace KebabApplication.DTO
{
    public class OrderMenuItemDTO : INotifyPropertyChanged
    {
        public Guid MenuItemId { get; set; }
        public Guid MenuId { get; set; }
        public Guid ItemId { get; set; }
        public decimal Price { get; set; }
        private byte quantity = 1;
        public byte Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                OnPropertyRaised("Quantity");
            }
        }
        public string Name { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}
