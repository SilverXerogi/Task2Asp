using Students.Domain.Enums;
using Students.Domain.Services;
using Students.Domain.Students;
using Students.Services.Students.Repositories;
using Students.Tools.Types;
using Students.Tools.Types.Results;

namespace Students.Services.Students;

public class StudentsService(IStudentsRepository studentsRepository) : IStudentsService
{
    public async Task<Result> SaveStudent(StudentBlank studentBlank)
    {
        DataResult<Student> validationResult = await ValidateStudentBlank(studentBlank);
        if (validationResult.IsFail(out Student student)) return validationResult.ToResult();

        await studentsRepository.SaveStudent(student);

        return Result.Success();
    }

    #region Validation

    private async Task<DataResult<Student>> ValidateStudentBlank(StudentBlank studentBlank)
    {
        DataResult<Guid?> existStudentValidationResult = await ValidateExistStudent(studentBlank);
        if (existStudentValidationResult.IsFail(out Guid? id)) return DataResult<Student>.Fail(existStudentValidationResult);

        DataResult<String> fullNameValidationResult = ValidateFullName(studentBlank);
        if (fullNameValidationResult.IsFail(out String fullName)) return DataResult<Student>.Fail(fullNameValidationResult);

        DataResult<Gender> genderValidationResult = ValidateGender(studentBlank);
        if (genderValidationResult.IsFail(out Gender gender)) return DataResult<Student>.Fail(genderValidationResult);

        DataResult<Int32> ageValidationResult = ValidateAge(studentBlank);
        if (ageValidationResult.IsFail(out Int32 age)) return DataResult<Student>.Fail(ageValidationResult);

        DataResult<Single> averageGradeValidationResult = ValidateAverageGrade(studentBlank);
        if (averageGradeValidationResult.IsFail(out Single averageGrade)) return DataResult<Student>.Fail(averageGradeValidationResult);

        DataResult<Guid> studentGroupIdValidationResult = await ValidateStudentGroupId(studentBlank);
        if (studentGroupIdValidationResult.IsFail(out Guid studentGroupId)) return DataResult<Student>.Fail(studentGroupIdValidationResult);

        String[] specialMarks = studentBlank.SpecialMarks ?? [];

        Student student = new(
            id ?? Guid.NewGuid(),
            fullName,
            gender,
            age,
            averageGrade,
            specialMarks,
            studentGroupId
        );

        return DataResult<Student>.Success(student);
    }

    private async Task<DataResult<Guid?>> ValidateExistStudent(StudentBlank studentBlank)
    {
        if (studentBlank.Id is not { } id)
            return DataResult<Guid?>.Success(null);

        Student existStudent = await GetStudent(id);

        return DataResult<Guid?>.Success(id);
    }

    private DataResult<String> ValidateFullName(StudentBlank studentBlank)
    {
        if (String.IsNullOrWhiteSpace(studentBlank.FullName))
            return DataResult<String>.Fail("Не указано ФИО студента");

        const Int32 maxFullNameLength = 255;
        if (studentBlank.FullName.Length >= maxFullNameLength)
            return DataResult<String>.Fail($"ФИО студента слишком длинное. Максимально допустимо {maxFullNameLength} символов");

        return DataResult<String>.Success(studentBlank.FullName);
    }

    private DataResult<Gender> ValidateGender(StudentBlank studentBlank)
    {
        if (studentBlank.Gender is not { } gender)
            return DataResult<Gender>.Fail("Не указан пол студента");

        if (!Enum.IsDefined(gender))
            throw new Exception($"Пол {gender} не существует");

        return DataResult<Gender>.Success(gender);
    }

    private DataResult<Int32> ValidateAge(StudentBlank studentBlank)
    {
        if (studentBlank.Age is not { } age)
            return DataResult<Int32>.Fail("Не указан возраст студента");

        if (age < 16 || age > 100)
            return DataResult<Int32>.Fail("Указан некорректный возраст студента");

        return DataResult<Int32>.Success(age);
    }

    private DataResult<Single> ValidateAverageGrade(StudentBlank studentBlank)
    {
        if (studentBlank.AverageGrade is not { } averageGrade)
            return DataResult<Single>.Fail("Не указана средняя оценка");

        if (averageGrade < 0 || averageGrade > 5)
            return DataResult<Single>.Fail("Средняя оценка должна быть от 0 до 5");

        return DataResult<Single>.Success(averageGrade);
    }

    private async Task<DataResult<Guid>> ValidateStudentGroupId(StudentBlank studentBlank)
    {
        if (studentBlank.StudentGroupId is not { } studentGroupId)
            return DataResult<Guid>.Fail("Не указана студенческая группа");

        return DataResult<Guid>.Success(studentGroupId);
    }

    #endregion Validation

    public async Task<Student> GetStudent(Guid studentId)
    {
        Student? student = await studentsRepository.GetStudent(studentId);
        if (student is null) throw new Exception($"Студент {studentId} не найден");

        return student;
    }

    public Task<Page<Student>> GetStudents(Int32 page, Int32 countInPage)
    {
        return studentsRepository.GetStudents(page, countInPage);
    }

    public Task<Page<Student>> GetStudentsByGroupId(Guid groupId, Int32 page, Int32 countInPage)
    {
        return studentsRepository.GetStudentsByGroupId(groupId, page, countInPage);
    }

    public async Task<Result> RemoveStudent(Guid id)
    {
        await studentsRepository.RemoveStudent(id);

        return Result.Success();
    }

    
}