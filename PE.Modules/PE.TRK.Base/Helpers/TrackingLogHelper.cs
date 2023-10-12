using Microsoft.Extensions.Logging;
using PE.TRK.Base.Exceptions;
using SMF.Core.Notification;

namespace PE.TRK.Base.Helpers
{
  public static class TrackingLogHelper
  {
    public static void LogTrackingInformation(string message)
    {
      NotificationController.Info(message);
    }
    
    public static void LogTrackingException(TrackingException ex)
    {
      NotificationController.LogException(ex);
    }
  }
}
