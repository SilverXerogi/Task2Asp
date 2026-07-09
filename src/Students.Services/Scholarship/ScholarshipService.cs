using Goods.Services.Products.Repositories;
using Students.Domain.Services;
using Students.Domain.Students;
using Students.Services.StudentGroups.Repositories;
using Students.Services.Students.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Services.Scholarship
{
    public class ScholarshipService(IStudentsRepository studentsRepository, IStudentGroupsRepository studentGroupsRepository) : IScholarshipService
    {
        public async Task<float> CalculateTotalScholarshipAsync()
        {
            var students = await studentsRepository.GetAllStudentsAsync();
        var groups = await studentGroupsRepository.GetAllGroupsAsync();

        var groupsDict = groups.ToDictionary(g => g.Id);

        int currentYear = DateTime.UtcNow.Year;
        float totalSum = 0;

            foreach (var student in students)
            {
                if (groupsDict.TryGetValue(student.StudentGroupId, out var group))
                {
                    int course = currentYear - group.StartYear + 1;
                    if (course< 1) course = 1;

                        float scholarship = student.AverageGrade;
                        scholarship = scholarship* 500 * (float) Math.Sqrt((double) course);

                        totalSum += scholarship;
                }
            }

        return totalSum;
        }
    }
}
