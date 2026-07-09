using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Domain.Services
{
    public interface IScholarshipService
    {
        Task<float> CalculateStudentScholarshipAsync(Guid studentId);
    }
}
