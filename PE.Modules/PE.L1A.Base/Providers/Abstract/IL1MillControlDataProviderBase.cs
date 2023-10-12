using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.L1A;
using PE.L1A.Base.Models;

namespace PE.L1A.Base.Providers.Abstract
{
  public interface IL1MillControlDataProviderBase : IL1SignalProviderBase
  {
    void SendMillControlDataMessage(DCMillControlMessage dc, L1MillControlDataBase millControlDataConfiguration, int retryCount = 3);
  }
}
