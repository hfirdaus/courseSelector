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
    class CartSelections : INotifyCollectionChanged
    {
        public static CartSelections instance = new CartSelections();

        // List of chosen offerings in cart
        public ObservableCollection<CartAndScheduleEntry> cart;

        // List of chosen offerings currently visible in cart
        public ObservableCollection<CartAndScheduleEntry> visible;

        // Event for when the list is changed
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private List<Semester> visibleSemesters = new List<Semester>();

        private CartSelections()
        {
            cart = new ObservableCollection<CartAndScheduleEntry>();
            visible = new ObservableCollection<CartAndScheduleEntry>();
        }

        public static CartAndScheduleEntry AddToCart(Course course)
        {
            Console.WriteLine("Moving this course to cart");

            Offering selected = course.SelectedOffering();
            Section chosenLecture = selected.Lecture;
            Section chosenTutorial = null;
            Section chosenLab = null;

            if (selected.Tutorials.Count > 0)
            {
                foreach (Section tutorial in selected.Tutorials)
                {
                    if (tutorial.IsChecked == true)
                    {
                        chosenTutorial = tutorial;
                        break;
                    }
                }
            }
            if (selected.Labs.Count > 0)
            {
                foreach (Section lab in selected.Labs)
                {
                    if (lab.IsChecked == true)
                    {
                        chosenLab = lab;
                        break;
                    }
                }
            }
            CartAndScheduleEntry entry = new CartAndScheduleEntry(course.Department, course.Number, course.Title, course.Semester, course.SemesterObject, chosenLecture, chosenLab, chosenTutorial);
            instance.cart.Insert(0, entry);
            if (instance.visibleSemesters.Contains(entry.SemesterObject))
            {
                instance.visible.Insert(0, entry);
                NotifyChange(NotifyCollectionChangedAction.Add, entry);
            }
            return entry;
        }

        public static CartAndScheduleEntry AddToCart(CartAndScheduleEntry entry) {
            instance.cart.Insert(0, entry);
            if (instance.visibleSemesters.Contains(entry.SemesterObject))
            {
                instance.visible.Insert(0, entry);
                NotifyChange(NotifyCollectionChangedAction.Add, entry);
            }
            return entry;
        }

        public static CartAndScheduleEntry RemoveFromCart(CartAndScheduleEntry entry)
        {
            if (instance.visible.Contains(entry))
            {
                instance.visible.Remove(entry);
                if (instance.CollectionChanged != null)
                    instance.CollectionChanged(instance, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, entry));
            }
            if (instance.cart.Contains(entry))
                instance.cart.Remove(entry);
            return entry;
        }

            public static void ClearCart()
            {
                instance.cart.Clear();
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

                foreach (CartAndScheduleEntry c in instance.cart)
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
                    Console.WriteLine("Notified change in cart");
                }
                else
                {
                    Console.WriteLine("Did not notify change in cart");
                }
            }   
        }


    }
