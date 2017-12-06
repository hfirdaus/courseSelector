using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CPSC481_Prototype
{
    public class AddCourseCommand : ICommand
    {

        public event EventHandler CanExecuteChanged;

        private Course course;

        public AddCourseCommand(Course course)
        {
            this.course = course;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Offering selected = course.SelectedOffering();
            if (selected != null)
            {
                CourseSelectorCourses.RemoveCourse(course);
                CartAndScheduleEntry entry = CartSelections.AddToCart(course);
                Messages.AddUndoMessage("Added " + course.Department + " " + course.Number + " in " + entry.Semester + " to Cart", () => CartSelections.RemoveFromCart(entry));

            }
            else
            {
                MessageBox.Show("Please ensure all sections (lecture/tutorial/lab) are selected for the course you wish to add");
            }
        }
    }
}
