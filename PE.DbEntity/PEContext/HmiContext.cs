using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PE.DbEntity.HmiModels;

namespace PE.DbEntity.PEContext
{
    public partial class HmiContext : DbContext
    {
        public HmiContext()
        {
        }

        public HmiContext(DbContextOptions<HmiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<V_AS_Defect> V_AS_Defects { get; set; }
        public virtual DbSet<V_AS_Delay> V_AS_Delays { get; set; }
        public virtual DbSet<V_AS_WorkOrder> V_AS_WorkOrders { get; set; }
        public virtual DbSet<V_ActualRollsOnStandsDiameter> V_ActualRollsOnStandsDiameters { get; set; }
        public virtual DbSet<V_ActualStandConfiguration> V_ActualStandConfigurations { get; set; }
        public virtual DbSet<V_Alarm> V_Alarms { get; set; }
        public virtual DbSet<V_AreaRawMaterialMeasurement> V_AreaRawMaterialMeasurements { get; set; }
        public virtual DbSet<V_Asset> V_Assets { get; set; }
        public virtual DbSet<V_AssetsLocationOverview> V_AssetsLocationOverviews { get; set; }
        public virtual DbSet<V_BundleSearchGrid> V_BundleSearchGrids { get; set; }
        public virtual DbSet<V_CassettesInStand> V_CassettesInStands { get; set; }
        public virtual DbSet<V_CassettesOverview> V_CassettesOverviews { get; set; }
        public virtual DbSet<V_DefectsSummary> V_DefectsSummaries { get; set; }
        public virtual DbSet<V_DelayOverview> V_DelayOverviews { get; set; }
        public virtual DbSet<V_Enum> V_Enums { get; set; }
        public virtual DbSet<V_Event> V_Events { get; set; }
        public virtual DbSet<V_EventCatalogueSearchGrid> V_EventCatalogueSearchGrids { get; set; }
        public virtual DbSet<V_EventCategorySearchGrid> V_EventCategorySearchGrids { get; set; }
        public virtual DbSet<V_EventGroupSearchGrid> V_EventGroupSearchGrids { get; set; }
        public virtual DbSet<V_EventTypeSearchGrid> V_EventTypeSearchGrids { get; set; }
        public virtual DbSet<V_EventsStructureSearchGrid> V_EventsStructureSearchGrids { get; set; }
        public virtual DbSet<V_Feature> V_Features { get; set; }
        public virtual DbSet<V_FeatureCustom> V_FeatureCustoms { get; set; }
        public virtual DbSet<V_GroovesView4Accumulation> V_GroovesView4Accumulations { get; set; }
        public virtual DbSet<V_Heat> V_Heats { get; set; }
        public virtual DbSet<V_HeatsByGroupOnYard> V_HeatsByGroupOnYards { get; set; }
        public virtual DbSet<V_HeatsOnYard> V_HeatsOnYards { get; set; }
        public virtual DbSet<V_KPIValue> V_KPIValues { get; set; }
        public virtual DbSet<V_L1L3MaterialAssignment> V_L1L3MaterialAssignments { get; set; }
        public virtual DbSet<V_LayerSearchGrid> V_LayerSearchGrids { get; set; }
        public virtual DbSet<V_MaterialLocation> V_MaterialLocations { get; set; }
        public virtual DbSet<V_MaterialSearchGrid> V_MaterialSearchGrids { get; set; }
        public virtual DbSet<V_Measurement> V_Measurements { get; set; }
        public virtual DbSet<V_MeasurementSearchGrid> V_MeasurementSearchGrids { get; set; }
        public virtual DbSet<V_PassChangeDataActual> V_PassChangeDataActuals { get; set; }
        public virtual DbSet<V_Product> V_Products { get; set; }
        public virtual DbSet<V_ProductCatalogue> V_ProductCatalogues { get; set; }
        public virtual DbSet<V_ProductOverview> V_ProductOverviews { get; set; }
        public virtual DbSet<V_ProductSearchGrid> V_ProductSearchGrids { get; set; }
        public virtual DbSet<V_ProductsOnYard> V_ProductsOnYards { get; set; }
        public virtual DbSet<V_QERating> V_QERatings { get; set; }
        public virtual DbSet<V_QualityBundlesInspectionSearchGrid> V_QualityBundlesInspectionSearchGrids { get; set; }
        public virtual DbSet<V_QualityExpertSearchGrid> V_QualityExpertSearchGrids { get; set; }
        public virtual DbSet<V_QualityInspectionSearchGrid> V_QualityInspectionSearchGrids { get; set; }
        public virtual DbSet<V_RawMaterialHistory> V_RawMaterialHistories { get; set; }
        public virtual DbSet<V_RawMaterialInFurnace> V_RawMaterialInFurnaces { get; set; }
        public virtual DbSet<V_RawMaterialLabel> V_RawMaterialLabels { get; set; }
        public virtual DbSet<V_RawMaterialLocation> V_RawMaterialLocations { get; set; }
        public virtual DbSet<V_RawMaterialMeasurement> V_RawMaterialMeasurements { get; set; }
        public virtual DbSet<V_RawMaterialOverview> V_RawMaterialOverviews { get; set; }
        public virtual DbSet<V_RawMaterialSearchGrid> V_RawMaterialSearchGrids { get; set; }
        public virtual DbSet<V_Roll> V_Rolls { get; set; }
        public virtual DbSet<V_RollHistory> V_RollHistories { get; set; }
        public virtual DbSet<V_RollHistoryPerGroove> V_RollHistoryPerGrooves { get; set; }
        public virtual DbSet<V_RollSetOverview> V_RollSetOverviews { get; set; }
        public virtual DbSet<V_RollsetInCassette> V_RollsetInCassettes { get; set; }
        public virtual DbSet<V_ScheduleSummary> V_ScheduleSummaries { get; set; }
        public virtual DbSet<V_SetupConfiguration> V_SetupConfigurations { get; set; }
        public virtual DbSet<V_SetupConfigurationSearchGrid> V_SetupConfigurationSearchGrids { get; set; }
        public virtual DbSet<V_SetupParameter> V_SetupParameters { get; set; }
        public virtual DbSet<V_ShiftCalendar> V_ShiftCalendars { get; set; }
        public virtual DbSet<V_TimeLine> V_TimeLines { get; set; }
        public virtual DbSet<V_TrackingInstruction> V_TrackingInstructions { get; set; }
        public virtual DbSet<V_WidgetConfiguration> V_WidgetConfigurations { get; set; }
        public virtual DbSet<V_WorkOrderLocation> V_WorkOrderLocations { get; set; }
        public virtual DbSet<V_WorkOrderSearchGrid> V_WorkOrderSearchGrids { get; set; }
        public virtual DbSet<V_WorkOrderSummary> V_WorkOrderSummaries { get; set; }
        public virtual DbSet<V_WorkOrdersOnMaterialYard> V_WorkOrdersOnMaterialYards { get; set; }
        public virtual DbSet<V_WorkOrdersOnProductYard> V_WorkOrdersOnProductYards { get; set; }
        public virtual DbSet<V_WorkOrdersOnYard> V_WorkOrdersOnYards { get; set; }
        public virtual DbSet<V_WorkOrdersOnYardLocation> V_WorkOrdersOnYardLocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<V_AS_Defect>(entity =>
            {
                entity.ToView("V_AS_Defects", "hmi");
            });

            modelBuilder.Entity<V_AS_Delay>(entity =>
            {
                entity.ToView("V_AS_Delays", "hmi");
            });

            modelBuilder.Entity<V_AS_WorkOrder>(entity =>
            {
                entity.ToView("V_AS_WorkOrders", "hmi");
            });

            modelBuilder.Entity<V_ActualRollsOnStandsDiameter>(entity =>
            {
                entity.ToView("V_ActualRollsOnStandsDiameter", "hmi");
            });

            modelBuilder.Entity<V_ActualStandConfiguration>(entity =>
            {
                entity.ToView("V_ActualStandConfiguration", "hmi");
            });

            modelBuilder.Entity<V_Alarm>(entity =>
            {
                entity.ToView("V_Alarms", "hmi");
            });

            modelBuilder.Entity<V_AreaRawMaterialMeasurement>(entity =>
            {
                entity.ToView("V_AreaRawMaterialMeasurements", "hmi");
            });

            modelBuilder.Entity<V_Asset>(entity =>
            {
                entity.ToView("V_Assets", "hmi");
            });

            modelBuilder.Entity<V_AssetsLocationOverview>(entity =>
            {
                entity.ToView("V_AssetsLocationOverview", "hmi");
            });

            modelBuilder.Entity<V_BundleSearchGrid>(entity =>
            {
                entity.ToView("V_BundleSearchGrid", "hmi");
            });

            modelBuilder.Entity<V_CassettesInStand>(entity =>
            {
                entity.ToView("V_CassettesInStands", "hmi");
            });

            modelBuilder.Entity<V_CassettesOverview>(entity =>
            {
                entity.ToView("V_CassettesOverview", "hmi");
            });

            modelBuilder.Entity<V_DefectsSummary>(entity =>
            {
                entity.ToView("V_DefectsSummary", "hmi");
            });

            modelBuilder.Entity<V_DelayOverview>(entity =>
            {
                entity.ToView("V_DelayOverview", "hmi");
            });

            modelBuilder.Entity<V_Enum>(entity =>
            {
                entity.ToView("V_Enums", "hmi");
            });

            modelBuilder.Entity<V_Event>(entity =>
            {
                entity.ToView("V_Events", "hmi");
            });

            modelBuilder.Entity<V_EventCatalogueSearchGrid>(entity =>
            {
                entity.ToView("V_EventCatalogueSearchGrid", "hmi");
            });

            modelBuilder.Entity<V_EventCategorySearchGrid>(entity =>
            {
                entity.ToView("V_EventCategorySearchGrid", "hmi");
            });

            modelBuilder.Entity<V_EventGroupSearchGrid>(entity =>
            {
                entity.ToView("V_EventGroupSearchGrid", "hmi");

                entity.Property(e => e.EventCategoryGroupId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<V_EventTypeSearchGrid>(entity =>
            {
                entity.ToView("V_EventTypeSearchGrid", "hmi");
            });

            modelBuilder.Entity<V_EventsStructureSearchGrid>(entity =>
            {
                entity.ToView("V_EventsStructureSearchGrid", "hmi");
            });

            modelBuilder.Entity<V_Feature>(entity =>
            {
                entity.ToView("V_Features", "hmi");
            });

            modelBuilder.Entity<V_FeatureCustom>(entity =>
            {
                entity.ToView("V_FeatureCustoms", "hmi");
            });

            modelBuilder.Entity<V_GroovesView4Accumulation>(entity =>
            {
                entity.ToView("V_GroovesView4Accumulation", "hmi");
            });

            modelBuilder.Entity<V_Heat>(entity =>
            {
                entity.ToView("V_Heats", "hmi");
            });

            modelBuilder.Entity<V_HeatsByGroupOnYard>(entity =>
            {
                entity.ToView("V_HeatsByGroupOnYards", "hmi");
            });

            modelBuilder.Entity<V_HeatsOnYard>(entity =>
            {
                entity.ToView("V_HeatsOnYards", "hmi");
            });

            modelBuilder.Entity<V_KPIValue>(entity =>
            {
                entity.ToView("V_KPIValues", "hmi");
            });

            modelBuilder.Entity<V_L1L3MaterialAssignment>(entity =>
            {
                entity.ToView("V_L1L3MaterialAssignment", "hmi");
            });

            modelBuilder.Entity<V_LayerSearchGrid>(entity =>
            {
                entity.ToView("V_LayerSearchGrid", "hmi");

                entity.Property(e => e.RawMaterialId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<V_MaterialLocation>(entity =>
            {
                entity.ToView("V_MaterialLocations", "hmi");
            });

            modelBuilder.Entity<V_MaterialSearchGrid>(entity =>
            {
                entity.ToView("V_MaterialSearchGrid", "hmi");
            });

            modelBuilder.Entity<V_Measurement>(entity =>
            {
                entity.ToView("V_Measurements", "hmi");
            });

            modelBuilder.Entity<V_MeasurementSearchGrid>(entity =>
            {
                entity.ToView("V_MeasurementSearchGrid", "hmi");
            });

            modelBuilder.Entity<V_PassChangeDataActual>(entity =>
            {
                entity.ToView("V_PassChangeDataActual", "hmi");
            });

            modelBuilder.Entity<V_Product>(entity =>
            {
                entity.ToView("V_Products", "hmi");
            });

            modelBuilder.Entity<V_ProductCatalogue>(entity =>
            {
                entity.ToView("V_ProductCatalogue", "hmi");
            });

            modelBuilder.Entity<V_ProductOverview>(entity =>
            {
                entity.ToView("V_ProductOverview", "hmi");
            });

            modelBuilder.Entity<V_ProductSearchGrid>(entity =>
            {
                entity.ToView("V_ProductSearchGrid", "hmi");
            });

            modelBuilder.Entity<V_ProductsOnYard>(entity =>
            {
                entity.ToView("V_ProductsOnYards", "hmi");
            });

            modelBuilder.Entity<V_QERating>(entity =>
            {
                entity.ToView("V_QERating", "hmi");
            });

            modelBuilder.Entity<V_QualityBundlesInspectionSearchGrid>(entity =>
            {
                entity.ToView("V_QualityBundlesInspectionSearchGrid", "hmi");
            });

            modelBuilder.Entity<V_QualityExpertSearchGrid>(entity =>
            {
                entity.ToView("V_QualityExpertSearchGrid", "hmi");
            });

            modelBuilder.Entity<V_QualityInspectionSearchGrid>(entity =>
            {
                entity.ToView("V_QualityInspectionSearchGrid", "hmi");
            });

            modelBuilder.Entity<V_RawMaterialHistory>(entity =>
            {
                entity.ToView("V_RawMaterialHistory", "hmi");
            });

            modelBuilder.Entity<V_RawMaterialInFurnace>(entity =>
            {
                entity.ToView("V_RawMaterialInFurnace", "hmi");
            });

            modelBuilder.Entity<V_RawMaterialLabel>(entity =>
            {
                entity.ToView("V_RawMaterialLabels", "hmi");
            });

            modelBuilder.Entity<V_RawMaterialLocation>(entity =>
            {
                entity.ToView("V_RawMaterialLocations", "hmi");
            });

            modelBuilder.Entity<V_RawMaterialMeasurement>(entity =>
            {
                entity.ToView("V_RawMaterialMeasurements", "hmi");
            });

            modelBuilder.Entity<V_RawMaterialOverview>(entity =>
            {
                entity.ToView("V_RawMaterialOverview", "hmi");
            });

            modelBuilder.Entity<V_RawMaterialSearchGrid>(entity =>
            {
                entity.ToView("V_RawMaterialSearchGrid", "hmi");
            });

            modelBuilder.Entity<V_Roll>(entity =>
            {
                entity.ToView("V_Rolls", "hmi");
            });

            modelBuilder.Entity<V_RollHistory>(entity =>
            {
                entity.ToView("V_RollHistory", "hmi");
            });

            modelBuilder.Entity<V_RollHistoryPerGroove>(entity =>
            {
                entity.ToView("V_RollHistoryPerGroove", "hmi");
            });

            modelBuilder.Entity<V_RollSetOverview>(entity =>
            {
                entity.ToView("V_RollSetOverview", "hmi");
            });

            modelBuilder.Entity<V_RollsetInCassette>(entity =>
            {
                entity.ToView("V_RollsetInCassettes", "hmi");
            });

            modelBuilder.Entity<V_ScheduleSummary>(entity =>
            {
                entity.ToView("V_ScheduleSummary", "hmi");
            });

            modelBuilder.Entity<V_SetupConfiguration>(entity =>
            {
                entity.ToView("V_SetupConfigurations", "hmi");
            });

            modelBuilder.Entity<V_SetupConfigurationSearchGrid>(entity =>
            {
                entity.ToView("V_SetupConfigurationSearchGrid", "hmi");

                entity.Property(e => e.ConfigurationId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<V_SetupParameter>(entity =>
            {
                entity.ToView("V_SetupParameters", "hmi");
            });

            modelBuilder.Entity<V_ShiftCalendar>(entity =>
            {
                entity.ToView("V_ShiftCalendar", "hmi");
            });

            modelBuilder.Entity<V_TimeLine>(entity =>
            {
                entity.ToView("V_TimeLine", "hmi");
            });

            modelBuilder.Entity<V_TrackingInstruction>(entity =>
            {
                entity.ToView("V_TrackingInstructions", "hmi");
            });

            modelBuilder.Entity<V_WidgetConfiguration>(entity =>
            {
                entity.ToView("V_WidgetConfigurations", "hmi");
            });

            modelBuilder.Entity<V_WorkOrderLocation>(entity =>
            {
                entity.ToView("V_WorkOrderLocations", "hmi");
            });

            modelBuilder.Entity<V_WorkOrderSearchGrid>(entity =>
            {
                entity.ToView("V_WorkOrderSearchGrid", "hmi");
            });

            modelBuilder.Entity<V_WorkOrderSummary>(entity =>
            {
                entity.ToView("V_WorkOrderSummary", "hmi");
            });

            modelBuilder.Entity<V_WorkOrdersOnMaterialYard>(entity =>
            {
                entity.ToView("V_WorkOrdersOnMaterialYards", "hmi");
            });

            modelBuilder.Entity<V_WorkOrdersOnProductYard>(entity =>
            {
                entity.ToView("V_WorkOrdersOnProductYards", "hmi");
            });

            modelBuilder.Entity<V_WorkOrdersOnYard>(entity =>
            {
                entity.ToView("V_WorkOrdersOnYards", "hmi");
            });

            modelBuilder.Entity<V_WorkOrdersOnYardLocation>(entity =>
            {
                entity.ToView("V_WorkOrdersOnYardLocations", "hmi");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
