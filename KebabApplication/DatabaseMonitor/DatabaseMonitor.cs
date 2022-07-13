
using KebabApplication.DatabaseMonitor;
using KebabApplication.DTO;
using KebabApplication.Mapper;
using KebabCore.DomainModels.Orders;
using KebabCore.Enums;
using KebabInfrastructure.Context;
using KebabInfrastructure.Options;
using Microsoft.Extensions.Options;
using System.Collections.ObjectModel;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;

namespace KebabInfrastructure.DatabaseMonitor
{
    public class DatabaseMonitor : IDatabaseMonitor
    {
        private KebabDbContext dbContext;
        private string connection;
        private SqlTableDependency<Order> orderDependency;
        private ObservableCollection<OrderDTO> orders;
        private object lockObj = new object();
        private SynchronizationContext uiContext;

        public DatabaseMonitor(SynchronizationContext context, IOptions<KebabDBConnectionSettings> dbOptions, KebabDbContext dbCtx)
        {
            dbContext = dbCtx;
            uiContext = context;
            connection = dbOptions.Value.ConnectionString;
        }
        public async Task StartMonitoring(ObservableCollection<OrderDTO> ord, bool updateMonit)
        {
            orders = ord;
            orderDependency = new SqlTableDependency<Order>(connection, "Orders");
            orderDependency.OnChanged += ChangeInsertDelete;
            if (updateMonit)
            {
                orderDependency.OnChanged += ChangeUpdate;
            }
            orderDependency.Start();
        }

        public void ChangeInsertDelete(object sender, RecordChangedEventArgs<Order> e)
        {
            var changedEntity = e.Entity;

            if (e.ChangeType == ChangeType.Insert)
            {
                lock (lockObj)
                {

                    var items = Mapper.MapOrderViewsToOrders(
                        dbContext.OrderView.Where(o => o.OrderId == changedEntity.OrderId).ToList());
                    items.ForEach(i => uiContext.Send(x => orders.Add(i), null));
                };
            }
            if (e.ChangeType == ChangeType.Delete)
            {
                uiContext.Send(x =>
                {
                    var itemsToRemove = orders.Where(o => o.OrderId == changedEntity.OrderId).ToList();
                    foreach (var itemToRemove in itemsToRemove)
                    {
                        orders.Remove(itemToRemove);
                    }

                }, null);
            }
        }

        public void ChangeUpdate(object sender, RecordChangedEventArgs<Order> e)
        {
            var changedEntity = e.Entity;

            if (e.ChangeType == ChangeType.Update)
            {
                uiContext.Send(x =>
                {
                    foreach (var order in orders)
                    {
                        if (order.OrderId == changedEntity.OrderId)
                        {
                            order.Status = (Status)changedEntity.StatusId;
                            break;
                        }
                    }

                }, null);
            }
        }

        public void Dispose()
        {
            orderDependency.Stop();
            orderDependency.Dispose();
        }
    }
}
