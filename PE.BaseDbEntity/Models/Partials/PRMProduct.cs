using System;
using System.Collections.Generic;
using System.Text;
using PE.BaseDbEntity.EnumClasses;

namespace PE.BaseDbEntity.Models
{
  public partial class PRMProduct
  {
    public void Initialize()
    {
      EnumWeightSource = WeightSource.Calculated;
    }
  }
}
