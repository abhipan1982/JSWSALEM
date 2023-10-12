using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.EFCoreExtensions;
using PE.DbEntity.PEContext;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Bypass;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class BypassService : IBypassService
  {
    private readonly HmiContext _hmiContext;

    public BypassService(HmiContext hmiContext)
    {
      _hmiContext = hmiContext;
    }

    public List<VM_ActiveBypass> GetActiveBypasses()
    {
      return _hmiContext.ExecuteSPGetActiveBypasses()
        .Select(x => new VM_ActiveBypass(x))
        .ToList();
    }

  }
}
