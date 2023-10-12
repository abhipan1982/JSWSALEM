using System;
using PE.DbEntity.HmiModels;
using PE.DbEntity.SPModels;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.EnumClasses;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.System
{
  public class VM_AlarmItem : VM_Base
  {
    public VM_AlarmItem() { }

    public VM_AlarmItem(V_Alarm alarm)
    {
      AlarmId = alarm.AlarmId;
      AlarmDate = alarm.AlarmDate;
      Message = alarm.MessageTextFilled;
      AlarmOwner = alarm.AlarmOwner;
      Confirmation = alarm.ConfirmationDate.HasValue;
      ConfirmationDate = alarm.ConfirmationDate;
      ConfirmedBy = alarm.UserName;
      AlarmCategoryName = alarm.CategoryCode;
      AlarmType = alarm.EnumAlarmType;
      AlarmTypeName = ResxHelper.GetResxByKey((AlarmType)alarm.EnumAlarmType);
      ToConfirm = alarm.IsToConfirm;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_AlarmItem(SPGetAlarmsByLanguage alarm)
    {
      AlarmId = alarm.AlarmId;
      AlarmDate = alarm.AlarmDate;
      Message = alarm.MessageTextFilled;
      AlarmOwner = alarm.AlarmOwner;
      Confirmation = alarm.ConfirmationDate.HasValue;
      ConfirmationDate = alarm.ConfirmationDate;
      ConfirmedBy = alarm.UserName;
      AlarmCategoryName = alarm.CategoryCode;
      AlarmType = alarm.EnumAlarmType;
      AlarmTypeName = ResxHelper.GetResxByKey((AlarmType)alarm.EnumAlarmType);
      ToConfirm = alarm.IsToConfirm;

      UnitConverterHelper.ConvertToLocal(this);
    }

    [SmfDisplay(typeof(VM_AlarmItem), "Id", "NAME_Id")]
    public long AlarmId { get; set; }

    [SmfDisplay(typeof(VM_AlarmItem), "AlarmDate", "NAME_AlarmDate")]
    public DateTime AlarmDate { get; set; }

    [SmfDisplay(typeof(VM_AlarmItem), "Message", "NAME_Message")]
    public string Message { get; set; }

    [SmfDisplay(typeof(VM_AlarmItem), "AlarmOwner", "NAME_AlarmOwner")]
    public string AlarmOwner { get; set; }

    [SmfDisplay(typeof(VM_AlarmItem), "Confirmation", "NAME_Confirmation")]
    public bool Confirmation { get; set; }

    [SmfDisplay(typeof(VM_AlarmItem), "ConfirmationDate", "NAME_ConfirmationDate")]
    public DateTime? ConfirmationDate { get; set; }

    [SmfDisplay(typeof(VM_AlarmItem), "ConfirmedBy", "NAME_ConfirmedBy")]
    public string ConfirmedBy { get; set; }

    [SmfDisplay(typeof(VM_AlarmItem), "AlarmCategoryName", "NAME_AlarmCategoryName")]
    public string AlarmCategoryName { get; set; }

    [SmfDisplay(typeof(VM_AlarmItem), "AlarmType", "NAME_AlarmType")]
    public int AlarmType { get; set; }

    [SmfDisplay(typeof(VM_AlarmItem), "Type", "NAME_AlarmType")]
    public string AlarmTypeName { get; set; }

    [SmfDisplay(typeof(VM_AlarmItem), "ToConfirm", "NAME_ToConfirm")]
    public bool ToConfirm { get; set; }
  }
}
