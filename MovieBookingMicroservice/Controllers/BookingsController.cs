using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieBookingMicroservice.Model;
using MovieBookingMicroservice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBookingMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(BookingsController));
        private readonly IBookingRepository _repository;

        public BookingsController(IBookingRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Bookings
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ResponseObj>> GetBookings()
        {
            try
            {
                _log4net.Info("GetBookings Method Called");
                IEnumerable<Booking> bookings = await _repository.GetBookings();
                return Ok(new ResponseObj { Status = 200, Msg = "All bookings", Payload = bookings });
                //return Ok(products);
            }
            catch
            {
                _log4net.Error("Database error");
                return StatusCode(500);
            }
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseObj>> GetBooking(int id)
        {
            try
            {
                _log4net.Info("GetBooking Method Called");
                var booking = await _repository.GetBookingById(id);

                if (booking == null)
                {
                    return NotFound();
                }

                return Ok(new ResponseObj { Status = 200, Msg = "Booking Found", Payload = booking });
                //return Ok(product);
            }
            catch
            {
                _log4net.Error("Database error");
                return StatusCode(500);
            }
        }

        // PUT: api/Bookings/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseObj>> PutBooking(int id, Booking booking)
        {
            _log4net.Info("PutBooking Method called");
            if (id != booking.Id)
            {
                return BadRequest();
            }
            try
            {
                booking = await _repository.PutBooking(id, booking);
                return Ok(new ResponseObj { Status = 200, Msg = "Update Successful", Payload = booking });
                //return Ok(product);
            }
            catch
            {
                _log4net.Error("Database error");
                return StatusCode(500);
            }
        }

        // POST: api/Bookings
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseObj>> PostBooking([FromBody] Booking booking)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _log4net.Info("PostBooking Method Called");
                    Booking bookingWithId = await _repository.CreateBooking(booking);
                    return CreatedAtAction("PostBooking", new ResponseObj { Status = 200, Msg = "Booking Added", Payload = bookingWithId });
                    //return CreatedAtAction("PostProduct", productWithId);
                }
                else
                {
                    _log4net.Info("Model is not valid in PostBooking");
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
        public async Task<ActionResult<Booking>> DeleteBooking(int id)
        {
            try
            {
                _log4net.Info("DeleteBooking Method Called");
                var booking = await _repository.DeleteBooking(id);
                if (booking == null)
                {
                    return NotFound();
                }

                return Ok(new ResponseObj { Status = 200, Msg = "Deleted Successfully", Payload = booking });
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
