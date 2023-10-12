using System;
using System.Threading.Tasks;
using SMF.Core.Notification;

namespace PE.Helpers
{
  public static class TaskHelper
  {
    /// <summary>
    ///   FireAndForget method calling with logging exception
    /// </summary>
    /// <param name="action"></param>
    public static void FireAndForget(Action action, string errorMessage = null)
    {
      Task.Run(() =>
      {
        try
        {
          action.Invoke();
        }
        catch (Exception ex)
        {
          NotificationController.LogException(ex,
            errorMessage ?? $"Something went wrong while executing method: {action.Method?.Name}");
        }
      });
    }
  }
}
