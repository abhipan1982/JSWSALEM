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
using PE.HMIWWW.ViewModel.Module.Lite.SteelFamily;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class SteelFamilyService : BaseService, ISteelFamilyService
  {
    private readonly PEContext _peContext;

    public SteelFamilyService(IHttpContextAccessor httpContextAccessor, PEContext peContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
    }

    public VM_SteelFamily GetSteelFamily(ModelStateDictionary modelState, long id)
    {
      VM_SteelFamily result = null;

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

      PRMSteelGroup sg = _peContext.PRMSteelGroups
        .SingleOrDefault(x => x.SteelGroupId == id);
      result = sg != null ? new VM_SteelFamily(sg) : null;

      return result;
    }

    public DataSourceResult GetSteelFamilyList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _peContext.PRMSteelGroups.ToDataSourceLocalResult(request, modelState, x => new VM_SteelFamily(x));

      return result;
    }

    public async Task<VM_Base> CreateSteelFamily(ModelStateDictionary modelState, VM_SteelFamily vm)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref vm);

      DCSteelFamily dcSteelFamily = new DCSteelFamily
      {
        SteelFamilyId = vm.Id,
        SteelFamilyCode = vm.SteelGroupCode,
        SteelFamilyName = vm.SteelGroupName,
        SteelFamilyDescription = vm.Description,
        WearCoefficient = vm.WearCoefficient,
        IsDefault = vm.IsDefault
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.SendCreateSteelFamilyAsync(dcSteelFamily);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> UpdateSteelFamily(ModelStateDictionary modelState, VM_SteelFamily vm)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref vm);

      DCSteelFamily dcSteelFamily = new DCSteelFamily
      {
        SteelFamilyId = vm.Id,
        SteelFamilyCode = vm.SteelGroupCode,
        SteelFamilyName = vm.SteelGroupName,
        SteelFamilyDescription = vm.Description,
        WearCoefficient = vm.WearCoefficient,
        IsDefault = vm.IsDefault
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.SendUpdateSteelFamilyAsync(dcSteelFamily);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> DeleteSteelFamily(ModelStateDictionary modelState, VM_SteelFamily vm)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref vm);

      DCSteelFamily dcSteelFamily = new DCSteelFamily {SteelFamilyId = vm.Id};

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.SendDeleteSteelFamilyAsync(dcSteelFamily);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public VM_SteelFamily GetSteelFamilyDetails(ModelStateDictionary modelState, long id)
    {
      VM_SteelFamily result = null;

      if (id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return result;
      }

      PRMSteelGroup data = _peContext.PRMSteelGroups
        .Where(x => x.SteelGroupId == id)
        .SingleOrDefault();

      result = new VM_SteelFamily(data);

      return result;
    }

    public IList<VM_SteelFamily> GetSteelFamilies()
    {
      IList<VM_SteelFamily> result = new List<VM_SteelFamily>();
      result = _peContext.PRMSteelGroups.AsEnumerable().Select(sg => new VM_SteelFamily(sg)).ToList();

      return result;
    }

    public async Task<bool> ValidateSteelFamilyCode(string code)
    {
      bool exists = false;
      if (!string.IsNullOrEmpty(code))
      {
        exists = await _peContext.PRMSteelGroups.AnyAsync(p => p.SteelGroupCode == code);
      }

      return exists;
    }

    public async Task<bool> ValidateSteelFamilyName(string name)
    {
      bool exists = false;
      if (!string.IsNullOrEmpty(name))
      {
        exists = await _peContext.PRMSteelGroups.AnyAsync(p => p.SteelGroupName == name);
      }

      return exists;
    }
  }
}
