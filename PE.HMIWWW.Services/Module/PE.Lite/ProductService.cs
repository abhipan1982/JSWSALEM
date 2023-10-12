using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Product;
using PE.HMIWWW.ViewModel.Module.Lite.Steelgrade;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class ProductService : BaseService, IProductService
  {
    private readonly PEContext _peContext;

    public ProductService(IHttpContextAccessor httpContextAccessor, PEContext peContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
    }

    public VM_ProductCatalogue GetProductCatalogue(long id)
    {
      VM_ProductCatalogue result = null;

      PRMProductCatalogue productCatalogue = _peContext.PRMProductCatalogues
        .Include(i => i.FKShape)
        .Include(i => i.FKProductCatalogueType)
        .SingleOrDefault(x => x.ProductCatalogueId == id);
      result = productCatalogue == null ? null : new VM_ProductCatalogue(productCatalogue);

      return result;
    }

    public DataSourceResult GetProductCatalogueList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _peContext.PRMProductCatalogues
        .Include(i => i.FKShape)
        .Include(i => i.FKProductCatalogueType)
        .ToDataSourceLocalResult(request, modelState, x => new VM_ProductCatalogue(x));

      return result;
    }

    public IList<VM_Steelgrade> GetSteelgradeList()
    {
      List<VM_Steelgrade> result = new List<VM_Steelgrade>();

      List<PRMSteelgrade> dbList = _peContext.PRMSteelgrades.ToList();
      foreach (PRMSteelgrade item in dbList)
      {
        result.Add(new VM_Steelgrade(item));
      }

      return result;
    }

    public IList<VM_Shape> GetShapeList()
    {
      List<VM_Shape> result = new List<VM_Shape>();

      List<PRMShape> dbList = _peContext.PRMShapes.ToList();
      foreach (PRMShape item in dbList)
      {
        result.Add(new VM_Shape(item));
      }

      return result;
    }


    public IList<VM_ProductCatalogueType> GetProductCatalogueTypeList()
    {
      List<VM_ProductCatalogueType> result = new List<VM_ProductCatalogueType>();
      List<PRMProductCatalogueType> dbList = _peContext.PRMProductCatalogueTypes.ToList();
      foreach (PRMProductCatalogueType item in dbList)
      {
        result.Add(new VM_ProductCatalogueType(item));
      }

      return result;
    }

    public async Task<VM_Base> CreateProductCatalogue(ModelStateDictionary modelState,
      VM_ProductCatalogue productCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref productCatalogue);

      DCProductCatalogue dcProductCatalogue = new DCProductCatalogue
      {
        Id = productCatalogue.Id,
        Name = productCatalogue.ProductCatalogueName,
        ProductExternalCatalogueName = productCatalogue.ProductExternalCatalogueName,
        Weight = productCatalogue.Weight,
        WeightMax = productCatalogue.WeightMax,
        WeightMin = productCatalogue.WeightMin,
        LastUpdateTs = productCatalogue.LastUpdateTs,
        Length = productCatalogue.Length,
        LengthMax = productCatalogue.LengthMax,
        LengthMin = productCatalogue.LengthMin,
        Width = productCatalogue.Width,
        WidthMax = productCatalogue.WidthMax,
        WidthMin = productCatalogue.WidthMin,
        Thickness = productCatalogue.Thickness,
        ThicknessMax = productCatalogue.ThicknessMax,
        ThicknessMin = productCatalogue.ThicknessMin,
        StdProductivity = productCatalogue.StdProductivity,
        StdMetallicYield = productCatalogue.StdMetallicYield,
        OvalityMax = productCatalogue.OvalityMax,
        ProductCatalogueDescription = productCatalogue.ProductCatalogueDescription,
        ShapeId = productCatalogue.ShapeId,
        TypeId = productCatalogue.TypeId

        //Steelgrade = productCatalogue.Steelgrade,
        //Shape = productCatalogue.Shape,
        //Type = productCatalogue.Type,
      };

      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.SendCreateProductCatalogueAsync(dcProductCatalogue);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> UpdateProductCatalogue(ModelStateDictionary modelState,
      VM_ProductCatalogue productCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref productCatalogue);

      DCProductCatalogue dcProductCatalogue = new DCProductCatalogue
      {
        Id = productCatalogue.Id,
        Name = productCatalogue.ProductCatalogueName,
        ProductExternalCatalogueName = productCatalogue.ProductExternalCatalogueName,
        Weight = productCatalogue.Weight,
        WeightMax = productCatalogue.WeightMax,
        WeightMin = productCatalogue.WeightMin,
        LastUpdateTs = productCatalogue.LastUpdateTs,
        Length = productCatalogue.Length,
        LengthMax = productCatalogue.LengthMax,
        LengthMin = productCatalogue.LengthMin,
        Width = productCatalogue.Width,
        WidthMax = productCatalogue.WidthMax,
        WidthMin = productCatalogue.WidthMin,
        Thickness = productCatalogue.Thickness,
        ThicknessMax = productCatalogue.ThicknessMax,
        ThicknessMin = productCatalogue.ThicknessMin,
        StdProductivity = productCatalogue.StdProductivity,
        StdMetallicYield = productCatalogue.StdMetallicYield,
        OvalityMax = productCatalogue.OvalityMax,
        ProductCatalogueDescription = productCatalogue.ProductCatalogueDescription,
        ShapeId = productCatalogue.ShapeId,
        TypeId = productCatalogue.TypeId

        //Steelgrade = productCatalogue.Steelgrade,
        //Shape = productCatalogue.Shape,
        //Type = productCatalogue.Type,
      };

      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.SendProductCatalogueAsync(dcProductCatalogue);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> DeleteProductCatalogue(ModelStateDictionary modelState,
      VM_ProductCatalogue productCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref productCatalogue);

      DCProductCatalogue dcProdCatalogue = new DCProductCatalogue { Id = productCatalogue.Id };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.SendDeleteProductCatalogueAsync(dcProdCatalogue);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public VM_ProductCatalogue GetProductDetails(ModelStateDictionary modelState, long id)
    {
      VM_ProductCatalogue result = null;

      if (id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return result;
      }

      PRMProductCatalogue data = _peContext.PRMProductCatalogues
        .Include(i => i.FKShape)
        .Include(i => i.FKProductCatalogueType)
        .Where(w => w.ProductCatalogueId == id)
        .SingleOrDefault();

      result = new VM_ProductCatalogue(data);

      return result;
    }

    public VM_ProductCatalogue GetProductCatalogueOverviewByWorkOrderId(ModelStateDictionary modelState, long workOrderId)
    {
      VM_ProductCatalogue result = null;

      if (workOrderId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return result;
      }

      PRMWorkOrder workOrder = _peContext.PRMWorkOrders
        .Include(x => x.FKProductCatalogue)
        .Include(x => x.FKProductCatalogue.FKShape)
        .Include(x => x.FKProductCatalogue.FKProductCatalogueType)
        .Where(x => x.WorkOrderId == workOrderId)
        .Single();

      result = new VM_ProductCatalogue(workOrder.FKProductCatalogue);

      return result;
    }
  }
}
