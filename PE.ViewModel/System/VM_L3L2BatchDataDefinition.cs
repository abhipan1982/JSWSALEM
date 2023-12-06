using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
using PE.DbEntity.EnumClasses;
using PE.DbEntity.TransferModels;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.System
{
  public class VM_L3L2BatchDataDefinition : VM_Base
  {
    #region ctor

    public VM_L3L2BatchDataDefinition() { }


    #region properties

    [Editable(false)]
    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "CounterId", "NAME_CounterId")]
    public virtual long CounterId { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "PO_NO", "NAME_PO_NO")]
    //[SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public virtual String PO_NO { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "BATCH_NO", "NAME_BATCH_NO")]
    //[SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public virtual String BATCH_NO { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "HEAT_NO", "NAME_HEAT_NO")]
    [SmfStringLength(50)]
    public string HEAT_NO { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "CUST_NAME", "NAME_CUST_NAME")]
    [SmfStringLength(50)]
    public string CUST_NAME { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "SO_QTY", "NAME_SO_QTY")]
    public Double SO_QTY { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "PSN", "NAME_PSN")]
    public String PSN { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "BLM_BRM_C_S_THICK", "NAME_BLM_BRM_C_S_THICK")]
    public float BLM_BRM_C_S_THICK { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "BLM_BRM_C_S_WIDTH", "NAME_BLM_BRM_C_S_WIDTH")]
    public float BLM_BRM_C_S_WIDTH { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "INPUT_MATERIAL", "NAME_INPUT_MATERIAL")]
    public string INPUT_MATERIAL { get; set; }


    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "BLM_BRM_WEIGHT", "NAME_BLM_BRM_WEIGHT")]
    public float BLM_BRM_WEIGHT { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "BLM_BRM_LENGTH", "NAME_BLM_BRM_LENGTH")]
    public float BLM_BRM_LENGTH { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "ROLLED_THICK", "NAME_ROLLED_THICK")]
    public float ROLLED_THICK { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "ROLLED_WIDTH", "NAME_ROLLED_WIDTH")]
    public float ROLLED_WIDTH { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "OUTPUT_MATERIAL", "NAME_OUTPUT_MATERIAL")]
    public string OUTPUT_MATERIAL { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "S_SIDE_TOL_MM_NEG", "NAME_S_SIDE_TOL_MM_NEG")]
    public float S_SIDE_TOL_MM_NEG { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "S_SIDE_TOL_MM_POS", "NAME_S_SIDE_TOL_MM_POS")]
    public float S_SIDE_TOL_MM_POS { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "S_OUT_OF_SQUARNESS_MM_MIN", "NAME_S_OUT_OF_SQUARNESS_MM_MIN")]
    public float S_OUT_OF_SQUARNESS_MM_MIN { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "S_OUT_OF_SQUARNESS_MM_MAX", "NAME_S_OUT_OF_SQUARNESS_MM_MAX")]
    public float S_OUT_OF_SQUARNESS_MM_MAX { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "S_DIA_TOL_MM_LOWER", "NAME_S_DIA_TOL_MM_LOWER")]
    public float S_DIA_TOL_MM_LOWER { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "S_DIA_TOL_MM_UPPAR", "NAME_S_DIA_TOL_MM_UPPAR")]
    public float S_DIA_TOL_MM_UPPAR { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "S_OVALITY_MM_MIN", "NAME_S_OVALITY_MM_MIN")]
    public float S_OVALITY_MM_MIN { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "S_OVALITY_MM_MAX", "NAME_S_OVALITY_MM_MAX")]
    public float S_OVALITY_MM_MAX { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "S_LENGTH_MM_MIN", "NAME_S_LENGTH_MM_MIN")]
    public float S_LENGTH_MM_MIN { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "S_LENGTH_MM_MAX", "NAME_S_LENGTH_MM_MAX")]
    public float S_LENGTH_MM_MAX { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "S_MULTIPLE_LENGTH_MM", "NAME_S_MULTIPLE_LENGTH_MM")]
    // [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public float S_MULTIPLE_LENGTH_MM { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "S_LENGTH", "NAME_S_LENGTH")]
    //[DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public float S_LENGTH { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "CHARGE_TYPE", "NAME_CHARGE_TYPE")]
    public short CHARGE_TYPE { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "COOL_TYPE", "NAME_COOL_TYPE")]
    public short COOL_TYPE { get; set; }


    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "LIFT_TYPE", "NAME_LIFT_TYPE")]
    public  short LIFT_TYPE { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "IsUpdated", "NAME_IsUpdated")]
    public bool IsUpdated { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "UpdatedTs", "NAME_UpdatedTs")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public virtual DateTime UpdatedTs { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "CreatedTs", "NAME_CreatedTs")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public virtual DateTime CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "AUDCreatedTs", "NAME_AUDCreatedTs")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public virtual DateTime AUDCreatedTs { get; set; }


    [SmfDisplay(typeof(VM_L3L2BatchDataDefinition), "CommMessage", "NAME_CommMessage")]
    public string CommMessage { get; set; }


    public virtual string ValidationCheck { get; set; }

    public virtual short? CommStatus { get; set; }

    #endregion

    public VM_L3L2BatchDataDefinition(L3L2BatchDataDefinition data)
    {
      BATCH_NO = data.BATCH_NO;
      PO_NO = data.PO_NO;
      CounterId = data.CounterId;
      HEAT_NO = data.HEAT_NO;
      CUST_NAME = data.CUST_NAME;
      SO_QTY = (double)data.SO_QTY;
      PSN = data.PSN;
      BLM_BRM_C_S_THICK = (float)data.BLM_BRM_C_S_THICK;
      BLM_BRM_C_S_WIDTH = (float)data.BLM_BRM_C_S_WIDTH;
      INPUT_MATERIAL = data.INPUT_MATERIAL;
      BLM_BRM_WEIGHT = (float)data.BLM_BRM_WEIGHT;
      BLM_BRM_LENGTH = (float)data.BLM_BRM_LENGTH;
      ROLLED_THICK = (float)data.ROLLED_THICK;
      ROLLED_WIDTH = (float)data.ROLLED_WIDTH;
      OUTPUT_MATERIAL =   data.OUTPUT_MATERIAL;
      S_SIDE_TOL_MM_NEG = (float)data.S_SIDE_TOL_MM_NEG;
      S_SIDE_TOL_MM_POS = (float)data.S_SIDE_TOL_MM_POS;
      S_OUT_OF_SQUARNESS_MM_MIN = (short)data.S_OUT_OF_SQUARNESS_MM_MIN;
      S_OUT_OF_SQUARNESS_MM_MAX = (float)data.S_OUT_OF_SQUARNESS_MM_MAX;
      S_DIA_TOL_MM_LOWER = (float)data.S_DIA_TOL_MM_LOWER;
     // S_DIA_TOL_MM_UPPER = data.S_DIA_TOL_MM_UPPER;
      S_OVALITY_MM_MIN = (float)data.S_OVALITY_MM_MIN;
      S_OVALITY_MM_MAX = (float)data.S_OVALITY_MM_MAX;
      S_LENGTH_MM_MIN = (float)data.S_LENGTH_MM_MIN;
      S_LENGTH_MM_MAX = (float)data.S_LENGTH_MM_MAX;
      S_MULTIPLE_LENGTH_MM = (float)data.S_MULTIPLE_LENGTH_MM;
      S_LENGTH = (float)data.S_LENGTH;
      CHARGE_TYPE = (short)data.CHARGE_TYPE;
      COOL_TYPE = (short)data.COOL_TYPE;
      LIFT_TYPE= (short)data.LIFT_TYPE;
      AUDCreatedTs=data.AUDCreatedTs;
      CreatedTs=data.CreatedTs;
      UpdatedTs=data.UpdatedTs;
      IsUpdated = data.IsUpdated;
      CommStatus = data.CommStatus;
      CommMessage = data.CommMessage;
      ValidationCheck = data.ValidationCheck;
    }

    #endregion

    
  }
}
