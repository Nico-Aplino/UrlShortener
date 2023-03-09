using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.BLL.Interfaces;
using UrlShortener.BLL.Models;
using UrlShortener.DLL.Persistence;

namespace UrlShortener.DLL.Repositories
{
    public class UrlRepository : IUrlRepository
    {
        private readonly IMongoCollection<UrlModel> _mongoCollection;

        public UrlRepository(IMongoDbSettings _settings, IMongoClient mongoClient)
        {
            IMongoDatabase db = mongoClient.GetDatabase(_settings.DatabaseName);
            _mongoCollection = db.GetCollection<UrlModel>("urlCollection");
        }

        public async Task<UrlModel> Add(UrlModel entity)
        {
            _mongoCollection.InsertOne(entity);
            return entity;
        }
     
        public async Task<UrlModel> GetByOriginalUrl(string url)
        {
            var filter = Builders<UrlModel>.Filter.Eq(u => u.OriginalUrl, url);
            return await _mongoCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<UrlModel> GetByShortUrl(string url)
        {
            var getall = _mongoCollection.Find(x => true).ToList();

            var filter = Builders<UrlModel>.Filter.Eq(u => u.ShortUrl, url);
            return await _mongoCollection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
