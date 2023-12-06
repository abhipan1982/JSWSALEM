using System.Threading.Tasks;
using System.Timers;
using System;
using PE.Interfaces.Managers.DBA;
using SMF.Module.Core;
using SMF.Core.Notification;

namespace PE.DBA.DataBaseAdapter.Module
{
  public class Worker : BaseWorker
  {
    private bool _isAvailableForCallTimerMethod = true;

    #region managers

    private readonly IL3DBCommunicationManager _l3DbCommunicationManager;

    #endregion

    #region ctor

    public Worker(IL3DBCommunicationManager l3DbCommunicationManager)
    {
      _l3DbCommunicationManager = l3DbCommunicationManager;
    }

    #endregion

    #region module calls

    public override async void TimerMethod(object sender, ElapsedEventArgs e)
    {
      if (_isAvailableForCallTimerMethod)
        await TransferBatchData();
    }
    #endregion

    #region private
    private async Task TransferBatchData()
    {
      try
      {
        _isAvailableForCallTimerMethod = false;

        //Added by Abhishek       
        await _l3DbCommunicationManager.TransferBatchDataFromTransferTableToAdapterAsync();
        await _l3DbCommunicationManager.UpdateBatchDataWithTimeoutAsync();
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
