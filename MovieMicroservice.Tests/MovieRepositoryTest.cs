using Microsoft.EntityFrameworkCore;
using MovieMicroservice.DBContext;
using MovieMicroservice.Model;
using MovieMicroservice.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMicroservice.Tests
{
    public class MovieRepositoryTest: IDisposable
    {
        private MovieContext _context;
        private MovieRepository _sut;
        public List<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie() {Name="Movie1", Price=250, Description="Awesome Movie", Type="Bollywood"},
                new Movie() {Name="Movie2", Price=280, Description="Awesome Movie", Type="Bollywood"},
                new Movie() {Name="Movie3", Price=300, Description="Awesome Movie", Type="Hollywood"},
                new Movie() {Name="Movie4", Price=250, Description="Awesome Movie", Type="Hollywood"}
            };
        }

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MovieContext>().UseSqlServer("Data Source=DESKTOP-9VUSAHM;Initial Catalog=movie;Integrated Security=True").Options;
            _context = new MovieContext(options);
            _context.Database.EnsureCreated();
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();

        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Check_GetMovies()
        {
            List<Movie> movies = GetMovies();
            _context.Movies.AddRange(movies);
            _context.SaveChanges();

            _sut = new MovieRepository(_context);

            var result = await _sut.GetMovies();

            Assert.That(result, Is.Not.Null);
            var returnedValue = result as List<Movie>;
            Assert.That(returnedValue, Is.Not.Null);
            Assert.That(returnedValue.Count, Is.EqualTo(movies.Count));
        }

        [Test]
        public async Task Check_GetMovieById()
        {
            int id = 2;
            _context.Movies.AddRange(GetMovies());
            _context.SaveChanges();
            _sut = new MovieRepository(_context);

            var result = await _sut.GetMovieById(id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(id));
        }

        [Test]
        public async Task Check_CreateMovie()
        {
            int c = _context.Movies.Count();
            List<Movie> movies = GetMovies();
            _context.Movies.AddRange(movies);
            _context.SaveChanges();
            _sut = new MovieRepository(_context);
            Movie p = new Movie() { Name = "Movie1", Price = 250, Description = "Awesome Movie", Type = "Bollywood" };

            await _sut.CreateMovie(p);

            Assert.That(_context.Movies.Count, Is.EqualTo(c+5));

        }

        [Test]
        public async Task Check_DeleteMovie()
        {
            int id = 2;
            List<Movie> movies = GetMovies();
            _context.Movies.AddRange(GetMovies());
            _context.SaveChanges();
            _sut = new MovieRepository(_context);

            await _sut.DeleteMovie(id);

            Assert.That(_context.Movies.Count, Is.EqualTo(3));

        }

        [Test]
        public async Task Check_PutMovies()
        {
            int id = 1;
            List<Movie> movies = GetMovies();
            _context.Movies.AddRange(movies);
            _context.SaveChanges();
            Movie p = _context.Movies.Find(id);
            string newName = "Test";
            p.Name = newName;
            _context.SaveChanges();
            _sut = new MovieRepository(_context);

            Movie returnedMovie = await _sut.PutMovies(id, p);

            Assert.That(p, Is.EqualTo(returnedMovie));


        }
    }
}