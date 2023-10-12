using System.Collections.Generic;
using PE.HMIWWW.ViewModel.Module.Lite.Bypass;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IBypassService
  {
    List<VM_ActiveBypass> GetActiveBypasses();
  }
}
