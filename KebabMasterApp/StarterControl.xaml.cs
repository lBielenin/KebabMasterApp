using System;
using System.Collections.Generic;
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
    /// Interaction logic for StarterControl.xaml
    /// </summary>
    public partial class StarterControl : UserControl
    {
        public StarterControl()
        {
            InitializeComponent();
        }

        private void ClientScreen_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = (MainWindow) Window.GetWindow(this);
            parentWindow.ChangeContent();
        }

        private void PopUp_Button_Confirm(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
