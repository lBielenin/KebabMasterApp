using KebabApplication.DatabaseMonitor;
using KebabApplication.DTO;
using KebabApplication.Services.Contracts;
using Serilog.Core;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;

namespace KebabMasterApp
{
    /// <summary>
    /// Interaction logic for OrderViewerControl.xaml
    /// </summary>
    public partial class OrderViewerControl : UserControl, IDisposable
    {
        private IDatabaseMonitor databaseMonitor;
        private readonly IOrderService orderService;

        public ObservableCollection<OrderDTO> Orders =
            new ObservableCollection<OrderDTO>();

        public OrderViewerControl(
            IOrderService orderServ, 
            IDatabaseMonitor monitor)
        {
            InitializeComponent();
            orderService = orderServ;
            databaseMonitor = monitor;
            SetUi();
            databaseMonitor.StartMonitoring(Orders, true);

        }

        public void Dispose()
        {
            databaseMonitor?.Dispose();
        }

        private void SetUi()
        {
            var orders = orderService.GetTodayOrders();
            Orders = new ObservableCollection<OrderDTO>(orders);
            orderList.ItemsSource = Orders;
            orderList.Items.SortDescriptions.Add(new SortDescription("CreationDate", ListSortDirection.Descending));

        }
    }
}
