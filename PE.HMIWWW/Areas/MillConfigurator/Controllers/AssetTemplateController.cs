using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.HMIWWW.Areas.MillConfigurator.Services;
using PE.HMIWWW.Areas.MillConfigurator.ViewModels.AssetTemplate;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.Models;

namespace PE.HMIWWW.Areas.MillConfigurator.Controllers
{
  [Area("MillConfigurator")]
  [Authorize(Policy = "PrimetalsOnly")]
  public class AssetTemplateController : BaseController
  {
    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      ViewBag.TrackingAreaTypeList = ListValuesHelper.GetTrackingAreaTypeList();
      ViewBag.UnitOfMeasureList = GetUnitOfMeasureList();
      ViewBag.DataTypeList = GetDataTypeList();
      ViewBag.FeatureTypeList = ListValuesHelper.GetFeatureTypeList();
      ViewBag.CommChannelTypeList = ListValuesHelper.GetCommChannelTypeList();
      ViewBag.AggregationStrategyList = ListValuesHelper.GetAggregationStrategyList();

      base.OnActionExecuting(ctx);
    }

    public IActionResult Index()
    {
      return View();
    }

    public async Task<IActionResult> ElementDetails(long assetTemplateId)
    {
      await using var ctx = new PEContext();

      return PartialView("~/Areas/MillConfigurator/Views/AssetTemplate/_AssetTemplateBody.cshtml",
        await new AssetTemplateService().GetAssetTemplateByIdAsync(ctx, assetTemplateId));
    }

    public async Task<IActionResult> ElementDetailsView(long assetTemplateId)
    {
      await using var ctx = new PEContext();

      return PartialView("~/Areas/MillConfigurator/Views/AssetTemplate/_AssetTemplateDetailsView.cshtml",
        await new AssetTemplateService().GetAssetTemplateByIdAsync(ctx, assetTemplateId));
    }

    public async Task<JsonResult> GetAssetTemplateSearchList([DataSourceRequest] DataSourceRequest request)
    {
      await using var ctx = new PEContext();

      return await PrepareJsonResultFromDataSourceResult(() =>
        new AssetTemplateService().GetAssetTemplateSearchList(ctx, request));
    }

    public IActionResult AssetTemplateCreatePopup()
    {
      return PartialView("~/Areas/MillConfigurator/Views/AssetTemplate/_AssetTemplateCreatePopup.cshtml");
    }

    [HttpPost]
    public async Task<IActionResult> CreateAssetTemplate(VM_AssetTemplate data)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new AssetTemplateService().CreateAssetTemplateAsync(ModelState, ctx, data));
    }

    public IActionResult AreaTemplateCreatePopup()
    {
      return PartialView("~/Areas/MillConfigurator/Views/AssetTemplate/_AreaTemplateCreatePopup.cshtml");
    }

    [HttpPost]
    public async Task<IActionResult> CreateAreaTemplate(VM_AreaTemplate data)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new AssetTemplateService().CreateAreaTemplateAsync(ModelState, ctx, data));
    }

    public async Task<IActionResult> AssetTemplateEditPopup(long assetTemplateId)
    {
      await using var ctx = new PEContext();

      var model = await new AssetTemplateService().GetAssetTemplateByIdAsync(ctx, assetTemplateId);

      if (!model.IsZone && !model.IsArea)
        return PartialView("~/Areas/MillConfigurator/Views/AssetTemplate/_AssetTemplateEditPopup.cshtml", model);

      return PartialView("~/Areas/MillConfigurator/Views/AssetTemplate/_AreaTemplateEditPopup.cshtml", new VM_AreaTemplate
      {
        AssetTemplateId = model.AssetTemplateId,
        AssetTemplateName = model.AssetTemplateName,
        AssetTemplateDescription = model.AssetTemplateDescription,
        EnumTrackingAreaType = model.EnumTrackingAreaType,
        IsZone = model.IsZone
      });
    }

    [HttpPost]
    public async Task<IActionResult> EditAssetTemplate(VM_AssetTemplate data)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new AssetTemplateService().EditAssetTemplateAsync(ModelState, ctx, data));
    }

    [HttpPost]
    public async Task<IActionResult> EditAreaTemplate(VM_AreaTemplate data)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new AssetTemplateService().EditAreaTemplateAsync(ModelState, ctx, data));
    }
    public async Task<IActionResult> AssetTemplateClonePopup(long assetTemplateId)
    {
      await using var ctx = new PEContext();

      var model = await new AssetTemplateService().GetAssetTemplateByIdAsync(ctx, assetTemplateId);

      if (!model.IsZone && !model.IsArea)
        return PartialView("~/Areas/MillConfigurator/Views/AssetTemplate/_AssetTemplateClonePopup.cshtml", model);

      return PartialView("~/Areas/MillConfigurator/Views/AssetTemplate/_AreaTemplateClonePopup.cshtml", new VM_AreaTemplate
      {
        AssetTemplateId = model.AssetTemplateId,
        AssetTemplateName = model.AssetTemplateName,
        AssetTemplateDescription = model.AssetTemplateDescription,
        EnumTrackingAreaType = model.EnumTrackingAreaType,
        IsZone = model.IsZone
      });
    }

    [HttpPost]
    public async Task<IActionResult> CloneAssetTemplate(VM_AssetTemplate data)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new AssetTemplateService().CreateAssetTemplateAsync(ModelState, ctx, data));
    }

    [HttpPost]
    public async Task<IActionResult> CloneAreaTemplate(VM_AreaTemplate data)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new AssetTemplateService().CreateAreaTemplateAsync(ModelState, ctx, data));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAssetTemplate(long assetTemplateId)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new AssetTemplateService().DeleteAssetTemplateAsync(ModelState, ctx, assetTemplateId));
    }

    private IEnumerable<UnitOfMeasure> GetUnitOfMeasureList()
    {
      using var smfCtx = new SMFContext();
      return new FeatureTemplateService().GetUnitOfMeasureList(smfCtx);
    }

    private IEnumerable<DBDataType> GetDataTypeList()
    {
      using var peCtx = new PEContext();
      return new FeatureTemplateService().GetDataTypeList(peCtx);
    }
  }
}
