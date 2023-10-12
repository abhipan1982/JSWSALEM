using PE.BaseModels.DataContracts.Internal.L1A;
using PE.TRK.Base.Models.Configuration.Abstract;

namespace PE.TRK.Base.Models.Configuration.Concrete
{
  public class StandardMessageToL1 : MessageToL1Base<DCL1Message>
  {
    public bool HeatChanged { get; private set; }

    public override bool ShouldBeSent()
    {
      return HeatChanged;
    }

    public override DCL1Message GetSignals()
    {
      return new DCL1Message()
      {
        HeatChanged = HeatChanged
      };
    }

    public override void Reset()
    {
      SetHeatChangedState(false);
    }

    public void SetHeatChangedState(bool heatChanged)
    {
      HeatChanged = heatChanged;
    }
  }
}
