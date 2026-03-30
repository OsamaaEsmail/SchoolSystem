using SchoolSystem.Core.Enums;
using SchoolSystem.Core.Models;



var school = new School("Cairo International School", "Cairo, Egypt");






// === AGGREGATION: Department holds Teachers but doesn't own them ===

Console.WriteLine("\n=== AGGREGATION ===\n");

// recreate science dept (we removed it above)
var scienceDept2 = school.CreateDepartment("Science Dept", DepartmentType.Science);

// Teachers created OUTSIDE — not by the department
var drHany = new Teacher("Dr. Hany", new DateTime(1980, 3, 10), "hany@school.com", "TCH-001", 15000m);
var drMona = new Teacher("Dr. Mona", new DateTime(1985, 7, 22), "mona@school.com", "TCH-002", 14000m);

// pass them IN to the department
scienceDept2.AddTeacher(drHany);
scienceDept2.AddTeacher(drMona);

Console.WriteLine(scienceDept2);  // 2 teachers

// remove Dr. Hany from department
scienceDept2.RemoveTeacher(drHany);

Console.WriteLine($"\nAfter removing Dr. Hany from dept:");
Console.WriteLine($"  Dept teachers: {scienceDept2.Teachers.Count}");  // 1
Console.WriteLine($"  Dr. Hany alive? {drHany.Name} - {drHany.Email}");  // still exists!
