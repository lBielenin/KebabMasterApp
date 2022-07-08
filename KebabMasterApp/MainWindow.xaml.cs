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

            ContentArea.Content = new StarterControl();
        }

        public void ChangeContent()
        {
            ContentArea.Content = new OrderContentControl();

        }
    }
}
