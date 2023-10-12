using Microsoft.Extensions.DependencyInjection;
using PE.HMIWWW.Services.Module;
using PE.HMIWWW.Services.Module.PE.Lite;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.Services.System;
using PE.Services.System;

namespace PE.HMIWWW.Services
{
  public static class Initialization
  {
    public static void RegisterServices(IServiceCollection services)
    {
      //services.AddSingleton<IBaseService, BaseService>();
      services.AddScoped<IViewToStringRendererService, ViewToStringRendererService>();
      services.AddScoped<IMainMenuService, MainMenuService>();
      services.AddScoped<IAlarmsService, AlarmsService>();
      services.AddSingleton<IWatchdogService, WatchdogService>();
      services.AddScoped<IAccessUnitsService, AccessUnitsService>();
      services.AddScoped<ILimitService, LimitService>();
      services.AddScoped<IParameterService, ParameterService>();
      services.AddScoped<IViewsStaticsService, ViewsStaticsService>();
      services.AddScoped<ICrewService, CrewService>();
      services.AddScoped<IRoleService, RoleService>();
      services.AddScoped<IShiftCalendarService, ShiftCalendarService>();
      services.AddScoped<IAccountService, AccountService>();
      services.AddScoped<IWidgetConfigurationService, WidgetConfigurationService>();
      services.AddScoped<IL3CommStatusService, L3CommStatusService>();
      services.AddScoped<IKPIService, KPIService>();

      //Modules
      services.AddScoped<IBilletService, BilletService>();
      services.AddScoped<IDefectService, DefectService>();
      services.AddScoped<IDefectGroupsCatalogueService, DefectGroupsCatalogueService>();
      services.AddScoped<IDefectCatalogueCategoriesService, DefectCatalogueCategoriesService>();
      services.AddScoped<IDelaysService, DelaysService>();
      services.AddScoped<IProductService, ProductService>();
      services.AddScoped<IConsumptionMonitoringService, ConsumptionMonitoringService>();
      services.AddScoped<ISteelgradeService, SteelgradeService>();
      services.AddScoped<IWorkOrderService, WorkOrderService>();
      services.AddScoped<ITrackingService, TrackingService>();
      services.AddScoped<ITrackingManagementService, TrackingManagementService>();
      services.AddScoped<IScheduleService, ScheduleService>();
      services.AddScoped<IHeatService, HeatService>();
      services.AddScoped<IMaterialService, MaterialService>();
      services.AddScoped<IRawMaterialService, RawMaterialService>();
      services.AddScoped<IBundleService, BundleService>();
      services.AddScoped<IFeatureMap, FeatureMapService>();
      services.AddScoped<IAssetService, AssetService>();
      services.AddScoped<ISetupService, SetupService>();
      services.AddScoped<ISetupConfigurationService, SetupConfigurationService>();
      services.AddScoped<IEventCalendarService, EventCalendarService>();
      services.AddScoped<IProductsService, ProductsService>();
      services.AddScoped<IFurnaceService, FurnaceService>();
      services.AddScoped<ICoilWeighingMonitorService, CoilWeighingMonitorService>();
      services.AddScoped<IBundleWeighingMonitorService, BundleWeighingMonitorService>();
      services.AddScoped<IRollsManagementService, RollsManagementService>();
      services.AddScoped<IRollSetManagementService, RollSetManagementService>();
      services.AddScoped<IRollSetHistoryService, RollSetHistoryService>();
      services.AddScoped<IRollTypeService, RollTypeService>();
      services.AddScoped<IGrooveTemplateService, GrooveTemplateService>();
      services.AddScoped<ICassetteTypeService, CassetteTypeService>();
      services.AddScoped<ICassetteService, CassetteService>();
      services.AddScoped<IGrindingTurningService, GrindingTurningService>();
      services.AddScoped<IActualStandsConfigurationService, ActualStandsConfigurationService>();
      services.AddScoped<IRollsetToCassetteService, RollsetToCassetteService>();
      services.AddScoped<IEquipmentGroupsService, EquipmentGroupsService>();
      services.AddScoped<IEquipmentService, EquipmentService>();
      services.AddScoped<IProductQualityMgmService, ProductQualityMgmService>();
      services.AddScoped<IVisualizationService, VisualizationService>();
      services.AddScoped<IMeasurementsService, MeasurementsService>();
      services.AddScoped<IScrapService, ScrapService>();
      services.AddScoped<ISteelFamilyService, SteelFamilyService>();
      services.AddScoped<IEventCatalogueCategoriesService, EventCatalogueCategoriesService>();
      services.AddScoped<IEventGroupsCatalogueService, EventGroupsCatalogueService>();
      services.AddScoped<IChargingService, ChargingService>();
      services.AddScoped<IInspectionService, InspectionService>();
      services.AddScoped<ILabelPrinterService, LabelPrinterService>();
      services.AddScoped<IDataAnalysisService, DataAnalysisService>();
      services.AddScoped<IBilletYardService, BilletYardService>();
      services.AddScoped<IProductYardService, ProductYardService>();
      services.AddScoped<ILayerService, LayerService>();
      services.AddScoped<IQualityExpertService, QualityExpertService>();
      services.AddScoped<IMeasurementAnalysisService, MeasurementAnalysisService>();
      services.AddScoped<IBarHandlingService, BarHandlingService>();
      services.AddScoped<IBarOutletManagementService, BarOutletManagementService>();
      services.AddScoped<IHistorianService, HistorianService>();
      services.AddScoped<IBypassService, BypassService>();
    }
  }
}
