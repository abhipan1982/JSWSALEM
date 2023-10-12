using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.EnumClasses;
using System.Reflection;
using PE.DbEntity.TransferModels;

namespace PE.Models.DataContracts.Internal.DBA
{
  public class DCL3L2BatchData : DataContractBase
  {
    /// <summary>
    /// Primary Key in transfer table
    /// </summary>
    [DataMember]
    public long Counter { get; set; }

    /// <summary>
    /// Time stamp when record has been inserted to transfer table
    /// </summary>
    [DataMember]
    public DateTime CreatedTs { get; set; }

    /// <summary>
    /// Time stamp when record has been updated in transfer table (initially null)
    /// </summary>
    [DataMember]
    public DateTime? UpdatedTs { get; set; }

    /// <summary>
    /// 0 - new record, 1 - processed and OK, 2 - processed and Error, 3 - processed and rejected
    /// </summary>
    /// <summary>
    ///   0 - new record, 1 - processed and OK, 2 - processed and Error, 3 - processed and rejected
    /// </summary>
    [DataMember]
    public CustomCommStatus CommStatus { get; set; }

    [DataMember] public string ValidationCheck { get; set; }

    /// <summary>
    /// Unique work order name
    /// </summary>
    //[DataMember]
    public string WorkOrderName { get; set; }

    /// <summary>
    ///  unique product name, reference to Product Catalogue
    /// </summary>
    [DataMember]
    public string ProductName { get; set; }

    /// <summary>
    /// Date and time  when work order has been created in L3
    /// </summary>
    [DataMember]
    public DateTime L3Created { get; set; }

    /// <summary>
    /// Customer name
    /// </summary>
    [DataMember]
    public string CustomerName { get; set; }

    /// <summary>
    /// Deadline for completing of the production
    /// </summary>
    [DataMember]
    public DateTime ToBeReadyBefore { get; set; }

    /// <summary>
    /// Target total weight of the products
    /// </summary>
    [DataMember]
    public double TargetOrderWeight { get; set; }

    /// <summary>
    /// Minimum total weight of the products
    /// </summary>
    [DataMember]
    public double? TargetOrderWeightMin { get; set; }

    /// <summary>
    /// Minimum total weight of the products
    /// </summary>
    [DataMember]
    public double? TargetOrderWeightMax { get; set; }

    /// <summary>
    /// Heat name
    /// </summary>
    [DataMember]
    public string HeatName { get; set; }

    /// <summary>
    /// Number of associated raw materials (related to Heat) to be used
    /// </summary>
    [DataMember]
    public short RawMaterialNumber { get; set; }

    /// <summary>
    /// S - Square, B - Billet, R - Round, F - Flat
    /// </summary>
    [DataMember]
    public string RawMaterialShapeType { get; set; }

    /// <summary>
    /// Raw material thickness
    /// </summary>
    [DataMember]
    public double RawMaterialThickness { get; set; }

    /// <summary>
    /// Raw material width
    /// </summary>
    [DataMember]
    public double RawMaterialWidth { get; set; }

    /// <summary>
    /// Standard raw material length
    /// </summary>
    [DataMember]
    public double? RawMaterialLength { get; set; }

    /// <summary>
    /// Standard raw material weight
    /// </summary>
    [DataMember]
    public double RawMaterialWeight { get; set; }

    /// <summary>
    /// Type defined by customer, free text
    /// </summary>
    [DataMember]
    public string RawMaterialType { get; set; }

    /// <summary>
    /// External steel grade code (to be printed on label etc.)
    /// </summary>
    [DataMember]
    public string ExternalSteelGradeCode { get; set; }

    /// <summary>
    /// steel grade code
    /// </summary>
    [DataMember]
    public string SteelGradeCode { get; set; }

    /// <summary>
    /// Next aggregate to process output materials
    /// </summary>
    [DataMember]
    public string NextAggregate { get; set; }

    /// <summary>
    /// work order operation code
    /// </summary>
    [DataMember]
    public string OperationCode { get; set; }

    /// <summary>
    /// Customer's information about heating group
    /// </summary>
    [DataMember]
    public string HeatingGroup { get; set; }

    /// <summary>
    /// Customer's information about quality policy
    /// </summary>
    [DataMember]
    public string QualityPolicy { get; set; }

    /// <summary>
    /// Additional information to be printed on product labels
    /// </summary>
    [DataMember]
    public string ExtraLabelInformation { get; set; }

    /// <summary>
    /// temp flag to simulator L3 - can and should be removed
    /// </summary>
    [DataMember]
    public bool AmISimulated { get; set; }

    public DCL3L2BatchData()
    {

    }

    public override string ToString()
    {
      string output = "Generated WO L3 telegram:\n";
      PropertyInfo[] properties = typeof(DCL3L2BatchData).GetProperties();
      foreach (PropertyInfo prop in properties)
      {
        output += "\t" + prop.Name + ": " + prop.GetValue(this) + "\n";

      }
      return output;
    }
    public DataContractBase ToInternal(int? telId = null)
    {
      return new DCL3L2BatchData()
      {
        Counter = this.Counter,
        CreatedTs = this.CreatedTs,
        UpdatedTs = this.UpdatedTs,
        //CommStatus = (CustomCommStatus)CommStatus,
        //WorkOrderName = this.ProductName,
        CustomerName = this.CustomerName,
        //TargetOrderWeight = this.TargetOrderWeight,
        HeatName = this.HeatName,
        //RawMaterialShapeType = this.RawMaterialShapeType.ToString(),
        //RawMaterialThickness = this.RawMaterialThickness,
        //RawMaterialWidth = this.RawMaterialWidth,
        //RawMaterialLength = this.RawMaterialLength,
        //RawMaterialWeight = this.RawMaterialWeight,
        //RawMaterialType = this.RawMaterialType,
        //ExternalSteelGradeCode = this.ExternalSteelGradeCode,
        //SteelGradeCode = this.SteelGradeCode,
        //NextAggregate = this.NextAggregate,
        //OperationCode = this.OperationCode,
        //HeatingGroup = this.HeatingGroup,
        //QualityPolicy = this.QualityPolicy,
        //ExtraLabelInformation = this.ExtraLabelInformation,

        AmISimulated = this.AmISimulated,
      };
    }

    public int ToExternal(DataContractBase dc)
    {
      DCL3L2BatchData dCL3L2BatchData = (DCL3L2BatchData)dc;
      Counter = dCL3L2BatchData.Counter;
      CommStatus = dCL3L2BatchData.CommStatus;
      CreatedTs = dCL3L2BatchData.CreatedTs;
      WorkOrderName = dCL3L2BatchData.WorkOrderName?.Trim();
      //ExternalWorkOrderName = dCL3L2WorkOrderDefinition.ExternalWorkOrderName?.Trim();
      //PreviousWorkOrderName = dCL3L2WorkOrderDefinition.PreviousWorkOrderName?.Trim();
      //OrderDeadline = dCL3L2WorkOrderDefinition.OrderDeadline?.Trim();
      HeatName = dCL3L2BatchData.HeatName?.Trim();
      //BilletWeight = dCL3L2WorkOrderDefinition.BilletWeight?.Trim();
      //NumberOfBillets = dCL3L2WorkOrderDefinition.NumberOfBillets?.Trim();
      CustomerName = dCL3L2BatchData.CustomerName?.Trim();
      //BundleWeightMin = dCL3L2WorkOrderDefinition.BundleWeightMin?.Trim();
      //BundleWeightMax = dCL3L2WorkOrderDefinition.BundleWeightMax?.Trim();
      //TargetWorkOrderWeight = dCL3L2WorkOrderDefinition.TargetWorkOrderWeight?.Trim();
      //TargetWorkOrderWeightMin = dCL3L2WorkOrderDefinition.TargetWorkOrderWeightMin?.Trim();
      //TargetWorkOrderWeightMax = dCL3L2WorkOrderDefinition.TargetWorkOrderWeightMax?.Trim();
      //MaterialCatalogue = dCL3L2WorkOrderDefinition.MaterialCatalogueName?.Trim();
      //ProductCatalogue = dCL3L2WorkOrderDefinition.ProductCatalogueName?.Trim();
      //SteelgradeCode = dCL3L2WorkOrderDefinition.SteelgradeCode?.Trim();
      //InputThickness = dCL3L2WorkOrderDefinition.InputThickness?.Trim();
      //InputWidth = dCL3L2WorkOrderDefinition.InputWidth?.Trim();
      //BilletLength = dCL3L2WorkOrderDefinition.BilletLength?.Trim();
      //InputShapeSymbol = dCL3L2WorkOrderDefinition.InputShapeSymbol?.Trim();
      //OutputThickness = dCL3L2WorkOrderDefinition.OutputThickness?.Trim();
      //OutputWidth = dCL3L2WorkOrderDefinition.OutputWidth?.Trim();
      //OutputShapeSymbol = dCL3L2WorkOrderDefinition.OutputShapeSymbol?.Trim();
      AmISimulated = dCL3L2BatchData.AmISimulated;
      return 0;
    }

    public DCL3L2BatchData(L3L2BatchDataDefinition data)
    {
      Counter = data.CounterId;
      CreatedTs = data.CreatedTs;
      UpdatedTs = data.UpdatedTs;
      //CommStatus = (CustomCommStatus)data.CommStatus;
      //CommMessage = data.CommMessage?.Trim();
      ValidationCheck = data.ValidationCheck?.Trim();
      ProductName = data.PO_NO?.Trim();
      //ExternalWorkOrderName = data.ExternalWorkOrderName?.Trim();
      //PreviousWorkOrderName = data.PreviousWorkOrderName?.Trim();
      //OrderDeadline = data.OrderDeadline?.Trim();
      //HeatName = data.HeatName?.Trim();
      //BilletWeight = data.BilletWeight?.Trim();
      //NumberOfBillets = data.NumberOfBillets?.Trim();
      //CustomerName = data.CustomerName?.Trim();
      //BundleWeightMin = data.BundleWeightMin?.Trim();
      //BundleWeightMax = data.BundleWeightMax?.Trim();
      //TargetWorkOrderWeight = data.TargetWorkOrderWeight?.Trim();
      //TargetWorkOrderWeightMin = data.TargetWorkOrderWeightMin?.Trim();
      //TargetWorkOrderWeightMax = data.TargetWorkOrderWeightMax?.Trim();
      //MaterialCatalogue = data.MaterialCatalogueName?.Trim();
      //ProductCatalogue = data.ProductCatalogueName?.Trim();
      //SteelgradeCode = data.SteelgradeCode?.Trim();
      //InputThickness = data.InputThickness?.Trim();
      //InputWidth = data.InputWidth?.Trim();
      //BilletLength = data.BilletLength?.Trim();
      //InputShapeSymbol = data.InputShapeSymbol?.Trim();
      //OutputThickness = data.OutputThickness?.Trim();
      //OutputWidth = data.OutputWidth?.Trim();
      //OutputShapeSymbol = data.OutputShapeSymbol?.Trim();
      //AmISimulated = WorkOrderName.Contains("Sim_");
    }

  }
}
