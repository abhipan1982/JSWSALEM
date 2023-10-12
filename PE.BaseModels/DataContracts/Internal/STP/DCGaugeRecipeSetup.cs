using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.STP
{
  public class DCGaugeRecipeSetup : DataContractBase
  {
    /// <summary>
    ///   Target recipe for gauge's asset.
    /// </summary>
    [DataMember]
    public int RecipeNumber { get; set; }
  }
}
