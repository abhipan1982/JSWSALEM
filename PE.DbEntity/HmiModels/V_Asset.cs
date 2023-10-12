using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_Asset
    {
        public long OrderSeq { get; set; }
        public long AssetId { get; set; }
        public int AssetOrderSeq { get; set; }
        public int AssetCode { get; set; }
        [Required]
        [StringLength(50)]
        public string AssetName { get; set; }
        [Required]
        [StringLength(100)]
        public string AssetDescription { get; set; }
        [StringLength(50)]
        public string AssetTypeName { get; set; }
        [StringLength(4000)]
        public string Levels { get; set; }
        public long? AreaId { get; set; }
        public int? AreaCode { get; set; }
        [StringLength(50)]
        public string AreaName { get; set; }
        [StringLength(100)]
        public string AreaDescription { get; set; }
        public long? AreaTypeId { get; set; }
        [StringLength(50)]
        public string AreaTypeName { get; set; }
        public int? ZoneCode { get; set; }
        [StringLength(50)]
        public string ZoneName { get; set; }
        [StringLength(100)]
        public string ZoneDescription { get; set; }
        public bool IsActive { get; set; }
        public bool IsArea { get; set; }
        public bool IsZone { get; set; }
        public bool IsPositionBased { get; set; }
        public bool IsDelayCheckpoint { get; set; }
        public bool IsTrackingPoint { get; set; }
        public bool? IsQueue { get; set; }
        public short EnumTrackingAreaType { get; set; }
        public short EnumYardType { get; set; }
        public short EnumFillPattern { get; set; }
        public short EnumFillDirection { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TrackingAreaType { get; set; }
        public short? PositionsNumber { get; set; }
        public short? VirtualPositionsNumber { get; set; }
        public long? ParentAssetId { get; set; }
        [StringLength(50)]
        public string ParentAssetName { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string Path { get; set; }
        public int ValidFeatures { get; set; }
        public int L1Features { get; set; }
        public int AllFeatures { get; set; }
        public bool? FeaturesAreValid { get; set; }
    }
}
