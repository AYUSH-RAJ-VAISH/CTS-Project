using Microsoft.EntityFrameworkCore;
using MovieBookingMicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBookingMicroservice.DBContext
{
    public class BookingContext:DbContext
    {
        public BookingContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Booking> Bookings { get; set; }
    }
}
