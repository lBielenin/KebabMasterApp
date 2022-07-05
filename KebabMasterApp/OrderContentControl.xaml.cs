using KebabCore.Entities.Orders;
using KebabInfrastructure;
using KebabMasterApp.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
    /// Interaction logic for OrderContentControl.xaml
    /// </summary>
    public partial class OrderContentControl : UserControl
    {
        public List<KebabCore.Entities.Menu.MenuItem> Menu;
        public ObservableCollection<OrderItemDto> Order =
            new ObservableCollection<OrderItemDto>();

        public OrderContentControl()
        {
            InitializeComponent();
            InitializeComponent();

            var repo = new MenuService();

            Menu = repo.GetNewest();
            menu.ItemsSource = Menu;
            order.ItemsSource = Order;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var index = menu.SelectedIndex;
            if (index == -1)
                return;
            var itemToAdd = Menu[index];
            var itemToUpdate = Order.FirstOrDefault(o => o.MenuItem.MenuItemId == itemToAdd.MenuItemId);

            if (itemToUpdate != null)
            {
                Order.Remove(itemToUpdate);
                itemToUpdate.Quantity++;
                Order.Add(itemToUpdate);
                return;
            }

            Order.Add(new OrderItemDto { MenuItem = Menu[index] });


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var order = new Order()
            {
                OrderId = Guid.NewGuid(),
                OrderItem = this.Order.Select(o => new OrderItem { MenuItemId = o.MenuItemId, OrderItemId = Guid.NewGuid() }).ToList(),
                PaymentMethod = (int)PaymentForm.Cash
                //StatusId = (int)Statuses.NotStarted
            };

            new OrdersService().PlaceOrder(order);

        }
    }
}
