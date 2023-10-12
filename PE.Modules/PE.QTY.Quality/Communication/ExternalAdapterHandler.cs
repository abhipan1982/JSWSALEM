using PE.BaseInterfaces.Managers.QTY;
using PE.QTY.Base.Module.Communication;

namespace PE.QTY.Quality.Communication
{
  public class ExternalAdapterHandler : ModuleBaseExternalAdapterHandler
  {
    public ExternalAdapterHandler(IDefectCatalogueBaseManager defectCatalogueManager,
      IQualityAssignmentBaseManager qualityAssignment,
      IDefectBaseManager defectManager,
      IDefectGroupsCatalogueBaseManager defectGroupsCatalogueManager,
      IDefectCatalogueCategoryBaseManager defectCatalogueCategoryManager)
      : base(defectCatalogueManager, qualityAssignment, defectManager, defectGroupsCatalogueManager,
        defectCatalogueCategoryManager)
    {
    }
  }
}
