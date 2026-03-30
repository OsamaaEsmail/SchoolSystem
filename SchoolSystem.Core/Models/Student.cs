using SchoolSystem.Core.Abstractions;
using SchoolSystem.Core.Enums;
using SchoolSystem.Core.Interfaces;

namespace SchoolSystem.Core.Models;

// Student IS A Person (Inheritance) + can take exams (IExamable - Abstraction)
public class Student : Person, IExamable
{
    // private collections - no one can touch them from outside (Encapsulation)
    private readonly Dictionary<string, Grade> _transcript = new();    // courseName → grade
    private readonly List<Course> _enrolledCourses = new();            // courses this student joined

    public string StudentId { get; }  // unique student id like "STU-001"

    // read-only view of enrolled courses - outside world can see but NOT modify
    public IReadOnlyList<Course> EnrolledCourses => _enrolledCourses.AsReadOnly();

    // calls Person's constructor using : base(...) — this is how inheritance works
    public Student(string name, DateTime dateOfBirth, string email, string studentId)
        : base(name, dateOfBirth, email)
    {
        if (string.IsNullOrWhiteSpace(studentId))
            throw new ArgumentException("Student ID cannot be empty.");

        StudentId = studentId;
    }

    // === POLYMORPHISM: same method name, different behavior ===
    public override string GetRole() => "Student";

    // === ASSOCIATION: Student ↔ Course (many-to-many) ===
    // student can join a course, and course knows about the student too
    public void EnrollInCourse(Course course)
    {
        // can't enroll in same course twice
        if (_enrolledCourses.Any(c => c.CourseCode == course.CourseCode))
            throw new InvalidOperationException($"Already enrolled in {course.CourseName}.");

        _enrolledCourses.Add(course);   // student side: add course
        course.AddStudent(this);         // course side: add student (both sides know each other)
    }

    public void DropCourse(Course course)
    {
        if (!_enrolledCourses.Remove(course))
            throw new InvalidOperationException($"Not enrolled in {course.CourseName}.");

        course.RemoveStudent(this);  // remove from both sides
    }

    // === IExamable Implementation (Abstraction - fulfilling the contract) ===

    // take an exam → convert score to grade → store in transcript
    public Grade TakeExam(string courseName, int score)
    {
        // must be enrolled first
        if (!_enrolledCourses.Any(c => c.CourseName == courseName))
            throw new InvalidOperationException($"Not enrolled in {courseName}.");

        // convert score to grade using switch expression
        var grade = score switch
        {
            >= 90 => Grade.A,
            >= 80 => Grade.B,
            >= 70 => Grade.C,
            >= 60 => Grade.D,
            _ => Grade.F
        };

        _transcript[courseName] = grade;  // save grade in transcript
        return grade;
    }

    // check if student passed a specific course (anything except F)
    public bool HasPassedCourse(string courseName)
        => _transcript.TryGetValue(courseName, out var grade) && grade != Grade.F;

    // return full transcript as read-only (Encapsulation)
    public IReadOnlyDictionary<string, Grade> GetTranscript()
        => _transcript.AsReadOnly();
}