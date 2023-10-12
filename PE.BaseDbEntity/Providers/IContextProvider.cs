using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Providers
{
  public interface IContextProvider<T> where T : DbContext, new()
  {
    T Create();
  }
}
