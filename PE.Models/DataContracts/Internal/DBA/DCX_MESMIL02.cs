using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.Adapter
{
  public class DCX_MESMIL02 : DataContractBase
  {
    /// <summary>
    /// Primary Key in transfer table
    /// </summary>
    [DataMember]
    public long Counter { get; set; }

    /// <summary>
    /// Time stamp when record has been inserted to transfer table
    /// </summary>
    [DataMember]
    public DateTime Reg_Datetime { get; set; }

    /// <summary>
    /// 0 - new record, 1 - processed and OK, 2 - processed and Error, 3 - processed and rejected
    /// </summary>
    //[DataMember]
    //public PE.DbEntity.Enums.CommStatus Reg_Status { get; set; }

    /// <summary>
    /// Unique work order name
    /// </summary>
    [DataMember]
    public string Po_Id { get; set; }

    /// <summary>
    ///  unique schedule name
    /// </summary>
    [DataMember]
    public long? Schedule_Id { get; set; }

    /// <summary>
    /// PO sequence number
    /// </summary>
    [DataMember]
    public int? Seq_No { get; set; }

  }
}
