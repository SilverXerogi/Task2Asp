using Goods.BackOffice.Controllers.Infrastructure;
using Goods.Domain.Products;
using Goods.Domain.Services;
using Goods.Tools.Types.Results;
using Microsoft.AspNetCore.Mvc;

namespace Goods.BackOffice.Controllers.Products;

public class ProductsController(IProductsService productsService) : BaseController
{
	[HttpGet("/products")]
    public IActionResult Index() => ReactApp();

	[HttpPost("products/save")]
	public Task<Result> SaveProducts([FromBody] ProductBlank productBlank)
	{
		return productsService.SaveProduct(productBlank);
	}

	[HttpGet("products/get-page")]
	public Task<Page<Product>> GetProductsPage([FromQuery] Int32 page, [FromQuery] Int32 count)
	{
		return productsService.GetProducts(page, count);
	}

	[HttpGet("products/get-by-id")]
	public Task<Product> GetProduct([FromQuery] Guid id)
	{
		return productsService.GetProduct(id);
	}

	[HttpGet("products/remove")]
	public Task<Result> RemoveProduct([FromQuery] Guid id)
	{
		return productsService.RemoveProduct(id);
	}
}
