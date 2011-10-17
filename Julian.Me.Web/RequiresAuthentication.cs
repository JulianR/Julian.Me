using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Julian.Me.Core.Security;

namespace Julian.Me.Web
{
  public class JulianMeAttribute : ActionFilterAttribute
  {
    public JulianMeAttribute()
    {

    }

    public bool RequiresAdmin { get; set; }

    public override void OnResultExecuted(ResultExecutedContext filterContext)
    {
      filterContext.HttpContext.Session["CurrentViewClass"] = filterContext.Controller.ViewBag.ViewClass;
    }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      if (RequiresAdmin)
      {
        UrlHelper url = new UrlHelper(filterContext.RequestContext);

        if (Client.User == null || !Client.User.Identity.IsAuthenticated || !Client.User.IsAdmin)
        {
          filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
          filterContext.HttpContext.Response.End();
        }
      }
      filterContext.Controller.ViewBag.PreviousViewClass = (filterContext.HttpContext.Session["CurrentViewClass"] as string) ?? "white";

      if (filterContext.Controller.ViewBag.ViewClass == null)
      {
        filterContext.Controller.ViewBag.ViewClass = filterContext.Controller.GetType().Name.Replace("Controller", "").ToLower();
      }
    }
  }
}