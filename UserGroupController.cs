using BugTracker.DAO;
using BugTracker.Models;
using BugTracker.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            return View();
        }

        [HttpGet]
        public JsonResult Create(string groupname, string groupcode, int id)
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
                if (id == 0)
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
                else
                {
                    //update part

                    UserGroup oldGrp = _context.UserGroup.Where(x => x.UserGroupID == id).FirstOrDefault();
                    if (oldGrp == null)
                    {
                        return Json(new
                        {
                            Success = false,
                            Message = "User Group Information Not Found"
                        });
                    }
                    else
                    {
                        oldGrp.UserGroupName = groupname;
                        oldGrp.UserGroupCode = groupcode;
                        _context.SaveChanges();
                        return Json(new
                        {
                            Success = true,
                            Message = "User Group Information Updated sucessfully...."

                        });
                    }
                }
            }
        }

        [HttpGet]
        public JsonResult Delete(int id)
        {
            UserGroup oldGroup = _context.UserGroup.Where(x => x.UserGroupID == id).FirstOrDefault();
            if (oldGroup == null)
            {
                return Json(new
                {
                    Success = false,
                    Message = "User Group Information Not Found..."
                });
            }
            else
            {
                oldGroup.Status = 0;
                _context.SaveChanges();


                return Json(new
                {
                    Success = true,
                    Message = "User Group Information Deleted Successfully..."
                });
            }

        }
        [HttpGet]
        public JsonResult GetAllData(string grpname, string grpcode)
        {
            List<UserGroupVM> ugs = _context
                            .UserGroup
                            .Where(x => x.Status == 1
                            && (string.IsNullOrEmpty(grpname) || x.UserGroupName.Contains(grpname))
                            && (string.IsNullOrEmpty(grpcode) || x.UserGroupCode.Contains(grpcode))
                            )
                            .Select(s => new UserGroupVM
                            {
                                UserGroupID = s.UserGroupID,
                                UserGroupName = s.UserGroupName,
                                UserGroupCode = s.UserGroupCode,
                                ActivateDate = s.ActivateDate.Value.ToString("yyyy/MM/dd")
                            })
                            .ToList();

            return Json(new
            {
                Success = true,
                Data = ugs
            });
        }
        [HttpGet]
        public JsonResult GetByID(int id)
        {
            UserGroup ug = _context.UserGroup.Where(x => x.UserGroupID == id).FirstOrDefault();
            if (ug == null)
            {
                return Json(new
                {
                    Success = false,
                    Message = "USer Group  not Found"
                });
            }
            else
            {
                return Json(new
                {
                    Success = true,
                    Data = ug
                });
            }
        }
    }
}