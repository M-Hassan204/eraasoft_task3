using System;
using System.Collections.Generic;

class Course(int courseId, string title, Instructor instructor)
{
    public int CourseId = courseId;
    public string Title = title;
    public Instructor Instructor = instructor;

    public string PrintDetails()
    {
        return $"Course [{CourseId}] : {Title}  |  Instructor: {Instructor.Name}";
    }
}

class Instructor(int instructorId, string name, string specialization)
{
    public int InstructorId = instructorId;
    public string Name = name;
    public string Specialization = specialization;

    public string PrintDetails()
    {
        return $"Instructor [{InstructorId}] : {Name}  |  Specialization: {Specialization}";
    }
}

class Student(int studentId, string name, int age)
{
    public int StudentId = studentId;
    public string Name = name;
    public int Age = age;
    public List<Course> Courses = new List<Course>();

    public bool Enroll(Course course)
    {
        foreach (Course c in Courses)
        {
            if (c.CourseId == course.CourseId)
                return false;
        }
        Courses.Add(course);
        return true;
    }

    public string PrintDetails()
    {
        string courseList = "";
        if (Courses.Count == 0)
        {
            courseList = "None";
        }
        else
        {
            foreach (Course c in Courses)
                courseList += c.Title + ", ";
            courseList = courseList.TrimEnd(',', ' ');
        }
        return $"Student [{StudentId}] : {Name}  |  Age: {Age}  |  Courses: {courseList}";
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
            if (s.StudentId == student.StudentId) return false;
        Students.Add(student);
        return true;
    }

    public bool AddCourse(Course course)
    {
        foreach (Course c in Courses)
            if (c.CourseId == course.CourseId) return false;
        Courses.Add(course);
        return true;
    }

    public bool AddInstructor(Instructor instructor)
    {
        foreach (Instructor i in Instructors)
            if (i.InstructorId == instructor.InstructorId) return false;
        Instructors.Add(instructor);
        return true;
    }

    public Student FindStudent(int studentId)
    {
        foreach (Student s in Students)
            if (s.StudentId == studentId) return s;
        return null;
    }

    public Course FindCourse(int courseId)
    {
        foreach (Course c in Courses)
            if (c.CourseId == courseId) return c;
        return null;
    }

    public Instructor FindInstructor(int instructorId)
    {
        foreach (Instructor i in Instructors)
            if (i.InstructorId == instructorId) return i;
        return null;
    }

    public bool EnrollStudentInCourse(int studentId, int courseId)
    {
        Student student = FindStudent(studentId);
        Course course = FindCourse(courseId);

        if (student == null || course == null) return false;
        return student.Enroll(course);
    }
}

class Program
{
    static void Main(string[] args)
    {
        SchoolStudentManager manager = new SchoolStudentManager();

        Console.WriteLine("==============================================");
        Console.WriteLine("   Student Management System");
        Console.WriteLine("==============================================");

        bool running = true;
        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Add Instructor");
            Console.WriteLine("3. Add Course");
            Console.WriteLine("4. Enroll Student in Course");
            Console.WriteLine("5. Show All Students");
            Console.WriteLine("6. Show All Courses");
            Console.WriteLine("7. Show All Instructors");
            Console.WriteLine("8. Find Student by ID");
            Console.WriteLine("9. Find Course by ID");
            Console.WriteLine("0. Exit");
            Console.WriteLine();
            Console.Write("Choose: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":  // Add Student
                    Console.WriteLine("\n--- Add Student ---");
                    Console.Write("Enter Student ID: ");
                    int sid = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Name: ");
                    string sname = Console.ReadLine();
                    Console.Write("Enter Age: ");
                    int age = Convert.ToInt32(Console.ReadLine());
                    if (manager.AddStudent(new Student(sid, sname, age)))
                        Console.WriteLine("Student added successfully.");
                    else
                        Console.WriteLine("Error: Student ID already exists.");
                    break;

                case "2": // Add Instructor
                    Console.WriteLine("\n--- Add Instructor ---");
                    Console.Write("Enter Instructor ID: ");
                    int iid = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Name: ");
                    string iname = Console.ReadLine();
                    Console.Write("Enter Specialization: ");
                    string spec = Console.ReadLine();
                    if (manager.AddInstructor(new Instructor(iid, iname, spec)))
                        Console.WriteLine("Instructor added successfully.");
                    else
                        Console.WriteLine("Error: Instructor ID already exists.");
                    break;

                case "3": // Add Course
                    Console.WriteLine("\n--- Add Course ---");
                    if (manager.Instructors.Count == 0)
                    {
                        Console.WriteLine("No instructors available. Add an instructor first.");
                        break;
                    }
                    Console.WriteLine("Available Instructors:");
                    foreach (Instructor i in manager.Instructors)
                        Console.WriteLine("  " + i.PrintDetails());
                    Console.Write("Enter Course ID: ");
                    int cid = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Course Title: ");
                    string title = Console.ReadLine();
                    Console.Write("Select Instructor ID: ");
                    int instrId = Convert.ToInt32(Console.ReadLine());
                    Instructor instr = manager.FindInstructor(instrId);
                    if (instr == null)
                    {
                        Console.WriteLine("Error: Instructor not found.");
                        break;
                    }
                    if (manager.AddCourse(new Course(cid, title, instr)))
                        Console.WriteLine("Course added successfully.");
                    else
                        Console.WriteLine("Error: Course ID already exists.");
                    break;

                case "4": // Enroll Student in Course
                    Console.WriteLine("\n--- Enroll Student in Course ---");
                    Console.Write("Enter Student ID: ");
                    int esid = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Course ID: ");
                    int ecid = Convert.ToInt32(Console.ReadLine());
                    if (manager.EnrollStudentInCourse(esid, ecid))
                        Console.WriteLine("Student enrolled successfully.");
                    else
                        Console.WriteLine("Error: Student or Course not found, or already enrolled.");
                    break;

                case "5": // Show All Students
                    Console.WriteLine("\n--- All Students ---");
                    if (manager.Students.Count == 0) { Console.WriteLine("No students yet."); break; }
                    foreach (Student s in manager.Students)
                        Console.WriteLine(s.PrintDetails());
                    break;

                case "6": // Show All Courses
                    Console.WriteLine("\n--- All Courses ---");
                    if (manager.Courses.Count == 0) { Console.WriteLine("No courses yet."); break; }
                    foreach (Course c in manager.Courses)
                        Console.WriteLine(c.PrintDetails());
                    break;

                case "7": // Show All Instructors
                    Console.WriteLine("\n--- All Instructors ---");
                    if (manager.Instructors.Count == 0) { Console.WriteLine("No instructors yet."); break; }
                    foreach (Instructor i in manager.Instructors)
                        Console.WriteLine(i.PrintDetails());
                    break;

                case "8":  // Find Student
                    Console.WriteLine("\n--- Find Student ---");
                    Console.Write("Enter Student ID: ");
                    int fsid = Convert.ToInt32(Console.ReadLine());
                    Student fs = manager.FindStudent(fsid);
                    if (fs != null)
                        Console.WriteLine("Found: " + fs.PrintDetails());
                    else
                        Console.WriteLine("Student not found.");
                    break;

                case "9": // Find Course
                    Console.WriteLine("\n--- Find Course ---");
                    Console.Write("Enter Course ID: ");
                    int fcid = Convert.ToInt32(Console.ReadLine());
                    Course fc = manager.FindCourse(fcid);
                    if (fc != null)
                        Console.WriteLine("Found: " + fc.PrintDetails());
                    else
                        Console.WriteLine("Course not found.");
                    break;

                case "0": // Exit
                    Console.WriteLine("Goodbye!");
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
        }
    }
}