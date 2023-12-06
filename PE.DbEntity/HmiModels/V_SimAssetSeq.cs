using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_SimAssetSeq
    {
        public long SeqNo { get; set; }
        public long AssetId { get; set; }
        public short AssetCode { get; set; }
        [Required]
        [StringLength(50)]
        public string AssetName { get; set; }
        public bool? IsInitial { get; set; }
        public bool? IsLast { get; set; }
        public short MaxPassNo { get; set; }
        public short? TimeIn { get; set; }
    }
}
