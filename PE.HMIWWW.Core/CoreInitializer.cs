using Serilog;
using SMF.Module.Limit;
using SMF.Module.Notification;

namespace PE.HMIWWW.Core
{
  public static class CoreInitializer
  {
    public static void Initialize()
    {
      NotificarionCore.Register(Log.Logger);
      LimitController.Reload();
    }
  }
}
