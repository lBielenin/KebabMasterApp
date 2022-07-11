using KebabCore.Enums;
using System.ComponentModel;
using System.Text;

namespace KebabApplication.DTO
{
    public class OrderDTO : INotifyPropertyChanged
    {
        public Guid OrderId { get; set; }
        public DateTime CreationDate { get; set; }
        public int OrderNumber { get; set; }
        public OrderForm OrderForm { get; set; }
        public PaymentForm PaymentFrom { get; set; }
        private Status status { get; set; }
        public Status Status
        {
            get => status;
            set
            {
                status = value;
                OnPropertyRaised("Status");
            }
        }

        public List<OrderItemDTO> OrderItems { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append($"Order Number: {OrderNumber} {Environment.NewLine}");
            builder.Append($"Created at: {CreationDate} {Environment.NewLine}");
            OrderItems?.ForEach(item => builder.Append($"- {item.ToString()}{Environment.NewLine}"));


            return builder.ToString();
        }
        private byte quantity = 1;

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
