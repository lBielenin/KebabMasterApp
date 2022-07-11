using KebabMasterApp.ContentStrategy;
using System;
using System.Windows;

namespace KebabMasterApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var context = new DisplayStrategyContext();
            var strategy = context.GetStrategyFromCommandArgs();

            strategy.DisplayContent(ContentArea);
        }

        public void ChangeContent()
        {
            ContentArea.Content = new OrderContentControl();

        }

        public void ChangeContentToStarter()
        {
            ContentArea.Content = new StarterControl();
        }
    }
}
