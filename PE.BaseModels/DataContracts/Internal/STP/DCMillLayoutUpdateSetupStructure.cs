using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.STP
{
  public class DCMillLayoutUpdateSetupStructure : DataContractBase
  {
    [DataMember] public double ProductSize { get; set; }

    [DataMember] public double LayoutNumber { get; set; }

    [DataMember] public string Issue { get; set; }

    [DataMember] public double MLD1Speed { get; set; }

    [DataMember] public double MLD2Speed { get; set; }

    [DataMember] public double MLD3Speed { get; set; }

    [DataMember] public double MLD4Speed { get; set; }

    [DataMember] public double MLD5Speed { get; set; }

    [DataMember] public double MLI1Speed { get; set; }

    [DataMember] public double MLI2Speed { get; set; }

    [DataMember] public double MLI3Speed { get; set; }

    [DataMember] public double MLI4Speed { get; set; }

    [DataMember] public double MLI5Speed { get; set; }

    [DataMember] public double MLI6Speed { get; set; }

    [DataMember] public double MLI7Speed { get; set; }

    [DataMember] public double MLI8Speed { get; set; }

    [DataMember] public double MLC12Speed { get; set; }

    [DataMember] public double MLC13Speed { get; set; }

    [DataMember] public double MLC16Speed { get; set; }

    [DataMember] public double MLC17Speed { get; set; }

    [DataMember] public double MLC18Speed { get; set; }

    [DataMember] public double MLC19Speed { get; set; }

    [DataMember] public double MLC20Speed { get; set; }

    [DataMember] public double MLC21Speed { get; set; }

    [DataMember] public double MLC22Speed { get; set; }

    [DataMember] public double MLC23Speed { get; set; }

    [DataMember] public double MLC24Speed { get; set; }

    [DataMember] public double MLC25Speed { get; set; }
  }
}
