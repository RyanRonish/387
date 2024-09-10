using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    // A list to store student objects
    static List<Student> students = new List<Student>();

    static void Main(string[] args)
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Enter Students");
            Console.WriteLine("2. Enter Student Grade");
            Console.WriteLine("3. Show List of Students");
            Console.WriteLine("4. Remove Students");
            Console.WriteLine("5. Grade Analytics");
            Console.WriteLine("6. Quit");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        EnterStudents();
                        break;
                    case 2:
                        EnterStudentGrade();
                        break;
                    case 3:
                        ShowListOfStudents();
                        break;
                    case 4:
                        RemoveStudent();
                        break;
                    case 5:
                        GradeAnalytics();
                        break;
                    case 6:
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter a number.");
            }
        }
        Console.ReadKey();
    }

    static void EnterStudents()
    {
        Console.WriteLine("Enter student details:");
        Console.Write("Student ID: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("First Name: ");
        string firstName = Console.ReadLine();
        Console.Write("Last Name: ");
        string lastName = Console.ReadLine();

        // Create a new student object and add it to the list of students
        students.Add(new Student(id, firstName, lastName));
        Console.WriteLine("Student added successfully.");
    }

    static void EnterStudentGrade()
    {
        Console.WriteLine("Enter student grade:");
        Console.Write("Student ID: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Grade (0-100): ");
        double grade = double.Parse(Console.ReadLine());

        // Find the student by ID
        Student student = students.FirstOrDefault(s => s.Id == id);
        if (student != null)
        {
            // Add the grade to the student's record
            student.AddGrade(grade);
            Console.WriteLine("Grade added successfully.");
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }

    static void ShowListOfStudents()
    {
        Console.WriteLine("List of Students:");
        foreach (var student in students)
        {
            // Display the list of students with their IDs and names
            Console.WriteLine($"{student.Id}: {student.FirstName} {student.LastName}");
        }
    }

    static void RemoveStudent()
    {
        Console.Write("Enter student ID to remove: ");
        int id = int.Parse(Console.ReadLine());

        // Find the student by ID
        Student studentToRemove = students.FirstOrDefault(s => s.Id == id);
        if (studentToRemove != null)
        {
            // Remove the student from the list
            students.Remove(studentToRemove);
            Console.WriteLine("Student removed successfully.");
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }

    static void GradeAnalytics()
    {
        if (students.Count == 0)
        {
            Console.WriteLine("No students to analyze.");
            return;
        }

        double totalGrades = 0;
        double minGrade = 100;
        string minGradeStudent = "";
        double maxGrade = 0;
        string maxGradeStudent = "";
        int countA = 0, countB = 0, countC = 0, countD = 0, countF = 0;

        foreach (var student in students)
        {
            if (student.Grades.Any())
            {
                double average = student.Grades.Average();
                totalGrades += average;

                if (average < minGrade)
                {
                    minGrade = average;
                    minGradeStudent = $"{student.FirstName} {student.LastName}";
                }

                if (average > maxGrade)
                {
                    maxGrade = average;
                    maxGradeStudent = $"{student.FirstName} {student.LastName}";
                }

                if (average >= 90)
                {
                    countA++;
                }
                else if (average >= 80)
                {
                    countB++;
                }
                else if (average >= 70)
                {
                    countC++;
                }
                else if (average >= 60)
                {
                    countD++;
                }
                else
                {
                    countF++;
                }
            }
        }

        double averageGrade = totalGrades / students.Count;

        Console.WriteLine($"Average Grade: {averageGrade:F2}");
        Console.WriteLine($"Minimum Grade ({minGradeStudent}): {minGrade:F2}");
        Console.WriteLine($"Maximum Grade ({maxGradeStudent}): {maxGrade:F2}");
        Console.WriteLine($"% of A's: {(double)countA / students.Count * 100:F2}%");
        Console.WriteLine($"% of B's: {(double)countB / students.Count * 100:F2}%");
        Console.WriteLine($"% of C's: {(double)countC / students.Count * 100:F2}%");
        Console.WriteLine($"% of D's: {(double)countD / students.Count * 100:F2}%");
        Console.WriteLine($"% of F's: {(double)countF / students.Count * 100:F2}%");
    }
}

class Student
{
    public int Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public List<double> Grades { get; } = new List<double>();

    public Student(int id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }

    public void AddGrade(double grade)
    {
        // Add a grade to the student's record
        Grades.Add(grade);
    }
}