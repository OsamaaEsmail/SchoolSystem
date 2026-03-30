using SchoolSystem.Core.Enums;

namespace SchoolSystem.Core.Models;

// Department HAS Teachers but does NOT own them (Aggregation)
// if Department is deleted, Teachers still exist somewhere else
public class Department
{
    // private list of teachers in this department
    private readonly List<Teacher> _teachers = new();

    public Guid Id { get; }
    public string Name { get; }              // like "Computer Science"
    public DepartmentType Type { get; }      // Science, Math, etc.

    // read-only view
    public IReadOnlyList<Teacher> Teachers => _teachers.AsReadOnly();

    public Department(string name, DepartmentType type)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Department name cannot be empty.");

        Id = Guid.NewGuid();
        Name = name;
        Type = type;
    }

    // Teacher is created OUTSIDE and passed in — this is Aggregation
    // Department did NOT create the teacher, it just holds a reference
    public void AddTeacher(Teacher teacher)
    {
        if (_teachers.Any(t => t.EmployeeId == teacher.EmployeeId))
            throw new InvalidOperationException($"Teacher {teacher.Name} is already in this department.");

        _teachers.Add(teacher);
    }

    // remove teacher from department — but teacher still alive
    public void RemoveTeacher(Teacher teacher)
    {
        if (!_teachers.Remove(teacher))
            throw new InvalidOperationException($"Teacher {teacher.Name} is not in this department.");
    }

    public override string ToString() => $"[{Type}] {Name} — {_teachers.Count} teacher(s)";
}