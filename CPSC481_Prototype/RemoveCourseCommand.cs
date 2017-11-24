using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CPSC481_Prototype
{
    public class RemoveCourseCommand : ICommand
    {

        public event EventHandler CanExecuteChanged;

        private Course course;

        public RemoveCourseCommand(Course course)
        {
            this.course = course;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            CourseSelectorCourses.RemoveCourse(course);
        }
    }
}
