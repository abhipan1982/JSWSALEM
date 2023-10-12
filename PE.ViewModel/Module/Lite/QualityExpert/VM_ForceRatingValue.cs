using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.QualityExpert
{
  public class VM_ForceRatingValue : VM_Base
  {
    public virtual long FactRatingKey { get; set; }

    [SmfDisplay(typeof(VM_ForceRatingValue), "RatingValue", "NAME_RatingValue")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? RatingValue { get; set; }

    [SmfDisplay(typeof(VM_ForceRatingValue), "ForcedValue", "NAME_ForcedValue")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? RatingForcedValue { get; set; }

    [SmfDisplay(typeof(VM_ForceRatingValue), "RatingCurrentValue", "NAME_RatingCurrentValue")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? RatingCurrentValue { get; set; }

    public VM_ForceRatingValue(V_QERating rating)
    {
      FactRatingKey = rating.RatingId;
      RatingValue = rating.RatingValue ?? 0;
      RatingForcedValue = rating.RatingValueForced;
      RatingCurrentValue = rating.RatingCurrentValue;
    }

    public VM_ForceRatingValue() { }
  }
}
