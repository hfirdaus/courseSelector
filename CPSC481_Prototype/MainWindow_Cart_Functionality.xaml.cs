using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CPSC481_Prototype
{
    public partial class MainWindow : Window
    {
        private static string cart_label_hide = "Hide Cart";
        private static string cart_label_show = "Show Cart";
        private static string sched_label_hide = "Hide Schedule";
        private static string sched_label_show = "Show Schedule";

        private void Show_Cart_Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Cart_Stack_Panel.Visibility == Visibility.Collapsed)
            {
                Cart_Stack_Panel.Visibility = Visibility.Visible;
                Show_Cart_Label.Content = cart_label_hide;
            }
            else
            {
                Cart_Stack_Panel.Visibility = Visibility.Collapsed;
                Show_Cart_Label.Content = cart_label_show;
            }
        }
    }
}
