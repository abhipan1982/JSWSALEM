using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.QTY;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Helpers;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Defect;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class DefectCatalogueCategoriesService : BaseService, IDefectCatalogueCategoriesService
  {
    private readonly PEContext _peContext;

    public DefectCatalogueCategoriesService(IHttpContextAccessor httpContextAccessor, PEContext peContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
    }

    public VM_DefectCatalogueCategory GetDefectCatalogueCategory(ModelStateDictionary modelState, long id)
    {
      VM_DefectCatalogueCategory result = null;

      //VALIDATE ENTRY PARAMETERS
      if (id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION
      QTYDefectCatalogueCategory defect = _peContext.QTYDefectCatalogueCategories
        .Include(x => x.FKDefectCategoryGroup)
        .SingleOrDefault(x => x.DefectCatalogueCategoryId == id);
      result = defect == null ? null : new VM_DefectCatalogueCategory(defect);

      return result;
    }

    public DataSourceResult GetDefectCatalogueCategoriesList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _peContext.QTYDefectCatalogueCategories
        .Include(x => x.FKDefectCategoryGroup)
        .ToDataSourceLocalResult(request, modelState, x => new VM_DefectCatalogueCategory(x));

      return result;
    }

    public IList<VM_DefectCatalogueCategory> GetDefectCatalogueCategories()
    {
      List<VM_DefectCatalogueCategory> result = new List<VM_DefectCatalogueCategory>();
      IQueryable<QTYDefectCatalogueCategory> dbList = _peContext.QTYDefectCatalogueCategories.AsQueryable();
      foreach (QTYDefectCatalogueCategory item in dbList)
      {
        result.Add(new VM_DefectCatalogueCategory(item));
      }

      return result;
    }

    public async Task<VM_Base> UpdateDefectCatalogueCategoriesAsync(ModelStateDictionary modelState,
      VM_DefectCatalogueCategory vm)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref vm);

      DCDefectCatalogueCategory dc = new DCDefectCatalogueCategory
      {
        DefectCatalogueCategoryId = vm.Id,
        DefectCatalogueCategoryName = vm.DefectCatalogueCategoryName,
        DefectCatalogueCategoryCode = vm.DefectCatalogueCategoryCode,
        DefectCatalogueCategoryDescription = vm.DefectCatalogueCategoryDescription,
        IsDefault = vm.IsDefault,
        FKDefectCategoryGroupsId = vm.DefectCategoryGroupId,
        EnumAssignmentType = vm.EnumAssignmentTypeId
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendDefectCatalogueCategoryAsync(dc);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> AddDefectCatalogueCategoryAsync(ModelStateDictionary modelState,
      VM_DefectCatalogueCategory vm)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref vm);

      DCDefectCatalogueCategory dc = new DCDefectCatalogueCategory
      {
        DefectCatalogueCategoryId = vm.Id,
        DefectCatalogueCategoryName = vm.DefectCatalogueCategoryName,
        DefectCatalogueCategoryCode = vm.DefectCatalogueCategoryCode,
        DefectCatalogueCategoryDescription = vm.DefectCatalogueCategoryDescription,
        IsDefault = vm.IsDefault,
        FKDefectCategoryGroupsId = vm.DefectCategoryGroupId,
        EnumAssignmentType = vm.EnumAssignmentTypeId
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendAddDefectCatalogueCategoryAsync(dc);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> DeleteDefectCatalogueCategoryAsync(ModelStateDictionary modelState,
      VM_DefectCatalogueCategory vm)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref vm);

      DCDefectCatalogueCategory dc = new DCDefectCatalogueCategory { DefectCatalogueCategoryId = vm.Id };
      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.SendDeleteDefectCatalogueCategoryAsync(dc);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public IList<VM_DefectGroupsCatalogue> GetDefectGroups()
    {
      List<VM_DefectGroupsCatalogue> result = new List<VM_DefectGroupsCatalogue>();
      IQueryable<QTYDefectCategoryGroup> dbList = _peContext.QTYDefectCategoryGroups.AsQueryable();
      foreach (QTYDefectCategoryGroup item in dbList)
      {
        result.Add(new VM_DefectGroupsCatalogue(item));
      }

      return result;
    }

    public async Task<bool> ValidateDefectCategoriesCode(string code)
    {
      bool exists = false;
      if (!string.IsNullOrEmpty(code))
      {
        exists = await _peContext.QTYDefectCatalogueCategories.AnyAsync(p => p.DefectCatalogueCategoryCode == code);
      }

      return exists;
    }

    public async Task<bool> ValidateDefectCategoriesName(string name)
    {
      bool exists = false;
      if (!string.IsNullOrEmpty(name))
      {
        exists = await _peContext.QTYDefectCatalogueCategories.AnyAsync(p => p.DefectCatalogueCategoryName == name);
      }

      return exists;
    }

    public SelectList GetEnumAssignmentTypeList()
    {
      return SelectListHelpers.GetSelectList<AssignmentType, int>();
    }
  }
}
