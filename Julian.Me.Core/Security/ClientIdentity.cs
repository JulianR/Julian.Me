using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using Julian.Me.Core.Data;
using Julian.Me.Core.Models;
using Julian.Me.Core.Security;

namespace Julian.Me.Core.Security
{
  public class ClientIdentity : IIdentity
  {

    public ClientIdentity(string email, string password, UserAuthenticationMode mode)
    {
      if (mode == UserAuthenticationMode.UsernameAndPassword)
      {
        Login(email, password);
      }
      else
      {
        LoginByUsernameOnly(email);
      }
    }

    private void LoginByUsernameOnly(string email)
    {
      using (var context = new DbDataContext())
      {
        var user = context.Users.GetByEmailAddress(email);

        if (user != null)
        {
          this.IsAuthenticated = true;
          FillFields(user);
        }

      }
    }

    private void Login(string email, string password)
    {

      if (string.IsNullOrEmpty(password)) throw new ArgumentNullException("password");

      using (var context = new DbDataContext())
      {
        var user = context.Users.GetByCredentials(email, password);

        if (user != null)
        {
          this.IsAuthenticated = true;
          FillFields(user);
        }

      }
    }

    private void FillFields(User user)
    {
      this.Name = user.EmailAddress;
      this.UserID = user.ID;
      this.IsAdmin = user.IsAdmin;
    }

    public string AuthenticationType
    {
      get { return "username"; }
    }

    public bool IsAuthenticated
    {
      get;
      private set;
    }

    public string Name
    {
      get;
      private set;
    }

    public int UserID
    {
      get;
      private set;
    }

    public bool IsAdmin
    {
      get;
      private set;
    }
  }
}