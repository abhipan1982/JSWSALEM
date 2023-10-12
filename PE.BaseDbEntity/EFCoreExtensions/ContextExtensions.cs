using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.Models;

namespace PE.BaseDbEntity.EFCoreExtensions
{
  public static class ContextExtensions
  {
    public static async Task<(int SaveChangesResult, string ErrorValidationMessages)> SaveChangesWithValidationAsync(this DbContext dbContext)
    {
      StringBuilder sb = new StringBuilder();
      var entities = dbContext.ChangeTracker.Entries()
        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
        .Select(e => e.Entity)
        .ToList();

      foreach (var entity in entities)
      {
        try
        {
          var validationContext = new ValidationContext(entity);
          Validator.ValidateObject(entity, validationContext, validateAllProperties: true);
        }
        catch (ValidationException exc)
        {
          sb.Append($"For property {entity.GetType().Name} validation failed with message {exc?.Message}");
        }
        catch (DbUpdateException exc)
        {
          sb.Append($"For property {entity.GetType().Name} DbUpdate failed with message {exc?.InnerException?.Message}");
        }
        catch (Exception exc)
        {
          sb.Append($"For property {entity.GetType().Name} unpredictable exception happened {exc?.Message}");
        }
      }

      var saveChangesResult = await dbContext.SaveChangesAsync();

      return (saveChangesResult, sb.ToString());
    }

    public static SPCalculatedKPI ExecuteSPGetKPIValue(this PEContext.PEContext ctx, SqlParameter[] parameters)
    {
      if (parameters.Length != 9) return null;

      ctx.Database.ExecuteSqlRaw(
        "[dbo].[SPGetKPIValue] @KPICode, @WorkOrderKey, @MaterialKey, @ShiftKey, @TimeFrom, @TimeTo, @KPIDefinitionKey out, @KPITime out, @KPIValue out",
        parameters);

      var definitionId = ConvertFromDBVal<long?>(parameters[6].Value);
      var time = ConvertFromDBVal<DateTime?>(parameters[7].Value);
      var value = ConvertFromDBVal<double?>(parameters[8].Value);

      return new SPCalculatedKPI
      {
        KPIDefinitionId = definitionId ?? default,
        KPITime = time ?? default,
        KPIValue = value ?? default
      };
    }

    public static List<SPWorkOrdersOnProductYard> ExecuteSPWorkOrdersOnProductYards(this PEContext.PEContext ctx, SqlParameter[] parameters)
    {
      if (parameters.Length != 1)
        return null;

      return ctx.SPWorkOrdersOnProductYards.FromSqlRaw("[dbo].[SPWorkOrdersOnProductYards] @WorkOrderId", parameters).ToList();
    }

    public static async Task<SPAliasValue> ExecuteSPGetAliasValue(this PEContext.PEContext ctx, SqlParameter[] parameters)
    {
      if (parameters.Length != 14)
        return null;
      await ctx.Database.ExecuteSqlRawAsync(
        "[dbo].[SPGetAliasValue] @AliasName, @Param1, @Param2, @Param3, @ResultSet out, @ResultValue out, @ResultValueNumber out, @ResultValueBoolean out, @ResultValueTimestamp out, @ResultValueText out, @UnitId out, @ErrorText out, @ErrorCode out, @NORecords out",
        parameters);

      string resultSet = parameters[4].Value.ToString();
      string resultValue = parameters[5].Value.ToString();
      var resultValueNumber = ConvertFromDBVal<double?>(parameters[6].Value);
      var resultValueBoolean = ConvertFromDBVal<bool?>(parameters[7].Value);
      var resultValueTimestamp = ConvertFromDBVal<DateTime?>(parameters[8].Value);
      string resultValueText = parameters[9].Value.ToString();
      var unitId = ConvertFromDBVal<long?>(parameters[10].Value);
      string errorText = parameters[11].Value.ToString();
      var errorCode = ConvertFromDBVal<int?>(parameters[12].Value);
      var recordsCount = ConvertFromDBVal<int?>(parameters[13].Value);

      return new SPAliasValue
      {
        ResultSet = resultSet,
        ResultValue = resultValue,
        ResultValueNumber = resultValueNumber ?? default,
        ResultValueBoolean = resultValueBoolean ?? default,
        ResultValueTimestamp = resultValueTimestamp ?? default,
        ResultValueText = resultValueText,

        ErrorText = errorText,
        ErrorCode = errorCode ?? default,
        RecordsCount = recordsCount ?? default,
      };
    }

    public static async Task<List<SPQEAliasTable>> ExecuteSPGetAliasTable(this PEContext.PEContext ctx, SqlParameter[] parameters)
    {
      if (parameters.Length != 5)
        return null;

      return await ctx.SPGetAliasValuesToTable.FromSqlRaw("[dbo].[SPGetAliasValuesToTable] @AliasName, @Param1, @Param2, @Param3, @GetSample", parameters).ToListAsync();
    }

    public static async Task<List<SPLastGradingOnAssets>> ExecuteSPLastGradingOnAssets(this PEContext.PEContext ctx)
    {
      return await ctx.SPLastGradingOnAssets.FromSqlRaw("[dbo].[SPLastGradingOnAssets]").ToListAsync();
    }

    public async static Task ExecuteSPRenumberAssets(this PEContext.PEContext ctx, SqlParameter[] parameters)
    {
      if (parameters.Length != 1)
      {
        return;
      }

      await ctx.Database.ExecuteSqlRawAsync("[dbo].[SPRenumberAssets] @AssetIds", parameters);
    }

    public static T ConvertFromDBVal<T>(object obj, object defaultValue = null)
    {
      if (obj == null || obj == DBNull.Value)
      {
        if (defaultValue == null)
          return default(T);

        return (T)defaultValue;
      }
      else
      {
        return (T)obj;
      }
    }
  }
}
