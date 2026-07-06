using Goods.Services.Products.Repositories.Models;
using Npgsql;
using Students.Domain.Enums;

namespace Goods.Services.Products.Repositories.Converters;

internal static class ProductsConverter
{
    internal static Students ToProduct(this ProductDb productDb)
    {
        return new Product(
            productDb.Id,
            productDb.Category,
            productDb.Name,
            productDb.Description,
            productDb.Price
        );
    }

    internal static ProductDb ToProductDb(this NpgsqlDataReader reader)
    {
        return new ProductDb(
            reader.GetGuid(reader.GetOrdinal("id")),
            (ProductCategory)reader.GetInt32(reader.GetOrdinal("category")),
            reader.GetString(reader.GetOrdinal("name")),
            reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader.GetString(reader.GetOrdinal("description")),
            reader.GetDouble(reader.GetOrdinal("price")),
            reader.GetDateTime(reader.GetOrdinal("createddatetimeutc")),
            reader.IsDBNull(reader.GetOrdinal("modifieddatetimeutc"))
                ? null
                : reader.GetDateTime(reader.GetOrdinal("modifieddatetimeutc")),
            reader.GetBoolean(reader.GetOrdinal("isremoved"))
        );
    }

    public static ProductDb ToProductDb(this Students product)
    {
        return new ProductDb(
            product.Id, 
            product.Category,
            product.Name, 
            product.Description, 
            product.Price,
            createdDateTimeUtc: DateTime.UtcNow,
            modifiedDateTimeUtc: DateTime.UtcNow,
            isRemoved: false
        );
    }
}