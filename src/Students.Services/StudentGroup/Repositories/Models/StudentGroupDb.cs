using Students.Domain.Enums;

namespace Students.Services.StudentGroups.Repositories.Models;

public class StudentGroupDb
{
    public Guid Id { get; set; }
    public String Name { get; set; }
    public String Abbreviation { get; set; }
    public Int32 StartYear { get; set; }
    public Int32 EndYear { get; set; }
    public StudyFormat StudyFormat { get; set; }
    public DateTime CreatedDateTimeUtc { get; set; }
    public DateTime? ModifiedDateTimeUtc { get; set; }
    public Boolean IsRemoved { get; set; }

    public StudentGroupDb(
        Guid id,
        String name,
        String abbreviation,
        Int32 startYear,
        Int32 endYear,
        StudyFormat studyFormat,
        DateTime createdDateTimeUtc,
        DateTime? modifiedDateTimeUtc,
        Boolean isRemoved
    )
    {
        Id = id;
        Name = name;
        Abbreviation = abbreviation;
        StartYear = startYear;
        EndYear = endYear;
        StudyFormat = studyFormat;
        CreatedDateTimeUtc = createdDateTimeUtc;
        ModifiedDateTimeUtc = modifiedDateTimeUtc;
        IsRemoved = isRemoved;
    }
}