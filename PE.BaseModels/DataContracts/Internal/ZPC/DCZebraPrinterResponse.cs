using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.ZPC
{
  public class DCZebraPrinterResponse : DataContractBase
  {
    [DataMember]
    public string PrinterResponse { get; set; }
  }
}
