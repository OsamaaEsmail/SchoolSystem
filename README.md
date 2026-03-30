# SchoolSystem — OOP Concepts & Class Relations Demo

A minimal .NET 9 Class Library demonstrating the four OOP pillars and class relationships through a simple School Management domain.

## Project Structure
```
SchoolSystem/
├── SchoolSystem.Core/
│   ├── Enums/
│   │   ├── Grade.cs
│   │   └── DepartmentType.cs
│   ├── Interfaces/
│   │   ├── IExamable.cs
│   │   └── ITeachable.cs
│   ├── Abstractions/
│   │   └── Person.cs
│   └── Models/
│       ├── Student.cs
│       ├── Teacher.cs
│       ├── Course.cs
│       ├── Department.cs
│       └── School.cs
└── SchoolSystem.Console/
    └── Program.cs
```

## OOP Concepts Covered

| Concept | Where | How |
|---------|-------|-----|
| **Encapsulation** | `Person` | Private backing fields with validated public properties. Name gets trimmed, Email gets lowercased, DateOfBirth rejects future dates. Age is a read-only calculated property. |
| **Inheritance** | `Student`, `Teacher` | Both inherit from the abstract `Person` class, sharing common properties (Name, Email, DateOfBirth) and adding their own (StudentId, Salary). |
| **Abstraction** | `IExamable`, `ITeachable` | Interfaces define contracts — *what* an object can do without specifying *how*. `Person` is abstract so it can't be instantiated directly. |
| **Polymorphism** | `GetRole()` | Virtual method in `Person`, overridden in `Student` and `Teacher`. Same method call on `Person` type returns different results based on actual type. |

## Class Relations Covered

| Relation | Classes | Description |
|----------|---------|-------------|
| **Inheritance (Is-A)** | `Student` → `Person` | Student *is a* Person. Teacher *is a* Person. |
| **Composition (Has-A, strong)** | `School` → `Department` | School creates and owns Departments. If School is deleted, Departments are gone too. `CreateDepartment()` uses `new` internally. |
| **Aggregation (Has-A, weak)** | `Department` → `Teacher` | Department holds references to Teachers but doesn't own them. Teachers are created outside and passed in. If Department is removed, Teachers still exist. |
| **Association** | `Student` ↔ `Course` | Many-to-many relationship. Both sides know about each other. Student can enroll/drop, both sides stay in sync via `internal` methods. |

