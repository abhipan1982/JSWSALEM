using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.HMIWWW.Areas.MillConfigurator.Services;
using PE.HMIWWW.Areas.MillConfigurator.ViewModels.FeatureTemplate;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.Models;

namespace PE.HMIWWW.Areas.MillConfigurator.Controllers
{
  [Area("MillConfigurator")]
  [Authorize(Policy = "PrimetalsOnly")]
  public class FeatureTemplateController : BaseController
  {
    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
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

    public async Task<IActionResult> ElementDetails(long featureTemplateId)
    {
      await using var ctx = new PEContext();

      return PartialView("~/Areas/MillConfigurator/Views/FeatureTemplate/_FeatureTemplateBody.cshtml",
        await new FeatureTemplateService().GetFeatureTemplateByIdAsync(ctx, featureTemplateId));
    }

    public async Task<JsonResult> GetFeatureTemplateSearchList([DataSourceRequest] DataSourceRequest request)
    {
      await using var ctx = new PEContext();

      return await PrepareJsonResultFromDataSourceResult(() =>
        new FeatureTemplateService().GetFeatureTemplateSearchList(ctx, request));
    }

    public async Task<JsonResult> GetAssignedFeatureTemplateSearchList([DataSourceRequest] DataSourceRequest request, long assetTemplateId)
    {
      await using var ctx = new PEContext();

      return await PrepareJsonResultFromDataSourceResult(() =>
        new FeatureTemplateService().GetAssignedFeatureTemplateSearchList(ctx, request, assetTemplateId));
    }

    public async Task<JsonResult> GetUnassignedFeatureTemplateSearchList([DataSourceRequest] DataSourceRequest request, long assetTemplateId)
    {
      await using var ctx = new PEContext();

      return await PrepareJsonResultFromDataSourceResult(() =>
        new FeatureTemplateService().GetUnassignedFeatureTemplateSearchList(ctx, request, assetTemplateId));
    }

    public IActionResult FeatureTemplateCreatePopup()
    {
      return PartialView("~/Areas/MillConfigurator/Views/FeatureTemplate/_FeatureTemplateCreatePopup.cshtml");
    }

    [HttpPost]
    public async Task<IActionResult> CreateFeatureTemplate(VM_FeatureTemplate data)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new FeatureTemplateService().CreateFeatureTemplateAsync(ModelState, ctx, data));
    }

    public async Task<IActionResult> FeatureTemplateEditPopup(long featureTemplateId)
    {
      await using var ctx = new PEContext();

      return PartialView("~/Areas/MillConfigurator/Views/FeatureTemplate/_FeatureTemplateEditPopup.cshtml",
        await new FeatureTemplateService().GetFeatureTemplateByIdAsync(ctx, featureTemplateId));
    }

    [HttpPost]
    public async Task<IActionResult> EditFeatureTemplate(VM_FeatureTemplate data)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new FeatureTemplateService().EditFeatureTemplateAsync(ModelState, ctx, data));
    }

    public async Task<IActionResult> FeatureTemplateClonePopup(long featureTemplateId)
    {
      await using var ctx = new PEContext();

      return PartialView("~/Areas/MillConfigurator/Views/FeatureTemplate/_FeatureTemplateClonePopup.cshtml",
        await new FeatureTemplateService().GetFeatureTemplateByIdAsync(ctx, featureTemplateId));
    }

    [HttpPost]
    public async Task<IActionResult> CloneFeatureTemplate(VM_FeatureTemplate data)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new FeatureTemplateService().CreateFeatureTemplateAsync(ModelState, ctx, data));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteFeatureTemplate(long featureTemplateId)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new FeatureTemplateService().DeleteFeatureTemplateAsync(ModelState, ctx, featureTemplateId));
    }

    [HttpPost]
    public async Task<IActionResult> AssignTemplates(long featureTemplateId, long assetTemplateId)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new FeatureTemplateService().AssignTemplatesAsync(ModelState, ctx, featureTemplateId, assetTemplateId));
    }

    public async Task<IActionResult> TemplatesAssignPopup(long assetTemplateId)
    {
      await using var ctx = new PEContext();

      return PartialView("~/Areas/MillConfigurator/Views/FeatureTemplate/_TemplatesAssignPopup.cshtml", assetTemplateId);
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
