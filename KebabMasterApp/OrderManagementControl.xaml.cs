using KebabApplication.DTO;
using KebabApplication.Services;
using KebabApplication.StateMachine;
using KebabInfrastructure;
using KebabInfrastructure.DatabaseMonitor;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace KebabMasterApp
{
    /// <summary>
    /// Interaction logic for OrderManagementControl.xaml
    /// </summary>
    public partial class OrderManagementControl : UserControl, IDisposable
    {
        public ObservableCollection<OrderDTO> Orders =
            new ObservableCollection<OrderDTO>();
        private DatabaseMonitor monitor;

        public OrderManagementControl()
        {
            InitializeComponent();
            var orders = new OrdersService().GetTodayOrders();
            Orders = new ObservableCollection<OrderDTO>(orders);
            monitor = new DatabaseMonitor(SynchronizationContext.Current);
            orderList.ItemsSource = Orders;
            orderList.Items.SortDescriptions.Add(new SortDescription("CreationDate", ListSortDirection.Descending));
            monitor.StartMonitoring(Orders, false);

        }

        public void Dispose()
        {
            monitor?.Dispose();
            
        }

        public void ListBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            orderDetailsTextBlock.Text = orderList.SelectedItem?.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            upStatusBtn.IsEnabled = false;
            var machine = new OrderStatusSimpleStateMachine(new OrdersService());
            var item = (OrderDTO)orderList.SelectedItem;

            machine.UpState(item, Orders);
            upStatusBtn.IsEnabled = true ;
        }
    }

}
