﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Julian.Me.Core.Models;
using System.Data.Entity.Infrastructure;

namespace Julian.Me.Core.Data
{
  public class DbDataContext : DbContext
  {
    public DbSet<User> Users { get; set; }
    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<ThisMemberPage> ThisMemberPages { get; set; }

    public static event Action<DbDataContext, IEnumerable<DbEntityEntry>> SavingChanges;

    public override int SaveChanges()
    {
      var entries = this.ChangeTracker.Entries();

      if (SavingChanges != null)
      {
        SavingChanges(this, entries);
      }

      return base.SaveChanges();
    }

  }
}
