// === ABSTRACTION: IExamable interface in action ===

using SchoolSystem.Core.Models;

Console.WriteLine("\n=== ABSTRACTION ===\n");

var sara = new Student("Sara Ahmed", new DateTime(2002, 5, 15), "sara@school.com", "STU-001");
var ahmed = new Student("Ahmed Ali", new DateTime(2001, 8, 20), "ahmed@school.com", "STU-002");

// must enroll first before taking exams
var math201 = new Course("MATH201", "Linear Algebra");
var cs101 = new Course("CS101", "Intro to Programming");

sara.EnrollInCourse(math201);
ahmed.EnrollInCourse(cs101);

// now they can take exams
var saraGrade = sara.TakeExam("Linear Algebra", 92);
var ahmedGrade = ahmed.TakeExam("Intro to Programming", 55);

Console.WriteLine($"  Sara's grade in Linear Algebra: {saraGrade}");
Console.WriteLine($"  Ahmed's grade in Programming: {ahmedGrade}");

Console.WriteLine($"  Sara passed? {sara.HasPassedCourse("Linear Algebra")}");
Console.WriteLine($"  Ahmed passed? {ahmed.HasPassedCourse("Intro to Programming")}");

Console.WriteLine("\n  Sara's transcript:");
foreach (var record in sara.GetTranscript())
    Console.WriteLine($"    {record.Key}: {record.Value}");


// === ENCAPSULATION: validation protects the object ===

Console.WriteLine("\n=== ENCAPSULATION ===\n");

// empty name → rejected
try
{
    var bad = new Student("", new DateTime(2000, 1, 1), "test@x.com", "STU-999");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"  Empty name: {ex.Message}");
}

// bad email → rejected
try
{
    var bad = new Student("Test", new DateTime(2000, 1, 1), "not-an-email", "STU-999");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"  Bad email: {ex.Message}");
}

// auto formatting
Console.WriteLine($"\n  Sara's email: {sara.Email}");  // lowercased
Console.WriteLine($"  Sara's age: {sara.Age}");         // calculated, read-only