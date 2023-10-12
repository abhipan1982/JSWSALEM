using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMF.Core.DC;

namespace PE.CommunicationTracer.Core
{
  public class ModelContainer
  {
    public DataContractBase Model { get; set; }
    public Type Type { get; set; }
  }
}
