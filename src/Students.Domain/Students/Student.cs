using Students.Domain.Enums;
namespace Students.Domain.Students;

public class Student
{
    public Guid Id { get; }
    public String FullName { get; }
    public Gender Gender { get;}
    public Int32 Age { get; }
    public Single AverageGrade { get;}
    public String[] SpecialMarks { get;}
    public Guid StudentGroupId { get;}
    public Boolean HasScholarship => AverageGrade >= 4;

    public Student(Guid id,String fullName, Gender gender, Int32 age, Single averageGrade, string[] specialMarks, Guid studentGroupId)
    {
        Id = id;
        FullName = fullName;
        Gender = gender;
        Age = age;
        AverageGrade = averageGrade;
        SpecialMarks = specialMarks;
        StudentGroupId = studentGroupId;
    }
    
    
}