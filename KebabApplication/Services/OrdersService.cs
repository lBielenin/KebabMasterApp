using KebabApplication.DTO;
using KebabCore.DomainModels.Orders;
using KebabInfrastructure.Context;
using System.Data.Entity;

namespace KebabApplication.Services
{
    public class OrdersService : IDisposable
    {
        private KebabMenuDbContext dbContext;
        private KebabMenuDbContext DbContext
        {
            get
            {
                if (dbContext is null)
                    dbContext = new KebabMenuDbContext();
                return dbContext;
            }
        }

        public List<OrderDTO> GetTodayOrders()
        {
            List<KebabInfrastructure.Views.OrderView>? views = DbContext.OrderView
                .Where(o => o.OrderCreationDate.Date == DateTime.Now.Date)
                .OrderByDescending(o => o.OrderCreationDate).ToList();

            List<OrderDTO>? test = views.GroupBy(v => v.OrderId)
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

            return test;
        }

        public int PlaceOrderReturnValue(Order order)
        {
            DbContext.Orders.Add(order);
            DbContext.SaveChanges();

            return DbContext.GetOrderNumberById(order.OrderId);
        }

        public void RemoveOrder(Guid orderId)
        {
            var order = DbContext.Orders.First(o => o.OrderId == orderId);
            IQueryable<KebabCore.Models.Orders.OrderItem>? orderItems = DbContext.OrderItem.Where(o => o.OrderId == orderId);
            DbContext.OrderItem.RemoveRange(orderItems);
            DbContext.Orders.Remove(order);
            DbContext.SaveChanges();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
