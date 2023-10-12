using System.Linq;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.DbEntity.PEContext;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class MaterialService : BaseService, IMaterialService
  {
    private readonly PEContext _peContext;
    private readonly HmiContext _hmiContext;

    public MaterialService(IHttpContextAccessor httpContextAccessor, PEContext peContext, HmiContext hmiContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
      _hmiContext = hmiContext;
    }

    public VM_MaterialOverview GetMaterialById(long? materialId)
    {
      PRMMaterial material = _peContext.PRMMaterials.Where(x => x.MaterialId == materialId).SingleOrDefault();

      if (material != null)
      {
        return new VM_MaterialOverview(material);
      }

      return null;
    }

    public DataSourceResult GetMaterialSearchList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      IQueryable<V_MaterialSearchGrid> materialList = _hmiContext.V_MaterialSearchGrids.AsQueryable();
      return materialList.ToDataSourceLocalResult(request, modelState, data => new VM_MaterialOverview(data));
    }

    public VM_MaterialOverview GetMaterialDetails(ModelStateDictionary modelState, long materialId)
    {
      VM_MaterialOverview result = null;

      if (materialId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      PRMMaterial material = _peContext.PRMMaterials
        .Include(i => i.FKHeat)
        .Include(i => i.FKWorkOrder)
        .Include(i => i.FKWorkOrder.FKMaterialCatalogue)
        .Include(i => i.FKWorkOrder.FKMaterialCatalogue.FKShape)
        .Include(i => i.FKWorkOrder.FKMaterialCatalogue.FKMaterialCatalogueType)
        .Include(i => i.FKWorkOrder.FKSteelgrade)
        .Include(i => i.TRKRawMaterials)
        .Where(x => x.MaterialId == materialId)
        .Single();

      result = new VM_MaterialOverview(material);

      return result;
    }

    public DataSourceResult GetNotAssignedMaterials(ModelStateDictionary modelState, DataSourceRequest request)
    {
      IQueryable<V_MaterialSearchGrid> materialList = _hmiContext.V_MaterialSearchGrids
        .Where(x => !x.MaterialIsAssigned)
        .OrderByDescending(x => x.MaterialId)
        .AsQueryable();
      return materialList.ToDataSourceLocalResult(request, modelState, data => new VM_MaterialOverview(data));
    }
  }
}
