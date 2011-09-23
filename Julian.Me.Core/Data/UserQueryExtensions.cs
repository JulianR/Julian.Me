using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Julian.Me.Core.Models;
using Julian.Me.Core.Security;

namespace Julian.Me.Core.Data
{
  public static class UserQueryExtensions
  {
    public static User GetByCredentials(this IQueryable<User> query, string email, string password)
    {
      var user = query.Where(u => u.EmailAddress == email).SingleOrDefault();

      if (user == null)
      {
        return null;
      }

      var security = new DefaultSecurityProvider();

      var expectedHash = security.Hash(password + user.PasswordSalt);

      if (expectedHash != user.PasswordHash)
      {
        return null;
      }

      return user;

    }

    public static User GetByEmailAddress(this IQueryable<User> query, string email)
    {
      var user = query.Where(u => u.EmailAddress == email).SingleOrDefault();

      return user;
    }
  }
}
