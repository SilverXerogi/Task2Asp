using Students.Domain.Enums;

namespace Students.Services.StudentGroups.Repositories.Models;

public class StudentGroupDb
{
    public Guid Id { get; set; }
    public String Name { get; set; }
    public String Abbreviation { get; set; }
    public DateTime StartDateTime { get; set; } 
    public DateTime EndDateTime { get; set; }   
    public StudyFormat StudyFormat { get; set; }
    public DateTime CreatedDateTimeUtc { get; set; }
    public DateTime? ModifiedDateTimeUtc { get; set; }
    public Boolean IsRemoved { get; set; }

    public StudentGroupDb(
        Guid id,
        String name,
        String abbreviation,
        DateTime startDateTime,
        DateTime endDateTime,
        StudyFormat studyFormat,
        DateTime createdDateTimeUtc,
        DateTime? modifiedDateTimeUtc,
        Boolean isRemoved
    )
    {
        Id = id;
        Name = name;
        Abbreviation = abbreviation;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        StudyFormat = studyFormat;
        CreatedDateTimeUtc = createdDateTimeUtc;
        ModifiedDateTimeUtc = modifiedDateTimeUtc;
        IsRemoved = isRemoved;
    }
}