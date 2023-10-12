using System;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.System
{
  public class VM_Test : VM_Base
  {
    public VM_Test()
    {
    }

    public VM_Test(String text)
    {
      Text = text;
    }

    [SmfDisplay(typeof(VM_Test), "Text", "NAME_Text")]
    public virtual String Text { get; set; }
  }
}
