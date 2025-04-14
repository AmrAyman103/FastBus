using System;
using System.Collections.Generic;

namespace FastBusMVC.Models
{
    public partial class Booking
    {
        public int BookId { get; set; }
        public int? TripId { get; set; }
        public int? UId { get; set; }
        public string Ticket { get; set; } = null!;
        public DateTime BookingDateTime { get; set; }

        public virtual Trip? Trip { get; set; }
        public virtual User? UIdNavigation { get; set; }
    }
}
