using System;
using PE.DbEntity.SPModels;

namespace PE.HMIWWW.ViewModel.Module.Lite.Bypass
{
  public class VM_ActiveBypass
  {
    public string BypassName { get; set; }
    public string OpcServerAddress { get; set; }
    public string OpcServerName { get; set; }
    public bool Value { get; set; }
    public DateTime TimeStamp { get; set; }

    private VM_ActiveBypass() { }

    public VM_ActiveBypass(SPActiveBypassResult activeBypassResult)
    {
      BypassName = activeBypassResult.BypassName;
      OpcServerAddress = activeBypassResult.OpcServerAddress;
      OpcServerName = activeBypassResult.OpcServerName;
      Value = activeBypassResult.Value;
      TimeStamp = activeBypassResult.LastTimeStamp;
    }
  }
}
