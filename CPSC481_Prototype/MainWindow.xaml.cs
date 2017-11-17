using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CPSC481_Prototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CourseSelectorCourses.addCourse("MGST", "217", "Introduction to Business Analytics", "COURSE DESCRIPTION", "Fall", "2017", 2, 2, 2);
            CourseSelectorCourses.addCourse("MGST", "217", "Introduction to Business Analytics", "COURSE DESCRIPTION", "Winter", "2017", 2, 2, 2);
            CourseSelectorCourses.addCourse("SGMA", "217", "Introduction to Business Skills", "COURSE DESCRIPTION", "Fall", "2017", 2, 2, 2);
            CourseSelectorCourses.addCourse("SGMA", "217", "Introduction to Business Skills", "COURSE DESCRIPTION", "Winter", "2017", 2, 2, 2);
            Course_Selector_Items.ItemsSource = CourseSelectorCourses.instance.visable;
        }

        private void Requirement_Popup_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Requirement_Popup.Visibility == Visibility.Visible)
            {
                Requirement_Popup.Visibility = Visibility.Hidden;
            }
        }
        private void Show_Complete_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Show_Completed_Stack_Panel.Visibility = Visibility.Visible;
        }

        private void Show_Complete_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Show_Completed_Stack_Panel.Visibility = Visibility.Collapsed;
        }

        private void Add_to_Cart_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Added to Cart"); 
        }

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            Requirement_Popup.Visibility = Visibility.Visible;
            Course_Search_Panel.Visibility = Visibility.Visible;
            Degree_Search_Panel.Visibility = Visibility.Hidden;
        }

        private void Add_Degree_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Requirement_Popup.Visibility = Visibility.Visible;
            Course_Search_Panel.Visibility = Visibility.Hidden;
            Degree_Search_Panel.Visibility = Visibility.Visible;
        }

        //private void Course_ACCT_Click(object sender, MouseButtonEventArgs e)
        //{
        //    Main_Faculty_Menu.Header = "ACCT - Accounting";
        //}

        private void Remove_From_Cart(object sender, RoutedEventArgs e)
        {
        }

        private void Enroll(object sender, RoutedEventArgs e)
        {
        }

        private void MGST217_MouseDown(object sender, RoutedEventArgs e)
        {
            if (CourseSelectorCourses.instance.visable.Count != 0)
            {
                int visCount = CourseSelectorCourses.instance.visable.Count;
                for (int i = 0; i < visCount; i++)
                {
                    CourseSelectorCourses.instance.visable.RemoveAt(0);
                }
            }

            foreach (Course clickedCourse in CourseSelectorCourses.instance.courses)
            {
                if (clickedCourse.Department == "MGST" && clickedCourse.Number == "217")
                {
                    CourseSelectorCourses.instance.visable.Add(clickedCourse);
                }
            }
        }

        private void SGMA217_MouseDown(object sender, RoutedEventArgs e)
        {
            if (CourseSelectorCourses.instance.visable.Count != 0)
            {
                //CourseSelectorCourses.removeCourse();
                
                int visCount = CourseSelectorCourses.instance.visable.Count;
                for (int i = 0; i < visCount; i++)
                {
                    CourseSelectorCourses.instance.visable.RemoveAt(0);
                }
                
            }

            foreach (Course clickedCourse in CourseSelectorCourses.instance.courses)
            {
                if (clickedCourse.Department == "SGMA" && clickedCourse.Number == "217")
                {
                    Console.WriteLine(clickedCourse.Semester); 
                    CourseSelectorCourses.instance.visable.Add(clickedCourse);
                }
            }
        }

        private void CheckBox_Fall(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Course> temp = new ObservableCollection<Course>();
            temp = CourseSelectorCourses.instance.visable;

            int visCount = CourseSelectorCourses.instance.visable.Count;
            for (int i = 0; i < visCount; i++)
            {
                CourseSelectorCourses.instance.visable.RemoveAt(0);
            }

            foreach (Course fallCourse in temp)
            {
                if(fallCourse.Semester.Equals("Fall 2017"))
                {
                    CourseSelectorCourses.instance.visable.Add(fallCourse);
                }
            }

        }

        private void DeleteCourse(object sender, RoutedEventArgs e)
        {
            
            
        }
    }
}
