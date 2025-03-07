using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourtCase.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(Session["UserId"]==null || Session["UserNumber"] == null)
            {
                filterContext.Result = new RedirectResult("~/Home/CompUserLogin");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}