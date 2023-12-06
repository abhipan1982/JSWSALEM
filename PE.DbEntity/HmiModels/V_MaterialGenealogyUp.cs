using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_MaterialGenealogyUp
    {
        [StringLength(30)]
        public string Path { get; set; }
        public int? LevelNo { get; set; }
        public long? RootId { get; set; }
        public long? MaterialId { get; set; }
        public long? ParentId { get; set; }
        public short? ChildsNo { get; set; }
        public int? IsParent { get; set; }
        public short? ParentsCut { get; set; }
        public double? RootInitialLength { get; set; }
        public double? InitialLength { get; set; }
        public double? ActualLength { get; set; }
        [StringLength(50)]
        public string RawMaterialName { get; set; }
        [StringLength(4000)]
        public string Level { get; set; }
    }
}
