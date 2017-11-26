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

            // Event for when the list is changed
            public event NotifyCollectionChangedEventHandler CollectionChanged;

            private List<Semester> visibleSemesters = new List<Semester>();

            private CartSelections()
            {
                cart = new ObservableCollection<CartAndScheduleEntry>();
            }

        public static void AddCartAndScheduleEntry(Course course)
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
            NotifyChange(NotifyCollectionChangedAction.Add, entry);
        }

            public static void RemoveCartAndScheduleEntry(CartAndScheduleEntry entry)
            {
                if (instance.cart.Contains(entry))
                {
                    instance.cart.Remove(entry);
                    if (instance.CollectionChanged != null)
                        instance.CollectionChanged(instance, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, entry));

                }
            }

            public static void ClearCart()
            {
                instance.cart.Clear();
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
                instance.cart.Clear();
                foreach (CartAndScheduleEntry c in instance.cart)
                {
                    if (instance.visibleSemesters.Contains(c.SemesterObject))
                    {
                        instance.cart.Add(c);
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
