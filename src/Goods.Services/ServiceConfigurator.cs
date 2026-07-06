using Goods.Domain.Services;
using Goods.Services.Products;
using Goods.Services.Products.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Goods.Services;

public static class ServiceConfigurator
{
    public static IServiceCollection AddServices(this IServiceCollection collection)
    {
        collection.AddSingleton<IProductsService, ProductsService>();
        collection.AddSingleton<IProductsRepository, ProductsRepository>();

        return collection;
    }
}
