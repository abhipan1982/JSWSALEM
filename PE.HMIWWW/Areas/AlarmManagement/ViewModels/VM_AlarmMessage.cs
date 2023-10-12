using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.Models;

namespace PE.HMIWWW.Areas.AlarmManagement.ViewModels
{
  public class VM_AlarmMessage : VM_Base
  {
    #region ctor

    public VM_AlarmMessage() { }

    public VM_AlarmMessage(AlarmMessage data)
    {
      AlarmMessageId = data.AlarmMessageId;
      FKAlarmDefinitionId = data.FKAlarmDefinitionId;
      FKLanguageId = data.FKLanguageId;
      MessageText = data.MessageText;
      LanguageCode = data.FKLanguage.LanguageCode;
      DefinitionCode = data.FKAlarmDefinition.DefinitionCode;
    }

    #endregion

    #region props
    public long AlarmMessageId { get; set; }

    public long FKAlarmDefinitionId { get; set; }

    public long FKLanguageId { get; set; }

    public string MessageText { get; set; }

    public string LanguageCode { get; set; }

    public string DefinitionCode { get; set; }

    #endregion
  }
}
