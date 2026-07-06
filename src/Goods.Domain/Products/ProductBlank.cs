namespace Goods.Domain.Products;

public class ProductBlank
{
	public Guid? Id { get; set; }
	public ProductCategory? Category { get; set; }
	public String? Name { get; set; }
	public String? Description { get; set; }
	public Decimal? Price { get; set; }
}
