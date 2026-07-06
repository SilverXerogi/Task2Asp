using Students.Domain.Enums;


namespace Students.Domain.StudentGroup
{
    public class StudentGroupABlank
    {
        public Guid? Id { get; set; }
        public String? Name { get; set; }
        public String? Abbr { get; set; }
        public DateOnly? Data { get; set; }
        public StudyFormat? StudyFormat { get; set; }
    }
}
