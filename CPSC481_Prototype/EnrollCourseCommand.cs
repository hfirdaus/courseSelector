using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                CartSelections.RemoveFromCart(entry);
                entry.UpdateRemoveFromSchedule();
                ScheduleSelections.AddToSchedule(entry);
        }
    }
}
