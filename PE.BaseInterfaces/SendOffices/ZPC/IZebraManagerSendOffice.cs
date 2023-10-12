using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.HMI;
using SMF.Core.Communication;
using SMF.Core.DC;

namespace PE.BaseInterfaces.SendOffices.ZPC
{
  public interface IZebraManagerSendOffice
  {
    Task<SendOfficeResult<DataContractBase>> SendCurrentLabelPrintingAsync(DCPrintingLabel dataToSend);
    Task<SendOfficeResult<DataContractBase>> UpdatePrinterStatusAsync(DCIntElementStatusUpdate printerStatus);
  }
}
