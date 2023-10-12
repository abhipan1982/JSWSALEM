using PE.Models.DataContracts.Internal.MDA;
using PE.Models.DataContracts.Internal.MDB;

namespace PE.MDA.Base.Interfaces
{
  public interface IHelloManagerBase
  {
    Task<DCAckMessage> ProcessHello(DCHelloMessage message);
  }
}
