using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.QEX;
using SMF.Core.Communication;
using SMF.Core.DC;

namespace PE.BaseInterfaces.SendOffices.QEX
{
  public interface IRuleEngineBaseSendOffice
  {
    Task<SendOfficeResult<DataContractBase>> UpdateSignalDefinitions(DCSignalDefinitions signalDefinitions);

    /// <summary>
    /// Returns a list of all available trigger events.
    /// </summary>
    Task<SendOfficeResult<DCRulesTriggerEvents>> GetRulesTriggerEvents(DataContractBase dc);

    /// <summary>
    /// Returns the currently rules revision number
    /// </summary>
    Task<SendOfficeResult<DCRevision>> GetRevision(DataContractBase dc);

    /// <summary>
    /// Returns the rule mapping for a trigger event
    /// </summary>
    Task<SendOfficeResult<DCRuleMapping>> GetRuleMapping(DCTrigger dc);

    /// <summary>
    /// Returns the result of the execution of rules for the given trigger event with the given input values
    /// </summary>
    Task<SendOfficeResult<DCRuleValues>> TriggerEvaluation(DCRuleEvaluation dc);
  }
}
