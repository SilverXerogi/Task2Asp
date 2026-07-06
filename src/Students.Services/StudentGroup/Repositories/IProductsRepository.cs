using Goods.Domain.Products;

namespace Goods.Services.Products.Repositories;

public interface IProductsRepository
{
    Task SaveProduct(Students productBlank);
    Task<Students?> GetProduct(Guid id);
    Task<Students?> GetProduct(String name);
    Task<Page<Students>> GetProducts(Int32 page, Int32 count);
    Task RemoveProduct(Guid id);
}