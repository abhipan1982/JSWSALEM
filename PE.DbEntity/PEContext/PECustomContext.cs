using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PE.DbEntity.Models;

namespace PE.DbEntity.PEContext
{
    public partial class PECustomContext : BaseDbEntity.PEContext.PEContext
    {
        public PECustomContext()
        {
        }

        public PECustomContext(DbContextOptions<PECustomContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AvtestEXT> AvtestEXTs { get; set; }
        public virtual DbSet<MVHAssetsEXT> MVHAssetsEXTs { get; set; }
        public virtual DbSet<MVHFeaturesEXT> MVHFeaturesEXTs { get; set; }
        public virtual DbSet<MVHMeasurementsEXT> MVHMeasurementsEXTs { get; set; }
        public virtual DbSet<MVHSamplesEXT> MVHSamplesEXTs { get; set; }
        public virtual DbSet<PRMCustomersEXT> PRMCustomersEXTs { get; set; }
        public virtual DbSet<PRMHeatsEXT> PRMHeatsEXTs { get; set; }
        public virtual DbSet<PRMMaterialCatalogueEXT> PRMMaterialCatalogueEXTs { get; set; }
        public virtual DbSet<PRMMaterialsEXT> PRMMaterialsEXTs { get; set; }
        public virtual DbSet<PRMProductCatalogueEXT> PRMProductCatalogueEXTs { get; set; }
        public virtual DbSet<PRMProductsEXT> PRMProductsEXTs { get; set; }
        public virtual DbSet<PRMSteelgradesEXT> PRMSteelgradesEXTs { get; set; }
        public virtual DbSet<PRMWorkOrdersEXT> PRMWorkOrdersEXTs { get; set; }
        public virtual DbSet<TRKRawMaterialsEXT> TRKRawMaterialsEXTs { get; set; }
        public virtual DbSet<TRKRawMaterialsStepsEXT> TRKRawMaterialsStepsEXTs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MVHAssetsEXT>(entity =>
            {
                entity.HasKey(e => e.FKAssetId)
                    .HasName("PK_AssetsEXT");

                entity.Property(e => e.FKAssetId).ValueGeneratedNever();

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.MaxPassNo).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<MVHFeaturesEXT>(entity =>
            {
                entity.HasKey(e => e.FKFeatureId)
                    .HasName("PK_MVFeaturesEXT");

                entity.Property(e => e.FKFeatureId).ValueGeneratedNever();

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.OnAssetEntry).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<MVHMeasurementsEXT>(entity =>
            {
                entity.HasKey(e => e.FKMeasurementId)
                    .HasName("PK_MVMeasurementsEXT");

                entity.Property(e => e.FKMeasurementId).ValueGeneratedNever();

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<MVHSamplesEXT>(entity =>
            {
                entity.Property(e => e.FKSampleId).ValueGeneratedNever();

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<PRMCustomersEXT>(entity =>
            {
                entity.Property(e => e.FKCustomerId).ValueGeneratedNever();

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<PRMHeatsEXT>(entity =>
            {
                entity.HasKey(e => e.FKHeatId)
                    .HasName("PK_HeatsEXT");

                entity.Property(e => e.FKHeatId).ValueGeneratedNever();

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<PRMMaterialCatalogueEXT>(entity =>
            {
                entity.Property(e => e.FKMaterialCatalogueId).ValueGeneratedNever();

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<PRMMaterialsEXT>(entity =>
            {
                entity.Property(e => e.FKMaterialId).ValueGeneratedNever();

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<PRMProductCatalogueEXT>(entity =>
            {
                entity.Property(e => e.FKProductCatalogueId).ValueGeneratedNever();

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<PRMProductsEXT>(entity =>
            {
                entity.Property(e => e.FKProductId).ValueGeneratedNever();

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<PRMSteelgradesEXT>(entity =>
            {
                entity.Property(e => e.FKSteelgradeId).ValueGeneratedNever();

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<PRMWorkOrdersEXT>(entity =>
            {
                entity.HasKey(e => e.FKWorkOrderId)
                    .HasName("PK_PEWorkOrdersExt");

                entity.Property(e => e.FKWorkOrderId).ValueGeneratedNever();

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<TRKRawMaterialsEXT>(entity =>
            {
                entity.HasKey(e => e.FKRawMaterialId)
                    .HasName("PK_RawMaterialsIndexEXT");

                entity.Property(e => e.FKRawMaterialId).ValueGeneratedNever();

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<TRKRawMaterialsStepsEXT>(entity =>
            {
                entity.HasKey(e => e.FKRawMaterialStepId)
                    .HasName("PK_MVHRawMaterialsStepsEXT");

                entity.Property(e => e.FKRawMaterialStepId).ValueGeneratedNever();

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
