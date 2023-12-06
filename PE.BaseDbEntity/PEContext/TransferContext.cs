using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PE.BaseDbEntity.TransferModels;

namespace PE.BaseDbEntity.PEContext
{
    public partial class TransferContext : DbContext
    {
        public TransferContext()
        {
        }

        public TransferContext(DbContextOptions<TransferContext> options)
            : base(options)
        {
        }

        public virtual DbSet<L2L3ProductReport> L2L3ProductReports { get; set; }
        public virtual DbSet<L2L3WorkOrderReport> L2L3WorkOrderReports { get; set; }
        public virtual DbSet<L3L2WorkOrderDefinition> L3L2WorkOrderDefinitions { get; set; }
        public virtual DbSet<V_L3L2TransferTablesSummary> V_L3L2TransferTablesSummaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<L2L3ProductReport>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedTs).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<L2L3WorkOrderReport>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedTs).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<L3L2WorkOrderDefinition>(entity =>
            {
                entity.HasKey(e => e.CounterId)
                    .HasName("PK_Counter");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedTs).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<V_L3L2TransferTablesSummary>(entity =>
            {
                entity.ToView("V_L3L2TransferTablesSummary", "xfr");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
