using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbMovies.Context;
using MongoDbMovies.Models;

namespace MongoDbMovies.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MoviesContext _context = null;

        public MovieRepository(IOptions<Setting> settings)
        {
            this._context = new MoviesContext(settings);
        }
        public async Task AddMovie(Movie movie)
        {
            try
            {
                await this._context.Movies.InsertOneAsync(movie);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to insert movie", ex);
                throw;
            }
        }

        public async Task<Movie> GetMovie(string movieId)
        {
            try
            {
                var filter_id = Builders<Movie>.Filter.Eq("_id", ObjectId.Parse(movieId));
                //ObjectId internalId = GetInternalId(movieId);
                var movie = await this._context.Movies.Find(filter_id).FirstOrDefaultAsync();
                return movie;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to get movie with id {0}", movieId, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Movie>> SearchMovie(string search)
        {
            try
            {
                //ObjectId internalId = GetInternalId(movieId);
                var movie = await this._context.Movies.Find(mov => mov.Actors.ToLower().Contains(search) || 
                    mov.Director.ToLower().Contains(search) || mov.Title.ToLower().Contains(search)).ToListAsync();
                return movie;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to get movie with search criteria: ", search, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            try
            {
                return await this._context.Movies.Find(movie => true).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not get all movies", ex);
                throw;
            }
        }

        public async Task<bool> RemoveMovie(string movieId)
        {
            try
            {
                DeleteResult result = await this._context.Movies.DeleteOneAsync(
                    Builders<Movie>.Filter.Eq("_id", ObjectId.Parse(movieId)));
                return result.IsAcknowledged && result.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to delete movie with id {0}", movieId, ex);
                throw;
            }
        }

        public async Task<bool> UpdateMovie(Movie movie)
        {
            try
            {
                ReplaceOneResult result = await this._context.Movies.ReplaceOneAsync(
                    Builders<Movie>.Filter.Eq("_id", ObjectId.Parse(movie.Id)), movie);
                return result.IsAcknowledged && result.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to update movie with id {0}", movie.Id, ex);
                throw;
            }
        }
    }
}
