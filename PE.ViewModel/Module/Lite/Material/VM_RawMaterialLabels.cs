using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.ViewModel.Module.Lite.Material
{
  public class VM_RawMaterialLabels : VM_Base
  {
    public VM_RawMaterialLabels(V_RawMaterialLabel rawMaterialLabels)
    {
      WorkOrderName = rawMaterialLabels.WorkOrderName;
      EnumWorkOrderStatus = rawMaterialLabels.EnumWorkOrderStatus;
      SteelgradeName = rawMaterialLabels.SteelgradeName;
      HeatName = rawMaterialLabels.HeatName;
      ProductCatalogueName = rawMaterialLabels.ProductCatalogueName;
      Thickness = rawMaterialLabels.Thickness;
      MaterialName = rawMaterialLabels.MaterialName;
      MaterialSeqNo = rawMaterialLabels.MaterialSeqNo;
    }

    public string WorkOrderName { get; set; }
    public short EnumWorkOrderStatus { get; set; }
    public string SteelgradeName { get; set; }
    public string HeatName { get; set; }
    public string ProductCatalogueName { get; set; }
    public double? Thickness { get; set; }
    public string MaterialName { get; set; }
    public short? MaterialSeqNo { get; set; }
  }
}
