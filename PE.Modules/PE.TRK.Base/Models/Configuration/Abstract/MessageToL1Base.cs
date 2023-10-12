using PE.TRK.Base.Providers.Abstract;
using SMF.Core.DC;

namespace PE.TRK.Base.Models.Configuration.Abstract
{
  public abstract class MessageToL1Base<T> where T : DataContractBase
  {
    public abstract bool ShouldBeSent();

    public abstract T GetSignals();

    public abstract void Reset();
  }
}
