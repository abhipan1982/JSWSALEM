using System;
using System.Threading.Tasks;
using System.Timers;
using PE.BaseInterfaces.Managers.DBA;
using SMF.Core.Notification;
using SMF.Module.Core;

namespace PE.DBA.Base.Module
{
  public class WorkerBase : BaseWorker
  {
    private bool _isAvailableForCallTimerMethod = true;
    
    #region managers

    private readonly IL3DBCommunicationBaseManager _l3DbCommunicationManager;

    #endregion

    #region ctor

    public WorkerBase(IL3DBCommunicationBaseManager l3DbCommunicationManager)
    {
      _l3DbCommunicationManager = l3DbCommunicationManager;
    }

    #endregion

    #region module calls

    public override async void TimerMethod(object sender, ElapsedEventArgs e)
    {
      if(_isAvailableForCallTimerMethod)
        await TransferWorkOrders();
    }
    #endregion

    #region private
    private async Task TransferWorkOrders()
    {
      try
      {
        _isAvailableForCallTimerMethod = false;
        
        await _l3DbCommunicationManager.TransferWorkOrderDataFromTransferTableToAdapterAsync();
        await _l3DbCommunicationManager.UpdateWorkOrdesWithTimeoutAsync();
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
      }
      finally
      {
        _isAvailableForCallTimerMethod = true;
      }
    }
    #endregion
  }
}
