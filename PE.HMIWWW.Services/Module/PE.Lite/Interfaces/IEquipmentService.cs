using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Maintenance;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IEquipmentService
  {
    VM_Equipment GetEquipment(long id);
    VM_EquipmentStatus GetEquipmentStatus(long id);
    VM_Equipment GetEmptyEquipment();
    DataSourceResult GetEquipmentList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);
    Task<VM_Base> UpdateEquipment(ModelStateDictionary modelState, VM_Equipment cat);
    Task<VM_Base> UpdateEquipmentStatus(ModelStateDictionary modelState, VM_EquipmentStatus cat);
    Task<VM_Base> CreateEquipment(ModelStateDictionary modelState, VM_Equipment cat);
    Task<VM_Base> CloneEquipment(ModelStateDictionary modelState, VM_Equipment cat);
    Task<VM_Base> DeleteEquipment(long id);
    DataSourceResult GetEquipmentHistoryList(long id, ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);
    VM_LongId GetEquipmentHistory(long id);
  }
}
