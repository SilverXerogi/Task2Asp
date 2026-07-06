using Goods.Services.Products.Repositories;
using Students.Domain.Enums;
using Students.Domain.Services;
using Students.Domain.StudentGroup;
using Students.Tools.Types.Results;

namespace Students.Services.StudentGroups;

public class StudentGroupsService(IStudentGroupsRepository studentGroupsRepository) : IStudentsGroupsService
{
    public async Task<Result> SaveStudentGroup(StudentGroupABlank groupBlank)
    {
        DataResult<StudentGroupA> validationResult = await ValidateStudentGroupBlank(groupBlank);
        if (validationResult.IsFail(out StudentGroupA group)) return validationResult.ToResult();

        await studentGroupsRepository.SaveStudentGroup(group);

        return Result.Success();
    }

    #region Validation

    private async Task<DataResult<StudentGroupA>> ValidateStudentGroupBlank(StudentGroupABlank groupBlank)
    {
        DataResult<Guid?> existGroupValidationResult = await ValidateExistStudentGroup(groupBlank);
        if (existGroupValidationResult.IsFail(out Guid? id)) return DataResult<StudentGroupA>.Fail(existGroupValidationResult);

        DataResult<String> nameValidationResult = ValidateGroupName(groupBlank);
        if (nameValidationResult.IsFail(out String name)) return DataResult<StudentGroupA>.Fail(nameValidationResult);

        DataResult<String> abbreviationValidationResult = ValidateAbbreviation(groupBlank);
        if (abbreviationValidationResult.IsFail(out String abbreviation)) return DataResult<StudentGroupA>.Fail(abbreviationValidationResult);

        DataResult<Int32> startYearValidationResult = ValidateStartYear(groupBlank);
        if (startYearValidationResult.IsFail(out Int32 startYear)) return DataResult<StudentGroupA>.Fail(startYearValidationResult);

        DataResult<Int32> endYearValidationResult = ValidateEndYear(groupBlank, startYear);
        if (endYearValidationResult.IsFail(out Int32 endYear)) return DataResult<StudentGroupA>.Fail(endYearValidationResult);

        DataResult<StudyFormat> studyFormatValidationResult = ValidateStudyFormat(groupBlank);
        if (studyFormatValidationResult.IsFail(out StudyFormat studyFormat)) return DataResult<StudentGroupA>.Fail(studyFormatValidationResult);

        StudentGroupA group = new(
            id ?? Guid.NewGuid(),
            name,
            abbreviation,
            startYear,
            endYear,
            studyFormat
        );

        return DataResult<StudentGroupA>.Success(group);
    }

    private async Task<DataResult<Guid?>> ValidateExistStudentGroup(StudentGroupABlank groupBlank)
    {
        if (groupBlank.Id is not { } id)
            return DataResult<Guid?>.Success(null);

        StudentGroupA existGroup = await GetStudentGroup(id);

        return DataResult<Guid?>.Success(id);
    }

    private DataResult<String> ValidateGroupName(StudentGroupABlank groupBlank)
    {
        if (String.IsNullOrWhiteSpace(groupBlank.Name))
            return DataResult<String>.Fail("Не указано название группы");

        const Int32 maxNameLength = 255;
        if (groupBlank.Name.Length >= maxNameLength)
            return DataResult<String>.Fail($"Название группы слишком длинное. Максимально допустимо {maxNameLength} символов");

        return DataResult<String>.Success(groupBlank.Name);
    }

    private DataResult<String> ValidateAbbreviation(StudentGroupABlank groupBlank)
    {
        if (String.IsNullOrWhiteSpace(groupBlank.Abbr))
            return DataResult<String>.Fail("Не указана аббревиатура группы");

        const Int32 maxAbbreviationLength = 50;
        if (groupBlank.Abbr.Length >= maxAbbreviationLength)
            return DataResult<String>.Fail($"Аббревиатура группы слишком длинная. Максимально допустимо {maxAbbreviationLength} символов");

        return DataResult<String>.Success(groupBlank.Abbr);
    }

    private DataResult<Int32> ValidateStartYear(StudentGroupABlank groupBlank)
    {
        if (groupBlank.StartYear is not { } startYear)
            return DataResult<Int32>.Fail("Не указан год начала обучения");

        if (startYear < 1900 || startYear > DateTime.Now.Year + 1)
            return DataResult<Int32>.Fail("Указан некорректный год начала обучения");

        return DataResult<Int32>.Success(startYear);
    }

    private DataResult<Int32> ValidateEndYear(StudentGroupABlank groupBlank, Int32 startYear)
    {
        if (groupBlank.EndYear is not { } endYear)
            return DataResult<Int32>.Fail("Не указан год окончания обучения");

        if (endYear <= startYear)
            return DataResult<Int32>.Fail("Год окончания должен быть больше года начала");

        if (endYear > DateTime.Now.Year + 10)
            return DataResult<Int32>.Fail("Указан некорректный год окончания обучения");

        return DataResult<Int32>.Success(endYear);
    }

    private DataResult<StudyFormat> ValidateStudyFormat(StudentGroupABlank groupBlank)
    {
        if (groupBlank.StudyFormat is not { } studyFormat)
            return DataResult<StudyFormat>.Fail("Не указан формат обучения");

        if (!Enum.IsDefined(studyFormat))
            throw new Exception($"Формат обучения {studyFormat} не существует");

        return DataResult<StudyFormat>.Success(studyFormat);
    }

    #endregion Validation

    public async Task<StudentGroupA> GetStudentGroup(Guid groupId)
    {
        StudentGroupA? group = await studentGroupsRepository.GetStudentGroup(groupId);
        if (group is null) throw new Exception($"Студенческая группа {groupId} не найдена");

        return group;
    }

    public Task<Page<StudentGroupA>> GetStudentsGroups(Int32 page, Int32 countInPage)
    {
        return studentGroupsRepository.GetStudentGroups(page, countInPage);
    }

    public async Task<Result> RemoveStudentsGroup(Guid id)
    {
        await studentGroupsRepository.RemoveStudentGroup(id);

        return Result.Success();
    }
}