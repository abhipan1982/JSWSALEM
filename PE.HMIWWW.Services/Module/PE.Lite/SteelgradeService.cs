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
using PE.HMIWWW.ViewModel.Module.Lite.Steelgrade;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class SteelgradeService : BaseService, ISteelgradeService
  {
    private readonly PEContext _peContext;

    public SteelgradeService(IHttpContextAccessor httpContextAccessor, PEContext peContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
    }

    public VM_Steelgrade GetSteelgrade(ModelStateDictionary modelState, long id)
    {
      VM_Steelgrade result = null;

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

      PRMSteelgrade sg = _peContext.PRMSteelgrades
        .Include(i => i.PRMSteelgradeChemicalComposition)
        .Include(i => i.FKSteelGroup)
        .Include(i => i.FKScrapGroup)
        .SingleOrDefault(x => x.SteelgradeId == id);
      result = sg != null ? new VM_Steelgrade(sg) : null;

      return result;
    }

    public IList<VM_Steelgrade> GetSteelgradeParents()
    {
      List<VM_Steelgrade> result = new List<VM_Steelgrade>();
      IQueryable<PRMSteelgrade> dbList = _peContext.PRMSteelgrades.AsQueryable();
      foreach (PRMSteelgrade item in dbList)
      {
        result.Add(new VM_Steelgrade(item));
      }

      return result;
    }

    public DataSourceResult GetSteelgradeList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _peContext.PRMSteelgrades
        .Include(i => i.PRMSteelgradeChemicalComposition)
        .Include(i => i.FKParentSteelgrade)
        .Include(i => i.FKSteelGroup)
        .Include(i => i.FKScrapGroup)
        .ToDataSourceLocalResult(request, modelState, x => new VM_Steelgrade(x));

      return result;
    }

    public async Task<VM_Base> CreateSteelgrade(ModelStateDictionary modelState, VM_Steelgrade steelgrade)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref steelgrade);

      DCSteelgrade dcSteelgrade = new DCSteelgrade
      {
        Id = steelgrade.Id,
        SteelgradeCode = steelgrade.SteelgradeCode,
        SteelgradeName = steelgrade.SteelgradeName,
        Description = steelgrade.Description,
        Density = steelgrade.Density,
        IsDefault = steelgrade.IsDefault,
        CustomCode = steelgrade.CustomCode,
        CustomDescription = steelgrade.CustomDescription,
        CustomName = steelgrade.CustomName,
        GroupCode = steelgrade.SteelFamilyCode,
        GroupDescription = steelgrade.GroupDescription,
        FkSteelGroup = steelgrade.SteelFamilyId,
        FkScrapGroup = steelgrade.FkScrapGroupId,
        ParentSteelgradeId = steelgrade.ParentSteelgradeId,
        FeMax = steelgrade.FeMax,
        FeMin = steelgrade.FeMin,
        CMax = steelgrade.CMax,
        CMin = steelgrade.CMin,
        MnMax = steelgrade.MnMax,
        MnMin = steelgrade.MnMin,
        CrMax = steelgrade.CrMax,
        CrMin = steelgrade.CrMin,
        MoMax = steelgrade.MoMax,
        MoMin = steelgrade.MoMin,
        VMax = steelgrade.VMax,
        VMin = steelgrade.VMin,
        NiMax = steelgrade.NiMax,
        NiMin = steelgrade.NiMin,
        CoMax = steelgrade.CoMax,
        CoMin = steelgrade.CoMin,
        SiMax = steelgrade.SiMax,
        SiMin = steelgrade.SiMin,
        PMax = steelgrade.PMax,
        PMin = steelgrade.PMin,
        SMax = steelgrade.SMax,
        SMin = steelgrade.SMin,
        CuMax = steelgrade.CuMax,
        CuMin = steelgrade.CuMin,
        NbMax = steelgrade.NbMax,
        NbMin = steelgrade.NbMin,
        AlMax = steelgrade.AlMax,
        AlMin = steelgrade.AlMin,
        NMax = steelgrade.NMax,
        NMin = steelgrade.NMin,
        CaMax = steelgrade.CaMax,
        CaMin = steelgrade.CaMin,
        BMax = steelgrade.BMax,
        BMin = steelgrade.BMin,
        TiMax = steelgrade.TiMax,
        TiMin = steelgrade.TiMin,
        SnMax = steelgrade.SnMax,
        SnMin = steelgrade.SnMin,
        OMax = steelgrade.OMax,
        OMin = steelgrade.OMin,
        HMax = steelgrade.HMax,
        HMin = steelgrade.HMin,
        WMax = steelgrade.WMax,
        WMin = steelgrade.WMin,
        PbMax = steelgrade.PbMax,
        PbMin = steelgrade.PbMin,
        ZnMax = steelgrade.ZnMax,
        ZnMin = steelgrade.ZnMin,
        AsMax = steelgrade.AsMax,
        AsMin = steelgrade.AsMin,
        MgMax = steelgrade.MgMax,
        MgMin = steelgrade.MgMin,
        SbMax = steelgrade.SbMax,
        SbMin = steelgrade.SbMin,
        BiMax = steelgrade.BiMax,
        BiMin = steelgrade.BiMin,
        TaMax = steelgrade.TaMax,
        TaMin = steelgrade.TaMin,
        ZrMax = steelgrade.ZrMax,
        ZrMin = steelgrade.ZrMin,
        CeMax = steelgrade.CeMax,
        CeMin = steelgrade.CeMin,
        TeMax = steelgrade.TeMax,
        TeMin = steelgrade.TeMin
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendCreateSteelgradeAsync(dcSteelgrade);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> UpdateSteelgrade(ModelStateDictionary modelState, VM_Steelgrade steelgrade)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref steelgrade);

      DCSteelgrade dcSteelgrade = new DCSteelgrade
      {
        Id = steelgrade.Id,
        SteelgradeCode = steelgrade.SteelgradeCode,
        SteelgradeName = steelgrade.SteelgradeName,
        Description = steelgrade.Description,
        Density = steelgrade.Density,
        IsDefault = steelgrade.IsDefault,
        CustomCode = steelgrade.CustomCode,
        CustomDescription = steelgrade.CustomDescription,
        CustomName = steelgrade.CustomName,
        GroupCode = steelgrade.SteelFamilyCode,
        GroupDescription = steelgrade.GroupDescription,
        FkSteelGroup = steelgrade.SteelFamilyId,
        FkScrapGroup = steelgrade.FkScrapGroupId,
        ParentSteelgradeId = steelgrade.ParentSteelgradeId,
        FeMax = steelgrade.FeMax,
        FeMin = steelgrade.FeMin,
        CMax = steelgrade.CMax,
        CMin = steelgrade.CMin,
        MnMax = steelgrade.MnMax,
        MnMin = steelgrade.MnMin,
        CrMax = steelgrade.CrMax,
        CrMin = steelgrade.CrMin,
        MoMax = steelgrade.MoMax,
        MoMin = steelgrade.MoMin,
        VMax = steelgrade.VMax,
        VMin = steelgrade.VMin,
        NiMax = steelgrade.NiMax,
        NiMin = steelgrade.NiMin,
        CoMax = steelgrade.CoMax,
        CoMin = steelgrade.CoMin,
        SiMax = steelgrade.SiMax,
        SiMin = steelgrade.SiMin,
        PMax = steelgrade.PMax,
        PMin = steelgrade.PMin,
        SMax = steelgrade.SMax,
        SMin = steelgrade.SMin,
        CuMax = steelgrade.CuMax,
        CuMin = steelgrade.CuMin,
        NbMax = steelgrade.NbMax,
        NbMin = steelgrade.NbMin,
        AlMax = steelgrade.AlMax,
        AlMin = steelgrade.AlMin,
        NMax = steelgrade.NMax,
        NMin = steelgrade.NMin,
        CaMax = steelgrade.CaMax,
        CaMin = steelgrade.CaMin,
        BMax = steelgrade.BMax,
        BMin = steelgrade.BMin,
        TiMax = steelgrade.TiMax,
        TiMin = steelgrade.TiMin,
        SnMax = steelgrade.SnMax,
        SnMin = steelgrade.SnMin,
        OMax = steelgrade.OMax,
        OMin = steelgrade.OMin,
        HMax = steelgrade.HMax,
        HMin = steelgrade.HMin,
        WMax = steelgrade.WMax,
        WMin = steelgrade.WMin,
        PbMax = steelgrade.PbMax,
        PbMin = steelgrade.PbMin,
        ZnMax = steelgrade.ZnMax,
        ZnMin = steelgrade.ZnMin,
        AsMax = steelgrade.AsMax,
        AsMin = steelgrade.AsMin,
        MgMax = steelgrade.MgMax,
        MgMin = steelgrade.MgMin,
        SbMax = steelgrade.SbMax,
        SbMin = steelgrade.SbMin,
        BiMax = steelgrade.BiMax,
        BiMin = steelgrade.BiMin,
        TaMax = steelgrade.TaMax,
        TaMin = steelgrade.TaMin,
        ZrMax = steelgrade.ZrMax,
        ZrMin = steelgrade.ZrMin,
        CeMax = steelgrade.CeMax,
        CeMin = steelgrade.CeMin,
        TeMax = steelgrade.TeMax,
        TeMin = steelgrade.TeMin
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendSteelgradeAsync(dcSteelgrade);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> DeleteSteelgrade(ModelStateDictionary modelState, VM_Steelgrade steelgrade)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref steelgrade);

      DCSteelgrade dcSteelgrade = new DCSteelgrade {Id = steelgrade.Id};

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendDeleteSteelgradeAsync(dcSteelgrade);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public VM_Steelgrade GetSteelgradeDetails(ModelStateDictionary modelState, long id)
    {
      VM_Steelgrade result = null;

      if (id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return result;
      }

      PRMSteelgrade data = _peContext.PRMSteelgrades
        .Include(i => i.PRMSteelgradeChemicalComposition)
        .Include(i => i.FKSteelGroup)
        .Include(i => i.FKScrapGroup)
        .Include(i => i.FKParentSteelgrade)
        .Where(x => x.SteelgradeId == id)
        .SingleOrDefault();

      result = new VM_Steelgrade(data);

      return result;
    }

    public IList<VM_Steelgrade> GetSteelgradesByHeat(long? heatId)
    {
      long? sgGroup = null;
      if (heatId.HasValue)
      {
        sgGroup = _peContext.PRMHeats.Where(x => x.HeatId == heatId).Select(x => x.FKSteelgradeId).FirstOrDefault();
      }

      List<VM_Steelgrade> result = _peContext.PRMSteelgrades
        .Where(x => sgGroup == null || x.SteelgradeId == sgGroup || x.FKParentSteelgradeId == sgGroup).ToList()
        .Select(x => new VM_Steelgrade(x)).ToList();
      return result;
    }

    public async Task<bool> ValidateSteelgradeCode(string code)
    {
      bool exists = false;
      if (!string.IsNullOrEmpty(code))
      {
        exists = await _peContext.PRMSteelgrades.AnyAsync(p => p.SteelgradeCode == code);
      }

      return exists;
    }

    public async Task<bool> ValidateSteelgradeName(string name)
    {
      bool exists = false;
      if (!string.IsNullOrEmpty(name))
      {
        exists = await _peContext.PRMSteelgrades.AnyAsync(p => p.SteelgradeName == name);
      }

      return exists;
    }

    public IList<VM_SteelFamily> GetSteelFamilies()
    {
      IList<VM_SteelFamily> result = new List<VM_SteelFamily>();
      result = _peContext.PRMSteelGroups.AsEnumerable().Select(sg => new VM_SteelFamily(sg)).ToList();

      return result;
    }
  }
}
