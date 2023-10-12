using System.Linq;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.BaseDbEntity.EnumClasses;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.RLS;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Cassette;
using PE.HMIWWW.ViewModel.System;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;
using PE.DbEntity.PEContext;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class CassetteService : BaseService, ICassetteService
  {
    private readonly HmiContext _hmiContext;

    public CassetteService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
    }

    #region interface ICassetteService
    public DataSourceResult GetCassetteList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;
      IQueryable<V_CassettesOverview> list = _hmiContext.V_CassettesOverviews
        .Where(x => x.EnumCassetteStatus != CassetteStatus.History.Value)
        .AsQueryable();
      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_CassetteOverview(data));

      return result;
    }

    public VM_CassetteOverview GetCassette(ModelStateDictionary modelState, long id)
    {
      VM_CassetteOverview returnValueVm = null;

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
        V_CassettesOverview cassette = _hmiContext.V_CassettesOverviews.FirstOrDefault(x => x.CassetteId == id && x.EnumCassetteStatus != CassetteStatus.History.Value);
        if (cassette != null)
        {
          returnValueVm = new VM_CassetteOverview(cassette);
        }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public async Task<VM_Base> InsertCassette(ModelStateDictionary modelState, VM_CassetteOverview viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCCassetteData entryDataContract = new DCCassetteData
      {
        CassetteName = viewModel.CassetteName,
        CassetteTypeId = viewModel.CassetteTypeId,
        NumberOfPositions = 1
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.InsertCassetteAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }


    public async Task<VM_Base> UpdateCassette(ModelStateDictionary modelState, VM_CassetteOverview viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);
      DCCassetteData entryDataContract = new DCCassetteData
      {
        Id = viewModel.CassetteId,
        CassetteName = viewModel.CassetteName,
        CassetteTypeId = viewModel.CassetteTypeId,
        NumberOfPositions = 1,
        Status = (CassetteStatus)viewModel.EnumCassetteStatus
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateCassetteAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> DeleteCassette(ModelStateDictionary modelState, VM_LongId viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCCassetteData entryDataContract = new DCCassetteData
      {
        Id = viewModel.Id
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.DeleteCassetteAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> DismountCassette(ModelStateDictionary modelState, VM_LongId viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCCassetteData entryDataContract = new DCCassetteData
      {
        Id = viewModel.Id
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.DismountCassetteAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    #endregion
  }
}
