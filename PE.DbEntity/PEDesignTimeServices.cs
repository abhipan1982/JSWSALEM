using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace PE.DbEntity
{
  public class PEDesignTimeServices : IDesignTimeServices
  {
    public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
    {
      //System.Diagnostics.Debugger.Launch();
#pragma warning disable EF1001 // Internal EF Core API usage.
      serviceCollection.AddSingleton<ICSharpEntityTypeGenerator, PEEntityTypeGenerator>();
      serviceCollection.AddSingleton<ICSharpDbContextGenerator, PECSharpDbContextGenerator>();
#pragma warning restore EF1001 // Internal EF Core API usage.
    }
  }


  [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "For context generation")]
  class PECSharpDbContextGenerator : CSharpDbContextGenerator
  {
    public PECSharpDbContextGenerator(IProviderConfigurationCodeGenerator providerConfigurationCodeGenerator, IAnnotationCodeGenerator annotationCodeGenerator, ICSharpHelper cSharpHelper) : base(providerConfigurationCodeGenerator, annotationCodeGenerator, cSharpHelper)
    {
    }

    public override string WriteCode(IModel model, string contextName, string connectionString, string contextNamespace, string modelNamespace, bool useDataAnnotations, bool useNullableReferenceTypes, bool suppressConnectionStringWarning, bool suppressOnConfiguring)
    {
      string code = base.WriteCode(model, contextName, connectionString, contextNamespace, modelNamespace, useDataAnnotations, useNullableReferenceTypes, suppressConnectionStringWarning, suppressOnConfiguring);
      //System.Diagnostics.Debugger.Launch();
      var lines = code.Split(Environment.NewLine);

      if(contextName.Equals("PECustomContext") || contextName.Equals("TransferCustomContext"))
      {
        for (var i = 0; i < lines.Length; i++)
        {
          if(lines[i].Contains("DbContext"))
          {
            if (contextName.Equals("PECustomContext"))
              lines[i] = lines[i].Replace("DbContext", "BaseDbEntity.PEContext.PEContext");

            if(lines[i].Contains("TransferCustomContext"))
              lines[i] = lines[i].Replace("DbContext", "BaseDbEntity.PEContext.TransferContext");
          break;
          }

        }
      }
        code = String.Join(Environment.NewLine, lines);

      return code;
    }
  }

  [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "For enums generation")]
  class PEEntityTypeGenerator : CSharpEntityTypeGenerator
  {
    public PEEntityTypeGenerator(IAnnotationCodeGenerator annotationCodeGenerator, ICSharpHelper cSharpHelper) : base(annotationCodeGenerator, cSharpHelper) { }

    /*protected override void GenerateProperties(IEntityType entityType)
    {
      Assembly.GetAssembly(typeof(PEEntityTypeGenerator)).GetTypes();

      var enums = (from x in Assembly.GetAssembly(typeof(PEEntityTypeGenerator)).GetTypes()
                  let y = x.BaseType
                  where !x.IsAbstract && !x.IsInterface &&
                  y != null && y.IsGenericType &&
                  y.GetGenericTypeDefinition() == typeof(GenericEnumType<>)
                  select x).ToDictionary(x => x.Name, x => x);

      foreach (var property in entityType.GetProperties())
      {
        if (property.Name.StartsWith("Enum"))
        {
          var expectedEnumName = property.Name.Replace("Enum", "");

          if (enums.TryGetValue(expectedEnumName, out Type enumType))
          {
            property.ClrType = enumType;
          }
        }
      }

      base.GenerateProperties(entityType);
    }*/

    public override string WriteCode(IEntityType entityType, string @namespace, bool useDataAnnotations, bool useNullableReferenceTypes)
    {
      string code = base.WriteCode(entityType, @namespace, useDataAnnotations, useNullableReferenceTypes);

      // public short EnumWeightSource { get; set; }
      // public WeightSource EnumWeightSource { get; set; }


      var includeEnums = false;
      var lines = code.Split(Environment.NewLine);

      if (!@namespace.Equals("PE.DbEntity.HmiModels"))
      {
        for (var i = 0; i < lines.Length; i++)
        {
          var tokens = lines[i].Split(' ');

          var startIndex = Array.IndexOf(tokens, "public");
          if (startIndex == -1 || tokens.Length <= (startIndex + 2)) continue;

          if (tokens[startIndex + 2].StartsWith("Enum"))
          {
            tokens[startIndex + 1] = tokens[startIndex + 2].Replace("Enum", "");
            includeEnums = true;
          }
          lines[i] = String.Join(' ', tokens);
        }
        if (includeEnums)
        {
          //lines.Prepend("using PE.DbEntity.EnumClasses;");
          //Array.Resize(ref lines, lines.Length + 1);
          lines = lines.Prepend("using PE.DbEntity.EnumClasses;")
            .Prepend("using PE.BaseDbEntity.EnumClasses;")
            .ToArray();
        }
      }

      
      code = String.Join(Environment.NewLine, lines);

      return code;
    }
  }
}
