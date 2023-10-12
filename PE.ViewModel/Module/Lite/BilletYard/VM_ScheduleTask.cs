using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.BilletYard
{
  public class VM_ScheduleTask : VM_Base
  {
    public VM_ScheduleTask() { }

    public VM_ScheduleTask(V_WorkOrderSummary db)
    {
      ScheduleOrderSeq = db.ScheduleOrderSeq;
      ScheduleId = db.ScheduleId;
      WorkOrderId = db.WorkOrderId;
      WorkOrderName = db.WorkOrderName;
      HeatId = db.HeatId;
      HeatName = db.HeatName;
      Steelgrade = db.SteelgradeName;
      MaterialsCharged = db.MaterialsCharged;
      MaterialsPlanned = db.MaterialsNumber;
      L3NumberOfBillets = db.MaterialsPlanned;
      RequiredMaterialsNumber = L3NumberOfBillets - MaterialsPlanned;
      OnChargingMaterialsNumber = MaterialsPlanned - MaterialsCharged;
    }

    public VM_ScheduleTask(V_WorkOrdersOnMaterialYard db)
    {
      ScheduleOrderSeq = db.ScheduleOrderSeq;
      WorkOrderId = db.WorkOrderId;
      WorkOrderName = db.WorkOrderName;
      HeatId = db.HeatId;
      HeatName = db.HeatName;
      Steelgrade = db.SteelgradeName;
      MaterialsCharged = db.MaterialsCharged;
      L3NumberOfBillets = db.MaterialsPlanned;
      MaterialsPlanned = db.MaterialsNumber;
      OnChargingMaterialsNumber = db.MaterialsOnArea ?? 0;
    }

    public long? ScheduleId { get; set; }
    public long? WorkOrderId { get; set; }
    public long? HeatId { get; set; }

    [SmfDisplay(typeof(VM_ScheduleTask), "ScheduleOrderSeq", "NAME_SequenceNumber")]
    public short? ScheduleOrderSeq { get; set; }

    [SmfDisplay(typeof(VM_ScheduleTask), "MaterialsPlanned", "NAME_MaterialsShort")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int MaterialsPlanned { get; set; }

    [SmfDisplay(typeof(VM_ScheduleTask), "TargetMaterialNumber", "NAME_TargetMaterialNumber")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short L3NumberOfBillets { get; set; }

    [SmfDisplay(typeof(VM_ScheduleTask), "RequiredMaterialNumber", "NAME_MaterialsShort")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int RequiredMaterialsNumber { get; set; }

    [SmfDisplay(typeof(VM_ScheduleTask), "MaterialsCharged", "NAME_MaterialsShort")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int MaterialsCharged { get; set; }

    [SmfDisplay(typeof(VM_ScheduleTask), "OnChargingMaterialsNumber", "NAME_MaterialsShort")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int OnChargingMaterialsNumber { get; set; }

    [SmfDisplay(typeof(VM_ScheduleTask), "WorkOrderName", "NAME_WorkOrderName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }


    [SmfDisplay(typeof(VM_ScheduleTask), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }


    [SmfDisplay(typeof(VM_ScheduleTask), "Steelgrade", "NAME_Steelgrade")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string Steelgrade { get; set; }
  }
}
