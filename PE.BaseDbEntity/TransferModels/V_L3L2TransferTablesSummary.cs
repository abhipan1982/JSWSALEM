using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.TransferModels
{
    [Keyless]
    public partial class V_L3L2TransferTablesSummary
    {
        public long OrderSeq { get; set; }
        [Required]
        [StringLength(30)]
        [Unicode(false)]
        public string TransferTableName { get; set; }
        public int? StatusNew { get; set; }
        public int? StatusInProc { get; set; }
        public int? StatusOK { get; set; }
        public int? StatusValErr { get; set; }
        public int? StatusProcErr { get; set; }
    }
}
