using Goods.Services.Products.Repositories;
using Students.Domain.Services;
using Students.Services.StudentGroups.Repositories;
using Students.Services.Students.Repositories;

namespace Students.Services.Scholarship;

public class ScholarshipService(
    IStudentsRepository studentsRepository,
    IStudentGroupsRepository studentGroupsRepository
) : IScholarshipService
{
    public async Task<float> CalculateStudentScholarshipAsync(Guid studentId)
    {
        var student = await studentsRepository.GetStudent(studentId);
        if (student == null)
            throw new Exception($"Студент с ID {studentId} не найден");
        if (student.AverageGrade < 4.0)
            return 0;
        var group = await studentGroupsRepository.GetStudentGroup(student.StudentGroupId);
        if (group == null)
            throw new Exception($"Группа с ID {student.StudentGroupId} не найдена");

        int currentYear = DateTime.UtcNow.Year;
        int course = currentYear - group.StartYear + 1;
        if (course < 1) course = 1;

        float scholarship = student.AverageGrade
                              * 500
                              * (float)Math.Sqrt((double)course);

        return scholarship;
    }
}