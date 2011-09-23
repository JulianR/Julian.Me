using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Julian.Me.Web.Models;
using Julian.Me.Core.Data;

namespace Julian.Me.Web.Controllers
{
  [JulianMeAttribute]
  public class BlogController : Controller
  {
    //
    // GET: /Blog/

    public ActionResult Post()
    {

      var model = new BlogPostModel
      {
        Title = "Title",
        Body = "<p> Pellentesque velit ante, vulputate nec lacinia at, ultricies et elit. Maecenas est nibh, ornare sed varius bibendum, aliquam id diam. </p><p>Pellentesque blandit sagittis odio eu dignissim. Nunc vel augue ac sapien consequat imperdiet. Ut id purus justo, id lacinia magna. Maecenas dolor quam, sodales quis mattis ut, porta nec velit.</p><p> Sed euismod aliquet nisi, sit amet mattis turpis vulputate vitae. Praesent vitae mi lectus, vel vulputate dolor. Nullam porta dapibus neque id mattis. In hac habitasse platea dictumst. Morbi eleifend lacus a purus viverra ultrices. Aenean nibh odio, fermentum eu auctor nec, mattis quis lectus. Suspendisse euismod eros vitae elit tempus ornare.</p>"
      };

      return PartialView(model);
    }

    [HttpGet]
    public ActionResult Edit(int id)
    {
      using (var context = new DbDataContext())
      {
        var post = context.BlogPosts.SingleOrDefault(b => b.ID == id);

        return View(post);

      }
    }

    public ActionResult Index(string date, int? postID)
    {
      ViewBag.ViewClass = "blog";

      using (var context = new DbDataContext())
      {
        int year;

        int? month;

        var selectLastMonth = false;

        if (string.IsNullOrEmpty(date))
        {
          year = DateTime.UtcNow.Year;
          month = null;
          selectLastMonth = true;
        }
        else
        {
          var split = date.Split('-');

          if (split.Length > 1) // Get the month
          {

            year = int.Parse(split[1]);
            month = int.Parse(split[0]);
          }
          else // Year only, month will be first month of year
          {

            year = int.Parse(split[0]);

            month = null;
          }
        }

        var years = (from p in context.BlogPosts
                     select p.Year).Distinct().OrderByDescending(y => y).ToList();

        var monthsForYear = (from p in context.BlogPosts
                             where p.Year == year
                             orderby p.Month ascending
                             select p.Month).Distinct().ToList();

        if (month == null)
        {
          if (selectLastMonth)
          {
            month = (from p in context.BlogPosts
                     where p.Year == year
                     select p.Month).Max();
          }
          else
          {
            month = (from p in context.BlogPosts
                     where p.Year == year
                     select p.Month).Min();
          }
        }

        var query = (from p in context.BlogPosts
                     where (postID.HasValue && p.ID == postID.Value) || (postID == null && p.Year == year
                     && p.Month == month.Value)
                     select p);

        var postsForMonth = (from p in query
                             select new BlogPostModel
                             {
                               ID = p.ID,
                               Body = p.Content,
                               Title = p.Title,
                               CreationTime = p.CreationTime
                             }).ToList();

        var model = new BlogIndexModel
        {
          Years = years,
          Months = monthsForYear,
          Posts = postsForMonth,
          CurrentYear = year,
          CurrentMonth = month.Value
        };

        return View(model);
      }
    }

  }
}
