using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CPSC481_Prototype
{
    public class RemoveFromScheduleCommand : ICommand
    {

        public event EventHandler CanExecuteChanged;

        private CartAndScheduleEntry entry;

        public RemoveFromScheduleCommand(CartAndScheduleEntry entry)
        {
            this.entry = entry;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ScheduleSelections.RemoveFromSchedule(entry);
        }
    }
}
