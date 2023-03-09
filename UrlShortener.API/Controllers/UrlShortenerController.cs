using Microsoft.AspNetCore.Mvc;
using UrlShortener.BLL.Models;
using UrlShortener.BLL.Models.RequestModels;
using UrlShortener.DLL.Services.UrlService;

namespace UrlShortener.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UrlShortenerController : ControllerBase
    {
        private readonly IUrlService _urlService;

        public UrlShortenerController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpPost("shorten")]
        public async Task<IActionResult> Shorten([FromBody] ShortenUrlCommand url)
        {
            try
            {
                var shortenedUrl = await _urlService.Save(url);
                return Ok(shortenedUrl);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{shortUrl}")]
        public async Task<IActionResult> RedirectUrl(string shortUrl)
        {
            var originalUrl = await _urlService.GetByShortUrl(shortUrl);
            if (string.IsNullOrEmpty(originalUrl))
            {
                return NotFound("Invalid short URL");
            }

            return Redirect(originalUrl);
        }

    }
}
