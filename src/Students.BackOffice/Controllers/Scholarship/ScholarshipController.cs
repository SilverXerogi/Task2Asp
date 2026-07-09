using Microsoft.AspNetCore.Mvc;
using Students.BackOffice.Controllers.Infrastructure;
using Students.Domain.Services;

namespace Students.BackOffice.Controllers.Scholarships;

[ApiController]
[Route("scholarships")]
public class ScholarshipsController(IScholarshipService scholarshipService) : BaseController
{
    [HttpGet("calculate")]
    public async Task<ActionResult<decimal>> CalculateForStudent([FromQuery] Guid studentId)
    {
        var sum = await scholarshipService.CalculateStudentScholarshipAsync(studentId);
        return Ok(sum);
    }
}