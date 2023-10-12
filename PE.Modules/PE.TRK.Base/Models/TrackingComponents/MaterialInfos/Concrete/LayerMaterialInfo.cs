using PE.TRK.Base.Models.TrackingComponents.MaterialInfos.Abstract;

namespace PE.TRK.Base.Models.TrackingComponents.MaterialInfos.Concrete
{
  public class LayerMaterialInfo : MaterialInfoBase
  {
    public short MaterialsSum { get; private set; }

    public LayerMaterialInfo(long materialId)
    : base(materialId)
    {
    }

    public LayerMaterialInfo(long materialId, short materialsSum)
    : base(materialId)
    {
      MaterialsSum = materialsSum;
    }

    public void SetMaterialsSum(short materialsSum)
    {
      MaterialsSum = materialsSum;
    }

    public void DecreaseMaterialsSum(short barsToRemove)
    {
      MaterialsSum -= barsToRemove;
    }
  }
}
