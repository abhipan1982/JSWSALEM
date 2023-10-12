using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.BaseModels.DataContracts.Internal.QTY;
using PE.BaseModels.DataContracts.Internal.TRK;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Asset;
using PE.HMIWWW.ViewModel.Module.Lite.Defect;
using PE.HMIWWW.ViewModel.Module.Lite.EventCalendar;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using SMF.Core.Communication;
using SMF.Core.DC;
using PE.DbEntity.PEContext;
using Microsoft.Data.SqlClient;
using System.Data;
using PE.DbEntity.EFCoreExtensions;
using PE.Core;
using PE.HMIWWW.ViewModel.Module.Lite.Inspection;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class RawMaterialService : BaseService, IRawMaterialService
  {
    private readonly PEContext _peContext;
    private readonly HmiContext _hmiContext;

    public RawMaterialService(IHttpContextAccessor httpContextAccessor, PEContext peContext, HmiContext hmiContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
      _hmiContext = hmiContext;
    }

    public VM_RawMaterialOverview GetRawMaterialById(long? materialId)
    {
      TRKRawMaterial material = _peContext.TRKRawMaterials
        .SingleOrDefault(x => x.RawMaterialId == materialId);

      if (material != null)
      {
        TRKRawMaterialRelation parentRelation = _peContext.TRKRawMaterialRelations
          .Include(x => x.ParentRawMaterial)
          .FirstOrDefault(x => x.ChildRawMaterialId == materialId);

        return new VM_RawMaterialOverview(material, parentRelation?.ParentRawMaterial);
      }

      return null;
    }

    public DataSourceResult GetRawMaterialSearchList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _hmiContext.V_RawMaterialSearchGrids.ToDataSourceLocalResult(request, modelState, data => new VM_RawMaterialsTree(data));

      return result;
    }

    public DataSourceResult GetMeasurementSearchList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _hmiContext.V_MeasurementSearchGrids.ToDataSourceLocalResult(request, modelState, data => new VM_RawMaterialsTree(data));

      return result;
    }

    public DataSourceResult GetNotAssignedRawMaterials(ModelStateDictionary modelState, DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _hmiContext.V_RawMaterialSearchGrids
        .Where(x => !x.MaterialIsAssigned)
        .OrderByDescending(x => x.RawMaterialId)
        .ToDataSourceLocalResult(request, modelState, data => new VM_RawMaterialsTree(data));

      return result;
    }

    public VM_RawMaterialGenealogy GetRawMaterialGenealogy(ModelStateDictionary modelState, long rawMaterialId)
    {
      VM_RawMaterialGenealogy result = null;

      if (rawMaterialId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      var data = FindRawMaterialGenealogy(_hmiContext, rawMaterialId);
      var current = data.First(x => x.RawMaterialId == rawMaterialId);

      var parent = !current.ParentRawMaterialId.HasValue
        ? current
        : data.FirstOrDefault(x => x.RawMaterialId == current.ParentRawMaterialId);

      var children = data.Where(x => x.ParentRawMaterialId == parent.RawMaterialId).ToList();

      return new VM_RawMaterialGenealogy()
      {
        Id = current.RawMaterialId,
        Name = current.RawMaterialName,
        Children = children.Select(x => new VM_RawMaterialGenealogyElement(x)).ToList(),
        Parent = new VM_RawMaterialGenealogyElement(parent)
      };
    }

    public VM_RawMaterialOverview GetRawMaterialDetails(ModelStateDictionary modelState, long rawMaterialId)
    {
      VM_RawMaterialOverview result = null;

      if (rawMaterialId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      TRKRawMaterial material = _peContext.TRKRawMaterials
        .Single(x => x.RawMaterialId == rawMaterialId);

      TRKRawMaterialRelation parentRelation = _peContext.TRKRawMaterialRelations
        .Include(x => x.ParentRawMaterial)
        .FirstOrDefault(x => x.ChildRawMaterialId == rawMaterialId);

      result = new VM_RawMaterialOverview(material, parentRelation?.ParentRawMaterial);

      return result;
    }

    public VM_RawMaterialMeasurements GetMeasurementDetails(ModelStateDictionary modelState, long measurementId)
    {
      VM_RawMaterialMeasurements result = null;

      if (measurementId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      V_RawMaterialMeasurement data = _hmiContext.V_RawMaterialMeasurements
        .Where(x => x.MeasurementId == measurementId)
        .FirstOrDefault();

      result = new VM_RawMaterialMeasurements(data);

      // TODOMN: For all of units not only custom - refresh should be on application start - on production once new unit has come - there should be anyway a restart of application
      // once client would like to have such option - we will prepare an API GET call, which can be called from database - and it will refresh the cache.
      // FeatureUnitConverterHelper.ClearCustomUnitsList();

      return result;
    }

    public VM_RawMaterialHistory GetHistoryDetails(ModelStateDictionary modelState, long rawMaterialStepId)
    {
      VM_RawMaterialHistory result = null;

      if (rawMaterialStepId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      V_RawMaterialHistory data = _hmiContext.V_RawMaterialHistories
        .Where(x => x.RawMaterialStepId == rawMaterialStepId)
        .FirstOrDefault();

      result = new VM_RawMaterialHistory(data);


      return result;
    }

    public VM_L3MaterialData GetL3MaterialData(ModelStateDictionary modelState, long rawMaterialId)
    {
      VM_L3MaterialData result = null;

      if (!modelState.IsValid)
      {
        return result;
      }
      long? materialId = _peContext.TRKRawMaterials.Where(y => y.RawMaterialId == rawMaterialId).Select(x => x.FKMaterialId)
        .FirstOrDefault();
      if (materialId != null)
      {
        result = new VM_L3MaterialData(_peContext.PRMMaterials.Where(x => x.MaterialId == materialId)
          .Include(y => y.FKWorkOrder)
          .Include(y => y.FKWorkOrder.FKHeat)
          .Include(y => y.FKWorkOrder.FKMaterialCatalogue)
          .Include(y => y.FKWorkOrder.FKSteelgrade).FirstOrDefault(), rawMaterialId);
      }
      else
      {
        result = new VM_L3MaterialData(rawMaterialId);
      }

      return result;
    }

    public async Task<VM_Base> AssignRawMaterial(ModelStateDictionary modelState, long rawMaterialId,
      long l3MaterialId)
    {
      VM_Base result = null;

      if (rawMaterialId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      DCMaterialAssign dataToSend = new DCMaterialAssign { RawMaterialId = rawMaterialId, L3MaterialId = l3MaterialId };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendAssignMaterial(dataToSend);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> UnassignRawMaterial(ModelStateDictionary modelState, long rawMaterialId)
    {
      VM_Base result = null;

      if (rawMaterialId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      DCMaterialUnassign dataToSend = new DCMaterialUnassign(rawMaterialId);

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendUnassignMaterial(dataToSend);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public DataSourceResult GetMeasurmentsByRawMaterialId(ModelStateDictionary modelState, DataSourceRequest request,
      long rawMaterialId)
    {
      DataSourceResult result = null;
      TRKRawMaterial material = _peContext.TRKRawMaterials
        .First(x => x.RawMaterialId == rawMaterialId);

      var parentRelation = _peContext.TRKRawMaterialRelations
        .FirstOrDefault(x => x.ChildRawMaterialId == rawMaterialId);

      IQueryable<V_RawMaterialMeasurement> list;

      if (parentRelation is not null)
      {
        list = _hmiContext.V_RawMaterialMeasurements
          .Where(x => x.RawMaterialId == rawMaterialId || x.RawMaterialId == parentRelation.ParentRawMaterialId)
          .AsQueryable();
      }
      else
      {
        list = _hmiContext.V_RawMaterialMeasurements.Where(x => x.RawMaterialId == rawMaterialId).AsQueryable();
      }

      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_RawMaterialMeasurements(data));

      // TODOMN: For all of units not only custom - refresh should be on application start - on production once new unit has come - there should be anyway a restart of application
      // once client would like to have such option - we will prepare an API GET call, which can be called from database - and it will refresh the cache.
      // FeatureUnitConverterHelper.ClearCustomUnitsList();

      return result;
    }

    public DataSourceResult GetHistoryByRawMaterialId(ModelStateDictionary modelState, DataSourceRequest request,
      long rawMaterialId)
    {
      IQueryable<V_RawMaterialHistory> list = _hmiContext.V_RawMaterialHistories.Where(x => x.RawMaterialId == rawMaterialId)
        .AsQueryable();

      V_Asset asset = new V_Asset();

      return list.ToDataSourceLocalResult(request, modelState, data => new VM_RawMaterialHistory(data));
    }

    public List<VM_DefectCatalogue> GetDefectsList()
    {
      List<VM_DefectCatalogue> result = new List<VM_DefectCatalogue>();
      IQueryable<QTYDefectCatalogue> dbList = _peContext.QTYDefectCatalogues.AsQueryable();
      foreach (QTYDefectCatalogue item in dbList)
      {
        result.Add(new VM_DefectCatalogue(item));
      }

      return result;
    }

    public async Task<VM_Base> AssignRawMaterialQualityAsync(ModelStateDictionary modelState, VM_QualityAssignment rawMaterialQuality)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      //UnitConverterHelper.ConvertToSi(ref defectCatalogue);

      DCQualityAssignment dcQualityAssignment = new DCQualityAssignment
      {
        DefectCatalogueElementIds = rawMaterialQuality.DefectIds,
        Remark = rawMaterialQuality.Remark,
        AssetId = rawMaterialQuality.AssetId.Value
      };

      if (dcQualityAssignment.RawMaterialIds is null)
      {
        dcQualityAssignment.RawMaterialIds = new List<long>();
      }

      dcQualityAssignment.RawMaterialIds.Add(rawMaterialQuality.Id);


      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.AssignQualityAsync(dcQualityAssignment);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public IList<VM_Asset> GetAssets()
    {
      List<VM_Asset> result = new List<VM_Asset>();
      foreach (V_Asset item in _hmiContext.V_Assets.Where(x => !x.IsArea).AsQueryable())
      {
        result.Add(new VM_Asset(item));
      }

      return result;
    }

    public DataSourceResult GetRawMaterialEvents(ModelStateDictionary modelState, DataSourceRequest request,
      long rawMaterialId)
    {
      IQueryable<V_Event> result = _hmiContext.V_Events.AsNoTracking().Where(x => x.RawMaterialId == rawMaterialId);
      return result.ToDataSourceLocalResult(request, modelState, data => new VM_EventCalendar(data));
    }

    public VM_RawMaterialGenealogy GetMaterialDivisionHistory(ModelStateDictionary modelState, long rawMaterialId)
    {
      VM_RawMaterialGenealogy result = null;

      if (rawMaterialId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      SqlParameter[] parameters = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@RawMaterialId",
                            SqlDbType =  SqlDbType.BigInt,
                            Direction = ParameterDirection.Input,
                            Value = rawMaterialId
                        }};

      var data = _hmiContext.ExecuteSPRawMaterialGenealogy(parameters);
      var current = data.First(x => x.RawMaterialId == rawMaterialId);

      var parent = !current.ParentRawMaterialId.HasValue
        ? current
        : data.FirstOrDefault(x => x.RawMaterialId == current.ParentRawMaterialId);

      var children = data.Where(x => x.ParentRawMaterialId == parent.RawMaterialId).ToList();

      return new VM_RawMaterialGenealogy
      {
        Id = current.RawMaterialId,
        Name = current.RawMaterialName,
        Dividable = true,
        Children = children.Select(x => new VM_RawMaterialGenealogyElement(x) { Dividable = false }).ToList(),
        Parent = new VM_RawMaterialGenealogyElement(parent)
      };
    }

    public VM_RawMaterialGenealogy GetMaterialForReadyOperation(ModelStateDictionary modelState, long rawMaterialId)
    {
      VM_RawMaterialGenealogy result = null;

      if (rawMaterialId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }
      var data = FindRawMaterialGenealogy(_hmiContext, rawMaterialId);
      var current = data.First(x => x.RawMaterialId == rawMaterialId);

      return new VM_RawMaterialGenealogy(current);
    }

    public IList<PRMProductCatalogueType> GetProductCatalogueTypes()
    {
      return _peContext.PRMProductCatalogueTypes
        .Where(x => x.ProductCatalogueTypeCode.ToUpper().Equals(Constants.Bar.ToUpper())
          || x.ProductCatalogueTypeCode.ToUpper().Equals(Constants.WireRod.ToUpper())
          || x.ProductCatalogueTypeCode.ToUpper().Equals(Constants.Garret.ToUpper()))
        .ToList();
    }

    private List<DbEntity.SPModels.SPRawMaterialGenealogy> FindRawMaterialGenealogy(HmiContext ctx, long rawMaterialId)
    {
      SqlParameter[] parameters = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@RawMaterialId",
                            SqlDbType =  SqlDbType.BigInt,
                            Direction = ParameterDirection.Input,
                            Value = rawMaterialId
                        }};

      return ctx.ExecuteSPRawMaterialGenealogy(parameters);
    }

    public DataSourceResult GetDefectListByRawMaterialId(ModelStateDictionary modelState, DataSourceRequest request, long rawMaterialId)
    {
      return _peContext.QTYDefects
        .Include(d => d.FKAsset)
        .Include(d => d.FKDefectCatalogue)
        .Where(d => d.FKRawMaterialId == rawMaterialId)
        .ToDataSourceLocalResult(request, modelState, d => new VM_Defect(d));
    }
  }
}
