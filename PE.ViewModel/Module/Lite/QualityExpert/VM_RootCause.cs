using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.QualityExpert
{
  public class VM_RootCause : VM_Base
  {
    public virtual long RootCauseId { get; set; }

    public virtual long FkRatingId { get; set; }

    [SmfDisplay(typeof(VM_RootCause), "Name", "NAME_Name")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RootCauseName { get; set; }

    [SmfDisplay(typeof(VM_RootCause), "Type", "NAME_Type")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? RootCauseType { get; set; }

    [SmfDisplay(typeof(VM_RootCause), "Priority", "NAME_Priority")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? RootCausePriority { get; set; }

    [SmfDisplay(typeof(VM_RootCause), "Info", "NAME_Info")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RootCauseInfo { get; set; }

    [SmfDisplay(typeof(VM_RootCause), "Verification", "NAME_Verification")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RootCauseVerification { get; set; } 

    [SmfDisplay(typeof(VM_RootCause), "Correction", "NAME_Correction")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RootCauseCorrection { get; set; }

    [SmfDisplay(typeof(VM_RootCause), "QeAggregates", "NAME_QeAggregates")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string Aggregates { get; set; }

    public VM_RootCause(QERootCause data)
    {
      RootCauseId = data.RootCauseId;
      FkRatingId = data.FKRatingId;
      RootCauseName = data.RootCauseName;
      RootCauseType = data.RootCauseType;
      RootCausePriority = data.RootCausePriority;
      RootCauseInfo = data.RootCauseInfo;
      RootCauseVerification = data.RootCauseVerification;
      RootCauseCorrection = data.RootCauseCorrection;
      Aggregates = String.Join(",", data.QERootCauseAggregates.Select(x => x.FKAssetId));
    }
  }
}
