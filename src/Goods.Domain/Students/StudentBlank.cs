using Students.Domain.Enums;

namespace Students.Domain.Students;

public class StudentBlank
{
    public Guid? Id { get; set; }
    public String? FullName { get; set; }
    public Gender? Gender { get; set; }
    public Int32? Age { get; set; }
    public Single? AverageGrade { get; set; }
    public String[]? SpecialMarks { get; set; }
    public Guid? StudentGroupId { get; set; }

}
