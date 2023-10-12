using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace PE.CommunicationTracer.Core.Converters
{
  public class StatusToColorConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is ModuleStatus && value != null)
      {
        ModuleStatus status = (ModuleStatus)value;
        var color = new SolidColorBrush(Colors.White);

        switch (status)
        {
          case ModuleStatus.Connected:
            color = new SolidColorBrush(Colors.Green);
            break;
          case ModuleStatus.Connecting:
            color = new SolidColorBrush(Colors.Orange);
            break;
          case ModuleStatus.Disconnected:
            color = new SolidColorBrush(Colors.Red);
            break;
          default:
            color = new SolidColorBrush(Colors.White);
            break;
        }

        return color;
      }

      return Binding.DoNothing;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotSupportedException();
    }
  }
}
