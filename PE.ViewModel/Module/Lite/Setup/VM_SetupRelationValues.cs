using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Setup
{
  public class VM_SetupRelationValues : VM_Base
  {
    public long InstructionId { get; set; }

    public string InstructionName { get; set; }

    [SmfDisplay(typeof(VM_SetupValues), "AssetName", "NAME_AssetName")]
    public string AssetName { get; set; }

    [SmfDisplay(typeof(VM_SetupValues), "UnitSymbol", "NAME_Unit")]
    public string UnitSymbol { get; set; }

    [SmfDisplay(typeof(VM_SetupValues), "Value", "NAME_SetupDisplayed")]
    public string DisplayedValue { get; set; }

    [SmfDisplay(typeof(VM_SetupValues), "Value", "NAME_SetupCurrent")]
    public string CurrentValue { get; set; }

    [SmfDisplay(typeof(VM_SetupValues), "Value", "NAME_SetupNext")]
    public string NextValue { get; set; }

    public string DataType { get; set; }
    public long UnitId { get; set; }
    public long OrderSeq { get; set; }

    #region ctor

    public VM_SetupRelationValues()
    {
    }

    //public VM_SetupRelationValues(V_SetupValue setup)
    //{
    //  AssetName = setup.AssetName;
    //  InstructionId = setup.InstructionId;
    //  InstructionName = setup.InstructionName;
    //  CurrentValue = setup.Value;

    //  UnitId = setup.UnitId;
    //  UnitSymbol = setup.UnitSymbol;
    //  DataType = setup.DataType;
    //}

    #endregion
  }
}
