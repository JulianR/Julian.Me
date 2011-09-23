using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Net;
using System.Text;
using System.IO;
using Soapi;
using Soapi.Parameters;

namespace Julian.Me.Web.Controllers
{
  public class ProfileController : Controller
  {

    private const string apiKey = "LxV4BxT4f0e0tGVpT0MwOg";

    public ActionResult StackOverflow()
    {

      //var result = new ApiContext(apiKey).Initialize(false)
      //  .Official
      //  .StackOverflow
      //  .Users
      //  .ById(61632)
      //  .Questions
      //  .WithBody(true)
      //  .Sort(PostSort.Creation)
      //  .Order(SortOrder.Desc)
      //  .FirstOrDefault();

      return PartialView();
    }

    private class Parameter
    {
      public string Name { get; set; }
      public object Value { get; set; }

      public Parameter(string name, object value)
      {
        this.Name = name;
        this.Value = value;
      }
    }
  }
}
