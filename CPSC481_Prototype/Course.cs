using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CPSC481_Prototype
{
    public class Semester
    {
        public static Semester FALL   = new Semester("Fall");
        public static Semester WINTER = new Semester("Winter");
        public static Semester SPRING = new Semester("Spring");
        public static Semester SUMMER = new Semester("Summer");
        public static List<Semester> ALL_SEMESTERS = new List<Semester> { FALL, WINTER, SPRING, SUMMER };
        public static List<Semester> FALL_WINTER = new List<Semester> { FALL, WINTER };

        private string name;

        private Semester(String name)
        {
            this.name = name;
        }

        public static Semester SearchSemester(string semester)
        {
            foreach (Semester s in ALL_SEMESTERS)
            {
                if(s.ToString().ToLower() == semester.ToLower())
                {
                    return s;
                }
            }
            return null;
        }

        override
        public string ToString()
        {
            return name;
        }

    }

    /// <summary>
    /// A course is offered for a given semester, and can be made up of multiple offerings.
    /// </summary>
    public class Course
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

        // Course Description
        public string Description { get { return _Description; } }
        private string _Description = "";

        public string Semester { get { return _Semester; } }
        private string _Semester = "";
        public Semester SemesterObject { get { return _SemesterObject; } }
        private Semester _SemesterObject;


        public ICommand Button_Click { get { return Click_Command; } }

        private ICommand Click_Command;

        // A list of lecture sections
        public ObservableCollection<Section> Lectures { get { return _Lectures; } }
        private ObservableCollection<Section> _Lectures = new ObservableCollection<Section>();

        // A list of tutorial sections
        public ObservableCollection<Section> Tutorials { get { return _Tutorials; } }
        private ObservableCollection<Section> _Tutorials = new ObservableCollection<Section>();

        // A list of lab sections
        public ObservableCollection<Section> Labs { get { return _Labs; } }
        private ObservableCollection<Section> _Labs = new ObservableCollection<Section>();

        // A course can have 1 or more offerings.
        private List<Offering> offerings = new List<Offering>();

        public Course(string Department, string Number, string Title, string Description, Semester Semester, int year)
        {
            this.ID = _id++;
            this._Department = Department;
            this._Number = Number;
            this._Title = Title;
            this._Description = Description;
            this._Semester = Semester + " " + year;
            this._SemesterObject = Semester;
            Click_Command = new CourseCommand(this);
        }

        public void AddOffering(Offering offering)
        {
            offerings.Add(offering);
            _Lectures.Add(offering.Lecture);
            foreach (Section tutorial in offering.Tutorials)
            {
                _Tutorials.Add(tutorial);
            }
            foreach (Section lab in offering.Labs)
            {
                _Labs.Add(lab);
            }
        }

        public void LectureSelected(Section section)
        {
            foreach (Offering offering in offerings)
            {
                if (offering.Lecture == section)
                {
                    foreach (Section tutorial in offering.Tutorials)
                    {
                        tutorial.Selectable = true;
                    }
                    foreach (Section lab in offering.Labs)
                    {
                        lab.Selectable = true;
                    }
                    continue;
                }
                foreach (Section tutorial in offering.Tutorials)
                {
                    tutorial.Selectable = false;
                    tutorial.IsChecked = false;
                }
                foreach (Section lab in offering.Labs)
                {
                    lab.Selectable = false;
                    lab.IsChecked = false;
                }
            }
        }

        public Offering SelectedOffering()
        {
            Offering chosen = null;
            foreach(Offering offering in offerings)
            {
                if(offering.Lecture.IsChecked == true)
                {
                    if (offering.Tutorials.Count > 0)
                    {
                        bool isChecked = false;
                        foreach (Section tutorial in offering.Tutorials)
                        {
                            if (tutorial.IsChecked == true)
                            {
                                isChecked = true;
                                break;
                            }
                        }
                        if (!isChecked)
                            break;
                    }
                    if (offering.Labs.Count > 0)
                    {
                        bool isChecked = false;
                        foreach (Section lab in offering.Labs)
                        {
                            if (lab.IsChecked == true)
                            {
                                isChecked = true;
                                break;
                            }
                        }
                        if (!isChecked)
                            break;
                    }
                    chosen = offering;
                    break;
                }
            }
            return chosen;
        }
    }
}
