using System.Collections.Generic;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.STP
{
  public class DCSetupValidationResult : DataContractBase
  {
    [DataMember] public bool IsCalculationValid { get; set; }

    [DataMember] public bool IsRollDiametersValid { get; set; }

    [DataMember] public List<string> MissedRollDiameters { get; set; }
  }
}
