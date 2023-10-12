using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace PE.CommunicationTracer.Core
{
  public class ModuleStatusEvent : PubSubEvent<ModuleDate> { }

  public class ModuleDate
  {
    public string Name { get; set; }
    public ModuleStatus Status { get; set; }
  }

  public enum ModuleStatus { Disconnected, Connecting, Connected }
}
