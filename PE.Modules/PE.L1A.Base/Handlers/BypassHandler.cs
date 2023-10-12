using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using L1A.Base.Models;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;

namespace PE.L1A.Base.Handlers
{
  public class BypassHandler
  {
    public virtual void InsertBypassInstances(List<BypassInstanceDto> bypassInstances)
    {
      using var ctx = new PEContext();

      ctx.L1ABypassInstances.AddRange(bypassInstances
        .Select(x => new L1ABypassInstance()
        {        
          OpcBypassNode = x.OpcBypassNode,
          FKBypassConfigurationId = x.FKBypassConfigurationId
        })
        .ToList());

      ctx.SaveChanges();
    }

    public virtual void TruncateBypassInstances()
    {
      using var ctx = new PEContext();

      var byPasses = ctx.L1ABypassInstances.ToList();

      ctx.L1ABypassInstances.RemoveRange(byPasses);

      ctx.SaveChanges();

      ctx.Database.ExecuteSqlRaw("DBCC CHECKIDENT (L1ABypassInstances, RESEED, 0)");
    }

    public virtual List<BypassConfigurationDto> GetBypassConfigurations()
    {
      using var ctx = new PEContext();

      return ctx.L1ABypassConfigurations
        .Include(x => x.FKBypassType)
        .Select(x => new BypassConfigurationDto()
        {
          BypassConfigurationId = x.BypassConfigurationId,
          OpcServerAddress = x.OpcServerAddress,
          OpcServerName = x.OpcServerName,
          OpcBypassParentStructureNode = x.OpcBypassParentStructureNode,
          OpcBypassName = x.OpcBypassName,
          BypassTypeCode = x.FKBypassType.BypassTypeCode,
          BypassTypeName = x.FKBypassType.BypassTypeName,
        })
        .ToList();
    }

    public virtual List<BypassInstanceDto> GetBypassInstances()
    {
      using var ctx = new PEContext();

      return ctx.L1ABypassInstances
        .Include(x => x.FKBypassConfiguration)
        .Include(x => x.FKBypassConfiguration.FKBypassType)
        .Select(x => new BypassInstanceDto()
        {
          FKBypassConfigurationId = x.FKBypassConfigurationId,
          OpcBypassNode = x.OpcBypassNode,
          OpcServerAddress = x.FKBypassConfiguration.OpcServerAddress,
          OpcServerName = x.FKBypassConfiguration.OpcServerName,
          BypassTypeCode = x.FKBypassConfiguration.FKBypassType.BypassTypeCode,
          BypassTypeName = x.FKBypassConfiguration.FKBypassType.BypassTypeName,
        })
        .ToList();
    }

    public virtual void InsertRaisedBypass(bool value, DateTime dateTime, BypassInstanceDto bypassInstance)
    {
      using var ctx = new PEContext();

      ctx.L1ARaisedBypasses.Add(new L1ARaisedBypass()
      {
        BypassName = bypassInstance.OpcBypassNode,
        Timestamp = dateTime,
        Value = value,
        OpcServerAddress = bypassInstance.OpcServerAddress,
        OpcServerName = bypassInstance.OpcServerName,
        BypassTypeName = bypassInstance.BypassTypeName
      });

      ctx.SaveChanges();
    }

    public virtual void InsertRaisedBypasses(List<L1ARaisedBypass> bypasses)
    {
      using var ctx = new PEContext();

      ctx.L1ARaisedBypasses.AddRange(bypasses);

      ctx.SaveChanges();
    }
  }
}
