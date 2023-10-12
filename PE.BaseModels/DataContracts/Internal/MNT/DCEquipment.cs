using System;
using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.MNT
{
  public class DCEquipment : DataContractBase
  {
    [DataMember]
    public long EquipmentId
    {
      get;
      set;
    }

    [DataMember]
    public string EquipmentCode
    {
      get;
      set;
    }

    [DataMember]
    public string EquipmentName
    {
      get;
      set;
    }

    [DataMember]
    public string EquipmentDescription
    {
      get;
      set;
    }

    [DataMember]
    public long EquipmentGroupId
    {
      get;
      set;
    }

    [DataMember]
    public long? AssetId
    {
      get;
      set;
    }

    [DataMember]
    public EquipmentStatus EqStatus
    {
      get;
      set;
    }

    [DataMember]
    public ServiceType EqServiceType
    {
      get;
      set;
    }

    [DataMember]
    public double? ActualValue
    {
      get;
      set;
    }

    [DataMember]
    public double? WarningValue
    {
      get;
      set;
    }

    [DataMember]
    public double? AlarmValue
    {
      get;
      set;
    }

    [DataMember]
    public DateTime? ServiceExpires
    {
      get;
      set;
    }

    [DataMember]
    public string Remark
    {
      get;
      set;
    }
  }
}
