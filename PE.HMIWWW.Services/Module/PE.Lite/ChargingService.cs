using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.DbEntity.PEContext;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using System.Collections.Generic;
using System.Data;


namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class ChargingService : MaterialInAreaBaseService, IChargingService
  {
    private readonly HmiContext _hmiContext;

    public ChargingService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
    }

    public List<VM_RawMaterialOverview> GetQueueAreas(ModelStateDictionary modelState, List<long> materialsInAreas)
    {
      List<VM_RawMaterialOverview> result = new List<VM_RawMaterialOverview>();

      if (!modelState.IsValid)
      {
        return result;
      }
      var areaList = GetQueueAreas(materialsInAreas, _hmiContext).ToList();
      short seq = 1;
      areaList.ForEach(x => result.Add(new VM_RawMaterialOverview(x, seq++)));

      return result;
    }
  }
}
