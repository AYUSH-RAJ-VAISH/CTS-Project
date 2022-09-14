using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieMicroservice.DBContext;
using MovieMicroservice.Model;
using MovieMicroservice.Repository;

namespace MovieMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(MoviesController));
        private readonly IMovieRepository _repository;

        public MoviesController(IMovieRepository repository)
        {
            _repository = repository;
        }


        // GET: api/Movies
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ResponseObj>> GetMovies()
        {
            try
            {
                _log4net.Info("GetMovies Method Called");
                IEnumerable<Movie> movies = await _repository.GetMovies();
                return Ok(new ResponseObj { Status = 200, Msg = "All movies", Payload = movies });
                //return Ok(products);
            }
            catch
            {
                _log4net.Error("Database error");
                return StatusCode(500);
            }
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseObj>> GetMovie(int id)
        {
            try
            {
                _log4net.Info("GetMovie Method Called");
                var movie = await _repository.GetMovieById(id);

                if (movie == null)
                {
                    return NotFound();
                }

                return Ok(new ResponseObj { Status = 200, Msg = "Movie Found", Payload = movie });
                //return Ok(product);
            }
            catch
            {
                _log4net.Error("Database error");
                return StatusCode(500);
            }
        }

        // PUT: api/Movies/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseObj>> PutMovie(int id, Movie movie)
        {
            _log4net.Info("PutMovie Method called");
            if (id != movie.Id)
            {
                return BadRequest();
            }
            try
            {
                movie = await _repository.PutMovies(id, movie);
                return Ok(new ResponseObj { Status = 200, Msg = "Update Successful", Payload = movie });
                //return Ok(product);
            }
            catch
            {
                _log4net.Error("Database error");
                return StatusCode(500);
            }
        }

        // POST: api/Movies
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseObj>> PostMovie([FromBody] Movie movie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _log4net.Info("PostMovie Method Called");
                    Movie movieWithId = await _repository.CreateMovie(movie);
                    return CreatedAtAction("PostMovie", new ResponseObj { Status = 200, Msg = "Movie Added", Payload = movieWithId });
                    //return CreatedAtAction("PostProduct", productWithId);
                }
                else
                {
                    _log4net.Info("Model is not valid in PostMovie");
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                _log4net.Error("Database Error", e);
                return StatusCode(500);
            }
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Movie>> DeleteMovie(int id)
        {
            try
            {
                _log4net.Info("DeleteMovie Method Called");
                var movie = await _repository.DeleteMovie(id);
                if (movie == null)
                {
                    return NotFound();
                }

                return Ok(new ResponseObj { Status = 200, Msg = "Deleted Successfully", Payload = movie });
                //return Ok(product);
            }
            catch
            {
                _log4net.Error("Database Error");
                return StatusCode(500);
            }
        }
    }
}
