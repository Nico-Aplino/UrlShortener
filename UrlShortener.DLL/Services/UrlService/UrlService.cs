using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.BLL.Interfaces;
using UrlShortener.BLL.Models;

namespace UrlShortener.DLL.Services.UrlService
{
    public class UrlService : IUrlService
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IHitCounterRepository _hitCounterRepository;
        private readonly IValidator<UrlModel> _validator;
        public UrlService(IHitCounterRepository hitCounterRepository, IUrlRepository urlRepository, IValidator<UrlModel> validator)
        {
            _hitCounterRepository = hitCounterRepository;
            _urlRepository = urlRepository;
            _validator = validator;
        }

        public async Task<string> Save(UrlModel url)
        {
            var validationResult = await _validator.ValidateAsync(url);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.Errors.First().ErrorMessage, nameof(url));
            }

            var existingUrl = await _urlRepository.GetByOriginalUrl(url.Id);
            if(existingUrl != null)
            {
                await AddHitCountAsync(existingUrl.ShortUrl);

                return existingUrl.ShortUrl;
            }

            UrlModel urlModel = await _urlRepository.Add(url);

            await AddHitCountAsync(urlModel.ShortUrl);

            return urlModel.ShortUrl;
        }

        public async Task<string> GetByShortUrl(string shortUrl)
        {
            var urlView = await _urlRepository.GetByShortUrl(shortUrl);
            if(urlView == null) return "";

            await AddHitCountAsync(urlView.Id);
            return urlView.OriginalUrl;
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
