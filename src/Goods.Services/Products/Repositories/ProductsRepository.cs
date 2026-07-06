using Goods.Domain.Products;
using Goods.Services.Products.Repositories.Converters;
using Goods.Services.Products.Repositories.Models;
using Goods.Services.Products.Repositories.Queries;
using Goods.Tools.Utils;
using static Goods.Tools.Utils.NumberUtils;

namespace Goods.Services.Products.Repositories;

public class ProductsRepository : IProductsRepository
{
	public Task SaveProduct(Product product)
    {
        ProductDb productDb = product.ToProductDb();

        return DatabaseUtils.ExecuteAsync(
            Sql.Products_Save,
            parameters =>
            {
                parameters.AddWithValue("@id", productDb.Id);
                parameters.AddWithValue("@category", (Int32)productDb.Category);
                parameters.AddWithValue("@name", productDb.Name);
                parameters.AddWithValue("@description", (Object?)productDb.Description ?? DBNull.Value);
                parameters.AddWithValue("@price", productDb.Price);
                parameters.AddWithValue("@createdDateTimeUtc", productDb.CreatedDateTimeUtc);
                parameters.AddWithValue("@modifiedDateTimeUtc", (Object?)productDb.ModifiedDateTimeUtc ?? DBNull.Value);
                parameters.AddWithValue("@isRemoved", productDb.IsRemoved);
            }
        );
    }

    public async Task<Product?> GetProduct(Guid id)
    {
        ProductDb? productDb = await DatabaseUtils.GetAsync<ProductDb?>(
            Sql.Products_GetById,
            parameters =>
            {
                parameters.AddWithValue("@id", id);
            },
            reader => reader.ToProductDb()
        );

        return productDb?.ToProduct();
    }

    public async Task<Product?> GetProduct(String name)
    {
        ProductDb? productDb = await DatabaseUtils.GetAsync<ProductDb?>(
            Sql.Products_GetByName,
            parameters =>
            {
                parameters.AddWithValue("@name", name);
            },
            reader => reader.ToProductDb()
        );

        return productDb?.ToProduct();
    }

    public async Task<Page<Product>> GetProducts(Int32 page, Int32 count)
    {
        (Int32 offset, Int32 limit) = NormalizeRange(page, count);

        Page<ProductDb> pageDb = await DatabaseUtils.GetPageAsync(
            Sql.Products_GetPage,
            parameters =>
            {
                parameters.AddWithValue("@offset", offset);
                parameters.AddWithValue("@limit", limit);
            },
            reader => reader.ToProductDb()
        );

        return pageDb.Convert(productDb => productDb.ToProduct());
    }
    
    public Task RemoveProduct(Guid id)
    {
        return DatabaseUtils.ExecuteAsync(
            Sql.Products_Remove,
            parameters =>
            {
                parameters.AddWithValue("@id", id);
                parameters.AddWithValue("@modifiedDateTimeUtc", DateTime.UtcNow);
            }
        );
    }
}
