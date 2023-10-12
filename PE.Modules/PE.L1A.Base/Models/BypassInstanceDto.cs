using System;

namespace L1A.Base.Models
{
  public class BypassInstanceDto
  {
    public long FKBypassConfigurationId { get; set; }
    public string OpcServerAddress { get; set; }
    public string OpcServerName { get; set; }
    public short BypassTypeCode { get; set; }     
    public string BypassTypeName { get; set; }
    public string OpcBypassNode { get; set; }
  }
}
