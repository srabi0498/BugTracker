using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers
{
    public class TextController : Controller
    {
        public IActionResult Album()
        {
            return View();
        }
    }
}
