using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.DbEntity.SPModels
{
  public class SPActiveBypassResult
  {
    public string BypassName { get; set; }
    public string BypassTypeName { get; set; }
    public string OpcServerAddress { get; set; }
    public string OpcServerName { get; set; }
    public bool Value { get; set; }
    public DateTime LastTimeStamp { get; set; }
  }
}
