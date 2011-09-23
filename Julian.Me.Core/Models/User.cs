using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Julian.Me.Core.Models.Base;

namespace Julian.Me.Core.Models
{
  public class User : DataModel<User>
  {
    public string EmailAddress { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PasswordHash { get; set; }

    public string PasswordSalt { get; set; }

    public bool IsAdmin { get; set; }
  }
}
