using System;

namespace PE.DbEntity.SPModels
{
  public class SPGetAlarmsByLanguage
  {
    public long AlarmId{ get; set; }
    public string AlarmOwner { get; set; }
    public DateTime AlarmDate { get; set; }
    public string DefaultMessage { get; set; }
    public string Param1 { get; set; }
    public string Param2 { get; set; }
    public string Param3 { get; set; }
    public string Param4 { get; set; }
    public DateTime? ConfirmationDate { get; set; }
    public string UserIdConfirmed { get; set; }
    public string UserName { get; set; }
    public long AlarmDefinitionId { get; set; }
    public string DefinitionCode { get; set; }
    public string DefinitionDescription { get; set; }
    public bool IsStandard { get; set; }
    public bool IsToConfirm { get; set; }
    public bool IsPopupShow { get; set; }
    public short EnumAlarmType { get; set; }
    public string CategoryCode { get; set; }
    public string MessageText { get; set; }
    public string MessageTextFilled { get; set; }
    public string ParamKeys { get; set; }
    public string ParamNames { get; set; }

  }
}
