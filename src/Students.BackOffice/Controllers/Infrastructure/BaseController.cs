using Microsoft.AspNetCore.Mvc;

namespace Students.BackOffice.Controllers.Infrastructure;

public class BaseController : Controller
{
	public IActionResult ReactApp() => View("ReactApp");
}
