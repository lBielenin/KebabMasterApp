using KebabApplication.DTO;
using KebabCore.DomainModels.Orders;
using KebabCore.Enums;
using KebabCore.Models.Orders;
using KebabCore.Views;
using KebabInfrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace KebabMasterApp
{
    /// <summary>
    /// Interaction logic for OrderContentControl.xaml
    /// </summary>
    public partial class OrderContentControl : UserControl
    {
        public List<MenuView> Menu;
        public ObservableCollection<OrderItemDTO> Order =
            new ObservableCollection<OrderItemDTO>();

        public OrderContentControl()
        {
            InitializeComponent();

            var repo = new MenuService();
            Menu = repo.GetNewestMenu();

            menu.ItemsSource = Menu;
            order.ItemsSource = Order;

            paymentCombo.ItemsSource = Enum.GetValues(typeof(PaymentForm));
            orderFormCombo.ItemsSource = Enum.GetValues(typeof(OrderForm));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var index = menu.SelectedIndex;
            if (index == -1)
                return;
            var itemToAdd = Menu[index];
            var itemToUpdate = Order.FirstOrDefault(o => o.MenuItemId == itemToAdd.MenuItemId);

            if (itemToUpdate != null)
            {
                itemToUpdate.Quantity++;
                return;
            }

            Order.Add(new OrderItemDTO { 
                Category = itemToAdd.ItemCategory, Description = itemToAdd.ItemDescription, 
                ItemId = itemToAdd.ItemId, MenuId = itemToAdd.MenuId, 
                MenuItemId = itemToAdd.MenuItemId, Name = itemToAdd.ItemName, 
                Price = itemToAdd.ItemPrice, Quantity = 1 });


        }
        
        private void Minus_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var ctx = (OrderItemDTO)btn.DataContext;

            if (ctx.Quantity > 1)
                ctx.Quantity--;
            else
                Order.Remove(ctx);
        }

        private void Plus_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var ctx = (OrderItemDTO)btn.DataContext;
            ctx.Quantity++;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var payment = paymentCombo.SelectedItem;
            var form = orderFormCombo.SelectedItem;
            if (!Order.Any())
            {

            }
            var order = new Order()
            {
                OrderId = Guid.NewGuid(),
                OrderItem = this.Order.Select(o => new OrderItem { MenuItemId = o.MenuItemId, OrderItemId = Guid.NewGuid() }).ToList(),
                PaymentMethod = (int)payment,
                OrderForm = (int)form,
                StatusId = (int)Status.NotStarted
            };
            addPosBtn.IsEnabled = false;
            placeOrderBtn.IsEnabled = false;

            new OrdersService().PlaceOrder(order);

        }
    }
}
