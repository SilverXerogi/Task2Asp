using Goods.Tools.Types.Results;
using Students.Domain.StudentGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
