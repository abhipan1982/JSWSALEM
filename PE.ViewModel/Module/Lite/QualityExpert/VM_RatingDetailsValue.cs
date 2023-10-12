using System;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.QualityExpert
{
  public class VM_RatingDetailsValue : VM_Base
  {
    public virtual long RatingId { get; set; }

    [SmfDisplay(typeof(VM_ForceRatingValue), "RatingValue", "NAME_RatingValue")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? RatingValue { get; set; }

    [SmfDisplay(typeof(VM_ForceRatingValue), "ForcedValue", "NAME_ForcedValue")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? RatingForcedValue { get; set; }

    [SmfDisplay(typeof(VM_ForceRatingValue), "RatingCurrentValue", "NAME_RatingCurrentValue")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? RatingCurrentValue { get; set; }

    [SmfDisplay(typeof(VM_ForceRatingValue), "RatingCurrentValue", "NAME_CreatedTs")]
    public DateTime CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_ForceRatingValue), "RatingCurrentValue", "NAME_RatingModifiedTs")]
    public DateTime ModifiedTs { get; set; }

    [SmfDisplay(typeof(VM_ForceRatingValue), "RatingCurrentValue", "NAME_RatingCode")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public double? RatingCode { get; set; }

    [SmfDisplay(typeof(VM_ForceRatingValue), "RatingCurrentValue", "NAME_RatingGroup")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public int? RatingGroup { get; set; }

    [SmfDisplay(typeof(VM_ForceRatingValue), "RatingCurrentValue", "NAME_RatingType")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public int? RatingType { get; set; }

    [SmfDisplay(typeof(VM_ForceRatingValue), "RatingAlarm", "NAME_RatingAlarm")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string RatingAlarm { get; set; }

    [SmfDisplay(typeof(VM_ForceRatingValue), "RatingAffectedArea", "NAME_RatingAffectedArea")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public double? RatingAffectedArea { get; set; }

    public VM_RatingDetailsValue(V_QERating rating)
    {
      RatingId = rating.RatingId;
      RatingValue = rating.RatingValue ?? 0;
      RatingForcedValue = rating.RatingValueForced;
      RatingCurrentValue = rating.RatingCurrentValue;
    }

    public VM_RatingDetailsValue() { }
  }
}
