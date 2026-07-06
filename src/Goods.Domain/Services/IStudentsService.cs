using Goods.Tools.Types.Results;
using Students.Domain.Students;

namespace Students.Domain.Services;

public interface IStudentsService
{
    Task<Result> SaveStudent(StudentBlank studentBlank);
    Task<Student> GetStudent(Guid id);
    Task<Page<Student>> GetStudents(Int32 page, Int32 count);
    Task<Page<Student>> GetStudentsByGroupId(Int32 page, Int32 count);
    Task<Result> RemoveStudent(Guid id);
}
