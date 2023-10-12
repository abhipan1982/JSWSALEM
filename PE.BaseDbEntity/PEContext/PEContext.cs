using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PE.BaseDbEntity.Models;

namespace PE.BaseDbEntity.PEContext
{
    public partial class PEContext : DbContext
    {
        public PEContext()
        {
        }

        public PEContext(DbContextOptions<PEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DBAttachmentType> DBAttachmentTypes { get; set; }
        public virtual DbSet<DBDataType> DBDataTypes { get; set; }
        public virtual DbSet<DBLog> DBLogs { get; set; }
        public virtual DbSet<DBLogProcsExecute> DBLogProcsExecutes { get; set; }
        public virtual DbSet<DBProperty> DBProperties { get; set; }
        public virtual DbSet<DBPropertyValue> DBPropertyValues { get; set; }
        public virtual DbSet<EVTCrew> EVTCrews { get; set; }
        public virtual DbSet<EVTDaysOfYear> EVTDaysOfYears { get; set; }
        public virtual DbSet<EVTEvent> EVTEvents { get; set; }
        public virtual DbSet<EVTEventCatalogue> EVTEventCatalogues { get; set; }
        public virtual DbSet<EVTEventCatalogueCategory> EVTEventCatalogueCategories { get; set; }
        public virtual DbSet<EVTEventCategoryGroup> EVTEventCategoryGroups { get; set; }
        public virtual DbSet<EVTEventType> EVTEventTypes { get; set; }
        public virtual DbSet<EVTShiftCalendar> EVTShiftCalendars { get; set; }
        public virtual DbSet<EVTShiftCrewPattern> EVTShiftCrewPatterns { get; set; }
        public virtual DbSet<EVTShiftDefinition> EVTShiftDefinitions { get; set; }
        public virtual DbSet<EVTShiftLayout> EVTShiftLayouts { get; set; }
        public virtual DbSet<EVTTrigger> EVTTriggers { get; set; }
        public virtual DbSet<EVTTriggersFeature> EVTTriggersFeatures { get; set; }
        public virtual DbSet<HMIRefreshKey> HMIRefreshKeys { get; set; }
        public virtual DbSet<HMIUserConfiguration> HMIUserConfigurations { get; set; }
        public virtual DbSet<HMIWidget> HMIWidgets { get; set; }
        public virtual DbSet<HMIWidgetConfiguration> HMIWidgetConfigurations { get; set; }
        public virtual DbSet<L1ABypassConfiguration> L1ABypassConfigurations { get; set; }
        public virtual DbSet<L1ABypassInstance> L1ABypassInstances { get; set; }
        public virtual DbSet<L1ABypassType> L1ABypassTypes { get; set; }
        public virtual DbSet<L1ARaisedBypass> L1ARaisedBypasses { get; set; }
        public virtual DbSet<LTRReportsConfiguration> LTRReportsConfigurations { get; set; }
        public virtual DbSet<MNTCrewMember> MNTCrewMembers { get; set; }
        public virtual DbSet<MNTEquipment> MNTEquipments { get; set; }
        public virtual DbSet<MNTEquipmentGroup> MNTEquipmentGroups { get; set; }
        public virtual DbSet<MNTEquipmentHistory> MNTEquipmentHistories { get; set; }
        public virtual DbSet<MNTEquipmentSupplier> MNTEquipmentSuppliers { get; set; }
        public virtual DbSet<MNTMaintenanceAction> MNTMaintenanceActions { get; set; }
        public virtual DbSet<MNTMaintenanceSchedule> MNTMaintenanceSchedules { get; set; }
        public virtual DbSet<MNTMember> MNTMembers { get; set; }
        public virtual DbSet<MNTMemberRole> MNTMemberRoles { get; set; }
        public virtual DbSet<MVHAsset> MVHAssets { get; set; }
        public virtual DbSet<MVHAssetFeatureTemplate> MVHAssetFeatureTemplates { get; set; }
        public virtual DbSet<MVHAssetLayout> MVHAssetLayouts { get; set; }
        public virtual DbSet<MVHAssetTemplate> MVHAssetTemplates { get; set; }
        public virtual DbSet<MVHAssetType> MVHAssetTypes { get; set; }
        public virtual DbSet<MVHAssetsLocation> MVHAssetsLocations { get; set; }
        public virtual DbSet<MVHFeature> MVHFeatures { get; set; }
        public virtual DbSet<MVHFeatureCalculated> MVHFeatureCalculateds { get; set; }
        public virtual DbSet<MVHFeatureCustom> MVHFeatureCustoms { get; set; }
        public virtual DbSet<MVHFeatureTemplate> MVHFeatureTemplates { get; set; }
        public virtual DbSet<MVHLayout> MVHLayouts { get; set; }
        public virtual DbSet<MVHMeasurement> MVHMeasurements { get; set; }
        public virtual DbSet<MVHSample> MVHSamples { get; set; }
        public virtual DbSet<PPLSchedule> PPLSchedules { get; set; }
        public virtual DbSet<PRFKPIDefinition> PRFKPIDefinitions { get; set; }
        public virtual DbSet<PRFKPIValue> PRFKPIValues { get; set; }
        public virtual DbSet<PRMCustomer> PRMCustomers { get; set; }
        public virtual DbSet<PRMHeat> PRMHeats { get; set; }
        public virtual DbSet<PRMHeatChemicalAnalysis> PRMHeatChemicalAnalyses { get; set; }
        public virtual DbSet<PRMHeatSupplier> PRMHeatSuppliers { get; set; }
        public virtual DbSet<PRMMaterial> PRMMaterials { get; set; }
        public virtual DbSet<PRMMaterialCatalogue> PRMMaterialCatalogues { get; set; }
        public virtual DbSet<PRMMaterialCatalogueType> PRMMaterialCatalogueTypes { get; set; }
        public virtual DbSet<PRMMaterialStep> PRMMaterialSteps { get; set; }
        public virtual DbSet<PRMProduct> PRMProducts { get; set; }
        public virtual DbSet<PRMProductCatalogue> PRMProductCatalogues { get; set; }
        public virtual DbSet<PRMProductCatalogueType> PRMProductCatalogueTypes { get; set; }
        public virtual DbSet<PRMProductStep> PRMProductSteps { get; set; }
        public virtual DbSet<PRMScrapGroup> PRMScrapGroups { get; set; }
        public virtual DbSet<PRMShape> PRMShapes { get; set; }
        public virtual DbSet<PRMSteelGroup> PRMSteelGroups { get; set; }
        public virtual DbSet<PRMSteelgrade> PRMSteelgrades { get; set; }
        public virtual DbSet<PRMSteelgradeChemicalComposition> PRMSteelgradeChemicalCompositions { get; set; }
        public virtual DbSet<PRMWorkOrder> PRMWorkOrders { get; set; }
        public virtual DbSet<QEAlias> QEAliases { get; set; }
        public virtual DbSet<QEAliasValue> QEAliasValues { get; set; }
        public virtual DbSet<QECompensation> QECompensations { get; set; }
        public virtual DbSet<QECompensationAggregate> QECompensationAggregates { get; set; }
        public virtual DbSet<QECompensationType> QECompensationTypes { get; set; }
        public virtual DbSet<QELengthSeriesValue> QELengthSeriesValues { get; set; }
        public virtual DbSet<QEMappingEntry> QEMappingEntries { get; set; }
        public virtual DbSet<QEMappingValue> QEMappingValues { get; set; }
        public virtual DbSet<QERating> QERatings { get; set; }
        public virtual DbSet<QERootCause> QERootCauses { get; set; }
        public virtual DbSet<QERootCauseAggregate> QERootCauseAggregates { get; set; }
        public virtual DbSet<QERuleMappingValue> QERuleMappingValues { get; set; }
        public virtual DbSet<QETimeSeriesValue> QETimeSeriesValues { get; set; }
        public virtual DbSet<QETrigger> QETriggers { get; set; }
        public virtual DbSet<QTYDefect> QTYDefects { get; set; }
        public virtual DbSet<QTYDefectCatalogue> QTYDefectCatalogues { get; set; }
        public virtual DbSet<QTYDefectCatalogueCategory> QTYDefectCatalogueCategories { get; set; }
        public virtual DbSet<QTYDefectCategoryGroup> QTYDefectCategoryGroups { get; set; }
        public virtual DbSet<QTYQualityInspection> QTYQualityInspections { get; set; }
        public virtual DbSet<RLSCassette> RLSCassettes { get; set; }
        public virtual DbSet<RLSCassetteType> RLSCassetteTypes { get; set; }
        public virtual DbSet<RLSGrooveTemplate> RLSGrooveTemplates { get; set; }
        public virtual DbSet<RLSRoll> RLSRolls { get; set; }
        public virtual DbSet<RLSRollGroovesHistory> RLSRollGroovesHistories { get; set; }
        public virtual DbSet<RLSRollSet> RLSRollSets { get; set; }
        public virtual DbSet<RLSRollSetHistory> RLSRollSetHistories { get; set; }
        public virtual DbSet<RLSRollType> RLSRollTypes { get; set; }
        public virtual DbSet<RLSStand> RLSStands { get; set; }
        public virtual DbSet<STPConfiguration> STPConfigurations { get; set; }
        public virtual DbSet<STPInstruction> STPInstructions { get; set; }
        public virtual DbSet<STPIssue> STPIssues { get; set; }
        public virtual DbSet<STPLayout> STPLayouts { get; set; }
        public virtual DbSet<STPParameter> STPParameters { get; set; }
        public virtual DbSet<STPProductLayout> STPProductLayouts { get; set; }
        public virtual DbSet<STPSetup> STPSetups { get; set; }
        public virtual DbSet<STPSetupConfiguration> STPSetupConfigurations { get; set; }
        public virtual DbSet<STPSetupInstruction> STPSetupInstructions { get; set; }
        public virtual DbSet<STPSetupParameter> STPSetupParameters { get; set; }
        public virtual DbSet<STPSetupSent> STPSetupSents { get; set; }
        public virtual DbSet<STPSetupType> STPSetupTypes { get; set; }
        public virtual DbSet<STPSetupTypeInstruction> STPSetupTypeInstructions { get; set; }
        public virtual DbSet<STPSetupTypeParameter> STPSetupTypeParameters { get; set; }
        public virtual DbSet<STPSetupWorkOrder> STPSetupWorkOrders { get; set; }
        public virtual DbSet<TRKLayerRawMaterialRelation> TRKLayerRawMaterialRelations { get; set; }
        public virtual DbSet<TRKLayerRelation> TRKLayerRelations { get; set; }
        public virtual DbSet<TRKMillControlData> TRKMillControlDatas { get; set; }
        public virtual DbSet<TRKRawMaterial> TRKRawMaterials { get; set; }
        public virtual DbSet<TRKRawMaterialLocation> TRKRawMaterialLocations { get; set; }
        public virtual DbSet<TRKRawMaterialRelation> TRKRawMaterialRelations { get; set; }
        public virtual DbSet<TRKRawMaterialsCut> TRKRawMaterialsCuts { get; set; }
        public virtual DbSet<TRKRawMaterialsInFurnace> TRKRawMaterialsInFurnaces { get; set; }
        public virtual DbSet<TRKRawMaterialsStep> TRKRawMaterialsSteps { get; set; }
        public virtual DbSet<TRKTrackingInstruction> TRKTrackingInstructions { get; set; }
        public virtual DbSet<ZPCZebraPrinter> ZPCZebraPrinters { get; set; }
        public virtual DbSet<ZPCZebraTemplate> ZPCZebraTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DBDataType>(entity =>
            {
                entity.HasKey(e => e.DataTypeId)
                    .HasName("PK_DataTypes");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<DBLog>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.LogDateTs).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<DBProperty>(entity =>
            {
                entity.HasKey(e => e.PropertyId)
                    .HasName("PK_Properties");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKDataType)
                    .WithMany(p => p.DBProperties)
                    .HasForeignKey(d => d.FKDataTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Properties_DataTypes");
            });

            modelBuilder.Entity<DBPropertyValue>(entity =>
            {
                entity.HasKey(e => e.PropertyValueId)
                    .HasName("PK_PropertyValues");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKProperty)
                    .WithMany(p => p.DBPropertyValues)
                    .HasForeignKey(d => d.FKPropertyId)
                    .HasConstraintName("FK_PropertyValues_Properties");
            });

            modelBuilder.Entity<EVTCrew>(entity =>
            {
                entity.HasKey(e => e.CrewId)
                    .HasName("PK_Crews");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.NextCrew)
                    .WithMany(p => p.InverseNextCrew)
                    .HasForeignKey(d => d.NextCrewId)
                    .HasConstraintName("FK_Crews_Crews");
            });

            modelBuilder.Entity<EVTDaysOfYear>(entity =>
            {
                entity.HasKey(e => e.DaysOfYearId)
                    .HasName("PK_DaysOfYear");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.DateANSI).HasComputedColumnSql("(CONVERT([varchar](10),[DateDay],(102)))", false);

                entity.Property(e => e.DateDE).HasComputedColumnSql("(CONVERT([varchar](10),[DateDay],(104)))", false);

                entity.Property(e => e.DateISO).HasComputedColumnSql("(CONVERT([varchar](8),[DateDay],(112)))", false);

                entity.Property(e => e.DateIT).HasComputedColumnSql("(CONVERT([varchar](10),[DateDay],(105)))", false);

                entity.Property(e => e.DateUK).HasComputedColumnSql("(CONVERT([varchar](10),[DateDay],(103)))", false);

                entity.Property(e => e.DateUS).HasComputedColumnSql("(CONVERT([varchar](10),[DateDay],(101)))", false);

                entity.Property(e => e.Day).HasComputedColumnSql("(datepart(day,[DateDay]))", false);

                entity.Property(e => e.DayNumber).HasComputedColumnSql("(datepart(day,[DateDay]))", false);

                entity.Property(e => e.DayYearNumber).HasComputedColumnSql("(datepart(dayofyear,[DateDay]))", false);

                entity.Property(e => e.FKShiftLayoutId).HasDefaultValueSql("((1))");

                entity.Property(e => e.FirstOfMonth).HasComputedColumnSql("(CONVERT([date],dateadd(month,datediff(month,(0),[DateDay]),(0))))", false);

                entity.Property(e => e.FirstOfYear).HasComputedColumnSql("(CONVERT([date],dateadd(year,datediff(year,(0),[DateDay]),(0))))", false);

                entity.Property(e => e.HalfOfYear).HasComputedColumnSql("(case when datepart(quarter,[DateDay])<=(2) then (1) else (2) end)", false);

                entity.Property(e => e.ISOWeekNumber).HasComputedColumnSql("(datepart(iso_week,[DateDay]))", false);

                entity.Property(e => e.IsWeekend).HasComputedColumnSql("(isnull(CONVERT([bit],case when datepart(weekday,[DateDay])=(7) OR datepart(weekday,[DateDay])=(1) then (1) else (0) end),(0)))", false);

                entity.Property(e => e.LastOfMonth).HasComputedColumnSql("(CONVERT([date],eomonth([DateDay])))", false);

                entity.Property(e => e.LastOfYear).HasComputedColumnSql("(CONVERT([date],dateadd(day,(-1),dateadd(year,datediff(year,(0),[DateDay])+(1),(0)))))", false);

                entity.Property(e => e.Month).HasComputedColumnSql("(datepart(month,[DateDay]))", false);

                entity.Property(e => e.MonthName).HasComputedColumnSql("(datename(month,[DateDay]))", false);

                entity.Property(e => e.MonthNumber).HasComputedColumnSql("(datepart(month,[DateDay]))", false);

                entity.Property(e => e.Quarter).HasComputedColumnSql("(datepart(quarter,[DateDay]))", false);

                entity.Property(e => e.WeekDayName).HasComputedColumnSql("(datename(weekday,[DateDay]))", false);

                entity.Property(e => e.WeekDayNumber).HasComputedColumnSql("(datepart(weekday,[DateDay]))", false);

                entity.Property(e => e.WeekNo).HasComputedColumnSql("(datepart(week,[DateDay]))", false);

                entity.Property(e => e.WeekNumber).HasComputedColumnSql("(datepart(week,[DateDay]))", false);

                entity.Property(e => e.Year).HasComputedColumnSql("(datepart(year,[DateDay]))", false);

                entity.Property(e => e.YearNumber).HasComputedColumnSql("(datepart(year,[DateDay]))", false);

                entity.HasOne(d => d.FKShiftLayout)
                    .WithMany(p => p.EVTDaysOfYears)
                    .HasForeignKey(d => d.FKShiftLayoutId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EVTDaysOfYear_EVTShiftLayouts");
            });

            modelBuilder.Entity<EVTEvent>(entity =>
            {
                entity.HasKey(e => e.EventId)
                    .HasName("PK_EVTMillEvents");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.EventStartTs).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.FKAsset)
                    .WithMany(p => p.EVTEvents)
                    .HasForeignKey(d => d.FKAssetId)
                    .HasConstraintName("FK_EVTMillEvents_MVHAssets");

                entity.HasOne(d => d.FKEventCatalogue)
                    .WithMany(p => p.EVTEvents)
                    .HasForeignKey(d => d.FKEventCatalogueId)
                    .HasConstraintName("FK_EVTEvents_EVTEventCatalogue");

                entity.HasOne(d => d.FKEventType)
                    .WithMany(p => p.EVTEvents)
                    .HasForeignKey(d => d.FKEventTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EVTMillEvents_EVTEventTypes");

                entity.HasOne(d => d.FKParentEvent)
                    .WithMany(p => p.InverseFKParentEvent)
                    .HasForeignKey(d => d.FKParentEventId)
                    .HasConstraintName("FK_EVTEvents_EVTEvents");

                entity.HasOne(d => d.FKRawMaterial)
                    .WithMany(p => p.EVTEvents)
                    .HasForeignKey(d => d.FKRawMaterialId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_EVTMillEvents_MVHRawMaterials");

                entity.HasOne(d => d.FKShiftCalendar)
                    .WithMany(p => p.EVTEvents)
                    .HasForeignKey(d => d.FKShiftCalendarId)
                    .HasConstraintName("FK_EVTMillEvents_ShiftCalendar");

                entity.HasOne(d => d.FKWorkOrder)
                    .WithMany(p => p.EVTEvents)
                    .HasForeignKey(d => d.FKWorkOrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_EVTMillEvents_PRMWorkOrders");
            });

            modelBuilder.Entity<EVTEventCatalogue>(entity =>
            {
                entity.HasKey(e => e.EventCatalogueId)
                    .HasName("PK_DelayCatalogue");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.FKEventCatalogueCategory)
                    .WithMany(p => p.EVTEventCatalogues)
                    .HasForeignKey(d => d.FKEventCatalogueCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DelayCatalogue_DelayCatalogueCategory");

                entity.HasOne(d => d.FKParentEventCatalogue)
                    .WithMany(p => p.InverseFKParentEventCatalogue)
                    .HasForeignKey(d => d.FKParentEventCatalogueId)
                    .HasConstraintName("FK_DelayCatalogue");
            });

            modelBuilder.Entity<EVTEventCatalogueCategory>(entity =>
            {
                entity.HasKey(e => e.EventCatalogueCategoryId)
                    .HasName("PK_DelayCatalogueCategory");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKEventCategoryGroup)
                    .WithMany(p => p.EVTEventCatalogueCategories)
                    .HasForeignKey(d => d.FKEventCategoryGroupId)
                    .HasConstraintName("FK_DLSDelayCatalogueCategory_DLSDelayCategoryGroups");

                entity.HasOne(d => d.FKEventType)
                    .WithMany(p => p.EVTEventCatalogueCategories)
                    .HasForeignKey(d => d.FKEventTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EVTEventCatalogueCategory_EVTEventTypes");
            });

            modelBuilder.Entity<EVTEventCategoryGroup>(entity =>
            {
                entity.HasKey(e => e.EventCategoryGroupId)
                    .HasName("PK_DLSDelayCategoryGroups");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<EVTEventType>(entity =>
            {
                entity.HasKey(e => e.EventTypeId)
                    .HasName("PK_EventTypes");

                entity.Property(e => e.EventTypeId).ValueGeneratedNever();

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKParentEvenType)
                    .WithMany(p => p.InverseFKParentEvenType)
                    .HasForeignKey(d => d.FKParentEvenTypeId)
                    .HasConstraintName("FK_EVTEventTypes_EVTEventTypes");
            });

            modelBuilder.Entity<EVTShiftCalendar>(entity =>
            {
                entity.HasKey(e => e.ShiftCalendarId)
                    .HasName("PK_PEShiftCalendar");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.FKCrew)
                    .WithMany(p => p.EVTShiftCalendars)
                    .HasForeignKey(d => d.FKCrewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PEShiftCalendar_PECrews");

                entity.HasOne(d => d.FKDaysOfYear)
                    .WithMany(p => p.EVTShiftCalendars)
                    .HasForeignKey(d => d.FKDaysOfYearId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShiftCalendar_DaysOfYear");

                entity.HasOne(d => d.FKShiftDefinition)
                    .WithMany(p => p.EVTShiftCalendars)
                    .HasForeignKey(d => d.FKShiftDefinitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PEShiftCalendar_PEShiftDefinitions");
            });

            modelBuilder.Entity<EVTShiftCrewPattern>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKCrew)
                    .WithMany(p => p.EVTShiftCrewPatterns)
                    .HasForeignKey(d => d.FKCrewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EVTShiftCalendar_EVTCrews");

                entity.HasOne(d => d.FKShiftDefinition)
                    .WithMany(p => p.EVTShiftCrewPatterns)
                    .HasForeignKey(d => d.FKShiftDefinitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EVTShiftCrewPattern_EVTShiftDefinitions");
            });

            modelBuilder.Entity<EVTShiftDefinition>(entity =>
            {
                entity.HasKey(e => e.ShiftDefinitionId)
                    .HasName("PK_ShiftDefinitions");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.FKShiftLayoutId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.FKShiftLayout)
                    .WithMany(p => p.EVTShiftDefinitions)
                    .HasForeignKey(d => d.FKShiftLayoutId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EVTShiftDefinitions_EVTShiftLayouts");
            });

            modelBuilder.Entity<EVTShiftLayout>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<EVTTrigger>(entity =>
            {
                entity.HasKey(e => e.TriggerId)
                    .HasName("PK_MVHTriggers");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<EVTTriggersFeature>(entity =>
            {
                entity.HasKey(e => e.TriggersFeatureId)
                    .HasName("PK_MVHTriggersFeatures");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.OrderSeq).HasDefaultValueSql("((1))");

                entity.Property(e => e.PassNo).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.FKFeature)
                    .WithMany(p => p.EVTTriggersFeatures)
                    .HasForeignKey(d => d.FKFeatureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MVHTriggersFeatures_MVHFeatures");

                entity.HasOne(d => d.FKTrigger)
                    .WithMany(p => p.EVTTriggersFeatures)
                    .HasForeignKey(d => d.FKTriggerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MVHTriggersFeatures_MVHTriggers");
            });

            modelBuilder.Entity<HMIRefreshKey>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<HMIUserConfiguration>(entity =>
            {
                entity.HasKey(e => e.ConfigurationId)
                    .HasName("PK_UserComparisonSchemes");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<HMIWidget>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<HMIWidgetConfiguration>(entity =>
            {
                entity.HasKey(e => e.WidgetConfigurationId)
                    .HasName("PK_PEWidgetConfigurations");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.FKWidget)
                    .WithMany(p => p.HMIWidgetConfigurations)
                    .HasForeignKey(d => d.FKWidgetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HMIWidgetConfigurations_HMIWidgets");
            });

            modelBuilder.Entity<L1ABypassConfiguration>(entity =>
            {
                entity.HasKey(e => e.BypassConfigurationId)
                    .HasName("PK_BypassConfigurations");

                entity.Property(e => e.OpcServerName).HasDefaultValueSql("(N' ')");

                entity.HasOne(d => d.FKBypassType)
                    .WithMany(p => p.L1ABypassConfigurations)
                    .HasForeignKey(d => d.FKBypassTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BypassConfigurations_BypassTypes");
            });

            modelBuilder.Entity<L1ABypassInstance>(entity =>
            {
                entity.HasKey(e => e.BypassInstanceId)
                    .HasName("PK_BypassInstances");

                entity.HasOne(d => d.FKBypassConfiguration)
                    .WithMany(p => p.L1ABypassInstances)
                    .HasForeignKey(d => d.FKBypassConfigurationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BypassInstances_BypassConfigurations");
            });

            modelBuilder.Entity<L1ABypassType>(entity =>
            {
                entity.HasKey(e => e.BypassTypeId)
                    .HasName("PK_BypassTypes");
            });

            modelBuilder.Entity<L1ARaisedBypass>(entity =>
            {
                entity.HasKey(e => e.RaisedBypassId)
                    .HasName("PK_RaisedBypasses");

                entity.Property(e => e.OpcServerName).HasDefaultValueSql("(N' ')");
            });

            modelBuilder.Entity<LTRReportsConfiguration>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<MNTCrewMember>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKCrew)
                    .WithMany(p => p.MNTCrewMembers)
                    .HasForeignKey(d => d.FKCrewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MNTCrewMembers_Crews");

                entity.HasOne(d => d.FKMember)
                    .WithMany(p => p.MNTCrewMembers)
                    .HasForeignKey(d => d.FKMemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MNTCrewMembers_MNTMembers");
            });

            modelBuilder.Entity<MNTEquipment>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.FKAsset)
                    .WithMany(p => p.MNTEquipments)
                    .HasForeignKey(d => d.FKAssetId)
                    .HasConstraintName("FK_MNTEquipments_MVHAssets");

                entity.HasOne(d => d.FKEquipmentGroup)
                    .WithMany(p => p.MNTEquipments)
                    .HasForeignKey(d => d.FKEquipmentGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MNTEquipments_MNTEquipmentGroups");

                entity.HasOne(d => d.FKEquipmentSupplier)
                    .WithMany(p => p.MNTEquipments)
                    .HasForeignKey(d => d.FKEquipmentSupplierId)
                    .HasConstraintName("FK_MNTEquipments_MNTEquipmentSuppliers");

                entity.HasOne(d => d.FKParentEquipment)
                    .WithMany(p => p.InverseFKParentEquipment)
                    .HasForeignKey(d => d.FKParentEquipmentId)
                    .HasConstraintName("FK_MNTEquipments_MNTEquipments");
            });

            modelBuilder.Entity<MNTEquipmentGroup>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKParentEquipmentGroup)
                    .WithMany(p => p.InverseFKParentEquipmentGroup)
                    .HasForeignKey(d => d.FKParentEquipmentGroupId)
                    .HasConstraintName("FK_MNTEquipmentGroups_MNTEquipmentGroups");
            });

            modelBuilder.Entity<MNTEquipmentHistory>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKEquipment)
                    .WithMany(p => p.MNTEquipmentHistories)
                    .HasForeignKey(d => d.FKEquipmentId)
                    .HasConstraintName("FK_MNTEquipmentHistory_MNTEquipments");
            });

            modelBuilder.Entity<MNTEquipmentSupplier>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<MNTMaintenanceAction>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.MaintenanceActionId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.FKMaintenanceSchedule)
                    .WithMany()
                    .HasForeignKey(d => d.FKMaintenanceScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MNTMaintenanceActions_MNTMaintenanceSchedules");

                entity.HasOne(d => d.FKMember)
                    .WithMany()
                    .HasForeignKey(d => d.FKMemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MNTMaintenanceActions_MNTMembers");
            });

            modelBuilder.Entity<MNTMaintenanceSchedule>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKEquipment)
                    .WithMany(p => p.MNTMaintenanceSchedules)
                    .HasForeignKey(d => d.FKEquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MNTMaintenanceSchedules_MNTEquipments");
            });

            modelBuilder.Entity<MNTMember>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKMemberRole)
                    .WithMany(p => p.MNTMembers)
                    .HasForeignKey(d => d.FKMemberRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MNTMembers_MNTMemberRoles");
            });

            modelBuilder.Entity<MNTMemberRole>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<MVHAsset>(entity =>
            {
                entity.HasKey(e => e.AssetId)
                    .HasName("PK_Assets");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsTrackingPoint).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.FKAssetType)
                    .WithMany(p => p.MVHAssets)
                    .HasForeignKey(d => d.FKAssetTypeId)
                    .HasConstraintName("FK_MVHAssets_MVHAssetTypes");

                entity.HasOne(d => d.FKParentAsset)
                    .WithMany(p => p.InverseFKParentAsset)
                    .HasForeignKey(d => d.FKParentAssetId)
                    .HasConstraintName("FK_Assets_ParentAsset");
            });

            modelBuilder.Entity<MVHAssetFeatureTemplate>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKAssetTemplate)
                    .WithMany(p => p.MVHAssetFeatureTemplates)
                    .HasForeignKey(d => d.FKAssetTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MVHAssetFeatureTemplates_MVHAssetTemplates");

                entity.HasOne(d => d.FKFeatureTemplate)
                    .WithMany(p => p.MVHAssetFeatureTemplates)
                    .HasForeignKey(d => d.FKFeatureTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MVHAssetFeatureTemplates_MVHFeatureTemplates");
            });

            modelBuilder.Entity<MVHAssetLayout>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKLayout)
                    .WithMany(p => p.MVHAssetLayouts)
                    .HasForeignKey(d => d.FKLayoutId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MVHAssetLayouts_MVHLayouts");

                entity.HasOne(d => d.FKNextAsset)
                    .WithMany(p => p.MVHAssetLayoutFKNextAssets)
                    .HasForeignKey(d => d.FKNextAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MVHAssetLayouts_MVHAssets1");

                entity.HasOne(d => d.FKPrevAsset)
                    .WithMany(p => p.MVHAssetLayoutFKPrevAssets)
                    .HasForeignKey(d => d.FKPrevAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MVHAssetLayouts_MVHAssets");
            });

            modelBuilder.Entity<MVHAssetTemplate>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<MVHAssetType>(entity =>
            {
                entity.HasKey(e => e.AssetTypeId)
                    .HasName("PK_AssetTypes");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<MVHAssetsLocation>(entity =>
            {
                entity.Property(e => e.FKAssetId).ValueGeneratedNever();

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.LayersMaxNumber).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.FKAsset)
                    .WithOne(p => p.MVHAssetsLocation)
                    .HasForeignKey<MVHAssetsLocation>(d => d.FKAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MVHAssetsLocation_MVHAssets");
            });

            modelBuilder.Entity<MVHFeature>(entity =>
            {
                entity.HasKey(e => e.FeatureId)
                    .HasName("PK_MVFeatures");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsConsumptionPoint).HasComputedColumnSql("(isnull(CONVERT([bit],case when [EnumFeatureType]>=(280) AND [EnumFeatureType]<=(299) then (1) else (0) end),(0)))", false);

                entity.Property(e => e.IsDigital).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsMaterialRelated).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsMeasurementPoint).HasComputedColumnSql("(isnull(CONVERT([bit],case when [EnumFeatureType]>=(200) AND [EnumFeatureType]<=(299) then (1) else (0) end),(0)))", false);

                entity.Property(e => e.IsTrackingPoint).HasComputedColumnSql("(isnull(CONVERT([bit],case when [EnumFeatureType]>=(100) AND [EnumFeatureType]<=(199) then (1) else (0) end),(0)))", false);

                entity.HasOne(d => d.FKAsset)
                    .WithMany(p => p.MVHFeatures)
                    .HasForeignKey(d => d.FKAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MVHFeatures_MVHAssets");

                entity.HasOne(d => d.FKDataType)
                    .WithMany(p => p.MVHFeatures)
                    .HasForeignKey(d => d.FKDataTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MVHFeatures_DataTypes");

                entity.HasOne(d => d.FKParentFeature)
                    .WithMany(p => p.InverseFKParentFeature)
                    .HasForeignKey(d => d.FKParentFeatureId)
                    .HasConstraintName("FK_MVHFeatures_MVHFeatures");
            });

            modelBuilder.Entity<MVHFeatureCalculated>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKFeature)
                    .WithMany(p => p.MVHFeatureCalculatedFKFeatures)
                    .HasForeignKey(d => d.FKFeatureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MVHFeatureCalculated_MVHFeatures");

                entity.HasOne(d => d.FKFeatureId_1Navigation)
                    .WithMany(p => p.MVHFeatureCalculatedFKFeatureId_1Navigations)
                    .HasForeignKey(d => d.FKFeatureId_1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MVHFeatureCalculated_MVHFeatures_1");

                entity.HasOne(d => d.FKFeatureId_2Navigation)
                    .WithMany(p => p.MVHFeatureCalculatedFKFeatureId_2Navigations)
                    .HasForeignKey(d => d.FKFeatureId_2)
                    .HasConstraintName("FK_MVHFeatureCalculated_MVHFeatures_2");
            });

            modelBuilder.Entity<MVHFeatureCustom>(entity =>
            {
                entity.HasOne(d => d.FKFeature)
                    .WithMany(p => p.MVHFeatureCustoms)
                    .HasForeignKey(d => d.FKFeatureId)
                    .HasConstraintName("FK_MVHFeatureCustoms_MVHFeatures");
            });

            modelBuilder.Entity<MVHFeatureTemplate>(entity =>
            {
                entity.HasKey(e => e.FeatureTemplateId)
                    .HasName("PK_MVHFeatureTemplatess");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKDataType)
                    .WithMany(p => p.MVHFeatureTemplates)
                    .HasForeignKey(d => d.FKDataTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FeatureTemplates_DataTypes");
            });

            modelBuilder.Entity<MVHLayout>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<MVHMeasurement>(entity =>
            {
                entity.HasKey(e => e.MeasurementId)
                    .HasName("PK_MVMeasurements");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.CreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsValid).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.FKFeature)
                    .WithMany(p => p.MVHMeasurements)
                    .HasForeignKey(d => d.FKFeatureId)
                    .HasConstraintName("FK_MVHMeasurements_MVHFeatures");

                entity.HasOne(d => d.FKRawMaterial)
                    .WithMany(p => p.MVHMeasurements)
                    .HasForeignKey(d => d.FKRawMaterialId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MVHMeasurements_MVHRawMaterials");
            });

            modelBuilder.Entity<MVHSample>(entity =>
            {
                entity.HasKey(e => e.SampleId)
                    .HasName("PK_MVSamples");

                entity.Property(e => e.IsValid).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.FKMeasurement)
                    .WithMany(p => p.MVHSamples)
                    .HasForeignKey(d => d.FKMeasurementId)
                    .HasConstraintName("FK_MVSamples_MVMeasurements");
            });

            modelBuilder.Entity<PPLSchedule>(entity =>
            {
                entity.HasKey(e => e.ScheduleId)
                    .HasName("PK_PPLScheduleItems");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKWorkOrder)
                    .WithOne(p => p.PPLSchedule)
                    .HasForeignKey<PPLSchedule>(d => d.FKWorkOrderId)
                    .HasConstraintName("FK_PPLSchedules_PRMWorkOrders1");
            });

            modelBuilder.Entity<PRFKPIDefinition>(entity =>
            {
                entity.HasKey(e => e.KPIDefinitionId)
                    .HasName("PK_KPIDefinitionId");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.EnumGaugeDirection).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<PRFKPIValue>(entity =>
            {
                entity.HasKey(e => e.KPIValueId)
                    .HasName("PK_KPIValues");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.KPITime).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.FKKPIDefinition)
                    .WithMany(p => p.PRFKPIValues)
                    .HasForeignKey(d => d.FKKPIDefinitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKKPIDefinitionId");

                entity.HasOne(d => d.FKShiftCalendar)
                    .WithMany(p => p.PRFKPIValues)
                    .HasForeignKey(d => d.FKShiftCalendarId)
                    .HasConstraintName("FK_PRFKPIValues_EVTShiftCalendar");

                entity.HasOne(d => d.FKWorkOrder)
                    .WithMany(p => p.PRFKPIValues)
                    .HasForeignKey(d => d.FKWorkOrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKWorkOrderId");
            });

            modelBuilder.Entity<PRMCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK_Customers");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<PRMHeat>(entity =>
            {
                entity.HasKey(e => e.HeatId)
                    .HasName("PK_Heats");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.HeatCreatedTs).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.FKHeatSupplier)
                    .WithMany(p => p.PRMHeats)
                    .HasForeignKey(d => d.FKHeatSupplierId)
                    .HasConstraintName("FK_PEHeats_PEHeatSuppliers");

                entity.HasOne(d => d.FKSteelgrade)
                    .WithMany(p => p.PRMHeats)
                    .HasForeignKey(d => d.FKSteelgradeId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_PRMHeats_PRMSteelgrades");
            });

            modelBuilder.Entity<PRMHeatChemicalAnalysis>(entity =>
            {
                entity.HasKey(e => e.HeatChemAnalysisId)
                    .HasName("PK_HeatChemAnalysis");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKHeat)
                    .WithMany(p => p.PRMHeatChemicalAnalyses)
                    .HasForeignKey(d => d.FKHeatId)
                    .HasConstraintName("FK_PEHeatChemAnalysis_PEHeats");
            });

            modelBuilder.Entity<PRMHeatSupplier>(entity =>
            {
                entity.HasKey(e => e.HeatSupplierId)
                    .HasName("PK_HeatSuppliers");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<PRMMaterial>(entity =>
            {
                entity.HasKey(e => e.MaterialId)
                    .HasName("PK_Materials");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.MaterialCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SeqNo).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.FKHeat)
                    .WithMany(p => p.PRMMaterials)
                    .HasForeignKey(d => d.FKHeatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRMMaterials_PRMHeats");

                entity.HasOne(d => d.FKMaterialCatalogue)
                    .WithMany(p => p.PRMMaterials)
                    .HasForeignKey(d => d.FKMaterialCatalogueId)
                    .HasConstraintName("FK_PRMMaterials_PRMMaterialCatalogue");

                entity.HasOne(d => d.FKWorkOrder)
                    .WithMany(p => p.PRMMaterials)
                    .HasForeignKey(d => d.FKWorkOrderId)
                    .HasConstraintName("FK_Materials_WorkOrders");
            });

            modelBuilder.Entity<PRMMaterialCatalogue>(entity =>
            {
                entity.HasKey(e => e.MaterialCatalogueId)
                    .HasName("PK_MaterialCatalogueId");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.FKMaterialCatalogueType)
                    .WithMany(p => p.PRMMaterialCatalogues)
                    .HasForeignKey(d => d.FKMaterialCatalogueTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRMMaterialCatalogue_PRMMaterialCatalogueTypes");

                entity.HasOne(d => d.FKShape)
                    .WithMany(p => p.PRMMaterialCatalogues)
                    .HasForeignKey(d => d.FKShapeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRMMaterialCatalogue_PRMShapes");
            });

            modelBuilder.Entity<PRMMaterialCatalogueType>(entity =>
            {
                entity.HasKey(e => e.MaterialCatalogueTypeId)
                    .HasName("PK_MaterialCatalogueTypeId");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<PRMMaterialStep>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.StepCreatedTs).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.FKAsset)
                    .WithMany(p => p.PRMMaterialSteps)
                    .HasForeignKey(d => d.FKAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRMMaterialSteps_MVHAssets");

                entity.HasOne(d => d.FKMaterial)
                    .WithMany(p => p.PRMMaterialSteps)
                    .HasForeignKey(d => d.FKMaterialId)
                    .HasConstraintName("FK_PRMMaterialSteps_PRMMaterials");
            });

            modelBuilder.Entity<PRMProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK_Products");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.ProductCreatedTs).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.FKWorkOrder)
                    .WithMany(p => p.PRMProducts)
                    .HasForeignKey(d => d.FKWorkOrderId)
                    .HasConstraintName("FK_Products_WorkOrders");
            });

            modelBuilder.Entity<PRMProductCatalogue>(entity =>
            {
                entity.HasKey(e => e.ProductCatalogueId)
                    .HasName("PK_ProductCatalogueId");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.StdMetallicYield).HasDefaultValueSql("((1))");

                entity.Property(e => e.StdProductivity).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.FKProductCatalogueType)
                    .WithMany(p => p.PRMProductCatalogues)
                    .HasForeignKey(d => d.FKProductCatalogueTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductCatalogue_ProductTypeId");

                entity.HasOne(d => d.FKShape)
                    .WithMany(p => p.PRMProductCatalogues)
                    .HasForeignKey(d => d.FKShapeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRMProductCatalogue_PRMShapes");
            });

            modelBuilder.Entity<PRMProductCatalogueType>(entity =>
            {
                entity.HasKey(e => e.ProductCatalogueTypeId)
                    .HasName("PK_ProductCatalogueTypeId");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<PRMProductStep>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.StepCreatedTs).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.FKAsset)
                    .WithMany(p => p.PRMProductSteps)
                    .HasForeignKey(d => d.FKAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRMProductSteps_MVHAssets");

                entity.HasOne(d => d.FKProduct)
                    .WithMany(p => p.PRMProductSteps)
                    .HasForeignKey(d => d.FKProductId)
                    .HasConstraintName("FK_PRMProductSteps_PRMProducts");
            });

            modelBuilder.Entity<PRMScrapGroup>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<PRMShape>(entity =>
            {
                entity.HasKey(e => e.ShapeId)
                    .HasName("PK_LRProductShape");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<PRMSteelGroup>(entity =>
            {
                entity.HasKey(e => e.SteelGroupId)
                    .HasName("PK_SteelGroupId");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.WearCoefficient).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<PRMSteelgrade>(entity =>
            {
                entity.HasKey(e => e.SteelgradeId)
                    .HasName("PK_SteelgradeId");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKParentSteelgrade)
                    .WithMany(p => p.InverseFKParentSteelgrade)
                    .HasForeignKey(d => d.FKParentSteelgradeId)
                    .HasConstraintName("FK_Steelgrades_Steelgrades");

                entity.HasOne(d => d.FKScrapGroup)
                    .WithMany(p => p.PRMSteelgrades)
                    .HasForeignKey(d => d.FKScrapGroupId)
                    .HasConstraintName("FK_PRMSteelgrades_PRMScrapGroups");

                entity.HasOne(d => d.FKSteelGroup)
                    .WithMany(p => p.PRMSteelgrades)
                    .HasForeignKey(d => d.FKSteelGroupId)
                    .HasConstraintName("FK_Steelgrades_SteelGroupId");
            });

            modelBuilder.Entity<PRMSteelgradeChemicalComposition>(entity =>
            {
                entity.HasKey(e => e.FKSteelgradeId)
                    .HasName("PK_SteelgradeChemicalCompositions");

                entity.Property(e => e.FKSteelgradeId).ValueGeneratedNever();

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKSteelgrade)
                    .WithOne(p => p.PRMSteelgradeChemicalComposition)
                    .HasForeignKey<PRMSteelgradeChemicalComposition>(d => d.FKSteelgradeId)
                    .HasConstraintName("FK_SteelgradeChemicalCompositions_Steelgrades");
            });

            modelBuilder.Entity<PRMWorkOrder>(entity =>
            {
                entity.HasKey(e => e.WorkOrderId)
                    .HasName("PK_WorkOrders");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.L3NumberOfBillets).HasDefaultValueSql("((1))");

                entity.Property(e => e.ToBeCompletedBeforeTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.WorkOrderCreatedInL3Ts).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.WorkOrderCreatedTs).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.FKCustomer)
                    .WithMany(p => p.PRMWorkOrders)
                    .HasForeignKey(d => d.FKCustomerId)
                    .HasConstraintName("FK_PRMWorkOrders_PRMCustomers");

                entity.HasOne(d => d.FKHeat)
                    .WithMany(p => p.PRMWorkOrders)
                    .HasForeignKey(d => d.FKHeatId)
                    .HasConstraintName("FK_PRMWorkOrders_PRMHeats");

                entity.HasOne(d => d.FKMaterialCatalogue)
                    .WithMany(p => p.PRMWorkOrders)
                    .HasForeignKey(d => d.FKMaterialCatalogueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRMWorkOrders_PRMMaterialCatalogue");

                entity.HasOne(d => d.FKParentWorkOrder)
                    .WithMany(p => p.InverseFKParentWorkOrder)
                    .HasForeignKey(d => d.FKParentWorkOrderId)
                    .HasConstraintName("FK_PRMWorkOrders_PRMWorkOrders");

                entity.HasOne(d => d.FKProductCatalogue)
                    .WithMany(p => p.PRMWorkOrders)
                    .HasForeignKey(d => d.FKProductCatalogueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkOrders_ProductCatalogue");

                entity.HasOne(d => d.FKSteelgrade)
                    .WithMany(p => p.PRMWorkOrders)
                    .HasForeignKey(d => d.FKSteelgradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRMWorkOrders_PRMSteelgrades");
            });

            modelBuilder.Entity<QEAlias>(entity =>
            {
                entity.HasKey(e => e.AliasId)
                    .HasName("PK_Aliases");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<QEAliasValue>(entity =>
            {
                entity.HasKey(e => e.AliasValueId)
                    .HasName("PK_AliasValues");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKAlias)
                    .WithMany(p => p.QEAliasValues)
                    .HasForeignKey(d => d.FKAliasId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QEAliasValues_QEAliases");
            });

            modelBuilder.Entity<QECompensation>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKCompensationType)
                    .WithMany(p => p.QECompensations)
                    .HasForeignKey(d => d.FKCompensationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QECompensation_QECompensationType");

                entity.HasOne(d => d.FKRating)
                    .WithMany(p => p.QECompensations)
                    .HasForeignKey(d => d.FKRatingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QECompensation_QERating");
            });

            modelBuilder.Entity<QECompensationAggregate>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKCompensation)
                    .WithMany(p => p.QECompensationAggregates)
                    .HasForeignKey(d => d.FKCompensationId)
                    .HasConstraintName("FK_QECompensationAggregate_QECompensation");
            });

            modelBuilder.Entity<QECompensationType>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<QELengthSeriesValue>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKMappingValue)
                    .WithMany(p => p.QELengthSeriesValues)
                    .HasForeignKey(d => d.FKMappingValueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QELengthSeriesValue_QEMappingValue");
            });

            modelBuilder.Entity<QEMappingEntry>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<QEMappingValue>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKMappingEntry)
                    .WithMany(p => p.QEMappingValues)
                    .HasForeignKey(d => d.FKMappingEntryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QEMappingValue_QEMappingEntry");

                entity.HasOne(d => d.FKRating)
                    .WithMany(p => p.QEMappingValues)
                    .HasForeignKey(d => d.FKRatingId)
                    .HasConstraintName("FK_QEMappingValue_QERating");

                entity.HasOne(d => d.FKRuleMappingValue)
                    .WithMany(p => p.QEMappingValues)
                    .HasForeignKey(d => d.FKRuleMappingValueId)
                    .HasConstraintName("FK_QEMappingValue_QERuleMappingValue");
            });

            modelBuilder.Entity<QERating>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.RatingCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RatingModified).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<QERootCause>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKRating)
                    .WithMany(p => p.QERootCauses)
                    .HasForeignKey(d => d.FKRatingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QERootCause_QERating");
            });

            modelBuilder.Entity<QERootCauseAggregate>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKRootCause)
                    .WithMany(p => p.QERootCauseAggregates)
                    .HasForeignKey(d => d.FKRootCauseId)
                    .HasConstraintName("FK_QERootCauseAggregate_QERootCause");
            });

            modelBuilder.Entity<QERuleMappingValue>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKRawMaterial)
                    .WithMany(p => p.QERuleMappingValues)
                    .HasForeignKey(d => d.FKRawMaterialId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_QERuleMappingValue_TRKRawMaterials");

                entity.HasOne(d => d.FKTrigger)
                    .WithMany(p => p.QERuleMappingValues)
                    .HasForeignKey(d => d.FKTriggerId)
                    .HasConstraintName("FK_QERuleMappingValue_QETrigger");
            });

            modelBuilder.Entity<QETimeSeriesValue>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKMappingValue)
                    .WithMany(p => p.QETimeSeriesValues)
                    .HasForeignKey(d => d.FKMappingValueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QETimeSeriesValue_QEMappingValue");
            });

            modelBuilder.Entity<QETrigger>(entity =>
            {
                entity.HasKey(e => e.TriggerId)
                    .HasName("PK_TPQCTrigger");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.FKAsset)
                    .WithMany(p => p.QETriggers)
                    .HasForeignKey(d => d.FKAssetId)
                    .HasConstraintName("FK_QETrigger_MVHAssets");
            });

            modelBuilder.Entity<QTYDefect>(entity =>
            {
                entity.HasKey(e => e.DefectId)
                    .HasName("PK_Defects");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.DefectCreatedTs).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.FKAsset)
                    .WithMany(p => p.QTYDefects)
                    .HasForeignKey(d => d.FKAssetId)
                    .HasConstraintName("FK_MVHDefects_MVHAssets");

                entity.HasOne(d => d.FKDefectCatalogue)
                    .WithMany(p => p.QTYDefects)
                    .HasForeignKey(d => d.FKDefectCatalogueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Defects_DefectCatalogue");

                entity.HasOne(d => d.FKProduct)
                    .WithMany(p => p.QTYDefects)
                    .HasForeignKey(d => d.FKProductId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_MVHDefects_PRMProducts");

                entity.HasOne(d => d.FKRawMaterial)
                    .WithMany(p => p.QTYDefects)
                    .HasForeignKey(d => d.FKRawMaterialId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Defects_MVHRawMaterials");
            });

            modelBuilder.Entity<QTYDefectCatalogue>(entity =>
            {
                entity.HasKey(e => e.DefectCatalogueId)
                    .HasName("PK_DefectCatalogue");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.FKDefectCatalogueCategory)
                    .WithMany(p => p.QTYDefectCatalogues)
                    .HasForeignKey(d => d.FKDefectCatalogueCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DefectCatalogue_DefectCatalogueCategory");

                entity.HasOne(d => d.FKParentDefectCatalogue)
                    .WithMany(p => p.InverseFKParentDefectCatalogue)
                    .HasForeignKey(d => d.FKParentDefectCatalogueId)
                    .HasConstraintName("FK_DefectCatalogue_DefectCatalogue");
            });

            modelBuilder.Entity<QTYDefectCatalogueCategory>(entity =>
            {
                entity.HasKey(e => e.DefectCatalogueCategoryId)
                    .HasName("PK_DefectCatalogueCategory");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKDefectCategoryGroup)
                    .WithMany(p => p.QTYDefectCatalogueCategories)
                    .HasForeignKey(d => d.FKDefectCategoryGroupId)
                    .HasConstraintName("FK_MVHDefectCatalogueCategory_MVHDefectCategoryGroups");
            });

            modelBuilder.Entity<QTYDefectCategoryGroup>(entity =>
            {
                entity.HasKey(e => e.DefectCategoryGroupId)
                    .HasName("PK_MVHDefectCategoryGroups");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<QTYQualityInspection>(entity =>
            {
                entity.HasKey(e => e.QualityInspectionId)
                    .HasName("PK_QTYQualityInspections_1");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKMaterial)
                    .WithMany(p => p.QTYQualityInspections)
                    .HasForeignKey(d => d.FKMaterialId)
                    .HasConstraintName("FK_QTYQualityInspections_PRMMaterials");

                entity.HasOne(d => d.FKProduct)
                    .WithMany(p => p.QTYQualityInspections)
                    .HasForeignKey(d => d.FKProductId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_QTYQualityInspections_PRMProducts");

                entity.HasOne(d => d.FKRawMaterial)
                    .WithOne(p => p.QTYQualityInspection)
                    .HasForeignKey<QTYQualityInspection>(d => d.FKRawMaterialId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_QTYQualityInspections_TRKRawMaterials");
            });

            modelBuilder.Entity<RLSCassette>(entity =>
            {
                entity.HasKey(e => e.CassetteId)
                    .HasName("PK_Cassettes");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.Arrangement).HasComment("0 - undefined, 1 - horizontal, 2- veritical, 3- other");

                entity.Property(e => e.EnumCassetteStatus).HasComment("SRC.Core.Constants.CassetteStatus");

                entity.Property(e => e.NumberOfPositions).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.FKCassetteType)
                    .WithMany(p => p.RLSCassettes)
                    .HasForeignKey(d => d.FKCassetteTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cassettes_CassetteTypes");

                entity.HasOne(d => d.FKStand)
                    .WithMany(p => p.RLSCassettes)
                    .HasForeignKey(d => d.FKStandId)
                    .HasConstraintName("FK_Cassettes_Stands");
            });

            modelBuilder.Entity<RLSCassetteType>(entity =>
            {
                entity.HasKey(e => e.CassetteTypeId)
                    .HasName("PK_CassetteTypes");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<RLSGrooveTemplate>(entity =>
            {
                entity.HasKey(e => e.GrooveTemplateId)
                    .HasName("PK_GrooveTemplates");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.Angle1).HasComment("Angle 1");

                entity.Property(e => e.Angle2).HasComment("Angle 2");

                entity.Property(e => e.D1).HasComment("Depth 1");

                entity.Property(e => e.D2).HasComment("Depth 2");

                entity.Property(e => e.Plane).HasComment("m2");

                entity.Property(e => e.R1).HasComment("Radius 1");

                entity.Property(e => e.R2).HasComment("Radius 2");

                entity.Property(e => e.R3).HasComment("Radius 3");

                entity.Property(e => e.W1).HasComment("Width 1");

                entity.Property(e => e.W2).HasComment("Width 2");
            });

            modelBuilder.Entity<RLSRoll>(entity =>
            {
                entity.HasKey(e => e.RollId)
                    .HasName("PK_Rolls");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKRollType)
                    .WithMany(p => p.RLSRolls)
                    .HasForeignKey(d => d.FKRollTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rolls_RollTypes");
            });

            modelBuilder.Entity<RLSRollGroovesHistory>(entity =>
            {
                entity.HasKey(e => e.RollGrooveHistoryId)
                    .HasName("PK_RollGroovesHistory");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.EnumGrooveCondition).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.FKGrooveTemplate)
                    .WithMany(p => p.RLSRollGroovesHistories)
                    .HasForeignKey(d => d.FKGrooveTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RollGrooves_GrooveTemplates");

                entity.HasOne(d => d.FKRoll)
                    .WithMany(p => p.RLSRollGroovesHistories)
                    .HasForeignKey(d => d.FKRollId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RollGroovesHistory_Rolls");

                entity.HasOne(d => d.FKRollSetHistory)
                    .WithMany(p => p.RLSRollGroovesHistories)
                    .HasForeignKey(d => d.FKRollSetHistoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_RollGroovesHistory_RollSetHistory");
            });

            modelBuilder.Entity<RLSRollSet>(entity =>
            {
                entity.HasKey(e => e.RollSetId)
                    .HasName("PK_RollSets");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKBottomRoll)
                    .WithMany(p => p.RLSRollSetFKBottomRolls)
                    .HasForeignKey(d => d.FKBottomRollId)
                    .HasConstraintName("FK_RollSets_RollsBottom");

                entity.HasOne(d => d.FKThirdRoll)
                    .WithMany(p => p.RLSRollSetFKThirdRolls)
                    .HasForeignKey(d => d.FKThirdRollId)
                    .HasConstraintName("FK_RollSets_RollsThird");

                entity.HasOne(d => d.FKUpperRoll)
                    .WithMany(p => p.RLSRollSetFKUpperRolls)
                    .HasForeignKey(d => d.FKUpperRollId)
                    .HasConstraintName("FK_RollSets_RollsUpper");
            });

            modelBuilder.Entity<RLSRollSetHistory>(entity =>
            {
                entity.HasKey(e => e.RollSetHistoryId)
                    .HasName("PK_RollSetHistory");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKCassette)
                    .WithMany(p => p.RLSRollSetHistories)
                    .HasForeignKey(d => d.FKCassetteId)
                    .HasConstraintName("FK_RollSetHistory_Cassettes");

                entity.HasOne(d => d.FKRollSet)
                    .WithMany(p => p.RLSRollSetHistories)
                    .HasForeignKey(d => d.FKRollSetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RollSetHistory_RollSets");
            });

            modelBuilder.Entity<RLSRollType>(entity =>
            {
                entity.HasKey(e => e.RollTypeId)
                    .HasName("PK_RollTypes");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.MatchingRollsetType).HasComment("refers to PE.Core.Constants.RollSetType");
            });

            modelBuilder.Entity<RLSStand>(entity =>
            {
                entity.HasKey(e => e.StandId)
                    .HasName("PK_Stands");

                entity.Property(e => e.StandId).ValueGeneratedNever();

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.IsOnLine).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<STPConfiguration>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.ConfigurationCreatedTs).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<STPInstruction>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKDataType)
                    .WithMany(p => p.STPInstructions)
                    .HasForeignKey(d => d.FKDataTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STPInstructions_DataTypes");
            });

            modelBuilder.Entity<STPIssue>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.Issue).IsFixedLength();
            });

            modelBuilder.Entity<STPLayout>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.Layout).IsFixedLength();
            });

            modelBuilder.Entity<STPParameter>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<STPProductLayout>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKIssue)
                    .WithMany(p => p.STPProductLayouts)
                    .HasForeignKey(d => d.FKIssueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductLayouts_Issues");

                entity.HasOne(d => d.FKLayout)
                    .WithMany(p => p.STPProductLayouts)
                    .HasForeignKey(d => d.FKLayoutId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductLayouts_Layouts");

                entity.HasOne(d => d.FKProductCatalogue)
                    .WithMany(p => p.STPProductLayouts)
                    .HasForeignKey(d => d.FKProductCatalogueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductLayouts_ProductCatalogue");
            });

            modelBuilder.Entity<STPSetup>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.CreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedTs).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.FKSetupType)
                    .WithMany(p => p.STPSetups)
                    .HasForeignKey(d => d.FKSetupTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STPSetups_STPSetupTypes");
            });

            modelBuilder.Entity<STPSetupConfiguration>(entity =>
            {
                entity.HasKey(e => e.SetupConfigurationId)
                    .HasName("PK_STPSetupGroup");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKConfiguration)
                    .WithMany(p => p.STPSetupConfigurations)
                    .HasForeignKey(d => d.FKConfigurationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STPSetupConfigurations_STPConfigurations");

                entity.HasOne(d => d.FKSetup)
                    .WithMany(p => p.STPSetupConfigurations)
                    .HasForeignKey(d => d.FKSetupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STPSetupConfigurations_STPSetups");
            });

            modelBuilder.Entity<STPSetupInstruction>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKSetup)
                    .WithMany(p => p.STPSetupInstructions)
                    .HasForeignKey(d => d.FKSetupId)
                    .HasConstraintName("FK_STPSetupInstructions_STPSetups");

                entity.HasOne(d => d.FKSetupTypeInstruction)
                    .WithMany(p => p.STPSetupInstructions)
                    .HasForeignKey(d => d.FKSetupTypeInstructionId)
                    .HasConstraintName("FK_STPSetupInstructions_STPSetupTypeInstructions");
            });

            modelBuilder.Entity<STPSetupParameter>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.IsRequired).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.FKParameter)
                    .WithMany(p => p.STPSetupParameters)
                    .HasForeignKey(d => d.FKParameterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STPSetupParameters_STPParameters");

                entity.HasOne(d => d.FKSetup)
                    .WithMany(p => p.STPSetupParameters)
                    .HasForeignKey(d => d.FKSetupId)
                    .HasConstraintName("FK_STPSetupParameters_STPSetups");
            });

            modelBuilder.Entity<STPSetupSent>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKSetup)
                    .WithMany(p => p.STPSetupSents)
                    .HasForeignKey(d => d.FKSetupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SetupSent_STPSetups");

                entity.HasOne(d => d.FKWorkOrder)
                    .WithMany(p => p.STPSetupSents)
                    .HasForeignKey(d => d.FKWorkOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STPSetupSent_PRMWorkOrders");
            });

            modelBuilder.Entity<STPSetupType>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.IsRequired).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<STPSetupTypeInstruction>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.IsRequired).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsSentToL1).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.FKAsset)
                    .WithMany(p => p.STPSetupTypeInstructions)
                    .HasForeignKey(d => d.FKAssetId)
                    .HasConstraintName("FK_STPSetupTypeInstructions_MVHAssets");

                entity.HasOne(d => d.FKInstruction)
                    .WithMany(p => p.STPSetupTypeInstructions)
                    .HasForeignKey(d => d.FKInstructionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STPSetupTypeInstructions_STPInstructions");

                entity.HasOne(d => d.FKSetupType)
                    .WithMany(p => p.STPSetupTypeInstructions)
                    .HasForeignKey(d => d.FKSetupTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STPSetupTypeInstructions_STPSetupTypes");
            });

            modelBuilder.Entity<STPSetupTypeParameter>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKParameter)
                    .WithMany(p => p.STPSetupTypeParameters)
                    .HasForeignKey(d => d.FKParameterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STPSetupTypeParameters_STPParameters");

                entity.HasOne(d => d.FKSetupType)
                    .WithMany(p => p.STPSetupTypeParameters)
                    .HasForeignKey(d => d.FKSetupTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STPSetupTypeParameters_STPSetupTypes");
            });

            modelBuilder.Entity<STPSetupWorkOrder>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.CalculatedTs).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.FKSetup)
                    .WithMany(p => p.STPSetupWorkOrders)
                    .HasForeignKey(d => d.FKSetupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SetupWorkOrders_STPSetups");

                entity.HasOne(d => d.FKWorkOrder)
                    .WithMany(p => p.STPSetupWorkOrders)
                    .HasForeignKey(d => d.FKWorkOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STPSetupWorkOrders_PRMWorkOrders");
            });

            modelBuilder.Entity<TRKLayerRawMaterialRelation>(entity =>
            {
                entity.HasKey(e => new { e.ParentLayerRawMaterialId, e.ChildLayerRawMaterialId });

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.ChildLayerRawMaterial)
                    .WithMany(p => p.TRKLayerRawMaterialRelationChildLayerRawMaterials)
                    .HasForeignKey(d => d.ChildLayerRawMaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TRKLayerRawMaterialRelations_TRKRawMaterials1");

                entity.HasOne(d => d.ParentLayerRawMaterial)
                    .WithMany(p => p.TRKLayerRawMaterialRelationParentLayerRawMaterials)
                    .HasForeignKey(d => d.ParentLayerRawMaterialId)
                    .HasConstraintName("FK_TRKLayerRawMaterialRelations_TRKRawMaterials");
            });

            modelBuilder.Entity<TRKLayerRelation>(entity =>
            {
                entity.HasKey(e => new { e.ParentLayerId, e.ChildLayerId });

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.ChildLayer)
                    .WithMany(p => p.TRKLayerRelationChildLayers)
                    .HasForeignKey(d => d.ChildLayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TRKLayerRelations_TRKRawMaterials1");

                entity.HasOne(d => d.ParentLayer)
                    .WithMany(p => p.TRKLayerRelationParentLayers)
                    .HasForeignKey(d => d.ParentLayerId)
                    .HasConstraintName("FK_TRKLayerRelations_TRKRawMaterials");
            });

            modelBuilder.Entity<TRKMillControlData>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            modelBuilder.Entity<TRKRawMaterial>(entity =>
            {
                entity.HasKey(e => e.RawMaterialId)
                    .HasName("PK_RawMaterialId");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.RawMaterialCreatedTs).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.FKLastAsset)
                    .WithMany(p => p.TRKRawMaterialFKLastAssets)
                    .HasForeignKey(d => d.FKLastAssetId)
                    .HasConstraintName("FK_TRKRawMaterials_MVHAssets_LastAsset");

                entity.HasOne(d => d.FKMaterial)
                    .WithMany(p => p.TRKRawMaterials)
                    .HasForeignKey(d => d.FKMaterialId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_TRKRawMaterials_PRMMaterials");

                entity.HasOne(d => d.FKProduct)
                    .WithMany(p => p.TRKRawMaterials)
                    .HasForeignKey(d => d.FKProductId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_TRKRawMaterials_PRMProducts");

                entity.HasOne(d => d.FKScrapAsset)
                    .WithMany(p => p.TRKRawMaterialFKScrapAssets)
                    .HasForeignKey(d => d.FKScrapAssetId)
                    .HasConstraintName("FK_TRKRawMaterials_MVHAssets");

                entity.HasOne(d => d.FKShiftCalendar)
                    .WithMany(p => p.TRKRawMaterials)
                    .HasForeignKey(d => d.FKShiftCalendarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TRKRawMaterials_EVTShiftCalendar");
            });

            modelBuilder.Entity<TRKRawMaterialLocation>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.OrderSeq).HasDefaultValueSql("((1))");

                entity.Property(e => e.PositionSeq).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.FKAsset)
                    .WithMany(p => p.TRKRawMaterialLocationFKAssets)
                    .HasForeignKey(d => d.FKAssetId)
                    .HasConstraintName("FK_TRKRawMaterialLocations_MVHAssets");

                entity.HasOne(d => d.FKCtrAsset)
                    .WithMany(p => p.TRKRawMaterialLocationFKCtrAssets)
                    .HasForeignKey(d => d.FKCtrAssetId)
                    .HasConstraintName("FK_TRKRawMaterialLocations_MVHAssets1");

                entity.HasOne(d => d.FKRawMaterial)
                    .WithMany(p => p.TRKRawMaterialLocations)
                    .HasForeignKey(d => d.FKRawMaterialId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_TRKRawMaterialLocations_MVHRawMaterials");
            });

            modelBuilder.Entity<TRKRawMaterialRelation>(entity =>
            {
                entity.HasKey(e => new { e.ParentRawMaterialId, e.ChildRawMaterialId });

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.ChildRawMaterial)
                    .WithMany(p => p.TRKRawMaterialRelationChildRawMaterials)
                    .HasForeignKey(d => d.ChildRawMaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TRKRawMaterialRelations_TRKRawMaterials1");

                entity.HasOne(d => d.ParentRawMaterial)
                    .WithMany(p => p.TRKRawMaterialRelationParentRawMaterials)
                    .HasForeignKey(d => d.ParentRawMaterialId)
                    .HasConstraintName("FK_TRKRawMaterialRelations_TRKRawMaterials");
            });

            modelBuilder.Entity<TRKRawMaterialsCut>(entity =>
            {
                entity.HasKey(e => e.RawMaterialCutId)
                    .HasName("PK_RawMaterialCutId");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.CuttingTs).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.FKAsset)
                    .WithMany(p => p.TRKRawMaterialsCuts)
                    .HasForeignKey(d => d.FKAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MVHRawMaterialsCuts_MVHAssets");

                entity.HasOne(d => d.FKRawMaterial)
                    .WithMany(p => p.TRKRawMaterialsCuts)
                    .HasForeignKey(d => d.FKRawMaterialId)
                    .HasConstraintName("FK_PERawMaterialsCut_TRKRawMaterials");
            });

            modelBuilder.Entity<TRKRawMaterialsInFurnace>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.ChargingTs).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.FKRawMaterial)
                    .WithMany(p => p.TRKRawMaterialsInFurnaces)
                    .HasForeignKey(d => d.FKRawMaterialId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_TRKRawMaterialsInFurnace_MVHRawMaterials");
            });

            modelBuilder.Entity<TRKRawMaterialsStep>(entity =>
            {
                entity.HasKey(e => e.RawMaterialStepId)
                    .HasName("PK_RawMaterialStepId");

                entity.HasComment("PE.Core.Constants.MaterialShapeType");

                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.PassNo).HasDefaultValueSql("((1))");

                entity.Property(e => e.ProcessingStepTs).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.FKAsset)
                    .WithMany(p => p.TRKRawMaterialsSteps)
                    .HasForeignKey(d => d.FKAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MVHRawMaterialsSteps_MVHAssets");

                entity.HasOne(d => d.FKRawMaterial)
                    .WithMany(p => p.TRKRawMaterialsSteps)
                    .HasForeignKey(d => d.FKRawMaterialId)
                    .HasConstraintName("FK_PERawMaterialsStep_PERawMaterialsIndex");
            });

            modelBuilder.Entity<TRKTrackingInstruction>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.Property(e => e.ChannelId).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsProcessedDuringAdjustment).HasDefaultValueSql("((1))");

                entity.Property(e => e.SeqNo).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.FKAreaAsset)
                    .WithMany(p => p.TRKTrackingInstructionFKAreaAssets)
                    .HasForeignKey(d => d.FKAreaAssetId)
                    .HasConstraintName("FK_TRKTrackingInstructions_MVHAssets1");

                entity.HasOne(d => d.FKFeature)
                    .WithMany(p => p.TRKTrackingInstructions)
                    .HasForeignKey(d => d.FKFeatureId)
                    .HasConstraintName("FK_TRKTrackingInstructions_MVHFeatures");

                entity.HasOne(d => d.FKParentTrackingInstruction)
                    .WithMany(p => p.InverseFKParentTrackingInstruction)
                    .HasForeignKey(d => d.FKParentTrackingInstructionId)
                    .HasConstraintName("FK_TRKTrackingInstructions_TRKTrackingInstructions");

                entity.HasOne(d => d.FKPointAsset)
                    .WithMany(p => p.TRKTrackingInstructionFKPointAssets)
                    .HasForeignKey(d => d.FKPointAssetId)
                    .HasConstraintName("FK_TRKTrackingInstructions_MVHAssets2");
            });

            modelBuilder.Entity<ZPCZebraPrinter>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");

                entity.HasOne(d => d.FKAsset)
                    .WithMany(p => p.ZPCZebraPrinters)
                    .HasForeignKey(d => d.FKAssetId)
                    .HasConstraintName("FK_ZPCZebraPrinters_MVHAssets");
            });

            modelBuilder.Entity<ZPCZebraTemplate>(entity =>
            {
                entity.Property(e => e.AUDCreatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDLastUpdatedTs).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AUDUpdatedBy).HasDefaultValueSql("(app_name())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
