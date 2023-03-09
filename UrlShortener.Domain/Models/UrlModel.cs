using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.BLL.Interfaces;

namespace UrlShortener.BLL.Models
{
    public class UrlModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        [BsonElement("originalUrl")]
        public string OriginalUrl { get; set; } = string.Empty;
        [BsonElement("shorlUrl")]
        public string ShortUrl { get; set; } = string.Empty;
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}
