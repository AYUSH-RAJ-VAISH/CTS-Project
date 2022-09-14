using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieMicroservice.Controllers;
using MovieMicroservice.Model;
using MovieMicroservice.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieMicroservice.Tests
{
    class MovieControllerTest
    {
        Mock<IMovieRepository> _repository = new Mock<IMovieRepository>();
        MoviesController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new MoviesController(_repository.Object);

        }

        [Test]
        public async Task CheckGetMovies()
        {
            List<Movie> movies = new List<Movie>()
            {
                new Movie() {Id=1,Name="Movie1", Price=250, Description="Awesome Movie", Type="Bollywood"},
                new Movie() {Id=2,Name="Movie2", Price=280, Description="Awesome Movie", Type="Bollywood"},
                new Movie() {Id=3,Name="Movie3", Price=300, Description="Awesome Movie", Type="Hollywood"},
                new Movie() {Id=4,Name="Movie4", Price=250, Description="Awesome Movie", Type="Hollywood"}
            };


            _repository.Setup(x => x.GetMovies()).ReturnsAsync(movies);


            //var response = await _controller.GetProducts();
            //Assert.IsInstanceOf<OkObjectResult>(response.Result);
            //OkObjectResult result = response.Result as OkObjectResult;
            //var returedValues = result.Value as IEnumerable<Product>;
            //Assert.That(returedValues.Count(), Is.EqualTo(products.Count));
            //Assert.That(products, Is.EqualTo(returedValues));



            var response = await _controller.GetMovies();
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
            var result = response.Result as OkObjectResult;
            ResponseObj converted = result.Value as ResponseObj;
            Assert.IsNotNull(converted.Payload);
            var returedValues = converted.Payload as IEnumerable<Movie>;
            Assert.That(returedValues.Count(), Is.EqualTo(movies.Count));
            Assert.That(movies, Is.EqualTo(returedValues));
        }

        [Test]
        public async Task CheckGetMovieById_MoviePresent()
        {
            int id = 1;
            Movie p = new Movie() { Id = 1, Name = "Movie1", Price = 250, Description = "Awesome Movie", Type = "Bollywood" };
            _repository.Setup(x => x.GetMovieById(id)).ReturnsAsync(p);

            //var response = await _controller.GetProduct(id);
            //Assert.IsInstanceOf<OkObjectResult>(response.Result);
            //var result = response.Result as OkObjectResult;
            //Product returedValue = result.Value as Product;
            //Assert.That(returedValue.Id, Is.EqualTo(id));
            //Assert.That(p, Is.EqualTo(returedValue));

            var response = await _controller.GetMovie(id);
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
            var result = response.Result as OkObjectResult;
            ResponseObj converted = result.Value as ResponseObj;
            Assert.IsNotNull(converted.Payload);
            var returedValue = converted.Payload as Movie;
            Assert.That(p, Is.EqualTo(returedValue));

        }

        [Test]
        public async Task CheckGetMovieById_MovieMissing()
        {
            int id = 6;

            _repository.Setup(x => x.GetMovieById(id)).ReturnsAsync((Movie)null);

            var response = await _controller.GetMovie(id);
            Assert.IsInstanceOf<NotFoundResult>(response.Result);
        }

        [Test]
        public async Task CheckPutMovie_ValidInputs()
        {
            Movie movie = new Movie() { Id = 1, Name = "Movie1", Price = 250, Description = "Awesome Movie", Type = "Bollywood" };

            _repository.Setup(x => x.PutMovies(movie.Id, movie)).ReturnsAsync(movie);

            //var response = await _controller.PutProduct(product.Id, product);
            //Assert.IsInstanceOf<OkObjectResult>(response.Result);
            //var result = response.Result as OkObjectResult;
            //var returedValue = result.Value as Product;
            //Assert.That(product.Id, Is.EqualTo(returedValue.Id));

            var response = await _controller.PutMovie(movie.Id, movie);
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
            var result = response.Result as OkObjectResult;
            ResponseObj converted = result.Value as ResponseObj;
            Assert.IsNotNull(converted.Payload);
            var returedValue = converted.Payload as Movie;
            Assert.That(returedValue, Is.EqualTo(movie));
        }

        [Test]
        public async Task CheckPutMovie_InvalidInputs()
        {
            int id = 6;
            Movie movie = new Movie() { Id = 1, Name = "Movie1", Price = 250, Description = "Awesome Movie", Type = "Bollywood" };

            _repository.Setup(x => x.PutMovies(id, movie)).ReturnsAsync(movie);

            var response = await _controller.PutMovie(id, movie);
            Assert.IsInstanceOf<BadRequestResult>(response.Result);
        }

        [Test]
        public async Task CheckPostMovie_ValidInputs()
        {
            Movie movie = new Movie() { Name = "Movie1", Price = 250, Description = "Awesome Movie", Type = "Bollywood" };
            Movie movieFinal = new Movie() { Id = 1, Name = "Movie1", Price = 250, Description = "Awesome Movie", Type = "Bollywood" };

            _repository.Setup(x => x.CreateMovie(movie)).ReturnsAsync(movieFinal);

            //var response = await _controller.PostProduct(product);
            //Assert.IsInstanceOf<CreatedAtActionResult>(response.Result);
            //var result = response.Result as CreatedAtActionResult;
            //var returedValue = result.Value as Product;
            //Assert.IsNotNull(returedValue.Id);

            var response = await _controller.PostMovie(movie);
            Assert.IsInstanceOf<CreatedAtActionResult>(response.Result);
            var result = response.Result as CreatedAtActionResult;
            ResponseObj converted = result.Value as ResponseObj;
            Assert.IsNotNull(converted.Payload);
            var returedValue = converted.Payload as Movie;
            Assert.That(returedValue, Is.EqualTo(movieFinal));
        }
        [Test]
        public async Task CheckPostMovie_InvalidInputs()
        {
            Movie movie = new Movie();

            _repository.Setup(x => x.CreateMovie(movie)).ReturnsAsync(movie);

            var response = await _controller.PostMovie(movie);
            Assert.IsInstanceOf<CreatedAtActionResult>(response.Result);
        }

        [Test]
        public async Task CheckDeleteMovie_MoviePresent()
        {
            Movie movie = new Movie() { Id = 1, Name = "Movie1", Price = 250, Description = "Awesome Movie", Type = "Bollywood" };

            _repository.Setup(x => x.DeleteMovie(movie.Id)).ReturnsAsync(movie);

            //var response = await _controller.DeleteProduct(product.Id);
            //Assert.IsInstanceOf<OkObjectResult>(response.Result);
            //var result = response.Result as OkObjectResult;
            //var returedProduct = result.Value as Product;
            //Assert.That(product.Id, Is.EqualTo(returedProduct.Id));
            //Assert.That(returedProduct, Is.EqualTo(product));

            var response = await _controller.DeleteMovie(movie.Id);
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
            var result = response.Result as OkObjectResult;
            var responseObj = result.Value as ResponseObj;
            var returedMovie = responseObj.Payload as Movie;
            Assert.That(movie.Id, Is.EqualTo(returedMovie.Id));
            Assert.That(returedMovie, Is.EqualTo(movie));
        }

        [Test]
        public async Task CheckDeleteMovie_MovieMissing()
        {
            int id = 10;

            _repository.Setup(x => x.DeleteMovie(id)).ReturnsAsync((Movie)null);

            //var response = await _controller.DeleteProduct(id);
            //Assert.IsInstanceOf<NotFoundResult>(response.Result);

            var response = await _controller.DeleteMovie(id);
            Assert.IsInstanceOf<NotFoundResult>(response.Result);
        }
    }
}
