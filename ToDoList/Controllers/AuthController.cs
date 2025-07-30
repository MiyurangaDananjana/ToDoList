using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToDoList.Controllers
{
    public class AuthController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Check if the user is authenticated
            if (Session["UserName"] == null)
            {
                // Redirect to the login page if not authenticated
                filterContext.Result = new RedirectResult("~/Account/Login");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}