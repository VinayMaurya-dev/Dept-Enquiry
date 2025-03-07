using CourtCase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourtCase.Controllers
{
    public class DropdownBinderController : Controller
    {
        // GET: DropdownBinder
        public ActionResult Index()
        {
            return View();
        }

		public JsonResult fillDropdown(dropdownBinderModel obj)
		{
			var data = obj.getDataToBind();
			return Json(data.ToArray(), JsonRequestBehavior.AllowGet);
		}
        public JsonResult fillDropdownData(dropdownBinderModel obj)
        {
            var data = obj.getDataToBindData();
            return Json(data.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult fillDropdownLoginData(dropdownBinderModel obj)
        {
            var data = obj.BindLoginData();
            return Json(data.ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}