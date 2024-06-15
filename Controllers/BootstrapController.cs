using BugTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;



namespace BugTracker.Controllers
{
    public class BootstrapController : Controller
    {
        public IActionResult Album()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
