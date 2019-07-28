using MongoDbMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbMovies.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetMovies();
        Task<Movie> GetMovie(string movieId);
        Task AddMovie(Movie movie);
        Task<bool> UpdateMovie(Movie movie);
        Task<bool> RemoveMovie(string movieId);
        Task<IEnumerable<Movie>> SearchMovie(string search);
    }
}
