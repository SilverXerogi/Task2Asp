using Goods.Services.Products.Repositories;
using Students.Domain.StudentGroup;
using Students.Services.StudentGroups.Repositories.Converters;
using Students.Services.StudentGroups.Repositories.Models;
using Students.Services.StudentGroups.Repositories.Queries;
using Students.Services.Students.Repositories.Converters;
using Students.Services.Students.Repositories.Models;
using Students.Tools.Types;
using Students.Tools.Utils;
using static Students.Tools.Utils.NumberUtils;

namespace Students.Services.StudentGroups.Repositories;

public class StudentGroupsRepository : IStudentGroupsRepository
{
    public Task SaveStudentGroup(StudentGroupA group)
    {
        StudentGroupDb groupDb = group.ToStudentGroupDb();

        return DatabaseUtils.ExecuteAsync(
            StudentGroupsSql.Save,
            parameters =>
            {
                parameters.AddWithValue("@id", groupDb.Id);
                parameters.AddWithValue("@name", groupDb.Name);
                parameters.AddWithValue("@abbreviation", groupDb.Abbreviation);
                parameters.AddWithValue("@startYear", groupDb.StartYear);
                parameters.AddWithValue("@endYear", groupDb.EndYear);
                parameters.AddWithValue("@studyFormat", (Int32)groupDb.StudyFormat);
                parameters.AddWithValue("@createdDateTimeUtc", groupDb.CreatedDateTimeUtc);
                parameters.AddWithValue("@modifiedDateTimeUtc", (Object?)groupDb.ModifiedDateTimeUtc ?? DBNull.Value);
                parameters.AddWithValue("@isRemoved", groupDb.IsRemoved);
            }
        );
    }

    public async Task<StudentGroupA?> GetStudentGroup(Guid id)
    {
        StudentGroupDb? groupDb = await DatabaseUtils.GetAsync<StudentGroupDb?>(
            StudentGroupsSql.GetById,
            parameters =>
            {
                parameters.AddWithValue("@id", id);
            },
            reader => reader.ToStudentGroupDb()
        );

        return groupDb?.ToStudentGroup();
    }

    public async Task<Page<StudentGroupA>> GetStudentGroups(Int32 page, Int32 count)
    {
        (Int32 offset, Int32 limit) = NormalizeRange(page, count);

        Page<StudentGroupDb> pageDb = await DatabaseUtils.GetPageAsync(
            StudentGroupsSql.GetPage,
            parameters =>
            {
                parameters.AddWithValue("@offset", offset);
                parameters.AddWithValue("@limit", limit);
            },
            reader => reader.ToStudentGroupDb()
        );

        return pageDb.Convert(groupDb => groupDb.ToStudentGroup());
    }

    public Task RemoveStudentGroup(Guid id)
    {
        return DatabaseUtils.ExecuteAsync(
            StudentGroupsSql.Remove,
            parameters =>
            {
                parameters.AddWithValue("@id", id);
                parameters.AddWithValue("@modifiedDateTimeUtc", DateTime.UtcNow);
            }
        );
    }
    public async Task<List<StudentGroupDb>> GetAllGroupsAsync()
    {
        var page = await DatabaseUtils.GetPageAsync(
            Queries.StudentGroupsSql.GetAll,
            parameters =>
            {
                parameters.AddWithValue("@offset", 1);
                parameters.AddWithValue("@limit", 1000);
            },
            reader => StudentGroupsConverter.ToStudentGroupDb(reader)
            );
        return page.Values.ToList();
    }
}