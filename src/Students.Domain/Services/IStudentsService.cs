using Students.Domain.Students;
using Students.Tools.Types.Results;

namespace Students.Domain.Services;

public interface IStudentsService
{
    Task<Result> SaveStudent(StudentBlank studentBlank);
    Task<Student> GetStudent(Guid id);
    Task<Page<Student>> GetStudents(Int32 page, Int32 count);
    Task<Page<Student>> GetStudentsByGroupId(Guid groupId, Int32 page, Int32 count);
    Task<Result> RemoveStudent(Guid id);
}
