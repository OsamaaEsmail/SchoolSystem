namespace SchoolSystem.Core.Abstractions;

// abstract = no one can do "new Person()", must inherit from it
public abstract class Person
{
    // === Private backing fields (Encapsulation - no one can access directly) ===
    private string _name = string.Empty;       // the real storage for Name
    private DateTime _dateOfBirth;              // the real storage for DateOfBirth
    private string _email = string.Empty;       // the real storage for Email

    // === Public properties (the only door to reach the private fields) ===

    public Guid Id { get; }  // read-only, set once in constructor, never changes

    public string Name
    {
        get => _name;   // return the private field
        set
        {
            // validation: reject empty names
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be empty.");

            _name = value.Trim();  // clean whitespace before storing
        }
    }

    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set
        {
            // validation: no one is born in the future
            if (value > DateTime.Now)
                throw new ArgumentException("Date of birth cannot be in the future.");

            _dateOfBirth = value;
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            // validation: must contain @ to be a valid email
            if (string.IsNullOrWhiteSpace(value) || !value.Contains('@'))
                throw new ArgumentException("Invalid email address.");

            _email = value.Trim().ToLowerInvariant();  // normalize: "Ahmed@X.COM" → "ahmed@x.com"
        }
    }

    // calculated property: read-only, no setter, computed from DateOfBirth
    public int Age => (int)((DateTime.Now - _dateOfBirth).TotalDays / 365.25);

    // protected = only child classes (Student, Teacher) can call this constructor
    protected Person(string name, DateTime dateOfBirth, string email)
    {
        Id = Guid.NewGuid();        // generate unique id once
        Name = name;                // goes through the setter → validated + trimmed
        DateOfBirth = dateOfBirth;  // goes through the setter → validated
        Email = email;              // goes through the setter → validated + lowercased
    }

    // virtual = child classes can override this and return something different (Polymorphism)
    public virtual string GetRole() => "Person";

    // override ToString to print something useful like: [Student] Ahmed (ahmed@school.com)
    public override string ToString() => $"[{GetRole()}] {Name} ({Email})";
}