
using Students.Domain.StudentGroup;
using Students.Tools.Types.Results;

namespace Students.Domain.Services
{
    public interface IStudentsGroupsService
    {
        Task<Result>SaveStudentGroup(StudentGroupABlank studentGroupABlank);
        Task<StudentGroupA> GetStudentGroup(Guid id);
        Task<Page<StudentGroupA>> GetStudentsGroups(Int32 page, Int32 count);
        Task<Result> RemoveStudentsGroup(Guid id);
    }
}
