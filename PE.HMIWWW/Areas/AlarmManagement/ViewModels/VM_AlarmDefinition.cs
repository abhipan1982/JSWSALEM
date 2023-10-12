using System;
using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.Models;

namespace PE.HMIWWW.Areas.AlarmManagement.ViewModels
{
  public class VM_AlarmDefinition : VM_Base
  {
    #region ctor

    public VM_AlarmDefinition() { }

    public VM_AlarmDefinition(AlarmDefinition data)
    {
      AlarmDefinitionId = data.AlarmDefinitionId;
      FKAlarmCategoryId = data.FKAlarmCategoryId;
      FKProjectId = data.FKProjectId;
      DefinitionCode = data.DefinitionCode;
      DefinitionDescription = data.DefinitionDescription;
      IsStandard = data.IsStandard;
      IsToConfirm = data.IsToConfirm;
      IsPopupShow = data.IsPopupShow;
      EnumAlarmType = data.EnumAlarmType;
      CategoryCode = data.FKAlarmCategory.CategoryCode;
      CategoryDescription = data.FKAlarmCategory.CategoryDescription;
      DefinitionCreated = data.DefinitionCreated;
    }

    #endregion

    #region props

    public long AlarmDefinitionId { get; set; }

    public long FKAlarmCategoryId { get; set; }

    public long? FKProjectId { get; set; }

    public string DefinitionCode { get; set; }

    public string DefinitionDescription { get; set; }

    public bool? IsStandard { get; set; }

    public bool IsToConfirm { get; set; }

    public bool IsPopupShow { get; set; }

    public short EnumAlarmType { get; set; }

    public string CategoryCode { get; set; }

    public string CategoryDescription { get; set; }

    public DateTime DefinitionCreated { get; set; }

    #endregion
  }
}
