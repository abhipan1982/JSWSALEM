using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PE.Models.DataContracts.Internal.DBA;
using PE.DbEntity.TransferModels;

namespace PE.Models.DataContracts.External.DBA
{
  public class DCL3L2BatchDataExt : DataContractBase
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
    [DataMember]
    public short? CommStatus { get; set; }

    [DataMember] public string CommMessage { get; set; }

    [DataMember] public string ValidationCheck { get; set; }

    /// <summary>
    /// Unique Production order name
    /// </summary>
    [DataMember]
    public string PoName { get; set; }

    /// <summary>
    /// Unique Batch name
    /// </summary>
    [DataMember]
    public string BatchNo { get; set; }

    /// <summary>
    /// Heat name
    /// </summary>
    [DataMember]
    public string HeatName { get; set; }

    /// <summary>
    /// Customer name
    /// </summary>
    [DataMember]
    public string CustomerName { get; set; }

    /// <summary>
    /// Target total weight of the products
    /// </summary>
    [DataMember]
    public double TargetOrderWeight { get; set; }

    /// <summary>
    ///  unique product name, reference to Product Catalogue
    /// </summary>
    [DataMember]
    public string Output_Material { get; set; }  

        

    /// <summary>
    /// Number of associated raw materials (related to Heat) to be used
    /// </summary>
    [DataMember]
    public short Input_Material { get; set; }

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
    /// simulation flag  - should be removed
    /// </summary>
    [DataMember]
    public bool AmISimulated { get; set; }


    public override string ToString()
    {
      string output = "Generated WO L3 telegram:\n";
      PropertyInfo[] properties = typeof(DCL3L2BatchDataExt).GetProperties();
      foreach (PropertyInfo prop in properties)
      {
        output += "\t" + prop.Name + ": " + prop.GetValue(this) + "\n";

      }
      return output;
    }
    public DataContractBase ToInternal(int? telId = null)
    {
      return new DCL3L2BatchDataExt()
      {
        Counter = this.Counter,
        CreatedTs = this.CreatedTs,
        UpdatedTs = this.UpdatedTs,
        //CommStatus = (PE.DbEntity.Enums.CommStatus)this.CommStatus,
        //WorkOrderName = this.PoName,       
        CustomerName = this.CustomerName,        
        TargetOrderWeight = this.TargetOrderWeight,       
        HeatName = this.HeatName,        
        RawMaterialShapeType = this.RawMaterialShapeType.ToString(),
        RawMaterialThickness = this.RawMaterialThickness,
        RawMaterialWidth = this.RawMaterialWidth,
        RawMaterialLength = this.RawMaterialLength,
        RawMaterialWeight = this.RawMaterialWeight,
        RawMaterialType = this.RawMaterialType,
        ExternalSteelGradeCode = this.ExternalSteelGradeCode,
        SteelGradeCode = this.SteelGradeCode,
        NextAggregate = this.NextAggregate,
        OperationCode = this.OperationCode,
        HeatingGroup = this.HeatingGroup,
        QualityPolicy = this.QualityPolicy,
        ExtraLabelInformation = this.ExtraLabelInformation,

        AmISimulated = this.AmISimulated,
      };

    }

    public DCL3L2BatchDataExt()
    {

    }

    public DCL3L2BatchDataExt(L3L2BatchDataDefinition data)
    {
      Counter = data.CounterId;
      CreatedTs = data.CreatedTs;
      UpdatedTs = data.UpdatedTs;
      CommStatus = data.CommStatus;
      CommMessage = data.CommMessage?.Trim();
      ValidationCheck = data.ValidationCheck?.Trim();
      PoName = data.PO_NO?.Trim();
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
