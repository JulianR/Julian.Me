using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Julian.Me.Core.Data;
using Julian.Me.Web.Models;

namespace Julian.Me.Web.Controllers
{
  public class ThisMemberController : Controller
  {
    public ActionResult Index()
    {
      using (var context = new DbDataContext())
      {
        var index = (from p in context.ThisMemberPages
                     where p.Name == "Index"
                     select p).SingleOrDefault();

        if (index == null)
        {
          index = new Core.Models.ThisMemberPage
          {
            Content = "Empty so far!",
            Name = "Index"
          };
        }

        return View(new ThisMemberIndexModel
        {
          Content = index.Content
        });

      }

    }
  }
}
