using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Setup
{
  public class VM_SetupValues : VM_Base
  {
    #region ctor

    public VM_SetupValues()
    {
    }

    public VM_SetupValues(STPSetupInstruction data, List<VM_Filters> listOfFilters)
    {
      SetupId = data.FKSetupId;
      Value = data.Value;
      ListOfFilters = listOfFilters;
    }

    public VM_SetupValues(DbEntity.SPModels.SPSetupInstruction setup)
    {
      SetupInstructionId = setup.SetupInstructionId;
      InstructionDescription = String.IsNullOrEmpty(setup.InstructionDescription) ? "-" : setup.InstructionDescription;
      SetupId = setup.SetupId;
      InstructionId = setup.InstructionId;
      SetupName = setup.SetupName;
      InstructionName = setup.InstructionName;
      UnitSymbol = setup.UnitSymbol;
      SetupCode = setup.SetupCode;
      UnitSymbol = setup.UnitSymbol;
      Value = setup.Value;
      RangeFrom = setup.RangeFrom;
      RangeTo = setup.RangeTo;
      DataType = setup.DataType;
      IsRequired = setup.IsRequired;
      UnitId = setup.UnitId;
      AssetName = setup.AssetName;
      OrderSeq = setup.OrderSeq;
    }

    #endregion

    #region props

    public long SetupInstructionId { get; set; }
    public long InstructionId { get; set; }
    [SmfDisplay(typeof(VM_SetupValues), "SetupId", "NAME_SetupId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? SetupId { get; set; }
    [SmfDisplay(typeof(VM_SetupValues), "SetupName", "NAME_SetupName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SetupName { get; set; }
    [SmfDisplay(typeof(VM_SetupValues), "SetupDescription", "NAME_SetupDescription")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SetupDescription { get; set; }
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string InstructionDescription { get; set; }
    [SmfDisplay(typeof(VM_SetupValues), "SetupCode", "NAME_SetupCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SetupCode { get; set; }
    [SmfDisplay(typeof(VM_SetupValues), "SetupParameter", "NAME_SetupParameter")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SetupParameter { get; set; }
    [SmfDisplay(typeof(VM_SetupValues), "InstructionName", "NAME_Instruction")]
    public string InstructionName { get; set; }
    [SmfDisplay(typeof(VM_SetupValues), "UnitSymbol", "NAME_Unit")]
    public string UnitSymbol { get; set; }
    [SmfDisplay(typeof(VM_SetupValues), "Value", "NAME_Value")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [StringLength(255, ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    public string Value { get; set; }
    [SmfDisplay(typeof(VM_SetupValues), "RangeFrom", "NAME_From")]
    public double? RangeFrom { get; set; }
    [SmfDisplay(typeof(VM_SetupValues), "RangeTo", "NAME_To")]
    public double? RangeTo { get; set; }
    public string DataType { get; set; }
    [SmfDisplay(typeof(VM_SetupValues), "IsRequired", "NAME_Required")]
    public bool IsRequired { get; set; }
    public long UnitId { get; set; }
    [SmfDisplay(typeof(VM_SetupValues), "AssetName", "NAME_AssetName")]
    public string AssetName { get; set; }
    public long OrderSeq { get; set; }
    public List<VM_Filters> ListOfFilters { get; set; }

#endregion
  }
}
