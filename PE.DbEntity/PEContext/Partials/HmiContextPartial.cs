using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PE.DbEntity.SPModels;

namespace PE.DbEntity.PEContext
{
  partial class HmiContext : DbContext
  {
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var connectionString = configuration.GetConnectionString("HmiContext");
        optionsBuilder.UseSqlServer(connectionString);
      }
    }
    public DbSet<SPStringResult> SPStringResults { get; set; }
    public DbSet<SPSetupInstruction> SPSetupInstructions { get; set; }
    public DbSet<SPL3L1MaterialAssignment> SPL3L1MaterialAssignments { get; set; }
    public DbSet<SPDictionary> SPDictionaries { get; set; }
    public DbSet<SPL3L1MaterialInArea> SPL3L1MaterialInAreas { get; set; }
    public DbSet<SPWorkOrderSummary> SPWorkOrderSummary { get; set; }
    public DbSet<SPL3L1MaterialsInFurnance> SPL3L1MaterialsInFurnances { get; set; }
    public DbSet<SPRawMaterialGenealogy> SPRawMaterialGenealogies { get; set; }
    public DbSet<SPUserMenu> SPUserMenus { get; set; }
    public DbSet<SPGetAlarmsByLanguage> SPGetAlarmsByLanguage { get; set; }
    public DbSet<SPActiveBypassResult> SPBypassWidget { get; set; }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<SPStringResult>().HasNoKey();
      modelBuilder.Entity<SPSetupInstruction>().HasNoKey();
      modelBuilder.Entity<SPL3L1MaterialAssignment>().HasNoKey();
      modelBuilder.Entity<SPWorkOrderSummary>().HasNoKey();
      modelBuilder.Entity<SPDictionary>().HasNoKey();
      modelBuilder.Entity<SPL3L1MaterialInArea>().HasNoKey();
      modelBuilder.Entity<SPL3L1MaterialsInFurnance>().HasNoKey();
      modelBuilder.Entity<SPRawMaterialGenealogy>().HasNoKey();
      modelBuilder.Entity<SPUserMenu>().HasNoKey();
      modelBuilder.Entity<SPGetAlarmsByLanguage>().HasNoKey();
      modelBuilder.Entity<SPActiveBypassResult>().HasNoKey();
      modelBuilder.Entity<SPDayShiftLayoutId>().HasNoKey();
    }
  }
}
