using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PE.DbEntity.PEContext;
using PE.DbEntity.SPModels;

namespace PE.DbEntity.EFCoreExtensions
{
  public static class ContextExtensions
  {
    public static List<SPSetupInstruction> ExecuteSetupInstruction(this HmiContext ctx, SqlParameter[] parameters)
    {
      if (parameters.Length != 3)
        return null;

      return ctx.SPSetupInstructions.FromSqlRaw("[dbo].[SPSetupInstructions] @SetupId, @AssetId, @IsSentToL1", parameters).ToList();
    }

    public static string ExecuteParameterLookup(this HmiContext ctx, SqlParameter[] parameters)
    {
      if (parameters.Length != 3)
        return null;
      ctx.Database.ExecuteSqlRaw("[dbo].[SPSTPParameterLookup] @ParameterCode, @ParameterValueId, @ResultValue out", parameters);

      return parameters[2].Value.ToString();
    }

    public static List<SPL3L1MaterialAssignment> ExecuteL3L1MaterialAssignment(this HmiContext ctx, SqlParameter[] parameters)
    {
      if (parameters.Length != 1)
        return null;

      return ctx.SPL3L1MaterialAssignments.FromSqlRaw("[dbo].[SPL3L1MaterialAssignment] @WorkOrderId", parameters).ToList();
    }

    public static Dictionary<long, string> ExecuteFilteringData(this HmiContext ctx, string query)
    {
      return ctx.SPDictionaries.FromSqlRaw(query).ToDictionary(x => x.Key, x => x.Value != null ? x.Value.ToString() : "");
    }

    public static List<SPL3L1MaterialInArea> ExecuteSPL3L1MaterialInArea(this HmiContext ctx, SqlParameter[] parameters)
    {
      if (parameters.Length != 1)
      {
        return null;
      }

      return ctx.SPL3L1MaterialInAreas.FromSqlRaw("[dbo].[SPRawMaterialsInArea] @RawMaterialId_list", parameters).ToList();
    }

    public static List<SPWorkOrderSummary> ExecuteSPWorkOrderSummary(this HmiContext ctx, SqlParameter[] parameters)
    {
      if (parameters.Length != 1)
      {
        return null;
      }

      return ctx.SPWorkOrderSummary.FromSqlRaw("[dbo].[SPRawMaterialsInArea] @WorkOrderId", parameters).ToList();
    }

    public static List<SPL3L1MaterialsInFurnance> ExecuteSPL3L1MaterialsInFurnance(this HmiContext ctx, SqlParameter[] parameters)
    {
      if (parameters.Length != 1)
      {
        return null;
      }

      return ctx.SPL3L1MaterialsInFurnances.FromSqlRaw("[dbo].[SPRawMaterialsInFurnace] @RawMaterialId_list", parameters).ToList();
    }

    public static List<SPRawMaterialGenealogy> ExecuteSPRawMaterialGenealogy(this HmiContext ctx, SqlParameter[] parameters)
    {
      if (parameters.Length != 1)
        return null;

      return ctx.SPRawMaterialGenealogies.FromSqlRaw("[dbo].[SPRawMaterialGenealogy] @RawMaterialId", parameters).ToList();
    }

    public static List<SPUserMenu> ExecuteSPUserMenu(this HmiContext ctx, SqlParameter[] parameters)
    {
      if (parameters.Length != 2)
      {
        return null;
      }

      return ctx.SPUserMenus.FromSqlRaw("[dbo].[SPUserMenu] @userName, @isAdmin", parameters).ToList();
    }

    public static List<SPGetAlarmsByLanguage> ExecuteSPGetAlarmsByLanguage(this HmiContext ctx, SqlParameter[] parameters)
    {
      if (parameters.Length != 1)
      {
        return null;
      }

      return ctx.SPGetAlarmsByLanguage.FromSqlRaw("[dbo].[SPGetAlarmsByLanguage] @LanguageId", parameters).ToList();
    }

    public static List<SPActiveBypassResult> ExecuteSPGetActiveBypasses(this HmiContext ctx)
    {
      return ctx.SPBypassWidget.FromSqlRaw("[dbo].[SPActualBypassWidget]").ToList();
    }
    
    public static void ExecuteSPUpdateShiftCalendar(this HmiContext ctx, List<SPDayShiftLayoutId> list)
    {
      string sql = "EXEC [dbo].SPUpdateShiftCalendar @Days";

      var dt = new DataTable();
      dt.Columns.Add("DateDay");
      dt.Columns.Add("ShiftLayoutId");

      foreach (var item in list)
      {
        dt.Rows.Add(item.DateDay, item.ShiftLayoutId);
      }

      List<SqlParameter> parms = new List<SqlParameter>
      {
          // Create parameter(s)    
          new SqlParameter("@Days", SqlDbType.Structured) { Value = dt, TypeName = "Date_ShiftLayoutId_List" }
      };

      _ = ctx.Database.ExecuteSqlRaw(sql, parms.ToArray());
    }
  }
}
