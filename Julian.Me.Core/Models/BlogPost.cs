﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Julian.Me.Core.Models.Base;

namespace Julian.Me.Core.Models
{
  public class BlogPost : DataModel<BlogPost>
  {
    public string Title { get; set; }

    public string Content { get; set; }

    public int Year { get; set; }

    public int Month { get; set; }

    public ICollection<Tag> Tags { get; set; }

    public DateTime CreationTime { get; set; }

    public DateTime? LastModificationTime { get; set; }
  }
}
