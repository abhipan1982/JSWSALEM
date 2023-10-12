using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L1A.Base.Models
{
  public class BypassConfigurationDto
  {
    public long BypassConfigurationId { get; set; }
    public string OpcServerAddress { get; set; }
    public string OpcServerName { get; set; }
    public string OpcBypassParentStructureNode { get; set; }
    public string OpcBypassName { get; set; }
    public short BypassTypeCode { get; set; }
    public string BypassTypeName { get; set; }
  }
}
