using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.RLS;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module;
using PE.HMIWWW.ViewModel.System;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;

public class GrooveTemplateService : BaseService, IGrooveTemplateService
{
  private readonly PEContext _peContext;

  public GrooveTemplateService(IHttpContextAccessor httpContextAccessor, PEContext peContext) : base(httpContextAccessor)
  {
    _peContext = peContext;
  }

  #region interface GrooveTemplatesService

  public DataSourceResult GetGrooveTemplateList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
  {
    DataSourceResult result = null;

    var list = _peContext.RLSGrooveTemplates
      .Where(x => x.EnumGrooveTemplateStatus != GrooveTemplateStatus.History);
    result = list.ToDataSourceLocalResult(request, modelState, data => new VM_GrooveTemplate(data));

    return result;
  }

  public VM_GrooveTemplate GetGrooveTemplate(ModelStateDictionary modelState, long id)
  {
    VM_GrooveTemplate returnValueVm = null;

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
    RLSGrooveTemplate grooveTemplate = _peContext.RLSGrooveTemplates.First(x => x.GrooveTemplateId == id && x.EnumGrooveTemplateStatus != GrooveTemplateStatus.History);
    if (grooveTemplate != null)
    {
      returnValueVm = new VM_GrooveTemplate(grooveTemplate);
    }

    //END OF DB OPERATION

    return returnValueVm;
  }
  public async Task<VM_Base> InsertGrooveTemplate(ModelStateDictionary modelState, VM_GrooveTemplate viewModel)
  {
    VM_Base result = new VM_Base();

    if (!modelState.IsValid)
      return result;

    UnitConverterHelper.ConvertToSi(ref viewModel);

    DCGrooveTemplateData entryDataContract = new DCGrooveTemplateData
    {
      AccBilletCntLimit = viewModel.AccBilletCntLimit,
      AccWeightLimit = viewModel.AccBilletWeightLimit,
      Angle1 = viewModel.Angle1,
      Angle2 = viewModel.Angle2,
      D1 = viewModel.D1,
      D2 = viewModel.D2,
      Description = viewModel.GrooveTemplateDescription,
      GrindingProgramName = viewModel.GrindingProgramName,
      GroovePlane = viewModel.Plane,
      GrooveTemplateName = viewModel.GrooveTemplateName,
      GrooveTemplateCode = viewModel.GrooveTemplateCode,
      NameShort = viewModel.NameShort,
      R1 = viewModel.R1,
      R2 = viewModel.R2,
      R3 = viewModel.R3,
      Shape = viewModel.Shape,
      SpreadFactor = viewModel.SpreadFactor,
      Status = viewModel.EnumGrooveTemplateStatus,
      W1 = viewModel.W1,
      W2 = viewModel.W2,
      Ds = viewModel.Ds,
      Dw = viewModel.Dw,
      EnumGrooveSetting = GrooveSetting.GetValue(viewModel.EnumGrooveSetting)
    };


    SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.InsertGrooveTemplateAsync(entryDataContract);

    //handle warning information
    HandleWarnings(sendOfficeResult, ref modelState);

    //return view model
    return result;
  }
  public async Task<VM_Base> UpdateGrooveTemplate(ModelStateDictionary modelState, VM_GrooveTemplate viewModel)
  {
    VM_Base result = new VM_Base();

    if (!modelState.IsValid)
      return result;

    UnitConverterHelper.ConvertToSi(ref viewModel);

    DCGrooveTemplateData entryDataContract = new DCGrooveTemplateData
    {
      AccBilletCntLimit = viewModel.AccBilletCntLimit,
      AccWeightLimit = viewModel.AccBilletWeightLimit,
      Angle1 = viewModel.Angle1,
      Angle2 = viewModel.Angle2,
      D1 = viewModel.D1,
      D2 = viewModel.D2,
      Description = viewModel.GrooveTemplateDescription,
      GrindingProgramName = viewModel.GrindingProgramName,
      GroovePlane = viewModel.Plane,
      GrooveTemplateName = viewModel.GrooveTemplateName,
      GrooveTemplateCode = viewModel.GrooveTemplateCode,
      NameShort = viewModel.NameShort,
      R1 = viewModel.R1,
      R2 = viewModel.R2,
      R3 = viewModel.R3,
      Shape = viewModel.Shape,
      SpreadFactor = viewModel.SpreadFactor,
      Status = viewModel.EnumGrooveTemplateStatus,
      W1 = viewModel.W1,
      W2 = viewModel.W2,
      Ds = viewModel.Ds,
      Dw = viewModel.Dw,
      GrooveTemplateId = viewModel.GrooveTemplateId,
      EnumGrooveSetting = GrooveSetting.GetValue(viewModel.EnumGrooveSetting)
    };

    SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateGrooveTemplateAsync(entryDataContract);

    //handle warning information
    HandleWarnings(sendOfficeResult, ref modelState);

    //return view model
    return result;
  }

  public async Task<VM_Base> DeleteGrooveTemplate(ModelStateDictionary modelState, VM_LongId viewModel)
  {
    VM_Base result = new VM_Base();

    if (!modelState.IsValid)
      return result;

    UnitConverterHelper.ConvertToSi(ref viewModel);

    DCGrooveTemplateData entryDataContract = new DCGrooveTemplateData();
    entryDataContract.GrooveTemplateId = viewModel.Id;

    //DB OPERATION
    SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.DeleteGrooveTemplateAsync(entryDataContract);

    //handle warning information
    HandleWarnings(sendOfficeResult, ref modelState);

    //return view model
    return result;
  }

  #endregion
}
