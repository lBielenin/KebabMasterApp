using KebabCore.DomainModels.Orders;
using KebabInfrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabInfrastructure
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

        public int PlaceOrderReturnValue(Order order)
        {
            DbContext.Orders.Add(order);
            DbContext.SaveChanges();

            return DbContext.GetOrderNumberById(order.OrderId);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
