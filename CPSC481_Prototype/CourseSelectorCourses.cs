using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CPSC481_Prototype
{
    abstract class Semester
    {
        public static string FALL = "Fall";
        public static string WINTER = "Winter";
        public static string SPRING = "Spring";
        public static string SUMMER = "Summer";
    }

    class CourseSelectorCourses : INotifyCollectionChanged
    {
        /*
         * TODO:
         *  1) Add ability to add courses
         *  2) Add ability to remove courses
         *  3) Add logic to tie to the Fall/Winter/Spring/Summer boxes
         */


        public static CourseSelectorCourses instance = new CourseSelectorCourses();

        // Instance list of visable courses. Will be filtered if necessary
        public ObservableCollection<Course> visable;

        // Instance list of all courses
        public ObservableCollection<Course> courses;

        // Event for when the visable list is changed
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private CourseSelectorCourses()
        {
            visable = new ObservableCollection<Course>();
            courses = new ObservableCollection<Course>();
        }

        // TODO this needs to be made to add an actual course. It is just making a new dummy course using the number given.
        public static void addCourse(string dept, string numb, string title, string desc, string sem, string year, int numofLec, int numofTut, int numofLab)
        {
            // Make a course
            Course newCourse = new Course(dept, numb, title, desc, sem, year);

            for (int lec = 1; lec <= numofLec; lec++)
            {
                Offering offering = new Offering();
                Section s = new Section()
                {
                    Name = "Lecture " + lec
                };
                s.Select_Command = new LectureCommand(newCourse, s);
                offering.Lecture = s;
                for (int i = 0; i < numofTut; i++)
                {
                    s = new Section()
                    {
                        Name = "Tutorial " + i
                    };
                    offering.Tutorials.Add(s);
                }
                for (int i = 0; i < numofLab; i++)
                {
                    s = new Section()
                    {
                        Name = "Lab " + i
                    };
                    
                    offering.Labs.Add(s);
                    
                }

                newCourse.AddOffering(offering);
            }

            instance.courses.Add(newCourse);

            // Notify listeners of the addition
            if(instance.CollectionChanged != null)
                instance.CollectionChanged(instance, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, null));

            // Debug log
            Console.WriteLine("Added course");
        }


        // TODO implement remove course
        public static void removeCourse(Course course)
        {
            if (instance.visable.Contains(course))
            {
                instance.visable.Remove(course);
                if (instance.CollectionChanged != null)
                    instance.CollectionChanged(instance, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, course));

            }
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

        public Course(string Department, string Number, string Title, string Description, string Semester, string year)
        {
            this._Department = Department;
            this._Number = Number;
            this._Title = Title;
            this._Description = Description;
            this._Semester = Semester + " " + year;
            Click_Command = new CourseCommand(this);
        }

        public void AddOffering(Offering offering)
        {
            offerings.Add(offering);
            _Lectures.Add(offering.Lecture);
            foreach( Section tutorial in offering.Tutorials ) {
                _Tutorials.Add(tutorial);
            }
            foreach (Section lab in offering.Labs)
            {
                _Labs.Add(lab);
            }
        }

        public void LectureSelected(Section section)
        {
            foreach(Offering offering in offerings)
            {
                if (offering.Lecture == section)
                {
                    foreach(Section tutorial in offering.Tutorials)
                    {
                        tutorial.Selectable = true;
                    }
                    foreach(Section lab in offering.Labs)
                    {
                        lab.Selectable = true;
                    }
                    continue;
                }
                foreach(Section tutorial in offering.Tutorials)
                {
                    tutorial.Selectable = false;
                }
                foreach (Section lab in offering.Labs)
                {
                    lab.Selectable = false;
                }
            }
        }

    }

    public class CourseCommand : ICommand
    {

        public event EventHandler CanExecuteChanged;

        private Course course;

        public CourseCommand(Course course)
        {
            this.course = course;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            CourseSelectorCourses.removeCourse(course);
        }
    }

    public class Offering
    {
        public Section Lecture { get; set; }

        public List<Section> Tutorials = new List<Section>();

        public List<Section> Labs = new List<Section>();

    }

    public class Section : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        // The name of the section (E.g. "Lec 01")
        public string Name { get; set; }

        private string _Name = "";

        // The time of the section (E.g. "MWF 2:00 - 4:00")
        public string Time { get; set; }

        // Whether or not the section is selectable
        public bool Selectable {
            get
            {
                return _Selectable;
            }
            set
            {
                _Selectable = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Selectable"));
            } }
        private bool _Selectable = true;

        public ICommand Section_Selected { get { return Select_Command; } }

        public ICommand Select_Command;
    }

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
