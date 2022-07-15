using KebabApplication.DatabaseMonitor;
using KebabApplication.DTO;
using KebabApplication.Services.Contracts;
using KebabApplication.StateMachine;
using KebabInfrastructure.DatabaseMonitor;
using Serilog.Core;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace KebabMasterApp
{
    /// <summary>
    /// Interaction logic for OrderManagementControl.xaml
    /// </summary>
    public partial class OrderManagementControl : UserControl, IDisposable
    { 

        private readonly IDatabaseMonitor databaseMonitor;
        private readonly IOrderService orderService;
        private readonly IOrderStateMachine orderStateMachine;

        public ObservableCollection<OrderDTO> Orders =
            new ObservableCollection<OrderDTO>();
        private DatabaseMonitor monitor;


        public OrderManagementControl(
            IOrderService orderServ,
            IDatabaseMonitor monitor,
            IOrderStateMachine stateMachine)
        {
            InitializeComponent();
            orderService = orderServ;
            databaseMonitor = monitor;
            orderStateMachine = stateMachine;
            SetUI();

            databaseMonitor.StartMonitoring(Orders, false);
        }

        public void ListBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            orderDetailsTextBlock.Text = orderList.SelectedItem?.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            upStatusBtn.IsEnabled = false;
            var machine = new OrderStatusSimpleStateMachine(orderService);
            var item = (OrderDTO)orderList.SelectedItem;

            machine.UpState(item, Orders);
            upStatusBtn.IsEnabled = true;
        }

        private void SetUI()
        {
            Orders = new ObservableCollection<OrderDTO>(orderService.GetTodayOrders());
            orderList.ItemsSource = Orders;
            orderList.Items.SortDescriptions.Add(new SortDescription("CreationDate", ListSortDirection.Descending));
        }
        public void Dispose()
        {
            monitor?.Dispose();
        }
    }
}
