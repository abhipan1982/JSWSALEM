using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.ViewModel.Module.Lite.Setup
{
  public class VM_Dictionary : VM_Base
  {
    #region ctor

    public VM_Dictionary() { }

    #endregion

    #region props

    public long Key { get; set; }
    public string Value { get; set; }

    #endregion
  }
}
