using System;
using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Setup
{
  public class VM_SetupGroup : VM_Base
  {
    #region ctor

    public VM_SetupGroup() { }

    #endregion

    #region props

    public long? SetupId { get; set; }

    [SmfDisplay(typeof(VM_Setup), "SetupName", "NAME_SetupName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SetupName { get; set; }

    [SmfDisplay(typeof(VM_Setup), "CalculationTs", "NAME_SetupDate")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? CalculationTs { get; set; }

    [SmfDisplay(typeof(VM_Setup), "SentTs", "NAME_SetupSentDate")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? SentTs { get; set; }

    [SmfDisplay(typeof(VM_Setup), "CreatedTs", "NAME_Created")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_Setup), "UpdatedTs", "NAME_UpdatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? UpdatedTs { get; set; }

    [SmfDisplay(typeof(VM_Setup), "SetupParameter", "NAME_SetupParameter")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SetupParameter { get; set; }

    [SmfDisplay(typeof(VM_Setup), "Steelgrade", "NAME_Steelgrade")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string Steelgrade { get; set; }
    [SmfDisplay(typeof(VM_Setup), "Product_Size", "NAME_ProductSize")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string Product_Size { get; set; }
    [SmfDisplay(typeof(VM_Setup), "Work_Order", "NAME_WorkOrderName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string Work_Order { get; set; }
    [SmfDisplay(typeof(VM_Setup), "Heat_Number", "NAME_HeatNumber")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string Heat_Number { get; set; }
    [SmfDisplay(typeof(VM_Setup), "Layout", "NAME_Layout")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string Layout { get; set; }
    [SmfDisplay(typeof(VM_Setup), "Issue", "NAME_Issue")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string Issue { get; set; }
    [SmfDisplay(typeof(VM_Setup), "RelationToCurrent", "NAME_RelationToCurrent")]
    public short RelationToCurrent { get; set; }
    [SmfDisplay(typeof(VM_Setup), "RelationToNext", "NAME_RelationToNext")]
    public short RelationToNext { get; set; }
    public string RelationType { get; set; }

    #endregion
  }
}
