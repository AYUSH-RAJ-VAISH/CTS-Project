using MovieMicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMicroservice.Repository
{
    public interface IMovieRepository
    {
        public Task<Movie> CreateMovie(Movie movie);
        public Task<Movie> DeleteMovie(int id);
        public Task<Movie> GetMovieById(int id);
        public Task<IEnumerable<Movie>> GetMovies();
        public Task<Movie> PutMovies(int id, Movie movie);
    }
}
