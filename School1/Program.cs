using System;
using System.Collections.Generic;

namespace School_Management_System
{
    abstract class Person
    {
        public abstract string PrintDetails();
    }

    class Student : Person
    {
        public Student(int studentId, string name, int age, List<Course> courses)
        {
            StudentId = studentId;
            Name = name;
            Age = age;
            Courses = courses;
        }

        public int StudentId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Course> Courses { get; set; }

        public bool Enroll(Course course)
        {
            if (Courses.Contains(course))
                return false;
            Courses.Add(course);
            return true;
        }

        public override string PrintDetails()
        {
            List<string> titles = new List<string>();
            foreach (Course c in Courses)
            {
                titles.Add(c.Title);
            }
            string? courseNames = string.Join(", ", titles.ToArray());
            return StudentId + " " + Name + " " + Age + " Courses: " + courseNames;
        }

        public override string ToString()
        {
            return PrintDetails();
        }
    }

    class Instructor : Person
    {
        public Instructor(int instructorId, string name, string specialization)
        {
            InstructorId = instructorId;
            Name = name;
            Specialization = specialization;
        }

        public int InstructorId { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }

        public override string PrintDetails()
        {
            return InstructorId + " " + Name + " " + Specialization;
        }

        public override string ToString()
        {
            return PrintDetails();
        }
    }

    class Course : Person
    {
        public Course(int courseId, string title, Instructor instructor)
        {
            CourseId = courseId;
            Title = title;
            Instructor = instructor;
        }

        public int CourseId { get; set; }
        public string Title { get; set; }
        public Instructor Instructor { get; set; }

        public override string PrintDetails()
        {
            return CourseId + " " + Title + " Instructor: " + Instructor.Name;
        }

        public override string ToString()
        {
            return PrintDetails();
        }
    }

    class SchoolStudentManager
    {
        public List<Student> Students = new List<Student>();
        public List<Course> Courses = new List<Course>();
        public List<Instructor> Instructors = new List<Instructor>();

        public bool AddStudent(Student student)
        {
            foreach (Student s in Students)
            {
                if (s.StudentId == student.StudentId)
                    return false;
            }
            Students.Add(student);
            return true;
        }

        public bool AddCourse(Course course)
        {
            foreach (Course c in Courses)
            {
                if (c.CourseId == course.CourseId)
                    return false;
            }
            Courses.Add(course);
            return true;
        }

        public bool AddInstructor(Instructor instructor)
        {
            foreach (Instructor i in Instructors)
            {
                if (i.InstructorId == instructor.InstructorId)
                    return false;
            }
            Instructors.Add(instructor);
            return true;
        }

        public Student? FindStudent(int studentId)
        {
            foreach (Student s in Students)
            {
                if (s.StudentId == studentId)
                    return s;
            }
            return null;
        }

        public Course? FindCourse(int courseId)
        {
            foreach (Course c in Courses)
            {
                if (c.CourseId == courseId)
                    return c;
            }
            return null;
        }

        public Instructor? FindInstructor(int instructorId)
        {
            foreach (Instructor i in Instructors)
            {
                if (i.InstructorId == instructorId)
                    return i;
            }
            return null;
        }

        public bool DeleteStudent(int studentId)
        {
            Student? s = FindStudent(studentId);
            if (s != null)
                return Students.Remove(s);
            return false;
        }

        public bool UpdateStudent(int studentId, int newId, string newName, int newAge, List<Course> newCourses)
        {
            Student? st = FindStudent(studentId);
            if (st == null) return false;

            if (newId != studentId && FindStudent(newId) != null)
                return false;

            st.StudentId = newId;
            st.Name = newName;
            st.Age = newAge;
            st.Courses = newCourses;
            return true;
        }

        public bool EnrollStudentInCourse(int studentId, int courseId)
        {
            Student? st = FindStudent(studentId);
            Course? cr = FindCourse(courseId);
            if (st != null && cr != null)
                return st.Enroll(cr);
            return false;
        }

        public List<Student> GetAllStudents() { return Students; }
        public List<Instructor> GetAllInstructors() { return Instructors; }
        public List<Course> GetAllCourses() { return Courses; }
    }

    internal class Program
    {
        static int ReadInt(string prompt)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            int v;
            try
            {
                v = Convert.ToInt32(input);
            }
            catch
            {
                v = 0;
            }
            return v;
        }

        static string ReadString(string prompt)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            if (input == null || input == "")
                return "Unknown";
            return input;
        }

        static void Main()
        {
            SchoolStudentManager mgr = new SchoolStudentManager();

            mgr.AddInstructor(new Instructor(1, "Ali", "C# Basics"));
            mgr.AddInstructor(new Instructor(2, "Sara", "Java"));
            Instructor? i1 = mgr.FindInstructor(1);
            if (i1 != null)
                mgr.AddCourse(new Course(101, "C# Basics", i1));

            Instructor? i2 = mgr.FindInstructor(2);
            if (i2 != null)
                mgr.AddCourse(new Course(102, "Advanced Java", i2));

            mgr.AddStudent(new Student(1, "Ahmed", 20, new List<Course>()));
            mgr.AddStudent(new Student(2, "Mona", 22, new List<Course>()));

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n========================================");
                Console.WriteLine("      SCHOOL MANAGEMENT SYSTEM MENU      ");
                Console.WriteLine("========================================");
                Console.WriteLine("1.  Add Student");
                Console.WriteLine("2.  Add Instructor");
                Console.WriteLine("3.  Add Course");
                Console.WriteLine("4.  Enroll Student in Course");
                Console.WriteLine("5.  Update Student Information");
                Console.WriteLine("6.  Delete Student");
                Console.WriteLine("7.  View All Students");
                Console.WriteLine("8.  View All Instructors");
                Console.WriteLine("9.  View All Courses");
                Console.WriteLine("10. Find Student by ID");
                Console.WriteLine("11. Find Course by ID");
                Console.WriteLine("12. Find Instructor by ID");
                Console.WriteLine("0.  Exit");
                Console.WriteLine("========================================");
                Console.Write("Enter your choice: ");

                string? choice = Console.ReadLine();
                if (choice == "1")
                {
                    mgr.AddStudent(new Student(
                        ReadInt("Student ID: "),
                        ReadString("Student Name: "),
                        ReadInt("Student Age: "),
                        new List<Course>()));
                    Console.WriteLine("Student Added.");
                }
                else if (choice == "2")
                {
                    mgr.AddInstructor(new Instructor(
                        ReadInt("Instructor ID: "),
                        ReadString("Instructor Name: "),
                        ReadString("Specialization: ")));
                    Console.WriteLine("Instructor Added.");
                }
                else if (choice == "3")
                {
                    int cid = ReadInt("Course ID: ");
                    string? ttl = ReadString("Course Title: ");
                    Instructor? ins = mgr.FindInstructor(ReadInt("Instructor ID: "));
                    if (ins == null)
                    {
                        Console.WriteLine("Instructor not found!");
                    }
                    else
                    {
                        mgr.AddCourse(new Course(cid, ttl, ins));
                        Console.WriteLine("Course Added.");
                    }
                }
                else if (choice == "4")
                {
                    bool ok = mgr.EnrollStudentInCourse(
                        ReadInt("Student ID: "),
                        ReadInt("Course ID: "));
                    Console.WriteLine(ok ? "Enrollment Successful!" : "Enrollment Failed!");
                }
                else if (choice == "5")
                {
                    int oldId = ReadInt("Current Student ID: ");
                    if (mgr.FindStudent(oldId) == null)
                    {
                        Console.WriteLine("Student not found!");
                    }
                    else
                    {
                        bool upd = mgr.UpdateStudent(
                            oldId,
                            ReadInt("New ID: "),
                            ReadString("New Name: "),
                            ReadInt("New Age: "),
                            new List<Course>());
                        Console.WriteLine(upd ? "Student Updated!" : "Update Failed!");
                    }
                }
                else if (choice == "6")
                {
                    bool del = mgr.DeleteStudent(ReadInt("Student ID to Delete: "));
                    Console.WriteLine(del ? "Student Deleted!" : "Deletion Failed!");
                }
                else if (choice == "7")
                {
                    foreach (Student s in mgr.GetAllStudents())
                    {
                        Console.WriteLine(s);
                    }
                }
                else if (choice == "8")
                {
                    foreach (Instructor i in mgr.GetAllInstructors())
                    {
                        Console.WriteLine(i);
                    }
                }
                else if (choice == "9")
                {
                    foreach (Course c in mgr.GetAllCourses())
                    {
                        Console.WriteLine(c);
                    }
                }
                else if (choice == "10")
                {
                    Student? st = mgr.FindStudent(ReadInt("Student ID: "));
                    if (st != null)
                        Console.WriteLine(st.PrintDetails());
                    else
                        Console.WriteLine("Student not found!");
                }
                else if (choice == "11")
                {
                    Course? cr = mgr.FindCourse(ReadInt("Course ID: "));
                    if (cr != null)
                        Console.WriteLine(cr.PrintDetails());
                    else
                        Console.WriteLine("Course not found!");
                }
                else if (choice == "12")
                {
                    Instructor? insr = mgr.FindInstructor(ReadInt("Instructor ID: "));
                    if (insr != null)
                        Console.WriteLine(insr.PrintDetails());
                    else
                        Console.WriteLine("Instructor not found!");
                }
                else if (choice == "0")
                {
                    running = false;
                }
                else
                {
                    Console.WriteLine("Invalid choice!");
                }
            }

            Console.WriteLine("Exiting School Management System. Goodbye!");
        }
    }
}
