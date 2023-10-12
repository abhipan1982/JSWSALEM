using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.TRK.Base.Models._Base;

namespace PE.TRK.Base.Models.TrackingEntities.Concrete
{
  public record class IntCorrelationId(int Value) : TrackingCorrelation;
}
