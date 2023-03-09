using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.BLL.Models;

namespace UrlShortener.DLL.Services.UrlService
{
    public interface IUrlService
    {
        Task<string> GetByShortUrl(string shortUrl);
        Task<string> Save(UrlModel urlModel);
    }
}
