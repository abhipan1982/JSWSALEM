using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PE.DbEntity.PEContext
{
  partial class TransferCustomContext : BaseDbEntity.PEContext.TransferContext
  {
    public TransferCustomContext(DbContextOptions<BaseDbEntity.PEContext.TransferContext> options)
      : base(options)
    {

    }

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
    //Commented by Av cause of error gen.

    //partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    //{
    //  base.OnModelCreating(modelBuilder);
    //}
  }
}
