using System.Runtime.Serialization;
using PE.BaseModels.DataContracts.Internal._Base;
using PE.BaseModels.DataContracts.Internal.PRM;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  [DataContract]
  public class DCChargeMaterialOnFurnaceExit : IdBase<long>
  {
    private DCChargeMaterialOnFurnaceExit() { }
    public DCChargeMaterialOnFurnaceExit(long id):base(id) { }
  }
}
