using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.ZPC;
using SMF.Core.Communication;

namespace PE.BaseInterfaces.SendOffices.TRK
{
  public interface ITrackingLabelPrinterSendOffice
  {
    Task<SendOfficeResult<DCZebraPrinterResponse>> SendLabelPrintRequest(DCZebraRequest request);
  }
}
