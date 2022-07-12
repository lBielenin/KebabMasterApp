using KebabApplication.DTO;
using KebabInfrastructure.Views;

namespace KebabApplication.Mapper
{
    public static class Mapper
    {
        public static List<OrderDTO> MapOrderViewsToOrders(List<OrderView> orderViews)
        {
            return orderViews.GroupBy(v => v.OrderId)
                .Select(g => new OrderDTO
                {
                    CreationDate = g.First().OrderCreationDate,
                    OrderForm = g.First().OrderForm,
                    OrderId = g.First().OrderId,
                    PaymentFrom = g.First().PaymentForm,
                    Status = g.First().StatusName,
                    OrderNumber = g.First().OrderNumber,
                    OrderItems = g.Select(k => new OrderItemDTO
                    {
                        Category = k.CategoryName,
                        Name = k.Name,
                        Quantity = k.Quantity
                    }).ToList()
                }).ToList();
        }
    }
}
