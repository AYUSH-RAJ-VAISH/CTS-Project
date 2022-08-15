using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBookingMicroservice.Model
{
    public class Booking
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public int NoOfSeats { get; set; }
        public int TotalCost { get; set; }
    }
}
