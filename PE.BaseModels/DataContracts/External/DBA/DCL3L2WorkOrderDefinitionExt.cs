using System;
using System.Reflection;
using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.TransferModels;
using PE.BaseModels.DataContracts.Internal.DBA;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.External.DBA
{
  public class DCL3L2WorkOrderDefinitionExt : BaseExternalTelegram
  {
    /// <summary>
    ///   Primary Key in transfer table
    /// </summary>
    [DataMember]
    public long Counter { get; set; }

    /// <summary>
    ///   Time stamp when record has been inserted to transfer table
    /// </summary>
    [DataMember]
    public DateTime CreatedTs { get; set; }

    /// <summary>
    ///   Time stamp when record has been updated in transfer table (initially null)
    /// </summary>
    [DataMember]
    public DateTime? UpdatedTs { get; set; }

    /// <summary>
    ///   0 - new record, 1 - processed and OK, 2 - processed and Error, 3 - processed and rejected
    /// </summary>
    [DataMember]
    public short CommStatus { get; set; }

    /// <summary>
    ///   Unique work order name
    /// </summary>
    [DataMember]
    public string WorkOrderName { get; set; }

    /// <summary>
    ///   Customer name
    /// </summary>
    [DataMember]
    public string CustomerName { get; set; }

    /// <summary>
    ///   Target total weight of the products
    /// </summary>
    [DataMember]
    public string TargetWorkOrderWeight { get; set; }
    
    [DataMember]
    public string TargetWorkOrderWeightMax { get; set; }

    [DataMember]
    public string TargetWorkOrderWeightMin { get; set; }

    /// <summary>
    ///   steel grade code
    /// </summary>
    [DataMember]
    public string SteelgradeCode { get; set; }

    /// <summary>
    ///   simulation flag  - should be removed
    /// </summary>
    [DataMember]
    public bool AmISimulated { get; set; }

    [DataMember] public string CommMessage { get; set; }

    [DataMember] public string ValidationCheck { get; set; }

    [DataMember] public string ExternalWorkOrderName { get; set; }
    [DataMember] public string PreviousWorkOrderName { get; set; }
    [DataMember] public string OrderDeadline { get; set; }
    [DataMember] public string HeatName { get; set; }
    [DataMember] public string BilletWeight { get; set; }
    [DataMember] public string NumberOfBillets { get; set; }
    [DataMember] public string BundleWeightMin { get; set; }
    [DataMember] public string BundleWeightMax { get; set; }
    [DataMember] public string MaterialCatalogue { get; set; }
    [DataMember] public string ProductCatalogue { get; set; }
    [DataMember] public string InputThickness { get; set; }
    [DataMember] public string InputWidth { get; set; }
    [DataMember] public string BilletLength { get; set; }
    [DataMember] public string InputShapeSymbol { get; set; }
    [DataMember] public string OutputThickness { get; set; }
    [DataMember] public string OutputWidth { get; set; }
    [DataMember] public string OutputShapeSymbol { get; set; }

    public DCL3L2WorkOrderDefinitionExt()
    {
        
    }
    
    public DCL3L2WorkOrderDefinitionExt(L3L2WorkOrderDefinition data)
    {
      Counter = data.CounterId;
      CreatedTs = data.CreatedTs;
      UpdatedTs = data.UpdatedTs;
      CommStatus = data.CommStatus;
      CommMessage = data.CommMessage?.Trim();
      ValidationCheck = data.ValidationCheck?.Trim();


      WorkOrderName = data.WorkOrderName?.Trim();
      ExternalWorkOrderName = data.ExternalWorkOrderName?.Trim();
      PreviousWorkOrderName = data.PreviousWorkOrderName?.Trim();
      OrderDeadline = data.OrderDeadline?.Trim();
      HeatName = data.HeatName?.Trim();
      BilletWeight = data.BilletWeight?.Trim();
      NumberOfBillets = data.NumberOfBillets?.Trim();
      CustomerName = data.CustomerName?.Trim();
      BundleWeightMin = data.BundleWeightMin?.Trim();
      BundleWeightMax = data.BundleWeightMax?.Trim();
      TargetWorkOrderWeight = data.TargetWorkOrderWeight?.Trim();
      TargetWorkOrderWeightMin = data.TargetWorkOrderWeightMin?.Trim();
      TargetWorkOrderWeightMax = data.TargetWorkOrderWeightMax?.Trim();
      MaterialCatalogue = data.MaterialCatalogueName?.Trim();
      ProductCatalogue = data.ProductCatalogueName?.Trim();
      SteelgradeCode = data.SteelgradeCode?.Trim();
      InputThickness = data.InputThickness?.Trim();
      InputWidth = data.InputWidth?.Trim();
      BilletLength = data.BilletLength?.Trim();
      InputShapeSymbol = data.InputShapeSymbol?.Trim();
      OutputThickness = data.OutputThickness?.Trim();
      OutputWidth = data.OutputWidth?.Trim();
      OutputShapeSymbol = data.OutputShapeSymbol?.Trim();
      AmISimulated = WorkOrderName.Contains("Sim_");
    }

    public override string ToString()
    {
      string output = "Generated WO L3 telegram:\n";
      PropertyInfo[] properties = typeof(DCL3L2WorkOrderDefinitionExt).GetProperties();
      foreach (PropertyInfo prop in properties)
      {
        output += "\t" + prop.Name + ": " + prop.GetValue(this) + "\n";
      }

      return output;
    }

    public override DataContractBase ToInternal(int? telId = null)
    {
      return new DCL3L2WorkOrderDefinition
      {
        Counter = this.Counter,
        CreatedTs = this.CreatedTs,
        CommStatus = (CommStatus) CommStatus,
        
        WorkOrderName = this.WorkOrderName,
        ExternalWorkOrderName = this.ExternalWorkOrderName,
        PreviousWorkOrderName = this.PreviousWorkOrderName,
        OrderDeadline = this.OrderDeadline,
        HeatName = this.HeatName,
        BilletWeight = this.BilletWeight,
        NumberOfBillets = this.NumberOfBillets,
        CustomerName = this.CustomerName,
        BundleWeightMin = this.BundleWeightMin,
        BundleWeightMax = this.BundleWeightMax,
        TargetWorkOrderWeight = this.TargetWorkOrderWeight,
        TargetWorkOrderWeightMin = this.TargetWorkOrderWeightMin,
        TargetWorkOrderWeightMax = this.TargetWorkOrderWeightMax,
        MaterialCatalogueName = this.MaterialCatalogue,
        ProductCatalogueName = this.ProductCatalogue,
        SteelgradeCode = this.SteelgradeCode,
        InputThickness = this.InputThickness,
        InputWidth = this.InputWidth,
        BilletLength = this.BilletLength,
        InputShapeSymbol = this.InputShapeSymbol,
        OutputThickness = this.OutputThickness,
        OutputWidth = this.OutputWidth,
        OutputShapeSymbol = this.OutputShapeSymbol,
        AmISimulated = this.AmISimulated
      };
    }

    public override int ToExternal(DataContractBase dc)
    {
      DCL3L2WorkOrderDefinition internalDc = (DCL3L2WorkOrderDefinition)dc;
      
      this.Counter = internalDc.Counter;
      this.CommStatus = internalDc.CommStatus;
      this.CreatedTs = internalDc.CreatedTs;

      this.WorkOrderName = internalDc.WorkOrderName?.Trim();
      this.ExternalWorkOrderName = internalDc.ExternalWorkOrderName?.Trim();
      this.PreviousWorkOrderName = internalDc.PreviousWorkOrderName?.Trim();
      this.OrderDeadline = internalDc.OrderDeadline?.Trim();
      this.HeatName = internalDc.HeatName?.Trim();
      this.BilletWeight = internalDc.BilletWeight?.Trim();
      this.NumberOfBillets = internalDc.NumberOfBillets?.Trim();
      this.CustomerName = internalDc.CustomerName?.Trim();
      this.BundleWeightMin = internalDc.BundleWeightMin?.Trim();
      this.BundleWeightMax = internalDc.BundleWeightMax?.Trim();
      this.TargetWorkOrderWeight = internalDc.TargetWorkOrderWeight?.Trim();
      this.TargetWorkOrderWeightMin = internalDc.TargetWorkOrderWeightMin?.Trim();
      this.TargetWorkOrderWeightMax = internalDc.TargetWorkOrderWeightMax?.Trim();
      this.MaterialCatalogue = internalDc.MaterialCatalogueName?.Trim();
      this.ProductCatalogue = internalDc.ProductCatalogueName?.Trim();
      this.SteelgradeCode = internalDc.SteelgradeCode?.Trim();
      this.InputThickness = internalDc.InputThickness?.Trim();
      this.InputWidth = internalDc.InputWidth?.Trim();
      this.BilletLength = internalDc.BilletLength?.Trim();
      this.InputShapeSymbol = internalDc.InputShapeSymbol?.Trim();
      this.OutputThickness = internalDc.OutputThickness?.Trim();
      this.OutputWidth = internalDc.OutputWidth?.Trim();
      this.OutputShapeSymbol = internalDc.OutputShapeSymbol?.Trim();
      this.AmISimulated = internalDc.AmISimulated;
      
      return 0;
    }
  }
}
