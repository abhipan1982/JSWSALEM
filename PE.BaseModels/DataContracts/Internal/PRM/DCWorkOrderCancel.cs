using System.Collections.Generic;
using System.Runtime.Serialization;
using PE.BaseModels.DataContracts.Internal._Base;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.PRM
{
  public class DCWorkOrderCancel : IdBase<long>
  {
    private DCWorkOrderCancel() { }
    public DCWorkOrderCancel(long id) : base(id) { }

    public bool IsCancel { get; set; }
  }
}
