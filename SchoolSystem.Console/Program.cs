using SchoolSystem.Core.Abstractions;
using SchoolSystem.Core.Models;


// === ASSOCIATION: Student ↔ Course (many-to-many) ===


var sara = new Student("Sara Ahmed", new DateTime(2002, 5, 15), "sara@school.com", "STU-001");
var ahmed = new Student("Ahmed Ali", new DateTime(2001, 8, 20), "ahmed@school.com", "STU-002");

Console.WriteLine("\n=== ASSOCIATION ===\n");

var cs101 = new Course("CS101", "Intro to Programming", maxCapacity: 2);
var math201 = new Course("MATH201", "Linear Algebra");

// Sara enrolls in 2 courses
sara.EnrollInCourse(cs101);
sara.EnrollInCourse(math201);

// Ahmed enrolls in 1 course
ahmed.EnrollInCourse(cs101);

// both sides know about each other
Console.WriteLine($"  {cs101}");    // 2/2
Console.WriteLine($"  {math201}");  // 1/30

Console.WriteLine($"\n  Sara's courses: {sara.EnrolledCourses.Count}");    // 2
Console.WriteLine($"  Ahmed's courses: {ahmed.EnrolledCourses.Count}");    // 1
Console.WriteLine($"  CS101 students: {cs101.EnrolledStudents.Count}");    // 2

// drop course — both sides updated
sara.DropCourse(cs101);

Console.WriteLine($"\n  After Sara drops CS101:");
Console.WriteLine($"  Sara's courses: {sara.EnrolledCourses.Count}");    // 1
Console.WriteLine($"  CS101 students: {cs101.EnrolledStudents.Count}");  // 1