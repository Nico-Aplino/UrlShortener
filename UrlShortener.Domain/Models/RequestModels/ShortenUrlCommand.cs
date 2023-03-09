using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.BLL.Models.RequestModels
{
    public class ShortenUrlCommand
    {
        public string Url { get; set; } = string.Empty;
    }
}
