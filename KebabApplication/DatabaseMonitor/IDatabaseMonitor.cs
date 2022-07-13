using KebabApplication.DTO;
using KebabCore.DomainModels.Orders;
using System.Collections.ObjectModel;
using TableDependency.SqlClient.Base.EventArgs;

namespace KebabApplication.DatabaseMonitor
{
    public interface IDatabaseMonitor : IDisposable
    {
        Task StartMonitoring(ObservableCollection<OrderDTO> ord, bool updateMonit);
        void ChangeInsertDelete(object sender, RecordChangedEventArgs<Order> e);
        void ChangeUpdate(object sender, RecordChangedEventArgs<Order> e);
        void Dispose();
    }
}
