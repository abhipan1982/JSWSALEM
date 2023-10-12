using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.Providers
{
  public class DefaultContextProvider<T> : IContextProvider<T> where T : DbContext, new()
  {
    public T Create()
    {
      return new T();
    }
  }
}
