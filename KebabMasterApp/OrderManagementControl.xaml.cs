using KebabApplication.DTO;
using KebabApplication.Services;
using KebabApplication.StateMachine;
using KebabInfrastructure;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace KebabMasterApp
{
    /// <summary>
    /// Interaction logic for OrderManagementControl.xaml
    /// </summary>
    public partial class OrderManagementControl : UserControl
    {
        public ObservableCollection<OrderDTO> Orders =
            new ObservableCollection<OrderDTO>();

        public OrderManagementControl()
        {
            InitializeComponent();
            var orders = new OrdersService().GetTodayOrders();
            Orders = new ObservableCollection<OrderDTO>(orders);

            orderList.ItemsSource = Orders;
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
