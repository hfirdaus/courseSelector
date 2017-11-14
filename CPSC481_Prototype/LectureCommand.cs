using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CPSC481_Prototype
{
    public class LectureCommand : ICommand
    {

        public event EventHandler CanExecuteChanged;

        private Section section;
        private Course course;

        public LectureCommand(Course course, Section section)
        {
            this.course = course;
            this.section = section;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            course.LectureSelected(section);
        }
    }
}
