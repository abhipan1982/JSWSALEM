using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PE.Core;
using PE.Interfaces.Modules;
using SMF.Core.Infrastructure;

namespace PE.CommunicationTracer.Common
{
  public static class Common
  {
    public static Dictionary<string, (string code, Type type, string name)> Modules = new()
    {
        { nameof(IHmi),                ("HMI", typeof(IHmi),                PE.Core.Modules.Hmiexe.Name) },
        { nameof(IDBAdapter),          ("DBA", typeof(IDBAdapter),          PE.Core.Modules.DBAdapter.Name) },
        { nameof(IProdManager),        ("PRD", typeof(IProdManager),        PE.Core.Modules.ProdManager.Name) },
        { nameof(IProdPlaning),        ("PPL", typeof(IProdPlaning),        PE.Core.Modules.ProdPlaning.Name) },
        { nameof(IMVHistory),          ("MVH", typeof(IMVHistory),          PE.Core.Modules.MvHistory.Name) },
        { nameof(IEvents),             ("EVT", typeof(IEvents),             PE.Core.Modules.Events.Name) },
        { nameof(IL1Adapter),          ("L1A", typeof(IL1Adapter),          PE.Core.Modules.L1Adapter.Name) },
        { nameof(ITracking),           ("TRK", typeof(ITracking),           PE.Core.Modules.Tracking.Name) },
        { nameof(IWalkingBeamFurnace), ("WBF", typeof(IWalkingBeamFurnace), PE.Core.Modules.WalkingBeamFurnace.Name) },
        { nameof(ITcpProxy),           ("TCP", typeof(ITcpProxy),           PE.Core.Modules.TcpProxy.Name) },
        { nameof(ISetup),              ("STP", typeof(ISetup),              PE.Core.Modules.Setup.Name) },
        { nameof(IRollShop),           ("RLS", typeof(IRollShop),           PE.Core.Modules.RollShop.Name) },
        { nameof(IMaintenance),        ("MNT", typeof(IMaintenance),        PE.Core.Modules.Maintenance.Name) },
        { nameof(IQuality),            ("QTY", typeof(IQuality),            PE.Core.Modules.Quality.Name) },
        { nameof(IQualityExpert),      ("QEX", typeof(IQualityExpert),      PE.Core.Modules.QualityExpert.Name) },
        { nameof(IYards),              ("YRD", typeof(IYards),              PE.Core.Modules.Yards.Name) },
        { nameof(IZebraPC),            ("ZPC", typeof(IZebraPC),            PE.Core.Modules.ZebraPrinter.Name) },
      };

    public static string GetExceptionStringFormat(Exception ex)
    {
      if (ex == null)
        return string.Empty;

      return string.Format("Message = {0}\nStackTrace = {1}\n\r", RemoveLineBreak(ex.Message), ex.StackTrace) + GetAllInnerException(ex);
    }

    public static string RemoveLineBreak(string originString)
    {
      return Regex.Replace(originString, @"[\n\r]", "");
    }

    public static string GetAllInnerException(Exception ex)
    {
      if (ex.InnerException == null)
        return string.Empty;

      var tempEx = ex.InnerException;

      var stringBuilder = new StringBuilder();
      int i = 1;
      while (tempEx != null)
      {
        stringBuilder.AppendLine(string.Format("=================== InnerException[{0}] ===================", i++));
        stringBuilder.AppendLine(string.Format("Message = {0}\nStackTrace = {1}\n", RemoveLineBreak(tempEx.Message), tempEx.StackTrace));

        tempEx = tempEx.InnerException;
      }

      return stringBuilder.ToString();
    }
  }
}
