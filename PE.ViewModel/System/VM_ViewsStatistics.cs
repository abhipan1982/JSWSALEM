using System;
using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.Models;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.System
{
  public class VM_ViewsStatistics : VM_Base
  {
    #region properties

    public virtual Int64 Id { get; set; }

    [SmfDisplay(typeof(VM_ViewsStatistics), "Name", "NAME_Name")]
    public virtual string Name { get; set; }

    [SmfDisplay(typeof(VM_ViewsStatistics), "Created", "NAME_Created")]
    public virtual DateTime? Created { get; set; }

    [SmfDisplay(typeof(VM_ViewsStatistics), "Records", "NAME_Records")]
    public virtual int? Records { get; set; }

    [SmfDisplay(typeof(VM_ViewsStatistics), "Time", "NAME_Time")]
    public virtual int? Time { get; set; }

    [SmfDisplay(typeof(VM_ViewsStatistics), "TimePerRecord", "NAME_TimePerRecord")]
    public virtual double? TimePerRecord { get; set; }

    [SmfDisplay(typeof(VM_ViewsStatistics), "UsedInViews", "NAME_UsedInViews")]
    public virtual int? UsedInViews { get; set; }

    [SmfDisplay(typeof(VM_ViewsStatistics), "ViewsOwned", "NAME_ViewsOwned")]
    public virtual int? ViewsOwned { get; set; }

    #endregion

    #region ctor

    public VM_ViewsStatistics()
    {
    }

    public VM_ViewsStatistics(ViewsStatistic entity)
    {
      Id = entity.Id;
      Name = entity.Name;
      Created = entity.Created;
      Records = entity.Records;
      Time = entity.Time;
      TimePerRecord = entity.TimePerRecord;
      UsedInViews = entity.UsedInViews;
      ViewsOwned = entity.ViewsOwned;
    }

    #endregion
  }
}
