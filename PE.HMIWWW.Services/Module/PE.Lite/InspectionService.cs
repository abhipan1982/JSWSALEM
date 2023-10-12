using System.Collections.Generic;
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
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Extensions;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Asset;
using PE.HMIWWW.ViewModel.Module.Lite.Defect;
using PE.HMIWWW.ViewModel.Module.Lite.Inspection;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.QualityInspection;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;
using PE.DbEntity.PEContext;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class InspectionService : BaseService, IInspectionService
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;

    public InspectionService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    public DataSourceResult GetInspectionRawMaterialSearchList(ModelStateDictionary modelState,
      DataSourceRequest request)
    {
      DataSourceResult result = null;
      request.RenameRequestSortDescriptor(nameof(VM_QualityInspection.HasDefects), nameof(VM_QualityInspection.DefectsNumber));
      request.FilterShortByBooleanValue(nameof(VM_QualityInspection.HasDefects), nameof(VM_QualityInspection.DefectsNumber), 1);

      var coilInspection = _hmiContext.V_QualityInspectionSearchGrids
        .Where(x => x.EnumRawMaterialStatus >= RawMaterialStatus.Discharged && x.EnumRawMaterialStatus <= RawMaterialStatus.Finished)
        .Select(x => new VM_QualityInspection(x))
        .ToList();
      var bundleInspection = _hmiContext.V_QualityBundlesInspectionSearchGrids.Select(x => new VM_QualityInspection(x)).ToList();

      var inspections = coilInspection.Concat(bundleInspection).ToList();

      result = inspections
        .ToDataSourceLocalResult<VM_QualityInspection, VM_QualityInspection>(request, modelState);

      return result;
    }

    public DataSourceResult GetCoilInspectionStationRawMaterialSearchList(ModelStateDictionary modelState,
      DataSourceRequest request)
    {
      DataSourceResult result = null;
      request.RenameRequestSortDescriptor(nameof(VM_QualityInspection.HasDefects), nameof(VM_QualityInspection.DefectsNumber));
      request.FilterShortByBooleanValue(nameof(VM_QualityInspection.HasDefects), nameof(VM_QualityInspection.DefectsNumber), 1);
      result = _hmiContext.V_QualityInspectionSearchGrids
        .ToDataSourceLocalResult(request, modelState, data => new VM_QualityInspection(data));

      return result;
    }

    public DataSourceResult GetBundleInspectionStationRawMaterialSearchList(ModelStateDictionary modelState,
      DataSourceRequest request)
    {
      DataSourceResult result = null;
      request.RenameRequestSortDescriptor(nameof(VM_QualityInspection.HasDefects), nameof(VM_QualityInspection.DefectsNumber));
      request.FilterShortByBooleanValue(nameof(VM_QualityInspection.HasDefects), nameof(VM_QualityInspection.DefectsNumber), 1);
      result = _hmiContext.V_QualityBundlesInspectionSearchGrids
        .ToDataSourceLocalResult(request, modelState, data => new VM_QualityInspection(data));

      return result;
    }

    public VM_QualityInspection GetInspectionDetailsByRawMaterialId(ModelStateDictionary modelState, long rawMaterialId)
    {
      VM_QualityInspection result = null;

      if (rawMaterialId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      QTYQualityInspection quality =
        _peContext.QTYQualityInspections.SingleOrDefault(x => x.FKRawMaterialId == rawMaterialId);
      result = quality == null ? new VM_QualityInspection(rawMaterialId) : new VM_QualityInspection(quality);

      return result;
    }

    public DataSourceResult GetDefectsByRawMaterialId(ModelStateDictionary modelState, DataSourceRequest request,
      long rawMaterialId)
    {
      DataSourceResult result = null;

      result = _hmiContext.V_DefectsSummaries
        .Where(x => x.RawMaterialId == rawMaterialId)
        .ToDataSourceLocalResult(request, modelState, data => new VM_Defect(data));

      return result;
    }

    public async Task<VM_Base> DeleteDefect(ModelStateDictionary modelState, long defectId)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      DCDefect entryDataContract = new DCDefect();
      entryDataContract.Id = defectId;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.DeleteDefectAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> AssignRawMaterialQuality(ModelStateDictionary modelState,
      VM_QualityInspection rawMaterialQuality)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref rawMaterialQuality);

      DCRawMaterialQuality qualityDataContract = new DCRawMaterialQuality
      {
        RawMaterialId = rawMaterialQuality.RawMaterialId,
        DiameterMin = rawMaterialQuality.DiameterMin,
        DiameterMax = rawMaterialQuality.DiameterMax,
        VisualInspection = rawMaterialQuality.VisualInspection,
        CrashTest = CrashTest.GetValue(rawMaterialQuality.EnumCrashTest),
        InspectionResult = InspectionResult.GetValue(rawMaterialQuality.EnumInspectionResult)
      };

      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.AssignRawMaterialQualityAsync(qualityDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> AssignRawMaterialFinalQuality(ModelStateDictionary modelState,
      VM_QualityInspection rawMaterialQuality)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref rawMaterialQuality);

      DCRawMaterialQuality qualityDataContract = new DCRawMaterialQuality
      {
        RawMaterialId = rawMaterialQuality.RawMaterialId,
        InspectionResult = InspectionResult.GetValue(rawMaterialQuality.EnumInspectionResult)
      };

      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.AssignRawMaterialFinalQuality(qualityDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public VM_Defect GetDefect(long id)
    {
      VM_Defect result = null;

      V_DefectsSummary defect = _hmiContext.V_DefectsSummaries.SingleOrDefault(x => x.DefectId == id);
      result = defect == null ? null : new VM_Defect(defect);

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

    public IList<VM_DefectCatalogue> GetDefectCatalogues()
    {
      List<VM_DefectCatalogue> result = new List<VM_DefectCatalogue>();

      foreach (QTYDefectCatalogue item in _peContext.QTYDefectCatalogues.AsQueryable())
      {
        result.Add(new VM_DefectCatalogue(item));
      }

      return result;
    }

    public VM_QualityInspection GetQualityByRawMaterial(long id)
    {
      VM_QualityInspection result = null;

      QTYQualityInspection quality = _peContext.QTYQualityInspections.SingleOrDefault(x => x.FKRawMaterialId == id);
      result = quality == null ? new VM_QualityInspection(id) : new VM_QualityInspection(quality);

      return result;
    }

    public VM_Scrap GetScrapByRawMaterial(long id)
    {
      VM_Scrap result = null;

      TRKRawMaterial data = _peContext.TRKRawMaterials
        .Where(x => x.RawMaterialId == id && x.ScrapPercent != null &&
                    x.ScrapPercent > 0).Include(i => i.FKScrapAsset).SingleOrDefault();
      //TRKRawMaterialsStep data = ctx.TRKRawMaterialsSteps.Where(x => x.FKRawMaterialId == 82944 && x.ProcessingStepNo == 0 && (x.ScrapPercent != null && x.ScrapPercent > 0)).Include(i => i.MVHAsset).SingleOrDefault();
      result = data == null ? new VM_Scrap(id) : new VM_Scrap(data);

      return result;
    }


    public async Task<VM_Base> UpdateDefect(ModelStateDictionary modelState, VM_Defect defect)
    {
      VM_Base result = new VM_Base();

      if (!defect.DefectId.HasValue || defect.DefectId.Value <= 0 || !defect.DefectCatalogueId.HasValue ||
          defect.DefectCatalogueId.Value <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref defect);

      DCDefect dcDefect = new DCDefect
      {
        Id = defect.DefectId.Value,
        DefectName = defect.DefectName,
        DefectCatalogueId = defect.DefectCatalogueId,
        AssetId = defect.AssetId,
        RawMaterialId = defect.RawMaterialId,
        DefectPosition = defect.DefectPosition,
        DefectDescription = defect.DefectDescription
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.EditDefectAsync(dcDefect);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
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

    public DataSourceResult GetInspectionByWorkOrder(ModelStateDictionary modelState, DataSourceRequest request, long workOrderId)
    {
      request.RenameRequestSortDescriptor(nameof(VM_QualityInspection.HasDefects), nameof(VM_QualityInspection.DefectsNumber));
      request.FilterShortByBooleanValue(nameof(VM_QualityInspection.HasDefects), nameof(VM_QualityInspection.DefectsNumber), 1);

      IQueryable<V_RawMaterialOverview> list = _hmiContext.V_RawMaterialOverviews
        .Where(x => x.WorkOrderId == workOrderId)
        .AsQueryable();

      return list.ToDataSourceLocalResult<V_RawMaterialOverview, VM_QualityInspection>(request, modelState, x => new VM_QualityInspection(x));
    }
  }
}
