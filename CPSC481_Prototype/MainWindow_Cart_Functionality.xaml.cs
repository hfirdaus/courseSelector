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

        private void Cart_Schedule_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender.Equals(Show_Cart_Label) || sender.Equals(Show_Cart_Border))
            {
                // User clicked on the Show_Cart
                if (Show_Cart_Label.Content.Equals(cart_label_hide))
                {
                    // Hide the cart+sched stack panel, change the label name for both labels
                    Cart_Sched_Stack_Panel.Visibility = Visibility.Collapsed;
                    Show_Cart_Label.Content = cart_label_show;
                    Show_Schedule_Label.Content = sched_label_show;
                }
                else
                {
                    // Show the cart+sched stack panel, show the cart panel, collapse the sched panel, change the labels
                    Cart_Sched_Stack_Panel.Visibility = Visibility.Visible;
                    Cart_Stack_Panel.Visibility = Visibility.Visible;
                    Sched_Stack_Panel.Visibility = Visibility.Collapsed;
                    Show_Cart_Label.Content = cart_label_hide;
                    Show_Schedule_Label.Content = sched_label_show;

                }
            }
            else if (sender.Equals(Show_Schedule_Label) || sender.Equals(Show_Schedule_Border))
            {
                // User clicked on the Show_Schedule
                if (Show_Schedule_Label.Content.Equals(sched_label_hide))
                {
                    // Hide the cart+sched stack panel, change the label name for both labels
                    Cart_Sched_Stack_Panel.Visibility = Visibility.Collapsed;
                    Show_Cart_Label.Content = cart_label_show;
                    Show_Schedule_Label.Content = sched_label_show;
                }
                else
                {
                    // Show the cart+sched stack panel, collapse the cart panel, show the sched panel, change the labels
                    Cart_Sched_Stack_Panel.Visibility = Visibility.Visible;
                    Cart_Stack_Panel.Visibility = Visibility.Collapsed;
                    Sched_Stack_Panel.Visibility = Visibility.Visible;
                    Show_Cart_Label.Content = cart_label_show;
                    Show_Schedule_Label.Content = sched_label_hide;

                }
            }
        }

    }
}
