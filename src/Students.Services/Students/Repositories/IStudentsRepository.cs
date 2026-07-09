using Students.Domain.Students;
using Students.Services.Students.Repositories.Models;


namespace Students.Services.Students.Repositories;

public interface IStudentsRepository
{
    Task SaveStudent(Student student);
    Task<Student?> GetStudent(Guid id);
    Task<Page<Student>> GetStudents(Int32 page, Int32 count);
    Task<Page<Student>> GetStudentsByGroupId(Guid groupId, Int32 page, Int32 count);
    Task RemoveStudent(Guid id);
    Task<List<StudentDb>> GetAllStudentsAsync();
}