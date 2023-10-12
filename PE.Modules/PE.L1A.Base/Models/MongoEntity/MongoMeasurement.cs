using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using PE.L1A.Base.Managers;

namespace PE.L1A.Base.Models.MongoEntity
{
  public class MongoMeasurement
  {
    [BsonId] [JsonIgnore] public ObjectId Id { get; set; }

    public int FeatureCode { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public DateTime? ChunkStartDate { get; set; } 

    public IEnumerable<MongoSample> Samples { get; set; }

    public MongoMeasurement()
    {
      Id = ObjectId.GenerateNewId();
      Samples = new List<MongoSample>();
    }
  }
}
