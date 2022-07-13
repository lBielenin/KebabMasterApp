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

        public static OrderMenuItemDTO MapMenuViewToOrderMenuItemOrderDTO(MenuView menuView)
        {
            return new OrderMenuItemDTO
            {
                Category = menuView.ItemCategory,
                Description = menuView.ItemDescription,
                ItemId = menuView.ItemId,
                MenuId = menuView.MenuId,
                MenuItemId = menuView.MenuItemId,
                Name = menuView.ItemName,
                Price = menuView.ItemPrice,
                Quantity = 1
            };
        }
    }
}
