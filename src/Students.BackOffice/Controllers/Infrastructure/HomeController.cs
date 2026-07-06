using Microsoft.AspNetCore.Mvc;

namespace Goods.BackOffice.Controllers.Infrastructure;

public class HomeController : BaseController
{
	[HttpGet("/")]
	public IActionResult Index() => ReactApp();
}
