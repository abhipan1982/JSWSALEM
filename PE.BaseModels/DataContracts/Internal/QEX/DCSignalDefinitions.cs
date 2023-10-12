using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.QEX
{
  public class DCSignalDefinitions : DataContractBase
  {
    [DataMember] public List<DCSignalDefinition> SignalDefinitions { get; set; }
  }
}
