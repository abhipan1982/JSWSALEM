using Microsoft.EntityFrameworkCore;
using PE.DbEntity.PEContext;

namespace PE.DbEntity.Providers
{
  public interface ICustomContextProvider<T> where T : DbContext, new()
  {
    T Create();
  }
}
