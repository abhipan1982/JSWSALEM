using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PE.DbEntity.DWModels;

namespace PE.DbEntity.PEContext
{
    public partial class DWContext : DbContext
    {
        public DWContext()
        {
        }

        public DWContext(DbContextOptions<DWContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DimAsset> DimAssets { get; set; }
        public virtual DbSet<DimCrew> DimCrews { get; set; }
        public virtual DbSet<DimCustomer> DimCustomers { get; set; }
        public virtual DbSet<DimDate> DimDates { get; set; }
        public virtual DbSet<DimDefectCatalogue> DimDefectCatalogues { get; set; }
        public virtual DbSet<DimEventCatalogue> DimEventCatalogues { get; set; }
        public virtual DbSet<DimEventType> DimEventTypes { get; set; }
        public virtual DbSet<DimFeature> DimFeatures { get; set; }
        public virtual DbSet<DimHeat> DimHeats { get; set; }
        public virtual DbSet<DimHour> DimHours { get; set; }
        public virtual DbSet<DimInspectionResult> DimInspectionResults { get; set; }
        public virtual DbSet<DimMaterial> DimMaterials { get; set; }
        public virtual DbSet<DimMaterialCatalogue> DimMaterialCatalogues { get; set; }
        public virtual DbSet<DimMaterialStatus> DimMaterialStatuses { get; set; }
        public virtual DbSet<DimProduct> DimProducts { get; set; }
        public virtual DbSet<DimProductCatalogue> DimProductCatalogues { get; set; }
        public virtual DbSet<DimRawMaterial> DimRawMaterials { get; set; }
        public virtual DbSet<DimRule> DimRules { get; set; }
        public virtual DbSet<DimShift> DimShifts { get; set; }
        public virtual DbSet<DimShiftDefinition> DimShiftDefinitions { get; set; }
        public virtual DbSet<DimSteelgrade> DimSteelgrades { get; set; }
        public virtual DbSet<DimTime> DimTimes { get; set; }
        public virtual DbSet<DimTypeOfScrap> DimTypeOfScraps { get; set; }
        public virtual DbSet<DimUnit> DimUnits { get; set; }
        public virtual DbSet<DimUser> DimUsers { get; set; }
        public virtual DbSet<DimWorkOrder> DimWorkOrders { get; set; }
        public virtual DbSet<DimWorkOrderStatus> DimWorkOrderStatuses { get; set; }
        public virtual DbSet<DimYear> DimYears { get; set; }
        public virtual DbSet<FactEvent> FactEvents { get; set; }
        public virtual DbSet<FactMaterial> FactMaterials { get; set; }
        public virtual DbSet<FactRating> FactRatings { get; set; }
        public virtual DbSet<FactRatingCompensation> FactRatingCompensations { get; set; }
        public virtual DbSet<FactRatingRootCause> FactRatingRootCauses { get; set; }
        public virtual DbSet<FactShift> FactShifts { get; set; }
        public virtual DbSet<FactWorkOrder> FactWorkOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DimAsset>(entity =>
            {
                entity.ToView("DimAsset", "dw");
            });

            modelBuilder.Entity<DimCrew>(entity =>
            {
                entity.ToView("DimCrew", "dw");
            });

            modelBuilder.Entity<DimCustomer>(entity =>
            {
                entity.ToView("DimCustomer", "dw");
            });

            modelBuilder.Entity<DimDate>(entity =>
            {
                entity.ToView("DimDate", "dw");
            });

            modelBuilder.Entity<DimDefectCatalogue>(entity =>
            {
                entity.ToView("DimDefectCatalogue", "dw");
            });

            modelBuilder.Entity<DimEventCatalogue>(entity =>
            {
                entity.ToView("DimEventCatalogue", "dw");
            });

            modelBuilder.Entity<DimEventType>(entity =>
            {
                entity.ToView("DimEventType", "dw");
            });

            modelBuilder.Entity<DimFeature>(entity =>
            {
                entity.ToView("DimFeature", "dw");
            });

            modelBuilder.Entity<DimHeat>(entity =>
            {
                entity.ToView("DimHeat", "dw");
            });

            modelBuilder.Entity<DimHour>(entity =>
            {
                entity.ToView("DimHour", "dw");
            });

            modelBuilder.Entity<DimInspectionResult>(entity =>
            {
                entity.ToView("DimInspectionResult", "dw");
            });

            modelBuilder.Entity<DimMaterial>(entity =>
            {
                entity.ToView("DimMaterial", "dw");
            });

            modelBuilder.Entity<DimMaterialCatalogue>(entity =>
            {
                entity.ToView("DimMaterialCatalogue", "dw");
            });

            modelBuilder.Entity<DimMaterialStatus>(entity =>
            {
                entity.ToView("DimMaterialStatus", "dw");
            });

            modelBuilder.Entity<DimProduct>(entity =>
            {
                entity.ToView("DimProduct", "dw");
            });

            modelBuilder.Entity<DimProductCatalogue>(entity =>
            {
                entity.ToView("DimProductCatalogue", "dw");
            });

            modelBuilder.Entity<DimRawMaterial>(entity =>
            {
                entity.ToView("DimRawMaterial", "dw");
            });

            modelBuilder.Entity<DimRule>(entity =>
            {
                entity.ToView("DimRule", "dw");
            });

            modelBuilder.Entity<DimShift>(entity =>
            {
                entity.ToView("DimShift", "dw");
            });

            modelBuilder.Entity<DimShiftDefinition>(entity =>
            {
                entity.ToView("DimShiftDefinition", "dw");
            });

            modelBuilder.Entity<DimSteelgrade>(entity =>
            {
                entity.ToView("DimSteelgrade", "dw");
            });

            modelBuilder.Entity<DimTime>(entity =>
            {
                entity.ToView("DimTime", "dw");
            });

            modelBuilder.Entity<DimTypeOfScrap>(entity =>
            {
                entity.ToView("DimTypeOfScrap", "dw");
            });

            modelBuilder.Entity<DimUnit>(entity =>
            {
                entity.ToView("DimUnit", "dw");
            });

            modelBuilder.Entity<DimUser>(entity =>
            {
                entity.ToView("DimUser", "dw");
            });

            modelBuilder.Entity<DimWorkOrder>(entity =>
            {
                entity.ToView("DimWorkOrder", "dw");
            });

            modelBuilder.Entity<DimWorkOrderStatus>(entity =>
            {
                entity.ToView("DimWorkOrderStatus", "dw");
            });

            modelBuilder.Entity<DimYear>(entity =>
            {
                entity.ToView("DimYear", "dw");
            });

            modelBuilder.Entity<FactEvent>(entity =>
            {
                entity.ToView("FactEvent", "dw");
            });

            modelBuilder.Entity<FactMaterial>(entity =>
            {
                entity.ToView("FactMaterial", "dw");
            });

            modelBuilder.Entity<FactRating>(entity =>
            {
                entity.ToView("FactRating", "dw");
            });

            modelBuilder.Entity<FactRatingCompensation>(entity =>
            {
                entity.ToView("FactRatingCompensation", "dw");
            });

            modelBuilder.Entity<FactRatingRootCause>(entity =>
            {
                entity.ToView("FactRatingRootCause", "dw");
            });

            modelBuilder.Entity<FactShift>(entity =>
            {
                entity.ToView("FactShift", "dw");
            });

            modelBuilder.Entity<FactWorkOrder>(entity =>
            {
                entity.ToView("FactWorkOrder", "dw");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
