using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CPSC481_Prototype
{
    public class EnrollCourseCommand : ICommand
    {

        public event EventHandler CanExecuteChanged;

        private CartAndScheduleEntry entry;

        public EnrollCourseCommand(CartAndScheduleEntry entry)
        {
            this.entry = entry;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
                if (!entry.Number.StartsWith("2") || entry.Number.Equals("203"))
            {
                MessageBox.Show("You can't add " + entry.Department + " " + entry.Number + " to Schedule as you do not have the prerequisites.");
                return;
            }
                
                CartSelections.RemoveFromCart(entry);
                entry.UpdateRemoveFromSchedule();
                ScheduleSelections.AddToSchedule(entry);
                Messages.AddUndoMessage("Moved " + entry.Department + " " + entry.Number + "in " + entry.Semester + " to Schedule", () => reverseEnroll(entry));        
        }

        public void reverseEnroll(CartAndScheduleEntry entry)
        {
            entry.UpdateRemoveFromCart();
            CartSelections.AddToCart(entry);
            ScheduleSelections.RemoveFromSchedule(entry);
        }
    }
}
