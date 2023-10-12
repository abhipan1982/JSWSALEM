using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Shift
{
  public class VM_ShiftGenerate
  {
    public VM_ShiftGenerate()
    {

    }

    [SmfDisplay(typeof(VM_ShiftGenerate), "From", "NAME_From")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime From { get; set; }

    [SmfDisplay(typeof(VM_ShiftGenerate), "To", "NAME_To")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime To { get; set; }

    public IList<VM_ShiftDay> ShiftDays { get; set; }

  }

  public class VM_ShiftDay
  {
    public VM_ShiftDay()
    {

    }

    [DataType(DataType.Date)]
    [SmfDisplay(typeof(VM_ShiftDay), "Date", "NAME_Date")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false, ApplyFormatInEditMode = true)]
    public DateTime Date { get; set; }

    [SmfDisplay(typeof(VM_ShiftDay), "ShiftLayout", "NAME_ShiftLayout")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public int ShiftLayout { get; set; }

  }
}
