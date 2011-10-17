using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Julian.Me.Core.Security
{
  public static class Client
  {
    public static ClientPrincipal User
    {
      get
      {
        if (HttpContext.Current != null)
        {
          return HttpContext.Current.User as ClientPrincipal;
        }

        return null;

      }
    }
  }
}
