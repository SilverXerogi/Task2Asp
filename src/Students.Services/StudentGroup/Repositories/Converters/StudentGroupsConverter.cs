using Npgsql;
using Students.Domain.Enums;
using Students.Domain.StudentGroup;
using Students.Services.StudentGroups.Repositories.Models;


namespace Students.Services.StudentGroups.Repositories.Converters;

internal static class StudentGroupsConverter
{
    internal static StudentGroupA ToStudentGroup(this StudentGroupDb groupDb)
    {
        return new StudentGroupA(
            groupDb.Id,
            groupDb.Name,
            groupDb.Abbreviation,
            groupDb.StartYear,
            groupDb.EndYear,
            groupDb.StudyFormat
        );
    }

    internal static StudentGroupDb ToStudentGroupDb(this NpgsqlDataReader reader)
    {
        return new StudentGroupDb(
            reader.GetGuid(reader.GetOrdinal("id")),
            reader.GetString(reader.GetOrdinal("name")),
            reader.GetString(reader.GetOrdinal("abbreviation")),
            reader.GetInt32(reader.GetOrdinal("startyear")),
            reader.GetInt32(reader.GetOrdinal("endyear")),
            (StudyFormat)reader.GetInt32(reader.GetOrdinal("studyformat")),
            reader.GetDateTime(reader.GetOrdinal("createddatetimeutc")),
            reader.IsDBNull(reader.GetOrdinal("modifieddatetimeutc"))
                ? null
                : reader.GetDateTime(reader.GetOrdinal("modifieddatetimeutc")),
            reader.GetBoolean(reader.GetOrdinal("isremoved"))
        );
    }

    public static StudentGroupDb ToStudentGroupDb(this StudentGroupA group)
    {
        return new StudentGroupDb(
            group.Id,
            group.Name,
            group.Abbr,
            group.StartYear,
            group.EndYear,
            group.StudyFormat,
            createdDateTimeUtc: DateTime.UtcNow,
            modifiedDateTimeUtc: DateTime.UtcNow,
            isRemoved: false
        );
    }
}