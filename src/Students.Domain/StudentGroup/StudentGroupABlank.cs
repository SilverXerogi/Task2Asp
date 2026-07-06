using Students.Domain.Enums;


namespace Students.Domain.StudentGroup
{
    public class StudentGroupABlank
    {
        public Guid? Id { get; set; }
        public String? Name { get; set; }
        public String? Abbr { get; set; }
        public Int32? StartYear { get; set; }
        public Int32? EndYear { get; set; }
        public StudyFormat? StudyFormat { get; set; }
    }
}
