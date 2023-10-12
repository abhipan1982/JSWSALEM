using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.Models;

namespace PE.HMIWWW.Areas.AlarmManagement.ViewModels
{
  public class VM_AlarmRole : VM_Base
  {
    #region ctor
    public VM_AlarmRole() { }

    public VM_AlarmRole(Role data, AlarmDefinitionsToRole alarm)
    {
      AlarmDefinitionId = alarm?.FKAlarmDefinitionId;
      Id = data.Id;
      AlarmDefinitionToRoleId = alarm?.AlarmDefinitionToRoleId;
      Name = data.Name;
      IsAssigned = alarm != null;
    }

    #endregion

    #region props

    public long? AlarmDefinitionId { get; set; }

    public string Id { get; set; }

    public long? AlarmDefinitionToRoleId { get; set; }

    public string Name { get; set; }

    public bool IsAssigned { get; set; }

    #endregion
  }
}
