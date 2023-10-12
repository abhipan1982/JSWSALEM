using System.Threading.Tasks;
using PE.BaseInterfaces.Managers.EVT;
using PE.EVT.Base.Module.Communication;
using PE.Interfaces.Managers.EVT;
using SMF.Core.DC;

namespace PE.EVT.Events.Communication
{
  public class ExternalAdapterHandler : ModuleBaseExternalAdapterHandler
  {
    private readonly IShiftManager _shiftManager;

    public ExternalAdapterHandler(IEventCatalogueManagerBase delayCatalogueManager,
      IDelayManagerBase delayManager,
      IDelayProcessManagerBase delayProcessManager,
      IShiftManager shiftManager,
      IEventCatalogueCategoryManagerBase delayCatalogueCategoryManager,
      IEventGroupsCatalogueManagerBase delayGroupsCatalogueManager,
      IMillEventManagerBase millEventManager,
      ICrewManagerBase crewManager) : base(delayCatalogueManager,
      delayManager,
      delayProcessManager,
      shiftManager,
      delayCatalogueCategoryManager,
      delayGroupsCatalogueManager,
      millEventManager,
      crewManager)
    {
      _shiftManager = shiftManager;
    }

  }
}
