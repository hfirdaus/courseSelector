using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CPSC481_Prototype
{
    public class RemoveFromCartCommand : ICommand
    {

        public event EventHandler CanExecuteChanged;

        private CartAndScheduleEntry entry;

        public RemoveFromCartCommand(CartAndScheduleEntry entry)
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
            Messages.AddUndoMessage("Removed " + entry.Department + " " + entry.Number + " from Cart", () => CartSelections.AddToCart(entry));
        }
    }
}
