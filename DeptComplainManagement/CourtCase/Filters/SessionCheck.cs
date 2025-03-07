using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CourtCase.Filters
{
    public class SessionCheck : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            if (session != null && session["UserId"] == null || session["UserNumber"] == null || session["LastLogin"] == null || session["RoleId"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                                { "Controller", "Home" },
                                { "Action", "CompUserLogin" }

                    });
            }
        }
    }
}