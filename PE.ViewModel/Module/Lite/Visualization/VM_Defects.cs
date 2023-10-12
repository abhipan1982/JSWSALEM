using System;
using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Visualization
{
  public class VM_Defects : VM_Base
  {
    [SmfDisplay(typeof(VM_Defects), "RawMaterialId", "NAME_RawMaterialId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long RawMaterialId { get; set; }

    [SmfDisplay(typeof(VM_Defects), "MaterialId", "NAME_MeasurementId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? MaterialId { get; set; }

    [SmfDisplay(typeof(VM_Defects), "WorkOrderId", "NAME_WorkOrderId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? WorkOrderId { get; set; }

    [SmfDisplay(typeof(VM_Defects), "CreatedTs", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_Defects), "Long_1_Value", "NAME_Long_1_Value")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? Long_1_Value { get; set; }

    [SmfDisplay(typeof(VM_Defects), "Long_2_Value", "NAME_Long_2_Value")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? Long_2_Value { get; set; }

    [SmfDisplay(typeof(VM_Defects), "Long_3_Value", "NAME_Long_3_Valued")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? Long_3_Value { get; set; }

    [SmfDisplay(typeof(VM_Defects), "Long_Total", "NAME_Long_Total")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? Long_Total { get; set; }

    [SmfDisplay(typeof(VM_Defects), "Trans_1_Value", "NAME_Trans_1_Valued")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? Trans_1_Value { get; set; }

    [SmfDisplay(typeof(VM_Defects), "Trans_2_Value", "NAME_Trans_2_Value")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? Trans_2_Value { get; set; }

    [SmfDisplay(typeof(VM_Defects), "Trans_3_Value", "NAME_Trans_3_Value")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? Trans_3_Value { get; set; }

    [SmfDisplay(typeof(VM_Defects), "Trans_Total", "NAME_Trans_Total")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? Trans_Total { get; set; }

    [SmfDisplay(typeof(VM_Defects), "Rep_1_Value", "NAME_Rep_1_Value")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? Rep_1_Value { get; set; }

    [SmfDisplay(typeof(VM_Defects), "Rep_2_Value", "NAME_Rep_2_Value")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? Rep_2_Value { get; set; }

    [SmfDisplay(typeof(VM_Defects), "Rep_3_Value", "NAME_Rep_3_Value")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? Rep_3_Value { get; set; }

    [SmfDisplay(typeof(VM_Defects), "Rep_Total", "NAME_Rep_Total")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? Rep_Total { get; set; }

    [SmfDisplay(typeof(VM_Defects), "Defects_Total", "NAME_Defects_Total")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? Defects_Total { get; set; }
  }
}
