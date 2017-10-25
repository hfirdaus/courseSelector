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
        private void Show_Cart_Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Cart_Stack_Panel.Visibility == Visibility.Collapsed)
            {
                Cart_Stack_Panel.Visibility = Visibility.Visible;
                Show_Cart_Label.Content = "Hide Cart";
            }
            else
            {
                Cart_Stack_Panel.Visibility = Visibility.Collapsed;
                Show_Cart_Label.Content = "Show Cart";
            }
        }

        private void Requirement_Popup_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Requirement_Popup.Visibility == Visibility.Visible)
            {
                Requirement_Popup.Visibility = Visibility.Hidden;
            }
        }

        private void Show_Complete_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Show_Completed_Stack_Panel.Visibility = Visibility.Visible;
        }

        private void Show_Complete_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Show_Completed_Stack_Panel.Visibility = Visibility.Collapsed;
        }
    }
}
