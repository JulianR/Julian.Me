using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Julian.Me.Web.Models;
using Julian.Me.Web.Security;
using Julian.Me.Core.Security;

namespace Julian.Me.Web.Controllers
{
  public class AdminController : Controller
  {
    public ActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Login(LoginModel model, string returnUrl)
    {
      if (ModelState.IsValid)
      {
        var identity = new ClientIdentity(model.EmailAddress, model.Password, UserAuthenticationMode.UsernameAndPassword);

        if (!identity.IsAuthenticated)
        {
          ModelState.AddModelError("", "The user name or password provided is incorrect.");
        }

        var principal = new ClientPrincipal(identity);

        FormsAuthentication.SetAuthCookie(principal.Identity.Name, false);

        Session["Principal"] = principal;

        HttpContext.User = principal;

        if (Url.IsLocalUrl(returnUrl))
        {
          return Redirect(returnUrl);
        }
        else
        {
          return RedirectToAction("Index", "Home");
        }
      }

      return View(model);
    }

    public ActionResult LogOff()
    {
      FormsAuthentication.SignOut();
      return RedirectToAction("Index", "Home");
    }
  }
}
