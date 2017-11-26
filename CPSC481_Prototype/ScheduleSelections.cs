using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CPSC481_Prototype
{
    class ScheduleSelections : INotifyCollectionChanged
    {
        public static ScheduleSelections instance = new ScheduleSelections();

        // List of chosen offerings in schedule
        public ObservableCollection<CartAndScheduleEntry> schedule;

        // List of chosen offerings currently visible in cart
        public ObservableCollection<CartAndScheduleEntry> visible;

        // Event for when the list is changed
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private List<Semester> visibleSemesters = new List<Semester>();

        private ScheduleSelections()
        {
            schedule = new ObservableCollection<CartAndScheduleEntry>();
            visible = new ObservableCollection<CartAndScheduleEntry>();
        }

        public static void AddToSchedule(CartAndScheduleEntry entry)
        {
            Console.WriteLine("Moving this course to schedule");
            instance.schedule.Insert(0, entry);
            if (instance.visibleSemesters.Contains(entry.SemesterObject))
            {
                instance.visible.Insert(0, entry);
                NotifyChange(NotifyCollectionChangedAction.Add, entry);
            }
        }

        public static void RemoveFromSchedule(CartAndScheduleEntry entry)
        {
            if (instance.visible.Contains(entry))
            {
                instance.visible.Remove(entry);
                if (instance.CollectionChanged != null)
                    instance.CollectionChanged(instance, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, entry));
            }
            if (instance.schedule.Contains(entry))
                instance.schedule.Remove(entry);
        }

        public static void ClearSchedule()
        {
            instance.schedule.Clear();
            instance.visible.Clear();
            if (instance.CollectionChanged != null)
                instance.CollectionChanged(instance, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public static void AddVisibleSemester(Semester semester)
        {
            if (!instance.visibleSemesters.Contains(semester))
            {
                instance.visibleSemesters.Add(semester);
            }
            UpdateSemesters();
        }

        public static void RemoveVisibleSemester(Semester semester)
        {
            if (instance.visibleSemesters.Contains(semester))
            {
                instance.visibleSemesters.Remove(semester);
            }
            UpdateSemesters();
        }

        private static void UpdateSemesters()
        {
            instance.visible.Clear();

            foreach (CartAndScheduleEntry c in instance.schedule)
            {
                if (instance.visibleSemesters.Contains(c.SemesterObject))
                {
                    instance.visible.Add(c);
                }
            }
        }

        private static void NotifyChange(NotifyCollectionChangedAction action, CartAndScheduleEntry entry)
        {
            if (instance.CollectionChanged != null)
            {
                instance.CollectionChanged(instance, new NotifyCollectionChangedEventArgs(action));
                Console.WriteLine("Notified change in schedule");
            }
            else
            {
                Console.WriteLine("Did not notify change in schedule");
            }
        }
    }


}
