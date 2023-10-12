using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.QTY;
using PE.BaseModels.DataContracts.Internal.ZPC;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.Products;
using SMF.Core.Communication;
using SMF.Core.DC;
using PE.DbEntity.PEContext;
using SMF.HMIWWW.UnitConverter;
using Microsoft.CodeAnalysis;
using PE.Interfaces.Modules;
using PE.BaseModels.DataContracts.Internal.TRK;
using System;
using PE.BaseModels.DataContracts.Internal.PRM;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class ProductsService : BaseService, IProductsService
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;

    public ProductsService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    public VM_ProductsOverview GetProductsDetails(ModelStateDictionary modelState, long productId)
    {
      VM_ProductsOverview result = null;

      if (productId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }
      V_ProductOverview product = _hmiContext.V_ProductOverviews
        .Where(x => x.ProductId == productId)
        .Single();

      result = new VM_ProductsOverview(product);

      return result;
    }

    public DataSourceResult GetProductsSearchList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      IQueryable<V_ProductSearchGrid> workOrderList = _hmiContext.V_ProductSearchGrids.AsQueryable();
      return workOrderList.ToDataSourceLocalResult(request, modelState, data => new VM_ProductsList(data));
    }

    public async Task<Stream> RequestPdfFromZebraWebServiceForHmiAsync(ModelStateDictionary modelState,
      long productId = 1)
    {
      //PREPARE EXIT OBJECT
      Stream returnStream = default;

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return returnStream;
      }

      if (productId < 1)
      {
        return returnStream;
      }

      //END OF VALIDATION

      DCZebraRequest entryDataContract = new DCZebraRequest();

      entryDataContract.Id = productId;

      //SendOfficeResult<DCZebraResponse> sendOfficeResult = await HmiSendOffice.RequestPDFFromZebraAsync(entryDataContract);

      ////handle warning information
      //HandleWarnings(sendOfficeResult, ref modelState);

      //if (sendOfficeResult.OperationSuccess)
      //  returnStream = new MemoryStream(sendOfficeResult.DataConctract.Picture);

      return returnStream;
    }

    public async Task<VM_Base> AssignProductQualityAsync(ModelStateDictionary modelState, long productId, short quality,
      List<long> defectIds)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      //UnitConverterHelper.ConvertToSi(ref defectCatalogue);

      DCQualityAssignment dcQualityAssignment = new DCQualityAssignment();
      dcQualityAssignment.DefectCatalogueElementIds = defectIds;
      if (dcQualityAssignment.ProductIds is null)
        dcQualityAssignment.ProductIds = new List<long>();
      dcQualityAssignment.ProductIds.Add(productId);
      dcQualityAssignment.QualityFlag = ProductQuality.GetValue(quality);


      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.AssignQualityAsync(dcQualityAssignment);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public DataSourceResult GetMaterialsListByProductId(ModelStateDictionary modelState, DataSourceRequest request, long productId)
    {
      List<long?> rawMaterialsList = _peContext.TRKRawMaterials.
        Where(x => x.FKMaterialId != null && x.FKProductId == productId)
        .Select(x => x.FKMaterialId)
        .ToList();

      IQueryable<PRMMaterial> materialsList = _peContext.PRMMaterials
        .Where(x => rawMaterialsList
        .Contains(x.MaterialId))
        .Include(x => x.FKWorkOrder)
        .AsQueryable();

      return materialsList.ToDataSourceLocalResult<PRMMaterial, VM_MaterialOverview>(request, modelState, data => new VM_MaterialOverview(data));
    }

    public async Task<VM_Base> CreateBundleAsync(ModelStateDictionary modelState, VM_Bundle data)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref data);

      var dc = new DCBundleData
      {
        WorkOrderId = data.WorkOrderId,
        OverallWeight = data.Weight,
        BarsCounter = data.BarsNumber,
        BundleWeightSource = WeightSource.Operator,
        KeepInTracking = data.KeepInTracking,
        Date = DateTime.Now
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.CreateBundleAsync(dc);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Bundle> GetNewBundleDataAsync(ModelStateDictionary modelState, long workOrderId)
    {
      VM_Bundle result = new();

      var workOrder = await _peContext.PRMWorkOrders
        .Include(x => x.FKProductCatalogue)
        .FirstAsync(x => x.WorkOrderId == workOrderId);

      if (workOrder.EnumWorkOrderStatus >= WorkOrderStatus.Finished)
      {
        modelState.AddModelError("error", "WorkOrderStatusPreventsBundleCreation");
      }

      if (!modelState.IsValid)
        return result;

      result.WorkOrderId = workOrderId;
      result.Weight = workOrder.FKProductCatalogue.Weight ?? 0;

      //return view model
      return result;
    }
  }
}
