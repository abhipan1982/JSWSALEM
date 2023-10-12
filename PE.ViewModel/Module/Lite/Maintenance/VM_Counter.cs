using System;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Maintenance
{
  public class VM_Counter : VM_Base
  {
    public VM_Counter()
    {
      UnitConverterHelper.ConvertToLocal(this);
    }

    //public VM_Counter(MNTLimitCounter p)
    //{
    //  //TODOMN - refactor this
    //  //this.CounterId = p.CounterId;
    //  //this.CreatedTs = p.CreatedTs;
    //  //this.LastUpdateTs = p.LastUpdateTs;
    //  //this.LifeCycle = p.LifeCycle;
    //  //this.FkDeviceId = p.FKDeviceId;
    //  //this.FkQuantityTypeId = p.FKQuantityTypeId;
    //  //this.Value = p.Value;
    //  //if (p.MNTQuantityType != null)
    //  //{
    //  //  this.QuantityTypeCode = p.MNTQuantityType.QuantityTypeCode;
    //  //  this.QuantityTypeId = p.MNTQuantityType.QuantityTypeId;
    //  //  this.QuantityTypeName = p.MNTQuantityType.QuantityTypeName;
    //  //  this.FKUnitId = p.MNTQuantityType.FKUnitId;
    //  //}
    //  //if (p.MNTQuantityType.MNTLimits1.FirstOrDefault() != null)
    //  //{
    //  //  MNTLimit limit = p.MNTQuantityType.MNTLimits1.FirstOrDefault();
    //  //  this.ValueAlarm = limit.ValueAlarm;
    //  //  this.ValueMax = limit.ValueMax;
    //  //  this.ValueWarning = limit.ValueWarning;
    //  //  this.FKUnitId = p.MNTQuantityType.FKUnitId;
    //  //}


    //  UnitConverterHelper.ConvertToLocal(this);
    //}

    public long? CounterId { get; set; }
    public long? FkDeviceId { get; set; }

    [SmfDisplay(typeof(VM_Counter), "CreatedTs", "NAME_CreatedTs")]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_Counter), "LastUpdateTs", "NAME_LastUpdateTs")]
    public DateTime? LastUpdateTs { get; set; }

    [SmfDisplay(typeof(VM_Counter), "LifeCycle", "NAME_LifeCycle")]
    public short LifeCycle { get; set; }

    public long? FkQuantityTypeId { get; set; }

    [SmfDisplay(typeof(VM_Counter), "Value", "NAME_Value")]
    public double Value { get; set; }

    public string QuantityTypeCode { get; set; }
    public long? QuantityTypeId { get; set; }
    public string QuantityTypeName { get; set; }
    public long? FKUnitId { get; set; }
    public string Unit { get; set; }

    [SmfDisplay(typeof(VM_Counter), "ValueAlarm", "NAME_Value")]
    public double? ValueAlarm { get; set; }

    [SmfDisplay(typeof(VM_Counter), "ValueMax", "NAME_Value")]
    public double? ValueMax { get; set; }

    [SmfDisplay(typeof(VM_Counter), "ValueWarning", "NAME_Value")]
    public double? ValueWarning { get; set; }
  }
}
