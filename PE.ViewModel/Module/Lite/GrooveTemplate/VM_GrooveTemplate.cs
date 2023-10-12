using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module
{
  public class VM_GrooveTemplate : VM_Base
  {
    #region ctor

    public VM_GrooveTemplate() { }

    public VM_GrooveTemplate(RLSGrooveTemplate model)
    {
      GrooveTemplateId = model.GrooveTemplateId;
      Shape = model.Shape;
      GrooveTemplateName = model.GrooveTemplateName;
      EnumGrooveTemplateStatus = model.EnumGrooveTemplateStatus;
      R1 = model.R1;
      R2 = model.R2;
      R3 = model.R3;
      D1 = model.D1;
      D2 = model.D2;
      W1 = model.W1;
      W2 = model.W2;
      Angle1 = model.Angle1;
      Angle2 = model.Angle2;
      SpreadFactor = model.SpreadFactor;
      GrindingProgramName = model.GrindingProgramName;
      GrooveTemplateDescription = model.GrooveTemplateDescription;
      Plane = model.Plane;
      NameShort = model.GrooveTemplateCode;
      Ds = model.Ds;
      Dw = model.Dw;
      GrooveTemplateCode = model.GrooveTemplateCode;
      GrooveSettingEnum = model.EnumGrooveSetting;
      EnumGrooveSetting = model.EnumGrooveSetting;
      AccBilletCntLimit = model.AccBilletCntLimit;
      AccBilletWeightLimit = model.AccWeightLimit;

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region properties

    public virtual long? GrooveTemplateId { get; set; }

    [SmfDisplay(typeof(VM_GrooveTemplate), "Shape", "NAME_Shape")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual String Shape { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_GrooveTemplate), "GrooveTemplateName", "NAME_GrooveTemplateName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual String GrooveTemplateName { get; set; }

    [SmfDisplay(typeof(VM_GrooveTemplate), "EnumGrooveTemplateStatus", "NAME_Status")]
    public virtual GrooveTemplateStatus EnumGrooveTemplateStatus { get; set; }

    [SmfDisplay(typeof(VM_GrooveTemplate), "R1", "NAME_Radius1")]
    [SmfFormat("FORMAT_Radius")]
    [SmfUnit("UNIT_Radius")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? R1 { get; set; }

    [SmfDisplay(typeof(VM_GrooveTemplate), "R2", "NAME_Radius2")]
    [SmfFormat("FORMAT_Radius")]
    [SmfUnit("UNIT_Radius")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? R2 { get; set; }

    [SmfDisplay(typeof(VM_GrooveTemplate), "R3", "NAME_Radius3")]
    [SmfFormat("FORMAT_Radius")]
    [SmfUnit("UNIT_Radius")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? R3 { get; set; }

    [SmfDisplay(typeof(VM_GrooveTemplate), "D1", "NAME_Depth1")]
    [SmfFormat("FORMAT_Depth")]
    [SmfUnit("UNIT_Depth")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? D1 { get; set; }

    [SmfDisplay(typeof(VM_GrooveTemplate), "D2", "NAME_Depth2")]
    [SmfFormat("FORMAT_Depth")]
    [SmfUnit("UNIT_Depth")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? D2 { get; set; }

    [SmfDisplay(typeof(VM_GrooveTemplate), "W1", "NAME_Width1")]
    [SmfFormat("FORMAT_Width")]
    [SmfUnit("UNIT_WidthSmall")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? W1 { get; set; }

    [SmfDisplay(typeof(VM_GrooveTemplate), "W2", "NAME_Width2")]
    [SmfFormat("FORMAT_Width")]
    [SmfUnit("UNIT_WidthSmall")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? W2 { get; set; }

    [SmfDisplay(typeof(VM_GrooveTemplate), "Angle1", "NAME_Angle1")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfFormat("FORMAT_Angle")]
    public virtual double? Angle1 { get; set; }

    [SmfDisplay(typeof(VM_GrooveTemplate), "Angle2", "NAME_Angle2")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfFormat("FORMAT_Angle")]
    public virtual double? Angle2 { get; set; }

    [SmfDisplay(typeof(VM_GrooveTemplate), "SpreadFactor", "NAME_SpreadFactor")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? SpreadFactor { get; set; }

    [SmfDisplay(typeof(VM_GrooveTemplate), "GrindingProgramName", "NAME_GrindingProgrammeName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual String GrindingProgramName { get; set; }

    [SmfDisplay(typeof(VM_GrooveTemplate), "GrooveTemplateDescription", "NAME_Description")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual String GrooveTemplateDescription { get; set; }

    [SmfDisplay(typeof(VM_GrooveTemplate), "Plane", "NAME_Plane")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfFormat("FORMAT_Plain3", HtmlEncode = false)]
    [SmfUnit("UNIT_ProfilePlane")]
    public virtual double? Plane { get; set; }

    [SmfDisplay(typeof(VM_GrooveTemplate), "NameShort", "NAME_GrooveNameShort")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual String NameShort { get; set; }

    [SmfDisplay(typeof(VM_GrooveTemplate), "GrooveTemplateCode", "NAME_Code")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual String GrooveTemplateCode { get; set; }

    [SmfDisplay(typeof(VM_GrooveTemplate), "Ds", "NAME_Ds")]  // diameter of material
    [SmfFormat("FORMAT_Depth")]
    [SmfUnit("UNIT_Depth")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? Ds { get; set; }

    [SmfDisplay(typeof(VM_GrooveTemplate), "Dw", "NAME_Dw")]   // diameter of tool
    [SmfFormat("FORMAT_Depth")]
    [SmfUnit("UNIT_Depth")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? Dw { get; set; }

    [SmfDisplay(typeof(VM_GrooveTemplate), "GrooveSettingEnum", "NAME_GrooveSettingForColoring")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual GrooveSetting GrooveSettingEnum { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_GrooveTemplate), "EnumGrooveSetting", "NAME_GrooveSettingForColoring")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short EnumGrooveSetting { get; set; }

    [SmfDisplay(typeof(VM_GrooveTemplate), "AccBilletCntLimit", "NAME_AccBilletCntLimit")]
    [SmfFormat("FORMAT_DefaultLong")]
    [SmfRequired]
    public virtual long? AccBilletCntLimit { get; set; }

    [SmfDisplay(typeof(VM_GrooveTemplate), "AccBilletWeightLimit", "NAME_AccBilletWeightLimit")]
    [SmfUnit("UNIT_WeightTons")]
    [SmfFormat("FORMAT_HeatWeight")]
    [SmfRequired]
    public virtual double? AccBilletWeightLimit { get; set; }

    #endregion
  }
}
