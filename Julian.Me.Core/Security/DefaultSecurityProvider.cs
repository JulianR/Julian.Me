using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Julian.Me.Core.Security
{
  public class DefaultSecurityProvider : ISecurityProvider
  {
    private readonly RNGCryptoServiceProvider _cryptoService = new RNGCryptoServiceProvider();

    public string GetSalt()
    {
      var data = new byte[32];

      _cryptoService.GetBytes(data);

      return Convert.ToBase64String(data);
    }

    public string Hash(string original)
    {
      return BitConverter.ToString(SHA256.Create().ComputeHash(Encoding.Default.GetBytes(original))).Replace("-", "");
    }
  }
}
