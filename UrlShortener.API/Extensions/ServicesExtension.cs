using FluentValidation;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UrlShortener.BLL.Interfaces;
using UrlShortener.BLL.Models.RequestModels;
using UrlShortener.BLL.Validations;
using UrlShortener.DLL.Persistence;
using UrlShortener.DLL.Repositories;
using UrlShortener.DLL.Services.UrlService;
using UrlShortener.DLL.Services.UrlShortener;

namespace UrlShortener.API.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.Configure<MongoDBSettings>(configuration.GetSection("MongoDb"));
            services.AddSingleton<IMongoDbSettings>(sp => sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);
            services.AddSingleton<IMongoClient>(s => new MongoClient(configuration.GetValue<string>("MongoDb:ConnString")));

            services.AddScoped<IUrlRepository, UrlRepository>();
            services.AddScoped<IHitCounterRepository, HitCounterRepository>();

            services.AddScoped<IUrlService, UrlService>();
            services.AddScoped<IUrlShortenerService, UrlShortenerService>();

            services.AddScoped<IValidator<ShortenUrlCommand>, ShortenUrlCommandValidator>();

            return services;
        }
    }
}
