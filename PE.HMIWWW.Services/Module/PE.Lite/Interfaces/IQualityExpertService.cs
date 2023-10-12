using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.QualityExpert;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IQualityExpertService
  {
    DataSourceResult GetRawMaterialSearchList(ModelStateDictionary modelState, DataSourceRequest request);
    DataSourceResult GetGradingPerAssetForMaterial(ModelStateDictionary modelState, DataSourceRequest request, long selectedRawMaterial);
    DataSourceResult GetRatingsForAssetForMaterial(ModelStateDictionary modelState, DataSourceRequest request, long rawMaterialId, long assetId);
    Dictionary<double, string> GetRatingValuesTypes();
    VM_ForceRatingValue GetRatingById(ModelStateDictionary modelState, long ratingId);
    Task<VM_Base> ForceRatingValue(ModelStateDictionary modelState, VM_ForceRatingValue reqeust);
    VM_RatingDetailsValue GetRatingDetailsById(ModelStateDictionary modelState, long ratingId);
    DataSourceResult GetRootCausesByRatingId(ModelStateDictionary modelState, DataSourceRequest request, long ratingId);
    DataSourceResult GetCompensationsByRatingId(ModelStateDictionary modelState, DataSourceRequest request, long ratingId);
    Task<VM_Base> ToggleCompensation(ModelStateDictionary modelState, long compensationId, long ratingId, bool isChosen);
    Task<VM_MaterialGrading> GetMaterialGrading(ModelStateDictionary modelState, long rawMaterialId);
    Task<VM_QualityExpertOverview> GetOverview(ModelStateDictionary modelState, VM_QualityExpertRawMaterial selectedMaterialInfo);
  }
}
