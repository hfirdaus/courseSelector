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

namespace CPSC481_Prototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Show_Cart_Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(Cart_Stack_Panel.Visibility == Visibility.Collapsed)
            {
                Cart_Stack_Panel.Visibility = Visibility.Visible;
                Show_Cart_Label.Content = "Hide Cart";
            } else
            {
                Cart_Stack_Panel.Visibility = Visibility.Collapsed;
                Show_Cart_Label.Content = "Show Cart";
            }
        }
    }
}
