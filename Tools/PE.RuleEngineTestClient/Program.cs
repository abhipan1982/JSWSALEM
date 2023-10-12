using PE.QEX.Base.Module;
using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Net;
using System.Threading.Tasks;

namespace PE.RuleEngineTestClient
{
  internal class Program
  {
    static async Task Main(string[] args)
    {
      var rootCommand = new RootCommand
      {
          new Option<string>(
              new[] { "--ip", "-i" },
              getDefaultValue: () => "127.0.0.1",
              description: "Rule Engine server ip"),
          new Option<int>(
              new[] { "--port", "-p" },
              getDefaultValue: () => 8090,
              description: "Rule Engine server port")
      };

      rootCommand.AddValidator(commandResult =>
      {
        if (!commandResult.Children.Contains("-v") ||
            !commandResult.Children.Contains("-t") ||
            !commandResult.Children.Contains("-r"))
        {
          return "Options '-v', '-r' or '-t' is required.";
        }

        return null;
      });

      rootCommand.Description = "To receive a rule mapping use -r -n ruleName";

      var c1 = new Command("getTriggers", "Call getRulesTriggerEvents()");
      c1.AddAlias("-t");
      c1.Handler = CommandHandler.Create<string, int>((ip, port) =>
      {
        Console.WriteLine($"calling getTriggers at {ip}:{port}");

        try
        {
          var client = new SimpleRuleEngineClient(IPAddress.Parse(ip), port);

          var triggers = client.GetRulesTriggerEvents().GetAwaiter().GetResult();
          Console.WriteLine("Triggers:");
          foreach (var trigger in triggers)
            Console.WriteLine("  - " + trigger);
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex);
        }

      });
      rootCommand.Add(c1);

      var c2 = new Command("getRules", "Call getRuleMappingForTriggerEvent(triggerName)")
      {
        new Option<string>(new[] { "--triggerName", "-n" }, "Name of the trigger.") { IsRequired = true },
      };
      c2.AddAlias("-r");
      c2.Handler = CommandHandler.Create<string, int, string>((ip, port, triggerName) =>
      {
        Console.WriteLine($"calling rules for {triggerName} at {ip}:{port}");

        try
        {
          var client = new SimpleRuleEngineClient(IPAddress.Parse(ip), port);

          var mappings = client.GetRuleMappingForTriggerEvent(triggerName).GetAwaiter().GetResult();
          Console.WriteLine("Mappings:");

          foreach (var mapping in mappings.RuleMappings)
            Console.WriteLine($"  - {mapping.Direction} {mapping.Type} {mapping.Identifier.Identifier} {mapping.RulesIdentifier}");
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex);
        }
      });
      rootCommand.Add(c2);

      var c3 = new Command("server", "Run test server");
      c3.AddAlias("-s");
      c3.Handler = CommandHandler.Create<string, int>((ip, port) =>
      {
        Console.WriteLine($"starting test server at {port}");

        try
        {
          TestQualitySAAsyncHandler.StartTestServer(/*port*/).GetAwaiter().GetResult();
          Console.WriteLine("Press [ENTER] to stop server...");
          Console.ReadLine();
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex);
        }

      });
      rootCommand.Add(c3);

      var c4 = new Command("getRevision", "Call GetRevision()");
      c4.AddAlias("-v");
      c4.Handler = CommandHandler.Create<string, int>((ip, port) =>
      {
        Console.WriteLine($"calling getRevision at {ip}:{port}");

        try
        {
          var client = new SimpleRuleEngineClient(IPAddress.Parse(ip), port);

          var revision = client.GetRevision().GetAwaiter().GetResult();
          Console.WriteLine("Revision: " + revision);
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex);
        }

      });
      rootCommand.Add(c4);

      await rootCommand.InvokeAsync(args);
    }
  }
}
