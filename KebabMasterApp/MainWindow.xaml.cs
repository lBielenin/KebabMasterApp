using KebabApplication.Services.Contracts;
using KebabMasterApp.ContentStrategy;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;
using System;
using System.Windows;

namespace KebabMasterApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider serviceProvider;

        public MainWindow(
            IServiceProvider serviceProvider)
        {
            InitializeComponent();


            var context = new DisplayStrategyContext(serviceProvider);
            var strategy = context.GetStrategyFromCommandArgs();

            strategy.DisplayContent(ContentArea);
            this.serviceProvider = serviceProvider;
        }

        public void ChangeContent()
        {
            ContentArea.Content = 
                new OrderContentControl(
                    serviceProvider.GetService<IMenuService>(), 
                    serviceProvider.GetService<IOrderService>(),
                    serviceProvider.GetService<Logger>());
        }

        public void ChangeContentToStarter()
        {
            ContentArea.Content = new StarterControl();
        }

        private void Error_PopUp_Confirm(object sender, RoutedEventArgs e)
        {
            errorPopUp.IsEnabled = false;
            ChangeContentToStarter();
        }

        public void ShowError()
        {
            errorPopUp.IsEnabled = true;
        }
    }
}
