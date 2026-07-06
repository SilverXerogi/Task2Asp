using Students.Domain.Services;
using Students.Services.Students;
using Students.Services.Students.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Students.Services;

public static class ServiceConfigurator
{
    public static IServiceCollection AddServices(this IServiceCollection collection)
    {
        collection.AddSingleton<IStudentsService, StudentsService>();
        collection.AddSingleton<IStudentsRepository, StudentsRepository>();

        return collection;
    }
}
