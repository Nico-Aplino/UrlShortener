using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.BLL.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UrlShortener.BLL.Models
{
    public class HitCounterModel 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        [BsonElement("shorlUrlId")]
        public string ShortUrlId { get; set; } = string.Empty;
        [BsonElement("hitDateTime")]
        public DateTime HitDateTime { get; set; }
    }
}
