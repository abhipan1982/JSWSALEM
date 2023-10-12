using System.Collections.Generic;
using System.Runtime.Serialization;
using PE.BaseModels.DataContracts.Internal._Base;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.PRM
{
  public class DCWorkOrderBlock : IdBase<long>
  {
    private DCWorkOrderBlock() { }
    public DCWorkOrderBlock(long id) : base(id) { }

    public bool IsBlock { get; set; }
  }
}
