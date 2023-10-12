using System;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Material
{
  public class VM_RawMaterialHistory : VM_Base
  {
    public VM_RawMaterialHistory(V_RawMaterialHistory data)
    {
      //Sorting = data.Sorting;
      RawMaterialId = data.RawMaterialId;
      //TODOMN - exclude this
      //CreatedTs = data.CreatedTs;
      //RawMaterialThickness = data.RawMaterialThickness;
      RawMaterialWeight = data.RawMaterialLastWeight;

      RawMaterialPassNo = data.RawMaterialPassNo;
      RawMaterialLength = data.RawMaterialLastLength;


      //RawMaterialDeclaredLength = data.RawMaterialDeclaredLength;
      //RawMaterialDeclaredWeight = data.RawMaterialDeclaredWeight;
      //RawMaterialLastLength = data.RawMaterialLastLength;
      //RawMaterialLastWeight = data.RawMaterialLastWeight;

      RawMaterialStepId = data.RawMaterialStepId;
      RawMaterialIsReversed = data.RawMaterialIsReversed;
      AssetName = data.AssetName;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public long Sorting { get; set; }
    public long RawMaterialId { get; set; }
    public long RawMaterialStepId { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialHistory), "CreatedTs", "NAME_Date")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialHistory), "RawMaterialStep", "NAME_Step")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short RawMaterialStep { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialHistory), "RawMaterialPassNo", "NAME_PassNo")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short RawMaterialPassNo { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialHistory), "RawMaterialIsReversed", "NAME_IsReversed")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool RawMaterialIsReversed { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialHistory), "RawMaterialLength", "NAME_Length")]
    [SmfUnit("UNIT_Length")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? RawMaterialLength { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialHistory), "RawMaterialWeight", "NAME_Weight")]
    [SmfUnit("UNIT_Weight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? RawMaterialWeight { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialHistory), "RawMaterialDeclaredLength", "NAME_RawDeclaredLength")]
    [SmfUnit("UNIT_Length")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? RawMaterialDeclaredLength { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialHistory), "RawMaterialDeclaredWeight", "NAME_RawDeclaredWeight")]
    [SmfUnit("UNIT_Weight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? RawMaterialDeclaredWeight { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialHistory), "RawMaterialLastLength", "NAME_CurrentLength")]
    [SmfUnit("UNIT_Length")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? RawMaterialLastLength { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialHistory), "RawMaterialLastWeight", "NAME_CurrentWeight")]
    [SmfUnit("UNIT_Weight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? RawMaterialLastWeight { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialHistory), "Thickness", "NAME_Thickness")]
    [SmfUnit("UNIT_BilletThickness")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? RawMaterialThickness { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialHistory), "RawMaterialElongation", "NAME_Elongation")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? RawMaterialElongation { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialHistory), "RawMaterialCutType", "NAME_CutType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short? RawMaterialCutType { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialHistory), "AssetName", "NAME_AssetName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AssetName { get; set; }
  }
}
