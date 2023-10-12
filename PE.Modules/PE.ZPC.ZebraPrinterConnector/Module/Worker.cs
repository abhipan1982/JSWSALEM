using PE.BaseInterfaces.Managers.ZPC;
using PE.ZPC.Base.Module;

namespace PE.ZPC.ZebraPrinterConnector.Module
{
  public class Worker : WorkerBase
  {
    #region managers

    #endregion

    #region properties

    #endregion

    #region ctor
    public Worker(IZebraConnectionManagerBase zebraConnectionManager) : base(zebraConnectionManager)
    {
    }
    #endregion
  }
}
