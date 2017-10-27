using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSC481_Prototype
{
    class CourseSelectorCourses
    {
        public static CourseSelectorCourses instance = new CourseSelectorCourses();

        public List<Course> visable;


        private CourseSelectorCourses()
        {
            

        }

    }

    /// <summary>
    /// A course is offered for a given semester, and can be made up of multiple offerings.
    /// </summary>
    public class Course
    {
        // Department offering the course (e.g. CPSC)
        public string Department { get; }

        // Course Name (e.g. "Calculus I")
        public string Title { get; }

        // Course Description
        public string Description { get; }

        // A list of lecture sections
        public List<Section> Lectures;

        // A list of tutorial sections
        public List<Section> Tutorials;

        // A list of lab sections
        public List<Section> Labs;

        // A course can have 1 or more offerings.
        private List<Offering> offerings;

        public Course(string Title, string Description, string Semester)
        {

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
