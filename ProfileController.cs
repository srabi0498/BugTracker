using BugTracker.DAO;
using BugTracker.Models;
using BugTracker.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BugTracker.Controllers
{
    public class ProfileController : Controller
    {
        ApplicationDbContext _context;
        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            HttpContext.Session.Remove("USER_INFO");
            return View();
        }

        [HttpPost]
        public JsonResult SignIn([FromBody]LoginVM vm)
        {
            Users usr = _context.Users
                 .Where(x => x.Status == 1
                 && x.Username == vm.Username
                  && x.Username == vm.password
                  )
                 .FirstOrDefault();
            if (usr == null )
            {
                return Json(new
                {
                    success = false,
                    message = "User Not Found"
                });
            }
            else
            {

                HttpContext.Session.SetString("USER_INFO", usr.UsersID.ToString());

                return Json(new
                {
                    success = true,
                });
            }
        }
    }
}
