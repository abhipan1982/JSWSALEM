using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.MNT;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Extensions;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Maintenance;
using PE.HMIWWW.ViewModel.System;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class EquipmentService : BaseService, IEquipmentService
  {
    private readonly PEContext _peContext;

    public EquipmentService(IHttpContextAccessor httpContextAccessor, PEContext peContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
    }

    public VM_Equipment GetEquipment(long id)
    {
      VM_Equipment result = null;

      var equipment = _peContext.MNTEquipments
        .Include(i => i.FKEquipmentGroup)
        .SingleOrDefault(x => x.EquipmentId == id);
      result = equipment == null ? null : new VM_Equipment(equipment);

      return result;
    }

    public VM_EquipmentStatus GetEquipmentStatus(long id)
    {
      VM_EquipmentStatus result = null;

      var equipment = _peContext.MNTEquipments.SingleOrDefault(x => x.EquipmentId == id);
      result = equipment == null ? null : new VM_EquipmentStatus(equipment);

      return result;
    }

    public VM_Equipment GetEmptyEquipment()
    {
      var result = new VM_Equipment();
      return result;
    }

    public DataSourceResult GetEquipmentList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _peContext.MNTEquipments
        .Include(i => i.FKEquipmentGroup)
        .ToDataSourceLocalResult(request, modelState, x => new VM_Equipment(x));

      return result;
    }

    public async Task<VM_Base> CreateEquipment(ModelStateDictionary modelState, VM_Equipment data)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref data);

      var dc = new DCEquipment
      {
        AlarmValue = data.AlarmValue,
        EquipmentCode = data.EquipmentCode,
        EquipmentDescription = data.EquipmentDescription,
        EquipmentGroupId = data.EquipmentGroupId,
        EquipmentId = data.EquipmentId,
        EquipmentName = data.EquipmentName,
        ServiceExpires = data.ServiceExpires,
        WarningValue = data.WarningValue
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendEquipmentCreateRequest(dc);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> UpdateEquipment(ModelStateDictionary modelState, VM_Equipment data)
    {
      VM_Base result = new VM_Base();
      ValidateEditEquipment(modelState, data);

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref data);

      var dc = new DCEquipment
      {
        AlarmValue = data.AlarmValue,
        EquipmentCode = data.EquipmentCode,
        EquipmentDescription = data.EquipmentDescription,
        EquipmentGroupId = data.EquipmentGroupId,
        EquipmentId = data.EquipmentId,
        EquipmentName = data.EquipmentName,
        ServiceExpires = data.ServiceExpires,
        WarningValue = data.WarningValue,
        EqStatus = EquipmentStatus.GetValue(data.EquipmentStatus ?? 0),
        EqServiceType = ServiceType.GetValue(data.EnumServiceType ?? 0),
        Remark = data.Remark
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendEquipmentUpdateRequest(dc);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> UpdateEquipmentStatus(ModelStateDictionary modelState, VM_EquipmentStatus data)
    {
      VM_Base result = new VM_Base();
      ValidateEditEquipment(modelState, data);

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref data);

      var dc = new DCEquipment
      {
        AlarmValue = data.AlarmValue,
        EquipmentId = data.EquipmentId,
        ServiceExpires = data.ServiceExpires,
        WarningValue = data.WarningValue,
        EqStatus = EquipmentStatus.GetValue(data.EquipmentStatus ?? 0),
        EqServiceType = ServiceType.GetValue(data.EnumServiceType ?? 0),
        Remark = data.Remark
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendEquipmentStatusUpdateRequest(dc);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> DeleteEquipment(long id)
    {
      VM_Base result = new VM_Base();

      var dc = new DCEquipment
      {
        EquipmentId = id
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendEquipmentDeleteRequest(dc);

      //HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> CloneEquipment(ModelStateDictionary modelState, VM_Equipment data)
    {
      VM_Base result = new VM_Base();

      UnitConverterHelper.ConvertToSi(ref data);

      var dc = new DCEquipment
      {
        EquipmentCode = data.EquipmentCode,
        EquipmentId = data.EquipmentId,
        EquipmentName = data.EquipmentName,
        EquipmentDescription = data.EquipmentDescription,
        WarningValue = data.WarningValue,
        AlarmValue = data.AlarmValue
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendEquipmentCloneRequest(dc);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public DataSourceResult GetEquipmentHistoryList(long id, ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _peContext.MNTEquipmentHistories
        .Where(w => w.FKEquipmentId == id)
        .OrderBy(o => o.EquipmentHistoryId)
        .ToDataSourceLocalResult(request, modelState, x => new VM_EquipmentHistory(x));

      return result;
    }

    public VM_LongId GetEquipmentHistory(long id)
    {
      VM_LongId result = new VM_LongId
      {
        Id = id
      };

      return result;
    }

    private void ValidateEditEquipment(ModelStateDictionary modelState, VM_Equipment vm)
    {
      if (vm.AlarmValue.HasValue && vm.WarningValue.HasValue && vm.AlarmValue.Value < vm.WarningValue.Value)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("AlarmValueCannotBeLessThanWarningValue"));
      }

      if (vm.ServiceExpires.HasValue && vm.ServiceExpires.Value < DateTime.Now)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("ServiceExpiresCannotBeBeforeToday"));
      }

      if (vm.EquipmentStatus == EquipmentStatus.InOperation.Value &&
          vm.EnumServiceType == ServiceType.Undefined.Value)
      {
        AddModelStateError(modelState,
          ResourceController.GetErrorText("InOperationStatusCannotHaveUndefinedServiceType"));
      }

      if (vm.EnumServiceType == ServiceType.WeightRelated.Value && !vm.AlarmValue.HasValue)
      {
        AddModelStateError(modelState,
          ResourceController.GetErrorText("ForWeightRelatedServiceTypeAlarmValueIsRequired"));
      }

      if (vm.EnumServiceType == ServiceType.DateRelated.Value && !vm.ServiceExpires.HasValue)
      {
        AddModelStateError(modelState,
          ResourceController.GetErrorText("ForDateRelatedServiceTypeServiceExpiresIsRequired"));
      }

      if ((vm.EnumServiceType == ServiceType.Both.Value && !vm.ServiceExpires.HasValue) || !vm.AlarmValue.HasValue)
      {
        AddModelStateError(modelState,
          ResourceController.GetErrorText(
            "ForDateRelatedAndWeightRelatedServiceTypeServiceExpiresAndAlarmValueAreRequired"));
      }
    }

    private void ValidateEditEquipment(ModelStateDictionary modelState, VM_EquipmentStatus vm)
    {
      if (vm.AlarmValue.HasValue && vm.WarningValue.HasValue && vm.AlarmValue.Value < vm.WarningValue.Value)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("AlarmValueCannotBeLessThanWarningValue"));
      }

      if (vm.ServiceExpires.HasValue && vm.ServiceExpires.Value < DateTime.Now)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("ServiceExpiresCannotBeBeforeToday"));
      }

      if (vm.EquipmentStatus == EquipmentStatus.InOperation.Value &&
          vm.EnumServiceType == ServiceType.Undefined.Value)
      {
        AddModelStateError(modelState,
          ResourceController.GetErrorText("InOperationStatusCannotHaveUndefinedServiceType"));
      }

      if (vm.EnumServiceType == ServiceType.WeightRelated.Value && !vm.AlarmValue.HasValue)
      {
        AddModelStateError(modelState,
          ResourceController.GetErrorText("ForWeightRelatedServiceTypeAlarmValueIsRequired"));
      }

      if (vm.EnumServiceType == ServiceType.DateRelated.Value && !vm.ServiceExpires.HasValue)
      {
        AddModelStateError(modelState,
          ResourceController.GetErrorText("ForDateRelatedServiceTypeServiceExpiresIsRequired"));
      }

      if ((vm.EnumServiceType == ServiceType.Both.Value && !vm.ServiceExpires.HasValue) || !vm.AlarmValue.HasValue)
      {
        AddModelStateError(modelState,
          ResourceController.GetErrorText(
            "ForDateRelatedAndWeightRelatedServiceTypeServiceExpiresAndAlarmValueAreRequired"));
      }
    }
  }
}
