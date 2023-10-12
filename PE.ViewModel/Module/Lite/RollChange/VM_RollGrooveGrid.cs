using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.RollChange
{
  public class VM_RollGrooveGrid : VM_Base
  {
    #region properties

    public long? RollId { get; set; }

    [SmfDisplay(typeof(VM_RollGrooveGrid), "GrooveNumber", "NAME_GrooveNumberShort")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public short GrooveNumber { get; set; }

    [SmfDisplay(typeof(VM_RollGrooveGrid), "GrooveTemplateName", "NAME_GrooveTemplateNameShort")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string GrooveTemplateName { get; set; }

    [SmfDisplay(typeof(VM_RollGrooveGrid), "AccWeight", "NAME_AccWeightShort")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfUnit("UNIT_WeightTons")]
    [SmfFormat("FORMAT_Weight")]
    public double? AccWeight { get; set; }

    //[SmfDisplay(typeof(VM_RollGrooveGrid), "AccWeightLimit", "NAME_AccWeightLimitShort")]
    //[DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    //[SmfUnit("UNIT_WeightTons")]
    //[SmfFormat("FORMAT_Weight")]
    //public double? AccWeightLimit { get; set; }

    [SmfDisplay(typeof(VM_RollGrooveGrid), "AccWeightRatio", "NAME_AccWeightRatio_ToTable")]
    [SmfFormat("FORMAT_Percent")]
    [SmfUnit("UNIT_Percent")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? AccWeightRatio { get; set; }

    [SmfDisplay(typeof(VM_RollGrooveGrid), "StatusString", "NAME_Status")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public short EnumRollGrooveStatus { get; set; }

    #endregion

    #region ctor

    public VM_RollGrooveGrid()
    {
    }

    public VM_RollGrooveGrid(V_RollHistoryPerGroove entity)
    {
      RollId = entity.RollId;
      GrooveNumber = entity.GrooveNumber;
      GrooveTemplateName = entity.GrooveTemplateName;
      AccWeight = entity.AccWeight;
      //this.AccWeightLimit = entity.AccWeightLimit ?? 0;
      EnumRollGrooveStatus = entity.EnumRollGrooveStatus;
      AccWeightRatio = AccWeightRatio = (entity.AccWeightLimit ?? 0.0) == 0.0 ? 0.0 : AccWeight / entity.AccWeightLimit;
      ;

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion
  }
}
