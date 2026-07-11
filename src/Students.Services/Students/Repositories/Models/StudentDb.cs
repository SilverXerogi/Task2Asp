using Students.Domain.Enums;

namespace Students.Services.Students.Repositories.Models;

public class StudentDb
{
    public Guid Id { get; set; }
    public String FullName { get; set; }
    public Gender Gender { get; set; }
    public DateTime BirthDate { get; set; }
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
        DateTime birthDate,
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
        BirthDate = birthDate;
        AverageGrade = averageGrade;
        SpecialMarksJson = specialMarksJson;
        StudentGroupId = studentGroupId;
        CreatedDateTimeUtc = createdDateTimeUtc;
        ModifiedDateTimeUtc = modifiedDateTimeUtc;
        IsRemoved = isRemoved;
    }
}