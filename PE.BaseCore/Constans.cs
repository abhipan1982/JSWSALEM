using SMF.Core.Infrastructure;

namespace PE.Core
{
  /// <summary>
  ///   static class for storing project parameters
  /// </summary>
  public static class Constants
  {
    //opc tag types in config file
    public const string TypeString = "string";
    public const string TypeInteger = "int";
    public const string TypeShort = "short";
    public const string TypeFloat = "float";
    public const string TypeDouble = "double";
    public const string TypeDateAsString = "datestring";
    public const string TypeBool = "bool";
    public const string TypeUnsignedShort = "ushort";
    public const string TypeUnsignedInteger = "uint";

    public static readonly int ExtMessageBufferSize = 90000000;
    public static readonly string ExtDateTimeFormat = "yyyyMMddHHmmss";

    public static readonly string KeyL1TcpStatus = "SYS_STAT_TCP";
    public static readonly string KeyL1OpcStatus = "SYS_STAT_OPC";
    public static readonly string KeyExtDBStatus = "SYS_STAT_ExtDB";


    #region Modules

    public const string SmfAuthorization_Module_System = "System";
    public const string SmfAuthorization_Module_Watchdog = "Watchdog";
    public const string SmfAuthorization_Module_ProdManager = "ProdManager";
    public const string SmfAuthorization_Module_L1Adapter = "L1Adapter";
    public const string SmfAuthorization_Module_EventCalendar = "EventCalendar";
    public const string SmfAuthorization_Module_ProdPlanning = "ProdPlanning";
    public const string SmfAuthorization_Module_MeasValueHistory = "MeasValueHistory";
    public const string SmfAuthorization_Module_Events = "Events";
    public const string SmfAuthorization_Module_WeighingMonitor = "WeighingMonitor";
    public const string SmfAuthorization_Module_Maintenance = "Maintenance";
    public const string SmfAuthorization_Module_Quality = "Quality";
    public const string SmfAuthorization_Module_RollShop = "RollShop";
    public const string SmfAuthorization_Module_Yards = "Yards";
    public const string SmfAuthorization_Module_Tracking = "Tracking";
    public const string SmfAuthorization_Module_QualityExpert = "QualityExpert";
    public const string SmfAuthorization_Module_ZebraPrinterConnector = "ZebraPrinter";
    public const string SmfAuthorization_Module_WalkingBeamFurnace = "WalkingBeamFurnace";

    #endregion

    #region Controllers

    //framework
    public const string SmfAuthorization_Controller_Alarm = "Alarm";
    public const string SmfAuthorization_Controller_Watchdog = "Watchdog";
    public const string SmfAuthorization_Controller_Limit = "Limit";
    public const string SmfAuthorization_Controller_Service = "Service";
    public const string SmfAuthorization_Controller_Parameter = "Parameter";
    public const string SmfAuthorization_Controller_Db2DbCommunication = "Db2DbCommunication";
    public const string SmfAuthorization_Controller_Crew = "Crew";
    public const string SmfAuthorization_Controller_ExtTelArchive = "ExtTelArchive";
    public const string SmfAuthorization_Controller_ShiftCalendar = "ShiftCalendar";
    public const string SmfAuthorization_Controller_Role = "Role";
    public const string SmfAuthorization_Controller_UserAccounts = "UserAccounts";
    public const string SmfAuthorization_Controller_UserAccountAdministration = "UserAccountAdministration";
    public const string SmfAuthorization_Controller_ViewsStatistics = "ViewsStatistics";
    public const string SmfAuthorization_Controller_Test = "Test";
    public const string SmfAuthorization_Controller_WidgetConfigurations = "WidgetConfiguration";
    public const string SmfAuthorization_Controller_L3CommStatus = "L3CommStatus";
    public const string SmfAuthorization_Controller_Tools = "Tools";

    //system
    public const string SmfAuthorization_Controller_FeatureMap = "FeatureMap";

    public const string SmfAuthorization_Controller_Asset = "Asset";

    //system
    public const string SmfAuthorization_Controller_Steelgrade = "Steelgrade";
    public const string SmfAuthorization_Controller_Product = "Product";
    public const string SmfAuthorization_Controller_Billet = "Billet";
    public const string SmfAuthorization_Controller_Defect = "Defect";
    public const string SmfAuthorization_Controller_Delays = "Delays";
    public const string SmfAuthorization_Controller_WorkOrder = "WorkOrder";
    public const string SmfAuthorization_Controller_WorkOrderConfirmation = "WorkOrderConfirmation";
    public const string SmfAuthorization_Controller_Schedule = "Schedule";
    public const string SmfAuthorization_Controller_Heat = "Heat";
    public const string SmfAuthorization_Controller_Material = "Material";
    public const string SmfAuthorization_Controller_RawMaterial = "RawMaterial";
    public const string SmfAuthorization_Controller_Layer = "Layer";
    public const string SmfAuthorization_Controller_Inspection = "Inspection";
    public const string SmfAuthorization_Controller_InspectionStation = "InspectionStation";
    public const string SmfAuthorization_Controller_MaterialFurnace = "MaterialFurnace";
    public const string SmfAuthorization_Controller_Setup = "Setup";
    public const string SmfAuthorization_Controller_Products = "Products";
    public const string SmfAuthorization_Controller_ConsumptionMonitoring = "ConsumptionMonitoring";
    public const string SmfAuthorization_Controller_EventCalendar = "EventCalendar";
    public const string SmfAuthorization_Controller_Furnace = "Furnace";
    public const string SmfAuthorization_Controller_Furnace2 = "Furnace2";
    public const string SmfAuthorization_Controller_Tracking = "Tracking";
    public const string SmfAuthorization_Controller_TrackingManagement = "TrackingManagement";
    public const string SmfAuthorization_Controller_CoilWeighingMonitor = "CoilWeighingMonitor";
    public const string SmfAuthorization_Controller_BundleWeighingMonitor = "BundleWeighingMonitor";
    public const string SmfAuthorization_Controller_RollsManagement = "RollsManagement";
    public const string SmfAuthorization_Controller_Visualization = "Visualization";
    public const string SmfAuthorization_Controller_Measurements = "Measurements";
    public const string SmfAuthorization_Controller_EquipmentGroups = "EquipmentGroups";
    public const string SmfAuthorization_Controller_Equipment = "Equipment";
    public const string SmfAuthorization_Controller_RollTypes = "RollType";
    public const string SmfAuthorization_Controller_GrooveTemplates = "GrooveTemplate";
    public const string SmfAuthorization_Controller_RollSetManagement = "RollSetManagement";
    public const string SmfAuthorization_Controller_CassetteType = "CassetteType";
    public const string SmfAuthorization_Controller_Cassette = "Cassette";
    public const string SmfAuthorization_Controller_GrindingTurning = "GrindingTurning";
    public const string SmfAuthorization_Controller_ActualStandConfiguration = "ActualStandConfiguration";
    public const string SmfAuthorization_Controller_RollSetToCassette = "RollSetToCassette";
    public const string SmfAuthorization_Controller_ProductQualityMgm = "ProductQualityMgm";
    public const string SmfAuthorization_Controller_L1CommStatus = "L1CommStatus";
    public const string SmfAuthorization_Controller_CoolingBed = "CoolingBed";
    public const string SmfAuthorization_Controller_BilletYard = "BilletYard";
    public const string SmfAuthorization_Controller_ProductYard = "ProductYard";
    public const string SmfAuthorization_Controller_SetupConfiguration = "SetupConfiguration";
    public const string SmfAuthorization_Controller_QualityExpert = "QualityExpert";
    public const string SmfAuthorization_Controller_BarHandling = "BarHandling";
    public const string SmfAuthorization_Controller_BarOutletManagementarHandling = "BarOutletManagement";
    public const string SmfAuthorization_Controller_LabelPrinter = "LabelPrinter";
    public const string SmfAuthorization_Controller_Notebooks = "Notebooks";

    #endregion

    #region Tracking Configuration

    public static readonly string WireRod = "W";
    public static readonly string Bar = "B";
    public static readonly string Garret = "G";

    #endregion
  }

  public static class Modules
  {
    public static readonly ModuleDescription Wdog = new ModuleDescription("wdog", "watchdog module");
    public static readonly ModuleDescription Hmiexe = new ModuleDescription("hmi", "hmi module");


    public static readonly ModuleDescription ModuleA = new ModuleDescription("moduleA", "Module A");
    public static readonly ModuleDescription ModuleB = new ModuleDescription("moduleB", "Module B");
    public static readonly ModuleDescription Adapter = new ModuleDescription("adapter", "Adapter Module");
    public static readonly ModuleDescription L1Adapter = new ModuleDescription("L1Adapter", "L1Adapter");
    public static readonly ModuleDescription DBAdapter = new ModuleDescription("dBAdapter", "DB Adapter Module");

    public static readonly ModuleDescription ProdManager =
      new ModuleDescription("prodManager", "Production Manager Module");

    public static readonly ModuleDescription ProdPlaning =
      new ModuleDescription("prodPlaning", "Production Planing Module");

    public static readonly ModuleDescription MvHistory = new ModuleDescription("MVHistory", "MV History");
    public static readonly ModuleDescription Events = new ModuleDescription("events", "Events Module");
    public static readonly ModuleDescription Tracking = new ModuleDescription("tracking", "Tracking Module");

    public static readonly ModuleDescription WalkingBeamFurnace =
      new ModuleDescription("walkingBeamFurnance", "Walking Beam Furnance Module");

    public static readonly ModuleDescription L1Sim = new ModuleDescription("l1Sim", "Level 1 Simulation");
    public static readonly ModuleDescription L3Sim = new ModuleDescription("SimL3", "Level 3 Simulation");
    public static readonly ModuleDescription TcpProxy = new ModuleDescription("tcpProxy", "L1 TCP Communication");

    public static readonly ModuleDescription ZebraPrinter =
      new ModuleDescription("zebraPrinterConnector", "Zebra Printer Communication");

    public static readonly ModuleDescription Setup = new ModuleDescription("Setup", "Setup");
    public static readonly ModuleDescription Simulation = new ModuleDescription("Simulation", "Simulation");
    public static readonly ModuleDescription Dispatcher = new ModuleDescription("Dispatcher", "Dispatcher");
    public static readonly ModuleDescription RollShop = new ModuleDescription("RollShop", "RollShop");
    public static readonly ModuleDescription Maintenance = new ModuleDescription("Maintenance", "Maintenance");
    public static readonly ModuleDescription Quality = new ModuleDescription("Quality", "Quality");
    public static readonly ModuleDescription QualityExpert = new ModuleDescription("QualityExpert", "QualityExpert");
    public static readonly ModuleDescription Performance = new ModuleDescription("Performance", "Performance");
    public static readonly ModuleDescription Yards = new ModuleDescription("Yards", "Yards");
  }
}
