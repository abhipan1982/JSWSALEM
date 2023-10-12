using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.DbEntity.SPModels;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Furnace
{
  public class VM_WorkOrderLocation
  {
    public VM_WorkOrderLocation(V_WorkOrderLocation entity)
    {
      Seq = entity.OrderSeq;
      WorkOrderName = entity.WorkOrderName;
      HeatName = entity.HeatName;
      SteelgradeName = entity.SteelgradeName;
      WorkOrderId = entity.WorkOrderId;
      SteelgradeId = entity.SteelgradeId;
      HeatId = entity.HeatId;
      RawMaterialCounter = entity.RawMaterialNumber;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_WorkOrderLocation(long seq, SPL3L1MaterialInArea area, int? rawMaterialCounter)
    {
      Seq = seq;
      WorkOrderName = area.WorkOrderName;
      HeatName = area.HeatName;
      SteelgradeName = area.SteelgradeCode;
      WorkOrderId = area.WorkOrderId;
      SteelgradeId = area.SteelgradeId;
      HeatId = area.HeatId;
      RawMaterialCounter = rawMaterialCounter;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public long Seq { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderLocation), "WorkOrderName", "NAME_WorkOrderName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderLocation), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderLocation), "SteelgradeName", "NAME_SteelgradeName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeName { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderLocation), "RawMaterialCounter", "NAME_RawMaterialCounter")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int? RawMaterialCounter { get; set; }

    public long? HeatId { get; set; }
    public long? WorkOrderId { get; set; }
    public long? SteelgradeId { get; set; }
  }
}
