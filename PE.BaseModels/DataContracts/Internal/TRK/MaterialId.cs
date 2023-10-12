using System;
using System.Runtime.Serialization;
using PE.BaseModels.DataContracts.Internal._Base;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  public class MaterialId : IdBase<long>
  {
    private MaterialId() { }
    public MaterialId(long id) : base(id) { }

  }
}
