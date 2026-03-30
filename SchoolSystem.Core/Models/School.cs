using SchoolSystem.Core.Enums;

namespace SchoolSystem.Core.Models;

// School OWNS Departments (Composition)
// if School is deleted, its Departments are gone too
public class School
{
    // private list - School controls the lifecycle of departments
    private readonly List<Department> _departments = new();

    public Guid Id { get; }
    public string Name { get; }       // like "Cairo International School"
    public string Address { get; }

    // read-only view
    public IReadOnlyList<Department> Departments => _departments.AsReadOnly();

    public School(string name, string address)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("School name cannot be empty.");

        Id = Guid.NewGuid();
        Name = name;
        Address = address;
    }

    // School CREATES the department — this is Composition
    // the "new Department()" happens HERE, not outside
    public Department CreateDepartment(string name, DepartmentType type)
    {
        // only one department per type
        if (_departments.Any(d => d.Type == type))
            throw new InvalidOperationException($"A {type} department already exists.");

        var department = new Department(name, type);  // created BY the school
        _departments.Add(department);
        return department;
    }

    // School DESTROYS the department — no one else has it
    public void RemoveDepartment(DepartmentType type)
    {
        var department = _departments.FirstOrDefault(d => d.Type == type)
            ?? throw new InvalidOperationException($"No {type} department found.");

        _departments.Remove(department);
        // department is now unreferenced — GC will collect it
    }

    public Department? GetDepartment(DepartmentType type)
        => _departments.FirstOrDefault(d => d.Type == type);

    public override string ToString() => $"{Name} — {_departments.Count} department(s)";
}