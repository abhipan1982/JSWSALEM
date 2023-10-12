using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.WorkOrder
{
  public class VM_WorkOrderMaterials : VM_Base
  {
    public VM_WorkOrderMaterials()
    {
    }

    public VM_WorkOrderMaterials(PRMWorkOrder order)
    {
      WorkOrderId = order.WorkOrderId;
      BilletWeight = order.TargetOrderWeight / order.L3NumberOfBillets;
      TargetOrderWeight = order.TargetOrderWeight;
      MaterialsNumber = order.L3NumberOfBillets;

      UnitConverterHelper.ConvertToLocal(this);
    }

    [SmfDisplay(typeof(VM_WorkOrderOverview), "WorkOrderId", "NAME_WorkOrderId")]
    public long WorkOrderId { get; set; }

    [SmfDisplay(typeof(VM_WorkOrder), "BilletWeight", "NAME_BilletWeight")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfUnit("UNIT_Weight")]
    public double BilletWeight { get; set; }

    [SmfDisplay(typeof(VM_WorkOrder), "MaterialNumber", "NAME_MaterialsNumber")]
    [SmfFormat("FORMAT_Integer", NullDisplayText = "-", HtmlEncode = false)]
    public short MaterialsNumber { get; set; }

    [SmfDisplay(typeof(VM_WorkOrder), "TargetOrderWeight", "NAME_TargetWorkOrderWeight")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfUnit("UNIT_Weight")]
    public double TargetOrderWeight { get; set; }
  }
}
