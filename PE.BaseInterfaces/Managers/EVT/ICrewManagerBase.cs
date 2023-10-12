using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.EVT;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.EVT
{
  public interface ICrewManagerBase
  {
    Task<DataContractBase> DeleteCrew(DCCrewId dc);
    Task<DataContractBase> InsertCrew(DCCrewElement dc);
    Task<DataContractBase> UpdateCrew(DCCrewElement dc);
  }
}
