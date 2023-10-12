using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SMF.Core.DC;
using PE.BaseDbEntity.EnumClasses;

namespace PE.BaseModels.DataContracts.Internal.L1A
{
  public class DcRelatedToMaterialMeasurementRequest  : DcMeasurementRequest
  {
    protected DcRelatedToMaterialMeasurementRequest()
    {
    }

    public DcRelatedToMaterialMeasurementRequest(long materialId, int featureCode, DateTime measurementFromDate, DateTime measurementToDate)
      : base(featureCode, measurementFromDate, measurementToDate)
    {
      MaterialId = materialId;
    }

    public DcRelatedToMaterialMeasurementRequest(long materialId, DcMeasurementRequest measurementRequest)
      : base(measurementRequest.FeatureCode, measurementRequest.MeasurementFromDate, measurementRequest.MeasurementToDate)
    {
      MaterialId = materialId;
    }

    [DataMember]
    public long MaterialId { get; set; }
  }

  public class DcAggregatedMeasurementRequest : DataContractBase
  {
    [DataMember]
    public List<DcMeasurementRequest> MeasurementListToProcess { get; set; } = new List<DcMeasurementRequest>();

    [DataMember]
    public int AreaAssetCode { get; set; }

    [DataMember]
    public long MaterialId { get; set; }
  }

  public class DcMeasurementRequest : DataContractBase
  {
    protected DcMeasurementRequest()
    {
    }

    /// <summary>
    ///   ctor
    /// </summary>
    /// <param name="featureCode"></param>
    /// <param name="measurementFromDate"></param>
    /// <param name="measurementToDate"></param>
    public DcMeasurementRequest(int featureCode, DateTime measurementFromDate, DateTime measurementToDate)
    {
      FeatureCode = featureCode;
      MeasurementFromDate = measurementFromDate;
      MeasurementToDate = measurementToDate;
    }

    [DataMember]
    public int FeatureCode { get; set; }

    [DataMember]
    public DateTime MeasurementFromDate { get; set; }

    [DataMember]
    public DateTime MeasurementToDate { get; set; }
  }

  public class DcMeasurementResponse : DataContractBase
  {
    [DataMember]
    public double? Min { get; set; }

    [DataMember]
    public double? Max { get; set; }

    [DataMember]
    public double? Avg { get; set; }
  }

  public class DcRawMeasurementResponse : DataContractBase
  {
    [DataMember]
    public List<DcMeasurement> Measurements { get; set; }
  }

  public class DcMeasurement
  {
    [DataMember]
    public int FeatureCode { get; set; }
    [DataMember]
    public List<DcMeasurementSample> MeasurementSamples { get; set; } = new List<DcMeasurementSample>();
  }

  public class DcMeasurementSample
  {
    [DataMember]
    public bool IsValid { get; set; }
    [DataMember]
    public DateTime MeasurementDate { get; set; }
    [DataMember]
    public double Value { get; set; }
  }

  public class DcReadTagActualValuesRequest : DataContractBase
  {
    [DataMember]
    public List<int> FeatureCodes { get; set; } = new List<int>();
  }
  
  public class DcReadTagActualValuesResponse : DataContractBase
  {
    [DataMember]
    public List<DcTagToReadResponse> TagsRead { get; set; } = new List<DcTagToReadResponse>();
  }

  public class DcTagToReadRequest : DataContractBase
  {
    [DataMember]
    public int FeatureCode { get; set; }
    [DataMember]
    public CommChannelType CommChannelType {  get; set; }
    [DataMember]
    public string CommAttr1 { get; set; }
    [DataMember]
    public string CommAttr2 { get; set; }
    [DataMember]
    public string CommAttr3 { get; set; }
  }

  public class DcTagToReadResponse : DataContractBase
  {
    [DataMember]
    public int FeatureCode { get; set; }
    [DataMember]
    public int Value { get; set; }
  }
}
