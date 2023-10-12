using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.ViewModel.Module.Lite.Setup
{
  public class VM_SetupConnector : VM_Base
  {
    #region ctor

    public VM_SetupConnector() { }

    public VM_SetupConnector(long setupTypeId, long setupId, string setupTypeName)
    {
      SetupTypeId = setupTypeId;
      SetupId = setupId;
      SetupTypeName = setupTypeName;
    }

    #endregion

    #region props

    public long SetupTypeId { get; set; }
    public long? SetupId { get; set; }
    public string SetupTypeName { get; set; }

    #endregion
  }
}
