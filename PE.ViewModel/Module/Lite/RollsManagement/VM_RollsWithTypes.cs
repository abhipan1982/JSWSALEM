using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.RollsManagement
{
  public class VM_RollsWithTypes : VM_Base
  {
    #region ctor

    public VM_RollsWithTypes()
    {
      EnumRollStatus = RollStatus.New.Value;
    }

    public VM_RollsWithTypes(V_Roll model)
    {
      RollId = model.RollId;
      RollName = model.RollName;
      EnumRollStatus = model.EnumRollStatus;
      RollTypeId = model.RollTypeId;
      RollTypeName = model.RollTypeName;
      ActualDiameter = model.ActualDiameter;
      InitialDiameter = model.InitialDiameter;
      MinimumDiameter = model.MinimumDiameter;
      Supplier = model.Supplier;
      GroovesNumber = model.GroovesNumber;
      RollTypeDescription = model.RollTypeDescription;
      RollDescription = model.RollDescription;
      DiameterMin = model.DiameterMin ?? 0;
      DiameterMax = model.DiameterMax ?? 0;
      RoughnessMin = model.RoughnessMin ?? 0;
      RoughnessMax = model.RoughnessMax ?? 0;
      YieldStrengthRef = model.YieldStrengthRef ?? 0;
      RollSteelgrade = model.RollSteelgrade;
      RollLength = model.RollLength;
      //RollSetUpper = model.RollSetUpper;
      //RollSetBottom = model.RollSetBottom;
      //RollSetThird = model.RollSetThird;
      RollSetName = model.RollSetName;
      RollsetId = model.RollSetId;
      ScrapReason = model.EnumRollScrapReason;
      ScrapTime = model.ScrapTime;
      Editable = (model.EnumRollStatus != RollStatus.InRollSet) && (model.EnumRollStatus != RollStatus.NotAvailable) && (model.EnumRollStatus != RollStatus.Turning) && (model.EnumRollStatus != RollStatus.Scrapped);
      Removable = (model.EnumRollStatus == RollStatus.Undefined) || (model.EnumRollStatus == RollStatus.New);

      UnitConverterHelper.ConvertToLocal(this);
    }

    //public VM_RollsWithTypes(V_Roll model)
    //{
    //  RollId = model.RollId;
    //  RollName = model.RollName;
    //  EnumRollStatus = model.EnumRollStatus;
    //  RollTypeId = model.RollTypeId;
    //  RollTypeName = model.RollTypeName;
    //  ActualDiameter = model.ActualDiameter;
    //  InitialDiameter = model.InitialDiameter;
    //  MinimumDiameter = model.DiameterMin;
    //  Supplier = model.Supplier;
    //  GroovesNumber = model.GroovesNumber;
    //  Description = model.RollDescription;
    //  DiameterMin = model.DiameterMin ?? 0;
    //  DiameterMax = model.DiameterMax ?? 0;
    //  RollSetName = model.RollSetName;
    //  RollsetId = model.RollSetId;
    //  ScrapReason = model.ScrapReason ?? 0;
    //  ScrapTime = model.ScrapTime;
    //  Editable = (model.EnumRollStatus != RollStatus.InRollSet) && (model.EnumRollStatus != RollStatus.NotAvailable) && (model.EnumRollStatus != RollStatus.Turning) && (model.EnumRollStatus != RollStatus.Scrapped);
    //  Removable = (model.EnumRollStatus == RollStatus.Undefined) || (model.EnumRollStatus == RollStatus.New);

    //  UnitConverterHelper.ConvertToLocal(this);
    //}

    #endregion

    #region properties

    public virtual long? RollId { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_RollsWithTypes), "RollName", "NAME_RollName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollName { get; set; }

    [SmfDisplay(typeof(VM_RollsWithTypes), "EnumRollStatus", "NAME_RollStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short EnumRollStatus { get; set; }

    [SmfDisplay(typeof(VM_RollsWithTypes), "RollTypeId", "NAME_RollTypeName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? RollTypeId { get; set; }

    [SmfDisplay(typeof(VM_RollsWithTypes), "RollTypeName", "NAME_RollTypeName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollTypeName { get; set; }

    [SmfDisplay(typeof(VM_RollsWithTypes), "ActualDiameter", "NAME_DiameterActual")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfFormat("FORMAT_SmallDiameter")]
    [SmfUnit("UNIT_SmallDiameter")]
    public virtual double? ActualDiameter { get; set; }

    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfDisplay(typeof(VM_RollsWithTypes), "InitialDiameter", "NAME_DiameterInitial")]
    [SmfFormat("FORMAT_SmallDiameter")]
    [SmfUnit("UNIT_SmallDiameter")]
    public virtual double? InitialDiameter { get; set; }

    [SmfDisplay(typeof(VM_RollsWithTypes), "MinimumDiameter", "NAME_DiameterMinium")]
    [SmfFormat("FORMAT_Diameter")]
    [SmfUnit("UNIT_SmallDiameter")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? MinimumDiameter { get; set; }

    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfDisplay(typeof(VM_RollsWithTypes), "Supplier", "NAME_Supplier")]
    public virtual string Supplier { get; set; }

    [SmfDisplay(typeof(VM_RollsWithTypes), "GroovesNumber", "NAME_RollGroovesNumber")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? GroovesNumber { get; set; }

    [SmfDisplay(typeof(VM_RollsWithTypes), "RollTypeDescription", "NAME_DescriptionType")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollTypeDescription { get; set; }

    [SmfDisplay(typeof(VM_RollsWithTypes), "DiameterMin", "NAME_DiameterMinium")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfFormat("FORMAT_Diameter")]
    [SmfUnit("UNIT_Diameter")]
    public virtual double? DiameterMin { get; set; }

    [SmfDisplay(typeof(VM_RollsWithTypes), "DiameterMax", "NAME_DiameterMaximum")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfFormat("FORMAT_SmallDiameter")]
    [SmfUnit("UNIT_SmallDiameter")]
    public virtual double? DiameterMax { get; set; }

    [SmfDisplay(typeof(VM_RollsWithTypes), "RoughnessMin", "NAME_RoughnessMinimum")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfFormat("FORMAT_Roughness")]
    public virtual double? RoughnessMin { get; set; }

    [SmfDisplay(typeof(VM_RollsWithTypes), "RoughnessMax", "NAME_RoughnessMaximum")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfFormat("FORMAT_Roughness")]
    public virtual double? RoughnessMax { get; set; }

    [SmfDisplay(typeof(VM_RollsWithTypes), "YieldStrengthRef", "NAME_YieldStrengthRef")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfFormat("FORMAT_YieldStrengthRef")]
    public virtual double? YieldStrengthRef { get; set; }

    [SmfDisplay(typeof(VM_RollsWithTypes), "SteelgradeRoll", "NAME_SteelgradeName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollSteelgrade { get; set; }

    [SmfDisplay(typeof(VM_RollsWithTypes), "Length", "NAME_Length")]
    [SmfFormat("FORMAT_Length")]
    [SmfUnit("UNIT_RollLength")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? RollLength { get; set; }

    [SmfDisplay(typeof(VM_RollsWithTypes), "StatusName", "NAME_RollStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string StatusName { get; set; }

    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfDisplay(typeof(VM_RollsWithTypes), "RollSetUpper", "NAME_RollSetUpper")]
    public virtual string RollSetUpper { get; set; }

    [SmfDisplay(typeof(VM_RollsWithTypes), "RollSetBottom", "NAME_RollSetBottom")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollSetBottom { get; set; }

    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfDisplay(typeof(VM_RollsWithTypes), "RollSetThird", "NAME_RollSetThird")]
    public virtual string RollSetThird { get; set; }

    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfDisplay(typeof(VM_RollsWithTypes), "RollSetName", "NAME_RollSetName")]
    public virtual string RollSetName { get; set; }

    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfDisplay(typeof(VM_RollsWithTypes), "ScrapReason", "NAME_RollScrapReason")]
    public virtual short? ScrapReason { get; set; }

    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfDisplay(typeof(VM_RollsWithTypes), "ScrapDate", "NAME_ScrapDate")]
    public virtual DateTime? ScrapTime { get; set; }

    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfDisplay(typeof(VM_RollsWithTypes), "RollDescription", "NAME_Description")]
    public virtual string RollDescription { get; set; }

    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? RollsetId { get; set; }

    public virtual bool Editable { get; set; }

    public virtual bool Removable { get; set; }

    #endregion
  }
}
