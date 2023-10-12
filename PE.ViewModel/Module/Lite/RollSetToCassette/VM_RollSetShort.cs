using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.RollSetToCassette
{
  public class VM_RollSetShort : VM_Base
  {
    #region ctor

    public VM_RollSetShort() { }

    public VM_RollSetShort(short position)
    {
      RollSetId = null;
      RollSetStatus = RollSetStatus.Undefined;
      EnumRollSetStatus = RollSetStatus.Undefined.Value;
      RollSetName = "";
      UpperRollTypeName = "";
      PositionInCassette = null;
      RollsetCombinedInfo = "";

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_RollSetShort(V_RollSetOverview model)
    {
      RollSetId = model.RollSetId;
      RollSetStatus = RollSetStatus.GetValue(model.EnumRollSetStatus);
      EnumRollSetStatus = model.EnumRollSetStatus;
      RollSetName = model.RollSetName;
      RollSetType = model.RollSetType;
      UpperRollTypeName = model.UpperRollTypeName;
      PositionInCassette = model.PositionInCassette;
      RollsetCombinedInfo = "";

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region properties

    [SmfDisplay(typeof(VM_RollSetShort), "RollSetId", "NAME_RollSetName")]
    public virtual long? RollSetId { get; set; }

    [Editable(false)]
    [SmfDisplay(typeof(VM_RollSetShort), "RollSetStatus", "NAME_RollsetStatus")]
    public virtual RollSetStatus RollSetStatus { get; set; }

    [Editable(false)]
    [SmfDisplay(typeof(VM_RollSetShort), "EnumRollSetStatus", "NAME_RollsetStatus")]
    public virtual short EnumRollSetStatus { get; set; }

    [Editable(false)]
    [SmfDisplay(typeof(VM_RollSetShort), "RollSetName", "NAME_RollSetName")]
    public virtual string RollSetName { get; set; }

    [SmfDisplay(typeof(VM_RollSetShort), "RollSetType", "NAME_RollSetType")]
    public virtual short RollSetType { get; set; }

    [SmfDisplay(typeof(VM_RollSetShort), "UpperRollTypeName", "NAME_RollTypeUpper")]
    public virtual string UpperRollTypeName { get; set; }

    [Editable(false)]
    [SmfDisplay(typeof(VM_RollSetShort), "PositionInCassette", "NAME_PositionInCassette")]
    public virtual short? PositionInCassette { get; set; }

    public virtual string RollsetCombinedInfo { get; set; }

    #endregion
  }
}
