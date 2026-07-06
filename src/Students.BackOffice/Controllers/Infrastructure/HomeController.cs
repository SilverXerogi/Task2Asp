using Microsoft.AspNetCore.Mvc;

namespace Students.BackOffice.Controllers.Infrastructure;

public class HomeController : BaseController
{
	[HttpGet("/")]
	public IActionResult Index() => ReactApp();
}
