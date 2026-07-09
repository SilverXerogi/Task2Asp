using Students.BackOffice.Controllers.Infrastructure;
using Students.Domain.Students;
using Students.Domain.Services;
using Students.Tools.Types;
using Students.Tools.Types.Results;
using Microsoft.AspNetCore.Mvc;

namespace Students.BackOffice.Controllers.Students;

public class StudentsController(IStudentsService studentsService) : BaseController
{
    [HttpGet("/students")]
    public IActionResult Index() => ReactApp();

    [HttpPost("students/save")]
    public Task<Result> SaveStudent([FromBody] StudentBlank studentBlank)
    {
        return studentsService.SaveStudent(studentBlank);
    }

    [HttpGet("students/get-page")]
    public Task<Page<Student>> GetStudentsPage([FromQuery] Int32 page, [FromQuery] Int32 count)
    {
        return studentsService.GetStudents(page, count);
    }

    [HttpGet("students/get-by-group")]
    public Task<Page<Student>> GetStudentsByGroupId([FromQuery] Guid groupId, [FromQuery] Int32 page, [FromQuery] Int32 count)
    {
        return studentsService.GetStudentsByGroupId(groupId, page, count);
    }

    [HttpGet("students/get-by-id")]
    public Task<Student> GetStudent([FromQuery] Guid studentId)
    {
        return studentsService.GetStudent(studentId);
    }

    [HttpPost("students/remove")]
    public async Task<Result> Remove([FromQuery] Guid studentId)
    {
        Console.WriteLine($"=== CONTROLLER: Received id = {studentId} ===");
        Console.WriteLine($"=== CONTROLLER: id.ToString() = {studentId.ToString()} ===");

        var result = await studentsService.RemoveStudent(studentId);

        Console.WriteLine($"=== CONTROLLER: Result = {result.IsSuccess} ===");

        return result;
    }

}