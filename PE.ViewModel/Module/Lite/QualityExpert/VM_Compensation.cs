using System.ComponentModel.DataAnnotations;
using System.Linq;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.QualityExpert
{
  public class VM_Compensation : VM_Base
  {
    public virtual long? CompensationId { get; set; }

    public virtual long? FKRatingId { get; set; }

    [SmfDisplay(typeof(VM_Compensation), "Name", "NAME_Name")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string CompensationName { get; set; }

    [SmfDisplay(typeof(VM_Compensation), "FkCompensationTypeId", "NAME_FkCompensationTypeId")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? FKCompensationTypeId { get; set; }

    [SmfDisplay(typeof(VM_Compensation), "Alternative", "NAME_Alternative")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? CompensationAlternative { get; set; }

    [SmfDisplay(typeof(VM_Compensation), "Info", "NAME_Info")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string CompensationInfo { get; set; }

    [SmfDisplay(typeof(VM_Compensation), "Detail", "NAME_Detail")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string CompensationDetail { get; set; }

    [SmfDisplay(typeof(VM_Compensation), "IsChosen", "NAME_IsChosen")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual bool? IsChosen { get; set; }

    [SmfDisplay(typeof(VM_Compensation), "QeAggregates", "NAME_QeAggregates")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string Aggregates { get; set; }

    [SmfDisplay(typeof(VM_Compensation), "QeCompensationType", "NAME_QeCompensationType")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string CompensationType { get; set; }

    [SmfDisplay(typeof(VM_Compensation), "RatingCode", "NAME_RatingCode")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? RatingCode { get; set; }

    public VM_Compensation(QECompensation data)
    {
      CompensationId = data.CompensationId;
      FKRatingId = data.FKRatingId;
      CompensationName = data.CompensationName;
      FKCompensationTypeId = data.FKCompensationTypeId;
      CompensationAlternative = data.CompensationAlternative;
      CompensationInfo = data.CompensationInfo;
      CompensationDetail = data.CompensationDetail;
      IsChosen = data.IsChosen;
      CompensationType = data.FKCompensationType.CompensationName;
      RatingCode = data.FKCompensationType.CompensationRatingCode;
      Aggregates = string.Join(", ", data.QECompensationAggregates.Select(x => x.FKAssetId));
    }
  }
}
