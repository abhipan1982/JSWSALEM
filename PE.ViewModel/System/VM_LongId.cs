using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.ViewModel.System
{
  public class VM_LongId : VM_Base
  {
    public VM_LongId(long id)
    {
      Id = id;
    }

    public VM_LongId()
    {
      Id = 0;
    }

    #region properties

    public virtual long Id { get; set; }

    #endregion
  }
}
