﻿using System.ServiceModel;
using SMF.Core.Interfaces;

namespace PE.BaseInterfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IL1SimulationBase : IBaseModule
  {
  }
}
