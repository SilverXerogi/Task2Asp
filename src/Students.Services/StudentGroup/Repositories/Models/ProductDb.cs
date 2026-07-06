using Students.Domain.Enums;

namespace Goods.Services.Products.Repositories.Models;

public class ProductDb
{
    public Guid Id { get; set; }
    public ProductCategory Category { get; set; }
    public String Name { get; set; }
    public String? Description { get; set; }
    public Double Price { get; set; }
    public DateTime CreatedDateTimeUtc { get; set; }
    public DateTime? ModifiedDateTimeUtc { get; set; }
    public Boolean IsRemoved { get; set; }

    public ProductDb(
        Guid id,
        ProductCategory category,
        String name,
        String? description,
        Double price,
        DateTime createdDateTimeUtc,
        DateTime? modifiedDateTimeUtc,
        Boolean isRemoved
    )
    {
        Id = id;
        Category = category;
        Name = name;
        Description = description;
        Price = price;
        CreatedDateTimeUtc = createdDateTimeUtc;
        ModifiedDateTimeUtc = modifiedDateTimeUtc;
        IsRemoved = isRemoved;
    }
}