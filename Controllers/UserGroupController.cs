using Microsoft.AspNetCore.Mvc;
using BugTracker.DAO;
using BugTracker.Models;
using System.Collections.Generic;
using BugTracker.Models.ViewModel;
using System.Linq;
using System;
using BugTracker.Models.ViewModel;

namespace BugTracker.Controllers
{
    public class UserGroupController : Controller
    {

        ApplicationDbContext _context;
        public UserGroupController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<UserGroupVM> ugs = _context
                .UserGroup
                .Where(x => x.Status == 1)
                .Select(s => new UserGroupVM
                {
                    UserGroupID = s.UserGroupID,
                    UserGroupName = s.UserGroupName,
                    UserGroupCode = s.UserGroupCode,
                    ActivateDate = s.ActivateDate.Value.ToString("yyyy/MM/dd")
                })
                .ToList();

            return View(ugs);
        }

        [HttpGet]
        public JsonResult Create(string groupname, string groupcode)
        {
            if (string.IsNullOrEmpty(groupname))
            {
                return Json(new
                {
                    Success = false,
                    Message = "Enter User Group Name..."
                });

            }
            else if (string.IsNullOrEmpty(groupcode))
            {
                return Json(new
                {
                    Success = false,
                    Message = "Enter User Group Code..."
                });
            }
            else
            {
                UserGroup oldCode = _context.UserGroup
                     .Where(x => x.UserGroupCode == groupcode)
                     .FirstOrDefault();

                if (oldCode == null)
                {
                    UserGroup ug = new UserGroup()
                    {
                        UserGroupName = groupname,
                        UserGroupCode = groupcode,
                        Status = 1,
                        ActivateDate = DateTime.Now
                    };

                    _context.UserGroup.Add(ug);
                    _context.SaveChanges();

                    return Json(new
                    {
                        Success = true,
                        Message = "User Group Added Successfully..."
                    });
                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "User Group Code Already Exist..."
                    });
                }
            }
        }
    }
}
