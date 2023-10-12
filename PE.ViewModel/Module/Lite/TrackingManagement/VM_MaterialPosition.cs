using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.TrackingManagement
{
  public class VM_MaterialPosition : VM_Base
  {
    #region ctor

    public VM_MaterialPosition(V_RawMaterialLocation rawMaterialLocation)
    {
      OrderSeq = rawMaterialLocation.OrderSeq;
      IsVirtual = rawMaterialLocation.IsVirtual;
      RawMaterialId = rawMaterialLocation.RawMaterialId;
      RawMaterialName = rawMaterialLocation.RawMaterialName;
      MaterialId = rawMaterialLocation.MaterialId;
      MaterialName = rawMaterialLocation.MaterialName;
      WorkOrderId = rawMaterialLocation.WorkOrderId;
      WorkOrderName = rawMaterialLocation.WorkOrderName;

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region prop

    [SmfDisplay(typeof(VM_MaterialPosition), "OrderSeq", "NAME_OrderSeq")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long OrderSeq { get; set; }

    [SmfDisplay(typeof(VM_MaterialPosition), "IsVirtual", "NAME_IsVirtual")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsVirtual { get; set; }

    [SmfDisplay(typeof(VM_MaterialPosition), "RawMaterialId", "NAME_RawMaterialId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? RawMaterialId { get; set; }

    [SmfDisplay(typeof(VM_MaterialPosition), "RawMaterialName", "NAME_RawMaterialName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string RawMaterialName { get; set; }

    [SmfDisplay(typeof(VM_MaterialPosition), "MaterialId", "NAME_MaterialId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? MaterialId { get; set; }

    [SmfDisplay(typeof(VM_MaterialPosition), "MaterialName", "NAME_MaterialName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialName { get; set; }

    [SmfDisplay(typeof(VM_MaterialPosition), "WorkOrderId", "NAME_WorkOrderId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? WorkOrderId { get; set; }

    [SmfDisplay(typeof(VM_MaterialPosition), "WorkOrderName", "NAME_WorkOrderName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }

    #endregion
  }
}
