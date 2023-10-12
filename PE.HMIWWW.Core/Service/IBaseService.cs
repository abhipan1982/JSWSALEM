using SMF.Core.DC;

namespace PE.HMIWWW.Core.Service
{
  public interface IBaseService
  {
    //void InitService(Logger Logger);
    void SendHmiOperationRequest(HmiInitiator hmiInitiator, string moduleName, int operation);
  }
}
