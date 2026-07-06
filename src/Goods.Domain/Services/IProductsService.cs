using Goods.Domain.Products;
using Goods.Tools.Types.Results;

namespace Goods.Domain.Services;

public interface IProductsService
{
    Task<Result> SaveProduct(ProductBlank productBlank);
    Task<Product> GetProduct(Guid id);
    Task<Page<Product>> GetProducts(Int32 page, Int32 count);
    Task<Result> RemoveProduct(Guid id);
}
