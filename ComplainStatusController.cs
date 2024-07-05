using BugTracker.DAO;
using BugTracker.Models;
using BugTracker.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BugTracker.Controllers
{
    public class ComplainStatusController : Controller
    {
        ApplicationDbContext _context;
        public ComplainStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult StatusForm()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetAllData(string statusname)
        {
            List<ComplainStatusVM>
    cs = _context
    .ComplainStatus
    .Where(x => x.IsActive == true
    && (string.IsNullOrEmpty(statusname) || x.StatusName.Contains(statusname))

    )
    .Select(s => new ComplainStatusVM
    {
        ComplainStatusId = s.ComplainStatusId,
        StatusName = s.StatusName,
        StatusCode = s.StatusCode,
        IsActive = s.IsActive,
        CreatedBy = s.CreatedBy,
        CreatedDate = s.CreatedDate,


    })
    .ToList();

            return Json(new
            {
                Success = true,
                Data = cs
            });
        }
        [HttpGet]

        public JsonResult Create(string STATUSNAME, string STATUSCODE, int CREATEDBY, string CREATEDDATE, int id)
        {
            if (string.IsNullOrEmpty(STATUSNAME))
            {
                return Json(new
                {
                    sucess = false,
                    Message = "Enter all Data"
                });
            }
            else
            {
                if (id == 0)
                {

                    ComplainStatus oldCode = _context.ComplainStatus
                    .Where(x => x.StatusName == STATUSNAME && x.IsActive == true)
                    .FirstOrDefault();

                    if (oldCode == null)
                    {
                        ComplainStatus cp = new ComplainStatus()
                        {
                            StatusName = STATUSNAME,
                            StatusCode = STATUSCODE,
                            CreatedBy = CREATEDBY,
                            IsActive = true,
                            CreatedDate = DateTime.Parse(CREATEDDATE),


                        };
                        _context.ComplainStatus.Add(cp);
                        _context.SaveChanges();
                        return Json(new
                        {
                            Success = true,
                            Message = "Complain Status Added Successfully..."
                        });
                    }

                    else
                    {
                        return Json(new
                        {
                            Success = false,
                            Message = "Complain Status Already Exist..."
                        });
                    }
                }

                else
                {
                    ComplainStatus oldGrp = _context.ComplainStatus.Where(x => x.ComplainStatusID == id).FirstOrDefault();
                    if (oldGrp == null)
                    {
                        return Json(new
                        {
                            Success = false,
                            Message = "Complain Status Not Found"
                        });
                    }
                    else
                    {
                        oldGrp.StatusName = STATUSNAME;
                        oldGrp.StatusCode = STATUSCODE;
                        oldGrp.CreatedBy = CREATEDBY;
                        oldGrp.CreatedDate = DateTime.Parse(CREATEDDATE);

                        _context.SaveChanges();
                        return Json(new
                        {
                            Success = true,
                            Message = "Complain Status Information Update..."

                        });
                    }
                }

            }


        }

        [HttpGet]
        public JsonResult Delete(int id)
        {
            ComplainStatus oldGroup = _context.ComplainStatus.Where(x => x.ComplainStatusID == id).FirstOrDefault();
            if (oldGroup == null)
            {
                return Json(new
                {
                    Success = false,
                    Message = "Complain Status Not Found..."
                });
            }
            else
            {
                oldGroup.IsActive = false;
                _context.SaveChanges();


                return Json(new
                {
                    Success = true,
                    Message = "Complain Status Deleted Successfully..."
                });
            }

        }


        [HttpGet]

        public JsonResult GetByID(int id)
        {
            ComplainStatus ci = _context.ComplainStatus.Where(x => x.ComplainStatusID == id).FirstOrDefault();
            if (ci == null)
            {
                return Json(new
                {
                    Success = false,
                    Message = "Complain Status not Found"
                });
            }
            else
            {
                return Json(new
                {
                    Success = true,
                    Data = ci
                });
            }
        }
    }
}