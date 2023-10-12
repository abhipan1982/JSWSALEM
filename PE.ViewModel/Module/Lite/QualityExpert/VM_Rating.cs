using System;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.QualityExpert
{
  public class VM_Rating : VM_Base
  {
    public VM_Rating() { }

    public VM_Rating(V_QERating rating)
    {
      FactRatingKey = rating.RatingId;
      DimAssetKey = rating.AssetId;
      DimMaterialKey = rating.RawMaterialId;
      RatingRanking = rating.RatingRanking;
      RatingAlternative = rating.RatingAlternative;
      RatingValue = rating.RatingValue ?? 0;
      RatingForcedValue = rating.RatingValueForced;
      RatingCurrentValue = rating.RatingCurrentValue;
      RatingType = rating.RatingType;
      RatingCreatedTs = rating.RatingCreated;
      RatingModifiedTs = rating.RatingModified;
      AssetName = rating.AssetName;
      RawMaterialName = rating.RawMaterialName;
      RuleIdentifier = rating.RulesIdentifierPart2;
      RatingName = $"{rating.RulesIdentifierPart2}.{rating.RulesIdentifierPart3}";
    }

    public VM_Rating(V_QERating rating, long rawMaterial) : this(rating)
    {
      DimMaterialKey = rawMaterial;
    }

    public long FactRatingKey { get; set; }

    public long DimAssetKey { get; set; }

    public long DimMaterialKey { get; set; }

    [SmfDisplay(typeof(VM_Rating), "Ranking", "NAME_Ranking")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? RatingRanking { get; set; }

    [SmfDisplay(typeof(VM_Rating), "RatingValue", "NAME_RatingValue")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? RatingValue { get; set; }

    [SmfDisplay(typeof(VM_Rating), "ForcedValue", "NAME_ForcedValue")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? RatingForcedValue { get; set; }

    [SmfDisplay(typeof(VM_Rating), "RatingCurrentValue", "NAME_RatingCurrentValue")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? RatingCurrentValue { get; set; }

    [SmfDisplay(typeof(VM_Rating), "RatingAlternative", "NAME_RatingRatingAlternative")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? RatingAlternative { get; set; }

    [SmfDisplay(typeof(VM_Rating), "RatingType", "NAME_RatingType")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? RatingType { get; set; }

    [SmfDisplay(typeof(VM_Rating), "CreatedTs", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual DateTime? RatingCreatedTs { get; set; }

    [SmfDisplay(typeof(VM_Rating), "RatingModifiedTs", "NAME_RatingModifiedTs")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual DateTime? RatingModifiedTs { get; set; }

    [SmfDisplay(typeof(VM_Rating), "AssetName", "NAME_AssetName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string AssetName { get; set; }

    [SmfDisplay(typeof(VM_Rating), "RawMaterialName", "NAME_RawMaterialName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RawMaterialName { get; set; }

    [SmfDisplay(typeof(VM_Rating), "RuleIdentifier", "NAME_RuleIdentifier")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public virtual string RuleIdentifier { get; set; }

    [SmfDisplay(typeof(VM_Rating), "RatingName", "NAME_RatingName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public virtual string RatingName { get; set; }
  }
}
