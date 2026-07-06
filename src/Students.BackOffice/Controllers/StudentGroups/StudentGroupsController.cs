using Microsoft.AspNetCore.Mvc;
using Students.BackOffice.Controllers.Infrastructure;
using Students.Domain.Services;
using Students.Domain.StudentGroup;
using Students.Tools.Types.Results;

namespace Students.BackOffice.Controllers.StudentGroups;

public class StudentGroupsController(IStudentsGroupsService studentGroupsService) : BaseController
{
    [HttpGet("/student-groups")]
    public IActionResult Index() => ReactApp();

    [HttpPost("student-groups/save")]
    public Task<Result> SaveStudentGroup([FromBody] StudentGroupABlank groupBlank)
    {
        return studentGroupsService.SaveStudentGroup(groupBlank);
    }

    [HttpGet("student-groups/get-page")]
    public Task<Page<StudentGroupA>> GetStudentGroupsPage([FromQuery] Int32 page, [FromQuery] Int32 count)
    {
        return studentGroupsService.GetStudentsGroups(page, count);
    }

    [HttpGet("student-groups/get-by-id")]
    public Task<StudentGroupA> GetStudentGroup([FromQuery] Guid id)
    {
        return studentGroupsService.GetStudentGroup(id);
    }

    [HttpGet("student-groups/remove")]
    public Task<Result> RemoveStudentGroup([FromQuery] Guid id)
    {
        return studentGroupsService.RemoveStudentsGroup(id);
    }
}