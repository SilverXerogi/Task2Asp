
using Students.Domain.Students;
using Students.Services.Students.Repositories.Converters;
using Students.Services.Students.Repositories.Models;
using Students.Services.Students.Repositories.Queries;
using Students.Tools.Types;
using Students.Tools.Utils;
using static Students.Tools.Utils.NumberUtils;

namespace Students.Services.Students.Repositories;

public class StudentsRepository : IStudentsRepository
{
    public Task SaveStudent(Student student)
    {
        StudentDb studentDb = student.ToStudentDb();

        return DatabaseUtils.ExecuteAsync(
            Queries.StudentSql.Students_Save,
            parameters =>
            {
                parameters.AddWithValue("@id", studentDb.Id);
                parameters.AddWithValue("@fullName", studentDb.FullName);
                parameters.AddWithValue("@gender", (Int32)studentDb.Gender);
                parameters.AddWithValue("@age", studentDb.Age);
                parameters.AddWithValue("@averageGrade", studentDb.AverageGrade);
                parameters.AddWithValue("@specialMarks", studentDb.SpecialMarksJson);
                parameters.AddWithValue("@studentGroupId", studentDb.StudentGroupId);
                parameters.AddWithValue("@createdDateTimeUtc", studentDb.CreatedDateTimeUtc);
                parameters.AddWithValue("@modifiedDateTimeUtc", (Object?)studentDb.ModifiedDateTimeUtc ?? DBNull.Value);
                parameters.AddWithValue("@isRemoved", studentDb.IsRemoved);
            }
        );
    }

    public async Task<Student?> GetStudent(Guid id)
    {
        StudentDb? studentDb = await DatabaseUtils.GetAsync<StudentDb?>(
            Queries.StudentSql.Students_GetById,
            parameters =>
            {
                parameters.AddWithValue("@id", id);
            },
            reader => reader.ToStudentDb()
        );

        return studentDb?.ToStudent();
    }

    public async Task<Page<Student>> GetStudents(Int32 page, Int32 count)
    {
        (Int32 offset, Int32 limit) = NormalizeRange(page, count);

        Page<StudentDb> pageDb = await DatabaseUtils.GetPageAsync(
            Queries.StudentSql.Students_GetPage,
            parameters =>
            {
                parameters.AddWithValue("@offset", offset);
                parameters.AddWithValue("@limit", limit);
            },
            reader => reader.ToStudentDb()
        );

        return pageDb.Convert(studentDb => studentDb.ToStudent());
    }

    public async Task<Page<Student>> GetStudentsByGroupId(Guid groupId, Int32 page, Int32 count)
    {
        (Int32 offset, Int32 limit) = NormalizeRange(page, count);

        Page<StudentDb> pageDb = await DatabaseUtils.GetPageAsync(
            Queries.StudentSql.Students_GetByGroupId,
            parameters =>
            {
                parameters.AddWithValue("@studentGroupId", groupId);
                parameters.AddWithValue("@offset", offset);
                parameters.AddWithValue("@limit", limit);
            },
            reader => reader.ToStudentDb()
        );

        return pageDb.Convert(studentDb => studentDb.ToStudent());
    }


    public Task RemoveStudent(Guid id)
    {
        Console.WriteLine($"=== repo: Received id = {id} ===");
        return DatabaseUtils.ExecuteAsync(
            Queries.StudentSql.Students_Remove,
            parameters =>
            {
                parameters.AddWithValue("@id", id);
                parameters.AddWithValue("@modifiedDateTimeUtc", DateTime.UtcNow);
            }
        );
    }
    public async Task<List<StudentDb>> GetAllStudentsAsync()
    {
        var page = await DatabaseUtils.GetPageAsync(
            Queries.StudentSql.Students_GetAll,
            parameters =>
            {
                parameters.AddWithValue("@offset", 1);
                parameters.AddWithValue("@limit", 1000);
            },
            reader => StudentsConverter.ToStudentDb(reader)
            );
        return page.Values.ToList();
    }

}
