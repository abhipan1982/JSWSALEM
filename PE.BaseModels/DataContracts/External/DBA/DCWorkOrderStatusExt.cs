using System.Runtime.Serialization;
using PE.BaseModels.DataContracts.Internal.DBA;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.External.DBA
{
  public class DCWorkOrderStatusExt : BaseExternalTelegram
  {
    [DataMember] public long Counter { get; set; }

    [DataMember] public dynamic Status { get; set; }

    [DataMember] public string BackMsg { get; set; }

    public override int ToExternal(DataContractBase dc)
    {
      Counter = (dc as DCWorkOrderStatus).Counter;
      Status = (dc as DCWorkOrderStatus).Status;
      BackMsg = (dc as DCWorkOrderStatus).BackMessage;
      return 0;
    }
  }
}
