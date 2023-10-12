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
  public class DefectGroupsCatalogueService : BaseService, IDefectGroupsCatalogueService
  {
    private readonly PEContext _peContext;

    public DefectGroupsCatalogueService(IHttpContextAccessor httpContextAccessor, PEContext peContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
    }

    public VM_DefectGroupsCatalogue GetDefectGroup(ModelStateDictionary modelState, long id)
    {
      VM_DefectGroupsCatalogue result = null;

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
      QTYDefectCategoryGroup defect = _peContext.QTYDefectCategoryGroups
        .SingleOrDefault(x => x.DefectCategoryGroupId == id);
      result = defect == null ? null : new VM_DefectGroupsCatalogue(defect);

      return result;
    }

    public DataSourceResult GetDefectGroupList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _peContext.QTYDefectCategoryGroups
        .ToDataSourceLocalResult(request, modelState, x => new VM_DefectGroupsCatalogue(x));

      return result;
    }

    //public IList<VM_DefectGroupsCatalogue> GetDefectGroups()
    //{
    //  List<VM_DefectGroupsCatalogue> result = new List<VM_DefectGroupsCatalogue>();
    //  using (PEContext ctx = new PEContext())
    //  {

    //    IQueryable<MVHDefectCategoryGroups> dbList = ctx.MVHDefectCategoryGroupss.AsQueryable();
    //    foreach (MVHDefectCategoryGroups item in dbList)
    //    {
    //      result.Add(new VM_DefectGroupsCatalogue(item));
    //    }
    //  }
    //  return result;
    //}

    public async Task<VM_Base> UpdateDefectGroupAsync(ModelStateDictionary modelState,
      VM_DefectGroupsCatalogue defectGroupsCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref defectGroupsCatalogue);

      DCDefectGroup dc = new DCDefectGroup
      {
        Id = defectGroupsCatalogue.DefectGroupId,
        DefectGroupName = defectGroupsCatalogue.DefectGroupName,
        DefectGroupCode = defectGroupsCatalogue.DefectGroupCode
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendUpdateDefectGroupAsync(dc);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> AddDefectGroupAsync(ModelStateDictionary modelState,
      VM_DefectGroupsCatalogue defectGroupsCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref defectGroupsCatalogue);

      DCDefectGroup dc = new DCDefectGroup
      {
        DefectGroupName = defectGroupsCatalogue.DefectGroupName,
        DefectGroupCode = defectGroupsCatalogue.DefectGroupCode
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendAddDefectGroupAsync(dc);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> DeleteDefectGroupAsync(ModelStateDictionary modelState,
      VM_DefectGroupsCatalogue defectGroupsCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref defectGroupsCatalogue);

      DCDefectGroup dc = new DCDefectGroup { Id = defectGroupsCatalogue.DefectGroupId };
      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendDeleteDefectGroupAsync(dc);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<bool> ValidateDefectGroupsCode(string code)
    {
      bool exists = false;
      if (!string.IsNullOrEmpty(code))
      {
        exists = await _peContext.QTYDefectCategoryGroups.AnyAsync(p => p.DefectCategoryGroupCode == code);
      }

      return exists;
    }

    public async Task<bool> ValidateDefectGroupsName(string name)
    {
      bool exists = false;
      if (!string.IsNullOrEmpty(name))
      {
        exists = await _peContext.QTYDefectCategoryGroups.AnyAsync(p => p.DefectCategoryGroupName == name);
      }

      return exists;
    }
  }
}
