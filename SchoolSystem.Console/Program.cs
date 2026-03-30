using SchoolSystem.Core.Enums;
using SchoolSystem.Core.Models;

// === COMPOSITION: School creates and owns Departments ===

var school = new School("Cairo International School", "Cairo, Egypt");

var scienceDept = school.CreateDepartment("Science Dept", DepartmentType.Science);
var mathDept = school.CreateDepartment("Math Dept", DepartmentType.Mathematics);

Console.WriteLine(school);
Console.WriteLine($"  - {scienceDept}");
Console.WriteLine($"  - {mathDept}");

// Composition proof: School DESTROYS the department
school.RemoveDepartment(DepartmentType.Science);
Console.WriteLine($"\nAfter removing Science Dept:");
Console.WriteLine($"  Departments count: {school.Departments.Count}");
