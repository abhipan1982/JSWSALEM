using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.RollType
{
  public class VM_RollType : VM_Base
  {
    #region ctor
    public VM_RollType() { }

    public VM_RollType(RLSRollType model)
    {
      RollTypeId = model.RollTypeId;
      RollTypeName = model.RollTypeName;
      RollTypeDescription = model.RollTypeDescription;
      DiameterMin = model.DiameterMin;
      DiameterMax = model.DiameterMax;
      RoughnessMin = model.RoughnessMin;
      RoughnessMax = model.RoughnessMax;
      YieldStrengthRef = model.YieldStrengthRef;
      RollSteelgrade = model.RollSteelgrade;
      RollLength = model.RollLength;
      DrawingName = model.DrawingName;
      ChokeType = model.ChokeType;
      MatchingRollsetType = model.MatchingRollsetType;

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region properties
    public virtual long? RollTypeId { get; set; }

    [SmfDisplay(typeof(VM_RollType), "RollTypeName", "NAME_RollTypeName")]
    [SmfRequired]
    public virtual string RollTypeName { get; set; }

    [SmfDisplay(typeof(VM_RollType), "RollTypeDescription", "NAME_Description")]
    public virtual string RollTypeDescription { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_RollType), "DiameterMin", "NAME_DiameterMinium")]
    [SmfFormat("FORMAT_Diameter")]
    [SmfUnit("UNIT_SmallDiameter")]
    public virtual double? DiameterMin { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_RollType), "DiameterMax", "NAME_DiameterMaximum")]
    [SmfFormat("FORMAT_Diameter")]
    [SmfUnit("UNIT_SmallDiameter")]
    public virtual double? DiameterMax { get; set; }

    [SmfDisplay(typeof(VM_RollType), "RoughnessMin", "NAME_RoughnessMinimum")]
    [SmfFormat("FORMAT_Roughness")]
    public virtual double? RoughnessMin { get; set; }

    [SmfDisplay(typeof(VM_RollType), "RoughnessMax", "NAME_RoughnessMaximum")]
    [SmfFormat("FORMAT_Roughness")]
    public virtual double? RoughnessMax { get; set; }

    [SmfDisplay(typeof(VM_RollType), "YieldStrengthRef", "NAME_YieldStrengthRef")]
    [SmfFormat("FORMAT_YieldStrengthRef")]
    public virtual double? YieldStrengthRef { get; set; }

    [SmfDisplay(typeof(VM_RollType), "RollSteelgrade", "NAME_SteelGradeName")]
    public virtual string RollSteelgrade { get; set; }

    [SmfDisplay(typeof(VM_RollType), "RollLength", "NAME_Length")]
    [SmfFormat("FORMAT_Length")]
    [SmfUnit("UNIT_Length")]
    public virtual double? RollLength { get; set; }

    [SmfDisplay(typeof(VM_RollType), "DrawingName", "NAME_DrawingName")]
    public virtual string DrawingName { get; set; }

    [SmfDisplay(typeof(VM_RollType), "ChokeType", "NAME_ChokeType")]
    public virtual string ChokeType { get; set; }

    [SmfDisplay(typeof(VM_RollType), "Adjust", "NAME_Adjust")]
    public virtual short? Adjust { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_RollType), "MatchingRollsetType", "NAME_MatchingRollsetType")]
    public virtual short? MatchingRollsetType { get; set; }

    public virtual bool IsInUse { get; set; }

    #endregion
  }
}
