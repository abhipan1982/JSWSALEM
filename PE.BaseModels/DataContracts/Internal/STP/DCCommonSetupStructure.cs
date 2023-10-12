using System.Collections.Generic;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.STP
{
  public class DCCommonSetupStructure : DataContractBase
  {
    public DCCommonSetupStructure()
    {
      SetupProperties = new List<SetupProperty>();
    }

    [DataMember] public long TelegramId { get; set; }

    [DataMember] public short Port { get; set; }

    [DataMember] public long? StructureId { get; set; }

    [DataMember] public string IpAddress { get; set; }

    [DataMember] public List<SetupProperty> SetupProperties { get; set; }
  }

  public class SetupProperty
  {
    [DataMember] public long PropertyId { get; set; }

    public string PropertyName { get; set; }
    public string PropertyValue { get; set; }
    public string PropertyType { get; set; }
  }
}
