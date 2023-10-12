using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.HMIWWW.Areas.MillConfigurator.ViewModels.FeatureTemplate;
using PE.HMIWWW.Core.Extensions;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.Models;

namespace PE.HMIWWW.Areas.MillConfigurator.Services
{
  public class FeatureTemplateService
  {
    public FeatureTemplateService() { }

    public async Task<VM_FeatureTemplate> GetFeatureTemplateByIdAsync(PEContext peCtx, long featureTemplateId)
    {
      var data = new VM_FeatureTemplate(await peCtx.MVHFeatureTemplates
        .Include(x => x.FKDataType)
        .FirstAsync(x => x.FeatureTemplateId == featureTemplateId));

      await using var smfCtx = new SMFContext();

      var uom = GetUnitOfMeasureList(smfCtx);
      data.UnitOfMeasureSymbol = uom.First(x => x.UnitId == data.FKUnitOfMeasureId).UnitSymbol;
      data.ExtUnitOfMeasureSymbol = uom.First(x => x.UnitId == data.FKExtUnitOfMeasureId).UnitSymbol;

      return data;
    }

    public DataSourceResult GetFeatureTemplateSearchList(PEContext peCtx, DataSourceRequest request)
    {
      return peCtx.MVHFeatureTemplates
        .ToListAsync().GetAwaiter().GetResult()
        .ToDataSourceLocalResult(request, data => new VM_FeatureTemplate(data));
    }

    public DataSourceResult GetAssignedFeatureTemplateSearchList(PEContext peCtx, DataSourceRequest request, long assetTemplateId)
    {
      return peCtx.MVHAssetFeatureTemplates
        .Include(x => x.FKAssetTemplate)
        .Where(x => x.FKAssetTemplateId == assetTemplateId)
        .Select(x => x.FKFeatureTemplate)
        .ToListAsync().GetAwaiter().GetResult()
        .ToDataSourceLocalResult(request, data => new VM_FeatureTemplate(data));
    }

    public DataSourceResult GetUnassignedFeatureTemplateSearchList(PEContext peCtx, DataSourceRequest request, long assetTemplateId)
    {
      return peCtx.MVHFeatureTemplates
        .Include(x => x.MVHAssetFeatureTemplates)
        .Where(x => !x.MVHAssetFeatureTemplates.Select(y => y.FKAssetTemplateId).Contains(assetTemplateId))
        .ToListAsync().GetAwaiter().GetResult()
        .ToDataSourceLocalResult(request, data => new VM_FeatureTemplate(data));
    }

    public IList<UnitOfMeasure> GetUnitOfMeasureList(SMFContext smfCtx)
    {
      return smfCtx.UnitOfMeasures
        .ToList();
    }

    public IList<DBDataType> GetDataTypeList(PEContext peCtx)
    {
      return peCtx.DBDataTypes
        .ToList();
    }

    public async Task<VM_Base> CreateFeatureTemplateAsync(ModelStateDictionary modelState, PEContext ctx, VM_FeatureTemplate data)
    {
      VM_Base result = new VM_Base();

      await CheckFeatureTemplateUniqueNameAsync(ctx, modelState, data.FeatureTemplateName);

      if (!modelState.IsValid)
      {
        return result;
      }

      await ctx.MVHFeatureTemplates.AddAsync(new MVHFeatureTemplate
      {
        FKUnitOfMeasureId = data.FKUnitOfMeasureId,
        FKExtUnitOfMeasureId = data.FKExtUnitOfMeasureId,
        FKDataTypeId = data.FKDataTypeId,
        FeatureTemplateName = data.FeatureTemplateName,
        FeatureTemplateDescription = data.FeatureTemplateDescription,
        EnumFeatureType = FeatureType.GetValue(data.EnumFeatureType),
        EnumCommChannelType = CommChannelType.GetValue(data.EnumCommChannelType),
        EnumAggregationStrategy = AggregationStrategy.GetValue(data.EnumAggregationStrategy),
        TemplateCommAttr1 = data.TemplateCommAttr1,
        TemplateCommAttr2 = data.TemplateCommAttr2,
        TemplateCommAttr3 = data.TemplateCommAttr3,
      });

      await ctx.SaveChangesAsync();

      //return view model
      return result;
    }

    public async Task<VM_Base> EditFeatureTemplateAsync(ModelStateDictionary modelState, PEContext ctx, VM_FeatureTemplate data)
    {
      VM_Base result = new VM_Base();

      await CheckFeatureTemplateUniqueNameAsync(ctx, modelState, data.FeatureTemplateName, data.FeatureTemplateId);

      if (!modelState.IsValid)
      {
        return result;
      }

      var featureTemplate = await ctx.MVHFeatureTemplates.FirstAsync(x => x.FeatureTemplateId == data.FeatureTemplateId);

      featureTemplate.FKExtUnitOfMeasureId = data.FKExtUnitOfMeasureId;
      featureTemplate.FKDataTypeId = data.FKDataTypeId;
      featureTemplate.FeatureTemplateName = data.FeatureTemplateName;
      featureTemplate.FeatureTemplateDescription = data.FeatureTemplateDescription;
      featureTemplate.EnumFeatureType = FeatureType.GetValue(data.EnumFeatureType);
      featureTemplate.EnumCommChannelType = CommChannelType.GetValue(data.EnumCommChannelType);
      featureTemplate.EnumAggregationStrategy = AggregationStrategy.GetValue(data.EnumAggregationStrategy);
      featureTemplate.TemplateCommAttr1 = data.TemplateCommAttr1;
      featureTemplate.TemplateCommAttr2 = data.TemplateCommAttr2;
      featureTemplate.TemplateCommAttr3 = data.TemplateCommAttr3;

      await ctx.SaveChangesAsync();

      //return view model
      return result;
    }

    public async Task<VM_Base> AssignTemplatesAsync(ModelStateDictionary modelState, PEContext ctx, long featureTemplateId, long assetTemplateId)
    {
      VM_Base result = new VM_Base();

      var featureTemplate = await ctx.MVHAssetFeatureTemplates
        .FirstOrDefaultAsync(x => x.FKFeatureTemplateId == featureTemplateId && x.FKAssetTemplateId == assetTemplateId);

      if (featureTemplate is not null)
        ctx.MVHAssetFeatureTemplates.Remove(featureTemplate);
      else
      {
        await CheckAssetTemplateAssignmentPossibilityAsync(ctx, modelState, assetTemplateId);

        if (!modelState.IsValid)
        {
          return result;
        }

        await ctx.MVHAssetFeatureTemplates.AddAsync(new MVHAssetFeatureTemplate
        {
          FKAssetTemplateId = assetTemplateId,
          FKFeatureTemplateId = featureTemplateId
        });
      }

      await ctx.SaveChangesAsync();

      //return view model
      return result;
    }

    public async Task<VM_Base> DeleteFeatureTemplateAsync(ModelStateDictionary modelState, PEContext ctx, long featureTemplateId)
    {
      VM_Base result = new VM_Base();

      await CheckFeatureTemplateAssignmentAsync(ctx, modelState, featureTemplateId);

      if (!modelState.IsValid)
      {
        return result;
      }

      ctx.MVHFeatureTemplates.Remove(await ctx.MVHFeatureTemplates.FirstAsync(x => x.FeatureTemplateId == featureTemplateId));

      await ctx.SaveChangesAsync();

      //return view model
      return result;
    }

    private async Task CheckFeatureTemplateUniqueNameAsync(PEContext ctx, ModelStateDictionary modelState, string featureTemplateName, long? featureTemplateId = null)
    {
      MVHFeatureTemplate featureTemplate = null;
      if (featureTemplateId is null)
        featureTemplate = await ctx.MVHFeatureTemplates
          .FirstOrDefaultAsync(x => x.FeatureTemplateName.ToUpper().Equals(featureTemplateName.ToUpper()));
      else
        featureTemplate = await ctx.MVHFeatureTemplates
          .FirstOrDefaultAsync(x => x.FeatureTemplateId != featureTemplateId && x.FeatureTemplateName.ToUpper().Equals(featureTemplateName.ToUpper()));

      if (featureTemplate is not null)
        modelState.AddModelError("error", ResourceController.GetErrorText("FeatureTemplateNameNotUnique"));
    }

    private async Task CheckFeatureTemplateAssignmentAsync(PEContext ctx, ModelStateDictionary modelState, long featureTemplateId)
    {
      MVHAssetFeatureTemplate assetFeatureTemplate = await ctx.MVHAssetFeatureTemplates
        .FirstOrDefaultAsync(x => x.FKFeatureTemplateId == featureTemplateId);

      if (assetFeatureTemplate is not null)
        modelState.AddModelError("error", ResourceController.GetErrorText("FeatureTemplateAssignedToAssetTemplate"));
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
