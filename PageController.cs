using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers
{
    public class PageController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
