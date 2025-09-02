using School_Management_System;
using System.ComponentModel;

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
            {
                return false;
            }
            else
            {
                Courses.Add(course);
                return true;
            }
        }
        private static string GetTitle(Course c) => c.Title;

        public override string PrintDetails()
        {
            string courseNames = string.Join(", ", Courses.ConvertAll(GetTitle));

            return $"{StudentId} {Name} {Age} Courses: {courseNames}";
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
        public override string PrintDetails() => $"{InstructorId} {Name} {Specialization}";
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
        public override string PrintDetails() => $"{CourseId} {Title} Instructor: {Instructor.Name}";
    }

    class SchoolStudentManager
    {
        public SchoolStudentManager()
        {
            Students = new List<Student>();
            Courses = new List<Course>();
            Instructors = new List<Instructor>();
        }

        public List<Student> Students { get; set; }
        public List<Course> Courses { get; set; }
        public List<Instructor> Instructors { get; set; }
        public bool AddStudent(Student student)
        {
            for (int i = 0; i < Students.Count; i++)
            {
                if (Students[i].StudentId == student.StudentId)
                {
                    return false;
                }
            }

            Students.Add(student);
            return true;
        }
        public bool AddCourse(Course course)
        {
            for (int i = 0; i < Courses.Count; i++)
            {
                if (Courses[i].CourseId == course.CourseId)
                {
                    return false;
                }
            }
            Courses.Add(course);
            return true;
        }
        public bool AddInstructor(Instructor instructor)
        {
            for (int i = 0; i < Instructors.Count; i++)
            {
                if (Instructors[i].InstructorId == instructor.InstructorId)
                {
                    return false;
                }
            }
            Instructors.Add(instructor);
            return true;
        }
        public Student? FindStudent(int studentId, string name)
        {
            for (int i = 0; i < Students.Count; i++)
            {
                if (Students[i].StudentId == studentId && Students[i].Name.ToLower() == name.ToLower())
                {
                    return Students[i];
                }
            }
            return null;
        }
        public Course? FindCourse(int courseId, string name)
        {
            for (int i = 0; i < Courses.Count; i++)
            {
                if (Courses[i].CourseId == courseId && Courses[i].Title.ToLower() == name.ToLower())
                {
                    return Courses[i];
                }
            }
            return null;
        }
        public Instructor? FindInstructor(int instructorId, string name)
        {
            for (int i = 0; i < Instructors.Count; i++)
            {
                if (Instructors[i].InstructorId == instructorId || Instructors[i].Name.ToLower() == name.ToLower())
                {
                    return Instructors[i];
                }
            }
            return null;
        }
        public bool DeleteStudent(int studentId, string name)
        {
            var student = FindStudent(studentId, name);
            if (student != null)
            {
                Students.Remove(student);
                return true;
            }
            return false;
        }
        public bool UpdateStudent(int studentId, string name, int newId, string newName, int newAge, List<Course> newCourses)
        {
            Student? student = FindStudent(studentId, name);
            if (student == null)
                return false;

            student.StudentId = newId;
            student.Name = newName;
            student.Age = newAge;
            student.Courses = newCourses;

            return true;
        }
        public bool EnrollStudentInCourse(int studentId, int courseId)
        {
            for (int i = 0; i < Students.Count; i++)
            {
                if (Students[i].StudentId == studentId)
                {
                    for (int j = 0; j < Courses.Count; j++)
                    {
                        if (Courses[j].CourseId == courseId)
                        {
                            return Students[i].Enroll(Courses[j]);
                        }
                    }
                }
            }
            return false;
        }


        public List<Student> GetAllStudents() => Students;
        public List<Instructor> GetAllInstructors() => Instructors;
        public List<Course> GetAllCourses() => Courses;




    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var manager = new SchoolStudentManager();

            var instructor1 = new Instructor(1, "Ali", "C# Basics");
            var instructor2 = new Instructor(2, "Sara", "Java");
            manager.AddInstructor(instructor1);
            manager.AddInstructor(instructor2);

            var course1 = new Course(101, "C# Basics", instructor1);
            var course2 = new Course(102, "Advanced Java", instructor2);
            manager.AddCourse(course1);
            manager.AddCourse(course2);

            var student1 = new Student(1, "Ahmed", 20, new List<Course>());
            var student2 = new Student(2, "Mona", 22, new List<Course>());
            manager.AddStudent(student1);
            manager.AddStudent(student2);

            bool running = true;

            while (running)
            {
                Console.WriteLine("\n========================================");
                Console.WriteLine("      SCHOOL MANAGEMENT SYSTEM MENU      ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Add Instructor");
                Console.WriteLine("3. Add Course");
                Console.WriteLine("4. Enroll Student in Course");
                Console.WriteLine("5. Update Student Information");
                Console.WriteLine("6. Delete Student");
                Console.WriteLine("7. View All Students");
                Console.WriteLine("8. View All Instructors");
                Console.WriteLine("9. View All Courses");
                Console.WriteLine("10. Find Student by ID & Name");
                Console.WriteLine("11. Find Course by ID & Name");
                Console.WriteLine("12. Find Instructor by ID & Name");
                Console.WriteLine("0. Exit");
                Console.WriteLine("========================================");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine() ?? " ";

                switch (choice)
                {
                    case "1":
                        Console.Write("Student ID: ");
                        int sid = int.Parse(Console.ReadLine() ?? " ");
                        Console.Write("Student Name: ");
                        string sname = Console.ReadLine() ?? " ";
                        Console.Write("Student Age: ");
                        int sage = int.Parse(Console.ReadLine() ?? " ");
                        manager.AddStudent(new Student(sid, sname, sage, new List<Course>()));
                        Console.WriteLine("Student Added.");
                        break;

                    case "2":
                        Console.Write("Instructor ID: ");
                        int iid = int.Parse(Console.ReadLine() ?? " ");
                        Console.Write("Instructor Name: ");
                        string iname = Console.ReadLine() ?? " ";
                        Console.Write("Specialization: ");
                        string spec = Console.ReadLine() ?? " ";
                        manager.AddInstructor(new Instructor(iid, iname, spec));
                        Console.WriteLine("Instructor Added.");
                        break;

                    case "3":
                        Console.Write("Course ID: ");
                        int cid = int.Parse(Console.ReadLine() ?? " ");
                        Console.Write("Course Title: ");
                        string ctitle = Console.ReadLine() ?? " ";
                        Console.Write("Instructor ID: ");
                        int ciid = int.Parse(Console.ReadLine() ?? " ");
                        Instructor? ins = manager.FindInstructor(ciid, "");
                        if (ins != null)
                        {
                            manager.AddCourse(new Course(cid, ctitle, ins));
                            Console.WriteLine("Course Added.");
                        }
                        else Console.WriteLine("Instructor not found!");
                        break;

                    case "4":
                        Console.Write("Student ID: ");
                        int esid = int.Parse(Console.ReadLine() ?? " ");
                        Console.Write("Course ID: ");
                        int ecid = int.Parse(Console.ReadLine() ?? " ");
                        bool enrolled = manager.EnrollStudentInCourse(esid, ecid);
                        Console.WriteLine(enrolled ? "Enrollment Successful!" : "Enrollment Failed!");
                        break;

                    case "5":
                        Console.Write("Current Student ID: ");
                        int usid = int.Parse(Console.ReadLine() ?? " ");
                        Console.Write("Current Student Name: ");
                        string usname = Console.ReadLine() ?? " ";
                        Console.Write("New ID: ");
                        int nusid = int.Parse(Console.ReadLine() ?? " ");
                        Console.Write("New Name: ");
                        string nname = Console.ReadLine() ?? " ";
                        Console.Write("New Age: ");
                        int nuage = int.Parse(Console.ReadLine() ?? " ");
                        bool updated = manager.UpdateStudent(usid, usname, nusid, nname, nuage, new List<Course>());
                        Console.WriteLine(updated ? "Student Updated!" : "Update Failed!");
                        break;

                    case "6":
                        Console.Write("Student ID to Delete: ");
                        int dsid = int.Parse(Console.ReadLine() ?? " ");
                        Console.Write("Student Name: ");
                        string dsname = Console.ReadLine() ?? " ";
                        bool deleted = manager.DeleteStudent(dsid, dsname);
                        Console.WriteLine(deleted ? "Student Deleted!" : "Deletion Failed!");
                        break;

                    case "7":
                        foreach (var s in manager.GetAllStudents())
                            Console.WriteLine(s.PrintDetails());
                        break;

                    case "8":
                        foreach (var i in manager.GetAllInstructors())
                            Console.WriteLine(i.PrintDetails());
                        break;

                    case "9":
                        foreach (var c in manager.GetAllCourses())
                            Console.WriteLine(c.PrintDetails());
                        break;

                    case "10":
                        Console.Write("Student ID: ");
                        int fsid = int.Parse(Console.ReadLine() ?? " ");
                        Console.Write("Student Name: ");
                        string fsname = Console.ReadLine() ?? " ";
                        var fStudent = manager.FindStudent(fsid, fsname);
                        Console.WriteLine(fStudent != null ? fStudent.PrintDetails() : "Student not found!");
                        break;

                    case "11":
                        Console.Write("Course ID: ");
                        int fcid = int.Parse(Console.ReadLine() ?? " ");
                        Console.Write("Course Name: ");
                        string fcname = Console.ReadLine() ?? " ";
                        var fCourse = manager.FindCourse(fcid, fcname);
                        Console.WriteLine(fCourse != null ? fCourse.PrintDetails() : "Course not found!");
                        break;

                    case "12":
                        Console.Write("Instructor ID: ");
                        int fiid = int.Parse(Console.ReadLine() ?? " ");
                        Console.Write("Instructor Name: ");
                        string finame = Console.ReadLine() ?? " ";
                        var fInstructor = manager.FindInstructor(fiid, finame);
                        Console.WriteLine(fInstructor != null ? fInstructor.PrintDetails() : "Instructor not found!");
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }

            Console.WriteLine("Exiting School Management System. Goodbye!");
        }

    }
}

