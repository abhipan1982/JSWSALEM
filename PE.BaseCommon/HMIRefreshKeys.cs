using System;

namespace PE.Common
{
  public class HmiAlarmPrefixes
  {
    public static readonly string System = "System";
    public static readonly string Tracking = "Tracking";
    public static readonly string Module = "Module";
  }

  public class HMIRefreshKeys
  {
    //system
    public static readonly string SysAlarmRefresh = String.Format("{0}.AlarmRefresh", HmiAlarmPrefixes.System);
    public static readonly string SysDb2DbRefresh = String.Format("{0}.Db2DbRefresh", HmiAlarmPrefixes.System);
    public static readonly string SysExtTelArchiveRefresh = String.Format("{0}.ExtTelArchive", HmiAlarmPrefixes.System);
    public static readonly string Limits = String.Format("{0}.Limits", HmiAlarmPrefixes.System);

    public static readonly string L3TransferTableWorkOrders =
      String.Format("{0}.L3TransferTableWorkOrders", HmiAlarmPrefixes.System);

    public static readonly string L3TransferTableSteelGrades =
      String.Format("{0}.L3TransferTableSteelGrades", HmiAlarmPrefixes.System);

    public static readonly string L3TransferTableWorkOrderReports =
      String.Format("{0}.L3TransferTableWorkOrderReports", HmiAlarmPrefixes.System);

    public static readonly string L3TransferTableProductReports =
      String.Format("{0}.L3TransferTableProductReports", HmiAlarmPrefixes.System);

    public static readonly string Setup = String.Format("{0}.Setup", HmiAlarmPrefixes.System);
    public static readonly string SetupConfiguration = String.Format("{0}.SetupConfiguration", HmiAlarmPrefixes.System);
    public static readonly string L3TransferTable = String.Format("{0}.L3TransferTable", HmiAlarmPrefixes.System);

    //catalogues
    public static readonly string Steelgrade = String.Format("{0}.Steelgrade", HmiAlarmPrefixes.Module);
    public static readonly string Steelgroup = String.Format("{0}.Steelgroup", HmiAlarmPrefixes.Module);

    public static readonly string RawMaterialCatalogue =
      String.Format("{0}.RawMaterialCatalogue", HmiAlarmPrefixes.Module);

    public static readonly string ProductCatalogue = String.Format("{0}.ProductCatalogue", HmiAlarmPrefixes.Module);

    public static readonly string ProductCatalogueType =
      String.Format("{0}.ProductCatalogueType", HmiAlarmPrefixes.Module);

    public static readonly string DefectCatalogue = String.Format("{0}.DefectCatalogue", HmiAlarmPrefixes.Module);

    public static readonly string ProfileConfiguration =
      String.Format("{0}.CutOptimalization", HmiAlarmPrefixes.Module);

    public static readonly string Heat = String.Format("{0}.Heat", HmiAlarmPrefixes.Module);
    public static readonly string PrimaryDataInput = String.Format("{0}.PrimaryDataInput", HmiAlarmPrefixes.Module);
    public static readonly string TrackingMaterials = String.Format("{0}.TrackingMaterials", HmiAlarmPrefixes.Module);
    public static readonly string PrimaryDataOutput = String.Format("{0}.PrimaryDataOutput", HmiAlarmPrefixes.Module);
    public static readonly string MaterialCatalogue = String.Format("{0}.MaterialCatalogue", HmiAlarmPrefixes.Module);
    public static readonly string EventCatalogue = String.Format("{0}.DelayCatalogue", HmiAlarmPrefixes.Module);
    public static readonly string Delay = String.Format("{0}.Delay", HmiAlarmPrefixes.Module);
    public static readonly string Products = String.Format("{0}.Products", HmiAlarmPrefixes.Module);
    public static readonly string RawMaterialDetails = String.Format("{0}.RawMaterialDetails", HmiAlarmPrefixes.Module);
    public static readonly string EquipmentGroups = String.Format("{0}.EquipmentGroups", HmiAlarmPrefixes.Module);
    public static readonly string Equipment = String.Format("{0}.Equipment", HmiAlarmPrefixes.Module);
    public static readonly string ProductQualityMgm = String.Format("{0}.ProductQualityMgm", HmiAlarmPrefixes.Module);
    public static readonly string InspectionStation = String.Format("{0}.InspectionStation", HmiAlarmPrefixes.Module);
    public static readonly string Inspection = String.Format("{0}.Inspection", HmiAlarmPrefixes.Module);
    public static readonly string WorkOrder = String.Format("{0}.WorkOrder", HmiAlarmPrefixes.Module);
    public static readonly string ScrapGroup = String.Format("{0}.ScrapGroup", HmiAlarmPrefixes.Module);
    public static readonly string SteelFamily = String.Format("{0}.SteelFamily", HmiAlarmPrefixes.Module);
    public static readonly string CoilWeighingMonitor = String.Format("{0}.CoilWeighingMonitor", HmiAlarmPrefixes.Module);
    public static readonly string BundleWeighingMonitor = String.Format("{0}.BundleWeighingMonitor", HmiAlarmPrefixes.Module);

    public static readonly string EventCatalogueCategories =
      String.Format("{0}.DelayCatalogueCategories", HmiAlarmPrefixes.Module);

    public static readonly string EventGroupsCatalogue =
      String.Format("{0}.DelayGroupsCatalogue", HmiAlarmPrefixes.Module);

    public static readonly string DefectGroupsCatalogue =
      String.Format("{0}.DefectGroupsCatalogue", HmiAlarmPrefixes.Module);

    public static readonly string DefectCatalogueCategories =
      String.Format("{0}.DefectCatalogueCategories", HmiAlarmPrefixes.Module);

    public static readonly string BilletYards = String.Format("{0}.BilletYards", HmiAlarmPrefixes.Module);


    //Rollshop
    public static readonly string Roll = String.Format("{0}.Roll", HmiAlarmPrefixes.Module);


    // Lite
    public static readonly string Schedule = String.Format("{0}.Schedule", HmiAlarmPrefixes.Module);

    // TrackingManagement
    public static readonly string TrackingManagement = String.Format("{0}.TrackingManagement", HmiAlarmPrefixes.Module);

    // Walking beam furnace
    public static readonly string Furnace = String.Format("{0}.Furnace", HmiAlarmPrefixes.Module);

    // Events
    public static readonly string Event = String.Format("{0}.Event", HmiAlarmPrefixes.Module);

    // Active bypasses
    public static readonly string ActiveBypasses = String.Format("{0}.ActiveBypasses", HmiAlarmPrefixes.Module);
  }
}
