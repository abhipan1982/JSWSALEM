using System;
using System.Text;

namespace PE.L1A.Base.Providers.Abstract
{
  public interface IL1SignalProviderBase
  {
    void Init(int sleepPeriodWhenFail = 3000);
    void Start(int sleepPeriodWhenFail = 3000);
    void Stop();
  }
}
