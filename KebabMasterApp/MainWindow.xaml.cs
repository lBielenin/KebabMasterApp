using KebabCore.Entities.Orders;
using KebabInfrastructure;
using KebabMasterApp.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace KebabMasterApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<KebabCore.Entities.Menu.MenuItem> Menu;
        public ObservableCollection<OrderItemDto> Order = 
            new ObservableCollection<OrderItemDto>();
        public MainWindow()
        {
            InitializeComponent();

            ContentArea.Content = new StarterControl();
        }

        public void ChangeContent()
        {
            ContentArea.Content = new OrderContentControl();

        }
    }
}
