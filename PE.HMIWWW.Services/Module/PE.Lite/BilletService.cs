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
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.Product;
using PE.HMIWWW.ViewModel.Module.Lite.Steelgrade;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class BilletService : BaseService, IBilletService
  {
    /// <summary>
    /// Not Used
    /// </summary>
    private readonly PEContext _peContext;

    public BilletService(IHttpContextAccessor httpContextAccessor, PEContext peContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
    }

    public VM_MaterialCatalogue GetMaterialCatalogue(long id)
    {
      VM_MaterialCatalogue result = null;

      PRMMaterialCatalogue materialCatalogue = _peContext.PRMMaterialCatalogues
        .Include(x => x.FKMaterialCatalogueType)
        .SingleOrDefault(x => x.MaterialCatalogueId == id);
      result = materialCatalogue == null ? null : new VM_MaterialCatalogue(materialCatalogue);

      return result;
    }

    public DataSourceResult GetProductCatalogueList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _peContext.PRMMaterialCatalogues
        .Include(x => x.FKShape)
        .Include(x => x.FKMaterialCatalogueType)
        .ToDataSourceLocalResult(request, modelState, x => new VM_MaterialCatalogue(x));

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

    public async Task<VM_Base> UpdateMaterialCatalogue(ModelStateDictionary modelState,
      VM_MaterialCatalogue materialCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref materialCatalogue);

      DCMaterialCatalogue dcMaterialCatalogue = new DCMaterialCatalogue
      {
        MaterialCatalogueId = materialCatalogue.Id,
        MaterialCatalogueName = materialCatalogue.MaterialCatalogueName,
        ExternalMaterialCatalogueName = materialCatalogue.ExternalMaterialCatalogueName,
        Description = materialCatalogue.Description,
        MaterialCatalogueTypeId = materialCatalogue.TypeId,
        ShapeId = materialCatalogue.ShapeId,


        // Details
        LengthMin = materialCatalogue.LengthMin,
        LengthMax = materialCatalogue.LengthMax,
        WidthMin = materialCatalogue.WidthMin,
        WidthMax = materialCatalogue.WidthMax,
        ThicknessMin = materialCatalogue.ThicknessMin,
        ThicknessMax = materialCatalogue.ThicknessMax,
        WeightMin = materialCatalogue.WeightMin,
        WeightMax = materialCatalogue.WeightMax,
        TypeId = materialCatalogue.TypeId
      };


      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.SendMaterialCatalogueAsync(dcMaterialCatalogue);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> CreateMaterialCatalogue(ModelStateDictionary modelState,
      VM_MaterialCatalogue materialCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref materialCatalogue);

      DCMaterialCatalogue dcMaterialCatalogue = new DCMaterialCatalogue
      {
        MaterialCatalogueId = materialCatalogue.Id,
        MaterialCatalogueName = materialCatalogue.MaterialCatalogueName,
        ExternalMaterialCatalogueName = materialCatalogue.ExternalMaterialCatalogueName,
        Description = materialCatalogue.Description,
        ShapeId = materialCatalogue.ShapeId,
        // Details
        LengthMin = materialCatalogue.LengthMin,
        LengthMax = materialCatalogue.LengthMax,
        WidthMin = materialCatalogue.WidthMin,
        WidthMax = materialCatalogue.WidthMax,
        ThicknessMin = materialCatalogue.ThicknessMin,
        ThicknessMax = materialCatalogue.ThicknessMax,
        WeightMin = materialCatalogue.WeightMin,
        WeightMax = materialCatalogue.WeightMax,
        TypeId = materialCatalogue.TypeId,
        MaterialCatalogueTypeId = materialCatalogue.TypeId
      };

      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.SendCreateMaterialCatalogueAsync(dcMaterialCatalogue);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public VM_MaterialCatalogue GetBilletDetails(ModelStateDictionary modelState, long id)
    {
      VM_MaterialCatalogue result = null;

      if (id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return result;
      }

      PRMMaterialCatalogue data = _peContext.PRMMaterialCatalogues
        .Include(x => x.FKShape)
        .Include(x => x.FKMaterialCatalogueType)
        .Where(x => x.MaterialCatalogueId == id)
        .SingleOrDefault();

      result = new VM_MaterialCatalogue(data);

      return result;
    }

    public VM_MaterialCatalogue GetBilletCatalogueOverviewByWorkOrderId(ModelStateDictionary modelState, long workOrderId)
    {
      VM_MaterialCatalogue result = null;

      if (workOrderId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return result;
      }

      PRMWorkOrder workOrer = _peContext.PRMWorkOrders
        .Include(x => x.FKMaterialCatalogue)
        .Include(x => x.FKMaterialCatalogue.FKShape)
        .Include(x => x.FKMaterialCatalogue.FKMaterialCatalogueType)
        .Where(x => x.WorkOrderId == workOrderId)
        .Single();

      result = new VM_MaterialCatalogue(workOrer.FKMaterialCatalogue);

      return result;
    }

    public async Task<VM_Base> DeleteMaterialCatalogue(ModelStateDictionary modelState,
      VM_MaterialCatalogue materialCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref materialCatalogue);

      DCMaterialCatalogue dcMatCatalogue = new DCMaterialCatalogue { MaterialCatalogueId = materialCatalogue.Id };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.SendDeleteMaterialCatalogueAsync(dcMatCatalogue);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public IList<VM_MaterialCatalogueType> GetMaterialCatalogueTypeList()
    {
      List<VM_MaterialCatalogueType> result = new List<VM_MaterialCatalogueType>();

      List<PRMMaterialCatalogueType> dbList = _peContext.PRMMaterialCatalogueTypes.ToList();
      foreach (PRMMaterialCatalogueType item in dbList)
      {
        result.Add(new VM_MaterialCatalogueType(item));
      }

      return result;
    }

    public IList<VM_MaterialCatalogue> GetMaterialCataloguesByAnyFeaure(string text)
    {
      IList<VM_MaterialCatalogue> result = new List<VM_MaterialCatalogue>();

      if (!string.IsNullOrEmpty(text))
      {
        result = _peContext.PRMMaterialCatalogues
          .Where(m => m.MaterialCatalogueName.Contains(text)).AsEnumerable()
          .Select(mc => new VM_MaterialCatalogue(mc)).ToList();
      }

      return result;
    }
  }
}
