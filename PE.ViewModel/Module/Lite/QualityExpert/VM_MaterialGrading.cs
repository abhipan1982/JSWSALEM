using PE.BaseDbEntity.EnumClasses;
using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.ViewModel.Module.Lite.QualityExpert
{
  public class VM_MaterialGrading : VM_Base
  {
    public VM_MaterialGrading() { }

    public short Grading { get; set; }
    public GradingSource GradingSource { get; set; }
  }
}
