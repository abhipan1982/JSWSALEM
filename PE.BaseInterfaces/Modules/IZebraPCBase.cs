using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.ZPC;
using SMF.Core.DC;
using SMF.Core.Interfaces;

namespace PE.BaseInterfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IZebraPCBase : IBaseModule
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DCZebraPrinterResponse> PrintLabelAsync(DCZebraRequest dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DCZebraImageResponse> RequestPreviewForHmiAsync(DCZebraRequest dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DCZebraPrinterResponse> RequestPrinterStatus(DCZebraRequest dc);
  }
}
