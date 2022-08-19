using Microsoft.EntityFrameworkCore;
using MovieBookingMicroservice.DBContext;
using MovieBookingMicroservice.Model;
using MovieBookingMicroservice.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBookingMicroservice.Tests
{
    class BookingRepositoryTest: IDisposable
    {
        private BookingContext _context;
        private BookingRepository _sut;
        public List<Booking> GetBookings()
        {
            return new List<Booking>
            {
                new Booking() {Id=1,MovieId=1, UserId=1, NoOfSeats=1, TotalCost=250},
                new Booking() {Id=2,MovieId=2, UserId=1, NoOfSeats=2, TotalCost=560},
                new Booking() {Id=3,MovieId=3, UserId=2, NoOfSeats=2, TotalCost=700},
                new Booking() {Id=4,MovieId=4, UserId=2, NoOfSeats=1, TotalCost=250}
            };
        }

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BookingContext>().UseSqlServer("Data Source=DESKTOP-9VUSAHM;Initial Catalog=booking;Integrated Security=True").Options;
            _context = new BookingContext(options);
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
        public async Task Check_GetBookings()
        {
            List<Booking> bookings = GetBookings();
            _context.Bookings.AddRange(bookings);
            _context.SaveChanges();

            _sut = new BookingRepository(_context);

            var result = await _sut.GetBookings();

            Assert.That(result, Is.Not.Null);
            var returnedValue = result as List<Booking>;
            Assert.That(returnedValue, Is.Not.Null);
            Assert.That(returnedValue.Count, Is.EqualTo(bookings.Count));
        }

        [Test]
        public async Task Check_GetBookingById()
        {
            int id = 2;
            _context.Bookings.AddRange(GetBookings());
            _context.SaveChanges();
            _sut = new BookingRepository(_context);

            var result = await _sut.GetBookingById(id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(id));
        }

        [Test]
        public async Task Check_CreateBooking()
        {
            List<Booking> movies = GetBookings();
            _context.Bookings.AddRange(movies);
            _context.SaveChanges();
            _sut = new BookingRepository(_context);
            Booking p = new Booking() { MovieId = 1, UserId = 1, NoOfSeats = 1, TotalCost = 250 };

            await _sut.CreateBooking(p);

            Assert.That(_context.Bookings.Count, Is.EqualTo(5));

        }

        [Test]
        public async Task Check_DeleteBooking()
        {
            int id = 2;
            List<Booking> movies = GetBookings();
            _context.Bookings.AddRange(GetBookings());
            _context.SaveChanges();
            _sut = new BookingRepository(_context);

            await _sut.DeleteBooking(id);

            Assert.That(_context.Bookings.Count, Is.EqualTo(3));

        }

        [Test]
        public async Task Check_PutBookings()
        {
            int id = 1;
            List<Booking> bookings = GetBookings();
            Booking p = bookings.Find(x => x.Id == id);
            int newNoOfSeats = 2;
            p.NoOfSeats = newNoOfSeats;
            _context.Bookings.AddRange(bookings);
            _context.SaveChanges();
            _sut = new BookingRepository(_context);

            Booking returnedMovie = await _sut.PutBooking(id, p);

            Assert.That(p, Is.EqualTo(returnedMovie));


        }
    }
}
