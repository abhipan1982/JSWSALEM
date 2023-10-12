using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.Providers
{
  public class DefaultCustomContextProvider<T> : ICustomContextProvider<T> where T : DbContext, new()
  {
    public T Create()
    {
      return new T();
    }
  }
}
