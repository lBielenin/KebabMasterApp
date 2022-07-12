using KebabApplication.DTO;
using KebabApplication.Services;
using KebabInfrastructure.DatabaseMonitor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KebabMasterApp
{
    /// <summary>
    /// Interaction logic for OrderViewerControl.xaml
    /// </summary>
    public partial class OrderViewerControl : UserControl, IDisposable
    {
        public ObservableCollection<OrderDTO> Orders =
            new ObservableCollection<OrderDTO>();
        private DatabaseMonitor monitor;
        public OrderViewerControl()
        {
            InitializeComponent();
            var orders = new OrdersService().GetTodayOrders();
            Orders = new ObservableCollection<OrderDTO>(orders);
            monitor = new DatabaseMonitor(SynchronizationContext.Current);
            orderList.ItemsSource = Orders;
            orderList.Items.SortDescriptions.Add(new SortDescription("CreationDate", ListSortDirection.Descending));
            monitor.StartMonitoring(Orders, true);
        }

        public void Dispose()
        {
            monitor?.Dispose();
        }
    }
}
