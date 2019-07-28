using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbMovies.Context
{
    public class MoviesContext
    {
        public IMongoDatabase _database = null;

        public MoviesContext(IOptions<Setting> settings)
        {
            try
            {
                var client = new MongoClient(settings.Value.ConnectionString);
                if (client != null)
                {
                    _database = client.GetDatabase(settings.Value.DatabaseName);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public IMongoCollection<Movie> Movies
        {
            get
            {
                 return this._database.GetCollection<Movie>("movie");
            }
        }
    }
}
