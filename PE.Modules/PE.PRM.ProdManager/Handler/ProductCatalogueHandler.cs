using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using com.wibu.xpm.AxpNet;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Macs;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.DbEntity.HmiModels;
using PE.DbEntity.Models;
using PE.DbEntity.PEContext;
using PE.Models.DataContracts.Internal.PRM;
using PE.PRM.Base.Handlers;

namespace PE.PRM.ProdManager.Handler
{
  public class ProductCatalogueHandler : ProductCatalogueHandlerBase
  {

    /// <summary>
    ///   Return product catalogue by his code or return default if not exist ( optional )
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="productCatalogueCode"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>

    public PRMProductCatalogueEXT CreateProductCatalogue(DCProductCatalogueEXT dc,long temp_id)
    {
      var productCatalogue = new PRMProductCatalogueEXT();
      //@av
      productCatalogue.FKProductCatalogueId = temp_id;
      productCatalogue.MinOvality = dc.MinOvality;
      productCatalogue.MinDiameter = dc.MinDiameter;
      productCatalogue.MaxDiameter = dc.MaxDiameter;
      productCatalogue.Diameter = dc.Diameter;
      productCatalogue.MinSquareness = dc.MinSquareness;
      productCatalogue.MaxSquareness = dc.MaxSquareness;
      productCatalogue.PosRcsSide = dc.PosRcsSide;
      productCatalogue.NegRcsSide = dc.NegRcsSide;


      return productCatalogue;
    }


    public PRMProductCatalogue CreateProductCatalogue1(DCProductCatalogueEXT dc)
    {
      var productCatalogue1 = new PRMProductCatalogue();
      // Width_Thickness_Shape
      productCatalogue1.ProductCatalogueName = dc.Name;
      productCatalogue1.Length = dc.Length.GetValueOrDefault();
      productCatalogue1.Width = dc.Width;
      productCatalogue1.Thickness = dc.Thickness;
      productCatalogue1.FKShapeId = dc.ShapeId;
      productCatalogue1.IsActive = true;

      productCatalogue1.FKProductCatalogueTypeId = dc.TypeId;

      productCatalogue1.ExternalProductCatalogueName = dc.ProductExternalCatalogueName;
      productCatalogue1.LengthMax = dc.LengthMax.GetValueOrDefault();
      productCatalogue1.LengthMin = dc.LengthMin.GetValueOrDefault();
      productCatalogue1.WidthMax = dc.WidthMax;
      productCatalogue1.WidthMin = dc.WidthMin;
      productCatalogue1.ThicknessMax = dc.ThicknessMax;
      productCatalogue1.ThicknessMin = dc.ThicknessMin;
      productCatalogue1.Weight = dc.Weight;
      productCatalogue1.WeightMax = dc.WeightMax;
      productCatalogue1.WeightMin = dc.WeightMin;
      productCatalogue1.MaxOvality = dc.OvalityMax;
      productCatalogue1.ProductCatalogueDescription = dc.ProductCatalogueDescription;
      productCatalogue1.StdProductivity = dc.StdProductivity;
      productCatalogue1.StdMetallicYield = dc.StdMetallicYield;

      return productCatalogue1;

    }






      public void UpdateProductCatalogue(PRMProductCatalogueEXT productCatalogue, PRMProductCatalogue productCatalogue1, DCProductCatalogueEXT dc)
    {
      if (productCatalogue1 != null)
      {
        productCatalogue1.ProductCatalogueName = dc.Name;
        productCatalogue1.ExternalProductCatalogueName = dc.ProductExternalCatalogueName;

        productCatalogue1.Length = dc.Length.GetValueOrDefault();
        productCatalogue1.LengthMax = dc.LengthMax.GetValueOrDefault();
        productCatalogue1.LengthMin = dc.LengthMin.GetValueOrDefault();
        productCatalogue1.Width = dc.Width;
        productCatalogue1.WidthMax = dc.WidthMax;
        productCatalogue1.WidthMin = dc.WidthMin;
        productCatalogue1.Thickness = dc.Thickness;
        productCatalogue1.ThicknessMax = dc.ThicknessMax;
        productCatalogue1.ThicknessMin = dc.ThicknessMin;
        productCatalogue1.Weight = dc.Weight;
        productCatalogue1.WeightMax = dc.WeightMax;
        productCatalogue1.WeightMin = dc.WeightMin;
        productCatalogue1.MaxOvality = dc.OvalityMax;
        productCatalogue1.ProductCatalogueDescription = dc.ProductCatalogueDescription;
        productCatalogue1.FKShapeId = dc.ShapeId;
        productCatalogue1.FKProductCatalogueTypeId = dc.TypeId;

        //productCatalogue.shapeId = dc.ShapeId;
        //productCatalogue.ProductCatalogueTypeID = dc.TypeId;

        productCatalogue1.StdProductivity = dc.StdProductivity;
        productCatalogue1.StdMetallicYield = dc.StdMetallicYield;
      }


       else if (productCatalogue != null)
        {
          //productCatalogue.FKProductCatalogueId = dc.Id;
          productCatalogue.MinOvality = dc.MinOvality;
          productCatalogue.MinDiameter = dc.MinDiameter;
          productCatalogue.MaxDiameter = dc.MaxDiameter;
          productCatalogue.Diameter = dc.Diameter;
          productCatalogue.MinSquareness = dc.MinSquareness;
          productCatalogue.MaxSquareness = dc.MaxSquareness;
          productCatalogue.PosRcsSide = dc.PosRcsSide;
          productCatalogue.NegRcsSide = dc.NegRcsSide;
        }

       
      
      else
      {
        throw new Exception { Source = "UpdateMaterialCatalogue - PC not found" };
      }
    }
















    public async Task<PRMProductCatalogue> GetProductCatalogueByNameEXTAsync1(PECustomContext ctx, string productCatalogueName,
     bool getDefault = false)
    {

      PRMProductCatalogue productCatalogue = string.IsNullOrWhiteSpace(productCatalogueName)
       ? null

       : await ctx.PRMProductCatalogues
          .Where(x => x.ProductCatalogueName.ToLower().Equals(productCatalogueName.ToLower()))
          .SingleOrDefaultAsync();

      if (productCatalogue == null && getDefault)
      {
        productCatalogue = await ctx.PRMProductCatalogues
          .Where(x => x.IsDefault)
          .SingleAsync();
      }

      return productCatalogue;
    }










    //public async Task<PRMProductCatalogueEXT> GetProductCatalogueByNameEXTAsync(PECustomContext ctx, long productId,
    //bool getDefault = false)
    //{
    //  PRMProductCatalogueEXT productCatalogue = 
       
    //    await ctx.PRMProductCatalogueEXTs.Where(x => x.FKProductCatalogueId == productId)
    //      .SingleOrDefaultAsync();



    //  if (productCatalogue == null && getDefault)
    //  {
    //    //productCatalogue = await ctx.PRMProductCatalogueEXTs
    //    //  .Where(x => x.IsDefault)
    //    //  .SingleAsync();
    //    return null;
    //  }

    //  return productCatalogue;
    //}










































    public async Task<PRMProductCatalogueEXT> GetProductCatalogueByIdEXTAsync(PECustomContext ctx, long productId,
      bool getDefault = false)
    {
      PRMProductCatalogueEXT productCatalogue =
        await ctx.PRMProductCatalogueEXTs.Where(x => x.FKProductCatalogueId == productId)
          .SingleOrDefaultAsync();

      if (productCatalogue == null && getDefault)
      {
        //productCatalogue = await ctx.PRMProductCatalogueEXTs
        //                    select ctx.
        //                   .Where(x => x.IsDefault).SingleAsync();
        return null;
      }

      return productCatalogue;
    }


     public async Task<PRMProductCatalogue> GetProductCatalogueByIdEXTAsync1(PECustomContext ctx, long productId, bool getDefault = false)
     {
        PRMProductCatalogue productCatalogue1 =
          await ctx.PRMProductCatalogues.Where(x => x.ProductCatalogueId == productId)
          .SingleOrDefaultAsync();

        if (productCatalogue1 == null && getDefault)
        {
          productCatalogue1 = await ctx.PRMProductCatalogues.Where(x => x.IsDefault).SingleAsync();
          return null;
        }

        return productCatalogue1;
     }
  }

}

