﻿using System;
using System.Collections.Generic;
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

            Course_Selector_Items.ItemsSource = CourseSelectorCourses.instance.visable;
        }

        private void Tutorial_Button_Click(object sender, RoutedEventArgs e)
        {
            Tutorial_Popup.Visibility = Visibility.Visible;
        }

        private void Tutorial_Popup_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(Tutorial_Popup.Visibility == Visibility.Visible)
            {
                Tutorial_Popup.Visibility = Visibility.Hidden;
            }
        }

        //Changing for the pop-up screens for adding an additional degree and searching for courses back to the main screen. 
        private void Requirement_Popup_MouseDown(object sender, RoutedEventArgs e)
        {
            if (Requirement_Popup.Visibility == Visibility.Visible)
            {
                Requirement_Popup.Visibility = Visibility.Hidden;
            }

            //Clearing the selections in the comboboxs in the add an additional degree pop-up.
            cmbDegree.SelectedIndex = -1;
            cmbMajor.SelectedIndex = -1;
            cmbConcentration.SelectedIndex = -1;
            cmbMinor.SelectedIndex = -1;
            cmbMajor.Items.Remove("ENGL - English");
            cmbMajor.Items.Remove("CPSC - Computer Science");
            cmbConcentration.Items.Remove("Creative Writing");
            cmbConcentration.Items.Remove("Software Engineering");
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

        public void Option_Search_MouseDown(object sender, MouseButtonEventArgs e)
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

        private void DeleteCourse(object sender, RoutedEventArgs e)
        {

        }

        private void Semester_Checked(object sender, RoutedEventArgs e)
        {
            if (((CheckBox)sender).IsChecked == true)
                CourseSelectorCourses.AddVisibleSemester(Semester.SearchSemester(((CheckBox)sender).Tag.ToString()));
            else
                CourseSelectorCourses.RemoveVisibleSemester(Semester.SearchSemester(((CheckBox)sender).Tag.ToString()));

        }

        private void MGST217_MouseDown(object sender, RoutedEventArgs e)
        {
            int contain_flag = 0;
            foreach (Course check_course in CourseSelectorCourses.instance.courses)
            {
                if(check_course.Department.Equals("MGST") && check_course.Number.Equals("217"))
                {
                    contain_flag = 1;
                }
            }
            
            if(contain_flag == 0)
            {
                foreach (Semester semester in Semester.ALL_SEMESTERS.Take(2))
                {
                    int year = 0;
                    if (semester == Semester.FALL) year = 2017;
                    else year = 2018;
                    Add_Course_Click("MGST", "217", "Introduction to Business Analytics", "COURSE DESCR", semester, year, 2, 2, 2);
                }
            }
        }

        private void SGMA217_MouseDown(object sender, RoutedEventArgs e)
        {
            int contain_flag = 0;
            foreach (Course check_course in CourseSelectorCourses.instance.courses)
            {
                if (check_course.Department.Equals("SGMA") && check_course.Number.Equals("217"))
                {
                    contain_flag = 1;
                }
            }

            if (contain_flag == 0)
            {
                foreach (Semester semester in Semester.ALL_SEMESTERS.Take(2))
                {
                    int year = 0;
                    if (semester == Semester.FALL) year = 2017;
                    else year = 2018;
                    Add_Course_Click("SGMA", "217", "Introduction to Business Skills", "COURSE DESCR", semester, year, 2, 2, 2);
                }
            }
        }

        private void ECON201_MouseDown(object sender, RoutedEventArgs e)
        {
            int contain_flag = 0;
            foreach (Course check_course in CourseSelectorCourses.instance.courses)
            {
                if (check_course.Department.Equals("ECON") && check_course.Number.Equals("201"))
                {
                    contain_flag = 1;
                }
            }

            if (contain_flag == 0)
            {
                foreach (Semester semester in Semester.ALL_SEMESTERS.Take(3))
                {
                    int year = 0;
                    if (semester == Semester.FALL) year = 2017;
                    else year = 2018;
                    Add_Course_Click("ECON", "201", "Principles of Microeconomic", "COURSE DESCR", semester, year, 2, 2, 2);
                }
            }
        }

        private void Add_Course_Click(string dept, string number, string title, string descr, Semester semester, int year, int num_of_Off, int num_of_Tut, int num_of_Lab)
        {
            Course newCourse = new CPSC481_Prototype.Course(dept, number, title, descr, semester, year);
            for (int i = 0; i < num_of_Off; i++)
            {
                Offering offering = new Offering();
                Section lecture = new Section();
                lecture.Name = "Lecture " + i;
                lecture.Time = "MWF 0:00";
                lecture.Select_Command = new LectureCommand(newCourse, lecture);
                offering.Lecture = lecture;
                for (int j = 0; j < num_of_Tut; j++)
                {
                    Section tutorial = new Section();
                    tutorial.Name = "Tutorial " + j;
                    tutorial.Time = "W 1:11";
                    offering.Tutorials.Add(tutorial);
                }
                for (int j = 0; j < num_of_Lab; j++)
                {
                    Section lab = new Section();
                    lab.Name = "Lab " + j;
                    lab.Time = "W 1:11";
                    offering.Labs.Add(lab);
                }
                newCourse.AddOffering(offering);
            }
            CourseSelectorCourses.AddCourse(newCourse);
        }

        private void Remove_Course_Click()
        {

        }

        //List containing the selections from the additional degree pop-up combobox.
        List<string> selection_cmb = new List<string>();

        private void addButton_cmb_Click(object sender, RoutedEventArgs e)
        {
            if (Requirement_Popup.Visibility == Visibility.Visible)
            {
                Requirement_Popup.Visibility = Visibility.Hidden;
            }

            if (selection_cmb.Contains("ECON_Minor")) {
                Econ_Minor.Visibility = Visibility.Visible;
                Phil_Minor.Visibility = Visibility.Collapsed;
            }

            if (selection_cmb.Contains("PHIL_Minor"))
            {
                Phil_Minor.Visibility = Visibility.Visible;
                Econ_Minor.Visibility = Visibility.Collapsed;
                selection_cmb.Clear();
            }
        }

        private void cmb_DropDownClosed(object sender, EventArgs e)
        {
            cmb_Filter();
        }

        private void cmb1_DropDownClosed(object sender, EventArgs e)
        {
            cmb_Filter1();
        }

        //Fix issue where only one is selected instead of both.
        private void cmb_Minor_DropDownClosed(object sender, EventArgs e)
        {
            if (selection_cmb.Contains("ECON_Minor")) {
                selection_cmb.Clear();
                selection_cmb.Add("ECON_Minor");
            }

            else if(selection_cmb.Contains("PHIL_Minor"))
            {
                selection_cmb.Clear();
                selection_cmb.Add("PHIL_Minor");
            }
                
        }

        
        private void cmb_Filter()
        {
            String cmbItem;
            if (cmbDegree.SelectedItem != null)
            {
                cmbItem = cmbDegree.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last();
                if(cmbItem.Equals("Bachelor of Arts"))
                {
                    cmbMajor.Items.Remove("ENGL - English");
                    cmbMajor.Items.Remove("CPSC - Computer Science");
                    cmbMajor.Items.Add("ENGL - English");
                }
                else if(cmbItem.Equals("Bachelor of Science"))
                {
                    cmbMajor.Items.Remove("ENGL - English");
                    cmbMajor.Items.Remove("CPSC - Computer Science");
                    cmbMajor.Items.Add("CPSC - Computer Science");
                }
            }
        }

        private void cmb_Filter1()
        {
            String cmbItem;
            if (cmbMajor.SelectedItem != null)
            {
                cmbItem = cmbMajor.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last();
                Console.WriteLine(cmbItem);
                if(cmbItem.Equals("ENGL - English"))
                {
                    cmbConcentration.Items.Remove("Creative Writing");
                    cmbConcentration.Items.Remove("Software Engineering");
                    cmbConcentration.Items.Add("Creative Writing");
                }
                else if(cmbItem.Equals("CPSC - Computer Science"))
                {
                    cmbConcentration.Items.Remove("Creative Writing");
                    cmbConcentration.Items.Remove("Software Engineering");
                    cmbConcentration.Items.Add("Software Engineering");
                }
            }

        }

        private void Remove_SENG_Conc_Click(object sender, RoutedEventArgs e)
        {
            SoftEng_Conc.Visibility = Visibility.Collapsed;
        }

        private void Remove_ECON_Minor_Click(object sender, RoutedEventArgs e)
        {
            Econ_Minor.Visibility = Visibility.Collapsed;
        }

        private void Remove_PHIL_Minor_Click(object sender, RoutedEventArgs e)
        {
            Phil_Minor.Visibility = Visibility.Collapsed;
        }

    }
}

