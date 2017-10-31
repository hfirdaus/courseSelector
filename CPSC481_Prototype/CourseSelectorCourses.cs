using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        public static void addCourse(int num)
        {
            // Make a course
            Course newCourse = new Course("Course Title " + num, "Course Description " + num, "Course Semester " + num);

            // Add the course to the visible list
            instance.visable.Add(newCourse);

            // Notify listeners of the addition
            if(instance.CollectionChanged != null)
                instance.CollectionChanged(instance, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, null));

            // Debug log
            Console.WriteLine("Added course " + num);
        }

        // TODO implement remove course
        public void removeCourse(Course course)
        {
            
        }

    }

    /// <summary>
    /// A course is offered for a given semester, and can be made up of multiple offerings.
    /// </summary>
    public class Course
    {
        // Department offering the course (e.g. CPSC)
        public string Department { get { return _Department; } }
        private string _Department = "";

        // Course Name (e.g. "Calculus I")
        public string Title { get { return _Title; } }
        private string _Title = "";

        // Course Description
        public string Description { get { return _Description; } }
        private string _Description = "";
        

        public ICommand Button_Click { get { return Click_Command; } }

        private ICommand Click_Command;

        // A list of lecture sections
        public List<Section> Lectures;

        // A list of tutorial sections
        public List<Section> Tutorials;

        // A list of lab sections
        public List<Section> Labs;

        // A course can have 1 or more offerings.
        private List<Offering> offerings;

        public Course(string Title, string Description, string Semester, int year)
        {
            this._Title = Title;
            this._Description = Description;
            Click_Command = new CourseCommand(this);
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
            MessageBox.Show("Button was clicked for course " + course.Title + "!");
        }
    }

    public class Offering
    {
        public Section Lecture { get; set; }

        public List<Section> Tutorials;

        public List<Section> Labs;

    }

    public class Section
    {
        // The name of the section (E.g. "Lec 01")
        public string Name { get; set; }

        // The time of the section (E.g. "MWF 2:00 - 4:00")
        public string Time { get; set; }

        // Whether or not the section is selectable
        public bool Selectable { get; set; }
    }
}
