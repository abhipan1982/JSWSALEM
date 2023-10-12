using System;
using PE.TRK.Base.Models.TrackingEntities.Abstract;

namespace PE.TRK.Base.Models._Base
{
  public interface ITrackingSimpleCollectionAreaBase
  {
    bool IsPositionBasedCollection { get; }
    int PositionsAmount { get; }
    int VirtualPositionsAmount { get; }
  }

  public interface IUnChargeableCollectionAreaBase
  {
    ITrackingInstructionDataContractBase UnChargeElement(DateTime operationDate);
  }

  public interface IUnDischargeableCollectionAreaBase
  {
    void UnDischargeElement(ITrackingInstructionDataContractBase element, DateTime operationDate);
    bool TryUnDischargeVirtualElement(DateTime operationDate);
  }

  public interface IChargeableOnExitCollectionAreaBase
  {
    void ChargeElementOnExit(ITrackingInstructionDataContractBase element, DateTime operationDate);
  }

  public interface ICollectionMoveable
  {
    void MoveForward(DateTime operationDate);
    void MoveBackward();
  }
}
