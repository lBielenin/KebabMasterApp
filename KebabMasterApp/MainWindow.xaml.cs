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
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private readonly IServiceProvider serviceProvider;

        public MainWindow(
            IServiceProvider serProv)
        {
            try
            {
                Loaded += ToolWindow_Loaded;

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
        void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Code to remove close box from window
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
    }
}
