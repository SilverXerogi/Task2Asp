using Microsoft.AspNetCore.Mvc;

namespace Goods.BackOffice.Controllers.Infrastructure;

public class BaseController : Controller
{
	public IActionResult ReactApp() => View("ReactApp");
}
