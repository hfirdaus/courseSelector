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
            instance.courses.Add(course);
            if (instance.visibleSemesters.Contains(course.SemesterObject))
            {
                instance.visable.Add(course);
                NotifyChange(NotifyCollectionChangedAction.Add, course);
            }
        }

        // TODO implement remove course
        public static void RemoveCourse(Course course)
        {
            if (instance.visable.Contains(course))
            {
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
            ObservableCollection<Course> oldList = instance.visable;
            instance.visable = new ObservableCollection<Course>();
            foreach(Course c in instance.courses)
            {
                if(instance.visibleSemesters.Contains(c.SemesterObject))
                {
                    instance.visable.Add(c);
                }
            }

            foreach(Course c in oldList)
            {
                if(!instance.visable.Contains(c))
                {
                    NotifyChange(NotifyCollectionChangedAction.Remove, c);
                }
            }
            foreach(Course c in instance.visable)
            {
                if(!oldList.Contains(c))
                {
                    NotifyChange(NotifyCollectionChangedAction.Add, c);
                }
            }

        }

        private static void NotifyChange(NotifyCollectionChangedAction action, Course course)
        {
            if (instance.CollectionChanged != null)
            {
                instance.CollectionChanged(instance, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                Console.WriteLine("Notified change");
            }
            else
            {
                Console.WriteLine("Did not notify change");
            }
        }
    }

}
