using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.EVT;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Extensions;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Event;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class EventGroupsCatalogueService : BaseService, IEventGroupsCatalogueService
  {
    private readonly PEContext _peContext;

    public EventGroupsCatalogueService(IHttpContextAccessor httpContextAccessor, PEContext peContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
    }

    public VM_EventGroupsCatalogue GetEventGroup(ModelStateDictionary modelState, long id)
    {
      VM_EventGroupsCatalogue result = null;

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

      EVTEventCategoryGroup eventCategory = _peContext.EVTEventCategoryGroups
                      .SingleOrDefault(x => x.EventCategoryGroupId == id);
      result = eventCategory == null ? null : new VM_EventGroupsCatalogue(eventCategory);
      
      return result;
    }

    public DataSourceResult GetEventGroupList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = _peContext.EVTEventCategoryGroups
                  .ToDataSourceLocalResult<EVTEventCategoryGroup, VM_EventGroupsCatalogue>(request, modelState, x => new VM_EventGroupsCatalogue(x));
      
      return result;
    }

    //public IList<VM_DelayGroupsCatalogue> GetDelayGroups()
    //{
    //  List<VM_DelayGroupsCatalogue> result = new List<VM_DelayGroupsCatalogue>();
    //  using (PEContext ctx = new PEContext())
    //  {

    //    IQueryable<EVTDelayCategoryGroup> dbList = ctx.EVTDelayCategoryGroups.AsQueryable();
    //    foreach (EVTDelayCategoryGroup item in dbList)
    //    {
    //      result.Add(new VM_DelayGroupsCatalogue(item));
    //    }
    //  }
    //  return result;
    //}

    public async Task<VM_Base> UpdateEventGroupAsync(ModelStateDictionary modelState,
      VM_EventGroupsCatalogue eventGroupsCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref eventGroupsCatalogue);

      DCEventGroup dc = new DCEventGroup
      {
        Id = eventGroupsCatalogue.EventCategoryGroupId,
        GroupName = eventGroupsCatalogue.EventCategoryGroupName,
        GroupCode = eventGroupsCatalogue.EventCategoryGroupCode
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateEventCategoryGroupAsync(dc);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> AddEventGroupAsync(ModelStateDictionary modelState,
      VM_EventGroupsCatalogue eventGroupsCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref eventGroupsCatalogue);

      DCEventGroup dc = new DCEventGroup
      {
        GroupName = eventGroupsCatalogue.EventCategoryGroupName,
        GroupCode = eventGroupsCatalogue.EventCategoryGroupCode
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.AddEventCategoryGroupAsync(dc);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> DeleteEventGroupAsync(ModelStateDictionary modelState,
      VM_EventGroupsCatalogue eventGroupsCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref eventGroupsCatalogue);

      DCEventGroup dc = new DCEventGroup {Id = eventGroupsCatalogue.EventCategoryGroupId};
      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.DeleteEventCategoryGroupAsync(dc);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<bool> ValidateEventGroupsCode(string code)
    {
      bool exists = false;
      if (!string.IsNullOrEmpty(code))
      {
        exists = await _peContext.EVTEventCategoryGroups.AnyAsync(p => p.EventCategoryGroupCode == code);
      }
      return exists;
    }

    public async Task<bool> ValidateEventGroupsName(string name)
    {
      bool exists = false;
      if (!string.IsNullOrEmpty(name))
      {
        exists = await _peContext.EVTEventCategoryGroups.AnyAsync(p => p.EventCategoryGroupName == name);
      }
      return exists;
    }
  }
}
