using Students.Tools.Utils.Json;
using Npgsql;
using Students.Domain.Enums;
using Students.Domain.Students;
using Students.Services.Students.Repositories.Models;
namespace Students.Services.Students.Repositories.Converters;

internal static class StudentsConverter
{
    private static readonly IJsonSerializer _jsonSerializer = new TextJsonSerializer();

    internal static Student ToStudent(this StudentDb studentDb)
    {
        String[] specialMarks = _jsonSerializer.Deserialize<String[]>(studentDb.SpecialMarksJson) ?? [];

        return new Student(
            studentDb.Id,
            studentDb.FullName,
            studentDb.Gender,
            studentDb.Age,
            studentDb.AverageGrade,
            specialMarks,
            studentDb.StudentGroupId
        );
    }

    internal static StudentDb ToStudentDb(this NpgsqlDataReader reader)
    {
        return new StudentDb(
            reader.GetGuid(reader.GetOrdinal("id")),
            reader.GetString(reader.GetOrdinal("fullname")),
            (Gender)reader.GetInt32(reader.GetOrdinal("gender")),
            reader.GetInt32(reader.GetOrdinal("age")),
            reader.GetFloat(reader.GetOrdinal("averagegrade")),
            reader.IsDBNull(reader.GetOrdinal("specialmarks"))
                ? "[]"
                : reader.GetString(reader.GetOrdinal("specialmarks")),
            reader.GetGuid(reader.GetOrdinal("studentgroupid")),
            reader.GetDateTime(reader.GetOrdinal("createddatetimeutc")),
            reader.IsDBNull(reader.GetOrdinal("modifieddatetimeutc"))
                ? null
                : reader.GetDateTime(reader.GetOrdinal("modifieddatetimeutc")),
            reader.GetBoolean(reader.GetOrdinal("isremoved"))
        );
    }

    public static StudentDb ToStudentDb(this Student student)
    {
        String specialMarksJson = _jsonSerializer.Serialize(student.SpecialMarks);

        return new StudentDb(
            student.Id,
            student.FullName,
            student.Gender,
            student.Age,
            student.AverageGrade,
            specialMarksJson,
            student.StudentGroupId,
            createdDateTimeUtc: DateTime.UtcNow,
            modifiedDateTimeUtc: DateTime.UtcNow,
            isRemoved: false
        );
    }
}