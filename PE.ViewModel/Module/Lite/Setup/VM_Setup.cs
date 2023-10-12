using System;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Setup
{
  public class VM_Setup : VM_Base
  {
    #region ctor

    public VM_Setup() { }

    public VM_Setup(V_SetupParameter data, DateTime? setupConfigurationLastSent = null)
    {
      SetupId = data.SetupId;
      SetupName = data.SetupName;
      SetupTypeCode = data.SetupTypeCode ?? "";
      SetupTypeId = data.SetupTypeId ?? 0;
      Steelgrade = data.Steelgrade;
      Product_Size = data.Product_Size;
      Layout = data.Layout;
      Work_Order = data.Work_Order;
      Heat_Number = data.Heat_Number;
      Issue = data.Issue;
      Previous_Steelgrade = data.Previous_Steelgrade;
      Previous_Product_Size = data.Previous_Product_Size;
      Previous_Layout = data.Previous_Layout;
      Parameter_Name = data.Parameter_Name;
      IsSteelgradeRelated = data.IsSteelgradeRelated;
      IsActive = data.IsActive;
      SetupConfigurationLastSent = setupConfigurationLastSent;
    }

    public VM_Setup(STPSetup data)
    {
      SetupId = data.SetupId;
      SetupName = data.SetupName;
      SetupCode = data.SetupCode;
      SetupTypeId = data.FKSetupTypeId;
    }

    #endregion

    #region props

    public long? SetupId { get; set; }
    [SmfDisplay(typeof(VM_Setup), "SetupName", "NAME_SetupName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SetupName { get; set; }
    [SmfDisplay(typeof(VM_SetupConfigurations), "SetupCode", "NAME_SetupCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SetupCode { get; set; }
    [SmfDisplay(typeof(VM_Setup), "CalculationTs", "NAME_SetupDate")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? CalculationTs { get; set; }
    [SmfDisplay(typeof(VM_Setup), "CreatedTs", "NAME_Created")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? CreatedTs { get; set; }
    [SmfDisplay(typeof(VM_Setup), "UpdatedTs", "NAME_UpdatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? UpdatedTs { get; set; }
    [SmfDisplay(typeof(VM_Setup), "ConfigurationLastSent", "NAME_LastSent")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? SetupConfigurationLastSent { get; set; }
    [SmfDisplay(typeof(VM_Setup), "SetupDescription", "NAME_SetupDescription")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SetupDescription { get; set; }
    [SmfDisplay(typeof(VM_Setup), "SetupCode", "NAME_SetupCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SetupTypeCode { get; set; }
    [SmfDisplay(typeof(VM_Setup), "SetupTypeName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SetupTypeName { get; set; }
    [SmfDisplay(typeof(VM_Setup), "SetupParameter", "NAME_SetupParameter")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SetupParameter { get; set; }
    [SmfDisplay(typeof(VM_Setup), "Steelgrade", "NAME_SteelgradeCode")]
    public string Steelgrade { get; set; }
    [SmfDisplay(typeof(VM_Setup), "Product_Size", "NAME_ProductSize")]
    public string Product_Size { get; set; }
    [SmfDisplay(typeof(VM_Setup), "Work_Order", "NAME_WorkOrderName")]
    public string Work_Order { get; set; }
    [SmfDisplay(typeof(VM_Setup), "Parameter_Name", "NAME_ParameterName")]
    public string Parameter_Name { get; set; }
    [SmfDisplay(typeof(VM_Setup), "Heat_Number", "NAME_HeatNumber")]
    public string Heat_Number { get; set; }
    [SmfDisplay(typeof(VM_Setup), "Previous_Steelgrade", "NAME_PreviousSteelgrade")]
    public string Previous_Steelgrade { get; set; }
    [SmfDisplay(typeof(VM_Setup), "Previous_Product_Size", "NAME_PreviousProductSize")]
    public string Previous_Product_Size { get; set; }
    [SmfDisplay(typeof(VM_Setup), "Previous_Layout", "NAME_PreviousLayout")]
    public string Previous_Layout { get; set; }
    [SmfDisplay(typeof(VM_Setup), "Layout", "NAME_Layout")]
    public string Layout { get; set; }
    [SmfDisplay(typeof(VM_Setup), "Issue", "NAME_Issue")]
    public string Issue { get; set; }
    public long WorkOrderId { get; set; }
    public long SetupTypeId { get; set; }
    [SmfDisplay(typeof(VM_Setup), "Required", "NAME_Required")]
    public bool Required { get; set; }
    [SmfDisplay(typeof(VM_Setup), "Status", "NAME_SetupStatus")]
    public short Status { get; set; }
    [SmfDisplay(typeof(VM_Setup), "IsActive", "NAME_IsActive")]
    public bool? IsActive { get; set; }
    [SmfDisplay(typeof(VM_Setup), "IsSteelgradeRelated", "NAME_IsSteelgradeRelated")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsSteelgradeRelated { get; set; }

    #endregion
  }
}
