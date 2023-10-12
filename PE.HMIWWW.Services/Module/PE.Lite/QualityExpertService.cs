using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.QEX;
using PE.DbEntity.HmiModels;
using PE.DbEntity.PEContext;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Extensions;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.QualityExpert;
using SMF.Core.Communication;
using SMF.Core.DC;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class QualityExpertService : BaseService, IQualityExpertService
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;

    public QualityExpertService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    public DataSourceResult GetRawMaterialSearchList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _hmiContext.V_QualityExpertSearchGrids
        .ToDataSourceLocalResult(request, modelState, data => new VM_QualityExpertRawMaterial(data));

      return result;
    }

    public async Task<VM_QualityExpertOverview> GetOverview(ModelStateDictionary modelState, VM_QualityExpertRawMaterial selectedMaterialInfo)
    {
      var result = new VM_QualityExpertOverview();
      var selectedRawMaterialId = selectedMaterialInfo.RawMaterialId;

      var selectedRawMaterial = await _peContext.TRKRawMaterials.FirstAsync(x => x.RawMaterialId == selectedRawMaterialId);

      // If material is not a child and does not have any parent
      if (selectedRawMaterial.CuttingSeqNo == 0 && selectedRawMaterial.ChildsNo == 0)
      {
        var rawMaterialModel = new VM_QualityExpertSlimRawMaterial(selectedRawMaterial);
        result.HeaderRawMaterial = rawMaterialModel;
        result.CurrentRawMaterial = rawMaterialModel;
        result.RawMaterials = new List<VM_QualityExpertSlimRawMaterial> { rawMaterialModel };
        result.ChildMaterials = 0;
      }
      else
      {
        List<TRKRawMaterialRelation> relatives;

        if (selectedRawMaterial.ChildsNo > 0)
        {
          relatives = await _peContext.TRKRawMaterialRelations.Include(x => x.ChildRawMaterial).Include(x => x.ParentRawMaterial)
            .Where(x => x.ParentRawMaterialId == selectedRawMaterialId || x.ChildRawMaterialId == selectedRawMaterialId).ToListAsync();
        }
        else
        {
          var parent = await _peContext.TRKRawMaterialRelations.Include(x => x.ChildRawMaterial).Include(x => x.ParentRawMaterial)
            .FirstAsync(x => x.ChildRawMaterialId == selectedRawMaterialId);
          relatives = await _peContext.TRKRawMaterialRelations.Include(x => x.ChildRawMaterial).Include(x => x.ParentRawMaterial)
            .Where(x => x.ParentRawMaterialId == parent.ParentRawMaterialId).ToListAsync();
        }

        var childRawMaterials = relatives.Select(x => new VM_QualityExpertSlimRawMaterial(x.ChildRawMaterial)).ToList();
        result.RawMaterials = childRawMaterials;
        result.ChildMaterials = childRawMaterials.Count;

        if (selectedRawMaterial.CuttingSeqNo == 0)
        {
          result.HeaderRawMaterial = new VM_QualityExpertSlimRawMaterial(selectedRawMaterial);
          result.CurrentRawMaterial = result.RawMaterials.First();
        }
        else
        {
          result.HeaderRawMaterial = new VM_QualityExpertSlimRawMaterial(relatives.First().ParentRawMaterial);
          result.CurrentRawMaterial = new VM_QualityExpertSlimRawMaterial(selectedRawMaterial);
        }
      }

      return result;
    }

    public DataSourceResult GetGradingPerAssetForMaterial(ModelStateDictionary modelState, DataSourceRequest request, long selectedRawMaterial)
    {
      var materialIds = GetRawMaterialIdsWithParent(selectedRawMaterial).GetAwaiter().GetResult();

      List<V_QERating> result = _hmiContext.V_QERatings
        .Where(x => materialIds.Contains(x.RawMaterialId) && x.RatingRanking == 1)
        .GroupBy(x => x.AssetId, (key, g) => g.OrderByDescending(y => y.RatingCurrentValue).First())
        .ToList();
      
      return result.Select(x => new VM_Rating(x, selectedRawMaterial)).ToDataSourceResult(request, modelState);
    }

    public DataSourceResult GetRatingsForAssetForMaterial(ModelStateDictionary modelState, DataSourceRequest request, long rawMaterialId, long assetId)
    {
      var materialIds = GetRawMaterialIdsWithParent(rawMaterialId).GetAwaiter().GetResult();

      List<V_QERating> result = _hmiContext.V_QERatings
          .Where(x => materialIds.Contains(x.RawMaterialId) && x.AssetId == assetId)
          .ToList();

      return result.Select(x => new VM_Rating(x)).ToDataSourceResult(request, modelState);
    }

    public async Task<VM_MaterialGrading> GetMaterialGrading(ModelStateDictionary modelState, long rawMaterialId)
    {
      var rating = await _peContext.TRKRawMaterials
          .Where(x => x.RawMaterialId == rawMaterialId)
          .Select(x => new VM_MaterialGrading { Grading = (short?)x.LastGrading ?? 0, GradingSource = x.EnumGradingSource })
          .FirstOrDefaultAsync();

      return rating;
    }

    public Dictionary<double, string> GetRatingValuesTypes()
    {
      var resultDictionary = new Dictionary<double, string>
      {
        { 5, VM_Resources.ResourceManager.GetString("RATING_VALUE_Critical") },
        { 4, VM_Resources.ResourceManager.GetString("RATING_VALUE_Bad") },
        { 3, VM_Resources.ResourceManager.GetString("RATING_VALUE_Caution") },
        { 2, VM_Resources.ResourceManager.GetString("RATING_VALUE_Sufficient") },
        { 1, VM_Resources.ResourceManager.GetString("RATING_VALUE_Good") },
        { 0, VM_Resources.ResourceManager.GetString("RATING_VALUE_Null") }
      };
      return resultDictionary;
    }

    public VM_ForceRatingValue GetRatingById(ModelStateDictionary modelState, long ratingId)
    {
      return _hmiContext.V_QERatings
        .Select(x => new VM_ForceRatingValue
        {
          FactRatingKey = x.RatingId,
          RatingValue = x.RatingValue.HasValue ? x.RatingValue : 0,
          RatingForcedValue = x.RatingValueForced,
          RatingCurrentValue = x.RatingCurrentValue
        })
        .First(x => x.FactRatingKey == ratingId);
    }

    public async Task<VM_Base> ForceRatingValue(ModelStateDictionary modelState, VM_ForceRatingValue reqeust)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      var dataContract = new DCRatingForce()
      {
        RatingId = reqeust.FactRatingKey,
        RatingForcedValue = (int?)reqeust.RatingForcedValue
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.ForceRatingValue(dataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public VM_RatingDetailsValue GetRatingDetailsById(ModelStateDictionary modelState, long ratingId)
    {

      VM_RatingDetailsValue result = _hmiContext.V_QERatings
        .Select(x => new VM_RatingDetailsValue
        {
          RatingId = x.RatingId,
          RatingValue = x.RatingValue.HasValue ? x.RatingValue : 0,
          RatingForcedValue = x.RatingValueForced,
          RatingCurrentValue = x.RatingCurrentValue,
          CreatedTs = x.RatingCreated,
          ModifiedTs = x.RatingModified,
        })
        .First(x => x.RatingId == ratingId);

      var rating = _peContext.QERatings.First(x => x.RatingId == ratingId);
      result.RatingCode = rating.RatingCode;
      result.RatingGroup = rating.RatingGroup;
      result.RatingType = rating.RatingType;
      result.RatingAlarm = rating.RatingAlarm;
      result.RatingAffectedArea = rating.RatingAffectedArea;

      return result;
    }

    public DataSourceResult GetRootCausesByRatingId(ModelStateDictionary modelState, DataSourceRequest request, long ratingId)
    {
      DataSourceResult result = null;
      if (!modelState.IsValid)
      {
        return result;
      }

      IQueryable<QERootCause> list = _peContext.QERootCauses
        .Include(x => x.QERootCauseAggregates)
        .Where(x => x.FKRatingId == ratingId);

      result = list.ToDataSourceResult<QERootCause, VM_RootCause>(request, modelState, data => new VM_RootCause(data));
      return result;
    }

    public DataSourceResult GetCompensationsByRatingId(ModelStateDictionary modelState, DataSourceRequest request, long ratingId)
    {
      DataSourceResult result = null;
      if (!modelState.IsValid)
      {
        return result;
      }

      IQueryable<QECompensation> list = _peContext.QECompensations
        .Include(x => x.FKCompensationType)
        .Include(x => x.QECompensationAggregates)
        .Where(x => x.FKRatingId == ratingId);

      result = list.ToDataSourceResult<QECompensation, VM_Compensation>(request, modelState, data => new VM_Compensation(data));
      return result;
    }

    public async Task<VM_Base> ToggleCompensation(ModelStateDictionary modelState, long compensationId, long ratingId, bool isChosen)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      var dataContract = new DCCompensationTrigger()
      {
        CompensationId = compensationId,
        RatingId = ratingId,
        IsChosen = isChosen
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.ToggleCompensation(dataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    private async Task<List<long>> GetRawMaterialIdsWithParent(long selectedRawMaterial)
    {
      long? rawMaterialId = selectedRawMaterial;
      var materialIds = new List<long>() { selectedRawMaterial };
      while (rawMaterialId.HasValue)
      {
        // get parent material
        rawMaterialId = await _peContext.TRKRawMaterialRelations
          .Where(x => x.ChildRawMaterialId == rawMaterialId)
          .Select(x => (long?)x.ParentRawMaterialId)
          .FirstOrDefaultAsync();
        if (rawMaterialId.HasValue) materialIds.Add(rawMaterialId.Value);
      }
      return materialIds;
    }
  }
}
