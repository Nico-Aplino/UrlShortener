using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.DLL.Services.UrlShortener
{
    public class UrlShortenerService : IUrlShortenerService
    {
        public async Task<string> ShortenUrl(string url)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(url));
            var base64Url = Convert.ToBase64String(hash)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');

            return base64Url.Substring(0, 7); 
        }
    }
}
