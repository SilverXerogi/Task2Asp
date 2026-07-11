using Microsoft.AspNetCore.Mvc;
using Students.BackOffice.Controllers.Infrastructure;
using Students.Domain.Services;

namespace Students.BackOffice.Controllers.Scholarships;

[ApiController]
[Route("scholarships")]
public class ScholarshipsController(IScholarshipService scholarshipService) : BaseController
{
    [HttpGet("calculate-for-student")]
    public async Task<ActionResult<decimal>> CalculateForStudent([FromQuery] Guid studentId)
    {
        var sum = await scholarshipService.CalculateStudentScholarshipAsync(studentId);
        return Ok(sum);
    }

    [HttpGet("calculate-for-group-period")]
    public async Task<ActionResult<Decimal>> CalculateForGroupPeriod([FromQuery] Guid groupId)
    {
        var sum = await scholarshipService.CalculateTotalScholarshipForGroupPeriodAsync(groupId);
        return Ok(sum);
    }
}