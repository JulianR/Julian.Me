using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace Julian.Me.Web.Security
{
  public class ClientPrincipal : IPrincipal
  {
    private  ClientIdentity identity;
    
    public ClientPrincipal(ClientIdentity identity)
    {
      this.identity = identity;
    }

    public IIdentity Identity
    {
      get
      {
        return this.identity;
      }
    }

    public bool IsInRole(string role)
    {
      return false;
    }

    public bool IsAdmin
    {
      get
      {
        return this.identity.IsAdmin;
      }
    }

  }
}