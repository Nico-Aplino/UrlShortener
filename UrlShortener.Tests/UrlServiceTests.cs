using FluentValidation;
using Moq;
using System.ComponentModel.DataAnnotations;
using UrlShortener.BLL.Interfaces;
using UrlShortener.BLL.Models;
using UrlShortener.BLL.Models.RequestModels;
using UrlShortener.DLL.Services.UrlService;
using UrlShortener.DLL.Services.UrlShortener;

namespace UrlShortener.Tests
{
    public class UrlServiceTests
    {
        private readonly Mock<IUrlRepository> _urlRepositoryMock = new();
        private readonly Mock<IHitCounterRepository> _hitCounterRepositoryMock = new();
        private readonly Mock<IValidator<ShortenUrlCommand>> _validatorMock = new();
        private readonly Mock<IUrlShortenerService> _urlShortenerServiceMock = new();
        private readonly UrlService _urlService;

        public UrlServiceTests()
        {
            _urlService = new UrlService(
                _hitCounterRepositoryMock.Object,
                _urlRepositoryMock.Object,
                _validatorMock.Object,
                _urlShortenerServiceMock.Object);
        }

        [Fact]
        public async Task Save_ShouldReturnShortUrl_WhenValidUrl()
        {
            // Arrange
            var command = new ShortenUrlCommand { Url = "http://www.example.com" };

            _validatorMock.Setup(x => x.ValidateAsync(command, It.IsAny<CancellationToken>()))
                .Returns(() => Task.FromResult(new FluentValidation.Results.ValidationResult()));

            _urlShortenerServiceMock.Setup(x => x.ShortenUrl(command.Url))
                .ReturnsAsync("abc123");

            _urlRepositoryMock.Setup(x => x.GetByOriginalUrl(command.Url))
                .ReturnsAsync((UrlModel)null);

            _urlRepositoryMock.Setup(x => x.Add(It.IsAny<UrlModel>()))
                .ReturnsAsync(new UrlModel { ShortUrl = "abc123" });

            // Act
            var result = await _urlService.Save(command);

            // Assert
            Assert.Equal("abc123", result);
        }

        [Fact]
        public async Task Save_ShouldReturnExistingShortUrl_WhenExistingUrl()
        {
            // Arrange
            
            var command = new ShortenUrlCommand { Url = "http://www.example.com" };

            _validatorMock.Setup(x => x.ValidateAsync(command, It.IsAny<CancellationToken>()))
                .Returns(() => Task.FromResult(new FluentValidation.Results.ValidationResult()));

            _urlRepositoryMock.Setup(x => x.GetByOriginalUrl(command.Url))
                .ReturnsAsync(new UrlModel { ShortUrl = "abc123" });

            
            // Act
            var result = await _urlService.Save(command);

            // Assert
            Assert.Equal("abc123", result);
        }

        [Fact]
        public async Task GetByShortUrl_ShouldReturnOriginalUrl_WhenValidShortUrl()
        {
            // Arrange
            var shortUrl = "abc123";
            var decodedUrl = "http://www.example.com";
            _urlRepositoryMock.Setup(x => x.GetByShortUrl(It.IsAny<string>()))
                .ReturnsAsync(new UrlModel { OriginalUrl = decodedUrl });
            _hitCounterRepositoryMock.Setup(x => x.Add(It.IsAny<HitCounterModel>()))
                .Returns(() => Task.FromResult(new HitCounterModel()));

            // Act
            var result = await _urlService.GetByShortUrl(shortUrl);

            // Assert
            Assert.Equal(decodedUrl, result);
        }

        [Fact]
        public async Task GetByShortUrl_ShouldReturnEmptyString_WhenInvalidShortUrl()
        {
            // Arrange
            var shortUrl = "invalid";
            _urlRepositoryMock.Setup(x => x.GetByShortUrl(It.IsAny<string>()))
                .ReturnsAsync((UrlModel)null);

            // Act
            var result = await _urlService.GetByShortUrl(shortUrl);

            // Assert
            Assert.Equal("", result);
        }
    }
}