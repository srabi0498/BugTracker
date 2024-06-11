using Microsoft.AspNetCore.Mvc;
using BugTracker.Models;
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
