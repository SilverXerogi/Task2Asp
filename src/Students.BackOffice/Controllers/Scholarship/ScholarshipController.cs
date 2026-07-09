using Microsoft.AspNetCore.Mvc;
using Students.BackOffice.Controllers.Infrastructure;
using Students.Domain.Services;
using Students.Domain.Students;
using Students.Tools.Types.Results;

namespace Students.BackOffice.Controllers.Scholarship
{
    public class ScholarshipController(IStudentsService studentsService, IScholarshipService scholarshipService) : BaseController
    {
        [HttpPost("calculateScholarship")]
        public async Task<ActionResult<float>> CalculateTotalScholarshipAsync()
        {
            var sum = await scholarshipService.CalculateTotalScholarshipAsync();
            return sum;
        }
    }
}
