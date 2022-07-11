using KebabCore.Enums;

namespace KebabApplication.DTO
{
    public class OrderItemDTO
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Category Category { get; set; }

        public override string ToString()
        {
            return $"{Name} {Quantity} {Category}";
        }
    }
}
