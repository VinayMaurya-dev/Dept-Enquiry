using CourtCase.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourtCase.Filters;
using CourtCase.DAL;
using CourtCases.Controllers;

namespace CourtCase.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        DBLayer db = new DBLayer();  
        [HttpGet]  
        public ActionResult ViewEnquiry(string ComplaintNo)
        { 
            ComplaintDetail obj = new ComplaintDetail();
            obj.ComplaintNo = UrlEncoding.Decrypt(ComplaintNo);
            obj.Action = "selectUserDetails"; 
            DataTable dt = db.ShowUserDetails(obj); 
            if (dt.Rows.Count > 0)
            { 
                ViewBag.data = dt;
            }
            else
            { 
                ViewBag.data = null;
            } 
            return View(dt);
        } 
    }
}