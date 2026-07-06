using Students.Domain.StudentGroup;

namespace Goods.Services.Products.Repositories;

public interface IStudentGroupsRepository
{
    Task SaveStudentGroup(StudentGroupA group);
    Task<StudentGroupA?> GetStudentGroup(Guid id);
    Task<Page<StudentGroupA>> GetStudentGroups(Int32 page, Int32 count);
    Task RemoveStudentGroup(Guid id);
}