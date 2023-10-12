using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PE.TRK.Base.Models.TrackingComponents.MaterialInfos.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Concrete;

namespace PE.TRK.Base.Models.TrackingComponents.MaterialInfos.Abstract
{
  public class MaterialInfoCollection
  {
    public readonly List<MaterialInfoBase> MaterialInfos = new List<MaterialInfoBase>();

    public virtual MaterialInfoBase this[long materialId]
    {
      get => GetMaterialInfo(materialId);
    }

    public virtual void RemoveMaterial(long materialId)
    {
      var materialInfo = GetMaterialInfo(materialId);

      if (materialInfo == null)
        throw new ArgumentException($"There is no material with Id: {materialId}");

      MaterialInfos.Remove(materialInfo);
    }

    public override bool Equals(object obj)
    {
      if(obj is MaterialInfoCollection objToCompare)
      {
        if (!MaterialInfos.Any())
          return false;

        return MaterialInfos.Select(x => x.MaterialId).Except(objToCompare.MaterialInfos.Select(x => x.MaterialId)).Count() == 0;
      }

      return base.Equals(obj);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    protected virtual MaterialInfoBase GetMaterialInfo(long materialId)
    {
      return MaterialInfos.FirstOrDefault(x => x.MaterialId == materialId);
    }

    public void ReplaceMaterialInfo(MaterialInfoBase materialInfoToRemove, MaterialInfoBase materialInfoToReplace)
    {
      MaterialInfos.Remove(materialInfoToRemove);
      MaterialInfos.Add(materialInfoToReplace);
    }
  }
}
