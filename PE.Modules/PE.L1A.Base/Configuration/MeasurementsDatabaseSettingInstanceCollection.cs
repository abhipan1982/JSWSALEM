using System.Configuration;

namespace PE.L1A.Base.Configuration
{
  public class MeasurementsDatabaseSettingInstanceCollection : ConfigurationElementCollection
  {
    // Create a property that lets us access an element in the
    // collection with the int index of the element
    public MeasurementsDatabaseSettingInstanceElement this[int index]
    {
      get
      {
        // Gets the MeasurementsDatabaseSettingInstanceElement at the specified
        // index in the collection
        return (MeasurementsDatabaseSettingInstanceElement)BaseGet(index);
      }
      set
      {
        // Check if a MeasurementsDatabaseSettingInstanceElement exists at the
        // specified index and delete it if it does
        if (Count > index && BaseGet(index) != null)
          BaseRemoveAt(index);

        // Add the new MeasurementsDatabaseSettingInstanceElement at the specified
        // index
        BaseAdd(index, value);
      }
    }

    // Create a property that lets us access an element in the
    // collection with the name of the element
    public new virtual MeasurementsDatabaseSettingInstanceElement this[string key]
    {
      get
      {
        // Gets the MeasurementsDatabaseSettingInstanceElement where the name
        // matches the string key specified
        return (MeasurementsDatabaseSettingInstanceElement)BaseGet(key);
      }
      set
      {
        // Checks if a MeasurementsDatabaseSettingInstanceElement exists with
        // the specified name and deletes it if it does
        if (BaseGet(key) != null)
          BaseRemoveAt(BaseIndexOf(BaseGet(key)));

        // Adds the new MeasurementsDatabaseSettingInstanceElement
        BaseAdd(value);
      }
    }

    // Method that must be overriden to create a new element
    // that can be stored in the collection
    protected override ConfigurationElement CreateNewElement()
    {
      return new MeasurementsDatabaseSettingInstanceElement();
    }

    // Method that must be overriden to get the key of a
    // specified element
    protected override object GetElementKey(ConfigurationElement element)
    {
      return ((MeasurementsDatabaseSettingInstanceElement)element).Name;
    }
  }
}
