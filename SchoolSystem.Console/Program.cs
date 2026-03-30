using SchoolSystem.Core.Abstractions;
using SchoolSystem.Core.Models;



var school = new School("Cairo International School", "Cairo, Egypt");


// Teachers created OUTSIDE — not by the department
var drHany = new Teacher("Dr. Hany", new DateTime(1980, 3, 10), "hany@school.com", "TCH-001", 15000m);
var drMona = new Teacher("Dr. Mona", new DateTime(1985, 7, 22), "mona@school.com", "TCH-002", 14000m);



Console.WriteLine("\n=== INHERITANCE + POLYMORPHISM ===\n");

var sara = new Student("Sara Ahmed", new DateTime(2002, 5, 15), "sara@school.com", "STU-001");
var ahmed = new Student("Ahmed Ali", new DateTime(2001, 8, 20), "ahmed@school.com", "STU-002");

// Inheritance: Student and Teacher are both Person
Console.WriteLine($"  Sara is Person? {sara is Person}");      // True
Console.WriteLine($"  Dr. Hany is Person? {drHany is Person}"); // True

// Polymorphism: same method call → different result
Console.WriteLine($"\n  Sara.GetRole() = {sara.GetRole()}");       // Student
Console.WriteLine($"  Dr. Hany.GetRole() = {drHany.GetRole()}");   // Teacher

// the real power: loop through base type, each behaves differently
Console.WriteLine("\n  All people:");
List<Person> people = [sara, ahmed, drHany, drMona];

foreach (var person in people)
    Console.WriteLine($"    {person}");  // ToString() uses GetRole() internally