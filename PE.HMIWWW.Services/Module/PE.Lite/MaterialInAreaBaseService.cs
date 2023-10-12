using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using PE.DbEntity.EFCoreExtensions;
using PE.DbEntity.PEContext;
using PE.DbEntity.SPModels;
using PE.HMIWWW.Core.Service;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class MaterialInAreaBaseService: BaseService
  {
    public MaterialInAreaBaseService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
    }

    protected DataTable GenerateDataTable(List<long> materials)
    {
      DataTable result = new DataTable();
      result.Columns.Add("RawMaterialId");

      foreach (long materialId in materials)
        result.Rows.Add(materialId);
      
      return result;
    }

    protected List<SPL3L1MaterialInArea> GetQueueAreas(List<long> materialsInAreas, HmiContext ctx)
    {
      DataTable materialsInAreasDataTable = GenerateDataTable(materialsInAreas);

      SqlParameter[] parametersArea = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@RawMaterialId_list",
                            TypeName = "dbo.TRK_RM_Id_List",
                            IsNullable = false,
                            Direction = ParameterDirection.Input,
                            Value = materialsInAreasDataTable
                        }};
      var areaList = ctx.ExecuteSPL3L1MaterialInArea(parametersArea);

      return areaList;
    }


  }
}
