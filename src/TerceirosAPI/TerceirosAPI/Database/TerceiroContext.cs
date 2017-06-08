using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TerceirosAPI.Models;

namespace TerceirosAPI.Database
{
    public class TerceiroContext
    {
        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }
        public static bool IsSSL { get; set; }

        private IMongoDatabase _database { get; }

        
        public TerceiroContext()
        {
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
                if (IsSSL)
                {
                    settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                }
                var mongoClient = new MongoClient(settings);
                _database = mongoClient.GetDatabase(DatabaseName);
            }
            catch (Exception ex)
            {
                throw new Exception("Can not access to db server.", ex);
            }
        }

        public IMongoCollection<Terceiro> Terceiros
        {
            get
            {
                return _database.GetCollection<Terceiro>("Terceiros");
            }
        }

        public IMongoCollection<BsonDocument> Object
        {
            get
            {
                return _database.GetCollection<BsonDocument>("Terceiros");
            }
        }

        public IMongoCollection<TerceirosLog> TerceirosLog
        {
            get
            {
                return _database.GetCollection<TerceirosLog>("TerceirosLog");
            }
        }


    }
}
