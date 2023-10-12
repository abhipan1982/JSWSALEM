﻿using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.WorkOrder
{
  public class VM_Material : VM_Base
  {
    public VM_Material(PRMMaterial data)
    {
      MaterialId = data.MaterialId;
      //TODOMN - refactor this
      //CreatedTs = data.CreatedTs;
      //LastUpdateTs = data.LastUpdateTs;
      IsDummy = data.IsDummy;
      IsAssigned = data.IsAssigned;
      MaterialName = data.MaterialName;
      MaterialWeight = data.MaterialWeight;
      WorkOrderName = data.FKWorkOrder?.WorkOrderName;
      FKHeatId = data.FKHeatId;
      FKWorkOrderId = data.FKWorkOrderId;

      UnitConverterHelper.ConvertToLocal(this);
    }

    [SmfDisplay(typeof(VM_Material), "MaterialId", "NAME_MaterialId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? MaterialId { get; set; }

    [SmfDisplay(typeof(VM_Material), "CreatedTs", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_Material), "LastUpdateTs", "NAME_LastUpdateTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? LastUpdateTs { get; set; }

    [SmfDisplay(typeof(VM_Material), "IsDummy", "NAME_IsDummyMaterial")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsDummy { get; set; }

    [SmfDisplay(typeof(VM_Material), "IsAssigned", "NAME_IsAssigned")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsAssigned { get; set; }

    [SmfDisplay(typeof(VM_Material), "MaterialName", "NAME_MaterialName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialName { get; set; }

    [SmfDisplay(typeof(VM_Material), "Weight", "NAME_Weight")]
    [SmfFormat("FORMAT_Weight")]
    [SmfUnit("UNIT_Weight")]
    public double MaterialWeight { get; set; }

    [SmfDisplay(typeof(VM_Material), "WorkOrderName", "NAME_WorkOrderName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_Material), "FKHeatId", "NAME_FKHeatId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long FKHeatId { get; set; }

    [SmfDisplay(typeof(VM_Material), "FKWorkOrderId", "NAME_FKWorkOrderId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FKWorkOrderId { get; set; }

    public short? Status { get; set; }
  }
}
