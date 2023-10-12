using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.HMIWWW.Areas.MillConfigurator.ViewModels.AssetTemplate;
using PE.HMIWWW.Core.Extensions;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.Areas.MillConfigurator.Services
{
  public class AssetTemplateService
  {
    public AssetTemplateService() { }

    public async Task<VM_AssetTemplate> GetAssetTemplateByIdAsync(PEContext peCtx, long assetTemplateId)
    {
      return new VM_AssetTemplate(await peCtx.MVHAssetTemplates
        .FirstAsync(x => x.AssetTemplateId == assetTemplateId));
    }

    public DataSourceResult GetAssetTemplateSearchList(PEContext peCtx, DataSourceRequest request)
    {
      return peCtx.MVHAssetTemplates
        .ToListAsync().GetAwaiter().GetResult()
        .ToDataSourceLocalResult(request, data => new VM_AssetTemplate(data));
    }

    public async Task<VM_Base> CreateAssetTemplateAsync(ModelStateDictionary modelState, PEContext ctx, VM_AssetTemplate data)
    {
      VM_Base result = new VM_Base();

      await CheckAssetTemplateUniqueNameAsync(ctx, modelState, data.AssetTemplateName);

      if (!modelState.IsValid)
      {
        return result;
      }

      var assetTemplate = new MVHAssetTemplate
      {
        AssetTemplateName = data.AssetTemplateName,
        AssetTemplateDescription = data.AssetTemplateDescription,
        IsArea = false,
        IsZone = false,
        EnumTrackingAreaType = TrackingAreaType.None,
      };

      await ctx.MVHAssetTemplates.AddAsync(assetTemplate);

      if (data.CloneFeatureTemplates && data.AssetTemplateId != 0)
      {
        var assetFeatureTemplates = await ctx.MVHAssetFeatureTemplates
          .Where(x => x.FKAssetTemplateId == data.AssetTemplateId)
          .ToListAsync();

        assetFeatureTemplates.ForEach(x =>
        {
          ctx.MVHAssetFeatureTemplates.Add(new MVHAssetFeatureTemplate
          {
            FKAssetTemplate = assetTemplate,
            FKFeatureTemplateId = x.FKFeatureTemplateId
          });
        });
      }

      await ctx.SaveChangesAsync();

      //return view model
      return result;
    }

    public async Task<VM_Base> CreateAreaTemplateAsync(ModelStateDictionary modelState, PEContext ctx, VM_AreaTemplate data)
    {
      VM_Base result = new VM_Base();

      await CheckAssetTemplateUniqueNameAsync(ctx, modelState, data.AssetTemplateName);

      if (data.CloneFeatureTemplates && data.AssetTemplateId != 0)
      {
        await CheckAssetTemplateAssignmentPossibilityAsync(ctx, modelState, data.AssetTemplateId);
      }

      if (!modelState.IsValid)
      {
        return result;
      }

      var assetTemplate = new MVHAssetTemplate
      {
        AssetTemplateName = data.AssetTemplateName,
        AssetTemplateDescription = data.AssetTemplateDescription,
        IsArea = !data.IsZone,
        IsZone = data.IsZone,
        EnumTrackingAreaType = TrackingAreaType.GetValue(data.EnumTrackingAreaType),
      };

      await ctx.MVHAssetTemplates.AddAsync(assetTemplate);

      if (data.CloneFeatureTemplates && data.AssetTemplateId != 0)
      {
        var assetFeatureTemplates = await ctx.MVHAssetFeatureTemplates
          .Where(x => x.FKAssetTemplateId == data.AssetTemplateId)
          .ToListAsync();

        assetFeatureTemplates.ForEach(x =>
        {
          ctx.MVHAssetFeatureTemplates.Add(new MVHAssetFeatureTemplate
          {
            FKAssetTemplate = assetTemplate,
            FKFeatureTemplateId = x.FKFeatureTemplateId
          });
        });
      }

      await ctx.SaveChangesAsync();

      //return view model
      return result;
    }

    public async Task<VM_Base> EditAssetTemplateAsync(ModelStateDictionary modelState, PEContext ctx, VM_AssetTemplate data)
    {
      VM_Base result = new VM_Base();

      await CheckAssetTemplateUniqueNameAsync(ctx, modelState, data.AssetTemplateName, data.AssetTemplateId);

      if (!modelState.IsValid)
      {
        return result;
      }

      var assetTemplate = await ctx.MVHAssetTemplates.FirstAsync(x => x.AssetTemplateId == data.AssetTemplateId);

      assetTemplate.AssetTemplateName = data.AssetTemplateName;
      assetTemplate.AssetTemplateDescription = data.AssetTemplateDescription;

      await ctx.SaveChangesAsync();

      //return view model
      return result;
    }

    public async Task<VM_Base> EditAreaTemplateAsync(ModelStateDictionary modelState, PEContext ctx, VM_AreaTemplate data)
    {
      VM_Base result = new VM_Base();

      await CheckAssetTemplateUniqueNameAsync(ctx, modelState, data.AssetTemplateName, data.AssetTemplateId);

      if (!modelState.IsValid)
      {
        return result;
      }

      var assetTemplate = await ctx.MVHAssetTemplates.FirstAsync(x => x.AssetTemplateId == data.AssetTemplateId);

      assetTemplate.AssetTemplateName = data.AssetTemplateName;
      assetTemplate.AssetTemplateDescription = data.AssetTemplateDescription;
      assetTemplate.IsArea = !data.IsZone;
      assetTemplate.IsZone = data.IsZone;
      assetTemplate.EnumTrackingAreaType = TrackingAreaType.GetValue(data.EnumTrackingAreaType);

      await ctx.SaveChangesAsync();

      //return view model
      return result;
    }

    public async Task<VM_Base> DeleteAssetTemplateAsync(ModelStateDictionary modelState, PEContext ctx, long assetTemplateId)
    {
      VM_Base result = new VM_Base();

      await CheckAssetTemplateAssignmentAsync(ctx, modelState, assetTemplateId);

      if (!modelState.IsValid)
      {
        return result;
      }

      ctx.MVHAssetTemplates.Remove(await ctx.MVHAssetTemplates.FirstAsync(x => x.AssetTemplateId == assetTemplateId));

      await ctx.SaveChangesAsync();

      //return view model
      return result;
    }

    private async Task CheckAssetTemplateUniqueNameAsync(PEContext ctx, ModelStateDictionary modelState, string assetTemplateName, long? assetTemplateId = null)
    {
      MVHAssetTemplate assetTemplate = null;
      if (assetTemplateId is null)
        assetTemplate = await ctx.MVHAssetTemplates
          .FirstOrDefaultAsync(x => x.AssetTemplateName.ToUpper().Equals(assetTemplateName.ToUpper()));
      else
        assetTemplate = await ctx.MVHAssetTemplates
          .FirstOrDefaultAsync(x => x.AssetTemplateId != assetTemplateId && x.AssetTemplateName.ToUpper().Equals(assetTemplateName.ToUpper()));

      if (assetTemplate is not null)
        modelState.AddModelError("error", ResourceController.GetErrorText("AssetTemplateNameNotUnique"));
    }

    private async Task CheckAssetTemplateAssignmentAsync(PEContext ctx, ModelStateDictionary modelState, long assetTemplateId)
    {
      MVHAssetFeatureTemplate assetFeatureTemplate = await ctx.MVHAssetFeatureTemplates
        .FirstOrDefaultAsync(x => x.FKAssetTemplateId == assetTemplateId);

      if (assetFeatureTemplate is not null)
        modelState.AddModelError("error", ResourceController.GetErrorText("AssetTemplateAssignedToFeatureTemplate"));
    }

    private async Task CheckAssetTemplateAssignmentPossibilityAsync(PEContext ctx, ModelStateDictionary modelState, long assetTemplateId)
    {
      var assetTemplate = await ctx.MVHAssetTemplates
        .FirstAsync(x => x.AssetTemplateId == assetTemplateId);

      if (assetTemplate.IsZone)
        modelState.AddModelError("error", ResourceController.GetErrorText("CannotAssignFeatureTemplatesToZoneTemplate"));
    }
  }
}
