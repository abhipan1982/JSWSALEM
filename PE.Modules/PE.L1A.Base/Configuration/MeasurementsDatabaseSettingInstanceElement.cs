using System.Configuration;

namespace PE.L1A.Base.Configuration
{
  public class MeasurementsDatabaseSettingInstanceElement : ConfigurationElement
  {
    [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
    public string Name
    {
      get
      {
        // Return the value of the 'name' attribute as a string
        return (string)base["name"];
      }
      set
      {
        // Set the value of the 'name' attribute
        base["name"] = value;
      }
    }

    [ConfigurationProperty("server", IsRequired = true)]
    public string Server
    {
      get
      {
        // Return the value of the 'server' attribute as a string
        return (string)base["server"];
      }
      set
      {
        // Set the value of the 'server' attribute
        base["server"] = value;
      }
    }

    [ConfigurationProperty("databaseName", IsRequired = true)]
    public string DatabaseName
    {
      get
      {
        // Return the value of the 'databaseName' attribute as a string
        return (string)base["databaseName"];
      }
      set
      {
        // Set the value of the 'databaseName' attribute
        base["databaseName"] = value;
      }
    }

    [ConfigurationProperty("collectionName", IsRequired = true)]
    public string CollectionName
    {
      get
      {
        // Return the value of the 'collectionName' attribute as a string
        return (string)base["collectionName"];
      }
      set
      {
        // Set the value of the 'collectionName' attribute
        base["collectionName"] = value;
      }
    }
  }
}
