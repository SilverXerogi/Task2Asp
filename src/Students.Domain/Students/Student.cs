using Students.Domain.Enums;
namespace Students.Domain.Students;

public class Student
{
    public Guid Id { get; }
    public String FullName { get; }
    public Gender Gender { get;}
    public DateTime BirthDate { get; }
    public Single AverageGrade { get;}
    public String[] SpecialMarks { get;}
    public Guid StudentGroupId { get;}
    public Boolean HasScholarship => AverageGrade >= 4;

    public Student(Guid id,String fullName, Gender gender, DateTime birthDate, Single averageGrade, string[] specialMarks, Guid studentGroupId)
    {
        Id = id;
        FullName = fullName;
        Gender = gender;
        BirthDate = birthDate;
        AverageGrade = averageGrade;
        SpecialMarks = specialMarks;
        StudentGroupId = studentGroupId;
    }

    public Int32 GetAge(DateTime currentDate)
    {
        Int32 age = currentDate.Year - BirthDate.Year;

        if (currentDate < BirthDate.AddYears(age))
            age--;

        return age;
    }
}