using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using PE.DbEntity.EnumClasses;


namespace PE.DbEntity.PEContext
{
  partial class PECustomContext : BaseDbEntity.PEContext.PEContext
  {
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var connectionString = configuration.GetConnectionString("PECustomContext");
        optionsBuilder.UseSqlServer(connectionString);
      }
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      foreach (var entityType in modelBuilder.Model.GetEntityTypes())
      {
        if (entityType.ClrType.Name.StartsWith("SP"))
          continue;

        // note that entityType.GetProperties() will throw an exception, so we have to use reflection 
        var properties = entityType.ClrType.GetProperties().Where(p => p.Name.StartsWith("Enum"));
        foreach (var property in properties)
        {
          modelBuilder.Entity(entityType.Name).Property(property.Name)
              .HasConversion(CreateConverter(property));
        }
      }
    }

    private ValueConverter CreateConverter(PropertyInfo property)
    {
      Type t1 = property.PropertyType;
      Type t2 = property.PropertyType.BaseType.GenericTypeArguments[0];

      var converterType = typeof(ValueConverter<,>);
      Type[] typeArgs = { t1, t2 };
      var converter = converterType.MakeGenericType(typeArgs);

      ParameterExpression param1 = Expression.Parameter(t1, "x");
      ParameterExpression param2 = Expression.Parameter(t2, "x");

      Expression forvardCast = Expression.Lambda(Expression.Convert(param1, t2), param1);
      Expression backwardCast = Expression.Lambda(Expression.Convert(param2, t1), param2);
      var converterInstance = Activator.CreateInstance(converter, new object[] { forvardCast, backwardCast, null }) as ValueConverter;

      return converterInstance;
    }
  }
}
