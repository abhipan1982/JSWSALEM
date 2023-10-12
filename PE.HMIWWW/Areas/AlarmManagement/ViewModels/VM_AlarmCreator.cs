using System.Collections.Generic;
using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.Models;

namespace PE.HMIWWW.Areas.AlarmManagement.ViewModels
{
  public class VM_AlarmCreator : VM_Base
  {
    #region ctor

    public VM_AlarmCreator() { }

    public VM_AlarmCreator(AlarmDefinition data)
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
      ProjectName = data?.FKProject?.ProjectName;
      FKProjectId = data.FKProjectId;
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

    public string ProjectName { get; set; }

    public VM_AlarmMessage DefaultMessage { get; set; }

    public List<VM_AlarmMessage> Messages { get; set; }

    public VM_AlarmParameter Parameter0 { get; set; }

    public VM_AlarmParameter Parameter1 { get; set; }

    public VM_AlarmParameter Parameter2 { get; set; }

    public VM_AlarmParameter Parameter3 { get; set; }

    #endregion
  }
}
