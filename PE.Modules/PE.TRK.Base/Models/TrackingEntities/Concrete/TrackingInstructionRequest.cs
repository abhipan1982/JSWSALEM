using System;
using System.Collections.Generic;
using System.Text;
using PE.BaseDbEntity.EnumClasses;
using PE.TRK.Base.Models._Base;
using PE.TRK.Base.Models.TrackingEntities.Abstract;

namespace PE.TRK.Base.Models.TrackingEntities.Concrete
{
  public interface ITrackingInstructionRequest
  {
    DateTime OperationDate { get;}
    TrackingInstructionType InstructionType { get;}
    int Value { get;}
    ITrackingInstructionDataContractBase PreviousOperationResult { get; }
    int AreaAssetCode { get;}
    int? PointAssetCode { get;}
  }

  public class TrackingInstructionRequest : ITrackingInstructionRequest
  {
    public DateTime OperationDate { get; private set; }
    public TrackingInstructionType InstructionType { get; private set; }
    public int Value { get; private set; }
    public ITrackingInstructionDataContractBase PreviousOperationResult { get; private set; }
    public int AreaAssetCode { get; private set; }
    public int? PointAssetCode { get; private set; }

    public TrackingInstructionRequest(DateTime operationDate, 
      TrackingInstructionType instructionType, 
      int value,
      int areaAssetCode,
      int? pointAssetCode,
      ITrackingInstructionDataContractBase previousOperationResult = null)
    {
      OperationDate = operationDate;
      InstructionType = instructionType;
      Value = value;
      PreviousOperationResult = previousOperationResult;
      AreaAssetCode = areaAssetCode;
      PointAssetCode = pointAssetCode;
    }
  }

  public class TrackingInstructionWithCorrelationIdRequest
    : TrackingInstructionRequest
  {
    public TrackingCorrelation CorrelationId { get; private set; }

    public TrackingInstructionWithCorrelationIdRequest(TrackingCorrelation correlationId, DateTime operationDate, TrackingInstructionType instructionType, int value, int areaAssetCode, int? pointAssetCode, ITrackingInstructionDataContractBase previousOperationResult = null) 
      : base(operationDate, instructionType, value, areaAssetCode, pointAssetCode, previousOperationResult)
    {
      CorrelationId = correlationId;
    }

    //public virtual void SetCorrelationId(TrackingCorrelation correlationId)
    //{
    //  CorrelationId = correlationId;
    //}
  }
}
