using SchoolSystem.Core.Abstractions;
using SchoolSystem.Core.Enums;
using SchoolSystem.Core.Models;

// ============================================================
// 1. COMPOSITION: School creates and owns Departments
// ============================================================
Console.WriteLine("=== COMPOSITION: School → Department ===\n");

var school = new School("Cairo International School", "Cairo, Egypt");

var scienceDept = school.CreateDepartment("Science Dept", DepartmentType.Science);
var mathDept = school.CreateDepartment("Math Dept", DepartmentType.Mathematics);

Console.WriteLine(school);
Console.WriteLine($"  - {scienceDept}");
Console.WriteLine($"  - {mathDept}");

school.RemoveDepartment(DepartmentType.Science);
Console.WriteLine($"\nAfter removing Science Dept:");
Console.WriteLine($"  Departments count: {school.Departments.Count}");

// ============================================================
// 2. AGGREGATION: Department holds Teachers but doesn't own them
// ============================================================
Console.WriteLine("\n=== AGGREGATION: Department → Teacher ===\n");

var scienceDept2 = school.CreateDepartment("Science Dept", DepartmentType.Science);

var drHany = new Teacher("Dr. Hany", new DateTime(1980, 3, 10), "hany@school.com", "TCH-001", 15000m);
var drMona = new Teacher("Dr. Mona", new DateTime(1985, 7, 22), "mona@school.com", "TCH-002", 14000m);

scienceDept2.AddTeacher(drHany);
scienceDept2.AddTeacher(drMona);

Console.WriteLine(scienceDept2);

scienceDept2.RemoveTeacher(drHany);

Console.WriteLine($"\nAfter removing Dr. Hany from dept:");
Console.WriteLine($"  Dept teachers: {scienceDept2.Teachers.Count}");
Console.WriteLine($"  Dr. Hany alive? {drHany.Name} - {drHany.Email}");

// ============================================================
// 3. INHERITANCE + POLYMORPHISM
// ============================================================
Console.WriteLine("\n=== INHERITANCE + POLYMORPHISM ===\n");

var sara = new Student("Sara Ahmed", new DateTime(2002, 5, 15), "sara@school.com", "STU-001");
var ahmed = new Student("Ahmed Ali", new DateTime(2001, 8, 20), "ahmed@school.com", "STU-002");

Console.WriteLine($"  Sara is Person? {sara is Person}");
Console.WriteLine($"  Dr. Hany is Person? {drHany is Person}");

Console.WriteLine($"\n  Sara.GetRole() = {sara.GetRole()}");
Console.WriteLine($"  Dr. Hany.GetRole() = {drHany.GetRole()}");

Console.WriteLine("\n  All people:");
List<Person> people = [sara, ahmed, drHany, drMona];

foreach (var person in people)
    Console.WriteLine($"    {person}");

// ============================================================
// 4. ASSOCIATION: Student ↔ Course (many-to-many)
// ============================================================
Console.WriteLine("\n=== ASSOCIATION: Student ↔ Course ===\n");

var cs101 = new Course("CS101", "Intro to Programming", maxCapacity: 2);
var math201 = new Course("MATH201", "Linear Algebra");

sara.EnrollInCourse(cs101);
sara.EnrollInCourse(math201);
ahmed.EnrollInCourse(cs101);

Console.WriteLine($"  {cs101}");
Console.WriteLine($"  {math201}");

Console.WriteLine($"\n  Sara's courses: {sara.EnrolledCourses.Count}");
Console.WriteLine($"  Ahmed's courses: {ahmed.EnrolledCourses.Count}");
Console.WriteLine($"  CS101 students: {cs101.EnrolledStudents.Count}");

sara.DropCourse(cs101);

Console.WriteLine($"\n  After Sara drops CS101:");
Console.WriteLine($"  Sara's courses: {sara.EnrolledCourses.Count}");
Console.WriteLine($"  CS101 students: {cs101.EnrolledStudents.Count}");

// ============================================================
// 5. ABSTRACTION: IExamable interface in action
// ============================================================
Console.WriteLine("\n=== ABSTRACTION: IExamable ===\n");

ahmed.EnrollInCourse(math201);

var saraGrade = sara.TakeExam("Linear Algebra", 92);
var ahmedGrade = ahmed.TakeExam("Intro to Programming", 55);

Console.WriteLine($"  Sara's grade in Linear Algebra: {saraGrade}");
Console.WriteLine($"  Ahmed's grade in Programming: {ahmedGrade}");

Console.WriteLine($"  Sara passed? {sara.HasPassedCourse("Linear Algebra")}");
Console.WriteLine($"  Ahmed passed? {ahmed.HasPassedCourse("Intro to Programming")}");

Console.WriteLine("\n  Sara's transcript:");
foreach (var record in sara.GetTranscript())
    Console.WriteLine($"    {record.Key}: {record.Value}");

// ============================================================
// 6. ENCAPSULATION: validation protects the object
// ============================================================
Console.WriteLine("\n=== ENCAPSULATION: Validation ===\n");

try
{
    var bad = new Student("", new DateTime(2000, 1, 1), "test@x.com", "STU-999");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"  Empty name: {ex.Message}");
}

try
{
    var bad = new Student("Test", new DateTime(2000, 1, 1), "not-an-email", "STU-999");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"  Bad email: {ex.Message}");
}

Console.WriteLine($"\n  Sara's email: {sara.Email}");
Console.WriteLine($"  Sara's age: {sara.Age}");

Console.WriteLine("\n=== Done! ===");