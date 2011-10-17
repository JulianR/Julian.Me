using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Julian.Me.Web.Models;
using Julian.Me.Core.Security;

namespace Julian.Me.Web.Controllers
{
  [JulianMeAttribute]
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult About()
    {
      return View();
    }

    
  }
}
