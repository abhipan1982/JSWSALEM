using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.Models;

namespace PE.HMIWWW.Areas.AlarmManagement.ViewModels
{
  public class VM_AlarmParameter : VM_Base
  {
    #region ctor

    public VM_AlarmParameter() { }

    public VM_AlarmParameter(AlarmDefinitionParam data)
    {
      AlarmDefinitionParamId = data.AlarmDefinitionParamId;
      FKAlarmDefinitionId = data.FKAlarmDefinitionId;
      ParamKey = data.ParamKey;
      ParamName = data.ParamName;
    }

    #endregion

    #region props

    public long AlarmDefinitionParamId { get; set; }

    public long FKAlarmDefinitionId { get; set; }

    public short ParamKey { get; set; }

    public string ParamName { get; set; }

    #endregion
  }
}
