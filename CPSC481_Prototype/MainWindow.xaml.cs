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
using System.Windows.Media.Animation;
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
        public static MainWindow instance;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Course_Selector_Items.ItemsSource = CourseSelectorCourses.instance.visable;
            Cart_Items.ItemsSource = CartSelections.instance.visible;
            Schedule_Items.ItemsSource = ScheduleSelections.instance.visible;
            instance = this;

        }

        public void AddMessage(Message msg)
        {

            Console.Out.WriteLine("Adding message");
            Border border = new Border();
            border.Background = Brushes.AliceBlue;
            border.BorderBrush = Brushes.Black;
            border.Margin = new Thickness(0, 5, 0, 0);
            border.BorderThickness = new Thickness(1);
            border.CornerRadius = new CornerRadius(10);
            border.Opacity = 0;
            border.Width = 250;

            double fadeInDur = 0.5;
            double waitDur = 2.0;
            double fadeOutDur = 1.5;
            
            DoubleAnimation fadeIn = new DoubleAnimation();
            Storyboard.SetTargetProperty(fadeIn, new PropertyPath(Border.OpacityProperty));
            fadeIn.To = 1;
            fadeIn.Duration = new Duration(TimeSpan.FromSeconds(fadeInDur));

            DoubleAnimation quickFadeOut = new DoubleAnimation();
            Storyboard.SetTargetProperty(quickFadeOut, new PropertyPath(Border.OpacityProperty));
            fadeIn.To = 1;
            fadeIn.Duration = new Duration(TimeSpan.FromSeconds(0));

            DoubleAnimation fadeOut = new DoubleAnimation();
            Storyboard.SetTargetProperty(fadeOut, new PropertyPath(Border.OpacityProperty));
            fadeOut.To = 0;
            fadeOut.BeginTime = TimeSpan.FromSeconds(waitDur);
            fadeOut.Duration = new Duration(TimeSpan.FromSeconds(fadeOutDur));
            fadeOut.Completed += (s, e) => RemoveMessage(border, msg);


            Storyboard quickFadeInSB = new Storyboard();
            quickFadeInSB.Children.Add(fadeIn);

            Storyboard fadeOutSB = new Storyboard();
            fadeOutSB.Children.Add(fadeOut);

            Storyboard fullRunSB = new Storyboard();
            fullRunSB.Children.Add(fadeIn);
            fullRunSB.Children.Add(fadeOut);

            EventTrigger loadedTrigger = new EventTrigger();
            border.Triggers.Add(loadedTrigger);
            loadedTrigger.RoutedEvent = Border.LoadedEvent;
            BeginStoryboard bsb = new BeginStoryboard();
            bsb.Storyboard = fullRunSB;
            loadedTrigger.Actions.Add(bsb);

            EventTrigger mouseOverTrigger = new EventTrigger();
            border.Triggers.Add(mouseOverTrigger);
            mouseOverTrigger.RoutedEvent = Border.MouseEnterEvent;
            BeginStoryboard fisb = new BeginStoryboard();
            fisb.Storyboard = quickFadeInSB;
            mouseOverTrigger.Actions.Add(fisb);

            EventTrigger mouseOutTrigger = new EventTrigger();
            border.Triggers.Add(mouseOutTrigger);
            mouseOutTrigger.RoutedEvent = Border.MouseLeaveEvent;
            BeginStoryboard fosb = new BeginStoryboard();
            fosb.Storyboard = fadeOutSB;
            mouseOutTrigger.Actions.Add(fosb);


            StackPanel content = new StackPanel();
            border.Child = content;

            TextBlock text = new TextBlock();
            content.Children.Add(text);
            text.Margin = new Thickness(5);
            text.Text = msg.text;
            text.TextWrapping = TextWrapping.WrapWithOverflow;

            if (msg.clearMsg != null)
            {
                Label undo = new Label();
                content.Children.Add(undo);
                if (msg.clearMsg is UndoAction)
                    undo.Content = "Undo";
                else
                    undo.Content = "Redo";
                undo.Foreground = Brushes.Blue;
                undo.MouseDown += (s, e) => RemoveMessageAction(border, msg);
                undo.MouseEnter += (s, e) => SetMousePoint();
                undo.MouseLeave += (s, e) => SetMouseNormal();
            }
            Messages_Box.Children.Add(border);
        }

        public void RemoveMessage(Border border, Message msg)
        {
            if (border.Opacity == 0)
            {
                Messages_Box.Children.Remove(border);
            }
        }

        public void RemoveMessageAction(Border border, Message msg)
        {
            msg.clearMsg.run();
            Messages_Box.Children.Remove(border);
        }

        private void SetMousePoint()
        {
            Cursor = Cursors.Hand;
        }

        private void SetMouseNormal()
        {
            Cursor = Cursors.AppStarting;
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

        private void Requirement_Popup_Button(object sender, RoutedEventArgs e)
        {
            if (Requirement_Popup.Visibility == Visibility.Visible)
            {
                Requirement_Popup.Visibility = Visibility.Hidden;
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
            Current_Degree_Panel.Visibility = Visibility.Hidden;
        }

        public void Option_Search_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Requirement_Popup.Visibility = Visibility.Visible;
            Course_Search_Panel.Visibility = Visibility.Visible;
            Degree_Search_Panel.Visibility = Visibility.Hidden;
            Current_Degree_Panel.Visibility = Visibility.Hidden;
        }

        private void Add_Degree_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Requirement_Popup.Visibility = Visibility.Visible;
            Course_Search_Panel.Visibility = Visibility.Hidden;
            Degree_Search_Panel.Visibility = Visibility.Visible;
            Current_Degree_Panel.Visibility = Visibility.Hidden;
        }

        private void Current_Degree_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Requirement_Popup.Visibility = Visibility.Visible;
            Course_Search_Panel.Visibility = Visibility.Hidden;
            Degree_Search_Panel.Visibility = Visibility.Hidden;
            Current_Degree_Panel.Visibility = Visibility.Visible;
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
            foreach (Semester semester in Semester.ALL_SEMESTERS)
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

        private void DeleteCourse(object sender, RoutedEventArgs e)
        {

        }

        private void Course_Semester_Checked(object sender, RoutedEventArgs e)
        {
            Console.Write("Course semester checked...");
            if (((CheckBox)sender).IsChecked == true)
                CourseSelectorCourses.AddVisibleSemester(Semester.SearchSemester(((CheckBox)sender).Tag.ToString()));
            else
                CourseSelectorCourses.RemoveVisibleSemester(Semester.SearchSemester(((CheckBox)sender).Tag.ToString()));

        }

        private void Cart_Semester_Checked(object sender, RoutedEventArgs e)
        {
            Console.Write("Cart semester checked...");
            if (((CheckBox)sender).IsChecked == true)
            {
                CartSelections.AddVisibleSemester(Semester.SearchSemester(((CheckBox)sender).Tag.ToString()));
                ScheduleSelections.AddVisibleSemester(Semester.SearchSemester(((CheckBox)sender).Tag.ToString()));
            }
            else { 
                CartSelections.RemoveVisibleSemester(Semester.SearchSemester(((CheckBox)sender).Tag.ToString()));
                ScheduleSelections.RemoveVisibleSemester(Semester.SearchSemester(((CheckBox)sender).Tag.ToString()));
            }
        }

        private void MGST217_MouseDown(object sender, RoutedEventArgs e)
        {
            int contain_flag = 0;
            foreach (Course check_course in CourseSelectorCourses.instance.visable)
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
                    Add_Course_Click("MGST", "217", "MGST 217 - Introduction to Business Analytics", "Introduction to data representation and analysis. Students will think critically about business problems, gather, evaluate, analyze and synthesize relevant data, and create insightful models to improve the quality of decisions. Communicating and presenting quantitative analysis to lead managerial decision making will be emphasized while continuing to advance both individual and group leadership skills.", semester, year, 3, 2, 2);
                }
            }
        }
        
        private void SGMA217_MouseDown(object sender, RoutedEventArgs e)
        {
            
            int contain_flag = 0;
            foreach (Course check_course in CourseSelectorCourses.instance.visable)
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
                    Add_Course_Click("SGMA", "217", "SGMA 217 - Introduction to Business Skills", "Introduction to the external business environment, human resource management, and marketing. Topics will include ethical decision-making, teamwork, secondary research, proper use of business communication tools and presentation skills. Pedagogical approaches may include case analysis, exercises, simulations, and class discussion.", semester, year, 2, 2, 2);
                }
            }
            
        }

        private void ECON201_MouseDown(object sender, RoutedEventArgs e)
        {
            
            int contain_flag = 0;
            foreach (Course check_course in CourseSelectorCourses.instance.visable)
            {
                if (check_course.Department.Equals("ECON") && check_course.Number.Equals("201"))
                {
                    contain_flag = 1;
                }
            }

            if (contain_flag == 0)
            {
                foreach (Semester semester in Semester.ALL_SEMESTERS.Take(4))
                {
                    int year = 0;
                    if (semester == Semester.FALL) year = 2017;
                    else year = 2018;
                    Add_Course_Click("ECON", "201", "ECON 201 - Principles of Microeconomic", "Principles of consumption, production, exchange: market and firm equilibrium under different competitive conditions. These principles are applied to various contemporary problems in the Canadian economy, such as the changing structure of agriculture, foreign ownership and control, and pollution.", semester, year, 3, 3, 0);
                }
            }            
        }

        private void ECON203_MouseDown(object sender, RoutedEventArgs e)
        {

            int contain_flag = 0;
            foreach (Course check_course in CourseSelectorCourses.instance.visable)
            {
                if (check_course.Department.Equals("ECON") && check_course.Number.Equals("203"))
                {
                    contain_flag = 1;
                }
            }

            if (contain_flag == 0)
            {
                foreach (Semester semester in Semester.ALL_SEMESTERS.Take(4))
                {
                    int year = 0;
                    if (semester == Semester.FALL) year = 2017;
                    else year = 2018;
                    Add_Course_Click("ECON", "203", "ECON 203 - Principles of Macroeconomic", "National income determination, the monetary and banking system, and elementary fiscal and monetary policies. Contemporary problems of unemployment, inflation, economic growth, business cycles and the international economy.", semester, year, 2, 4, 0);
                }
            }
        }

        private void ACCT323_MouseDown(object sender, RoutedEventArgs e)
        {

            int contain_flag = 0;
            foreach (Course check_course in CourseSelectorCourses.instance.visable)
            {
                if (check_course.Department.Equals("ACCT") && check_course.Number.Equals("323"))
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
                    Add_Course_Click("ACCT", "323", "ACCT 323 - Introductory Managerial Accounting", "An introduction to the use of accounting within an organizational context. Emphasis is placed on the development and dissemination of accounting information necessary for effective management including: planning, directing, motivating, and controlling activities and behaviours.", semester, year, 3, 2, 0);
                }
            }
        }

        private void MGST451_MouseDown(object sender, RoutedEventArgs e)
        {

            int contain_flag = 0;
            foreach (Course check_course in CourseSelectorCourses.instance.visable)
            {
                if (check_course.Department.Equals("MGST") && check_course.Number.Equals("451"))
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
                    Add_Course_Click("MGST", "451", "MGST 451 - Corporate Governance and Ethical Decision-Making", "Develop an increased awareness of the importance of corporate governance systems and strong financial decision making systems in developing effective business enterprises. Specific emphasis on the resolution of agency problems, the role of the board of directors, compensation systems and financial modelling.", semester, year, 2, 0, 0);
                }
            }
        }

        private void SMGA591_MouseDown(object sender, RoutedEventArgs e)
        {

            int contain_flag = 0;
            foreach (Course check_course in CourseSelectorCourses.instance.visable)
            {
                if (check_course.Department.Equals("SGMA") && check_course.Number.Equals("591"))
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
                    Add_Course_Click("SGMA", "591", "SGMA 591 - Strategic Management", "Analysis of competitive situations from the general management point of view, including fit between key environmental forces and the firm's resources, and changes in these over time. Formulating and implementing strategy based on that analysis. Developing and leveraging a firm's core competencies to gain long-term sustainable advantage.", semester, year, 4, 0, 0);
                }
            }
        }

        private void FNCE451_MouseDown(object sender, RoutedEventArgs e)
        {

            int contain_flag = 0;
            foreach (Course check_course in CourseSelectorCourses.instance.visable)
            {
                if (check_course.Department.Equals("FNCE") && check_course.Number.Equals("451"))
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
                    Add_Course_Click("FNCE", "451", "FNCE 451 - Advanced Financial Management", "Focuses on understanding the advanced theories and practices of financial management that are required for finance majors. Topics include market imperfections arising from asymmetric information and taxation. It applies these concepts to study incentives and conflicts in various financial agent pairings. These concepts are then used in a theoretical and empirical study of important financial decisions, such as capital structure, dividend policy, retained ownership, security underwriting, management of distressed firms, managerial compensation, corporate governance and mergers.", semester, year, 1, 0, 0);
                }
            }
        }

        private void CPSC231_MouseDown(object sender, RoutedEventArgs e)
        {

            int contain_flag = 0;
            foreach (Course check_course in CourseSelectorCourses.instance.visable)
            {
                if (check_course.Department.Equals("CPSC") && check_course.Number.Equals("231"))
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
                    Add_Course_Click("CPSC", "231", "CSPC 231 - Introduction to Computer Science for Computer Science Majors I", "Introduction to problem solving, the analysis and design of small-scale computational systems, and implementation using a procedural programming language. For computer science majors.", semester, year, 2, 2, 0);
                }
            }
        }

        private void CPSC313_MouseDown(object sender, RoutedEventArgs e)
        {

            int contain_flag = 0;
            foreach (Course check_course in CourseSelectorCourses.instance.visable)
            {
                if (check_course.Department.Equals("CPSC") && check_course.Number.Equals("313"))
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
                    Add_Course_Click("CPSC", "313", "CPSC 313 - Introduction to Computability", "An introduction to abstract models of sequential computation, including finite automata, regular expressions, context-free grammars, and Turing machines. Formal languages, including regular, context-free, and recursive languages, methods for classifying languages according to these types, and relationships among these classes.", semester, year, 2, 3, 0);
                }
            }
        }

        private void CPSC413_MouseDown(object sender, RoutedEventArgs e)
        {

            int contain_flag = 0;
            foreach (Course check_course in CourseSelectorCourses.instance.visable)
            {
                if (check_course.Department.Equals("CPSC") && check_course.Number.Equals("413"))
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
                    Add_Course_Click("CPSC", "413", "CPSC 413 - Design and Analysis of Algorithms I", "Techniques for the analysis of algorithms, including counting, summation, recurrences, and asymptotic relations; techniques for the design of efficient algorithms, including greedy methods, divide and conquer, and dynamic programming; examples of their application; an introduction to tractable and intractable problems.", semester, year, 2, 3, 0);
                }
            }
        }

        private void PHIL279_MouseDown(object sender, RoutedEventArgs e)
        {

            int contain_flag = 0;
            foreach (Course check_course in CourseSelectorCourses.instance.visable)
            {
                if (check_course.Department.Equals("PHIL") && check_course.Number.Equals("279"))
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
                    Add_Course_Click("PHIL", "279", "PHIL 279 - Logic I", "Sentential and first-order logic from both deductive and semantic points of view. Some elementary metatheorems.", semester, year, 2, 2, 0);
                }
            }
        }

        private void CPSC481_MouseDown(object sender, RoutedEventArgs e)
        {

            int contain_flag = 0;
            foreach (Course check_course in CourseSelectorCourses.instance.visable)
            {
                if (check_course.Department.Equals("CPSC") && check_course.Number.Equals("481"))
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
                    Add_Course_Click("CPSC", "481", "CPSC 481 - Human-Computer Interaction I", "Fundamental theory and practice of the design, implementation, and evaluation of human-computer interfaces. Topics include: principles of design; methods for evaluating interfaces with or without user involvement; techniques for prototyping and implementing graphical user interfaces.", semester, year, 1, 4, 0);
                }
            }
        }

        private void POLI321_MouseDown(object sender, RoutedEventArgs e)
        {

            int contain_flag = 0;
            foreach (Course check_course in CourseSelectorCourses.instance.visable)
            {
                if (check_course.Department.Equals("POLI") && check_course.Number.Equals("321"))
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
                    Add_Course_Click("POLI", "321", "POLI 321 - Politics and Government in Canada", "An examination of institutions and political processes in Canada. Significant attention is paid to key institutions such as Parliament, the executive, federalism, the Constitution, and the courts. Emphasis is also placed on the way that political processes are shaped by these and other institutions. This course may have a special instructional format. Please consult the Department for details.", semester, year, 1, 3, 0);
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
                    lab.Time = "F 1:11";
                    offering.Labs.Add(lab);
                }
                newCourse.AddOffering(offering);
            }
            CourseSelectorCourses.AddCourse(newCourse);
        }

        //List containing the selections from the minor section combobox.
        List<string> selection_cmb = new List<string>();
        //List containing the selections from the degree/major section combobox.
        List<string> selection_degree_cmb = new List<string>();
        //List containing the selections from the concentration section combobox.
        List<string> selection_conc_cmb = new List<string>();
        //List containing the selection from the concentration for the current degree combobox.
        List<string> selection_cmb_Comm_conc = new List<string>();

        private void degreeNav_ExpandAll_MouseDown(object sender, RoutedEventArgs e)
        {
            req1_expander.IsExpanded = true;
            req1_higher_expander.IsExpanded = true;
            req1_lower_expander.IsExpanded = true;
            req2_expander.IsExpanded = true;
            req3_expander.IsExpanded = true;
            if (selection_cmb_Comm_conc.Contains("FNCE")) FNCE_Conc.IsExpanded = true;
            if (selection_cmb.Contains("ECON_Minor")) Econ_Minor_Expander.IsExpanded = true;
            if (selection_cmb.Contains("PHIL_Minor")) Phil_Minor_Expander.IsExpanded = true;

            if (selection_degree_cmb.Contains("CPSC"))
            {
                req1CPSC_expander.IsExpanded = true;
                req1CPSC_higher_expander.IsExpanded = true;
                req1CPSC_lower_expander.IsExpanded = true;
                req2CPSC_expander.IsExpanded = true;
                req3CPSC_expander.IsExpanded = true;
            }
            if (selection_conc_cmb.Contains("SENG")) SoftEng_Conc.IsExpanded = true;

            if (selection_degree_cmb.Contains("POLI"))
            {
                req1Poli_expander.IsExpanded = true;
                req1Poli_lower_expander.IsExpanded = true;
                req1Poli_higher_expander.IsExpanded = true;
                req2Poli_expander.IsExpanded = true;
            }
        }

        private void degreeNav_CollapseAll_MouseDown(object sender, RoutedEventArgs e)
        {
            req1_expander.IsExpanded = false;
            req1_higher_expander.IsExpanded = false;
            req1_lower_expander.IsExpanded = false;
            req2_expander.IsExpanded = false;
            req3_expander.IsExpanded = false;
            if (selection_cmb_Comm_conc.Contains("FNCE")) FNCE_Conc.IsExpanded = false;
            if (selection_cmb.Contains("ECON_Minor")) Econ_Minor_Expander.IsExpanded = false;
            if (selection_cmb.Contains("PHIL_Minor")) Phil_Minor_Expander.IsExpanded = false;

            if (selection_degree_cmb.Contains("CPSC"))
            {
                req1CPSC_expander.IsExpanded = false;
                req1CPSC_higher_expander.IsExpanded = false;
                req1CPSC_lower_expander.IsExpanded = false;
                req2CPSC_expander.IsExpanded = false;
                req3CPSC_expander.IsExpanded = false;
            }
            if (selection_conc_cmb.Contains("SENG")) SoftEng_Conc.IsExpanded = false;

            if (selection_degree_cmb.Contains("POLI"))
            {
                req1Poli_expander.IsExpanded = false;
                req1Poli_lower_expander.IsExpanded = false;
                req1Poli_higher_expander.IsExpanded = false;
                req2Poli_expander.IsExpanded = false;
            }
        }

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
                
            }

            if(!selection_degree_cmb.Contains("CPSC"))
            {
                Degree_CPSC.Visibility = Visibility.Collapsed;
                SoftEng_Conc.Visibility = Visibility.Collapsed;
            }

            if (!selection_degree_cmb.Contains("POLI"))
            {
                Degree_Poli.Visibility = Visibility.Collapsed;
            }

            if (selection_degree_cmb.Contains("CPSC"))
            {
                Degree_CPSC.Visibility = Visibility.Visible;
                SoftEng_Conc.Visibility = Visibility.Collapsed;
                Degree_Poli.Visibility = Visibility.Collapsed;
            }

            if (selection_degree_cmb.Contains("POLI"))
            {
                Degree_CPSC.Visibility = Visibility.Collapsed;
                SoftEng_Conc.Visibility = Visibility.Collapsed;
                Degree_Poli.Visibility = Visibility.Visible;
            }

            if (selection_conc_cmb.Contains("SENG"))
            {
                CPSCdegree_Conc.Visibility = Visibility.Visible;
                CPSCdegree_Regular.Visibility = Visibility.Collapsed;
                SoftEng_Conc.Visibility = Visibility.Visible;
            }
            else
            {
                CPSCdegree_Conc.Visibility = Visibility.Collapsed;
                CPSCdegree_Regular.Visibility = Visibility.Visible;
            }
        }

        private void addButton_cmb_Comm_conc_Click(object sender, RoutedEventArgs e)
        {
            if (Requirement_Popup.Visibility == Visibility.Visible)
            {
                Requirement_Popup.Visibility = Visibility.Hidden;
            }

            if (selection_cmb_Comm_conc.Contains("FNCE"))
            {
                Regular_Degree_Title.Visibility = Visibility.Collapsed;
                Conc_Degree_Title.Visibility = Visibility.Visible;
                FNCE_Conc.Visibility = Visibility.Visible;
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

        private void cmb2_DropDownClosed(object sender, EventArgs e)
        {
            cmb_Filter2();
        }

        private void cmb_Comm_conc_DropDownClosed(object sender, EventArgs e)
        {
            String cmbItem;
            if (cmbComm_Conc.SelectedItem != null)
            {
                cmbItem = cmbComm_Conc.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last();
                if(!selection_cmb_Comm_conc.Contains("FNCE - Finance")) {
                    selection_cmb_Comm_conc.Add("FNCE");
                }
            }    
        }

        private void cmb_Minor_DropDownClosed(object sender, EventArgs e)
        {
            String cmbItem;
            if (cmbMinor.SelectedItem != null)
            {
                cmbItem = cmbMinor.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last();

                if (cmbItem.Equals("ECON - Economics"))
                {
                    if (!selection_cmb.Contains("ECON - Economics"))
                    {
                        selection_cmb.Clear();
                        selection_cmb.Add("ECON_Minor");
                    }
                }

                else if (cmbItem.Equals("PHIL - Philosophy"))
                {
                    if (!selection_cmb.Contains("PHIL - Philosophy"))
                    {
                        selection_cmb.Clear();
                        selection_cmb.Add("PHIL_Minor");
                    }
                }
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
                    cmbConcentration.SelectedIndex = -1;
                    selection_degree_cmb.Clear();
                    selection_conc_cmb.Clear();
                    cmbMajor.Items.Remove("POLI - Political Science");
                    cmbMajor.Items.Remove("CPSC - Computer Science");
                    cmbMajor.Items.Add("POLI - Political Science");
                }
                else if(cmbItem.Equals("Bachelor of Science"))
                {
                    selection_degree_cmb.Clear();
                    selection_conc_cmb.Clear();
                    cmbMajor.Items.Remove("POLI - Political Science");
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
                if(cmbItem.Equals("POLI - Political Science"))
                {                   
                    cmbConcentration.Items.Remove("Software Engineering");
                    if (!selection_degree_cmb.Contains("POLI"))
                    {
                        selection_degree_cmb.Add("POLI");
                    }
                }

                else if(cmbItem.Equals("CPSC - Computer Science"))
                {                    
                    cmbConcentration.Items.Remove("Software Engineering");
                    cmbConcentration.Items.Add("Software Engineering");
                    if (!selection_degree_cmb.Contains("CPSC"))
                    {
                        selection_degree_cmb.Add("CPSC");
                    }
                }
            }
        }

        //Selection of concentration
        private void cmb_Filter2()
        {
            String cmbItem;
            if (cmbConcentration.SelectedItem != null)
            {
                cmbItem = cmbConcentration.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last();
                if (cmbItem.Equals("Software Engineering"))
                {
                    if (!selection_conc_cmb.Contains("SENG"))
                    {
                        selection_conc_cmb.Add("SENG");
                    }
                }
            }
        }

        private void Remove_CPSC_Degree_Click(object sender, RoutedEventArgs e)
        {
            Degree_CPSC.Visibility = Visibility.Collapsed;
            SoftEng_Conc.Visibility = Visibility.Collapsed;

            selection_degree_cmb.Clear();
            selection_conc_cmb.Clear();

            cmbDegree.SelectedIndex = -1;
            cmbMajor.SelectedIndex = -1;
            cmbConcentration.SelectedIndex = -1;

            req1CPSC_expander.IsExpanded = false;
            req1CPSC_higher_expander.IsExpanded = false;
            req1CPSC_lower_expander.IsExpanded = false;
            req2CPSC_expander.IsExpanded = false;
            req3CPSC_expander.IsExpanded = false;
            if(selection_conc_cmb.Contains("SENG")) SoftEng_Conc.IsExpanded = false; 
        }

        private void Remove_POLI_Degree_Click(object sender, RoutedEventArgs e)
        {
            Degree_Poli.Visibility = Visibility.Collapsed;

            selection_degree_cmb.Clear();
            selection_conc_cmb.Clear();

            cmbDegree.SelectedIndex = -1;
            cmbMajor.SelectedIndex = -1;

            req1Poli_expander.IsExpanded = false;
            req1Poli_lower_expander.IsExpanded = false;
            req1Poli_higher_expander.IsExpanded = false;
            req2Poli_expander.IsExpanded = false;
        }

        private void Remove_SENG_Conc_Click(object sender, RoutedEventArgs e)
        {
            SoftEng_Conc.Visibility = Visibility.Collapsed;
            selection_conc_cmb.Clear();
            cmbConcentration.SelectedIndex = -1;
            SoftEng_Conc.IsExpanded = false;
            CPSCdegree_Conc.Visibility = Visibility.Collapsed;
            CPSCdegree_Regular.Visibility = Visibility.Visible;
        }
        
        private void Remove_FNCE_Conc_Click(object sender, RoutedEventArgs e)
        {
            FNCE_Conc.Visibility = Visibility.Collapsed;
            selection_cmb_Comm_conc.Clear();
            cmbComm_Conc.SelectedIndex = -1;
            Regular_Degree_Title.Visibility = Visibility.Visible;
            Conc_Degree_Title.Visibility = Visibility.Collapsed;
            FNCE_Conc.IsExpanded = false;
        }

        private void Remove_ECON_Minor_Click(object sender, RoutedEventArgs e)
        {
            Econ_Minor.Visibility = Visibility.Collapsed;
            selection_cmb.Clear();
            cmbMinor.SelectedIndex = -1;
            Econ_Minor_Expander.IsExpanded = false;
        }

        private void Remove_PHIL_Minor_Click(object sender, RoutedEventArgs e)
        {
            Phil_Minor.Visibility = Visibility.Collapsed;
            selection_cmb.Clear();
            cmbMinor.SelectedIndex = -1;
            Phil_Minor_Expander.IsExpanded = false;
        }

        private void Clear_All_Click(object sender, RoutedEventArgs e)
        {
            //Clearing the selections in the comboboxs in the add an additional degree pop-up.
            cmbDegree.SelectedIndex = -1;
            cmbMajor.SelectedIndex = -1;
            cmbConcentration.SelectedIndex = -1;
            cmbMinor.SelectedIndex = -1;

            cmbMajor.Items.Remove("POLI - Political Science");
            cmbMajor.Items.Remove("CPSC - Computer Science");
            cmbConcentration.Items.Remove("Software Engineering");

            Degree_CPSC.Visibility = Visibility.Collapsed;
            SoftEng_Conc.Visibility = Visibility.Collapsed;
            Phil_Minor.Visibility = Visibility.Collapsed;
            Econ_Minor.Visibility = Visibility.Collapsed;
            Degree_Poli.Visibility = Visibility.Collapsed;

            selection_cmb.Clear();
            selection_degree_cmb.Clear();
            selection_conc_cmb.Clear();

            Econ_Minor_Expander.IsExpanded = false;
            Phil_Minor_Expander.IsExpanded = false;
            SoftEng_Conc.IsExpanded = false;
            FNCE_Conc.IsExpanded = false;
            req1CPSC_expander.IsExpanded = false;
            req1CPSC_higher_expander.IsExpanded = false;
            req1CPSC_lower_expander.IsExpanded = false;
            req2CPSC_expander.IsExpanded = false;
            req3CPSC_expander.IsExpanded = false;
            req1Poli_expander.IsExpanded = false;
            req1Poli_lower_expander.IsExpanded = false;
            req1Poli_higher_expander.IsExpanded = false;
            req2Poli_expander.IsExpanded = false;
        }

        private void RemoveAll_MouseDown (object sender, RoutedEventArgs e)
        {
            //Clearing the selections in the comboboxs in the add an additional degree pop-up.
            cmbDegree.SelectedIndex = -1;
            cmbMajor.SelectedIndex = -1;
            cmbConcentration.SelectedIndex = -1;
            cmbMinor.SelectedIndex = -1;

            cmbMajor.Items.Remove("POLI - Political Science");
            cmbMajor.Items.Remove("CPSC - Computer Science");
            cmbConcentration.Items.Remove("Software Engineering");

            Degree_CPSC.Visibility = Visibility.Collapsed;
            SoftEng_Conc.Visibility = Visibility.Collapsed;
            Phil_Minor.Visibility = Visibility.Collapsed;
            Econ_Minor.Visibility = Visibility.Collapsed;
            Degree_Poli.Visibility = Visibility.Collapsed;

            selection_cmb.Clear();
            selection_degree_cmb.Clear();
            selection_conc_cmb.Clear();

            Econ_Minor_Expander.IsExpanded = false;
            Phil_Minor_Expander.IsExpanded = false;
            SoftEng_Conc.IsExpanded = false;
            FNCE_Conc.IsExpanded = false;
            req1CPSC_expander.IsExpanded = false;
            req1CPSC_higher_expander.IsExpanded = false;
            req1CPSC_lower_expander.IsExpanded = false;
            req2CPSC_expander.IsExpanded = false;
            req3CPSC_expander.IsExpanded = false;
            req1Poli_expander.IsExpanded = false;
            req1Poli_lower_expander.IsExpanded = false;
            req1Poli_higher_expander.IsExpanded = false;
            req2Poli_expander.IsExpanded = false;

            FNCE_Conc.Visibility = Visibility.Collapsed;
            selection_cmb_Comm_conc.Clear();
            cmbComm_Conc.SelectedIndex = -1;
            Regular_Degree_Title.Visibility = Visibility.Visible;
            Conc_Degree_Title.Visibility = Visibility.Collapsed;
        }

        private void Advanced_Search_Click(object sender, RoutedEventArgs e)
        {
            CourseSelectorCourses.ClearAllCourses();
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
            if (Faculty_Selection == "System.Windows.Controls.ComboBoxItem: BSEN - Business")
            {
                //Then Level
                if (Level_Selection == "System.Windows.Controls.ComboBoxItem: 300")
                {
                    BSEN395();
                }
                else if (Level_Selection == "System.Windows.Controls.ComboBoxItem: 400")
                {
                    BSEN401();
                }
                else if (Level_Selection == "System.Windows.Controls.ComboBoxItem: 500")
                {
                    BSEN533();
                    BSEN569();
                }
                else
                {
                    BSEN395();
                    BSEN401();
                    BSEN533();
                    BSEN569();
                }
            }
            Console.WriteLine(Faculty_Selection);
            Console.WriteLine("KEYWORD: " + Text_Selection);

            Requirement_Popup.Visibility = Visibility.Hidden;
            Course_Search_Panel.Visibility = Visibility.Hidden;
            Degree_Search_Panel.Visibility = Visibility.Hidden;
        }

        private void ACCT217()
        {

            foreach (Semester semester in Semester.FALL_WINTER)
            {
                int year = 0;
                if (semester == Semester.FALL) year = 2017;
                else year = 2018;
                Add_Course_Click("ACCT", "217", "Introduction to Financial Accounting",
                    "Introduction to accounting for business organizations", semester, year, 2, 2, 2);
            }
        }

        private void ACCT301()
        {

            foreach (Semester semester in Semester.FALL_WINTER)
            {
                int year = 0;
                if (semester == Semester.FALL) year = 2017;
                else year = 2018;
                Add_Course_Click("ACCT", "301", "Accounting Principles",
                    "Introduction to basic accounting principles and practices", semester, year, 2, 2, 2);
            }
        }

        private void ACCT341()
        {

            foreach (Semester semester in Semester.FALL_WINTER)
            {
                int year = 0;
                if (semester == Semester.FALL) year = 2017;
                else year = 2018;
                Add_Course_Click("ACCT", "341", "Intermediate Financial Accounting I",
                    "Financial accounting from a producer point of view", semester, year, 2, 2, 2);
            }
        }

        private void BSEN395()
        {

            foreach (Semester semester in Semester.FALL_WINTER)
            {
                int year = 0;
                if (semester == Semester.FALL) year = 2017;
                else year = 2018;
                Add_Course_Click("BSEN", "395", "Business Law for Strategic Decision Makers",
                    "Strategic management of international business, securities law and other current business law issues", semester, year, 2, 2, 2);
            }
        }

        private void BSEN401()
        {
            
            foreach (Semester semester in Semester.ALL_SEMESTERS.Take(3))
            {
                int year = 0;
                if (semester == Semester.FALL) year = 2017;
                else year = 2018;
                Add_Course_Click("BSEN", "401", "Business in Canada", 
                    "A comparative analysis of Canada's competitive position in the global economy.", semester, year, 2, 2, 2);
            }
        }

        private void BSEN533()
        {

            foreach (Semester semester in Semester.ALL_SEMESTERS.Take(3))
            {
                int year = 0;
                if (semester == Semester.FALL) year = 2017;
                else year = 2018;
                Add_Course_Click("BSEN", "533", "Dilemmas and Decisions in Business",
                    "Balance ethical, social and legal decision-making principles, process complex facts and obtain well-reasoned results in order to make decisions in turbulent times", semester, year, 2, 2, 2);
            }
        }

        private void BSEN569()
        {

            foreach (Semester semester in Semester.ALL_SEMESTERS.Take(3))
            {
                int year = 0;
                if (semester == Semester.FALL) year = 2017;
                else year = 2018;
                Add_Course_Click("BSEN", "569", "Ethical Issues and the Professional Manager",
                    "provide tools for making sound ethical decisions in various business situations and in the face of moral dilemmas", semester, year, 2, 2, 2);
            }
        }

        private void ECON201()
        {

            foreach (Semester semester in Semester.ALL_SEMESTERS.Take(3))
            {
                int year = 0;
                if (semester == Semester.FALL) year = 2017;
                else year = 2018;
                Add_Course_Click("ECON", "201", "Introduction to Microeconomics",
                    "Principles of consumption, production, exchange: market and firm equilibrium under different competitive conditions", semester, year, 2, 2, 2);
            }
        }

        private void ECON203()
        {

            foreach (Semester semester in Semester.ALL_SEMESTERS.Take(3))
            {
                int year = 0;
                if (semester == Semester.FALL) year = 2017;
                else year = 2018;
                Add_Course_Click("ECON", "203", "Introduction to Macroeconomics",
                    "National income determination, the monetary and banking system, and elementary fiscal and monetary policies", semester, year, 2, 2, 2);
            }
        }

        private void ECON355()
        {

            foreach (Semester semester in Semester.ALL_SEMESTERS.Take(3))
            {
                int year = 0;
                if (semester == Semester.FALL) year = 2017;
                else year = 2018;
                Add_Course_Click("ECON", "355", "Canadian Public Finance",
                    "Examination of the institutions behind and economic rationale for Canadian government policy relating to public expenditures and taxation", semester, year, 2, 2, 2);
            }
        }

        private void Expand_All_Course_Selector(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Course_Selector_Items.Items.Count; i++)
            {
                ContentPresenter child = (ContentPresenter)Course_Selector_Items.ItemContainerGenerator.ContainerFromIndex(i);
                ChangeExpander(child, true);
            }
        }

        private void Collapse_All_Course_Selector(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Course_Selector_Items.Items.Count; i++)
            {
                ContentPresenter child = (ContentPresenter)Course_Selector_Items.ItemContainerGenerator.ContainerFromIndex(i);
                ChangeExpander(child, false);
            }
        }

        private void ChangeExpander(DependencyObject item, bool isExpanded)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(item); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(item, i);

                if (child is Expander)
                    ((Expander)child).IsExpanded = isExpanded;
                else
                    ChangeExpander(child, isExpanded);


            }
        }

        private void Clear_All_Course_Selector(object sender, RoutedEventArgs e)
        {
            CourseSelectorCourses.ClearAllCourses();
        }

    }
}

