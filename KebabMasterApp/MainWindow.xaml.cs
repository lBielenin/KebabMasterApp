using KebabCore.Entities;
using KebabCore.Entities.Menu;
using KebabInfrastructure;
using KebabInfrastructure.Dto;
using KebabInfrastructure.Repositories;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<KebabCore.Entities.Menu.MenuItem> Menu;
        public ObservableCollection<KebabCore.Entities.Orders.OrderItem> Order = 
            new ObservableCollection<KebabCore.Entities.Orders.OrderItem>();
        public MainWindow()
        {
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

            Order.Add(new KebabCore.Entities.Orders.OrderItem { MenuItem = Menu[index] });
            

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
