using Students.Domain.Services;
using Students.Services.Students;
using Students.Services.Students.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Students.Services.StudentGroups;
using Students.Services.StudentGroups.Repositories;
using Goods.Services.Products.Repositories;
namespace Students.Services;

public static class ServiceConfigurator
{
    public static IServiceCollection AddServices(this IServiceCollection collection)
    {
        collection.AddSingleton<IStudentsService, StudentsService>();
        collection.AddSingleton<IStudentsRepository, StudentsRepository>();

        collection.AddSingleton<IStudentsGroupsService, StudentGroupsService>();
        collection.AddSingleton<IStudentGroupsRepository, StudentGroupsRepository>();

        return collection;
    }
}
