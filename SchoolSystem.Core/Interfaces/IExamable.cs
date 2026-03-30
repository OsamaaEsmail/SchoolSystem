using SchoolSystem.Core.Enums;

namespace SchoolSystem.Core.Interfaces;

/// <summary>
/// أي حد يقدر يمتحن (زي Student) لازم يطبق العقد ده
/// </summary>
public interface IExamable
{
    Grade TakeExam(string courseName, int score);
    bool HasPassedCourse(string courseName);
    IReadOnlyDictionary<string, Grade> GetTranscript();
}