using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.BLL.Interfaces;
using UrlShortener.BLL.Models;
using UrlShortener.BLL.Models.RequestModels;
using UrlShortener.DLL.Services.UrlShortener;

namespace UrlShortener.DLL.Services.UrlService
{
    public class UrlService : IUrlService
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IHitCounterRepository _hitCounterRepository;
        private readonly IValidator<ShortenUrlCommand> _validator;
        private readonly IUrlShortenerService _urlShortenerService;
        public UrlService(IHitCounterRepository hitCounterRepository, IUrlRepository urlRepository, IValidator<ShortenUrlCommand> validator, IUrlShortenerService urlShortenerService)
        {
            _hitCounterRepository = hitCounterRepository;
            _urlRepository = urlRepository;
            _validator = validator;
            _urlShortenerService = urlShortenerService;
        }

        public async Task<string> Save(ShortenUrlCommand command)
        {
            await IsValidUrl(command);

            var existingUrl = await _urlRepository.GetByOriginalUrl(command.Url);
            if(existingUrl != null)
            {
                return existingUrl.ShortUrl;
            }

            var shortUrl = await _urlShortenerService.ShortenUrl(command.Url);

            var urlModel = await _urlRepository.Add(new()
            {
                OriginalUrl= command.Url,
                ShortUrl= shortUrl,
                CreatedAt= DateTime.UtcNow, 
            });

            return urlModel.ShortUrl;
        }

        public async Task<string> GetByShortUrl(string shortUrl)
        {
            var decodedUrl = WebUtility.UrlDecode(shortUrl);

            var urlView = await _urlRepository.GetByShortUrl(decodedUrl);
            if(urlView == null) return "";

            await AddHitCountAsync(urlView.Id);
            return urlView.OriginalUrl;
        }

        private async Task<bool> IsValidUrl(ShortenUrlCommand command)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.Errors.First().ErrorMessage, nameof(command));
            }

            return validationResult.IsValid;
        }

        private async Task AddHitCountAsync(string shortUrlId)
        {
            await _hitCounterRepository.Add(new()
            {
                ShortUrlId = shortUrlId,
                HitDateTime = DateTime.UtcNow,
            });
        }
    }
}
