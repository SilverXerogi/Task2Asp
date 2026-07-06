using Students.Domain.Enums;

namespace Students.Domain.StudentGroup;

public class StudentGroupA
{
    public Guid Id { get; }
    public String Name { get; }
    public String? Abbr { get; }
    public Int32 StartYear { get; set; }
    public Int32 EndYear { get; set; }
    public StudyFormat StudyFormat { get; set; }

    public StudentGroupA(Guid id, String name, String abbr, Int32 startYear, Int32 endYear, StudyFormat studyFormat) 
    {
        Id = id;
        Name = name;
        Abbr = abbr;
        StartYear = startYear;
        EndYear = endYear;
        StudyFormat = studyFormat;
    }
}