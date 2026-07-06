using Goods.Domain.Products;

namespace Goods.Services.Products.Repositories;

public interface IProductsRepository
{
    Task SaveProduct(Product productBlank);
    Task<Product?> GetProduct(Guid id);
    Task<Product?> GetProduct(String name);
    Task<Page<Product>> GetProducts(Int32 page, Int32 count);
    Task RemoveProduct(Guid id);
}