using System.Threading.Tasks;
using PE.BaseInterfaces.SendOffices.EVT;
using PE.EVT.Base.Managers;
using PE.EVT.Events.Handlers;
using PE.Interfaces.Managers.EVT;
using SMF.Core.DC;
using SMF.Core.Interfaces;

namespace PE.EVT.Events.Managers
{
  public class ShiftManager : ShiftManagerBase, IShiftManager
  {
    private readonly ShiftCustomHandler _shiftCustomHandler;

    public ShiftManager(IModuleInfo moduleInfo, IMillEventManagerSendOfficeBase sendOffice, IShiftManagerSendOfficeBase shiftManagerSendOffice) : base(moduleInfo,
      sendOffice, shiftManagerSendOffice)
    {
      _shiftCustomHandler = new ShiftCustomHandler();
    }
  }
}
