using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Julian.Me.Core.Models.Base
{
  public class DataModel<T> : IDataModel where T : DataModel<T>
  {
    public int ID { get; set; }
  }
}
