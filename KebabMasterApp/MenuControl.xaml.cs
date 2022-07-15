using KebabApplication.DTO;
using KebabApplication.Mapper;
using KebabApplication.Services.Contracts;
using KebabCore.DomainModels.Orders;
using KebabCore.Enums;
using KebabCore.Models.Orders;
using KebabInfrastructure.Views;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace KebabMasterApp
{
    /// <summary>
    /// Interaction logic for OrderContentControl.xaml
    /// </summary>
    public partial class MenuControl : UserControl
    {
        private readonly IMenuService menuService;
        private readonly IOrderService orderService;
        public int NewOrderNumber;
        public List<MenuView> Menu;
        public ObservableCollection<OrderMenuItemDTO> Order =
            new ObservableCollection<OrderMenuItemDTO>();
        private decimal currentOrderSum = 0;

        public MenuControl(
            IMenuService menuSer,
            IOrderService orderSer)
        {
            menuService = menuSer;
            orderService = orderSer;

            InitializeComponent();

            Menu = menuService.GetNewestMenu();
            sumVal.Text = currentOrderSum.ToString();
            SetUI();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            MenuView? itemToAdd = (MenuView)menu.SelectedItem;
            var itemToUpdate = Order.FirstOrDefault(o => o.MenuItemId == itemToAdd.MenuItemId);

            if (itemToUpdate != null)
            {
                itemToUpdate.Quantity++;
            } else
            {
                Order.Add(Mapper.MapMenuViewToOrderMenuItemOrderDTO(itemToAdd));
            }
            currentOrderSum += itemToAdd.ItemPrice;
            sumVal.Text = currentOrderSum.ToString();

        }

        private void Minus_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var ctx = (OrderMenuItemDTO)btn.DataContext;

            if (ctx.Quantity > 1)
                ctx.Quantity--;
            else
                Order.Remove(ctx);
            currentOrderSum -= ctx.Price;
            sumVal.Text = currentOrderSum.ToString();
        }

        private void Plus_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var ctx = (OrderMenuItemDTO)btn.DataContext;
            ctx.Quantity++;

            currentOrderSum += ctx.Price;
            sumVal.Text = currentOrderSum.ToString();
        }

        private void Place_Order_Click(object sender, RoutedEventArgs e)
        {
            var payment = paymentCombo.SelectedItem;
            var form = orderFormCombo.SelectedItem;

            var orderId = Guid.NewGuid();
            var order =
                new Order(
                    orderId,
                    (int)payment,
                    (int)form,
                    (int)Status.Added,
                    "",
                    DateTime.Now,
                    Order.Select(o => new OrderItem(Guid.NewGuid(), orderId, o.MenuItemId, o.Quantity)).ToList());

            addPosBtn.IsEnabled = false;
            placeOrderBtn.IsEnabled = false;

            var orderNumber = orderService.PlaceOrderReturnValue(order);
            NewOrderNumber = orderNumber;
            textBoxOrderNumber.Text = orderNumber.ToString();
            finishPopup.IsOpen = true;
        }


        private void PopUp_Button_Confirm(object sender, RoutedEventArgs e)
        {
            finishPopup.IsEnabled = false;
            finishPopup.IsOpen = false;
            MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
            parentWindow.ChangeContentToStarter();
        }

        private void SetUI()
        {
            menu.ItemsSource = Menu;
            order.ItemsSource = Order;
            paymentCombo.ItemsSource = Enum.GetValues(typeof(PaymentForm));
            orderFormCombo.ItemsSource = Enum.GetValues(typeof(OrderForm));

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(menu.ItemsSource);

            PropertyGroupDescription groupDescription = new PropertyGroupDescription("ItemCategory");
            view.GroupDescriptions.Add(groupDescription);
        }
    }
}
