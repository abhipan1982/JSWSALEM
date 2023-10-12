using System;
using System.Collections.Generic;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.ViewModel.Module.Lite.MeasurementAnalysis
{
  public class VM_RawMaterialMeasurement : VM_Base
  {
    public VM_RawMaterialMeasurement()
    {
    }

    public VM_RawMaterialMeasurementMaterial Material { get; set; }
    public VM_RawMaterialMeasurementFeature Feature { get; set; }
    public VM_RawMaterialMeasurementMeasurement Measurement { get; set; }
  }

  public class VM_RawMaterialMeasurementMaterial
  {
    public long RawmMaterialId { get; set; }
    public string RawmMaterialName { get; set; }
  }

  public class VM_RawMaterialMeasurementFeature
  {
    public long FeatureId { get; set; }
    public string FeatureName { get; set; }
    public bool IsLengthRelated { get; set; }
    public bool IsSampledFeature { get; set; }
    public string Unit { get; set; }
  }

  public class VM_RawMaterialMeasurementMeasurement
  {
    public double? Min { get; set; }
    public double? Max { get; set; }
    public double? Avg { get; set; }
    public List<VM_RawMaterialMeasurementSample> Samples { get; set; }
    public DateTime? FirstMeasurementTs { get; set; }
    public DateTime? LastMeasurementTs { get; set; }
    public bool? IsValid { get; set; }
    public double ActualLength { get; set; }
  }

  public class VM_RawMaterialMeasurementSample
  {
    public double Length { get; set; }
    public DateTime? Date { get; set; }
    public double Value { get; set; }
  }

}
