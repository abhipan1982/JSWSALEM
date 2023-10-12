using System;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.EVT
{
  public class DCDelayEvent : DataContractBase
  {
    public long? RawMaterialId { get; set; }

    public DateTime Date { get; set; }
  }
}
