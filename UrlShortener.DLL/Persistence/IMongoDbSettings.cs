using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.DLL.Persistence
{
    public interface IMongoDbSettings
    {
        public string ConnectionString { get; set; } 
        public string DatabaseName { get; set; }
    }
}
