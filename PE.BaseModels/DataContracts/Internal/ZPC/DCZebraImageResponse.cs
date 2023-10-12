using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.ZPC
{
  public class DCZebraImageResponse : DataContractBase
  {
    [DataMember] public string ImageBase64 { get; set; }
  }
}
