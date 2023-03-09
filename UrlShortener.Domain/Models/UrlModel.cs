using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.BLL.Interfaces;

namespace UrlShortener.BLL.Models
{
    public class UrlModel : IRootModel<ObjectId>
    {
        public ObjectId Id { get; set; }
        public string OriginalUrl { get; set; } = string.Empty;
        public string ShortUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime TriggeredDate { get; set; }
    }
}
