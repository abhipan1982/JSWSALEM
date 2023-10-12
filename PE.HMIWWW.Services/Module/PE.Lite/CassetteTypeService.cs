using System.Linq;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.RLS;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.CassetteType;
using PE.HMIWWW.ViewModel.System;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class CassetteTypeService : BaseService, ICassetteTypeService
  {
    private readonly PEContext _peContext;

    public CassetteTypeService(IHttpContextAccessor httpContextAccessor, PEContext peContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
    }

    #region interface ICassetteTypeService

    public DataSourceResult GetCassetteTypeList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      IQueryable<RLSCassetteType> list = _peContext.RLSCassetteTypes.AsQueryable();
      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_CassetteType(data));

      return result;
    }

    public VM_CassetteType GetCassetteType(ModelStateDictionary modelState, long id)
    {
      VM_CassetteType returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION


      //DB OPERATION
      RLSCassetteType cassetteType = _peContext.RLSCassetteTypes.Where(x => x.CassetteTypeId == id).FirstOrDefault();
      if (cassetteType != null)
      {
        returnValueVm = new VM_CassetteType(cassetteType);
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public async Task<VM_Base> InsertCassetteType(ModelStateDictionary modelState, VM_CassetteType viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCCassetteTypeData entryDataContract = new DCCassetteTypeData
      {
        CassetteTypeName = viewModel.CassetteTypeName,
        Description = viewModel.CassetteTypeDescription,
        NumberOfRolls = viewModel.NumberOfRolls
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.InsertCassetteTypeAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }


    public async Task<VM_Base> UpdateCassetteType(ModelStateDictionary modelState, VM_CassetteType viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCCassetteTypeData entryDataContract = new DCCassetteTypeData
      {
        Id = viewModel.CassetteTypeId,
        CassetteTypeName = viewModel.CassetteTypeName,
        Description = viewModel.CassetteTypeDescription,
        NumberOfRolls = viewModel.NumberOfRolls
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateCassetteTypeAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> DeleteCassetteType(ModelStateDictionary modelState, VM_LongId viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCCassetteTypeData entryDataContract = new DCCassetteTypeData
      {
        Id = viewModel.Id
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.DeleteCassetteTypeAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    #endregion
  }
}
