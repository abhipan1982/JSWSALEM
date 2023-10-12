using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.EVT;
using PE.DbEntity.HmiModels;
using PE.DbEntity.PEContext;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Extensions;
using PE.HMIWWW.Core.Helpers;
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
  public class EventCatalogueCategoriesService : BaseService, IEventCatalogueCategoriesService
  {
    private readonly PEContext _peContext;
    private readonly HmiContext _hmiContext;

    public EventCatalogueCategoriesService(IHttpContextAccessor httpContextAccessor, PEContext peContext, HmiContext hmiContext)
      : base(httpContextAccessor)
    {
      _peContext = peContext;
      _hmiContext = hmiContext;
    }

    public VM_EventCatalogueCategory GetEventCatalogueCategory(ModelStateDictionary modelState, long id)
    {
      VM_EventCatalogueCategory result = null;

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

      EVTEventCatalogueCategory eventCategory = _peContext.EVTEventCatalogueCategories
                    .Include(x => x.FKEventCategoryGroup)
                    .Include(x => x.FKEventType)
                    .SingleOrDefault(x => x.EventCatalogueCategoryId == id);
      result = eventCategory == null ? null : new VM_EventCatalogueCategory(eventCategory);

      return result;
    }

    public DataSourceResult GetEventCatalogueCategoriesList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;
      result = _hmiContext.V_EventCategorySearchGrids
                  .ToDataSourceLocalResult<V_EventCategorySearchGrid, VM_EventCatalogueCategory>(request, modelState, x => new VM_EventCatalogueCategory(x));

      return result;
    }

    public IList<VM_EventCatalogueCategory> GetEventCatalogueCategories()
    {
      List<VM_EventCatalogueCategory> result = new List<VM_EventCatalogueCategory>();
      //TODOMN - refactor this - use view
      IQueryable<EVTEventCatalogueCategory> dbList = _peContext.EVTEventCatalogueCategories.AsQueryable();
      foreach (EVTEventCatalogueCategory item in dbList)
      {
        result.Add(new VM_EventCatalogueCategory(item));
      }
      return result;
    }

    public async Task<VM_Base> UpdateEventCatalogueCategoriesAsync(ModelStateDictionary modelState,
      VM_EventCatalogueCategory vm)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref vm);

      DCEventCatalogueCategory dc = new DCEventCatalogueCategory
      {
        EventCatalogueCategoryId = vm.Id,
        EventCatalogueCategoryName = vm.EventCatalogueCategoryName,
        EventCatalogueCategoryCode = vm.EventCatalogueCategoryCode,
        EventCatalogueCategoryDescription = vm.EventCatalogueCategoryDescription,
        IsDefault = vm.IsDefault,
        FKEventCategoryGroupId = vm.EventCategoryGroupId,
        EnumAssignmentType = AssignmentType.GetValue(vm.EnumAssignmentTypeId),
        EventTypeId = vm.EventTypeId.Value
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateEventCatalogueCategoryAsync(dc);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> AddEventCatalogueCategoryAsync(ModelStateDictionary modelState,
      VM_EventCatalogueCategory vm)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref vm);

      DCEventCatalogueCategory dc = new DCEventCatalogueCategory
      {
        EventCatalogueCategoryId = vm.Id,
        EventCatalogueCategoryName = vm.EventCatalogueCategoryName,
        EventCatalogueCategoryCode = vm.EventCatalogueCategoryCode,
        EventCatalogueCategoryDescription = vm.EventCatalogueCategoryDescription,
        IsDefault = vm.IsDefault,
        FKEventCategoryGroupId = vm.EventCategoryGroupId,
        EnumAssignmentType = AssignmentType.GetValue(vm.EnumAssignmentTypeId),
        EventTypeId = vm.EventTypeId.Value
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.AddEventCatalogueCategoryAsync(dc);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> DeleteEventCatalogueCategoryAsync(ModelStateDictionary modelState,
      VM_EventCatalogueCategory vm)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref vm);

      DCEventCatalogueCategory dc = new DCEventCatalogueCategory {EventCatalogueCategoryId = vm.Id};
      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.DeleteEventCatalogueCategoryAsync(dc);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public IList<VM_EventGroupsCatalogue> GetEventGroups()
    {
      List<VM_EventGroupsCatalogue> result = new List<VM_EventGroupsCatalogue>();
      IQueryable<EVTEventCategoryGroup> dbList = _peContext.EVTEventCategoryGroups.AsQueryable();
      foreach (EVTEventCategoryGroup item in dbList)
      {
        result.Add(new VM_EventGroupsCatalogue(item));
      }
      return result;
    }

    public async Task<bool> ValidateEventCategoriesCode(string code)
    {
      bool exists = false;
      if (!string.IsNullOrEmpty(code))
      {
        exists = await _peContext.EVTEventCatalogueCategories.AnyAsync(p => p.EventCatalogueCategoryCode == code);
      }
      return exists;
    }

    public async Task<bool> ValidateEventCategoriesName(string name)
    {
      bool exists = false;
      if (!string.IsNullOrEmpty(name))
      {
        exists = await _peContext.EVTEventCatalogueCategories.AnyAsync(p => p.EventCatalogueCategoryName == name);
      }
      return exists;
    }

    public SelectList GetEnumAssignmentTypeList()
    {
      return SelectListHelpers.GetSelectList<AssignmentType, int>();
    }

    public List<DropDownTreeItemModel> GetEventTypesTree()
    {
      var data = _peContext.EVTEventTypes.ToList().ToLookup(c => c.FKParentEvenTypeId);

      var tree = data[null].Select(x => new DropDownTreeItemModel
      {
        Value = x.EventTypeId.ToString(),
        Text = x.EventTypeName,
        Expanded = true,
        Items = data[x.EventTypeId].Any()
          ? data[x.EventTypeId].Select(n => new DropDownTreeItemModel {
              Value = n.EventTypeId.ToString(),
              Text = n.EventTypeName,
            }).ToList()
          : null
      }).ToList();

      return tree;
    }
  }
}
