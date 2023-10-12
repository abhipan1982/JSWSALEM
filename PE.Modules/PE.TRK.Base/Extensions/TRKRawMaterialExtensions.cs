using System;
using PE.BaseDbEntity.Models;

namespace PE.TRK.Base.Extensions
{
  internal static class TRKRawMaterialExtensions
  {
    internal static void SetFKMaterial(this TRKRawMaterial rawMaterial, PRMMaterial l3Material)
    {
      if (rawMaterial.FKProduct != null) rawMaterial.FKProduct.FKWorkOrderId = l3Material?.FKWorkOrderId;

      rawMaterial.FKMaterialId = l3Material?.MaterialId;
      rawMaterial.FKMaterial = l3Material;
    }

    internal static void UnAssignProduct(this TRKRawMaterial rawMaterial)
    {
      if (rawMaterial.FKProduct != null)
      {
        rawMaterial.FKProduct.IsAssigned = false;
        rawMaterial.FKProduct.IsDummy = true;
        rawMaterial.FKProduct.FKWorkOrderId = null;
        rawMaterial.FKProduct.ProductName = "DummyProduct_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
      }

      rawMaterial.FKProductId = null;
      rawMaterial.FKProduct = null;
    }
  }
}
