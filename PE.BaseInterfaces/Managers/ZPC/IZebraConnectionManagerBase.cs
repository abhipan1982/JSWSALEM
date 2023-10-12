using System.Collections.Generic;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.ZPC;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.ZPC
{
  public interface IZebraConnectionManagerBase : IManagerBase
  {
    Task<DCZebraImageResponse> RequestPreviewForHmiAsync(DCZebraRequest dcZebraRequest);
    Task<DCZebraPrinterResponse> RequestPrinterStatusAsync(DCZebraRequest dcZebraRequest);
    Task<DCZebraPrinterResponse> PrintLabelAsync(DCZebraRequest dcZebraRequest);
    void SetSupportedAssets(Dictionary<int, long> assets);
    Task CheckPrintersStatusAsync();
  }
}
