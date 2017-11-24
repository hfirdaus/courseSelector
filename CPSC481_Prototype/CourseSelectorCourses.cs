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

    class CourseSelectorCourses : INotifyCollectionChanged
    {
        public static CourseSelectorCourses instance = new CourseSelectorCourses();

        // Instance list of visable courses. Will be filtered if necessary
        public ObservableCollection<Course> visable;

        // Instance list of all courses
        private ObservableCollection<Course> courses;

        // Event for when the visable list is changed
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private List<Semester> visibleSemesters = new List<Semester>();

        private CourseSelectorCourses()
        {
            visable = new ObservableCollection<Course>();
            courses = new ObservableCollection<Course>();
        }

        public static void AddCourse(Course course)
        {
            Console.WriteLine("Added course");
            instance.courses.Insert(0, course);
            if (instance.visibleSemesters.Contains(course.SemesterObject))
            {
                instance.visable.Insert(0, course); 
                Console.WriteLine("Added course to visable");
                NotifyChange(NotifyCollectionChangedAction.Add, course);
            }
        }

        // TODO implement remove course
        public static void RemoveCourse(Course course)
        {
            if (instance.visable.Contains(course))
            {
                instance.courses.Remove(course);
                instance.visable.Remove(course);
                if (instance.CollectionChanged != null)
                    instance.CollectionChanged(instance, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, course));

            }
        }

        public static void AddVisibleSemester(Semester semester)
        {
            if(!instance.visibleSemesters.Contains(semester))
            {
                instance.visibleSemesters.Add(semester);
            }
            UpdateSemesters();
        }

        public static void RemoveVisibleSemester(Semester semester)
        {
            if(instance.visibleSemesters.Contains(semester))
            {
                instance.visibleSemesters.Remove(semester);
            }
            UpdateSemesters();
        }

        private static void UpdateSemesters()
        {
            instance.visable.Clear();
            foreach(Course c in instance.courses)
            {
                if(instance.visibleSemesters.Contains(c.SemesterObject))
                {
                    instance.visable.Add(c);
                }
            }
        }

        private static void NotifyChange(NotifyCollectionChangedAction action, Course course)
        {
            if (instance.CollectionChanged != null)
            {
                instance.CollectionChanged(instance, new NotifyCollectionChangedEventArgs(action));
                Console.WriteLine("Notified change");
            }
            else
            {
                Console.WriteLine("Did not notify change");
            }
        }
    }

}
