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
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.ScrapGroup;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class ScrapService : BaseService, IScrapService
  {
    private readonly PEContext _peContext;

    public ScrapService(IHttpContextAccessor httpContextAccessor, PEContext peContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
    }

    public VM_ScrapGroup GetScrapGroup(ModelStateDictionary modelState, long id)
    {
      VM_ScrapGroup result = null;

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
      PRMScrapGroup sg = _peContext.PRMScrapGroups
        .SingleOrDefault(x => x.ScrapGroupId == id);
      result = sg != null ? new VM_ScrapGroup(sg) : null;

      return result;
    }

    public DataSourceResult GetScrapGroupList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _peContext.PRMScrapGroups.ToDataSourceLocalResult(request, modelState, x => new VM_ScrapGroup(x));

      return result;
    }

    public async Task<VM_Base> CreateScrapGroup(ModelStateDictionary modelState, VM_ScrapGroup scrapgroup)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref scrapgroup);

      DCScrapGroup dcScrapGroup = new DCScrapGroup
      {
        ScrapGroupId = scrapgroup.ScrapGroupId,
        ScrapGroupCode = scrapgroup.ScrapGroupCode,
        ScrapGroupName = scrapgroup.ScrapGroupName,
        ScrapGroupDescription = scrapgroup.ScrapGroupDescription
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendCreateScrapGroupAsync(dcScrapGroup);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> UpdateScrapGroup(ModelStateDictionary modelState, VM_ScrapGroup scrapgroup)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref scrapgroup);

      DCScrapGroup dcScrapGroup = new DCScrapGroup
      {
        ScrapGroupId = scrapgroup.ScrapGroupId,
        ScrapGroupCode = scrapgroup.ScrapGroupCode,
        ScrapGroupName = scrapgroup.ScrapGroupName,
        ScrapGroupDescription = scrapgroup.ScrapGroupDescription
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendScrapGroupAsync(dcScrapGroup);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> DeleteScrapGroup(ModelStateDictionary modelState, VM_ScrapGroup scrapgroup)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref scrapgroup);

      DCScrapGroup dcScrapGroup = new DCScrapGroup {ScrapGroupId = scrapgroup.ScrapGroupId};

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendDeleteScrapGroupAsync(dcScrapGroup);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public VM_ScrapGroup GetScrapGroupDetails(ModelStateDictionary modelState, long id)
    {
      VM_ScrapGroup result = null;

      if (id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return result;
      }

      PRMScrapGroup data = _peContext.PRMScrapGroups
        .Where(x => x.ScrapGroupId == id)
        .SingleOrDefault();

      result = new VM_ScrapGroup(data);

      return result;
    }

    public IList<VM_ScrapGroup> GetScrapGroups()
    {
      IList<VM_ScrapGroup> result = new List<VM_ScrapGroup>();
      result = _peContext.PRMScrapGroups.AsEnumerable().Select(sg => new VM_ScrapGroup(sg)).ToList();

      return result;
    }

    public async Task<bool> ValidateScrapGroupCode(string code)
    {
      bool exists = false;
      if (!string.IsNullOrEmpty(code))
      {
        exists = await _peContext.PRMScrapGroups.AnyAsync(p => p.ScrapGroupCode == code);
      }

      return exists;
    }

    public async Task<bool> ValidateScrapGroupName(string name)
    {
      bool exists = false;
      if (!string.IsNullOrEmpty(name))
      {
        exists = await _peContext.PRMScrapGroups.AnyAsync(p => p.ScrapGroupName == name);
      }

      return exists;
    }
  }
}
