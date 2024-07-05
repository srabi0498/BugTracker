using Microsoft.AspNetCore.Mvc;
namespace BugTracker.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

