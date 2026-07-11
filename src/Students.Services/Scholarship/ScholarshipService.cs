using Goods.Services.Products.Repositories;
using Students.Domain.Services;
using Students.Services.StudentGroups.Repositories;
using Students.Services.Students.Repositories;
using Students.Services.Students.Repositories.Converters;
using System.Text.RegularExpressions;

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

        Int32 course = group.GetCourse(DateTime.UtcNow);
        if (course < 1) course = 1;
        Int32 age = student.GetAge(DateTime.UtcNow);
        return CalculateScholarshipForCourse(student.AverageGrade, course, age);
    }

    public async Task<float> CalculateTotalScholarshipForGroupPeriodAsync(Guid studentGroupId)
    {
        var group = await studentGroupsRepository.GetStudentGroup(studentGroupId);
        if (group == null)
            throw new Exception($"Группа с ID {studentGroupId} не найдена");

        var allStudentsDb = await studentsRepository.GetAllStudentsAsync();
        var allStudents = allStudentsDb
            .Select(db => StudentsConverter.ToStudent(db))
            .ToList();
        var groupStudents = allStudents.Where(s => s.StudentGroupId == studentGroupId).ToList();

        Int32 totalCourses = group.GetTotalCourses();
        if (totalCourses < 1) totalCourses = 1;

        float totalSum = 0;

        foreach (var student in groupStudents)
        {
            if (student.AverageGrade < 4.0)
                continue;

            float studentTotal = 0;
            for (Int32 course = 1; course <= totalCourses; course++)
            {
                // Рассчитываем дату начала этого курса
                // Курс 1 начинается в StartDateTime
                // Курс 2 начинается через 1 год
                // Курс N начинается через (N-1) лет
                DateTime courseStartDate = group.StartDateTime.AddYears(course - 1);

                Int32 ageAtCourse = student.GetAge(courseStartDate);
                if (ageAtCourse < 1) ageAtCourse = 1;

                studentTotal += CalculateScholarshipForCourse(student.AverageGrade, course, ageAtCourse);
            }

            totalSum += studentTotal;
        }


        return totalSum;

    }
    private static float CalculateScholarshipForCourse(float averageGrade, Int32 course, Int32 age)
    {
        return averageGrade
               * 500
               * (float)Math.Sqrt((Double)course)
               * (float)Math.Sqrt((Double)age);
    }


}