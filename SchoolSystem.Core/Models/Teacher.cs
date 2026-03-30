using SchoolSystem.Core.Abstractions;
using SchoolSystem.Core.Interfaces;

namespace SchoolSystem.Core.Models;

// Teacher IS A Person (Inheritance) + can teach courses (ITeachable - Abstraction)
public class Teacher : Person, ITeachable
{
    // private list of course names this teacher is assigned to
    private readonly List<string> _assignedCourses = new();

    public string EmployeeId { get; }   // unique id like "TCH-001"
    public decimal Salary { get; set; } // salary can be updated later

    // calls Person's constructor
    public Teacher(string name, DateTime dateOfBirth, string email, string employeeId, decimal salary)
        : base(name, dateOfBirth, email)
    {
        if (string.IsNullOrWhiteSpace(employeeId))
            throw new ArgumentException("Employee ID cannot be empty.");

        EmployeeId = employeeId;
        Salary = salary;
    }

    // === POLYMORPHISM: returns "Teacher" instead of "Person" ===
    public override string GetRole() => "Teacher";

    // === ITeachable Implementation ===

    public void AssignCourse(string courseName)
    {
        if (_assignedCourses.Contains(courseName))
            throw new InvalidOperationException($"Already assigned to {courseName}.");

        _assignedCourses.Add(courseName);
    }

    public void RemoveCourse(string courseName)
    {
        if (!_assignedCourses.Remove(courseName))
            throw new InvalidOperationException($"Not assigned to {courseName}.");
    }

    // read-only view (Encapsulation)
    public IReadOnlyList<string> GetAssignedCourses()
        => _assignedCourses.AsReadOnly();
}