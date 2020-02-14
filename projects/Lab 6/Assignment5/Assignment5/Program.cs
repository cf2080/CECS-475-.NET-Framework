using BusinessLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6
{
    /// <summary>
    /// Class that allows users to create, update, delete, and see records of teachers/courses thorugh the console window
    /// </summary>
    public class Program
    {
        /// <summary>
        /// private businessLayer object that allows to call the functions needed in order to perform CRUD operations for 
        /// option 1-9
        /// </summary>
        private static IBusinessLayer businessLayer = new BusinessLayer.BusinessLayer();

        /// <summary>
        /// Main function that starts the program so it can run
        /// </summary>
        /// <param name="args">string variable</param>
        public static void Main(string[] args)
        {
            StartProgram();
        }

        /// <summary>
        /// Function that displays the main flow of how the program runs
        /// </summary>
        public static void StartProgram()
        {
            int input;

            IEnumerable<Teacher> teacherTable = businessLayer.GetAllTeachers();
            IEnumerable<Course> courseTable = businessLayer.GetAllCourses();

            do
            {
                Menu();
                input = Convert.ToInt32(Console.ReadLine());
                if (input == 1)
                {
                    AddTeacher(businessLayer);
                }
                else if (input == 2)
                {
                    UpdateTeacher(teacherTable, businessLayer);
                }
                else if (input == 3)
                {
                    DeleteTeacher(teacherTable, businessLayer);
                }
                else if (input == 4)
                {
                    DisplayCoursesTeacher(teacherTable, courseTable, businessLayer);
                }
                else if (input == 5)
                {
                    DisplayTeachers();

                }
                else if (input == 6)
                {
                    AddCourse(courseTable, businessLayer);
                }
                else if (input == 7)
                {
                    UpdateCourse(courseTable, businessLayer);
                }
                else if (input == 8)
                {
                    DeleteCourse(courseTable, businessLayer);
                }
                else if (input == 9)
                {
                    DisplayCourses(courseTable, businessLayer);
                }
            } while (input != 0);
        }

        #region OptionsTeachers
        /// <summary>
        /// Function that add a teacher to the database table if the user chooses option 1 in the menu
        /// </summary>
        /// <param name="businessLayer">business layer object used to call a function from another class in order to add teacher to the table</param>
        public static void AddTeacher(IBusinessLayer businessLayer)
        {
            Console.WriteLine("Enter new Teacher Name");
            string teacherName = Console.ReadLine();

            Teacher teacher = new Teacher { TeacherName = teacherName };
            Standard standard = new Standard();
            standard.Teachers.Add(teacher);
            businessLayer.AddTeacher(teacher);
        }

        /// <summary>
        /// Function that updates a teacher record that the user wants to change if they choose option 2 from the menu
        /// </summary>
        /// <param name="teacherTable">list that contains teachers that are stored in the table</param>
        /// <param name="businessLayer">business layer object used to call a function im order to update a teacher record</param>
        public static void UpdateTeacher(IEnumerable<Teacher> teacherTable, IBusinessLayer businessLayer)
        {
            DisplayTeachers();
            Console.WriteLine("Search Teacher by ID or Name?");
            Console.WriteLine("[1] ID");
            Console.WriteLine("[2] Name");

            int choice = Convert.ToInt32(Console.ReadLine());
            if (choice == 1)
            {
                Console.WriteLine("Enter Teacher ID: ");
                int tchrID = Convert.ToInt32(Console.ReadLine());
                Teacher tchr = businessLayer.GetTeacherByID(tchrID);
                Teacher newTeacher = tchr;
                Console.WriteLine("Enter the new teacher name: ");
                newTeacher.TeacherName = Console.ReadLine();
                businessLayer.UpdateTeacher(newTeacher);
            }
            else if (choice == 2)
            {
                Console.WriteLine("Enter Teacher Name: ");
                string tchrName = Console.ReadLine();
                Teacher tchr = businessLayer.GetTeacherByName(tchrName);
                Console.WriteLine("Enter the new Teacher name: ");
                tchr.TeacherName = Console.ReadLine();
                businessLayer.UpdateTeacher(tchr);
            }
            else
            {
                Console.WriteLine("Option not there");
            }

        }

        /// <summary>
        /// Function that allows user to delete a record from the teacher table
        /// </summary>
        /// <param name="teacherTable">list that contains all the teachers from the database table</param>
        /// <param name="businessLayer">business layer object used to call a function to delete teacher</param>
        public static void DeleteTeacher(IEnumerable<Teacher> teacherTable, IBusinessLayer businessLayer)
        {
            DisplayTeachers();
            Console.WriteLine("Enter Teacher ID you want to Delete: ");
            int tchrID = Convert.ToInt32(Console.ReadLine());
            Teacher tchr = businessLayer.GetTeacherByID(tchrID);
            businessLayer.RemoveTeacher(tchr);
        }

        /// <summary>
        /// Function that displays the teacher a user picked along with the courses that specific teacher teaches
        /// </summary>
        /// <param name="teacherTable">kist containing teachers stored in database table</param>
        /// <param name="courseTable">list containing courses stored in databse table</param>
        /// <param name="businessLayer">object used to call function to aid in displaying record info from database tables</param>
        public static void DisplayCoursesTeacher(IEnumerable<Teacher> teacherTable, IEnumerable<Course> courseTable, IBusinessLayer businessLayer)
        {
            DisplayTeachers();
            Console.WriteLine("Enter teacher ID to view all the Teachers Courses: ");
            int teacherID = Convert.ToInt32(Console.ReadLine());
            courseTable = businessLayer.GetAllCourses();
            Console.WriteLine("Teacher ID\tCourse ID\tNames");
            foreach (Course c in courseTable)
            {
                if (c.TeacherId == teacherID)
                {
                    Console.WriteLine(c.TeacherId + "\t\t" + c.CourseId + "\t\t" + c.CourseName);
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Function that displays all the teachers along with their names
        /// </summary>
        public static void DisplayTeachers()
        {
            IEnumerable<Teacher> teacherTable = businessLayer.GetAllTeachers();
            Console.WriteLine("Displaying Teachers");
            Console.WriteLine("ID\tNames");
            foreach (Teacher tchr in teacherTable)
            {
                Console.WriteLine(tchr.TeacherId + "\t" + tchr.TeacherName + " ");
            }
            Console.WriteLine();

        }
        #endregion

        #region optionCourses
        /// <summary>
        /// function that displays all the courses
        /// </summary>
        /// <param name="courseTable">list containing all the courses stored in database table</param>
        /// <param name="businessLayer">object used to aid in displaying courses</param>
        public static void DisplayCourses(IEnumerable<Course> courseTable, IBusinessLayer businessLayer)
        {
            Console.WriteLine("DisplayCourses");
            Console.WriteLine("\tCourse ID\tNames");
            courseTable = businessLayer.GetAllCourses();
            foreach (Course c in courseTable)
            {
                Console.WriteLine("\t\t" + c.CourseId + "\t\t" + c.CourseName);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Function used add a course to the database tab;es
        /// </summary>
        /// <param name="courseTable">list enumerable that contains all the courses from the courses table</param>
        /// <param name="businessLayer">object used to aid in adding course to the course table</param>
        public static void AddCourse(IEnumerable<Course> courseTable, IBusinessLayer businessLayer)
        {
            DisplayCourses(courseTable, businessLayer);
            Console.WriteLine("Enter new Course Name");
            string crseName = Console.ReadLine();

            Console.WriteLine("Enter Teacher ID:");
            int tchrID = Convert.ToInt32(Console.ReadLine());

            Course course = new Course { CourseName = crseName, TeacherId = tchrID };

            businessLayer.AddCourse(course);
        }

        /// <summary>
        /// Function used to add in updating a specific course from course table in dtabase
        /// </summary>
        /// <param name="courseTable">enumerable lisr that contains all the courses from database </param>
        /// <param name="businessLayer">object used to aid to update a course by calling a specific function</param>
        public static void UpdateCourse(IEnumerable<Course> courseTable, IBusinessLayer businessLayer)
        {
            DisplayCourses(courseTable, businessLayer);
            Console.WriteLine("Search Course by ID or Name?");
            Console.WriteLine("1. ID");
            Console.WriteLine("2. Name");

            int choice = Convert.ToInt32(Console.ReadLine());
            if (choice == 1)
            {
                Console.WriteLine("Enter Course ID: ");
                int courseID = Convert.ToInt16(Console.ReadLine());
                Course course = businessLayer.GetCourseByID(courseID);
                Console.WriteLine("Enter the new Course name: ");
                course.CourseName = Console.ReadLine();
                Console.WriteLine("Enter new Teacher ID: ");
                course.TeacherId = Convert.ToInt32(Console.ReadLine());
                businessLayer.UpdateCourse(course);
            }
            else if (choice == 2)
            {
                Console.WriteLine("Enter Course Name: ");
                string courseName = Console.ReadLine();
                Course course = businessLayer.GetCourseByName(courseName);
                Console.WriteLine("Enter the new Course name: ");
                Course modCourse = businessLayer.GetCourseByID(course.CourseId);
                modCourse.CourseName = Console.ReadLine();
                businessLayer.UpdateCourse(modCourse);
            }
            else
            {
                Console.WriteLine("Wrong Choice");
            }
        }

        /// <summary>
        /// Function used to delete a course record from a table
        /// </summary>
        /// <param name="courseTable">ienumerable list that contains all the courses that are stored in the database</param>
        /// <param name="businessLayer">object used to help in deleteing a course by calling functions that may prove helpful</param>
        public static void DeleteCourse(IEnumerable<Course> courseTable, IBusinessLayer businessLayer)
        {
            DisplayCourses(courseTable, businessLayer);
            Console.WriteLine("Enter Course ID you want to delete: ");
            int courseID = Convert.ToInt32(Console.ReadLine());
            Course course = businessLayer.GetCourseByID(courseID);
            Console.WriteLine(course.CourseName);

            businessLayer.RemoveCourse(course);
        }
        #endregion

        /// <summary>
        /// Function that displays the menu that the user sees so they can input their options
        /// </summary>
        public static void Menu()
        {
            Console.WriteLine("--------------------MENU-----------------------");
            Console.WriteLine("[1] Create a new Teacher");
            Console.WriteLine("[2] Update an existing Teacher");
            Console.WriteLine("[3] Delete a Teacher");
            Console.WriteLine("[4] Display courses related to a teacher");
            Console.WriteLine("[5] Display all Teachers");
            Console.WriteLine("[6] Create a new Course");
            Console.WriteLine("[7] Update a existing Course");
            Console.WriteLine("[8] Delete a Course");
            Console.WriteLine("[9] Display all Courses");
            Console.WriteLine("[0] Exit");
        }
    };
}
