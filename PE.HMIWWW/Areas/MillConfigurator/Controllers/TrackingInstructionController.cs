using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.DbEntity.HmiModels;
using PE.DbEntity.PEContext;
using PE.HMIWWW.Areas.MillConfigurator.Services;
using PE.HMIWWW.Areas.MillConfigurator.ViewModels.TrackingInstruction;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.Areas.MillConfigurator.Controllers
{
  [Area("MillConfigurator")]
  [Authorize(Policy = "PrimetalsOnly")]
  public class TrackingInstructionController : BaseController
  {
    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      ViewBag.TrackingInstructionTypeList = GetTrackingInstructionTypeList();
      ViewBag.FeatureList = GetFeatureList();
      ViewBag.AssetList = GetAssetList();
      ViewBag.AreaList = GetAreaList();
      ViewBag.TrackingInstructionList = GetTrackingInstructionList();

      base.OnActionExecuting(ctx);
    }

    public IActionResult Index()
    {
      return View();
    }

    public async Task<IActionResult> ElementDetails(long trackingInstructionId)
    {
      await using var ctx = new PEContext();

      return PartialView("~/Areas/MillConfigurator/Views/TrackingInstruction/_TrackingInstructionBody.cshtml",
        await new TrackingInstructionService().GetTrackingInstructionByIdAsync(ctx, trackingInstructionId));
    }

    public async Task<JsonResult> GetTrackingInstructionSearchList([DataSourceRequest] DataSourceRequest request)
    {
      await using var ctx = new HmiContext();

      return await PrepareJsonResultFromDataSourceResult(() =>
        new TrackingInstructionService().GetTrackingInstructionSearchList(ctx, request));
    }

    public async Task<JsonResult> GetRelatedTrackingInstructionsSearchList([DataSourceRequest] DataSourceRequest request, long featureId)
    {
      await using var ctx = new PEContext();

      return await PrepareJsonResultFromDataSourceResult(() =>
        new TrackingInstructionService().GetRelatedTrackingInstructionsSearchList(ctx, request, featureId));
    }

    public IActionResult TrackingInstructionCreatePopup()
    {
      return PartialView("~/Areas/MillConfigurator/Views/TrackingInstruction/_TrackingInstructionCreatePopup.cshtml");
    }

    [HttpPost]
    public async Task<IActionResult> CreateTrackingInstruction(VM_TrackingInstruction data)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new TrackingInstructionService().CreateTrackingInstructionAsync(ModelState, ctx, data));
    }

    public async Task<IActionResult> TrackingInstructionEditPopup(long trackingInstructionId)
    {
      await using var ctx = new PEContext();

      return PartialView("~/Areas/MillConfigurator/Views/TrackingInstruction/_TrackingInstructionEditPopup.cshtml",
        await new TrackingInstructionService().GetTrackingInstructionByIdAsync(ctx, trackingInstructionId));
    }

    [HttpPost]
    public async Task<IActionResult> EditTrackingInstruction(VM_TrackingInstruction data)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new TrackingInstructionService().EditTrackingInstructionAsync(ModelState, ctx, data));
    }

    public async Task<IActionResult> TrackingInstructionClonePopup(long trackingInstructionId)
    {
      await using var ctx = new PEContext();

      return PartialView("~/Areas/MillConfigurator/Views/TrackingInstruction/_TrackingInstructionClonePopup.cshtml",
        await new TrackingInstructionService().GetTrackingInstructionByIdAsync(ctx, trackingInstructionId));
    }

    [HttpPost]
    public async Task<IActionResult> CloneTrackingInstruction(VM_TrackingInstruction data)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new TrackingInstructionService().CreateTrackingInstructionAsync(ModelState, ctx, data));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteTrackingInstruction(long trackingInstructionId)
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new TrackingInstructionService().DeleteTrackingInstructionAsync(ModelState, ctx, trackingInstructionId));
    }

    [HttpPost]
    public async Task<IActionResult> CreateBasicTrackingInstructions()
    {
      await using var ctx = new PEContext();

      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        new TrackingInstructionService().CreateBasicTrackingInstructionsAsync(ModelState, ctx));
    }

    private IEnumerable<MVHFeature> GetFeatureList()
    {
      using var ctx = new PEContext();
      return new TrackingInstructionService().GetFeatureList(ctx);
    }

    private IEnumerable<MVHAsset> GetAssetList()
    {
      using var ctx = new PEContext();
      return new TrackingInstructionService().GetAssetList(ctx);
    }

    private IEnumerable<MVHAsset> GetAreaList()
    {
      using var ctx = new PEContext();
      return new TrackingInstructionService().GetAreaList(ctx);
    }

    private IEnumerable<TRKTrackingInstruction> GetTrackingInstructionList()
    {
      using var ctx = new PEContext();
      return new TrackingInstructionService().GetTrackingInstructionList(ctx);
    }

    private SelectList GetTrackingInstructionTypeList()
    {
      using var ctx = new PEContext();
      return new TrackingInstructionService().GetTrackingInstructionTypeList<TrackingInstructionType, int>();
    }

    public IEnumerable<V_TrackingInstruction> GetTrackingInstructionListForInput()
    {
      using var ctx = new HmiContext();
      return new TrackingInstructionService().GetTrackingInstructionListForInput(ctx);
    }
  }
}
