using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.QTY
{
  public class DCDefectGroup : DataContractBase
  {
    [DataMember] public long Id { get; set; }

    [DataMember] public string DefectGroupName { get; set; }

    [DataMember] public string DefectGroupCode { get; set; }
  }
}
