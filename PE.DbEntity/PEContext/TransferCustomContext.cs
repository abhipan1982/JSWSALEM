using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PE.DbEntity.TransferModels;

namespace PE.DbEntity.PEContext
{
    public partial class TransferCustomContext : BaseDbEntity.PEContext.TransferContext
    {
        public TransferCustomContext()
        {
        }

        public TransferCustomContext(DbContextOptions<TransferCustomContext> options)
            : base(options)
        {
        }

        public virtual DbSet<L2L3BatchReport> L2L3BatchReports { get; set; }
        public virtual DbSet<L3L2BatchDataDefinition> L3L2BatchDataDefinitions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<L2L3BatchReport>(entity =>
            {
                entity.HasKey(e => e.BATCH_NO)
                    .HasName("PK_L2L3BatchReportTelegram");

                entity.Property(e => e.BATCH_NO).HasComment("Bloom Id (unique)");

                entity.Property(e => e.BAR_CODE_SCAN_TIME).HasComment("YYYYMMDDHHMMSS Bar code scanning Time");

                entity.Property(e => e.BLOOMS_IN_BUNDLE).HasComment(" No. of blooms in bundle");

                entity.Property(e => e.BUNDLE_WGT).HasComment("Tons Bundle Weight");

                entity.Property(e => e.CHARGING_REMARKS).HasComment("Charging - Remarks");

                entity.Property(e => e.COBBLE_AREA).HasComment("Cobble Area");

                entity.Property(e => e.COBBLE_PERC).HasComment("% COBBLE %");

                entity.Property(e => e.COBBLE_REASON).HasComment("Cobble Reason");

                entity.Property(e => e.COLLING_REMARKS).HasComment("Cooling Bed - Remarks");

                entity.Property(e => e.CP1_OP_NAME).HasComment("Furnace Charging – CP1 Operator Name");

                entity.Property(e => e.CP1_SUP_NAME).HasComment("Furnace Charging – CP1 Supervisor Name");

                entity.Property(e => e.CP2_HELP_NAME).HasComment("Furnace Discharging –CP2 Helper Name");

                entity.Property(e => e.CP2_OP_NAME).HasComment("Furnace Discharging CP2 Operator Name");

                entity.Property(e => e.CP3_OP_NAME).HasComment("Rolling Mill – CP3 Operator Name");

                entity.Property(e => e.CP4_OP_NAME).HasComment("Cooling Bed – CP4 operator Name");

                entity.Property(e => e.CP5_OP_NAME).HasComment("Rolling Mill – CP5 Operator Name");

                entity.Property(e => e.CP6_OP_NAME).HasComment("Cooling Bed – CP6 Operator Name");

                entity.Property(e => e.CommMessage).HasComment("Additional messages from subsequent processing modules");

                entity.Property(e => e.CommStatus).HasComment("--Communication status,\r\n     --result of message\r\n      --processing:\r\n        --0: New entry, no validation\r\n         --errors\r\n         --1: Entry currently\r\n         --processed by system\r\n         --2: Entry processed by\r\n          --system, no errors\r\n         ---1: Validation errors (DB\r\n         --validation)\r\n      ---2: Entry processed by\r\n        --system, but processing\r\n          --errors occured");

                entity.Property(e => e.DISCHARGING_REMARKS).HasComment("DisCharging - Remarks");

                entity.Property(e => e.FUR_CHARGE_TIME).HasComment("YYYYMMDDHHMMSS Furnace Charging Time");

                entity.Property(e => e.FUR_DISCHARGE_TIME).HasComment("YYYYMMDDHHMMSS Furnace DisCharging Time");

                entity.Property(e => e.INDIRECT_PERC).HasComment("% INDIRECT %");

                entity.Property(e => e.MISROLL_PERC).HasComment("% MISROLL %");

                entity.Property(e => e.NO_ROLLED_BARS).HasComment("Total Number of Rolled Billets. Partial scraps are included.");

                entity.Property(e => e.PO_NO).HasComment("Unique work order number");

                entity.Property(e => e.ROLLING_REMARKS).HasComment("Rolling - Remarks");

                entity.Property(e => e.ROLLING_TIME).HasComment("YYYYMMDDHHMMSS Rolling Time");

                entity.Property(e => e.SHIFT_IN_CHARGE_NAME).HasComment("Shift-In-Charge Name");

                entity.Property(e => e.SUPERVISOR_NAME).HasComment("Supervisor Name ");

                entity.Property(e => e.ValidationCheck).HasComment("Text describing current state of the message");
            });

            modelBuilder.Entity<L3L2BatchDataDefinition>(entity =>
            {
                entity.HasKey(e => e.CounterId)
                    .HasName("PK_Counter");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.BATCH_NO).HasComment("Bloom Id (unique)");

                entity.Property(e => e.BLM_BRM_C_S_THICK).HasComment("Input thickness/diameter");

                entity.Property(e => e.BLM_BRM_C_S_WIDTH).HasComment("Input Width");

                entity.Property(e => e.BLM_BRM_LENGTH).HasComment("Bloom Length");

                entity.Property(e => e.BLM_BRM_WEIGHT).HasComment("Bloom Weight");

                entity.Property(e => e.CHARGE_TYPE).HasComment("1/2 Charging Type (1=Cold, 2=Hot)");

                entity.Property(e => e.COOL_TYPE).HasComment("Cooling Type (1=Slow, 2=Fast,3=Normal)");

                entity.Property(e => e.CUST_NAME).HasComment("Customer Name");

                entity.Property(e => e.CommMessage).HasComment("Additional messages from subsequent processing modules");

                entity.Property(e => e.CommStatus).HasComment("message processing:\r\n      --0: New entry, no validation errors\r\n        --1: Entry currently processed by system\r\n        --2: Entry processed by system, no errors\r\n         -- -1: Validation errors (DB Validation)\r\n          -- -2: Entry processed by system, but\r\n          --processing errors occurred.");

                entity.Property(e => e.CreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.HEAT_NO).HasComment("Heat No");

                entity.Property(e => e.INPUT_MATERIAL).HasComment("Bloom Shape Indicator (NRND,NBLM)");

                entity.Property(e => e.LIFT_TYPE).HasComment(" 1/2 Lifting Type (1=Chain, 2=Magnet)");

                entity.Property(e => e.OUTPUT_MATERIAL).HasComment("Product Shape Indicator (RND, RCS)");

                entity.Property(e => e.PO_NO).HasComment("Unique work order number");

                entity.Property(e => e.PSN).HasComment("Product Standard No for the order");

                entity.Property(e => e.ROLLED_THICK).HasComment("Output Thickness");

                entity.Property(e => e.ROLLED_WIDTH).HasComment("Output Width");

                entity.Property(e => e.SO_QTY).HasComment("Weight of order to be delivered to customer");

                entity.Property(e => e.S_DIA_TOL_MM_LOWER).HasComment("Diameter Tolerance RND MIN");

                entity.Property(e => e.S_DIA_TOL_MM_UPPER).HasComment("Diameter Tolerance RND MAX");

                entity.Property(e => e.S_LENGTH).HasComment("Target Length");

                entity.Property(e => e.S_LENGTH_MM_MAX).HasComment("Length Tolerance MAX");

                entity.Property(e => e.S_LENGTH_MM_MIN).HasComment("Length Tolerance MIN");

                entity.Property(e => e.S_MULTIPLE_LENGTH_MM).HasComment("Multiple Length");

                entity.Property(e => e.S_OUT_OF_SQUARNESS_MM_MAX).HasComment("Shape Tolerance for RCS MAX");

                entity.Property(e => e.S_OUT_OF_SQUARNESS_MM_MIN).HasComment("Shape Tolerance for RCS MIN");

                entity.Property(e => e.S_OVALITY_MM_MAX).HasComment("Ovality Tolerance MAX");

                entity.Property(e => e.S_OVALITY_MM_MIN).HasComment("Ovality Tolerance MIN");

                entity.Property(e => e.S_SIDE_TOL_MM_NEG).HasComment("RCS MIN");

                entity.Property(e => e.S_SIDE_TOL_MM_POS).HasComment("RCS MAX");

                entity.Property(e => e.UpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ValidationCheck).HasComment("Text describing current status of the message");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
