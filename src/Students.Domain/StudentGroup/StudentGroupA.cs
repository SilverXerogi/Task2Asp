using Students.Domain.Enums;

namespace Students.Domain.StudentGroup;

public class StudentGroupA
{
    public Guid Id { get; }
    public String Name { get; }
    public String? Abbr { get; }
    public DateTime StartDateTime { get; }
    public DateTime EndDateTime { get; }
    public StudyFormat StudyFormat { get; set; }

    public StudentGroupA(Guid id, String name, String abbr, DateTime startDateTime, DateTime endDateTime, StudyFormat studyFormat) 
    {
        Id = id;
        Name = name;
        Abbr = abbr;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        StudyFormat = studyFormat;
    }
    public Int32 GetTotalCourses()
    {
        return EndDateTime.Year - StartDateTime.Year;
    }
    public Int32 GetCourse(DateTime currentDate)
    {
        if (currentDate < StartDateTime)
            return 0;

        if (currentDate > EndDateTime)
            return GetTotalCourses();

        Int32 course = currentDate.Year - StartDateTime.Year + 1;

        if (currentDate < StartDateTime.AddYears(course - 1))
            course--;

        return course;
    }
}