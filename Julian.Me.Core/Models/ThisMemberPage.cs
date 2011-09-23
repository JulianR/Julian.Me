using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Julian.Me.Core.Models.Base;

namespace Julian.Me.Core.Models
{
  public class ThisMemberPage : DataModel<ThisMemberPage>
  {
    public string Name { get; set; }

    public string Content { get; set; }
  }
}
