using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.Adapter
{
  public class DCL2L3WorkOrderReport : DataContractBase
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
    /// Fabrication order
    /// [Required]
    /// </summary>
    [DataMember]
    public string WorkOrderName { get; set; }

    /// <summary>
    /// Operation number
    /// [Required]
    /// </summary>
    [DataMember]
    public string OperationNumber { get; set; }

    /// <summary>
    /// Production date (DDMMYYYY) (first material entering line)
    /// [Required]
    /// </summary>
    [DataMember]
    public string ProductionDate { get; set; }

    /// <summary>
    /// Shift number of the beginning
    /// [Required]
    /// </summary>
    [DataMember]
    public string ShiftNumber { get; set; }

    /// <summary>
    /// Production time = Rolling time (HHMMSS)
    /// [Required]
    /// </summary>
    [DataMember]
    public string ProductionTime { get; set; }

    /// <summary>
    /// Downtime attributable to the fabrication order (HHMMSS)
    /// [Required]
    /// </summary>
    [DataMember]
    public string DowntimeAttributableToTheFabricationOrder { get; set; }

    /// <summary>
    /// Downtime dispatchable to all the FO of the shift (HHMMSS)
    /// [Required]
    /// </summary>
    [DataMember]
    public string DowntimeDispatchableToAllTheFoOfTheShift { get; set; }

    /// <summary>
    /// Heat
    /// [Required]
    /// </summary>
    [DataMember]
    public string Heat { get; set; }

    /// <summary>
    /// Input weight - sum of measured billet weight
    /// [Required]
    /// </summary>
    [DataMember]
    public string InputWeight { get; set; }

    /// <summary>
    /// Number of entry pieces
    /// [kg]
    /// [Required]
    /// </summary>
    [DataMember]
    public string NumberOfEntryPieces { get; set; }

    /// <summary>
    /// ProductDimension
    /// [Required]
    /// </summary>
    [DataMember]
    public string ProductDimension { get; set; }

    /// <summary>
    /// Output weight - all rolled coils
    /// [kg]
    /// [Required]
    /// </summary>
    [DataMember]
    public string OutputWeight { get; set; }

    /// <summary>
    /// Number of output pieces - all rolled coils
    /// [Required]
    /// </summary>
    [DataMember]
    public string NumberOfOutputPieces { get; set; }

    /// <summary>
    /// Weight scrap or derivative product (Kg)
    /// [Required]
    /// </summary>
    [DataMember]
    public string WeightScrapOrDerivativeProductKg { get; set; }

    /// <summary>
    /// Number of scrapped billets
    /// [kg]
    /// [Required]
    /// </summary>
    [DataMember]
    public string NumberOfScrappedBillets { get; set; }

    /// <summary>
    /// End FO flag - if partial information then 0 (workshop closed)
    /// [Required]
    /// </summary>
    [DataMember]
    public string EndWOFlag { get; set; }

    /// <summary>
    /// Shape and dimension after furnace exit (Reject)
    /// </summary>
    [DataMember]
    public string MaterialFormDim1 { get; set; }

    /// <summary>
    /// manually filled by operator
    /// </summary>
    [DataMember]
    public string AutomaticOrderCreation1 { get; set; }

    /// <summary>
    /// Output weight removed of the furnace - billets not rolled
    /// [kg]
    /// </summary>
    [DataMember]
    public string OutputWeightRemovedOfTheFurnace1 { get; set; }

    /// <summary>
    /// Number of output pieces removed of the furnace - billets not rolled
    /// </summary>
    [DataMember]
    public string NumberOfOutputPiecesRemovedOfTheFurnace1 { get; set; }

    /// <summary>
    /// Shape and dimension after second pass of first rolling
    /// </summary>
    [DataMember]
    public string MaterialFormDim2 { get; set; }

    /// <summary>
    /// manually filled by operator
    /// </summary>
    [DataMember]
    public string AutomaticOrderCreation2 { get; set; }

    /// <summary>
    /// Output weight removed of the furnace - billets not rolled
    /// [kg]
    /// </summary>
    [DataMember]
    public string OutputWeightRemovedOfTheFurnace2 { get; set; }

    /// <summary>
    /// Number of output pieces removed of the furnace - billets not rolled
    /// </summary>
    [DataMember]
    public string NumberOfOutputPiecesRemovedOfTheFurnace2 { get; set; }

    /// <summary>
    /// after fourth pass
    /// </summary>
    [DataMember]
    public string MaterialFormDim3 { get; set; }

    /// <summary>
    /// manually filled by operator
    /// </summary>
    [DataMember]
    public string AutomaticOrderCreation3 { get; set; }

    /// <summary>
    /// Output weight removed of the furnace - billets not rolled
    /// [kg]
    /// </summary>
    [DataMember]
    public string OutputWeightRemovedOfTheFurnace3 { get; set; }

    /// <summary>
    /// Number of output pieces removed of the furnace - billets not rolled
    /// </summary>
    [DataMember]
    public string NumberOfOutputPiecesRemovedOfTheFurnace3 { get; set; }

    /// <summary>
    /// after sixth pass of rolling
    /// </summary>
    [DataMember]
    public string MaterialFormDim4 { get; set; }

    /// <summary>
    /// manually filled by operator
    /// </summary>
    [DataMember]
    public string AutomaticOrderCreation4 { get; set; }

    /// <summary>
    /// Output weight removed of the furnace - billets not rolled
    /// [kg]
    /// </summary>
    [DataMember]
    public string OutputWeightRemovedOfTheFurnace4 { get; set; }

    /// <summary>
    /// Number of output pieces removed of the furnace - billets not rolled
    /// </summary>
    [DataMember]
    public string NumberOfOutputPiecesRemovedOfTheFurnace4 { get; set; }

    /// <summary>
    /// Communication status, final result of message processing
    ///  0 - New entry, no validation errors
    ///  1 - Entry currently processed by system
    ///  2 - Entry processed by system, no errors
    ///  -1 - Validation errors(DB validation)
    ///  -2 - Entry processed by system, but processing errors occurred
    /// </summary>
    //[DataMember]
    //public PE.DbEntity.Enums.CommStatus CommStatus { get; set; }

    /// <summary>
    /// additional messages from subsequent processing modules
    /// </summary>
    [DataMember]
    public string CommMessage { get; set; }

    /// <summary>
    /// text describing current state of the message
    /// </summary>
    [DataMember]
    public string ValidationCheck { get; set; }
  }
}
