using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace PE.HMIWWW
{
  public static class Bundles
  {
    public static void AddBundles(this IServiceCollection services, string webRootPath)
    {
      services.AddWebOptimizer(pipeline =>
      {
        #region vendors
        pipeline.AddBundle("/bundles/jquery.js", "text/javascript; charset=UTF-8",
            "/lib/jquery/jquery-3.4.1.min.js",
            "/lib/microsoft-signalr/signalr.min.js",
            "/lib/jquery-validation/dist/jquery.validate.min.js",
            "/lib/jquery-ajax-unobtrusive/dist/jquery.unobtrusive-ajax.min.js",
            "/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js",
            "/lib/jquery/jquery-ui.min.js",
            "/js/jQueryFixes.js",
            "/lib/jszip/jszip.min.js")
            .EnforceFileExtensions(".js")
            .Concatenate()
            .AddResponseHeader("X-Content-Type-Options", "nosniff");

        pipeline.AddJavaScriptBundle("/bundles/modernizr.js",
          "/lib/modernizr-2.8.3.js");

        pipeline.AddJavaScriptBundle("/bundles/bootstrap.js",
          "/lib/bootstrap/bootstrap.js",
          "/lib/matchmedia/respond.js");

        pipeline.AddBundle("/bundles/telerikjs.js", "text/javascript; charset=UTF-8",
          "/js/kendo/2020.2.617/kendo.all.min.js",
          "/js/kendo/2020.2.617/kendo.aspnetmvc.min.js",
          "/js/kendo/2020.2.617/cultures/kendo.culture.pl-PL.min.js",
          "/js/kendo/2020.2.617/cultures/kendo.culture.tr-TR.min.js")
          .EnforceFileExtensions(".js")
          .Concatenate()
          .AddResponseHeader("X-Content-Type-Options", "nosniff");

        pipeline.AddJavaScriptBundle("/bundles/service.js",
          "/js/service.js",
          "/js/popup.js",
          "/js/Notifications.js",
          "/js/refreshHandler.js",
          "/lib/sweetalert/sweetalert.min.js",
          "/lib/peekabar.js",
          "/lib/jquery.popup.min.js",
          "/js/System/navigation.js",
          "/js/timer.js",
          "/js/contents.js",
          "/js/Shared/shared.js",
          "/js/Shared/Partial/_*.js");

        pipeline.AddCssBundle("/bundles/css/css.css",
          "/css/bootstrap/bootstrap.min.css",
          "/css/sweetalert.css",
          "/css/peekabar.css",
          "/css/Shared/navigation.css",
          "/css/jquery-ui/jquery-ui.css",
          "/css/popup.css",
          "/css/site.css")
        .UseFileProvider(new PhysicalFileProvider(webRootPath));

        pipeline.AddCssBundle("/bundles/css/telerikcss.css",
          "/css/kendo/2020.2.617/kendo.common.min.css",
          "/css/kendo/2020.2.617/kendo.uniform.min.css",
          "/css/kendo/2020.2.617/kendo.dataviz.default.min.css",
          "/css/kendo/2020.2.617/kendo.dataviz.min.css",
          "/css/kendo/TelerikSkin.css")
        .UseFileProvider(new PhysicalFileProvider(webRootPath));
        #endregion

        // Module CSS and JS
        // MAIN PAGE
        pipeline.AddCssBundle("/bundles/css/Module/MainPage.css",
            "/css/Module/PE.Lite/MainPage.css");

        // WORK ORDER
        pipeline.AddCssBundle("/bundles/css/Module/WorkOrder.css",
            "/css/Module/PE.Lite/WorkOrder.css",
            "/css/expandCategoriesButton.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/WorkOrder.js",
          "/js/Module/PE.Lite/WorkOrder.js",
          "/js/expandCategoriesButton.js");

        pipeline.AddCssBundle("/bundles/css/system/watchdog.css",
          "/css/system/watchdog.css");

        pipeline.AddJavaScriptBundle("/bundles/js/system/watchdog.js",
          "/js/system/watchdog.js");

        // INSPECTION
        pipeline.AddCssBundle("/bundles/css/Module/Inspection.css",
            "/css/Module/PE.Lite/Inspection.css",
            "/css/expandCategoriesButton.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/Inspection.js",
          "/js/Module/PE.Lite/Inspection.js",
          "/js/expandCategoriesButton.js");

        // PERFORMANCE MONITOR
        pipeline.AddCssBundle("/bundles/css/Module/PerformanceWorkOrderMonitor.css",
            "/css/Module/PE.Lite/PerformanceWorkOrderMonitor.css",
            "/css/expandCategoriesButton.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/PerformanceWorkOrderMonitor.js",
          "/js/Module/PE.Lite/PerformanceWorkOrderMonitor.js",
          "/js/expandCategoriesButton.js");

        // KPI
        pipeline.AddCssBundle("/bundles/css/Module/KPI.css",
          "/css/Module/PE.Lite/KPI.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/KPIGauge.js",
          "/js/Module/PE.Lite/KPIGauge.js");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/KPIChart.js",
          "/js/Module/PE.Lite/KPIChart.js");

        // WORK ORDER CONFIRMATION
        pipeline.AddCssBundle("/bundles/css/Module/WorkOrderConfirmation.css",
          "/css/Module/PE.Lite/WorkOrderConfirmation.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/WorkOrderConfirmation.js",
          "/js/Module/PE.Lite/WorkOrderConfirmation.js",
          "/js/expandCategoriesButton.js");

        // ZEBRA PRINTER
        pipeline.AddJavaScriptBundle("/bundles/js/Module/ZebraPrinter.js",
          "/js/Module/PE.Lite/ZebraPrinter.js",
          "/js/expandCategoriesButton.js");

        // COIL INSPECTION STATION
        pipeline.AddCssBundle("/bundles/css/Module/CoilInspectionStation.css",
          "/css/Module/PE.Lite/CoilInspectionStation.css",
          "/css/expandCategoriesButton.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/CoilInspectionStation.js",
          "/js/Module/PE.Lite/CoilInspectionStation.js",
          "/js/expandCategoriesButton.js");

        // BUNDLE INSPECTION STATION
        pipeline.AddCssBundle("/bundles/css/Module/BundleInspectionStation.css",
          "/css/Module/PE.Lite/BundleInspectionStation.css",
          "/css/expandCategoriesButton.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/BundleInspectionStation.js",
          "/js/Module/PE.Lite/BundleInspectionStation.js",
          "/js/expandCategoriesButton.js");

        // HEAT
        pipeline.AddCssBundle("/bundles/css/Module/Heat.css",
            "/css/Module/PE.Lite/Heat.css",
            "/css/expandCategoriesButton.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/Heat.js",
          "/js/Module/PE.Lite/Heat.js",
          "/js/expandCategoriesButton.js");

        // TRACKING
        pipeline.AddCssBundle("/bundles/css/Module/Tracking.css",
            "/css/Module/PE.Lite/Tracking.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/Tracking.js",
          "/js/Module/PE.Lite/Tracking.js");

        // TRACKING MANAGEMENT
        pipeline.AddCssBundle("/bundles/css/Module/TrackingManagement.css",
            "/css/Module/PE.Lite/TrackingManagement.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/TrackingManagement.js",
          "/js/Module/PE.Lite/TrackingManagement.js",
          "/js/Module/PE.Lite/TrackingManagementExtensions.js");

        // TRACKING VISUALIZATION
        pipeline.AddCssBundle("/bundles/css/Module/Visualization.css",
            "/css/Module/PE.Lite/Visualization.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/Visualization.js",
          "/js/Module/PE.Lite/Visualization.js",
          "/js/Module/PE.Lite/MeasurementsData.js",
          "/js/Module/PE.Lite/Schedule.js");

        // BAR HANDLING
        pipeline.AddCssBundle("/bundles/css/Module/BarHandling.css",
            "/css/Module/PE.Lite/BarHandling.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/BarHandling.js",
          "/js/Module/PE.Lite/BarHandling.js",
          "/js/Module/PE.Lite/MeasurementsData.js",
          "/js/Module/PE.Lite/Schedule.js");

        // BarOutlet Management
        pipeline.AddCssBundle("/bundles/css/Module/BarOutletManagement.css",
            "/css/Module/PE.Lite/BarOutletManagement.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/BarOutletManagement.js",
          "/js/Module/PE.Lite/BarOutletManagement.js",
          "/js/Module/PE.Lite/TrackingManagementExtensions.js");

        // MEASUREMENTS
        pipeline.AddCssBundle("/bundles/css/Module/Measurements.css",
            "/css/Module/PE.Lite/Measurements.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/Measurements.js",
          "/js/Module/PE.Lite/Measurements.js",
          "/js/Module/PE.Lite/MeasurementsData.js");

        // MEASUREMENTS SUMMARY
        pipeline.AddCssBundle("/bundles/css/Module/MeasurementsSummary.css",
            "/css/Module/PE.Lite/Measurements.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/MeasurementsSummary.js",
          "/js/Module/PE.Lite/MeasurementsSummary.js");


        //MATERIAL MEASUREMENTS CHART
        pipeline.AddCssBundle("/bundles/css/Module/MeasurementAnalysis.css",
            "/css/Module/PE.Lite/MeasurementAnalysis.css",
            "/css/Module/PE.Lite/expandCategoriesButton.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/MeasurementAnalysis.js",
          "/js/Module/PE.Lite/MeasurementAnalysis.js",
          "/lib/amcharts4/core.js",
          "/lib/amcharts4/charts.js",
          "/lib/amcharts4/index.js",
          "/js/Module/PE.Lite/MeasurementsChart.js",
          "/js/expandCategoriesButton.js");


        // MEASUREMENTS ANALYSIS COMPARISON
        pipeline.AddCssBundle("/bundles/css/Module/MeasurementAnalysisComparison.css",
      "/css/Module/PE.Lite/MeasurementAnalysisComparison.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/MeasurementAnalysisComparison.js",
          "/lib/amcharts4/index.js",
          "/lib/amcharts4/xy.js",
          "/lib/amcharts4/Animated.js",
          "/js/Module/PE.Lite/MeasurementAnalysisComparison.js");


        // Historian
        pipeline.AddCssBundle("/bundles/css/Module/Historian.css",
          "/css/Module/PE.Lite/Historian.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/Historian.js",
          "/lib/amcharts4/index.js",
          "/lib/amcharts4/xy.js",
          "/lib/amcharts4/Animated.js",
          "/js/Module/PE.Lite/Historian.js");

        // Material
        pipeline.AddCssBundle("/bundles/css/Module/Material.css",
      "/css/Module/PE.Lite/Material.css",
      "/css/expandCategoriesButton.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/Material.js",
          "/js/Module/PE.Lite/Material.js",
          "/js/expandCategoriesButton.js");

        // RawMaterial
        pipeline.AddCssBundle("/bundles/css/Module/RawMaterial.css",
            "/css/Module/PE.Lite/RawMaterial.css",
            "/css/expandCategoriesButton.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/RawMaterial.js",
          "/js/Module/PE.Lite/RawMaterial.js",
          "/js/Module/PE.Lite/MeasurementsData.js",
          "/js/expandCategoriesButton.js");

        // Bundle
        pipeline.AddCssBundle("/bundles/css/Module/Bundle.css",
            "/css/Module/PE.Lite/Bundle.css",
            "/css/expandCategoriesButton.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/Bundle.js",
          "/js/Module/PE.Lite/Bundle.js",
          "/js/Module/PE.Lite/MeasurementsData.js",
          "/js/expandCategoriesButton.js");

        // Layer
        pipeline.AddCssBundle("/bundles/css/Module/Layer.css",
            "/css/Module/PE.Lite/Layer.css",
            "/css/expandCategoriesButton.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/Layer.js",
          "/js/Module/PE.Lite/Layer.js",
          "/js/Module/PE.Lite/MeasurementsData.js",
          "/js/expandCategoriesButton.js");

        // SCHEDULE
        pipeline.AddCssBundle("/bundles/css/Module/Schedule.css",
            "/css/Module/PE.Lite/Schedule.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/Schedule.js",
          "/js/Module/PE.Lite/Schedule.js");

        // CoilWeighingMonitor
        pipeline.AddJavaScriptBundle("/bundles/js/Module/CoilWeighingMonitor.js",
            "/js/Module/PE.Lite/CoilWeighingMonitor.js");

        // BundleWeighingMonitor
        pipeline.AddJavaScriptBundle("/bundles/js/Module/BundleWeighingMonitor.js",
            "/js/Module/PE.Lite/BundleWeighingMonitor.js");

        // RollSetManagement
        pipeline.AddJavaScriptBundle("/bundles/js/Module/RollSetManagement.js",
            "/js/Module/PE.Lite/RollSetManagement.js",
            "/js/Module/PE.Lite/RollShopCommon.js");

        // ShiftCalendar
        pipeline.AddJavaScriptBundle("/bundles/js/Module/PE.Lite/ShiftCalendar.js",
            "/js/Module/PE.Lite/ShiftCalendar.js");

        // RollsManagement
        pipeline.AddCssBundle("/bundles/css/Module/RollsManagement.css",
            "/css/Module/PE.Lite/RollsManagement.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/RollsManagement.js",
          "/js/Module/PE.Lite/RollsManagement.js",
          "/js/Module/PE.Lite/RollShopCommon.js");

        // RollSetHistory
        pipeline.AddCssBundle("/bundles/css/Module/RollSetHistory.css",
            "/css/Module/PE.Lite/RollSetHistory.css",
            "/css/expandCategoriesButton.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/RollSetHistory.js",
          "/js/Module/PE.Lite/RollSetHistory.js",
          "/js/expandCategoriesButton.js",
          "/js/Module/PE.Lite/RollShopCommon.js");

        // RollType
        pipeline.AddJavaScriptBundle("/bundles/js/Module/RollType.js",
            "/js/Module/PE.Lite/RollType.js",
            "/js/Module/PE.Lite/RollShopCommon.js");

        // GrindingTurning
        pipeline.AddJavaScriptBundle("/bundles/js/Module/GrindingTurning.js",
            "/js/Module/PE.Lite/GrindingTurning.js",
            "/js/Module/PE.Lite/RollShopCommon.js");

        // ActualStandConfiguration
        pipeline.AddJavaScriptBundle("/bundles/js/Module/ActualStandConfiguration.js",
            "/js/Module/PE.Lite/ActualStandConfiguration.js",
            "/js/Module/PE.Lite/RollShopCommon.js");

        // RollSetToCassette
        pipeline.AddJavaScriptBundle("/bundles/js/Module/RollSetToCassette.js",
            "/js/Module/PE.Lite/RollSetToCassette.js",
            "/js/Module/PE.Lite/RollShopCommon.js");

        // CassetteType
        pipeline.AddJavaScriptBundle("/bundles/js/Module/CassetteType.js",
            "/js/Module/PE.Lite/CassetteType.js",
            "/js/Module/PE.Lite/RollShopCommon.js");

        // Cassette
        pipeline.AddJavaScriptBundle("/bundles/js/Module/Cassette.js",
            "/js/Module/PE.Lite/Cassette.js",
            "/js/Module/PE.Lite/RollShopCommon.js");


        // GrooveTemplate
        pipeline.AddJavaScriptBundle("/bundles/js/Module/GrooveTemplate.js",
            "/js/Module/PE.Lite/GrooveTemplate.js",
            "/js/Module/PE.Lite/RollShopCommon.js");

        // Steelgrade
        pipeline.AddJavaScriptBundle("/bundles/js/Module/Steelgrade.js",
            "/js/Module/PE.Lite/Steelgrade.js");

        // ProductCatalogue
        pipeline.AddJavaScriptBundle("/bundles/js/Module/ProductCatalogue.js",
            "/js/Module/PE.Lite/ProductCatalogue.js");

        // DefectCatalogue
        pipeline.AddJavaScriptBundle("/bundles/js/Module/DefectCatalogue.js",
            "/js/Module/PE.Lite/DefectCatalogue.js");

        // DefectGroupsCatalogue
        pipeline.AddJavaScriptBundle("/bundles/js/Module/DefectGroupsCatalogue.js",
            "/js/Module/PE.Lite/DefectGroupsCatalogue.js");

        // DefectCatalogueCategories
        pipeline.AddJavaScriptBundle("/bundles/js/Module/DefectCatalogueCategories.js",
            "/js/Module/PE.Lite/DefectCatalogueCategories.js");

        // DelayCatalogue
        pipeline.AddCssBundle("/bundles/css/Module/EventCatalogue.css",
            "/css/expandCategoriesButton.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/EventCatalogue.js",
          "/js/Module/PE.Lite/EventCatalogue.js",
          "/js/expandCategoriesButton.js");

        // MaterialCatalogue
        pipeline.AddCssBundle("/bundles/css/Module/MaterialCatalogue.css",
            "/css/Module/PE.Lite/MaterialCatalogue.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/MaterialCatalogue.js",
          "/js/Module/PE.Lite/MaterialCatalogue.js");

        // Delay
        pipeline.AddCssBundle("/bundles/css/Module/Delay.css",
            "/css/Module/PE.Lite/Delay.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/Delay.js",
          "/js/Module/PE.Lite/Delay.js");

        // Maintenance
        pipeline.AddCssBundle("/bundles/css/Module/Maintenance.css",
            "/css/Module/PE.Lite/Maintenance.css",
            "/css/expandCategoriesButton.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/Maintenance.js",
          "/js/Module/PE.Lite/Maintenance.js",
          "/js/expandCategoriesButton.js");

        // IncidentType
        pipeline.AddCssBundle("/bundles/css/Module/IncidentType.css",
            "/css/Module/PE.Lite/IncidentType.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/IncidentType.js",
          "/js/Module/PE.Lite/IncidentType.js",
          "/js/expandCategoriesButton.js");

        // DeviceGroup
        pipeline.AddCssBundle("/bundles/css/Module/DeviceGroup.css",
            "/css/Module/PE.Lite/DeviceGroup.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/DeviceGroup.js",
          "/js/Module/PE.Lite/DeviceGroup.js",
          "/js/expandCategoriesButton.js");


        // ComponentGroup
        pipeline.AddCssBundle("/bundles/css/Module/ComponentGroup.css",
            "/css/Module/PE.Lite/ComponentGroup.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/ComponentGroup.js",
          "/js/Module/PE.Lite/ComponentGroup.js",
          "/js/expandCategoriesButton.js");

        // RecommendedAction
        pipeline.AddCssBundle("/bundles/css/Module/RecommendedAction.css",
            "/css/Module/PE.Lite/RecommendedAction.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/RecommendedAction.js",
          "/js/Module/PE.Lite/RecommendedAction.js",
          "/js/expandCategoriesButton.js");

        // LabelPrinter
        pipeline.AddCssBundle("/bundles/css/Module/LabelPrinter.css",
            "/css/Module/PE.Lite/LabelPrinter.css");

        pipeline.AddCssBundle("/bundles/css/Module/LabelPrinterPreview.css",
          "/css/Module/PE.Lite/LabelPrinterPreview.css");

        // L3CommStatus
        pipeline.AddCssBundle("/bundles/css/System/L3TransferTableWorkOrder.css",
            "/css/System/L3TransferTableWorkOrder.css");

        pipeline.AddCssBundle("/bundles/css/System/L3TransferTableCommStatus.css",
          "/css/System/L3TransferTableCommStatus.css");

        pipeline.AddJavaScriptBundle("/bundles/js/System/L3TransferTableWorkOrder.js",
          "/js/System/L3TransferTableWorkOrder.js");

        pipeline.AddJavaScriptBundle("/bundles/js/System/L3TransferTableCommStatus.js",
          "/js/System/L3TransferTableCommStatus.js");

        // Setup
        pipeline.AddCssBundle("/bundles/css/Module/Setup.css",
            "/css/Module/PE.Lite/Setup.css",
            "/css/expandCategoriesButton.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/Setup.js",
          "/js/Module/PE.Lite/Setup.js",
          "/js/expandCategoriesButton.js");

        // SetupConfiguration
        pipeline.AddCssBundle("/bundles/css/Module/SetupConfiguration.css",
            "/css/Module/PE.Lite/SetupConfiguration.css",
            "/css/expandCategoriesButton.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/SetupConfiguration.js",
          "/js/Module/PE.Lite/SetupConfiguration.js",
          "/js/expandCategoriesButton.js");

        // Products
        pipeline.AddCssBundle("/bundles/css/Module/Products.css",
            "/css/Module/PE.Lite/Products.css",
            "/css/expandCategoriesButton.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/Products.js",
          "/js/Module/PE.Lite/Products.js",
          "/js/expandCategoriesButton.js");

        // ConsumptionMonitoring
        pipeline.AddCssBundle("/bundles/css/Module/ConsumptionMonitoring.css",
            "/css/Module/PE.Lite/ConsumptionMonitoring.css",
            "/css/expandCategoriesButton.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/ConsumptionMonitoring.js",
          "/lib/amcharts4/index.js",
          "/lib/amcharts4/xy.js",
          "/lib/amcharts4/Animated.js",
          "/js/Module/PE.Lite/ConsumptionMonitoring.js",
          "/js/expandCategoriesButton.js");

        // EventCalendar
        pipeline.AddCssBundle("/bundles/css/Module/EventCalendar.css",
            "/css/Module/PE.Lite/EventCalendar.css",
            "/css/expandCategoriesButton.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/EventCalendar.js",
          "/js/Module/PE.Lite/EventCalendar.js",
          "/js/expandCategoriesButton.js");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/EventCalendarScheduler.js",
          "/js/Module/PE.Lite/EventCalendarScheduler.js");

        // Furnace
        pipeline.AddCssBundle("/bundles/css/Module/Furnace.css",
          "/css/Module/PE.Lite/Furnace.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/Furnace.js",
          "/js/Module/PE.Lite/Furnace.js",
          "/js/Module/PE.Lite/MeasurementsData.js");

        // EquipmentGroups
        pipeline.AddJavaScriptBundle("/bundles/js/Module/EquipmentGroups.js",
            "/js/Module/PE.Lite/EquipmentGroups.js");

        //Equipment
        pipeline.AddJavaScriptBundle("/bundles/js/Module/Equipment.js",
          "/js/Module/PE.Lite/Equipment.js");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/ActionType.js",
          "/js/Module/PE.Lite/ActionType.js",
          "/js/expandCategoriesButton.js");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/QuantityType.js",
          "/js/Module/PE.Lite/QuantityType.js",
          "/js/expandCategoriesButton.js");

        // Quality
        pipeline.AddCssBundle("/bundles/css/Module/Quality.css",
            "/css/Module/PE.Lite/Quality.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/Quality.js",
          "/js/Module/PE.Lite/Quality.js");

        // DataAnalysis
        pipeline.AddCssBundle("/bundles/css/Module/DataAnalysis.css",
            "/css/Module/PE.Lite/DataAnalysis.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/DataAnalysis.js",
          "/js/Module/PE.Lite/DataAnalysis.js");

        // ScrapGroup
        pipeline.AddJavaScriptBundle("/bundles/js/Module/Scrap.js",
            "/js/Module/PE.Lite/Scrap.js");

        // SteelFamily
        pipeline.AddJavaScriptBundle("/bundles/js/Module/SteelFamily.js",
            "/js/Module/PE.Lite/SteelFamily.js");

        // EventCatalogueCategories
        pipeline.AddJavaScriptBundle("/bundles/js/Module/EventCatalogueCategories.js",
            "/js/Module/PE.Lite/EventCatalogueCategories.js");

        // EventGroupsCatalogue
        pipeline.AddJavaScriptBundle("/bundles/js/Module/EventGroupsCatalogue.js",
            "/js/Module/PE.Lite/EventGroupsCatalogue.js");

        // CoolingBed
        pipeline.AddCssBundle("/bundles/css/Module/CoolingBed.css",
            "/css/Module/PE.Lite/CoolingBed.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/CoolingBed.js",
          "/js/Module/PE.Lite/CoolingBed.js");

        // BilletYard
        pipeline.AddCssBundle("/bundles/css/Module/BilletYard.css",
            "/css/Module/PE.Lite/BilletYard.css",
            "/css/expandCategoriesButton.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/BilletYard.js",
          "/js/Module/PE.Lite/BilletYard.js",
          "/js/expandCategoriesButton.js");

        // ProductYard
        pipeline.AddCssBundle("/bundles/css/Module/ProductYard.css",
            "/css/Module/PE.Lite/ProductYard.css",
            "/css/expandCategoriesButton.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/ProductYard.js",
          "/js/Module/PE.Lite/ProductYard.js",
          "/js/expandCategoriesButton.js");

        // QualityExpert
        pipeline.AddCssBundle("/bundles/css/Module/QualityExpert.css",
            "/css/Module/PE.Lite/QualityExpert.css",
            "/css/expandCategoriesButton.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/QualityExpert.js",
          "/js/Module/PE.Lite/QualityExpert.js",
          "/js/expandCategoriesButton.js");

        // AlarmCreator
        pipeline.AddCssBundle("/bundles/css/Module/AlarmCreator.css",
            "/css/Module/PE.Lite/AlarmCreator.css",
            "/css/expandCategoriesButton.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/AlarmCreator.js",
          "/js/Module/PE.Lite/AlarmCreator.js",
          "/js/expandCategoriesButton.js");

        // FeatureTemplate
        pipeline.AddCssBundle("/bundles/css/Module/MillConfigurator.css",
            "/css/Module/PE.Lite/MillConfigurator.css",
            "/css/expandCategoriesButton.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/FeatureTemplate.js",
          "/js/Module/PE.Lite/FeatureTemplate.js",
          "/js/expandCategoriesButton.js");

        // AssetTemplate
        pipeline.AddCssBundle("/bundles/css/Module/MillConfigurator.css",
            "/css/Module/PE.Lite/MillConfigurator.css",
            "/css/expandCategoriesButton.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/AssetTemplate.js",
          "/js/Module/PE.Lite/AssetTemplate.js",
          "/js/expandCategoriesButton.js");

        // MillConfiguration
        pipeline.AddCssBundle("/bundles/css/Module/MillConfigurator.css",
            "/css/Module/PE.Lite/MillConfigurator.css",
            "/css/expandCategoriesButton.css");

        // PDF Features
        pipeline.AddCssBundle("/bundles/css/Module/PDFstyles.css",
            "/css/Module/PE.Lite/PDFstyles.css");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/MillConfiguration.js",
          "/js/Module/PE.Lite/MillConfiguration.js",
          "/js/expandCategoriesButton.js");

        pipeline.AddJavaScriptBundle("/bundles/js/Module/TrackingInstruction.js",
          "/js/Module/PE.Lite/TrackingInstruction.js",
          "/js/expandCategoriesButton.js");

        // Charging
        pipeline.AddJavaScriptBundle("/bundles/js/Module/Charging.js",
            "/js/Module/PE.Lite/Charging.js");

      });

    }
  }
}
