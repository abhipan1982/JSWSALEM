using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SMF.DbEntity.ExceptionHelpers;
using SMF.DbEntity.Models;

namespace PE.HMIWWW.Core.Parameter
{
  public static class ParameterController
  {
    #region ctor

    static ParameterController()
    {
      ReadParamaters();
    }

    #endregion

    public static void ReadParamaters()
    {
      try
      {
        using (SMFContext ctxSMF = new SMFContext())
        {
          // using (SMFUnitOfWork uow = new SMFUnitOfWork())
          //{

          //IRepository<SMF.DbEntity.Model.Parameter> ipr = uow.Repository<SMF.DbEntity.Model.Parameter>();
          if (String.IsNullOrEmpty(_groupName))
          {
            Parameters = ctxSMF.Parameters.Include(i => i.ParameterGroup).Include(i => i.Unit).AsNoTracking().ToList();
          }
          //Parameters = ipr.Query().Include(a => a.ParameterGroup).Include(b => b.UnitOfMeasure).Get().ToList();
          else
          {
            Parameters = ctxSMF.Parameters.Where(x => x.ParameterGroup.Name == _groupName)
              .Include(i => i.ParameterGroup).Include(i => i.Unit).AsNoTracking().ToList();
          }
          //Parameters = ipr.Query(x => x.ParameterGroup.Name == _groupName).Include(a => a.ParameterGroup).Include(b => b.UnitOfMeasure).Get().ToList();
        }
      }

      catch (Exception ex)
      {
        DbExceptionHelper.ProcessException(ex, "Error during reading parameters from the DB.");
      }
    }

    public static SMF.DbEntity.Models.Parameter GetParameter(string parameterName)
    {
      if (String.IsNullOrEmpty(parameterName))
      {
        return null;
      }

      return Parameters.FirstOrDefault(p => p.Name == parameterName);
    }

    #region members

    private static List<SMF.DbEntity.Models.Parameter> Parameters { get; set; }
    private static readonly string _groupName = "HMI_WWW";

    #endregion
  }
}
