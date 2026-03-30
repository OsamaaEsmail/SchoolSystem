namespace SchoolSystem.Core.Interfaces;

/// <summary>
/// أي حد يقدر يدرّس (زي Teacher) لازم يطبق العقد ده
/// </summary>
public interface ITeachable
{
    void AssignCourse(string courseName);
    void RemoveCourse(string courseName);
    IReadOnlyList<string> GetAssignedCourses();
}