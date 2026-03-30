namespace SchoolSystem.Core.Models;

// Course is independent - lives on its own without Student or Teacher
// the relation with Student is ASSOCIATION (many-to-many)
public class Course
{
    // private list - only Course itself can modify it (Encapsulation)
    private readonly List<Student> _enrolledStudents = new();

    public string CourseCode { get; }    // unique code like "CS101"
    public string CourseName { get; }    // display name like "Intro to Programming"
    public int MaxCapacity { get; }      // max students allowed in this course

    // read-only view - outside world can see students but NOT add/remove directly
    public IReadOnlyList<Student> EnrolledStudents => _enrolledStudents.AsReadOnly();

    // calculated property: true if course is full
    public bool IsFull => _enrolledStudents.Count >= MaxCapacity;

    public Course(string courseCode, string courseName, int maxCapacity = 30)
    {
        if (string.IsNullOrWhiteSpace(courseCode))
            throw new ArgumentException("Course code cannot be empty.");

        if (string.IsNullOrWhiteSpace(courseName))
            throw new ArgumentException("Course name cannot be empty.");

        if (maxCapacity <= 0)
            throw new ArgumentException("Max capacity must be positive.");

        CourseCode = courseCode;
        CourseName = courseName;
        MaxCapacity = maxCapacity;
    }

    // internal = only classes inside the same project can call this
    // Student.EnrollInCourse() calls this - not the outside world
    internal void AddStudent(Student student)
    {
        if (IsFull)
            throw new InvalidOperationException($"Course {CourseName} is full.");

        if (!_enrolledStudents.Contains(student))
            _enrolledStudents.Add(student);
    }

    // called by Student.DropCourse()
    internal void RemoveStudent(Student student)
    {
        _enrolledStudents.Remove(student);
    }

    public override string ToString() => $"{CourseCode}: {CourseName} ({_enrolledStudents.Count}/{MaxCapacity})";
}