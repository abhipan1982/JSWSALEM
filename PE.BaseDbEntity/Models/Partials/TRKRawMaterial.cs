using PE.BaseDbEntity.EnumClasses;

namespace PE.BaseDbEntity.Models
{
  public partial class TRKRawMaterial
  {
    public void Initialize()
    {
      EnumLayerStatus = LayerStatus.Undefined;
      EnumTypeOfScrap = TypeOfScrap.None;
      EnumRawMaterialStatus = RawMaterialStatus.Unassigned;
      EnumRejectLocation = RejectLocation.None;
      EnumChargeType = ChargeType.Undefined;
      EnumGradingSource = GradingSource.Undefined;
      EnumRawMaterialType = RawMaterialType.Material;
      EnumCuttingTip = CuttingTip.None;
      IsDeleted = false;
      ChildsNo = 0;
      IsDummy = false;
      IsVirtual = false;
      CuttingSeqNo = 0;
      IsBeforeCommit = false;
    }
  }
}
