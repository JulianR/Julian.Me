using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Julian.Me.Web.Models;
using Julian.Me.Core.Data;
using Julian.Me.Core.Models;

namespace Julian.Me.Web.Controllers
{
  [JulianMeAttribute]
  public class BlogController : Controller
  {
    [HttpGet]
    [JulianMe(RequiresAdmin = true)]
    public ActionResult Edit(int id)
    {
      using (var context = new DbDataContext())
      {
        var post = context.BlogPosts.Where(b => b.ID == id)
          .Select(p => new BlogPostModel
          {
            ID = p.ID,
            Body = p.Content,
            Title = p.Title
          }).SingleOrDefault();

        return View(post);

      }
    }

    [HttpPost]
    [JulianMe(RequiresAdmin = true)]
    public ActionResult Edit(BlogPostModel model)
    {
      using (var context = new DbDataContext())
      {
        var post = context.BlogPosts.SingleOrDefault(p => p.ID == model.ID);

        post.Content = model.Body;
        post.LastModificationTime = DateTime.Now;
        post.Title = model.Title;

        context.SaveChanges();

        return RedirectToAction("Index", "Blog");

      }
    }

    [HttpGet]
    [JulianMe(RequiresAdmin = true)]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost]
    [JulianMe(RequiresAdmin = true)]
    public ActionResult New(BlogPostModel model)
    {
      using (var context = new DbDataContext())
      {
        var creationTime = DateTime.Now;
        var post = new BlogPost
        {
          Content = model.Body,
          Title = model.Title,
          CreationTime = creationTime,
          Month = creationTime.Month,
          Year = creationTime.Year,
        };

        context.BlogPosts.Add(post);

        context.SaveChanges();

        return RedirectToAction("Index");

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
                     select (int?)p.Month).Max() ?? 1;
          }
          else
          {
            month = (from p in context.BlogPosts
                     where p.Year == year
                     select (int?)p.Month).Min() ?? 1;
          }
        }

        var query = (from p in context.BlogPosts
                     where (postID.HasValue && p.ID == postID.Value) || (postID == null && p.Year == year
                     && p.Month == month.Value)
                     orderby p.CreationTime descending
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
