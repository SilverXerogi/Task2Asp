using Students.Domain.Enums;

namespace Students.Services.Students.Repositories.Models;

public class StudentDb
{
    public Guid Id { get; set; }
    public String FullName { get; set; }
    public Gender Gender { get; set; }
    public Int32 Age { get; set; }
    public Single AverageGrade { get; set; }
    public String SpecialMarksJson { get; set; }
    public Guid StudentGroupId { get; set; }
    public DateTime CreatedDateTimeUtc { get; set; }
    public DateTime? ModifiedDateTimeUtc { get; set; }
    public Boolean IsRemoved { get; set; }

    public StudentDb(
        Guid id,
        String fullName,
        Gender gender,
        Int32 age,
        Single averageGrade,
        String specialMarksJson,
        Guid studentGroupId,
        DateTime createdDateTimeUtc,
        DateTime? modifiedDateTimeUtc,
        Boolean isRemoved
    )
    {
        Id = id;
        FullName = fullName;
        Gender = gender;
        Age = age;
        AverageGrade = averageGrade;
        SpecialMarksJson = specialMarksJson;
        StudentGroupId = studentGroupId;
        CreatedDateTimeUtc = createdDateTimeUtc;
        ModifiedDateTimeUtc = modifiedDateTimeUtc;
        IsRemoved = isRemoved;
    }
}