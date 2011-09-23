using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Julian.Me.Core.Models.Base;

namespace Julian.Me.Core.Models
{
  public class Tag : DataModel<Tag>
  {
    public string Name { get; set; }
    public ICollection<BlogPost> BlogPosts { get; set; }
  }
}
