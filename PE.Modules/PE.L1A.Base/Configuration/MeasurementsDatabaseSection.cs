using System.Configuration;

namespace PE.L1A.Base.Configuration
{
  public class MeasurementsDatabaseSection : ConfigurationSection, IMeasurementsDatabaseInstanceSettings
  {
    // Create a property that lets us access the collection
    // of MeasurementsDatabaseSettingElements

    // Specify the name of the element used for the property
    [ConfigurationProperty("Instances")]
    // Specify the type of elements found in the collection
    [ConfigurationCollection(typeof(MeasurementsDatabaseSettingInstanceCollection))]
    public MeasurementsDatabaseSettingInstanceCollection MeasurementsDatabaseSettingInstances
    {
      get
      {
        // Get the collection and parse it
        return (MeasurementsDatabaseSettingInstanceCollection)this["Instances"];
      }
    }
  }
}
