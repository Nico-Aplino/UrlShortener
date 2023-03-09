using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.BLL.Interfaces;

namespace UrlShortener.BLL.Models
{
    public class HitCounterModel : IRootModel<ObjectId>
    {
        public ObjectId Id { get; set; }
        public string ShortUrlId { get; set; }
        public DateTime HitDateTime { get; set; }
    }
}
