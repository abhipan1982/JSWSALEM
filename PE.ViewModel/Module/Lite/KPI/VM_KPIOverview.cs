using System;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.UnitConverter;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.KPI
{
  public class VM_KPIOverview : VM_Base
  {
    #region ctor

    public VM_KPIOverview() { }

    public VM_KPIOverview(V_KPIValue data)
    {
      KPIValueId = data.KPIValueId;
      KPITime = data.KPITime;
      KPIDefinitionId = data.KPIDefinitionId;
      KPIValue = data.KPIValue;
      WorkOrderId = data.WorkOrderId;
      KPICode = data.KPICode;
      KPIName = data.KPIName;
      MinValue = data.MinValue;
      AlarmTo = data.AlarmTo;
      WarningTo = data.WarningTo;
      MaxValue = data.MaxValue;
      UnitId = data.UnitId;
      EnumGaugeDirection = data.EnumGaugeDirection;
      UnitSymbol = data.UnitSymbol;

      FeatureUnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region props

    public long KPIValueId { get; set; }
    public DateTime KPITime { get; set; }
    public long KPIDefinitionId { get; set; }
    [SmfDisplay(typeof(VM_KPIOverview), "KPIValue", "NAME_Value")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [FeatureUnitValue(nameof(UnitId), "0")]
    public double KPIValue { get; set; }
    public long? WorkOrderId { get; set; }
    public string KPICode { get; set; }
    public string KPIName { get; set; }
    [SmfDisplay(typeof(VM_KPIOverview), "MinValue", "NAME_MinValue")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [FeatureUnitValue(nameof(UnitId), "0")]
    public double MinValue { get; set; }
    [SmfDisplay(typeof(VM_KPIOverview), "AlarmTo", "NAME_Alarm")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [FeatureUnitValue(nameof(UnitId), "0")]
    public double AlarmTo { get; set; }
    [SmfDisplay(typeof(VM_KPIOverview), "WarningTo", "NAME_Warning")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [FeatureUnitValue(nameof(UnitId), "0")]
    public double WarningTo { get; set; }
    [SmfDisplay(typeof(VM_KPIOverview), "MaxValue", "NAME_MaxValue")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [FeatureUnitValue(nameof(UnitId), "0")]
    public double MaxValue { get; set; }
    public long UnitId { get; set; }
    public short EnumGaugeDirection { get; set; }
    [FeatureUnitSymbol(nameof(UnitId), "0", false)]
    public string UnitSymbol { get; set; }

    #endregion
  }
}
