using PE.BaseDbEntity.Models;
using PE.BaseModels.DataContracts.Internal.DBA;
using PE.Core.UniversalValidator;
using PE.Models.DataContracts.Internal.DBA;

namespace PE.PRM.ProdManager.Handler.Models
{
  public class BatchDataValidationResult : ValidationResultBase
  {
    public DCBatchDataStatus DcBatchDataStatus { get; set; } 
    public PRMSteelgrade Steelgrade { get; set; } 
    public PRMProductCatalogue ProductCatalogue { get; set; } 
    public PRMMaterialCatalogue MaterialCatalogue { get; set; } 
    public PRMHeat Heat { get; set; } 
    public PRMCustomer Customer { get; set; } 
    
    public PRMWorkOrder ParentWorkOrder { get; set; }
  }
}
