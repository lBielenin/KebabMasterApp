using KebabApplication.DTO;
using KebabApplication.Services.Contracts;
using KebabCore.DomainModels.Orders;
using KebabCore.Enums;
using KebabInfrastructure.Context;

namespace KebabApplication.Services
{
    public class OrdersService : IDisposable, IOrderService
    {
        private KebabDbContext dbContext;

        public OrdersService(KebabDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<OrderDTO> GetTodayOrders()
        {
            List<KebabInfrastructure.Views.OrderView>? views = dbContext.OrderView
                .Where(o => o.OrderCreationDate.Date == DateTime.Now.Date)
                .OrderByDescending(o => o.OrderCreationDate).ToList();
            return Mapper.Mapper.MapOrderViewsToOrders(views);
        }

        public int PlaceOrderReturnValue(Order order)
        {
            dbContext.Orders.Add(order);
            dbContext.SaveChanges();

            return dbContext.GetOrderNumberById(order.OrderId);
        }
        public void UpdateOrderStatus(Guid orderId, Status status)
        {
            var order = dbContext.Orders.First(o => o.OrderId == orderId);
            order.StatusId = (int)status;
            dbContext.Update(order);
            dbContext.SaveChanges();

        }

        public void RemoveOrder(Guid orderId)
        {
            var order = dbContext.Orders.First(o => o.OrderId == orderId);
            IQueryable<KebabCore.Models.Orders.OrderItem>? orderItems = dbContext.OrderItem.Where(o => o.OrderId == orderId);
            dbContext.OrderItem.RemoveRange(orderItems);
            dbContext.Orders.Remove(order);
            dbContext.SaveChanges();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
