using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace PE.Helpers
{
  public static class Helpers
  {
    /// <summary>
    ///   Extension for string to encode utf8 'chars'
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string ToUtf8(this string input)
    {
      string result = "";
      byte[] bytes = Encoding.Default.GetBytes(input);
      result = Encoding.UTF8.GetString(bytes);

      return result;
    }

    public static string SerializeObject<T>(this T toSerialize)
    {
      XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

      using (StringWriter textWriter = new StringWriter())
      {
        xmlSerializer.Serialize(textWriter, toSerialize);
        return textWriter.ToString();
      }
    }

    public static double GetRandomNumber(double minimum, double maximum, int roundToPlaces = 2)
    {
      Random random = new Random();
      double input = (random.NextDouble() * (maximum - minimum)) + minimum;

      return Math.Round(input, roundToPlaces);
    }
  }
}
