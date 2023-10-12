using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.Providers
{
  public interface IContextProvider<T> where T : DbContext, new()
  {
    T Create();
  }
}
