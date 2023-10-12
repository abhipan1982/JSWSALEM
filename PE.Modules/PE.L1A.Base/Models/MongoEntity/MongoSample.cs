using System;
using MongoDB.Bson.Serialization.Attributes;

namespace PE.L1A.Base.Models.MongoEntity
{
  public class MongoSample
  {
    public double Value { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime TimeStamp { get; set; }
  }
}
