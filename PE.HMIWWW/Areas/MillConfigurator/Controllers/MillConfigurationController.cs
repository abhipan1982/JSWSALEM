using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.DbEntity.PEContext;
using PE.HMIWWW.Areas.MillConfigurator.Services;
using PE.HMIWWW.Areas.MillConfigurator.ViewModels.AssetTemplate;
using PE.HMIWWW.Areas.MillConfigurator.ViewModels.MillConfiguration;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.Helpers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.System;
using SMF.DbEntity.Models;

namespace PE.HMIWWW.Areas.MillConfigurator.Controllers
{
  [Area("MillConfigurator")]
  [Authorize(Policy = "PrimetalsOnly")]
  public class MillConfigurationController : BaseController
  {
    private readonly IViewToStringRendererService _renderService;

    public MillConfigurationController(IViewToStringRendererService renderService)
    {
      _renderService = renderService;
    }

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      ViewBag.UnitOfMeasureList = GetUnitOfMeasureList();
      ViewBag.DataTypeList = GetDataTypeList();
      ViewBag.AssetTemplateList = GetAssetTemplateList();
      ViewBag.AreaTemplateList = GetAreaTemplateList();
      ViewBag.AssetTypeList = GetAssetTypeList();
      ViewBag.AssetList = GetAssetList();
      ViewBag.FeatureList = GetFeatureList();
      ViewBag.PDFTitle = GetPDTitle();
      ViewBag.FeatureTypeList = ListValuesHelper.GetFeatureTypeList();
      ViewBag.CommChannelTypeList = ListValuesHelper.GetCommChannelTypeList();
      ViewBag.AggregationStrategyList = ListValuesHelper.GetAggregationStrategyList();
      ViewBag.YardTypeList = ListValuesHelper.GetYardTypeList();
      ViewBag.TrackingAreaTypeList = ListValuesHelper.GetTrackingAreaTypeList();
      ViewBag.FeatureProviderList = ListValuesHelper.GetFeatureProviderList();
      ViewBag.TagValidationResultList = ListValuesHelper.GetTagValidationResultList();
      base.OnActionExecuting(ctx);
    }

    public async Task<IActionResult> Index()
    {
      await using var ctx = new PEContext();

      await new MillConfigurationService().ValidateStandardAssets(ctx);
      await new MillConfigurationService().VerifyFeaturesAsync(ctx);

      return View();
    }

    public async Task<JsonResult> GetAssignedAssetsTreeList([DataSourceRequest] DataSourceRequest request)
    {
      await using var ctx = new HmiContext();

      return Json(new MillConfigurationService().GetAssignedAssetsTreeList(ctx, request));
    }

    public async Task<JsonResult> GetAssignedFeaturesList([DataSourceRequest] DataSourceRequest request)
    {
      await using var ctx = new HmiContext();

      return await PrepareJsonResultFromDataSourceResult(() =>
        new MillConfigurationService().GetAssignedFeaturesListAsync(ctx, request));
    }

    public async Task<JsonResult> GetUnassignedAssetsTreeList([DataSourceRequest] DataSourceRequest request)
    {
      await using var ctx = new PEContext();

      return Json(new MillConfigurationService().GetUnassignedAssetsTreeList(ctx, request));
    }

    public async Task<JsonResult> GetAssignedFeatureSearchList([DataSourceRequest] DataSourceRequest request, long assetId)
    {
      await using var ctx = new PEContext();

      return await PrepareJsonResultFromDataSourceResult(() =>
        new MillConfigurationService().GetAssignedFeatureSearchList(ctx, request, assetId));
    }

    public async Task<IActionResult> AssetDetails(long assetId)
    {
      await using var ctx = new PEContext();

      return PartialView("~/Areas/MillConfigurator/Views/MillConfiguration/_AssetBody.cshtml",
        await new MillConfigurationService().GetAssetByIdAsync(ctx, assetId));
    }

    public async Task<IActionResult> FeatureDetails(long featureId)
    {
      await using var ctx = new PEContext();

      return PartialView("~/Areas/MillConfigurator/Views/MillConfiguration/_FeatureBody.cshtml",
        await new MillConfigurationService().GetFeatureByIdAsync(ctx, featureId));
    }

    public async Task<IActionResult> AssetTypeDetails(long assetTypeId)
    {
      await using var ctx = new PEContext();

      return PartialView("~/Areas/MillConfigurator/Views/MillConfiguration/_AssetTypeDetails.cshtml",
        await new MillConfigurationService().GetAssetTypeByIdAsync(ctx, assetTypeId));
    }

    public IActionResult AssetCreatePopup()
    {
      return PartialView("~/Areas/MillConfigurator/Views/MillConfiguration/_AssetCreatePopup.cshtml");
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsset(VM_AssetInstance data)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new MillConfigurationService().CreateAssetAsync(ModelState, ctx, data));
    }

    public IActionResult AreaCreatePopup()
    {
      return PartialView("~/Areas/MillConfigurator/Views/MillConfiguration/_AreaCreatePopup.cshtml", new VM_AssetInstance());
    }

    [HttpPost]
    public async Task<IActionResult> CreateArea(VM_AssetInstance data)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new MillConfigurationService().CreateAreaAsync(ModelState, ctx, data));
    }

    public IActionResult AssetTypeCreatePopup()
    {
      return PartialView("~/Areas/MillConfigurator/Views/MillConfiguration/_AssetTypeCreatePopup.cshtml");
    }

    [HttpPost]
    public async Task<IActionResult> CreateAssetType(VM_AssetType data)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new MillConfigurationService().CreateAssetTypeAsync(ModelState, ctx, data));
    }

    public async Task<IActionResult> AssetEditPopup(long assetId)
    {
      await using var ctx = new PEContext();

      var model = await new MillConfigurationService().GetAssetByIdAsync(ctx, assetId);

      return (!model.IsZone && !model.IsArea) ?
        PartialView("~/Areas/MillConfigurator/Views/MillConfiguration/_AssetEditPopup.cshtml", model) :
        PartialView("~/Areas/MillConfigurator/Views/MillConfiguration/_AreaEditPopup.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> EditAsset(VM_AssetInstance data)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new MillConfigurationService().EditAssetAsync(ModelState, ctx, data));
    }

    [HttpPost]
    public async Task<IActionResult> EditArea(VM_AssetInstance data)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new MillConfigurationService().EditAreaAsync(ModelState, ctx, data));
    }

    public async Task<IActionResult> AssetTypeEditPopup(long assetTypeId)
    {
      await using var ctx = new PEContext();

      return PartialView("~/Areas/MillConfigurator/Views/MillConfiguration/_AssetTypeEditPopup.cshtml",
       await new MillConfigurationService().GetAssetTypeByIdAsync(ctx, assetTypeId));
    }

    [HttpPost]
    public async Task<IActionResult> EditAssetType(VM_AssetType data)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new MillConfigurationService().EditAssetTypeAsync(ModelState, ctx, data));
    }

    public async Task<IActionResult> AssetClonePopup(long assetId)
    {
      await using var ctx = new PEContext();

      var model = await new MillConfigurationService().GetAssetByIdAsync(ctx, assetId);

      return !model.IsZone && !model.IsArea ?
        PartialView("~/Areas/MillConfigurator/Views/MillConfiguration/_AssetClonePopup.cshtml", model) :
        PartialView("~/Areas/MillConfigurator/Views/MillConfiguration/_AreaClonePopup.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> CloneAsset(VM_AssetInstance data)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new MillConfigurationService().CloneAssetAsync(ModelState, ctx, data));
    }

    [HttpPost]
    public async Task<IActionResult> CloneArea(VM_AssetInstance data)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new MillConfigurationService().CloneAreaAsync(ModelState, ctx, data));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAsset(long assetId)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new MillConfigurationService().DeleteAssetAsync(ctx, assetId));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAssetType(long assetTypeId)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new MillConfigurationService().DeleteAssetTypeAsync(ctx, assetTypeId));
    }

    public IActionResult FeatureCreatePopup(long assetId)
    {
      return PartialView("~/Areas/MillConfigurator/Views/MillConfiguration/_FeatureCreatePopup.cshtml", new VM_FeatureInstance
      {
        FKAssetId = assetId
      });
    }

    [HttpPost]
    public async Task<IActionResult> CreateFeature(VM_FeatureInstance data)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new MillConfigurationService().CreateFeatureAsync(ModelState, ctx, data));
    }

    public async Task<IActionResult> FeatureEditPopup(long featureId)
    {
      await using var ctx = new PEContext();

      return PartialView("~/Areas/MillConfigurator/Views/MillConfiguration/_FeatureEditPopup.cshtml",
        await new MillConfigurationService().GetFeatureByIdAsync(ctx, featureId));
    }

    [HttpPost]
    public async Task<IActionResult> EditFeature(VM_FeatureInstance data)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new MillConfigurationService().EditFeatureAsync(ModelState, ctx, data));
    }

    public async Task<IActionResult> FeatureClonePopup(long featureId)
    {
      await using var ctx = new PEContext();

      var model = await new MillConfigurationService().GetFeatureByIdAsync(ctx, featureId);

      return PartialView("~/Areas/MillConfigurator/Views/MillConfiguration/_FeatureClonePopup.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> CloneFeature(VM_FeatureInstance data)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new MillConfigurationService().CreateFeatureAsync(ModelState, ctx, data));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteFeature(long featureId)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new MillConfigurationService().DeleteFeatureAsync(ctx, featureId));
    }

    [HttpPost]
    public async Task<IActionResult> ReorderAssignedAssets(long dragAssetId, long dropAssetId, long? parentAssetId, short dropMode)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new MillConfigurationService().ReorderAssignedAssetsAsync(ctx, dragAssetId, dropAssetId, parentAssetId, dropMode));
    }

    [HttpPost]
    public async Task<IActionResult> ReorderUnassignedAssets(long dragAssetId, long? parentAssetId)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new MillConfigurationService().ReorderUnassignedAssetsAsync(ctx, dragAssetId, parentAssetId));
    }

    [HttpPost]
    public async Task<IActionResult> AssignAsset(long dragAssetId, long dropAssetId)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new MillConfigurationService().AssignAssetAsync(ModelState, ctx, dragAssetId, dropAssetId));
    }

    [HttpPost]
    public async Task<IActionResult> UnassignAsset(long dragAssetId)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new MillConfigurationService().UnassignAssetAsync(ModelState, ctx, dragAssetId));
    }

    [HttpPost]
    public async Task<IActionResult> VerifyFeatures()
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new MillConfigurationService().VerifyFeaturesAsync(ctx));
    }

    public IActionResult ExportFeaturesPopup()
    {
      return PartialView("~/Areas/MillConfigurator/Views/MillConfiguration/_FeaturesExportPopup.cshtml");
    }

    public async Task<IActionResult> InterfaceDetailsPDF()
    {
      await using var ctx = new HmiContext();
      return PartialView("~/Areas/MillConfigurator/Views/MillConfiguration/InterfaceDetailsPDFTemplate.cshtml",
        await new MillConfigurationService().GetAssignedFeaturesListForPDFAsync(ctx));
    }

    public async Task<IActionResult> ExportFeaturesToPDF()
    {
      await using var ctx = new HmiContext();

      var data = await new MillConfigurationService().GetAssignedFeaturesListForPDFAsync(ctx);
      var html = await _renderService.RenderViewToStringAsync("~/Areas/MillConfigurator/Views/MillConfiguration/InterfaceDetailsPDFTemplate.cshtml", data);
      var fileBytes = PdfHelper.GetPdfA4ByteArray(html);

      return File(fileBytes, "application/pdf", data.FileName);
    }

    private IEnumerable<UnitOfMeasure> GetUnitOfMeasureList()
    {
      using var smfCtx = new SMFContext();
      return new FeatureTemplateService().GetUnitOfMeasureList(smfCtx);
    }

    public IEnumerable<DBDataType> GetDataTypeList()
    {
      using var peCtx = new PEContext();
      return new FeatureTemplateService().GetDataTypeList(peCtx);
    }

    private IEnumerable<VM_AssetTemplate> GetAssetTemplateList()
    {
      using var peCtx = new PEContext();
      return new MillConfigurationService().GetAssetTemplateList(peCtx);
    }

    private IEnumerable<VM_AssetTemplate> GetAreaTemplateList()
    {
      using var peCtx = new PEContext();
      return new MillConfigurationService().GetAreaTemplateList(peCtx);
    }

    private IEnumerable<MVHAssetType> GetAssetTypeList(bool autocomplete = false, string text = "")
    {
      using var peCtx = new PEContext();
      var data = new MillConfigurationService().GetAssetTypeList(peCtx);

      if (!autocomplete)
        return data;

      if (!string.IsNullOrEmpty(text))
      {
        data = data
          .Where(x => x.AssetTypeName
          .Contains(text))
          .ToList();
      }

      return data
        .ToList()
        .Select(x => new MVHAssetType
        {
          AssetTypeId = x.AssetTypeId,
          AssetTypeName = x.AssetTypeName
        })
        .ToList();
    }

    private IEnumerable<MVHAsset> GetAssetList()
    {
      using var peCtx = new PEContext();
      return new MillConfigurationService().GetAssetList(peCtx);
    }

    private IEnumerable<MVHFeature> GetFeatureList()
    {
      using var peCtx = new PEContext();
      return new MillConfigurationService().GetFeatureList(peCtx);
    }

    private string GetPDTitle()
    {
      return new MillConfigurationService().GetPDTitle();
    }
  }
}
