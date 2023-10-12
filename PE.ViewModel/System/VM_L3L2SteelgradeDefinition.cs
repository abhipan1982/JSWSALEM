using PE.DbEntity.Models;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;
using System;
using System.ComponentModel.DataAnnotations;

namespace PE.HMIWWW.ViewModel.System
{
  public class VM_L3L2SteelgradeDefinition : VM_Base
  {
    #region properties
    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_L3L2SteelgradeDefinition), "CounterId", "NAME_CounterId")]
    public virtual long CounterId { get; set; }

    [SmfDisplayAttribute(typeof(VM_L3L2SteelgradeDefinition), "CreatedTs", "NAME_CreatedTs")]
    public virtual DateTime CreatedTs { get; set; }

    [SmfDisplayAttribute(typeof(VM_L3L2SteelgradeDefinition), "UpdatedTs", "NAME_UpdatedTs")]
    public virtual DateTime UpdatedTs { get; set; }

    [SmfDisplayAttribute(typeof(VM_L3L2SteelgradeDefinition), "CommStatus", "NAME_CommStatus")]
    public virtual DbEntity.Enums.CommStatus CommStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_L3L2SteelgradeDefinition), "CommStatusName", "NAME_CommStatus")]
    public virtual string CommStatusName { get; set; }

    [SmfDisplayAttribute(typeof(VM_L3L2SteelgradeDefinition), "SteelgradeCode", "NAME_SteelgradeCode")]
    public virtual String SteelgradeCode { get; set; }

    [SmfDisplayAttribute(typeof(VM_L3L2SteelgradeDefinition), "SteelgradeDescription", "NAME_SteelgradeDescription")]
    public virtual String SteelgradeDescription { get; set; }

    [SmfDisplayAttribute(typeof(VM_L3L2SteelgradeDefinition), "ScrapGroupCode", "NAME_ScrapGroupCode")]
    public virtual String ScrapGroupCode { get; set; }

    [SmfDisplayAttribute(typeof(VM_L3L2SteelgradeDefinition), "ValidationCheck", "NAME_ValidationCheck")]
    public virtual String ValidationCheck { get; set; }
    public virtual String CommMessage { get; set; }

    [Editable(false)]
    public virtual bool CommStatusError { get; set; }
    #endregion

    #region ctor
    public VM_L3L2SteelgradeDefinition(){}

    public VM_L3L2SteelgradeDefinition(L3L2SteelgradeDefinition model)
    {
      this.CounterId = model.CounterId;
      this.CreatedTs = model.CreatedTs;
      this.UpdatedTs = model.UpdatedTs;
      this.CommStatus = model.CommStatus;
      this.CommStatusName = ResxHelper.GetResxByKey(model.CommStatus.ToString());
      this.CommStatusError = model.CommStatus == DbEntity.Enums.CommStatus.ENUM_COMMSTATUS_ProcessingError ||
                             model.CommStatus == DbEntity.Enums.CommStatus.ENUM_COMMSTATUS_ValidationError;
      this.ValidationCheck = model.ValidationCheck;
      this.CommMessage = model.CommMessage;

      this.SteelgradeCode = model.SteelgradeCode;
      this.SteelgradeDescription = model.SteelgradeDescription;
      this.ScrapGroupCode = model.ScrapGroupCode;

      UnitConverterHelper.ConvertToLocal(this);
    }
    #endregion
  }
}
