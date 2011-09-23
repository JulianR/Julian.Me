using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Julian.Me.Core.Models.Base
{

  public interface IAuditable : IDataModel
  {
    int CreatedByID { get; set; }

    DateTime CreationTime { get; set; }

    int? LastModifiedByID { get; set; }

    DateTime? LastModificationTime { get; set; }

    User CreatedBy { get; set; }

    User LastModifiedBy { get; set; }
  }

  public abstract class AuditedDataModel<TModel> : DataModel<TModel>, IAuditable where TModel : AuditedDataModel<TModel>
  {
    public int CreatedByID { get; set; }

    public DateTime CreationTime { get; set; }

    public int? LastModifiedByID { get; set; }

    public DateTime? LastModificationTime { get; set; }

    public User CreatedBy { get; set; }

    public User LastModifiedBy { get; set; }
  }
}
