using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.BLL.Models;
using UrlShortener.BLL.Models.RequestModels;

namespace UrlShortener.DLL.Services.UrlService
{
    public interface IUrlService
    {
        Task<string> GetByShortUrl(string shortUrl);
        Task<string> Save(ShortenUrlCommand command);
    }
}
