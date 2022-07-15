using KebabApplication.Services.Contracts;
using KebabMasterApp.ContentStrategy;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;
using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace KebabMasterApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider serviceProvider;

        public MainWindow(
            IServiceProvider serProv)
        {
            try
            {
                serviceProvider = serProv;

                InitializeComponent();


                var context = new DisplayStrategyContext(serProv);
                var strategy = context.GetStrategyFromCommandArgs();

                strategy.DisplayContent(ContentArea);

            }
            catch (SqlException sqlex)
            {
                LogSqlErrorWithMessage(sqlex.Message);
            }
            catch (Exception e)
            {
                LogErrorWithMessage(e.Message);
            }
        }

        public void ChangeContentToMenu()
        {
            try
            {
                ContentArea.Content =
                    new MenuControl(
                        serviceProvider.GetService<IMenuService>(),
                        serviceProvider.GetService<IOrderService>());
            }
            catch (SqlException sqlex)
            {
                LogSqlErrorWithMessage(sqlex.Message);
            }
            catch (Exception e)
            {
                LogErrorWithMessage(e.Message);
            }
        }

        public void ChangeContentToStarter()
        {
            try
            {
                ContentArea.Content = new StarterControl();

            }
            catch (SqlException sqlex)
            {
                LogSqlErrorWithMessage(sqlex.Message);
            }
            catch (Exception e)
            {
                LogErrorWithMessage(e.Message);
            }
        }

        private void Error_PopUp_Confirm(object sender, RoutedEventArgs e)
        {
            errorPopUp.IsEnabled = true;
            errorPopUp.IsOpen = true; 
            ChangeContentToStarter();
        }

        public void ShowError(string message)
        {
            errorHeader.Text = message;
            errorPopUp.IsEnabled = true;
            errorPopUp.IsOpen = true;
        }

        private void LogErrorWithMessage(string message)
        {
            serviceProvider.GetService<Logger>().Error(message);
            ShowError("Critical error occured!");
        }

        private void LogSqlErrorWithMessage(string message)
        {
            serviceProvider.GetService<Logger>().Error(message);
            ShowError("Connection order occured!");
        }
    }
}
