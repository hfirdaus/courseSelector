using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CPSC481_Prototype
{
    /// <summary>
    /// This is the course the user has selected, with their chosen tutorial,lecture, lab times.
    /// </summary>
    public class CartAndScheduleEntry
    {

        private static int _id = 0;
        public int ID { get; }
        // Department offering the course (e.g. CPSC)
        public string Department { get { return _Department; } }
        private string _Department = "";

        public string Number { get { return _Number; } }
        private string _Number = "";

        // Course Name (e.g. "Calculus I")
        public string Title { get { return _Title; } }
        private string _Title = "";

        public string Semester { get { return _Semester; } }
        private string _Semester = "";

        public Semester SemesterObject { get { return _SemesterObject; } }
        private Semester _SemesterObject;

        public ObservableCollection<Section> Sections { get { return _Sections; } }
        private ObservableCollection<Section> _Sections = new ObservableCollection<Section>();

        public ICommand Enroll { get { return _Enroll; } } // Only show for Cart
        private ICommand _Enroll;

        public ICommand Remove { get { return _Remove; } }
        private ICommand _Remove;

        public CartAndScheduleEntry(string Department, 
                                    string Number, 
                                    string Title, 
                                    string Semester, 
                                    Semester SemesterObject, 
                                    Section lecture, 
                                    Section lab, 
                                    Section tutorial)
        {
            this.ID = _id++;
            this._Department = Department;
            this._Number = Number;
            this._Title = Title;
            this._Semester = Semester;
            this._SemesterObject = SemesterObject;
            this._Sections.Add(lecture);
            this._Sections.Add(lab);
            this._Sections.Add(tutorial);
            this._Remove = new RemoveFromCartCommand(this);
            this._Enroll = new EnrollCourseCommand(this);
        }

        public void UpdateRemoveFromSchedule() 
        {
            this._Remove = new RemoveFromScheduleCommand(this);
        }
    }
}
