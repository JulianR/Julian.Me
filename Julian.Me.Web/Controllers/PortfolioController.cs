using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Julian.Me.Web.Controllers
{
  [JulianMeAttribute]
  public class PortfolioController : Controller
  {
    //
    // GET: /Portfolio/

    public ActionResult Index()
    {
      return View();
    }

  }
}
