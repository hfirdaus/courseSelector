using System;
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

        int number = 0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach(Semester semester in Semester.ALL_SEMESTERS)
            {
                Course newCourse = new CPSC481_Prototype.Course("Department", "" + number++, "Title", "Description", semester, 2017);
                for (int i = 0; i < 2; i++)
                {
                    Offering offering = new Offering();
                    Section lecture = new Section();
                    lecture.Name = "Lecture " + i;
                    lecture.Time = "MWF 0:00";
                    lecture.Select_Command = new LectureCommand(newCourse, lecture);
                    offering.Lecture = lecture;
                    for(int j = 0; j < 2; j++)
                    {
                        Section tutorial = new Section();
                        tutorial.Name = "Tutorial " + j;
                        tutorial.Time = "W 1:11";
                        offering.Tutorials.Add(tutorial);
                    }
                    for (int j = 0; j < 2; j++)
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

        private void Advanced_Search_Click(object sender, RoutedEventArgs e)
        {
            String Faculty_Selection = "";
            String Level_Selection = "";
            String Text_Selection = Advanced_Search_Textbox.Text;

            try
            {
                Faculty_Selection = Course_Faculty_Combobox.SelectedItem.ToString();
            }
            catch (Exception)
            {
                Faculty_Selection = "";
            }

            try
            {
                Level_Selection = Course_Level_Combobox.SelectedItem.ToString();
            }
            catch (Exception)
            {
                Level_Selection = "";
            }

            //Check what data is in the Boxes and change the output accordingly, not dtynamic, this is hardcoded.
            //First see if it is BSEN
            if (Faculty_Selection == "System.Windows.Controls.ComboBoxItem: BSEN - Business") {
                //Then Level
                if (Level_Selection == "System.Windows.Controls.ComboBoxItem: 300")
                {
                    BSEN395();      
                }
                else if (Level_Selection == "System.Windows.Controls.ComboBoxItem: 400")
                {
                    BSEN401();
                }
                else
                {
                    BSEN395();
                    BSEN401();
                }
            }
            Console.WriteLine(Faculty_Selection);
            Console.WriteLine("KEYWORD: " +  Text_Selection);

            Requirement_Popup.Visibility = Visibility.Hidden;
            Course_Search_Panel.Visibility = Visibility.Hidden;
            Degree_Search_Panel.Visibility = Visibility.Hidden;
        }

        private void BSEN395 ()
        {
            
            foreach (Semester semester in Semester.FALL_WINTER)
            {
                Course newCourse = new CPSC481_Prototype.Course("Department", "" + number++, "BSEN 395: Business in Canada", "Business law topics may include: regulatory compliance and environment management, tort and contractual liability, legal issues affecting the strategic management of sole proprietorships, partnerships, corporations and joint ventures, personal liability of corporate directors and officers, intellectual property, advertising and promotion law, consumer protection legislation, legal issues affecting employees and independent contractors, the strategic management of international business, securities law and other current business law issues.Business law topics may include: regulatory compliance and environment management, tort and contractual liability, legal issues affecting the strategic management of sole proprietorships, partnerships, corporations and joint ventures, personal liability of corporate directors and officers, intellectual property, advertising and promotion law, consumer protection legislation, legal issues affecting employees and independent contractors, the strategic management of international business, securities law and other current business law issues.", semester, 2017);
                for (int i = 0; i < 2; i++)
                {
                    Offering offering = new Offering();
                    Section lecture = new Section();
                    lecture.Name = "Lecture " + i;
                    lecture.Time = "MWF 0:00";
                    lecture.Select_Command = new LectureCommand(newCourse, lecture);
                    offering.Lecture = lecture;
                    for (int j = 0; j < 2; j++)
                    {
                        Section tutorial = new Section();
                        tutorial.Name = "Tutorial " + j;
                        tutorial.Time = "W 1:11";
                        offering.Tutorials.Add(tutorial);
                    }
                    for (int j = 0; j < 2; j++)
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
        }

        private void BSEN401()
        {
            foreach (Semester semester in Semester.ALL_SEMESTERS)
            {
                Course newCourse = new CPSC481_Prototype.Course("Department", "" + number++, "BSEN 401: Dilemmas and Decisions in Business", "A comparative analysis of Canada's competitive position in the global economy utilizing case studies analyzing strategies employed by Canadian corporations to be successful in world markets.", semester, 2017);
                
                    Offering offering = new Offering();
                    Section lecture = new Section();
                    lecture.Name = "Lecture " + 1;
                    lecture.Time = "MWF 0:00";
                    lecture.Select_Command = new LectureCommand(newCourse, lecture);
                    offering.Lecture = lecture;
                    for (int j = 0; j < 2; j++)
                    {
                        Section tutorial = new Section();
                        tutorial.Name = "Tutorial " + j;
                        tutorial.Time = "W 1:11";
                        offering.Tutorials.Add(tutorial);
                    }
                    
                    newCourse.AddOffering(offering);
                
                CourseSelectorCourses.AddCourse(newCourse);
            }
        }

        

    }
}

