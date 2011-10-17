using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Julian.Me.Web.Models
{

  public class BlogIndexModel
  {
    public IEnumerable<int> Years { get; set; }

    public IEnumerable<int> Months { get; set; }

    public IEnumerable<BlogPostModel> Posts { get; set; }

    public int CurrentMonth { get; set; }

    public int CurrentYear { get; set; }
  }

  public class BlogPostModel
  {
    public int ID { get; set; }
    public string Title { get; set; }
    [AllowHtml]
    public string Body { get; set; }
    public DateTime CreationTime { get; set; }

  }
}