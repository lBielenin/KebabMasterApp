using KebabApplication.DTO;
using KebabCore.DomainModels.Orders;
using KebabCore.Enums;
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
            return Mapper.Mapper.MapOrderViewsToOrders(views);
        }

        public int PlaceOrderReturnValue(Order order)
        {
            DbContext.Orders.Add(order);
            DbContext.SaveChanges();

            return DbContext.GetOrderNumberById(order.OrderId);
        }
        public void UpdateOrderStatus(Guid orderId, Status status)
        {
            var order = DbContext.Orders.First(o => o.OrderId == orderId);
            order.StatusId = (int)status;
            DbContext.Update(order);
            DbContext.SaveChanges();

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
