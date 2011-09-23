using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Julian.Me.Core.Security
{
  public interface ISecurityProvider
  {
    string GetSalt();

    string Hash(string original);
  }
}
