using PE.BaseInterfaces.Managers.RLS;
using PE.RLS.Base.Module.Communication;

namespace PE.RLS.RollShop.Communication
{
  public class ExternalAdapterHandler : ModuleBaseExternalAdapterHandler
  {
    public ExternalAdapterHandler(IRollBaseManager rollManager, IRollTypeBaseManager rollTypeManager, IGrooveTemplateBaseManager grooveTemplateManager,
      IRollSetBaseManager rollSetManager, ICassetteTypeBaseManager cassetteTypeManager, ICassetteBaseManager cassetteManager,
      IRollChangeBaseManager rollChangeManager, IRollSetHistoryBaseManager rollSetHistoryManager, ICustomRollGroovesBaseManager customRollGrooves)
      : base(rollManager, rollTypeManager, grooveTemplateManager, rollSetManager, cassetteTypeManager, cassetteManager, rollChangeManager,
          rollSetHistoryManager, customRollGrooves)
    { }
  }
}
