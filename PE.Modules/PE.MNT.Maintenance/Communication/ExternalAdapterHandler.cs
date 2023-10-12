using PE.BaseInterfaces.Managers.MNT;
using PE.MNT.Base.Module.Communication;

namespace PE.MNT.Maintenance.Communication
{
  public class ExternalAdapterHandler : ModuleBaseExternalAdapterHandler
  {
    public ExternalAdapterHandler(IEquipmentAccumulationBaseManager equipmentAccumulationBaseManager,
      IEquipmentBaseManager equipmentBaseManager, IEquipmentGroupBaseManager equipmentGroupBaseManager)
      : base(equipmentAccumulationBaseManager, equipmentBaseManager, equipmentGroupBaseManager)
    {
    }
  }
}
