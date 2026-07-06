namespace Goods.Domain.Products;

public class Product
{
    public Guid Id { get; }
    public ProductCategory Category { get; }
    public String Name { get; }
    public String? Description { get; }
    public Decimal Price { get; }
    public Boolean IsRemoved { get; }

    public Product(Guid id, ProductCategory category, String name, String? description, Decimal price, Boolean isRemoved)
    {
        Id = id;
        Category = category;
        Name = name;
        Description = description;
        Price = price;
        IsRemoved = isRemoved;
    }
}