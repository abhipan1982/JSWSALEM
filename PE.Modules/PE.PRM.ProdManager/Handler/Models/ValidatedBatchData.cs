using System;

namespace PE.PRM.ProdManager.Handler.Models
{
  public class ValidatedBatchData
  {
    public string WorkOrderName { get; set; }
    public string ParentWorkorderName { get; set; }
    public string CustomerName { get; set; }
    public string HeatName { get; set; }
    public bool AmISimulated { get; set; }
    public string ExternalWorkOrderName { get; set; }
    public string PreviousWorkOrderName { get; set; }
    public DateTime OrderDeadline { get; set; }
    public double BilletWeight { get; set; }
    public short NumberOfBillets { get; set; }
    public double? BundleWeightMin { get; set; }
    public double? BundleWeightMax { get; set; }
    public double TargetWorkOrderWeight { get; set; }
    public string MaterialCatalogue { get; set; }
    public string ProductCatalogue { get; set; }
    public string SteelgradeCode { get; set; }
    public double InputThickness { get; set; }
    public string InputThicknessString { get; set; }
    public double? InputWidth { get; set; }
    public string InputWidthString { get; set; }
    
    public double? InputLength { get; set; }
    public string InputLengthString { get; set; }
    public string InputShapeSymbol { get; set; }
    public double OutputThickness { get; set; }
    public string OutputThicknessString { get; set; }
    public double? OutputWidth { get; set; }
    public string OutputWidthString { get; set; }
    public string OutputShapeSymbol { get; set; }
    public double InputWorkOrderWeight { get; set; }
    public DateTime L3CreatedTs { get; set; }
    public double TargetWorkOrderWeightMin { get; set; }
    public double TargetWorkOrderWeightMax { get; set; }
  }
}
