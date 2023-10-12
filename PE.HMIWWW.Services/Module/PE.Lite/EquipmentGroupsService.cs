using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.MNT;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Extensions;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Maintenance;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class EquipmentGroupsService : BaseService, IEquipmentGroupsService
  {
    private readonly PEContext _peContext;

    public EquipmentGroupsService(IHttpContextAccessor httpContextAccessor, PEContext peContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
    }

    public VM_EquipmentGroup GetEquipmentGroup(long id)
    {
      VM_EquipmentGroup result = null;

      var equipmentGroup = _peContext.MNTEquipmentGroups
        .SingleOrDefault(x => x.EquipmentGroupId == id);
      result = equipmentGroup == null ? null : new VM_EquipmentGroup(equipmentGroup);

      return result;
    }

    public VM_EquipmentGroup GetEmptyEquipmentGroup()
    {
      VM_EquipmentGroup result = new VM_EquipmentGroup();
      return result;
    }

    public DataSourceResult GetEquipmentGroupsList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _peContext.MNTEquipmentGroups
        .ToDataSourceLocalResult<MNTEquipmentGroup, VM_EquipmentGroup>(request, modelState, x => new VM_EquipmentGroup(x));

      return result;
    }

    public async Task<VM_Base> CreateEquipmentGroup(ModelStateDictionary modelState, VM_EquipmentGroup vm)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref vm);

      DCEquipmentGroup dc = new DCEquipmentGroup();
      dc.EquipmentGroupCode = vm.EquipmentGroupCode;
      dc.EquipmentGroupName = vm.EquipmentGroupName;
      dc.Description = vm.EquipmentGroupDescription;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendEquipmentGroupCreateRequest(dc);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> UpdateEquipmentGroup(ModelStateDictionary modelState, VM_EquipmentGroup data)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref data);

      DCEquipmentGroup dc = new DCEquipmentGroup
      {
        EquipmentGroupId = data.EquipmentGroupId,
        EquipmentGroupCode = data.EquipmentGroupCode,
        EquipmentGroupName = data.EquipmentGroupName,
        Description = data.EquipmentGroupDescription
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendEquipmentGroupUpdateRequest(dc);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> DeleteEquipmentGroup(long id)
    {
      VM_Base result = new VM_Base();

      var dc = new DCEquipmentGroup
      {
        EquipmentGroupId = id
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendEquipmentGroupDeleteRequest(dc);

      //HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }
  }
}
