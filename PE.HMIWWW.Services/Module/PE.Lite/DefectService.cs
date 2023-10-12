using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.QTY;
using PE.HMIWWW.Core.Communication;
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
  public class DefectService : BaseService, IDefectService
  {
    private readonly PEContext _peContext;

    public DefectService(IHttpContextAccessor httpContextAccessor, PEContext peContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
    }

    public VM_DefectCatalogue GetDefectDetails(ModelStateDictionary modelState, long id)
    {
      VM_DefectCatalogue result = null;

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
      QTYDefectCatalogue delay = _peContext.QTYDefectCatalogues.Include(x => x.FKParentDefectCatalogue)
        .Include(x => x.FKDefectCatalogueCategory)
        .SingleOrDefault(x => x.DefectCatalogueId == id);
      result = delay == null ? null : new VM_DefectCatalogue(delay);

      return result;
    }

    public DataSourceResult GetDefectCatalogueList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _peContext.QTYDefectCatalogues
        .Include(x => x.FKParentDefectCatalogue)
        .Include(x => x.FKDefectCatalogueCategory)
        .ToDataSourceLocalResult(request, modelState, x => new VM_DefectCatalogue(x));

      return result;
    }

    public VM_DefectCatalogue GetDelayCatalogue(long id)
    {
      VM_DefectCatalogue result = null;

      QTYDefectCatalogue defectCatalogue = _peContext.QTYDefectCatalogues
        .Include(x => x.FKParentDefectCatalogue)
        .Include(x => x.FKDefectCatalogueCategory)
        .SingleOrDefault(x => x.DefectCatalogueId == id);
      result = defectCatalogue == null ? null : new VM_DefectCatalogue(defectCatalogue);

      return result;
    }

    public IList<VM_DefectCatalogueCategory> GetDefectCategoryList()
    {
      List<VM_DefectCatalogueCategory> result = new List<VM_DefectCatalogueCategory>();
      IQueryable<QTYDefectCatalogueCategory> dbList = _peContext.QTYDefectCatalogueCategories.AsQueryable();
      foreach (QTYDefectCatalogueCategory item in dbList)
      {
        result.Add(new VM_DefectCatalogueCategory(item));
      }

      return result;
    }

    public async Task<VM_Base> UpdateDefectCatalogue(ModelStateDictionary modelState,
      VM_DefectCatalogue defectCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref defectCatalogue);

      DCDefectCatalogue dcDefectCatalogue = new DCDefectCatalogue
      {
        Id = defectCatalogue.DefectCatalogueId,
        DefectCatalogueCode = defectCatalogue.DefectCatalogueCode,
        DefectCatalogueName = defectCatalogue.DefectCatalogueName,
        DefectCatalogueDescription = defectCatalogue.DefectCatalogueDescription,
        IsDefault = defectCatalogue.IsDefault,
        FkDelayCatalogueCategoryId = defectCatalogue.DefectCatalogueCategoryId,
        ParentDefectCatalogueId = defectCatalogue.ParentDefectCatalogueId,
        IsActive = defectCatalogue.IsActive
      };


      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendDefectCatalogue(dcDefectCatalogue);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> AddDefectCatalogue(ModelStateDictionary modelState, VM_DefectCatalogue defectCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref defectCatalogue);

      DCDefectCatalogue dcDefectCatalogue = new DCDefectCatalogue
      {
        Id = 0,
        DefectCatalogueName = defectCatalogue.DefectCatalogueName,
        DefectCatalogueCode = defectCatalogue.DefectCatalogueCode,
        DefectCatalogueDescription = defectCatalogue.DefectCatalogueDescription,
        IsDefault = defectCatalogue.IsDefault,
        FkDelayCatalogueCategoryId = defectCatalogue.DefectCatalogueCategoryId,
        ParentDefectCatalogueId = defectCatalogue.ParentDefectCatalogueId,
        IsActive = defectCatalogue.IsActive
      };

      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.SendAddDefectCatalogue(dcDefectCatalogue);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> DeleteDefectCatalogueAsync(ModelStateDictionary modelState,
      VM_DefectCatalogue defectCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref defectCatalogue);

      DCDefectCatalogue dcDefectCatalogue = new DCDefectCatalogue { Id = defectCatalogue.DefectCatalogueId };

      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.SendDeleteDefectCatalogue(dcDefectCatalogue);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public IList<VM_DefectCatalogue> GetParentDefectCatalogues()
    {
      List<VM_DefectCatalogue> result = new List<VM_DefectCatalogue>();
      IQueryable<QTYDefectCatalogue> dbList = _peContext.QTYDefectCatalogues
        .Include(x => x.FKParentDefectCatalogue)
        .Include(x => x.FKDefectCatalogueCategory)
        .AsQueryable();
      foreach (QTYDefectCatalogue item in dbList)
      {
        result.Add(new VM_DefectCatalogue(item));
      }

      return result;
    }

    public async Task<bool> ValidateDefectCode(string code)
    {
      bool exists = false;
      if (!string.IsNullOrEmpty(code))
      {
        exists = await _peContext.QTYDefectCatalogues.AnyAsync(p => p.DefectCatalogueCode == code);
      }

      return exists;
    }

    public async Task<bool> ValidateDefectName(string name)
    {
      bool exists = false;
      if (!string.IsNullOrEmpty(name))
      {
        exists = await _peContext.QTYDefectCatalogues.AnyAsync(p => p.DefectCatalogueName == name);
      }

      return exists;
    }
  }
}
