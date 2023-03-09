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
    public class HitCounterRepository : IHitCounterRepository
    {
        private readonly IMongoDbSettings _settings;
        private readonly IMongoCollection<HitCounterModel> _mongoCollection;

        public HitCounterRepository(IMongoClient mongoClient)
        {
            var db = mongoClient.GetDatabase(_settings.DatabaseName);
            _mongoCollection = db.GetCollection<HitCounterModel>("hitCounter");
        }

        public async Task<HitCounterModel> Add(HitCounterModel entity)
        {
            _mongoCollection.InsertOne(entity);
            return entity;
        }
    }
}
